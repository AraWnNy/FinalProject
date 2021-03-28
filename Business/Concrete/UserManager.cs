using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            try
            {
                return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
            }
            catch (Exception e)
            {
                return new ErrorDataResult<List<OperationClaim>>(e.InnerException.ToString());
            }
        }

        public IResult Add(User user)
        {
            try
            {
                _userDal.Add(user);
                return new SuccessResult();
            }
            catch (Exception e)
            {
                return new ErrorResult(e.InnerException.ToString());
            }
        }

        public IDataResult<User> GetByMail(string email)
        {
            try
            {
                return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
            }
            catch (Exception e)
            {
                return new ErrorDataResult<User>(e.InnerException.ToString());
            }
        }
    }
}
