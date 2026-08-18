using System;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001B3 RID: 435
	internal class ConventionsTypeActivator
	{
		// Token: 0x06000E98 RID: 3736 RVA: 0x0003F766 File Offset: 0x0003D966
		public virtual IConvention Activate(Type conventionType)
		{
			return (IConvention)Activator.CreateInstance(conventionType, true);
		}
	}
}
