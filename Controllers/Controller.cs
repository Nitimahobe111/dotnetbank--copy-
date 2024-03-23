using Microsoft.AspNetCore.Mvc;

namespace DotNetBank
{
    [ApiController]
    [Route("[controller]")]
    public class AccountUpdateController : Controller
    {
        private readonly IAccountUpdateService _accountUpdateService;
        public AccountUpdateController(IAccountUpdateService accountUpdateService)
        {
            _accountUpdateService = accountUpdateService;
        }
        [HttpPut]
        [Route("/AccountUpdate")]
        public IActionResult UpdateAccountRequest([FromBody] UpdateAccountRequest updateAccountRequest)
        {
            try
            {
                ResponseUpdateAccount response = _accountUpdateService.AccountUpdateServices(updateAccountRequest);
                return Ok(response);
            }

            catch (Exception exception)
            {
                return BadRequest(new { Error = exception.Message });


            }
        }
        [HttpPost]
        [Route("/CreateAccountRequest")]
        public IActionResult CreateAccount([FromBody] CreateAccountRequest createAccountRequest)
        {
            try
            {
                ResponseAccount responseAccount = _accountUpdateService.CreateAccountServices(createAccountRequest);

                return Ok(responseAccount);
            }

            catch (Exception exception)
            {
                return BadRequest(new { Error = exception.Message });

            }

        }
        [HttpPost]
        [Route("/CreateBank")]

        public IActionResult CreateBankRequest([FromBody] CreateBankRequest createBankRequest)
        {
            try
            {
                ResponseBank responseBank = _accountUpdateService.CreateBankServices(createBankRequest);
                return Ok(responseBank);
            }
            catch (Exception exception)
            {
                return BadRequest(new { Error = exception.Message });
            }

        }
        [HttpPost]
        [Route("/CreateBranch")]
        public IActionResult Branch([FromBody] Branch branch)
        {
            try
            {
                ResponseBranch responseBranch = _accountUpdateService.CreateBranchServices(branch);
                return Ok(responseBranch);
            }
            catch (Exception exception)
            {
                return BadRequest(new { Error = exception.Message });


            }
        }

        [HttpDelete]
        [Route("/DeleteAccount")]

        public IActionResult DeleteAccountRequest([FromBody] DeleteAccountRequest DeleteAccountRequest)
        {
            try
            {
                DeleteAccountResponse deleteAccountResponse = _accountUpdateService.DeleteAccountServices(DeleteAccountRequest);


                return Ok(deleteAccountResponse);
            }
            catch (Exception exception)
            {
                return BadRequest(new { Error = exception.Message });
            }

        }
        [HttpPost]
        [Route("/EMICalculator")]
        public IActionResult EMICalculator([FromBody] EMICalculatorRequest eMICalculatorRequest)
        {
            try
            {
                EMICalculatorResponse eMICalculatorResponse = _accountUpdateService.EMICalculatorServices(eMICalculatorRequest);

                return Ok(eMICalculatorResponse);
            }

            catch (Exception exception)
            {
                return BadRequest(new { Error = exception.Message });

            }

        }

        [HttpGet]
        [Route("/GetAccount/{AccountNumber}")]
        public IActionResult GetAccount(int AccountNumber)
        {
            try
            {
                CreateAccountRequest createAccountRequest = _accountUpdateService.GetAccountDetailsbyAccountNumber(AccountNumber);

                return Ok(createAccountRequest);
            }

            catch (Exception exception)
            {
                return BadRequest(new { Error = exception.Message });

            }
        }

        [HttpGet]
        [Route("/GetInfo/{IFSC}")]
        public IActionResult GetInfo(string IFSC)
        {
            try
            {
                CreateBankRequest response = _accountUpdateService.GetBankbyIFSC(IFSC);
                return Ok(response);
            }
            catch (Exception exception)
            {
                return BadRequest(new { Error = exception.Message });
            }
        }
      
        [HttpGet]
        [Route("/GetBranch/{BankName}")]
        public IActionResult GetBranch(string BankName)
        {
            try
            {
                List<Branch> branches = _accountUpdateService.GetBranch(BankName);

                if (branches.Capacity == 0) return BadRequest(new { Error = "No branch is availabe with " + BankName + "Bank Name" });
                return Ok(branches);
            }

            catch (Exception exception)
            {
                return BadRequest(new { Error = exception.Message });


            }
        }

        [HttpGet]
        [Route("/GetTransactionbyMonth")]
        public IActionResult GetTransactionsByMonth()
        {
            try
            {
                List<Transactions> transactions = _accountUpdateService.GetTransactionByMonth();
                return Ok(transactions);
            }
            catch (Exception exception)
            {
                return BadRequest(new { Error = exception.Message });


            }

        }
        [HttpPost]
        [Route("/GetTransactionbyDate")]
        public IActionResult GetTransactionsByDate(TransactionByDate transactionByDate)
        {
            try
            {
                string From = transactionByDate.From;
                string To = transactionByDate.To;
                List<Transactions> transactions = _accountUpdateService.GetTransactionbyDateService(From, To);
                return Ok(transactions);
            }
            catch (Exception exception)
            {
                return BadRequest(new { Error = exception.Message });

            }
        }

        [HttpPost("/Transaction")]
        public IActionResult Transaction([FromBody] Transactions transactions)
        {
            try
            {
                ResponseTransactions responseTransactions = _accountUpdateService.TransactionServices(transactions);
                return Ok(responseTransactions);
            }
            catch (Exception exception)
            {
                return BadRequest(new { Error = exception.Message });
            }
        }
    }
}

