using System;

namespace System.ServiceModel
{
	// Token: 0x0200011B RID: 283
	internal class EmptyArray
	{
		// Token: 0x0600073D RID: 1853 RVA: 0x0001E624 File Offset: 0x0001C824
		private EmptyArray()
		{
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x0001E62C File Offset: 0x0001C82C
		internal static object[] Instance
		{
			get
			{
				return EmptyArray.instance;
			}
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0001E633 File Offset: 0x0001C833
		internal static object[] Allocate(int n)
		{
			if (n == 0)
			{
				return EmptyArray.Instance;
			}
			return new object[n];
		}

		// Token: 0x04000ABC RID: 2748
		private static object[] instance = new object[0];
	}
}
