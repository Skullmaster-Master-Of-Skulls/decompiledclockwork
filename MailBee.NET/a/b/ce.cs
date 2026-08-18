using System;
using System.Collections;
using System.Reflection;

namespace a.b
{
	// Token: 0x02000379 RID: 889
	[DefaultMember("Item")]
	internal sealed class ce : b7, h6
	{
		// Token: 0x06002049 RID: 8265 RVA: 0x00086920 File Offset: 0x00085920
		public bool ga(string A_0)
		{
			return this.a.ContainsKey(A_0);
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x0008692E File Offset: 0x0008592E
		public fe gb(int A_0)
		{
			return base.InnerList[A_0] as fe;
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x00086941 File Offset: 0x00085941
		public fe gc(string A_0)
		{
			return this.a[A_0] as fe;
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x00086954 File Offset: 0x00085954
		public void gd(fe[] A_0, int A_1)
		{
			base.InnerList.CopyTo(A_0, A_1);
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x00086963 File Offset: 0x00085963
		public void a(fe A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("item");
			}
			base.InnerList.Add(A_0);
			this.a.Add(A_0.e4(), A_0);
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x00086992 File Offset: 0x00085992
		public void a()
		{
			base.InnerList.Clear();
			this.a.Clear();
		}

		// Token: 0x0400147C RID: 5244
		private new readonly Hashtable a = new Hashtable();
	}
}
