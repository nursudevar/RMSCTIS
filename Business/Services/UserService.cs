using Azure.Identity;
using Business.Models;
using DataAccess_.Contexts;
using DataAccess_.Entities;
using DataAccess_.Results;
using DataAccess_.Results.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{

    public interface IUserService
    {
        IQueryable<UserModel> Query();

        Result Add(UserModel model);

        Result Update(UserModel model);

        Result Delete(int Id);
    }
    public class UserService : IUserService
    {
        private readonly Db _db;

        public UserService(Db db)
        {
            _db = db;
        }


        public Result Add(UserModel model)
        {

            List<User> existingUsers = _db.Users.ToList();

            if (_db.Users.Any(e => e.UserName.ToLower() == model.UserName.ToLower().Trim()))
                return new ErrorResult("User Names with the same name exists");
            User entity = new User()
            {
                UserName = model.UserName.Trim(),
                IsActive =model.isActive,
                RoleId = model.RoleId,
                Password = model.Password,
                Status = model.Status,
            };

            _db.Users.Add(entity);
            _db.SaveChanges();

            return new SuccessResult("User Addedd Successfully");
        }

        public Result Delete(int Id)
        {

            User entity = _db.Users.SingleOrDefault(e => e.Id == Id);
            if (entity is null)
                return new ErrorResult("User not found");
            if (entity.Role is not null)
                return new ErrorResult("USer cannot be deleted because it has relational role");
            _db.Users.Remove(entity);
            _db.SaveChanges();
            return new SuccessResult("User deleted successfully");

        }

        public IQueryable<UserModel> Query()
        {

            return _db.Users.OrderByDescending(e => e.IsActive).ThenBy(e => e.UserName).Select(e => new UserModel()  
            {
                Id= e.Id,
                isActive=e.IsActive,
                Password =e.Password,
                RoleId=e.RoleId,
                Status=e.Status,
                UserName=e.UserName,

            });
        }

        public Result Update(UserModel model)
        {
            if (_db.Users.Any(e => e.Id != model.Id && e.UserName.ToLower() == model.UserName.ToLower().Trim()))
                return new ErrorResult("User Names with the same name exists");

            User entity = _db.Users.SingleOrDefault(e => e.Id == model.Id);
            if (entity is null)
                return new ErrorResult("User not found");
            entity.UserName = model.UserName.Trim();
            

            _db.Users.Update(entity);
            _db.SaveChanges();

            return new SuccessResult("User Updated Successfully");
        }
    }
}
