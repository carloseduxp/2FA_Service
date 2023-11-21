# 2FA_Service
Test app

Testing steps
1) Create db: 2FA_Service
2) Run the script file: 2FA_Service_tableCreate.sql
3) Open solution and run
4) You may use postman to test controller actions (POST and GET methods)

Samples
POST https://localhost:7126/_2FA
Payload (raw text) "94832942" as Json

GET https://localhost:7126/_2FA?phoneNumber=94832942&phoneCode=RJ45LPK5KFSS3SX5ULXJ
