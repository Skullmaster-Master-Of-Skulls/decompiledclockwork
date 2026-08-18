using System;
using System.IO;

namespace a.b
{
	// Token: 0x020002FA RID: 762
	internal class ik : ae, af
	{
		// Token: 0x06001AEF RID: 6895 RVA: 0x000760D9 File Offset: 0x000750D9
		public ik(y A_0)
		{
			this.a = new c3(A_0);
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x000760ED File Offset: 0x000750ED
		public ik(c3 A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x000760FC File Offset: 0x000750FC
		public new gx[] a(int A_0, int A_1)
		{
			y a_ = this.a.b();
			this.a.d(A_0);
			int num = Math.Min(A_0, 109);
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = A_1 + i;
			}
			this.a.a(array);
			gx[] array3;
			if (A_0 > 109)
			{
				int num2 = A_0 - 109;
				int[] array2 = new int[num2];
				for (int j = 0; j < num2; j++)
				{
					array2[j] = A_1 + j + 109;
				}
				array3 = gx.a(a_, array2, A_1 + A_0);
				this.a.b(A_1 + A_0);
			}
			else
			{
				array3 = gx.a(a_, new int[0], 0);
				this.a.b(-2);
			}
			this.a.a(array3.Length);
			return array3;
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x000761CC File Offset: 0x000751CC
		public new int d()
		{
			return this.a.g();
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x000761D9 File Offset: 0x000751D9
		public new void b(int A_0)
		{
			this.a.g(A_0);
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x000761E7 File Offset: 0x000751E7
		public new int c()
		{
			return this.a.e();
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x000761F4 File Offset: 0x000751F4
		public new void d(int A_0)
		{
			this.a.h(A_0);
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x00076202 File Offset: 0x00075202
		public new int b()
		{
			return this.a.e();
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x0007620F File Offset: 0x0007520F
		public new void c(int A_0)
		{
			this.a.h(A_0);
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x0007621D File Offset: 0x0007521D
		public new int a()
		{
			return this.a.i();
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x0007622A File Offset: 0x0007522A
		public new void a(int A_0)
		{
			this.a.e(A_0);
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x00076238 File Offset: 0x00075238
		public new static int a(y A_0, int A_1)
		{
			if (A_1 <= 109)
			{
				return 0;
			}
			return gx.c(A_0, A_1 - 109);
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x0007624C File Offset: 0x0007524C
		public void a3(Stream A_0)
		{
			try
			{
				this.a.b(A_0);
			}
			catch (IOException ex)
			{
				throw ex;
			}
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x00076278 File Offset: 0x00075278
		public new void a(he A_0)
		{
			MemoryStream memoryStream = new MemoryStream(this.a.b().f());
			this.a.b(memoryStream);
			A_0.b(memoryStream.ToArray());
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x000762B4 File Offset: 0x000752B4
		public new void a(byte[] A_0)
		{
			MemoryStream memoryStream = new MemoryStream(this.a.b().f());
			this.a.b(memoryStream);
			byte[] array = memoryStream.ToArray();
			Array.Copy(array, 0, A_0, 0, array.Length);
		}

		// Token: 0x04001313 RID: 4883
		private new c3 a;
	}
}
