using System;
using System.Text;

namespace a
{
	// Token: 0x020000E9 RID: 233
	internal class at
	{
		// Token: 0x0600078A RID: 1930 RVA: 0x00022D41 File Offset: 0x00021D41
		public at(byte[] A_0, Encoding A_1, string A_2, af A_3, string A_4)
		{
			this.b = A_0;
			this.d = A_1;
			this.f = A_2;
			this.c = A_3;
			this.g = A_4;
			this.e = null;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x00022D75 File Offset: 0x00021D75
		public byte[] q()
		{
			return this.b;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00022D7D File Offset: 0x00021D7D
		public string[] s()
		{
			if (this.e == null)
			{
				if (this.b == null)
				{
					return null;
				}
				this.e = bb.e(this.d.GetString(this.b, 0, this.b.Length));
			}
			return this.e;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00022DBC File Offset: 0x00021DBC
		public string o()
		{
			return this.f;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00022DC4 File Offset: 0x00021DC4
		public string r()
		{
			return this.g;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00022DCC File Offset: 0x00021DCC
		public af t()
		{
			return this.c;
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00022DD4 File Offset: 0x00021DD4
		public Encoding p()
		{
			return this.d;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00022DDC File Offset: 0x00021DDC
		public override string ToString()
		{
			if (this.b == null)
			{
				return null;
			}
			return this.d.GetString(this.b, 0, this.b.Length);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00022E02 File Offset: 0x00021E02
		public virtual void ag()
		{
			this.b = null;
			this.e = null;
			this.f = null;
			this.g = null;
		}

		// Token: 0x04000515 RID: 1301
		private byte[] b;

		// Token: 0x04000516 RID: 1302
		private af c;

		// Token: 0x04000517 RID: 1303
		private Encoding d;

		// Token: 0x04000518 RID: 1304
		private string[] e;

		// Token: 0x04000519 RID: 1305
		private string f;

		// Token: 0x0400051A RID: 1306
		private string g;
	}
}
