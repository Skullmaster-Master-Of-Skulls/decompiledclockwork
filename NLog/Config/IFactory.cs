using System;

namespace NLog.Config
{
	// Token: 0x02000048 RID: 72
	internal interface IFactory
	{
		// Token: 0x06000150 RID: 336
		void Clear();

		// Token: 0x06000151 RID: 337
		void ScanTypes(Type[] type, string prefix);

		// Token: 0x06000152 RID: 338
		void RegisterType(Type type, string itemNamePrefix);
	}
}
