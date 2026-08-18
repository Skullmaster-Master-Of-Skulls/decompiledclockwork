using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Migrations.History
{
	// Token: 0x020006F2 RID: 1778
	public class HistoryContext : DbContext, IDbModelCacheKeyProvider
	{
		// Token: 0x06004730 RID: 18224 RVA: 0x0015103C File Offset: 0x0014F23C
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		internal HistoryContext()
		{
			this.InternalContext.InitializerDisabled = true;
		}

		// Token: 0x06004731 RID: 18225 RVA: 0x00151050 File Offset: 0x0014F250
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public HistoryContext(DbConnection existingConnection, string defaultSchema) : base(existingConnection, false)
		{
			this._defaultSchema = defaultSchema;
			base.Configuration.ValidateOnSaveEnabled = false;
			this.InternalContext.InitializerDisabled = true;
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06004732 RID: 18226 RVA: 0x00151079 File Offset: 0x0014F279
		public virtual string CacheKey
		{
			get
			{
				return this._defaultSchema;
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06004733 RID: 18227 RVA: 0x00151081 File Offset: 0x0014F281
		protected string DefaultSchema
		{
			get
			{
				return this._defaultSchema;
			}
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06004734 RID: 18228 RVA: 0x00151089 File Offset: 0x0014F289
		// (set) Token: 0x06004735 RID: 18229 RVA: 0x00151091 File Offset: 0x0014F291
		public virtual IDbSet<HistoryRow> History { get; set; }

		// Token: 0x06004736 RID: 18230 RVA: 0x001511B4 File Offset: 0x0014F3B4
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema(this._defaultSchema);
			modelBuilder.Entity<HistoryRow>().ToTable("__MigrationHistory");
			modelBuilder.Entity<HistoryRow>().HasKey((HistoryRow h) => new
			{
				h.MigrationId,
				h.ContextKey
			});
			modelBuilder.Entity<HistoryRow>().Property((HistoryRow h) => h.MigrationId).HasMaxLength(new int?(150)).IsRequired();
			modelBuilder.Entity<HistoryRow>().Property((HistoryRow h) => h.ContextKey).HasMaxLength(new int?(300)).IsRequired();
			modelBuilder.Entity<HistoryRow>().Property((HistoryRow h) => h.Model).IsRequired().IsMaxLength();
			modelBuilder.Entity<HistoryRow>().Property((HistoryRow h) => h.ProductVersion).HasMaxLength(new int?(32)).IsRequired();
		}

		// Token: 0x04001A1B RID: 6683
		public const string DefaultTableName = "__MigrationHistory";

		// Token: 0x04001A1C RID: 6684
		internal const int ContextKeyMaxLength = 300;

		// Token: 0x04001A1D RID: 6685
		internal const int MigrationIdMaxLength = 150;

		// Token: 0x04001A1E RID: 6686
		private readonly string _defaultSchema;

		// Token: 0x04001A1F RID: 6687
		internal static readonly Func<DbConnection, string, HistoryContext> DefaultFactory = (DbConnection e, string d) => new HistoryContext(e, d);
	}
}
