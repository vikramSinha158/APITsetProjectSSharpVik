using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PxTransform.Auto.Data.Domain.Tran;

namespace PxTransform.Auto.Data.Data
{
    public class TranContext : DbContext
    {

        public TranContext(DbContextOptions<TranContext> options) : base(options)
        {

        }

        #region Methods
        public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        #endregion


        public virtual DbSet<Registration> Registrations { get; set; }
        public virtual DbSet<Coverage> Coverages { get; set; }

        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<AuthorizationNeed> AuthorizationNeeds { get; set; }

        public virtual DbSet<RegistrationAuthorization> RegistrationAuthorizations { get; set; }
        public virtual DbSet<Authorization> Authorizations { get; set; }
        public virtual DbSet<RecordTaskStatus> RecordTaskStatus { get; set; }
        public virtual DbSet<PayorPlan> PayorPlans { get; set; }

    }
}
