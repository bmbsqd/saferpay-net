# Made my Bombsquad Inc #

----------
## How to run tests
Under `SaferPay.Tests` there's a folder called `secrets`. (It has a .gitignore to ignore all your secrets, so don't worry about checking stuff in, as long as you know what you're doing)

For the `ApiTests` you need to get some test settings from *SaferPay* and put in `saferpay-test.json`. It should look something like this:

```json
{
	"baseUri": "https://test.saferpay.com/api/",
	"username": "API_123456_1234567",
	"password": "JsonApiPwd1_abc123abc",
	"customerId": "12345",
	"terminalId": "12345123"
}
```


If you want to run the IntegrationTest, that puts `1 CHF` on your creditcard, you have to fill in `saferpay.json` with your real JSON API data (same format as above) and also `saferpay-creditcard.json`

```json
{
  "Number": "9070101052000009",
  "ExpYear": 2019,
  "ExpMonth": 7,
  "HolderName": "Bombsquad Inc",
  "VerificationCode": "1234"
}
```

Use what you want as long as, you know, http://creativecommons.org/licenses/by-sa/3.0/
