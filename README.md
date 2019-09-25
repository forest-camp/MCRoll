# MCRoll
Midwood Camp Roll  
[![Docker](https://img.shields.io/badge/docker-latest-blue?logo=docker)](https://hub.docker.com/r/illyathehath/mcroll)
[![Docker Builds](https://img.shields.io/docker/cloud/build/illyathehath/mcroll)](https://github.com/midwoodcamp/MCRoll/blob/master/LICENSE)
[![License](https://img.shields.io/github/license/midwoodcamp/MCRoll)](https://hub.docker.com/r/illyathehath/mcroll/builds)

### Requirement
- mysql 8.0+, character set utf8mb4

### Docker Environment Variables

```
DB_HOST         数据库地址
DB_PORT         数据库端口
DB_USER         数据库用户名
DB_PASSWORD     数据库密码
DB_NAME         数据库名

MAIL_ENABLE     是否启用邮件通知(SMTP)
MAIL_SENDER     发件人SMTP登录名
MAIL_PASSWORD   发件人SMTP登录密码
MAIL_HOST       SMTP地址
MAIL_PORT       SMTP端口
```