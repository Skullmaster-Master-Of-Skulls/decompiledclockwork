using System;

namespace System.Configuration.Internal
{
	// Token: 0x020000B9 RID: 185
	internal sealed class InternalConfigConfigurationFactory : IInternalConfigConfigurationFactory
	{
		// Token: 0x06000746 RID: 1862 RVA: 0x000115BE File Offset: 0x0000F7BE
		private InternalConfigConfigurationFactory()
		{
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0001F9BD File Offset: 0x0001DBBD
		Configuration IInternalConfigConfigurationFactory.Create(Type typeConfigHost, params object[] hostInitConfigurationParams)
		{
			return new Configuration(null, typeConfigHost, hostInitConfigurationParams);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0001F9C7 File Offset: 0x0001DBC7
		string IInternalConfigConfigurationFactory.NormalizeLocationSubPath(string subPath, IConfigErrorInfo errorInfo)
		{
			return BaseConfigurationRecord.NormalizeLocationSubPath(subPath, errorInfo);
		}
	}
}
