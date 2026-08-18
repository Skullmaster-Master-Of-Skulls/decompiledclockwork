using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001B5 RID: 437
	internal class ConventionsTypeFinder
	{
		// Token: 0x06000EA0 RID: 3744 RVA: 0x0003F847 File Offset: 0x0003DA47
		public ConventionsTypeFinder() : this(new ConventionsTypeFilter(), new ConventionsTypeActivator())
		{
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0003F859 File Offset: 0x0003DA59
		public ConventionsTypeFinder(ConventionsTypeFilter conventionsTypeFilter, ConventionsTypeActivator conventionsTypeActivator)
		{
			this._conventionsTypeFilter = conventionsTypeFilter;
			this._conventionsTypeActivator = conventionsTypeActivator;
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0003F870 File Offset: 0x0003DA70
		public void AddConventions(IEnumerable<Type> types, Action<IConvention> addFunction)
		{
			foreach (Type conventionType in types)
			{
				if (this._conventionsTypeFilter.IsConvention(conventionType))
				{
					addFunction(this._conventionsTypeActivator.Activate(conventionType));
				}
			}
		}

		// Token: 0x040003F6 RID: 1014
		private readonly ConventionsTypeFilter _conventionsTypeFilter;

		// Token: 0x040003F7 RID: 1015
		private readonly ConventionsTypeActivator _conventionsTypeActivator;
	}
}
