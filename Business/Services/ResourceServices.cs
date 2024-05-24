using Business.Models;
using DataAccess_.Contexts;
using DataAccess_.Entities;
using DataAccess_.Results;
using DataAccess_.Results.Bases;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public interface IResourceService
    {
        IQueryable<ResourceModel> Query();
        Result Add(ResourceModel model);
        Result Update(ResourceModel model);
        Result Delete(int id);

       
        List<ResourceModel> GetList();
		ResourceModel GetItem(int id);

	}

    public class ResourceService : IResourceService
    {
        private readonly Db _db;

        public ResourceService(Db db)
        {
            _db = db;
        }

        public IQueryable<ResourceModel> Query()
        {
			return _db.Resources.Include(r => r.UserResources).Select(r => new ResourceModel()
			{
                Content = r.Content,
                Date = r.Date,
                Id = r.Id,
                Score = r.Score,
                Title = r.Title,
              
                ScoreOutput = r.Score.ToString("N1"),

				DateOutput = r.Date.HasValue ? r.Date.Value.ToString("MM/dd/yyyy hh:mm:ss") : "",

				UserCountOutput = r.UserResources.Count,

                UserNamesOutput = string.Join("<br />", r.UserResources.Select(ur => ur.User.UserName)), 
                UserIdsInput = r.UserResources.Select(ur => ur.UserId).ToList()

            }).OrderByDescending(r => r.Date).ThenByDescending(r => r.Score);
        }

        public Result Add(ResourceModel model)
        {
           
            if (model.Date.HasValue &&
                _db.Resources.Any(r => (r.Date ?? new DateTime()).Date == model.Date.Value.Date &&
                r.Title.ToUpper() == model.Title.ToUpper().Trim()))
                return new ErrorResult("Resource with the same title and date exists!");
            var entity = new Resource()
            {
                
                Content = model.Content?.Trim(),

                Date = model.Date,
                Score = model.Score ?? 0,
                Title = model.Title.Trim(),


                UserResources = model.UserIdsInput?.Select(userId => new UserResource()
                {
                    UserId = userId
                }).ToList()
            };
            _db.Resources.Add(entity);
            _db.SaveChanges();
            return new SuccessResult("Resource added successfully.");
        }

        public Result Update(ResourceModel model)
        {
			if (model.Date.HasValue &&
			   _db.Resources.Any(r => (r.Date ?? new DateTime()).Date == model.Date.Value.Date &&
			   r.Title.ToUpper() == model.Title.ToUpper().Trim() && r.Id != model.Id))
				return new ErrorResult("Resource with the same title and date exists!");

			var existingEntity = _db.Resources.Include(r => r.UserResources).SingleOrDefault(r => r.Id == model.Id);
			if (existingEntity is not null && existingEntity.UserResources is not null)
				_db.UserResources.RemoveRange(existingEntity.UserResources);



            existingEntity.Content = model.Content?.Trim();
            existingEntity.Date = model.Date;
            existingEntity.Score = model.Score ?? 0;
            existingEntity.Title = model.Title.Trim();

            // inserting many to many relational entity
            existingEntity.UserResources = model.UserIdsInput?.Select(userId => new UserResource()
            {
                UserId = userId
            }).ToList();

            _db.Resources.Update(existingEntity);
            _db.SaveChanges();
			return new SuccessResult("Resource updated successfully.");
        }

        public Result Delete(int id)
        {
            var entity = _db.Resources.Include(r => r.UserResources).SingleOrDefault(r => r.Id == id);
            if (entity is null)
                return new ErrorResult("Resource not found!");

            
            _db.UserResources.RemoveRange(entity.UserResources);

            
            _db.Resources.Remove(entity);

            _db.SaveChanges();

            return new SuccessResult("Resource deleted successfully.");
        }

        public List<ResourceModel> GetList()
        {
            
            return Query().ToList();
        }

		public ResourceModel GetItem(int id) => Query().SingleOrDefault(r => r.Id == id);
	}
}

