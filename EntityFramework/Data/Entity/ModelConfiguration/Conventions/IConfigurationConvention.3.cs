using System;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001C9 RID: 457
	internal interface IConfigurationConvention<TMemberInfo, TConfiguration> : IConvention where TMemberInfo : MemberInfo where TConfiguration : ConfigurationBase
	{
		// Token: 0x06000F3B RID: 3899
		void Apply(TMemberInfo memberInfo, Func<TConfiguration> configuration, ModelConfiguration modelConfiguration);
	}
}
