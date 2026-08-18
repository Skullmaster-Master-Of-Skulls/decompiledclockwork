using System;

namespace TechnoPro.Common.Unity.IoC
{
	// Token: 0x0200000E RID: 14
	public class SingletonObjectLifetimeManager : IObjectLifeTimeManager
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00003644 File Offset: 0x00001844
		public IcwObject GetIcwObject<T>()
		{
			return new SingletonIcwObject(typeof(T));
		}
	}
}
