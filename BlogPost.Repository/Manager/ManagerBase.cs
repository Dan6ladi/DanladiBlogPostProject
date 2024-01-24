using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Repository.Manager
{
    public class ManagerBase<TEntity> : BaseRepository<TEntity> where TEntity : class
    {
        public ManagerBase(DbContext context) : base(context)
        {

        }
    }
}
