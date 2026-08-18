using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace a.b
{
	// Token: 0x020002D2 RID: 722
	internal class hw : gj
	{
		// Token: 0x06001926 RID: 6438 RVA: 0x000702C4 File Offset: 0x0006F2C4
		public hw(gg A_0, h0 A_1)
		{
			this.a = A_0;
			this.b = A_1;
			if (A_0.h() < 4096)
			{
				this.c = new ga(this.b.g(), A_0.i());
				this.d = this.b.g().il();
				return;
			}
			this.c = new ga(this.b, A_0.i());
			this.d = this.b.il();
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x00070350 File Offset: 0x0006F350
		public hw(string A_0, h0 A_1, Stream A_2)
		{
			this.b = A_1;
			byte[] array;
			if (A_2 is MemoryStream)
			{
				MemoryStream memoryStream = (MemoryStream)A_2;
				array = new byte[memoryStream.Length];
				memoryStream.Read(array, 0, array.Length);
			}
			else
			{
				MemoryStream memoryStream2 = new MemoryStream();
				g9.a(A_2, memoryStream2);
				array = memoryStream2.ToArray();
			}
			if (array.Length <= 4096)
			{
				this.c = new ga(A_1.g());
				this.d = this.b.g().il();
			}
			else
			{
				this.c = new ga(A_1);
				this.d = this.b.il();
			}
			this.c.a(array);
			this.a = new gg(A_0, array.Length);
			this.a.c(this.c.b());
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x00070427 File Offset: 0x0006F427
		public int e()
		{
			return this.d;
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x0007042F File Offset: 0x0006F42F
		public IEnumerator<he> f()
		{
			if (this.c() > 0)
			{
				return this.c.a();
			}
			return new List<he>().GetEnumerator();
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x00070455 File Offset: 0x0006F455
		public int c()
		{
			return this.a.h();
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x00070462 File Offset: 0x0006F462
		public gg a()
		{
			return this.a;
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x0007046C File Offset: 0x0006F46C
		protected object[] d()
		{
			object[] array = new object[1];
			string text;
			try
			{
				if (this.c() > 0)
				{
					byte[] array2 = new byte[this.c()];
					int num = 0;
					foreach (he he in this.c)
					{
						int num2 = Math.Min(this.d, array2.Length - num);
						he.c(array2, num, num2);
						num += num2;
					}
					MemoryStream memoryStream = new MemoryStream();
					f5.a(array2, 0L, memoryStream, 0);
					text = memoryStream.ToString();
				}
				else
				{
					text = "<NO DATA>";
				}
			}
			catch (IOException ex)
			{
				text = ex.Message;
			}
			array[0] = text;
			return array;
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x00070534 File Offset: 0x0006F534
		protected IEnumerator b()
		{
			return null;
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x00070538 File Offset: 0x0006F538
		protected string g()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Document: \"").Append(this.a.f()).Append("\"");
			stringBuilder.Append(" size = ").Append(this.c());
			return stringBuilder.ToString();
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x0007058C File Offset: 0x0006F58C
		public bool jk()
		{
			return true;
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x0007058F File Offset: 0x0006F58F
		public string jl()
		{
			return this.g();
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x00070597 File Offset: 0x0006F597
		public Array ji()
		{
			return this.d();
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x0007059F File Offset: 0x0006F59F
		public IEnumerator jj()
		{
			return this.b();
		}

		// Token: 0x0400125B RID: 4699
		private gg a;

		// Token: 0x0400125C RID: 4700
		private h0 b;

		// Token: 0x0400125D RID: 4701
		private ga c;

		// Token: 0x0400125E RID: 4702
		private int d;
	}
}
