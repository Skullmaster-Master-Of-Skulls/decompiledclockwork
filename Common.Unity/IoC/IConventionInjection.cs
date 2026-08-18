using System;

namespace TechnoPro.Common.Unity.IoC
{
	// Token: 0x02000007 RID: 7
	public interface IConventionInjection
	{
		// Token: 0x06000014 RID: 20
		T ResolveByDefault<T>();

		// Token: 0x06000015 RID: 21
		bool Contains<T>();

		// Token: 0x06000016 RID: 22
		T ResolveByDefault<T>(string name);

		// Token: 0x06000017 RID: 23
		bool Contains<T>(string name);

		// Token: 0x06000018 RID: 24
		void Initialize();
	}
}
