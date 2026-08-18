using System;
using System.IO;
using MailBee;

namespace a.b
{
	// Token: 0x02000241 RID: 577
	internal class io : ab
	{
		// Token: 0x06001347 RID: 4935 RVA: 0x000570B0 File Offset: 0x000560B0
		public new void a(long A_0)
		{
			string text = "3705";
			base.b(text, "0003");
			base.a("__substg1.0_" + text, "0003", ab.a(A_0));
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x000570EC File Offset: 0x000560EC
		public new void b(string A_0)
		{
			string text = "3704";
			base.b(text, base.g());
			base.a("__substg1.0_" + text, base.g(), ab.a(A_0, this.o));
			text = "3707";
			base.b(text, base.g());
			base.a("__substg1.0_" + text, base.g(), ab.a(A_0, this.o));
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x00057168 File Offset: 0x00056168
		public new void a(byte[] A_0)
		{
			string text = "3701";
			base.b(text, "0102");
			base.a("__substg1.0_" + text, "0102", ab.a(A_0));
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x000571A4 File Offset: 0x000561A4
		public new void b(int A_0)
		{
			string text = "0FF9";
			base.b(text, "0102");
			byte[] a_ = new byte[]
			{
				0,
				0,
				0,
				(byte)A_0
			};
			base.a("__substg1.0_" + text, "0102", ab.a(a_));
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x000571EC File Offset: 0x000561EC
		public new void a(string A_0)
		{
			string text = "3712";
			base.b(text, base.g());
			base.a("__substg1.0_" + text, base.g(), ab.a(A_0, this.o));
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00057230 File Offset: 0x00056230
		public new void c(string A_0)
		{
			string text = "370E";
			base.b(text, base.g());
			base.a("__substg1.0_" + text, base.g(), ab.a(A_0, this.o));
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00057274 File Offset: 0x00056274
		public io(ig A_0)
		{
			this.k = A_0;
			this.c();
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x00057289 File Offset: 0x00056289
		public io(ig A_0, bool A_1) : this(A_0)
		{
			this.o = A_1;
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x00057299 File Offset: 0x00056299
		public new void c()
		{
			this.m = new bl();
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x000572A8 File Offset: 0x000562A8
		public new void a()
		{
			try
			{
				byte[] array = this.m.du();
				byte[] array2 = new byte[array.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = array[i];
				}
				Stream a_ = new MemoryStream(array2);
				this.k.em("__properties_version1.0", a_);
			}
			catch (IOException a_2)
			{
				throw new MailBeeStreamException(41, a_2);
			}
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x00057318 File Offset: 0x00056318
		public new void a(int A_0)
		{
			try
			{
				base.a(this.k, "0E21", "0003", (long)A_0);
				base.a(this.k, "0FFE", "0003", 7L);
				base.a(this.k, "0FF4", "0003", 2L);
				base.a(this.k, "0FF7", "0003", 0L);
				base.a(this.k, "3705", "0003", 1L);
				base.a(this.k, "370B", "0003", (long)((ulong)-1));
				base.a(this.k, "3710", "0003", 0L);
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x000573EC File Offset: 0x000563EC
		public new void b()
		{
			string text = "370A";
			base.b(text, "0102");
			byte[] a_ = new byte[]
			{
				42,
				134,
				72,
				134,
				247,
				20,
				3,
				10,
				4
			};
			base.a("__substg1.0_" + text, "0102", ab.a(a_));
		}
	}
}
