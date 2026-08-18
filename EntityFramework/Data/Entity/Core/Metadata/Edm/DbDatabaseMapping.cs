using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020001F3 RID: 499
	internal class DbDatabaseMapping
	{
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x00049B40 File Offset: 0x00047D40
		// (set) Token: 0x06001166 RID: 4454 RVA: 0x00049B48 File Offset: 0x00047D48
		public EdmModel Model { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x00049B51 File Offset: 0x00047D51
		// (set) Token: 0x06001168 RID: 4456 RVA: 0x00049B59 File Offset: 0x00047D59
		public EdmModel Database { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x00049B62 File Offset: 0x00047D62
		public DbProviderInfo ProviderInfo
		{
			get
			{
				return this.Database.ProviderInfo;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x00049B6F File Offset: 0x00047D6F
		public DbProviderManifest ProviderManifest
		{
			get
			{
				return this.Database.ProviderManifest;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x00049B7C File Offset: 0x00047D7C
		internal IList<EntityContainerMapping> EntityContainerMappings
		{
			get
			{
				return this._entityContainerMappings;
			}
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x00049B84 File Offset: 0x00047D84
		internal void AddEntityContainerMapping(EntityContainerMapping entityContainerMapping)
		{
			Check.NotNull<EntityContainerMapping>(entityContainerMapping, "entityContainerMapping");
			this._entityContainerMappings.Add(entityContainerMapping);
		}

		// Token: 0x0400052B RID: 1323
		private readonly List<EntityContainerMapping> _entityContainerMappings = new List<EntityContainerMapping>();
	}
}
