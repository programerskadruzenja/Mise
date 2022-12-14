//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mise.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ZUPATISNOEntities : DbContext
    {
        public ZUPATISNOEntities()
            : base("name=ZUPATISNOEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Boja> Boja { get; set; }
        public virtual DbSet<Detalji> Detalji { get; set; }
        public virtual DbSet<Korisnik> Korisnik { get; set; }
        public virtual DbSet<Mjesta> Mjesta { get; set; }
        public virtual DbSet<Osobe> Osobe { get; set; }
        public virtual DbSet<Termin> Termin { get; set; }
        public virtual DbSet<Vrsta> Vrsta { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Rezervirane> Rezervirane { get; set; }
        public virtual DbSet<vwOsobe> vwOsobe { get; set; }
    
        public virtual ObjectResult<VratiBrojMisaZaNarucitelje_Result> VratiBrojMisaZaNarucitelje(Nullable<int> mjestoID, Nullable<int> godina)
        {
            var mjestoIDParameter = mjestoID.HasValue ?
                new ObjectParameter("MjestoID", mjestoID) :
                new ObjectParameter("MjestoID", typeof(int));
    
            var godinaParameter = godina.HasValue ?
                new ObjectParameter("Godina", godina) :
                new ObjectParameter("Godina", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<VratiBrojMisaZaNarucitelje_Result>("VratiBrojMisaZaNarucitelje", mjestoIDParameter, godinaParameter);
        }
    
        public virtual ObjectResult<UkupnoMisa_Result> UkupnoMisa(Nullable<int> godina)
        {
            var godinaParameter = godina.HasValue ?
                new ObjectParameter("Godina", godina) :
                new ObjectParameter("Godina", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UkupnoMisa_Result>("UkupnoMisa", godinaParameter);
        }
    
        public virtual ObjectResult<MiseZaGodinu_Result> MiseZaGodinu(Nullable<int> godina)
        {
            var godinaParameter = godina.HasValue ?
                new ObjectParameter("godina", godina) :
                new ObjectParameter("godina", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MiseZaGodinu_Result>("MiseZaGodinu", godinaParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual ObjectResult<spPoImenu_Result> spPoImenu(string ime)
        {
            var imeParameter = ime != null ?
                new ObjectParameter("Ime", ime) :
                new ObjectParameter("Ime", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spPoImenu_Result>("spPoImenu", imeParameter);
        }
    }
}
