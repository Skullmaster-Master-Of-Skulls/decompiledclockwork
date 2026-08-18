using System;

namespace a.b
{
	// Token: 0x02000312 RID: 786
	internal class hm
	{
		// Token: 0x06001C0E RID: 7182 RVA: 0x0007B33F File Offset: 0x0007A33F
		public hm(string A_0) : this(A_0, " \t\n\r\f", false)
		{
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x0007B34E File Offset: 0x0007A34E
		public hm(string A_0, string A_1) : this(A_0, A_1, false)
		{
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x0007B359 File Offset: 0x0007A359
		public hm(string A_0, string A_1, bool A_2)
		{
			this.c = A_0.Length;
			this.b = A_0;
			this.d = A_1;
			this.e = A_2;
			this.a = 0;
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x0007B38C File Offset: 0x0007A38C
		public bool c()
		{
			if (!this.e)
			{
				while (this.a < this.c && this.d.IndexOf(this.b[this.a]) >= 0)
				{
					this.a++;
				}
			}
			return this.a < this.c;
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x0007B3EC File Offset: 0x0007A3EC
		public string a(string A_0)
		{
			this.d = A_0;
			return this.b();
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x0007B3FC File Offset: 0x0007A3FC
		public string b()
		{
			if (this.a < this.c && this.d.IndexOf(this.b[this.a]) >= 0)
			{
				int num;
				if (this.e)
				{
					string text = this.b;
					num = this.a;
					this.a = num + 1;
					return text.Substring(num, 1);
				}
				do
				{
					num = this.a + 1;
					this.a = num;
				}
				while (num < this.c && this.d.IndexOf(this.b[this.a]) >= 0);
			}
			if (this.a < this.c)
			{
				int num2 = this.a;
				int num;
				do
				{
					num = this.a + 1;
					this.a = num;
				}
				while (num < this.c && this.d.IndexOf(this.b[this.a]) < 0);
				return this.b.Substring(num2, this.a - num2);
			}
			throw new IndexOutOfRangeException();
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x0007B4FC File Offset: 0x0007A4FC
		public int a()
		{
			int num = 0;
			int num2 = 0;
			bool flag = false;
			int i = this.a;
			while (i < this.c)
			{
				if (this.d.IndexOf(this.b[i++]) >= 0)
				{
					if (flag)
					{
						num++;
						flag = false;
					}
					num2++;
				}
				else
				{
					flag = true;
					while (i < this.c && this.d.IndexOf(this.b[i]) < 0)
					{
						i++;
					}
				}
			}
			if (flag)
			{
				num++;
			}
			if (!this.e)
			{
				return num;
			}
			return num + num2;
		}

		// Token: 0x0400134C RID: 4940
		private int a;

		// Token: 0x0400134D RID: 4941
		private string b;

		// Token: 0x0400134E RID: 4942
		private int c;

		// Token: 0x0400134F RID: 4943
		private string d;

		// Token: 0x04001350 RID: 4944
		private bool e;
	}
}
