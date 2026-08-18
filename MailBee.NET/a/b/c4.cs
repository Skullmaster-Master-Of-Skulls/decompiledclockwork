using System;
using System.Resources;

namespace a.b
{
	// Token: 0x0200038A RID: 906
	internal abstract class c4
	{
		// Token: 0x060020CF RID: 8399 RVA: 0x00087CF6 File Offset: 0x00086CF6
		protected static string a(string A_0, params object[] A_1)
		{
			return ac.a(A_0, A_1);
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x00087CFF File Offset: 0x00086CFF
		protected static ResourceManager a(Type A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("singletonType");
			}
			return new ResourceManager(A_0.FullName, A_0.Assembly);
		}
	}
}
