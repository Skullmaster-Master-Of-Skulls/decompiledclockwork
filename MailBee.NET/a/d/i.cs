using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using a.n;
using MailBee;
using MailBee.Mime;
using MailBee.SmtpMail;

namespace a.d
{
	// Token: 0x02000473 RID: 1139
	internal class i : global::a.d.h
	{
		// Token: 0x0600276F RID: 10095 RVA: 0x000B67F4 File Offset: 0x000B57F4
		public i(bo A_0, bc A_1, Logger A_2, int A_3) : base(A_0, A_1, A_2, A_3)
		{
			this.a = null;
			this.b = 0;
			this.c = false;
			this.d = true;
			if (this.b != null)
			{
				this.e = (global::a.d.i.a)Delegate.Combine(this.e, new global::a.d.i.a(this.a));
			}
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x000B6854 File Offset: 0x000B5854
		public override void f4(string A_0, int A_1, string A_2, string A_3)
		{
			if (this.a.Count == 0)
			{
				throw new MailBeeInvalidArgumentException(300);
			}
			if (this.b >= this.a.Count)
			{
				this.b = 0;
			}
			this.f0(this.a[this.b].Server);
			base.f4(A_0, A_1, A_2, A_3);
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x000B68BC File Offset: 0x000B58BC
		private new void b()
		{
			if (this.a.Count > 0 && base.g())
			{
				int num = this.a.c(((global::a.d.d)this.av()).j());
				int i = 0;
				int num2 = this.b + 1;
				while (i < num - 1)
				{
					if (num2 == num)
					{
						num2 = 0;
					}
					if (!this.a[num2].Server.e())
					{
						this.b = num2;
						this.f0(this.a[this.b].Server);
						return;
					}
					i++;
					num2++;
				}
			}
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x000B695C File Offset: 0x000B595C
		public override void fy()
		{
			if (this.a.Count == 0)
			{
				throw new MailBeeInvalidArgumentException(300);
			}
			if (this.b >= this.a.Count)
			{
				this.b = 0;
			}
			if (this.c)
			{
				this.f0(this.a[this.b].Server);
				this.b();
				base.fy();
				return;
			}
			this.c = true;
			try
			{
				int num = 1;
				for (;;)
				{
					this.f0(this.a[this.b].Server);
					if (num == this.a.Count)
					{
						break;
					}
					this.b();
					try
					{
						base.fy();
						return;
					}
					catch (MailBeeNetworkException a_)
					{
						base.c(a_);
						base.@as();
					}
					this.b++;
					if (this.b >= this.a.Count)
					{
						this.b = 0;
					}
					num++;
				}
				base.fy();
			}
			finally
			{
				this.c = false;
			}
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x000B6A78 File Offset: 0x000B5A78
		public override void f5(MailMessage A_0, string A_1, EmailAddressCollection A_2, DeliveryNotificationOptions A_3, Smtp8bitDataConversion A_4, bool A_5, bool A_6, SendFailureThreshold A_7, bool A_8, global::a.d.k A_9, string A_10, global::a.n.a A_11)
		{
			if (this.a.Count == 0)
			{
				throw new MailBeeInvalidArgumentException(300);
			}
			if (this.b >= this.a.Count)
			{
				this.b = 0;
			}
			if (this.a.Count == 1)
			{
				this.f0(this.a[this.b].Server);
				base.f5(A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8, A_9, A_10, A_11);
				return;
			}
			this.c = true;
			try
			{
				bool flag = false;
				int num = 1;
				for (;;)
				{
					this.f0(this.a[this.b].Server);
					try
					{
						if (num == this.a.Count)
						{
							base.f5(A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8, A_9, A_10, A_11);
							flag = true;
							break;
						}
						try
						{
							this.d = false;
							base.f5(A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8, A_9, A_10, A_11);
							flag = true;
							break;
						}
						catch (MailBeeNetworkException ex)
						{
							MailBeeSmtpNegativeResponseException ex2 = ex as MailBeeSmtpNegativeResponseException;
							if (ex2 is IMailBeeSmtpSendException && (!ex2.IsTransientError || !this.a(A_8, A_0, A_1, A_2, ex2, A_9, A_10, A_11)))
							{
								throw;
							}
							base.c(ex);
							base.@as();
						}
						finally
						{
							this.d = true;
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
					num++;
				}
			}
			finally
			{
				this.c = false;
			}
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x000B6C34 File Offset: 0x000B5C34
		public override void f6(MailMessage A_0, string A_1, EmailAddressCollection A_2, MailBeeException A_3, global::a.d.k A_4, string A_5, global::a.n.a A_6)
		{
			if (this.d)
			{
				base.f6(A_0, A_1, A_2, A_3, A_4, A_5, A_6);
			}
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x000B6C50 File Offset: 0x000B5C50
		public new bool a(bool A_0, MailMessage A_1, string A_2, EmailAddressCollection A_3, MailBeeSmtpNegativeResponseException A_4, global::a.d.k A_5, string A_6, global::a.n.a A_7)
		{
			if (this.e != null)
			{
				SmtpTransientErrorOccurredEventArgs smtpTransientErrorOccurredEventArgs = new SmtpTransientErrorOccurredEventArgs(A_1, A_2, A_3, A_4, A_5, A_6, A_7, this);
				smtpTransientErrorOccurredEventArgs.Continue = !A_0;
				base.a(this.e, new object[]
				{
					smtpTransientErrorOccurredEventArgs,
					this
				});
				return smtpTransientErrorOccurredEventArgs.Continue;
			}
			return !A_0;
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x000B6CA8 File Offset: 0x000B5CA8
		public new void a(SmtpTransientErrorOccurredEventArgs A_0, bc A_1)
		{
			global::a.d.o o = (global::a.d.o)this.b;
			if (this.b.bq() && o.mh() && !this.b.bf())
			{
				o.mi(A_0);
			}
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x000B6CEA File Offset: 0x000B5CEA
		public new SmtpServerCollection d()
		{
			return this.a;
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x000B6CF2 File Offset: 0x000B5CF2
		public new void a(SmtpServerCollection A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x000B6CFB File Offset: 0x000B5CFB
		public new int c()
		{
			return this.b;
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x000B6D03 File Offset: 0x000B5D03
		public new void a(int A_0)
		{
			this.b = A_0;
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x000B6D0C File Offset: 0x000B5D0C
		public override Task f7(string A_0, int A_1, string A_2, string A_3)
		{
			if (this.a.Count == 0)
			{
				throw new MailBeeInvalidArgumentException(300);
			}
			if (this.b >= this.a.Count)
			{
				this.b = 0;
			}
			this.f0(this.a[this.b].Server);
			return base.f7(A_0, A_1, A_2, A_3);
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x000B6D74 File Offset: 0x000B5D74
		public override Task f2()
		{
			global::a.d.i.b b;
			b.c = this;
			b.b = AsyncTaskMethodBuilder.Create();
			b.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = b.b;
			asyncTaskMethodBuilder.Start<global::a.d.i.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x000B6DBC File Offset: 0x000B5DBC
		public override Task f8(MailMessage A_0, string A_1, EmailAddressCollection A_2, DeliveryNotificationOptions A_3, Smtp8bitDataConversion A_4, bool A_5, bool A_6, SendFailureThreshold A_7, bool A_8, global::a.d.k A_9, string A_10, global::a.n.a A_11)
		{
			global::a.d.i.c c;
			c.c = this;
			c.d = A_0;
			c.e = A_1;
			c.f = A_2;
			c.g = A_3;
			c.h = A_4;
			c.i = A_5;
			c.j = A_6;
			c.k = A_7;
			c.l = A_8;
			c.m = A_9;
			c.n = A_10;
			c.o = A_11;
			c.b = AsyncTaskMethodBuilder.Create();
			c.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = c.b;
			asyncTaskMethodBuilder.Start<global::a.d.i.c>(ref c);
			return c.b.Task;
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x000B6E6A File Offset: 0x000B5E6A
		[CompilerGenerated]
		[DebuggerHidden]
		private new Task a()
		{
			return base.f2();
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x000B6E74 File Offset: 0x000B5E74
		[CompilerGenerated]
		[DebuggerHidden]
		private new Task a(MailMessage A_0, string A_1, EmailAddressCollection A_2, DeliveryNotificationOptions A_3, Smtp8bitDataConversion A_4, bool A_5, bool A_6, SendFailureThreshold A_7, bool A_8, global::a.d.k A_9, string A_10, global::a.n.a A_11)
		{
			return base.f8(A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8, A_9, A_10, A_11);
		}

		// Token: 0x04001AF1 RID: 6897
		protected new SmtpServerCollection a;

		// Token: 0x04001AF2 RID: 6898
		protected new int b;

		// Token: 0x04001AF3 RID: 6899
		protected new bool c;

		// Token: 0x04001AF4 RID: 6900
		protected new bool d;

		// Token: 0x04001AF5 RID: 6901
		private new global::a.d.i.a e;

		// Token: 0x02000474 RID: 1140
		// (Invoke) Token: 0x06002781 RID: 10113
		protected new delegate void a(SmtpTransientErrorOccurredEventArgs A_0, bc A_1);
	}
}
