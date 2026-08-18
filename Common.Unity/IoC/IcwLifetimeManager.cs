using System;

namespace TechnoPro.Common.Unity.IoC
{
	// Token: 0x02000004 RID: 4
	public class IcwLifetimeManager
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002568 File Offset: 0x00000768
		public static IcwObject GetIcwObject<T>(string lifetime)
		{
			IObjectLifeTimeManager lifetimeManager = IcwLifetimeManager.GetLifetimeManager(lifetime);
			return lifetimeManager.GetIcwObject<T>();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002588 File Offset: 0x00000788
		private static IObjectLifeTimeManager GetLifetimeManager(string lifetime)
		{
			IObjectLifeTimeManager result;
			if (!(lifetime == "Singleton"))
			{
				if (!(lifetime == "Transient"))
				{
					result = ObjectFactory.Resolve<IObjectLifeTimeManager>(lifetime);
				}
				else
				{
					result = new TransientObjectLifetimeManager();
				}
			}
			else
			{
				result = new SingletonObjectLifetimeManager();
			}
			return result;
		}
	}
}
