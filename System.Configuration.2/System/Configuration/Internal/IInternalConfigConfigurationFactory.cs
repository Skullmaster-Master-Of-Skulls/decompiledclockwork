using System;
using System.Runtime.InteropServices;

namespace System.Configuration.Internal
{
	// Token: 0x020000B2 RID: 178
	[ComVisible(false)]
	public interface IInternalConfigConfigurationFactory
	{
		// Token: 0x06000701 RID: 1793
		Configuration Create(Type typeConfigHost, params object[] hostInitConfigurationParams);

		// Token: 0x06000702 RID: 1794
		string NormalizeLocationSubPath(string subPath, IConfigErrorInfo errorInfo);
	}
}
