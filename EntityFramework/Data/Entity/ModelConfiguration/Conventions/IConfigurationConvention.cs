using System;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001BF RID: 447
	internal interface IConfigurationConvention : IConvention
	{
		// Token: 0x06000F18 RID: 3864
		void Apply(ModelConfiguration modelConfiguration);
	}
}
