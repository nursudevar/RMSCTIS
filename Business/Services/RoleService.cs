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
				// model - entity property assignments
				Id = e.Id,
				Name = e.Name,

				// modified model - entity property assignments for displaying in views
				UserCountOutput = e.Users.Count // display the user count for each role
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
			
			var nameSqlParameter = new SqlParameter("name", model.Name.Trim());
																				
																				
			var query = _db.Roles.FromSqlRaw("select * from Roles where UPPER(Name) = UPPER(@name)", nameSqlParameter);
			if (query.Any()) 
				return new ErrorResult("Role with the same name already exists!");

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
			
			var nameSqlParameter = new SqlParameter("name", model.Name.Trim()); 
			var idSqlParameter = new SqlParameter("id", model.Id);
			

			var query = _db.Roles.FromSqlRaw("select * from Roles where UPPER(Name) = UPPER(@name) and Id != @id", nameSqlParameter, idSqlParameter);


			if (query.Any()) 
				return new ErrorResult("Role with the same name already exists!");


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
