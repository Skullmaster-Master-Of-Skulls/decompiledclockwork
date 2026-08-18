using System;
using System.Data.Entity.Core.Common;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x02000162 RID: 354
	internal class NamedDbProviderService
	{
		// Token: 0x06000B7B RID: 2939 RVA: 0x00039161 File Offset: 0x00037361
		public NamedDbProviderService(string invariantName, DbProviderServices providerServices)
		{
			this._invariantName = invariantName;
			this._providerServices = providerServices;
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x00039177 File Offset: 0x00037377
		public string InvariantName
		{
			get
			{
				return this._invariantName;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x0003917F File Offset: 0x0003737F
		public DbProviderServices ProviderServices
		{
			get
			{
				return this._providerServices;
			}
		}

		// Token: 0x0400032A RID: 810
		private readonly string _invariantName;

		// Token: 0x0400032B RID: 811
		private readonly DbProviderServices _providerServices;
	}
}
