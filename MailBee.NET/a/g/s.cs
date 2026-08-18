using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MailBee;
using MailBee.DnsMX;

namespace a.g
{
	// Token: 0x020003FE RID: 1022
	internal class s : p
	{
		// Token: 0x06002414 RID: 9236 RVA: 0x0009983A File Offset: 0x0009883A
		public s(bo A_0, bc A_1, Logger A_2, int A_3) : base(A_0, A_1, A_2, A_3)
		{
			this.a = null;
			this.b = 0;
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x00099858 File Offset: 0x00098858
		public override void o5(string A_0, h A_1, bool A_2, bool A_3)
		{
			this.b = null;
			this.c = A_0;
			this.d = A_1;
			if (this.a.Count == 0)
			{
				throw new MailBeeInvalidArgumentException(212);
			}
			if (this.b >= this.a.Count)
			{
				this.b = 0;
			}
			bool flag = false;
			for (int i = 0; i < this.a.Count; i++)
			{
				base.a(this.a[this.b]);
				try
				{
					if (i == this.a.Count - 1)
					{
						base.o5(A_0, A_1, A_2, A_3);
						flag = true;
						break;
					}
					if (!this.e.b() && !this.e.c() && this.e.UdpRetryCount > 0)
					{
						try
						{
							base.o5(A_0, A_1, A_2, A_3);
							flag = true;
							break;
						}
						catch (MailBeeDnsNameErrorException)
						{
							flag = true;
							throw;
						}
						catch (MailBeeNetworkException a_)
						{
							base.c(a_);
						}
					}
				}
				finally
				{
					if (!flag)
					{
						this.b++;
						if (this.b >= this.a.Count)
						{
							this.b = 0;
						}
					}
				}
			}
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x000999A0 File Offset: 0x000989A0
		public new DnsServerCollection a()
		{
			return this.a;
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x000999A8 File Offset: 0x000989A8
		public new void a(DnsServerCollection A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x000999B1 File Offset: 0x000989B1
		public new int b()
		{
			return this.b;
		}

		// Token: 0x06002419 RID: 9241 RVA: 0x000999B9 File Offset: 0x000989B9
		public new void a(int A_0)
		{
			this.b = A_0;
		}

		// Token: 0x0600241A RID: 9242 RVA: 0x000999C4 File Offset: 0x000989C4
		public override Task o6(string A_0, h A_1, bool A_2, bool A_3)
		{
			s.a a;
			a.c = this;
			a.d = A_0;
			a.e = A_1;
			a.f = A_2;
			a.g = A_3;
			a.b = AsyncTaskMethodBuilder.Create();
			a.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = a.b;
			asyncTaskMethodBuilder.Start<s.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x00099A2A File Offset: 0x00098A2A
		[CompilerGenerated]
		[DebuggerHidden]
		private new Task a(string A_0, h A_1, bool A_2, bool A_3)
		{
			return base.o6(A_0, A_1, A_2, A_3);
		}

		// Token: 0x040017E9 RID: 6121
		private new DnsServerCollection a;

		// Token: 0x040017EA RID: 6122
		protected new int b;
	}
}
