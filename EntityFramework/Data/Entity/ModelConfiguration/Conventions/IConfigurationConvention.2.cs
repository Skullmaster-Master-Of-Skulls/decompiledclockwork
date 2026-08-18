using System;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001C0 RID: 448
	internal interface IConfigurationConvention<TMemberInfo> : IConvention where TMemberInfo : MemberInfo
	{
		// Token: 0x06000F19 RID: 3865
		void Apply(TMemberInfo memberInfo, ModelConfiguration modelConfiguration);
	}
}
