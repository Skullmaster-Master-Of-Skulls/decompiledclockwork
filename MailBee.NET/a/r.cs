using System;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MailBee.Proxy;

namespace a
{
	// Token: 0x0200040D RID: 1037
	internal abstract class r : a7
	{
		// Token: 0x0600245D RID: 9309 RVA: 0x0009A481 File Offset: 0x00099481
		public r(string A_0, int A_1, Encoding A_2)
		{
			this.e = A_0;
			this.f = A_1;
			this.i = A_2;
			this.g = string.Empty;
			this.h = string.Empty;
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x0009A4B4 File Offset: 0x000994B4
		public override e a6()
		{
			return global::a.e.b;
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x0009A4B7 File Offset: 0x000994B7
		public override bool a7()
		{
			return false;
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x0009A4BA File Offset: 0x000994BA
		public override Stream d0()
		{
			if (this.k != null)
			{
				return this.k;
			}
			return base.d0();
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x0009A4D4 File Offset: 0x000994D4
		public override void d1(IPEndPoint A_0)
		{
			IPAddress address;
			if (IPAddress.TryParse(this.e, out address))
			{
				this.e.d1(new IPEndPoint(address, this.f));
			}
			else
			{
				IPHostEntry a_ = this.e.hk(this.e);
				this.e.g(a_, this.f);
			}
			if (this.d0() != null)
			{
				this.k = new s(this.d0(), this);
			}
			this.ab(A_0.Address.ToString(), A_0.Port);
			this.m();
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x0009A564 File Offset: 0x00099564
		public override void d2()
		{
			try
			{
				base.d2();
			}
			finally
			{
				this.k = null;
			}
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x0009A594 File Offset: 0x00099594
		public int g(byte[] A_0, int A_1, int A_2)
		{
			int num = 0;
			if (this.j != null && this.j.Length != 0)
			{
				num = ((A_2 > this.j.Length) ? this.j.Length : A_2);
				Array.Copy(this.j, 0, A_0, A_1, num);
				byte[] array = new byte[this.j.Length - num];
				if (array.Length != 0)
				{
					Array.Copy(this.j, num, array, 0, array.Length);
				}
				this.j = array;
			}
			return num;
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x0009A608 File Offset: 0x00099608
		public override int d3(byte[] A_0, int A_1)
		{
			if (this.k != null)
			{
				return this.k.Read(A_0, A_1, A_0.Length - A_1);
			}
			int num = this.g(A_0, A_1, A_0.Length - A_1);
			if (num > 0)
			{
				return num;
			}
			return this.e.d3(A_0, A_1);
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x0009A651 File Offset: 0x00099651
		public override int d4(byte[] A_0, int A_1, int A_2)
		{
			if (this.k != null)
			{
				this.k.Write(A_0, A_1, A_2);
				return A_2;
			}
			return this.e.d4(A_0, A_1, A_2);
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x0009A67C File Offset: 0x0009967C
		protected void m()
		{
			byte[] a_ = this.ac();
			if (!this.ad(a_))
			{
				throw new MailBeeProxyAuthorizationException(70, this.hs());
			}
		}

		// Token: 0x06002467 RID: 9319
		protected abstract void ab(string A_0, int A_1);

		// Token: 0x06002468 RID: 9320
		protected abstract byte[] ac();

		// Token: 0x06002469 RID: 9321
		protected abstract bool ad(byte[] A_0);

		// Token: 0x0600246A RID: 9322 RVA: 0x0009A6A8 File Offset: 0x000996A8
		public override Task d5(IPEndPoint A_0)
		{
			r.c c;
			c.c = this;
			c.d = A_0;
			c.b = AsyncTaskMethodBuilder.Create();
			c.a = -1;
			AsyncTaskMethodBuilder b = c.b;
			b.Start<r.c>(ref c);
			return c.b.Task;
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x0009A6F8 File Offset: 0x000996F8
		public override Task<int> d6(byte[] A_0, int A_1)
		{
			if (this.k != null)
			{
				return this.k.ReadAsync(A_0, A_1, A_0.Length - A_1);
			}
			int num = this.g(A_0, A_1, A_0.Length - A_1);
			if (num > 0)
			{
				return Task.FromResult<int>(num);
			}
			return this.e.d6(A_0, A_1);
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x0009A748 File Offset: 0x00099748
		protected Task n()
		{
			r.a a;
			a.c = this;
			a.b = AsyncTaskMethodBuilder.Create();
			a.a = -1;
			AsyncTaskMethodBuilder b = a.b;
			b.Start<r.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x0009A790 File Offset: 0x00099790
		public override Task<int> d7(byte[] A_0, int A_1, int A_2)
		{
			r.b b;
			b.c = this;
			b.d = A_0;
			b.e = A_1;
			b.f = A_2;
			b.b = AsyncTaskMethodBuilder<int>.Create();
			b.a = -1;
			AsyncTaskMethodBuilder<int> b2 = b.b;
			b2.Start<r.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x0600246E RID: 9326
		protected abstract Task ae(string A_0, int A_1);

		// Token: 0x0600246F RID: 9327
		protected abstract Task<byte[]> af();

		// Token: 0x0400181B RID: 6171
		protected new string e;

		// Token: 0x0400181C RID: 6172
		protected new int f;

		// Token: 0x0400181D RID: 6173
		protected new string g;

		// Token: 0x0400181E RID: 6174
		protected new string h;

		// Token: 0x0400181F RID: 6175
		protected new Encoding i;

		// Token: 0x04001820 RID: 6176
		protected new byte[] j;

		// Token: 0x04001821 RID: 6177
		private new s k;
	}
}
