using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace System.Data.Entity.Internal
{
	// Token: 0x020006C1 RID: 1729
	internal class EdmMetadataContext : DbContext
	{
		// Token: 0x060044B8 RID: 17592 RVA: 0x00144964 File Offset: 0x00142B64
		static EdmMetadataContext()
		{
			Database.SetInitializer<EdmMetadataContext>(null);
		}

		// Token: 0x060044B9 RID: 17593 RVA: 0x0014496C File Offset: 0x00142B6C
		public EdmMetadataContext(DbConnection existingConnection) : base(existingConnection, false)
		{
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x060044BA RID: 17594 RVA: 0x00144976 File Offset: 0x00142B76
		// (set) Token: 0x060044BB RID: 17595 RVA: 0x0014497E File Offset: 0x00142B7E
		public virtual IDbSet<EdmMetadata> Metadata { get; set; }

		// Token: 0x060044BC RID: 17596 RVA: 0x00144987 File Offset: 0x00142B87
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			EdmMetadataContext.ConfigureEdmMetadata(modelBuilder.ModelConfiguration);
		}

		// Token: 0x060044BD RID: 17597 RVA: 0x00144994 File Offset: 0x00142B94
		public static void ConfigureEdmMetadata(ModelConfiguration modelConfiguration)
		{
			modelConfiguration.Entity(typeof(EdmMetadata)).ToTable("EdmMetadata");
		}

		// Token: 0x04001950 RID: 6480
		public const string TableName = "EdmMetadata";
	}
}
