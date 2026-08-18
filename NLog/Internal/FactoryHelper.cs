using System;
using System.Reflection;

namespace NLog.Internal
{
	// Token: 0x02000080 RID: 128
	internal class FactoryHelper
	{
		// Token: 0x0600042F RID: 1071 RVA: 0x00009567 File Offset: 0x00007767
		private FactoryHelper()
		{
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00009570 File Offset: 0x00007770
		internal static object CreateInstance(Type t)
		{
			ConstructorInfo constructor = t.GetConstructor(FactoryHelper.emptyTypes);
			if (constructor != null)
			{
				return constructor.Invoke(FactoryHelper.emptyParams);
			}
			throw new NLogConfigurationException("Cannot access the constructor of type: " + t.FullName + ". Is the required permission granted?");
		}

		// Token: 0x040000D6 RID: 214
		private static Type[] emptyTypes = new Type[0];

		// Token: 0x040000D7 RID: 215
		private static object[] emptyParams = new object[0];
	}
}
