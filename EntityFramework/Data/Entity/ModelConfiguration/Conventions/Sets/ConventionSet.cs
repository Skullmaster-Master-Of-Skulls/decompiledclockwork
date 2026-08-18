using System;
using System.Collections.Generic;

namespace System.Data.Entity.ModelConfiguration.Conventions.Sets
{
	// Token: 0x020001D3 RID: 467
	internal class ConventionSet
	{
		// Token: 0x06000F69 RID: 3945 RVA: 0x00041674 File Offset: 0x0003F874
		public ConventionSet()
		{
			this.ConfigurationConventions = new IConvention[0];
			this.ConceptualModelConventions = new IConvention[0];
			this.ConceptualToStoreMappingConventions = new IConvention[0];
			this.StoreModelConventions = new IConvention[0];
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x000416AC File Offset: 0x0003F8AC
		public ConventionSet(IEnumerable<IConvention> configurationConventions, IEnumerable<IConvention> entityModelConventions, IEnumerable<IConvention> dbMappingConventions, IEnumerable<IConvention> dbModelConventions)
		{
			this.ConfigurationConventions = configurationConventions;
			this.ConceptualModelConventions = entityModelConventions;
			this.ConceptualToStoreMappingConventions = dbMappingConventions;
			this.StoreModelConventions = dbModelConventions;
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000F6B RID: 3947 RVA: 0x000416D1 File Offset: 0x0003F8D1
		// (set) Token: 0x06000F6C RID: 3948 RVA: 0x000416D9 File Offset: 0x0003F8D9
		public IEnumerable<IConvention> ConfigurationConventions { get; private set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000F6D RID: 3949 RVA: 0x000416E2 File Offset: 0x0003F8E2
		// (set) Token: 0x06000F6E RID: 3950 RVA: 0x000416EA File Offset: 0x0003F8EA
		public IEnumerable<IConvention> ConceptualModelConventions { get; private set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x000416F3 File Offset: 0x0003F8F3
		// (set) Token: 0x06000F70 RID: 3952 RVA: 0x000416FB File Offset: 0x0003F8FB
		public IEnumerable<IConvention> ConceptualToStoreMappingConventions { get; private set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x00041704 File Offset: 0x0003F904
		// (set) Token: 0x06000F72 RID: 3954 RVA: 0x0004170C File Offset: 0x0003F90C
		public IEnumerable<IConvention> StoreModelConventions { get; private set; }
	}
}
