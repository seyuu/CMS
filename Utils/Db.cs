using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Data.Entity.ModelConfiguration.Conventions;
using CMS.Models;

public class Db : DbContext {

    public virtual DbSet<Block> Block { get; set; }
    public virtual DbSet<BlockItem> BlockItem { get; set; }
    public virtual DbSet<Page> Page { get; set; }
    public virtual DbSet<Section> Section { get; set; }
    public virtual DbSet<Menu> Menu { get; set; }
    public virtual DbSet<MenuItem> MenuItem { get; set; }
    public virtual DbSet<Setting> Setting { get; set; }

    public Db(): base("name=db") {
        Database.SetInitializer(new MigrateDatabaseToLatestVersion<Db, DbConfiguration>("db"));
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder) {
        modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //modelBuilder.Conventions.Add<OneToManyCascadeDeleteConvention>();
        //modelBuilder.Conventions.Add<ManyToManyCascadeDeleteConvention>();

        //modelBuilder.Entity<Block>()
        //.HasOptional(a => a.BlockItem)
        //.WithOptionalDependent()
        //.WillCascadeOnDelete(true);

    }

    public override int SaveChanges() {
        //try {
            return base.SaveChanges();
        //}
        //catch (DbEntityValidationException e) {
        //    var message = "";
        //    foreach (var eve in e.EntityValidationErrors) {
        //        message += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:\n", eve.Entry.Entity.GetType().Name, eve.Entry.State);
        //        foreach (var ve in eve.ValidationErrors) {
        //            message += string.Format("- Property: \"{0}\", Error: \"{1}\"\n", ve.PropertyName, ve.ErrorMessage);
        //        }
        //    }
        //    throw new Exception(message);
        //}
        //catch (DbUpdateException e) {

        //}
        return 0;
    }

    public void Update<T>(T entity, params string[] exlude) where T : class {
        var entry = Entry<T>(entity);
        entry.State = EntityState.Modified;
        if (exlude != null) {
            foreach (var name in exlude) {
                entry.Property(name.Trim()).IsModified = false;
            }
        }
        SaveChanges();
        entry.State = EntityState.Detached;
    }

    public void Insert<T>(T entity) where T : class {
        Set<T>().Add(entity);
        SaveChanges();
    }

    public void Delete<T>(T entity) where T : class {
        if (entity != null) {
            Set<T>().Remove(entity);
            SaveChanges();
        }
    }

}