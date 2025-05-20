using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ToDoDbContext _dbContext;
        private bool disposed = false;

        public UnitOfWork(ToDoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IRepository<User> _user;
        private IRepository<ToDoTask> _task;

        public IRepository<User> Users => _user ??= new Repository<User>(_dbContext);

        public IRepository<ToDoTask> Tasks => _task ??= new Repository<ToDoTask>(_dbContext);

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
