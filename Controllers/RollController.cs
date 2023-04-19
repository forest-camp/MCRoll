using MCRoll.Data;
using MCRoll.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MCRoll.Controllers
{
    public class RollController : Controller
    {
        private readonly MCRollDbContext _context;
        private IConfiguration _configuration { get; }
        private ILogger _logger;

        public RollController(MCRollDbContext context, IConfiguration configuration, ILogger<RollController> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "健♂身♂房";

            return View();
        }

        // GET: Roll/Detail/5
        public async Task<IActionResult> Detail(Int32 id)
        {
            var roll = await _context.Rolls
                .Include(r => r.Participants)
                .Include(r => r.Winners)
                .FirstOrDefaultAsync(r => r.RollId == id);
            if (roll == null)
            {
                return NotFound();
            }

            ViewData["Title"] = roll.Name;
            return View(roll);
        }

        // GET: Roll/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "想要摔♂跤";

            return View();
        }

        // POST: Roll/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RollVM model)
        {
            if (ModelState.IsValid)
            {
                Roll roll = model.ToRoll();
                _context.Add(roll);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // POST: Roll/Join
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Join(ParticipantVM model)
        {
            if (ModelState.IsValid)
            {
                Participant participant = model.ToParticipant();
                _context.Add(participant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Detail), new { id = model.RollId });
            }
            return RedirectToAction(nameof(Detail), new { id = model.RollId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Close(Int32 id)
        {
            var roll = await this._context.Rolls
                .Include(r => r.Participants)
                .Include(r => r.Winners)
                .SingleOrDefaultAsync(r => r.RollId == id);
            if (roll == default(Roll))
            {
                return NotFound();
            }

            roll.IsOpen = false;
            var winners = GetWinners(roll.Participants.ToList(), roll.WinnerNumber);
            roll.Winners = winners;
            await _context.SaveChangesAsync();

            try
            {
                var mailEnable = _configuration.GetValue<Boolean>("MAIL_ENABLE");
                if (mailEnable)
                    await SendMailAsync(winners, roll);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Send Mail Error");
            }

            return RedirectToAction(nameof(Detail), new { id });
        }

        /// <summary>
        /// 随机抽取Winner
        /// </summary>
        /// <param name="participants">参与者</param>
        /// <param name="nums">抽取人数</param>
        /// <returns></returns>
        private IEnumerable<Winner> GetWinnersOld(List<Participant> participants, Int32 nums)
        {
            List<Participant> wp = new List<Participant>();
            Random random = new Random(Guid.NewGuid().GetHashCode());

            while (wp.Count < nums)
            {
                Int32 n = random.Next(0, participants.Count() - 1);
                if (!wp.Contains(participants[n]))
                {
                    wp.Add(participants[n]);
                }
            }

            foreach (var p in wp)
            {
                yield return Winner.FromParticipant(p);
            }
        }

        /// <summary>
        /// 随机抽取Winner
        /// </summary>
        /// <param name="participants">参与者</param>
        /// <param name="nums">抽取人数</param>
        /// <returns></returns>
        private List<Winner> GetWinners(List<Participant> participants, Int32 nums)
        {
            List<Winner> winner = new List<Winner>(nums);

            if (nums >= participants.Count)
            {
                participants.ForEach(p => winner.Add(Winner.FromParticipant(p)));
            }
            else
            {
                Random random = new Random(Guid.NewGuid().GetHashCode());

                for (var i = 0; i < nums; i++)
                {
                    winner.Add(Winner.FromParticipant(participants[i]));
                }

                for (var i = nums; i < participants.Count; i++)
                {
                    Int32 r = random.Next(i + 1);
                    if (r < nums)
                    {
                        winner[r] = Winner.FromParticipant(participants[i]);
                    }
                }
            }
            return winner;
        }

        private async Task SendMailAsync(List<Winner> winners, Roll roll)
        {
            var mailSender = _configuration.GetValue<String>("MAIL_SENDER");
            var mailPassword = _configuration.GetValue<String>("MAIL_PASSWORD");
            var mailHost = _configuration.GetValue<String>("MAIL_HOST");
            var mailPort = _configuration.GetValue<Int32>("MAIL_PORT");

            Utils.MailKit mailkit = new Utils.MailKit()
            {
                Sender = mailSender,
                Subject = "你中奖了，出来挨打",
                Text = $"你在 \"{roll.Creator}\" 创建的 roll \"{roll.Name}\" 中奖了，出来挨打",
                Username = mailSender,
                Password = mailPassword,
                Host = mailHost,
                Port = mailPort,
                ImagePath = "wwwroot/images/aida.jpg",
            };
            foreach (var winner in winners)
            {
                mailkit.Receivers.Add(winner.Email);
            }
            mailkit.Init();
            await mailkit.SendAsync();
        }
    }
}