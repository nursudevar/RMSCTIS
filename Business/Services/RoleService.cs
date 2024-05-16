using Business.Models;
using DataAccess_.Contexts;
using DataAccess_.Entities;
using DataAccess_.Results;
using DataAccess_.Results.Bases;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
	public interface IRoleService
	{
		IQueryable<RoleModel> Query();
		Result Add(RoleModel model);
		Result Update(RoleModel model);
		Result Delete(int id);
	}


	public class RoleService : IRoleService
	{
		private readonly Db _db;

		public RoleService(Db db)
		{
			_db = db;
		}

		public IQueryable<RoleModel> Query()
		{
			return _db.Roles.Include(e => e.Users).OrderBy(e => e.Name).Select(e => new RoleModel()
			{
				Id = e.Id,
				Name = e.Name,

				UserCountOutput = e.Users.Count 
			});
		}


		public Result Delete(int Id)
		{
			var existingEntity = _db.Roles.Include(r => r.Users).SingleOrDefault(r => r.Id == Id);
			if (existingEntity is null)
				return new ErrorResult("Role not found!");

			if (existingEntity.Users.Any())
				return new ErrorResult("Role can't be deleted because it has users!");

			_db.Roles.Remove(existingEntity);
			_db.SaveChanges();
			return new SuccessResult("Role deleted successfully.");
		}

		Result IRoleService.Add(RoleModel model)
		{
			

            if (_db.Roles.Any(r => r.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("Role with the same title and date exists!");

           

			var entity = new Role()
			{
				Name = model.Name.Trim()
			};

			_db.Roles.Add(entity);
			_db.SaveChanges();


			return new SuccessResult("Role added successfully.");



		}

	

		Result IRoleService.Update(RoleModel model)
		{

            if (_db.Roles.Any(r => r.Name.ToLower() == model.Name.ToLower().Trim() && r.Id!= model.Id))
                return new ErrorResult("Role with the same title and date exists!");





			var entity = new Role()
			{
				Id = model.Id,
				Name = model.Name.Trim()
			};

			
			_db.Roles.Update(entity);
			_db.SaveChanges();
			return new SuccessResult("Role updated successfully.");
		}
	}
}
