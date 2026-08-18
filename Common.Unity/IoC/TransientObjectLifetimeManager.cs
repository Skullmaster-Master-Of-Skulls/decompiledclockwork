using System;

namespace TechnoPro.Common.Unity.IoC
{
	// Token: 0x02000010 RID: 16
	public class TransientObjectLifetimeManager : IObjectLifeTimeManager
	{
		// Token: 0x06000059 RID: 89 RVA: 0x0000369C File Offset: 0x0000189C
		public IcwObject GetIcwObject<T>()
		{
			return new TransientIcwObject(typeof(T));
		}
	}
}
