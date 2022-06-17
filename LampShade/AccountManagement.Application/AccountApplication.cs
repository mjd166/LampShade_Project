using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using System;
using System.Collections.Generic;

namespace AccountManagement.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAccountRepository _accountRepository;
        private readonly IAuthHelper _authHelper;

        public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher, IFileUploader fileUploader, IAuthHelper authHelper)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _fileUploader = fileUploader;
            _authHelper = authHelper;
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            var operation = new OperationResult();

            var account = _accountRepository.Get(command.Id);
            if (account == null) return operation.Failed(ApplicationMessages.RecordNotFound);

            if (command.Password != command.RePassword)
                return operation.Failed(ApplicationMessages.PasswordNotMatch);

            var password = _passwordHasher.Hash(command.Password);

            account.ChangePassword(password);
            _accountRepository.Savechanges();
            return operation.Succedded();
        }

        public OperationResult Create(CreateAccount command)
        {
            var operation = new OperationResult();

            if (_accountRepository.Exist(x => x.Username == command.Username || x.Mobile==command.Mobile))
                return operation.Failed(ApplicationMessages.DoublicatedRecord);

            var password = _passwordHasher.Hash(command.Password);
            string path = $"ProfilePhotos";
            string filepath = _fileUploader.Upload(command.ProfilePhoto, path);
            var account = new Account(command.Fullname, command.Username, password, command.Mobile, command.RoleId, filepath);
            _accountRepository.Create(account);
            _accountRepository.Savechanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);
            if (account == null) return operation.Failed(ApplicationMessages.RecordNotFound);
            if (_accountRepository.Exist(x => x.Mobile == command.Mobile  && account.Id != command.Id || 
                                        (x.Username == command.Username && account.Id!=command.Id)))
                                         
                return operation.Failed(ApplicationMessages.DoublicatedRecord); 
            string path = $"Profilephotos";
            string filepath = _fileUploader.Upload(command.ProfilePhoto, path);
            account.Edit(command.Fullname, command.Username, command.Mobile, command.RoleId, filepath);
            _accountRepository.Savechanges();
            return operation.Succedded();
        }

        public EditAccount GetDetails(long id)
        {
            return _accountRepository.GetDetails(id);
        }

        public OperationResult Login(Login command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetBy(command.Username);
            if (account == null) return operation.Failed(ApplicationMessages.WrongUserPass);
            (bool Verified, bool NeedsUpgrade) result = _passwordHasher.Check(account.Password,command.Password);
            if (!result.Verified)
                return operation.Failed(ApplicationMessages.WrongUserPass);

            AuthViewModel _account = new AuthViewModel(account.Id, account.RoleId, account.Fullname, account.Username);
           

            _authHelper.SignIn(_account);
            return operation.Succedded();

        }

        public void Logout()
        {
            _authHelper.SignOut();
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {

            return _accountRepository.Search(searchModel);
        }
    }
}
