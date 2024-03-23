using MongoDB.Driver;
namespace DotNetBank
{
    public interface IAccountDatabase 
    {
        public ResponseAccount CreateAccount(CreateAccountRequest createAccountRequest);
        public ResponseUpdateAccount UpdateAccount(UpdateAccountRequest updateAccountRequest);
        public CreateAccountRequest GetAccountbyAccountNumber(int AccountNumber);
        public DeleteAccountResponse DeleteAccount(DeleteAccountRequest deleteAccountRequest);
    }
}