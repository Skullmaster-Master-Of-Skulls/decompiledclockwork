using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using a.g;
using a.n;
using MailBee;
using MailBee.AddressCheck;
using MailBee.AntiSpam;
using MailBee.DnsMX;
using MailBee.Mime;
using MailBee.Security;
using MailBee.SmtpMail;

namespace a.d
{
	// Token: 0x02000428 RID: 1064
	internal class f : bc, global::a.d.g, global::a.d.c
	{
		// Token: 0x06002509 RID: 9481 RVA: 0x0009F218 File Offset: 0x0009E218
		public f(bo A_0, bc A_1, Logger A_2, int A_3) : base(A_0, A_1, A_2, A_3)
		{
			this.a = new EmailAddressCollection();
			this.b = new EmailAddressCollection();
			this.g = null;
			this.h = null;
			this.i = null;
			this.j = null;
			this.d = null;
			this.c = null;
			this.e = null;
			this.f = false;
			this.k = null;
			this.l = null;
			this.m = null;
			this.n = null;
			this.o = null;
			this.p = null;
			this.q = null;
			this.r = null;
			this.s = null;
			if (this.b != null)
			{
				this.k = (global::a.d.f.i)Delegate.Combine(this.k, new global::a.d.f.i(this.a));
				this.l = (global::a.d.f.t)Delegate.Combine(this.l, new global::a.d.f.t(this.a));
				this.m = (global::a.d.f.j)Delegate.Combine(this.m, new global::a.d.f.j(this.a));
				this.n = (global::a.d.f.f)Delegate.Combine(this.n, new global::a.d.f.f(this.a));
				this.o = (global::a.d.f.l)Delegate.Combine(this.o, new global::a.d.f.l(this.a));
				this.p = (global::a.d.f.a)Delegate.Combine(this.p, new global::a.d.f.a(this.a));
				this.q = (global::a.d.f.o)Delegate.Combine(this.q, new global::a.d.f.o(this.a));
				this.r = (global::a.d.f.u)Delegate.Combine(this.r, new global::a.d.f.u(this.a));
				this.s = (global::a.d.f.c)Delegate.Combine(this.s, new global::a.d.f.c(this.a));
			}
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x0009F3FA File Offset: 0x0009E3FA
		public override string er()
		{
			return "SEND";
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x0009F401 File Offset: 0x0009E401
		protected override void fw(MailBeeException A_0)
		{
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x0009F404 File Offset: 0x0009E404
		public override void fx()
		{
			base.fx();
			aw aw = this.g;
			aw aw2 = this.h;
			global::a.d.i i = this.i;
			if (aw != null)
			{
				for (int j = 0; j < aw.Count; j++)
				{
					aw.a(j).fx();
				}
			}
			if (aw2 != null)
			{
				for (int k = 0; k < aw2.Count; k++)
				{
					aw2.a(k).fx();
				}
			}
			if (i != null)
			{
				i.fx();
			}
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x0009F47A File Offset: 0x0009E47A
		public new void a(MailMessage A_0, string A_1, EmailAddressCollection A_2, EmailAddressCollection A_3, EmailAddressCollection A_4)
		{
			if (this.m != null)
			{
				base.a(this.m, new object[]
				{
					A_0,
					A_1,
					A_2,
					A_3,
					A_4,
					this
				});
			}
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x0009F4B4 File Offset: 0x0009E4B4
		public new void a(MailMessage A_0, string A_1, EmailAddressCollection A_2, EmailAddressCollection A_3, EmailAddressCollection A_4, bc A_5)
		{
			global::a.d.o o = (global::a.d.o)this.b;
			if (this.b.bq() && o.mt() && !this.b.bf())
			{
				SmtpMessageDirectSendDoneEventArgs a_ = new SmtpMessageDirectSendDoneEventArgs(A_0, A_1, A_2, A_3, A_4, A_5);
				o.mu(a_);
			}
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x0009F505 File Offset: 0x0009E505
		public new void a(MailMessage A_0, StringCollection A_1, StringCollection A_2, StringCollection A_3)
		{
			if (this.n != null)
			{
				base.a(this.n, new object[]
				{
					A_0,
					A_1,
					A_2,
					A_3,
					this
				});
			}
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x0009F538 File Offset: 0x0009E538
		public new void a(MailMessage A_0, StringCollection A_1, StringCollection A_2, StringCollection A_3, bc A_4)
		{
			global::a.d.o o = (global::a.d.o)this.b;
			if (this.b.bq() && o.mr() && !this.b.bf())
			{
				SmtpMessageMXLookupDoneEventArgs a_ = new SmtpMessageMXLookupDoneEventArgs(A_0, A_1, A_2, A_3, A_4);
				o.ms(a_);
			}
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x0009F588 File Offset: 0x0009E588
		public new void a(MailMessage A_0, string A_1, EmailAddressCollection A_2, EmailAddressCollection A_3, EmailAddressCollection A_4, global::a.d.k A_5, string A_6, global::a.n.a A_7)
		{
			if (this.o != null)
			{
				base.a(this.o, new object[]
				{
					A_0,
					A_1,
					A_2,
					A_3,
					A_4,
					A_5,
					A_6,
					A_7,
					this
				});
			}
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x0009F5DC File Offset: 0x0009E5DC
		public new void a(MailMessage A_0, string A_1, EmailAddressCollection A_2, EmailAddressCollection A_3, EmailAddressCollection A_4, global::a.d.k A_5, string A_6, global::a.n.a A_7, bc A_8)
		{
			global::a.d.o o = (global::a.d.o)this.b;
			if (this.b.bq() && o.md() && !this.b.bf())
			{
				SmtpMessageSentEventArgs a_ = new SmtpMessageSentEventArgs(A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8);
				o.me(a_);
			}
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x0009F634 File Offset: 0x0009E634
		public new void a(MailMessage A_0, string A_1, EmailAddressCollection A_2, MailBeeException A_3, global::a.d.k A_4, string A_5, global::a.n.a A_6)
		{
			if (this.p != null)
			{
				base.a(this.p, new object[]
				{
					A_0,
					A_1,
					A_2,
					A_3,
					A_4,
					A_5,
					A_6,
					this
				});
			}
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x0009F680 File Offset: 0x0009E680
		public new void a(MailMessage A_0, string A_1, EmailAddressCollection A_2, MailBeeException A_3, global::a.d.k A_4, string A_5, global::a.n.a A_6, bc A_7)
		{
			global::a.d.o o = (global::a.d.o)this.b;
			if (this.b.bq() && o.mf() && !this.b.bf())
			{
				SmtpMessageNotSentEventArgs a_ = new SmtpMessageNotSentEventArgs(A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7);
				o.mg(a_);
			}
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x0009F6D8 File Offset: 0x0009E6D8
		public bool es(MailMessage A_0, string A_1, EmailAddressCollection A_2, DeliveryNotificationOptions A_3, global::a.d.k A_4, string A_5, global::a.n.a A_6)
		{
			if (this.k != null)
			{
				SmtpMergingMessageEventArgs smtpMergingMessageEventArgs = new SmtpMergingMessageEventArgs(A_0, A_1, A_2, A_3, A_4, A_5, A_6, this);
				base.a(this.k, new object[]
				{
					smtpMergingMessageEventArgs,
					this
				});
				return smtpMergingMessageEventArgs.MergeIt;
			}
			return true;
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x0009F724 File Offset: 0x0009E724
		public new void a(SmtpMergingMessageEventArgs A_0, bc A_1)
		{
			global::a.d.o o = (global::a.d.o)this.b;
			if (this.b.bq() && o.mj() && !this.b.bf())
			{
				o.mk(A_0);
			}
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x0009F768 File Offset: 0x0009E768
		public new SmtpSendingMessageEventArgs a(MailMessage A_0, string A_1, EmailAddressCollection A_2, DeliveryNotificationOptions A_3, global::a.d.k A_4, string A_5)
		{
			if (this.l != null)
			{
				SmtpSendingMessageEventArgs smtpSendingMessageEventArgs = new SmtpSendingMessageEventArgs(A_0, A_1, A_2, A_3, A_4, A_5, this);
				base.a(this.l, new object[]
				{
					smtpSendingMessageEventArgs,
					this
				});
				return smtpSendingMessageEventArgs;
			}
			return null;
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x0009F7AC File Offset: 0x0009E7AC
		public new void a(SmtpSendingMessageEventArgs A_0, bc A_1)
		{
			global::a.d.o o = (global::a.d.o)this.b;
			if (this.b.bq() && o.l3() && !this.b.bf())
			{
				o.l4(A_0);
			}
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x0009F7F0 File Offset: 0x0009E7F0
		public new SmtpSubmittingMessageToPickupFolderEventArgs b(MailMessage A_0, string A_1, EmailAddressCollection A_2, string A_3, string A_4, global::a.d.k A_5, string A_6)
		{
			if (this.q != null)
			{
				SmtpSubmittingMessageToPickupFolderEventArgs smtpSubmittingMessageToPickupFolderEventArgs = new SmtpSubmittingMessageToPickupFolderEventArgs(A_0, A_1, A_2, A_3, A_4, A_5, A_6, this);
				base.a(this.q, new object[]
				{
					smtpSubmittingMessageToPickupFolderEventArgs,
					this
				});
				return smtpSubmittingMessageToPickupFolderEventArgs;
			}
			return null;
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x0009F838 File Offset: 0x0009E838
		public new void a(SmtpSubmittingMessageToPickupFolderEventArgs A_0, bc A_1)
		{
			global::a.d.o o = (global::a.d.o)this.b;
			if (this.b.bq() && o.ml() && !this.b.bf())
			{
				o.mm(A_0);
			}
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x0009F87C File Offset: 0x0009E87C
		public new void a(MailMessage A_0, string A_1, EmailAddressCollection A_2, string A_3, string A_4, global::a.d.k A_5, string A_6)
		{
			if (this.r != null)
			{
				base.a(this.r, new object[]
				{
					A_0,
					A_1,
					A_2,
					A_3,
					A_4,
					A_5,
					A_6,
					this
				});
			}
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x0009F8C8 File Offset: 0x0009E8C8
		public new void a(MailMessage A_0, string A_1, EmailAddressCollection A_2, string A_3, string A_4, global::a.d.k A_5, string A_6, bc A_7)
		{
			global::a.d.o o = (global::a.d.o)this.b;
			if (this.b.bq() && o.mn() && !this.b.bf())
			{
				SmtpMessageSubmittedToPickupFolderEventArgs a_ = new SmtpMessageSubmittedToPickupFolderEventArgs(A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7);
				o.mo(a_);
			}
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x0009F920 File Offset: 0x0009E920
		public bool et(SendMailJob A_0)
		{
			if (this.s != null)
			{
				SmtpFinishingJobEventArgs smtpFinishingJobEventArgs = new SmtpFinishingJobEventArgs(A_0, this);
				base.a(this.s, new object[]
				{
					smtpFinishingJobEventArgs,
					this
				});
				return smtpFinishingJobEventArgs.KeepIt;
			}
			return true;
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x0009F960 File Offset: 0x0009E960
		public new void a(SmtpFinishingJobEventArgs A_0, bc A_1)
		{
			global::a.d.o o = (global::a.d.o)this.b;
			if (this.b.bq() && o.mp() && !this.b.bf())
			{
				o.mq(A_0);
			}
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x0009F9A4 File Offset: 0x0009E9A4
		private new static StringCollection a(EmailAddressCollection A_0)
		{
			StringCollection stringCollection = new StringCollection();
			for (int i = 0; i < A_0.Count; i++)
			{
				stringCollection.Add(A_0[i].GetDomain().ToLower());
			}
			global::a.bb.a(stringCollection);
			return stringCollection;
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x0009F9E8 File Offset: 0x0009E9E8
		private new global::a.g.f a(string A_0, global::a.g.h A_1)
		{
			if (DnsCache.Enabled)
			{
				object obj = DnsCache.a(A_1).a();
				lock (obj)
				{
					global::a.g.f f = DnsCache.a(A_1).b(A_0);
					if (f == null)
					{
						return null;
					}
					if (f.f())
					{
						DnsCache.a(A_1).a(A_0);
						return null;
					}
					return f;
				}
			}
			return null;
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x0009FA60 File Offset: 0x0009EA60
		private new void a(string A_0, global::a.g.f A_1, global::a.g.h A_2)
		{
			if (DnsCache.Enabled)
			{
				object obj = DnsCache.a(A_2).a();
				lock (obj)
				{
					if (DnsCache.a(A_2).b(A_0) == null)
					{
						DnsCache.a(A_2).a(A_0, A_1);
					}
				}
			}
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x0009FAC4 File Offset: 0x0009EAC4
		private new global::a.g.d a(StringCollection A_0, bool A_1, int A_2, ae A_3)
		{
			this.d.b(string.Format(Resources.Instance.Log_SendMailWillGetMXLists, new object[0]), null, LogMessageType.Info, this);
			if (A_0.Count == 0)
			{
				throw new MailBeeInvalidArgumentException(402);
			}
			if (this.c.Count == 0)
			{
				throw new MailBeeInvalidArgumentException(212);
			}
			this.g = new aw();
			global::a.g.s s = null;
			int num = A_0.IndexOf(string.Empty);
			if (num > -1)
			{
				A_0.RemoveAt(num);
			}
			int num2;
			WaitHandle[] array;
			WaitHandle[] a_;
			base.a(A_0.Count, A_2, A_3, out num2, out array, out a_);
			if (num2 == 0)
			{
				num2 = 1;
			}
			int num3 = 0;
			MailBeeException ex = null;
			bool flag = true;
			bool flag2 = false;
			int num4 = 0;
			string text = null;
			try
			{
				text = Dns.GetHostName();
			}
			catch (SocketException a_2)
			{
				throw new MailBeeGetLocalHostNameException(50, a_2);
			}
			global::a.g.d d = new global::a.g.d();
			int num5 = this.c.h();
			for (int i = 0; i < num2; i++)
			{
				s = new global::a.g.s(this.b, this, this.d, i);
				s.a(this.c);
				s.hc(this.k);
				s.hd(this.l);
				this.g.a(s);
			}
			int j = 0;
			int num6 = 0;
			while (j < A_0.Count)
			{
				s = (global::a.g.s)this.g.a(num3);
				s.a(num6);
				s.i(j);
				this.d.b(string.Format(Resources.Instance.Log_SendMailWillMakeDnsQueryToDnsAt0RegardingHost1, this.c[num6].Host, A_0[j]), null, LogMessageType.Info, this);
				if (num2 == 1)
				{
					if (num4 == A_0.Count - 1 && num < 0)
					{
						A_1 = false;
					}
					this.j = s;
					s.a(base.ba());
					try
					{
						s.a(this.a(A_0[j], global::a.g.h.o));
						if (s.e() == null)
						{
							s.o5(A_0[j], global::a.g.h.o, true, true);
							this.j = null;
							this.d.b(string.Format(Resources.Instance.Log_SendMailMadeDnsQueryToDnsAt0RegardingHost1, s.g().Host, s.c()), null, LogMessageType.Info, this);
							this.j = s;
							s.c(s.c());
							s.d(text);
							s.e().SortByPriority();
							this.j = null;
							this.d.b(string.Format(Resources.Instance.Log_SendMailProcessedDnsQueryToDnsAt0RegardingHost1, s.g().Host, s.c()), null, LogMessageType.Info, this);
							this.j = s;
							if (DnsCache.Enabled)
							{
								this.a(A_0[j], s.e(), global::a.g.h.o);
							}
						}
						else
						{
							this.j = null;
							this.d.b(string.Format(Resources.Instance.Log_SendMailGotDnsInfoRegardingHost0FromCache, A_0[j]), null, LogMessageType.Info, this);
							if (s.e().d() || !s.e().c())
							{
								int a_3 = s.e().d() ? 410 : 411;
								s.a(null);
								throw new MailBeeMXRecordsDisabledException(a_3, A_0[j]);
							}
						}
						goto IL_480;
					}
					catch (MailBeeUserAbortException ex)
					{
						flag2 = true;
						goto IL_480;
					}
					catch (MailBeeException ex2)
					{
						if (A_1)
						{
							num4++;
							if (this.j == null)
							{
								base.c(ex2);
							}
							else
							{
								s.c(ex2);
							}
						}
						else
						{
							ex = ex2;
							flag2 = true;
						}
						if (DnsCache.Enabled && ex2 is MailBeeDnsProtocolNegativeResponseException)
						{
							this.a(A_0[j], new global::a.g.f(true), global::a.g.h.o);
						}
						goto IL_480;
					}
					finally
					{
						this.j = null;
						d.a(s.e());
					}
					goto IL_38F;
				}
				goto IL_38F;
				IL_480:
				num6++;
				if (num6 >= num5)
				{
					num6 = 0;
				}
				if (num3 > 0 && (j == A_0.Count - 1 || num3 == num2))
				{
					for (int k = 0; k < num3; k++)
					{
						if (num4 == A_0.Count - 1 && num < 0)
						{
							A_1 = false;
						}
						base.a(this.g, null, array, a_, k);
						s = (global::a.g.s)this.g.a(k);
						s.a(base.ba());
						try
						{
							s.d();
							this.d.b(string.Format(Resources.Instance.Log_SendMailMadeDnsQueryToDnsAt0RegardingHost1, s.g().Host, s.c()), null, LogMessageType.Info, this);
							s.c(s.c());
							s.d(text);
							s.e().SortByPriority();
							if (DnsCache.Enabled)
							{
								this.a(s.c(), s.e(), global::a.g.h.o);
							}
							this.d.b(string.Format(Resources.Instance.Log_SendMailProcessedDnsQueryToDnsAt0RegardingHost1, s.g().Host, s.c()), null, LogMessageType.Info, this);
						}
						catch (MailBeeUserAbortException ex)
						{
							flag2 = true;
							flag = false;
						}
						catch (MailBeeException ex3)
						{
							if (A_1)
							{
								num4++;
								if (flag)
								{
									this.j = s;
									s.c(ex3);
									this.j = null;
								}
							}
							else if (!flag2)
							{
								ex = ex3;
								flag2 = true;
							}
							if (DnsCache.Enabled && ex3 is MailBeeDnsProtocolNegativeResponseException)
							{
								this.a(s.c(), new global::a.g.f(true), global::a.g.h.o);
							}
						}
						finally
						{
							d.a(s.e());
						}
					}
					num3 = 0;
				}
				if (!flag2)
				{
					j++;
					continue;
				}
				break;
				IL_38F:
				if (num3 >= num2)
				{
					goto IL_480;
				}
				s.a(this.a(A_0[j], global::a.g.h.o));
				if (s.e() == null)
				{
					s.bc();
					array[num3] = s.a(A_0[j], global::a.g.h.o, true, true, null, null).AsyncWaitHandle;
					num3++;
					goto IL_480;
				}
				this.d.b(string.Format(Resources.Instance.Log_SendMailGotDnsInfoRegardingHost0FromCache, A_0[j]), null, LogMessageType.Info, this);
				if (s.e().d() || !s.e().c())
				{
					int a_4 = s.e().d() ? 410 : 411;
					s.a(null);
					MailBeeException ex4 = new MailBeeMXRecordsDisabledException(a_4, A_0[j]);
					if (A_1)
					{
						num4++;
						if (flag)
						{
							base.c(ex4);
						}
					}
					else if (!flag2)
					{
						ex = ex4;
						flag2 = true;
					}
				}
				d.a(s.e());
				goto IL_480;
			}
			if (num2 > 1)
			{
				base.a(A_2, -num2, A_3);
			}
			while (d.Count < A_0.Count)
			{
				d.a(null);
			}
			if (num > -1)
			{
				A_0.Insert(num, string.Empty);
				global::a.g.f f = new global::a.g.f();
				f.a(new global::a.g.q(0, (Global.LocalSmtpMXServerName == null) ? text : Global.LocalSmtpMXServerName));
				d.a(num, f);
			}
			this.g = null;
			if (ex != null)
			{
				throw ex;
			}
			this.d.b(string.Format(Resources.Instance.Log_SendMailGotMXListsFor0DomainsOf1Total, A_0.Count - num4, A_0.Count), null, LogMessageType.Info, this);
			return d;
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x000A025C File Offset: 0x0009F25C
		public new string[] a(string A_0, int A_1, ae A_2)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			global::a.g.d d = this.a(new StringCollection
			{
				A_0
			}, false, A_1, A_2);
			d.a(0).SortByPriority();
			global::a.g.f f = d.a(0);
			string[] array = new string[f.Count];
			for (int i = 0; i < f.Count; i++)
			{
				array[i] = ((global::a.g.q)f.b(i)).a();
			}
			return array;
		}

		// Token: 0x06002524 RID: 9508 RVA: 0x000A02D4 File Offset: 0x0009F2D4
		private new string[] b(global::a.g.f A_0)
		{
			StringCollection stringCollection = null;
			string[] array = null;
			foreach (object obj in A_0)
			{
				global::a.g.m m = (global::a.g.m)obj;
				if (m is global::a.g.l)
				{
					if (array == null)
					{
						array = ((global::a.g.l)m).a();
					}
					else
					{
						if (stringCollection == null)
						{
							stringCollection = new StringCollection();
							stringCollection.AddRange(array);
						}
						stringCollection.AddRange(((global::a.g.l)m).a());
					}
				}
			}
			if (array == null)
			{
				return null;
			}
			if (stringCollection == null)
			{
				return array;
			}
			array = new string[stringCollection.Count];
			stringCollection.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x000A0384 File Offset: 0x0009F384
		public new string[] c(string A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			global::a.g.s s = new global::a.g.s(this.b, this, this.d, 0);
			s.a(this.c);
			s.hc(this.k);
			s.hd(this.l);
			this.j = s;
			s.a(base.ba());
			try
			{
				s.a(this.a(A_0, global::a.g.h.p));
				if (s.e() == null)
				{
					try
					{
						s.o5(A_0, global::a.g.h.p, true, true);
					}
					catch (MailBeeDnsProtocolNegativeResponseException)
					{
						if (DnsCache.Enabled)
						{
							this.a(A_0, new global::a.g.f(true), global::a.g.h.p);
						}
						throw;
					}
					if (DnsCache.Enabled)
					{
						this.a(A_0, s.e(), global::a.g.h.p);
					}
				}
				else if (s.e().d())
				{
					throw new MailBeeDnsRecordsDisabledException(410, A_0);
				}
			}
			finally
			{
				this.j = null;
			}
			return this.b(s.e());
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x000A0488 File Offset: 0x0009F488
		private new string[] a(global::a.g.f A_0)
		{
			StringCollection stringCollection = null;
			foreach (object obj in A_0)
			{
				global::a.g.m m = (global::a.g.m)obj;
				if (m is global::a.g.c)
				{
					if (stringCollection == null)
					{
						stringCollection = new StringCollection();
					}
					stringCollection.Add(((global::a.g.c)m).a());
				}
			}
			if (stringCollection == null)
			{
				return null;
			}
			string[] array = new string[stringCollection.Count];
			stringCollection.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x000A051C File Offset: 0x0009F51C
		public new string[] b(string A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			string[] array = A_0.Split(new char[]
			{
				'.'
			});
			if (array.Length != 4)
			{
				throw new MailBeeInvalidArgumentException(20);
			}
			A_0 = string.Concat(new string[]
			{
				array[3],
				".",
				array[2],
				".",
				array[1],
				".",
				array[0],
				".in-addr.arpa"
			});
			global::a.g.s s = new global::a.g.s(this.b, this, this.d, 0);
			s.a(this.c);
			s.hc(this.k);
			s.hd(this.l);
			this.j = s;
			s.a(base.ba());
			try
			{
				s.a(this.a(A_0, global::a.g.h.l));
				if (s.e() == null)
				{
					try
					{
						s.o5(A_0, global::a.g.h.l, false, true);
					}
					catch (MailBeeDnsProtocolNegativeResponseException)
					{
						if (DnsCache.Enabled)
						{
							this.a(A_0, new global::a.g.f(true), global::a.g.h.l);
						}
						throw;
					}
					if (DnsCache.Enabled)
					{
						this.a(A_0, s.e(), global::a.g.h.l);
					}
				}
				else if (s.e().d())
				{
					throw new MailBeeDnsRecordsDisabledException(410, A_0);
				}
			}
			finally
			{
				this.j = null;
			}
			return this.a(s.e());
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x000A0684 File Offset: 0x0009F684
		public new bool a(string A_0, string A_1)
		{
			this.d.b(string.Format(Resources.Instance.Log_SendMailWillGetARblRecordsForIP0, A_0), null, LogMessageType.Info, this);
			if (A_0 == null || A_1 == null || A_0 == string.Empty || A_1 == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			A_0 = this.a(A_0) + "." + A_1;
			global::a.g.s s = new global::a.g.s(this.b, this, this.d, 0);
			s.a(this.c);
			s.hc(this.k);
			s.hd(this.l);
			this.j = s;
			s.a(base.ba());
			try
			{
				s.a(this.a(A_0, global::a.g.h.a));
				if (s.e() == null)
				{
					try
					{
						s.o5(A_0, global::a.g.h.a, false, false);
					}
					catch (MailBeeDnsProtocolNegativeResponseException)
					{
						if (DnsCache.Enabled)
						{
							this.a(A_0, new global::a.g.f(true), global::a.g.h.a);
						}
						throw;
					}
					catch (MailBeeConnectionException)
					{
						if (DnsCache.Enabled)
						{
							this.a(A_0, new global::a.g.f(true), global::a.g.h.a);
						}
						throw;
					}
					if (DnsCache.Enabled)
					{
						this.a(A_0, s.e(), global::a.g.h.a);
					}
				}
				else if (s.e().d())
				{
					throw new MailBeeDnsRecordsDisabledException(410, A_0);
				}
			}
			finally
			{
				this.j = null;
			}
			this.d.b(string.Format(Resources.Instance.Log_SendMailGotARblListsFor0RblsOf1Total, 1, 1), null, LogMessageType.Info, this);
			return !s.e().b();
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x000A0824 File Offset: 0x0009F824
		private new string a(string A_0)
		{
			string[] array = A_0.Split(new char[]
			{
				'.'
			});
			if (array.Length != 4)
			{
				throw new MailBeeInvalidArgumentException(20);
			}
			return string.Concat(new string[]
			{
				array[3],
				".",
				array[2],
				".",
				array[1],
				".",
				array[0]
			});
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x000A088C File Offset: 0x0009F88C
		private new global::a.g.d a(string A_0, string[] A_1, bool A_2, int A_3, ae A_4)
		{
			this.d.b(string.Format(Resources.Instance.Log_SendMailWillGetARblRecordsForIP0, A_0), null, LogMessageType.Info, this);
			if (A_1.Length == 0)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			if (this.c.Count == 0)
			{
				throw new MailBeeInvalidArgumentException(212);
			}
			string[] array = new string[A_1.Length];
			string str = this.a(A_0);
			for (int i = 0; i < A_1.Length; i++)
			{
				if (A_1[i] == null || A_1[i] == string.Empty)
				{
					throw new MailBeeInvalidArgumentException(22);
				}
				array[i] = str + "." + A_1[i];
			}
			this.g = new aw();
			global::a.g.s s = null;
			int num;
			WaitHandle[] array2;
			WaitHandle[] a_;
			base.a(A_1.Length, A_3, A_4, out num, out array2, out a_);
			if (num == 0)
			{
				num = 1;
			}
			int num2 = 0;
			MailBeeException ex = null;
			bool flag = true;
			bool flag2 = false;
			int num3 = 0;
			global::a.g.d d = new global::a.g.d();
			int num4 = this.c.h();
			for (int j = 0; j < num; j++)
			{
				s = new global::a.g.s(this.b, this, this.d, j);
				s.a(this.c);
				s.hc(this.k);
				s.hd(this.l);
				this.g.a(s);
			}
			int k = 0;
			int num5 = 0;
			while (k < A_1.Length)
			{
				s = (global::a.g.s)this.g.a(num2);
				s.a(num5);
				s.i(k);
				this.d.b(string.Format(Resources.Instance.Log_SendMailWillMakeDnsQueryToDnsAt0RegardingHost1, this.c[num5].Host, array[k]), null, LogMessageType.Info, this);
				if (num == 1)
				{
					if (num3 == A_1.Length - 1)
					{
						A_2 = false;
					}
					this.j = s;
					s.a(base.ba());
					try
					{
						s.a(this.a(array[k], global::a.g.h.a));
						if (s.e() == null)
						{
							s.o5(array[k], global::a.g.h.a, true, false);
							this.j = null;
							this.d.b(string.Format(Resources.Instance.Log_SendMailMadeDnsQueryToDnsAt0RegardingHost1, s.g().Host, s.c()), null, LogMessageType.Info, this);
							this.j = s;
							if (DnsCache.Enabled)
							{
								this.a(array[k], s.e(), global::a.g.h.a);
							}
						}
						else
						{
							this.j = null;
							this.d.b(string.Format(Resources.Instance.Log_SendMailGotDnsInfoRegardingHost0FromCache, array[k]), null, LogMessageType.Info, this);
							if (s.e().d())
							{
								s.a(null);
								throw new MailBeeDnsRecordsDisabledException(410, array[k]);
							}
						}
						goto IL_3C2;
					}
					catch (MailBeeUserAbortException ex)
					{
						flag2 = true;
						goto IL_3C2;
					}
					catch (MailBeeException ex2)
					{
						if (A_2)
						{
							num3++;
							if (this.j == null)
							{
								base.c(ex2);
							}
							else
							{
								s.c(ex2);
							}
						}
						else
						{
							ex = ex2;
							flag2 = true;
						}
						if (DnsCache.Enabled && (ex2 is MailBeeDnsProtocolNegativeResponseException || ex2 is MailBeeConnectionException))
						{
							this.a(array[k], new global::a.g.f(true), global::a.g.h.a);
						}
						goto IL_3C2;
					}
					finally
					{
						this.j = null;
						d.a(s.e());
					}
					goto IL_306;
				}
				goto IL_306;
				IL_3C2:
				num5++;
				if (num5 >= num4)
				{
					num5 = 0;
				}
				if (num2 > 0 && (k == array.Length - 1 || num2 == num))
				{
					for (int l = 0; l < num2; l++)
					{
						if (num3 == array.Length - 1)
						{
							A_2 = false;
						}
						base.a(this.g, null, array2, a_, l);
						s = (global::a.g.s)this.g.a(l);
						s.a(base.ba());
						try
						{
							s.d();
							this.d.b(string.Format(Resources.Instance.Log_SendMailMadeDnsQueryToDnsAt0RegardingHost1, s.g().Host, s.c()), null, LogMessageType.Info, this);
							if (DnsCache.Enabled)
							{
								this.a(s.c(), s.e(), global::a.g.h.a);
							}
						}
						catch (MailBeeUserAbortException ex)
						{
							flag2 = true;
							flag = false;
						}
						catch (MailBeeException ex3)
						{
							if (A_2)
							{
								num3++;
								if (flag)
								{
									this.j = s;
									s.c(ex3);
									this.j = null;
								}
							}
							else if (!flag2)
							{
								ex = ex3;
								flag2 = true;
							}
							if (DnsCache.Enabled && (ex3 is MailBeeDnsProtocolNegativeResponseException || ex3 is MailBeeConnectionException))
							{
								this.a(s.c(), new global::a.g.f(true), global::a.g.h.a);
							}
						}
						finally
						{
							d.a(s.e());
						}
					}
					num2 = 0;
				}
				if (!flag2)
				{
					k++;
					continue;
				}
				break;
				IL_306:
				if (num2 >= num)
				{
					goto IL_3C2;
				}
				s.a(this.a(array[k], global::a.g.h.a));
				if (s.e() == null)
				{
					s.bc();
					array2[num2] = s.a(array[k], global::a.g.h.a, true, false, null, null).AsyncWaitHandle;
					num2++;
					goto IL_3C2;
				}
				this.d.b(string.Format(Resources.Instance.Log_SendMailGotDnsInfoRegardingHost0FromCache, array[k]), null, LogMessageType.Info, this);
				if (s.e().d())
				{
					s.a(null);
					MailBeeException ex4 = new MailBeeDnsRecordsDisabledException(410, array[k]);
					if (A_2)
					{
						num3++;
						if (flag)
						{
							base.c(ex4);
						}
					}
					else if (!flag2)
					{
						ex = ex4;
						flag2 = true;
					}
				}
				d.a(s.e());
				goto IL_3C2;
			}
			if (num > 1)
			{
				base.a(A_3, -num, A_4);
			}
			while (d.Count < array.Length)
			{
				d.a(null);
			}
			this.g = null;
			if (ex != null)
			{
				throw ex;
			}
			this.d.b(string.Format(Resources.Instance.Log_SendMailGotARblListsFor0RblsOf1Total, array.Length - num3, array.Length), null, LogMessageType.Info, this);
			return d;
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x000A0EB4 File Offset: 0x0009FEB4
		public new RblStatusCollection a(string A_0, string[] A_1, int A_2, ae A_3)
		{
			global::a.g.d d = this.a(A_0, A_1, true, A_2, A_3);
			RblStatusCollection rblStatusCollection = new RblStatusCollection();
			for (int i = 0; i < A_1.Length; i++)
			{
				global::a.g.f f = d.a(i);
				RblStatus a_ = (f != null) ? new RblStatus(A_1[i], !f.b(), f.b() ? null : ((global::a.g.r)f.b(0)).a()) : new RblStatus(A_1[i]);
				rblStatusCollection.a(a_);
			}
			return rblStatusCollection;
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x000A0F30 File Offset: 0x0009FF30
		private new SmtpServerCollection a(string A_0, global::a.g.f A_1, bool A_2)
		{
			SmtpServerCollection smtpServerCollection = new SmtpServerCollection();
			for (int i = 0; i < A_1.Count; i++)
			{
				global::a.g.q q = (global::a.g.q)A_1.b(i);
				smtpServerCollection.a(new SmtpServer(q.a(), 25, q.get_Priority(), this.e.Timeout, this.e.Pipelining, AuthenticationMethods.None, string.Empty, string.Empty, A_2, this.e.HelloDomain, this.e.SmtpOptions)
				{
					SslMode = (this.e.EnableStartTls ? SslStartupMode.UseStartTlsIfSupported : SslStartupMode.Manual),
					LocalEndPoint = this.e.LocalEndPoint
				});
			}
			return smtpServerCollection;
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x000A0FE4 File Offset: 0x0009FFE4
		public new static EmailAddressCollection a(string A_0, EmailAddressCollection A_1)
		{
			EmailAddressCollection emailAddressCollection = new EmailAddressCollection();
			for (int i = 0; i < A_1.Count; i++)
			{
				if (A_1[i].GetDomain().ToLower() == A_0)
				{
					emailAddressCollection.Add(A_1[i]);
				}
			}
			return emailAddressCollection;
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x000A1030 File Offset: 0x000A0030
		private new void a(MailMessage A_0, string A_1, EmailAddressCollection A_2, bool A_3, DeliveryNotificationOptions A_4, Smtp8bitDataConversion A_5, bool A_6, bool A_7, StringCollection A_8, global::a.g.d A_9, int A_10, ae A_11, global::a.d.k A_12, string A_13, global::a.n.a A_14)
		{
			this.d.b(string.Format(Resources.Instance.Log_SendMailWillSendToRecipientDomains, new object[0]), null, LogMessageType.Info, this);
			if (A_8.Count == 0)
			{
				throw new MailBeeInvalidArgumentException(401);
			}
			if (A_8.Count != A_9.Count)
			{
				throw new InvalidOperationException();
			}
			global::a.d.i i = null;
			this.h = new aw();
			int num;
			WaitHandle[] array;
			WaitHandle[] a_;
			base.a(A_8.Count, A_10, A_11, out num, out array, out a_);
			if (num == 0)
			{
				num = 1;
			}
			int num2 = 0;
			MailBeeException ex = null;
			bool flag = true;
			bool flag2 = false;
			int num3 = 0;
			for (int j = 0; j < num; j++)
			{
				i = new global::a.d.i(this.b, this, this.d, j);
				i.hc(this.k);
				i.hd(this.l);
				this.h.a(i);
			}
			for (int k = 0; k < A_8.Count; k++)
			{
				SmtpServerCollection a_2 = this.a(A_8[k], A_9.a(k), A_3);
				EmailAddressCollection a_3 = global::a.d.f.a(A_8[k], A_2);
				i = (global::a.d.i)this.h.a(k % num);
				i.a(a_2);
				i.a(0);
				i.i(k);
				this.d.b(string.Format(Resources.Instance.Log_SendMailWillSendToMXesOfDomain0, A_8[k]), null, LogMessageType.Info, this);
				if (num == 1)
				{
					if (num3 == A_8.Count - 1)
					{
						A_3 = false;
					}
					this.j = i;
					i.a(base.ba());
					bool flag3 = false;
					try
					{
						i.f5(A_0, A_1, a_3, A_4, A_5, A_6, A_7, SendFailureThreshold.Default, true, A_12, A_13, A_14);
						flag3 = true;
						this.a.Add(i.r());
						this.b.Add(i.m());
						if (i.c() > 0 && DnsCache.Enabled)
						{
							A_9.a(k).b(i.c()).b();
							for (int l = 0; l < i.c(); l++)
							{
								A_9.a(k).b(l).d();
							}
						}
					}
					catch (MailBeeUserAbortException ex)
					{
						flag2 = true;
					}
					catch (MailBeeException ex2)
					{
						this.b.Add(i.h());
						if (DnsCache.Enabled && ex2 is MailBeeNetworkException && !(ex2 is MailBeeEmailProtocolException))
						{
							A_9.a(k).g();
						}
						if (A_3)
						{
							num3++;
							i.c(ex2);
						}
						else
						{
							ex = ex2;
							flag2 = true;
						}
					}
					finally
					{
						i.ha();
						this.j = null;
					}
					if (flag3)
					{
						this.d.b(string.Format(Resources.Instance.Log_SendMailSentToMXesOfDomain0, A_8[i.bb()]), null, LogMessageType.Info, this);
					}
				}
				else if (num2 < num)
				{
					i.bc();
					array[num2] = i.a(A_0, A_1, a_3, A_4, A_5, A_6, A_7, SendFailureThreshold.Default, true, A_12, A_13, A_14, null, null).AsyncWaitHandle;
					num2++;
				}
				if (num2 > 0 && (k == A_8.Count - 1 || num2 == num))
				{
					for (int m = 0; m < num2; m++)
					{
						if (num3 == A_8.Count - 1)
						{
							A_3 = false;
						}
						base.a(this.h, null, array, a_, m);
						i = (global::a.d.i)this.h.a(m);
						i.a(base.ba());
						bool flag4 = false;
						try
						{
							i.i();
							flag4 = true;
							this.a.Add(i.r());
							this.b.Add(i.m());
							if (i.c() > 0 && DnsCache.Enabled)
							{
								A_9.a(i.bb()).b(i.c()).b();
								for (int n = 0; n < i.c(); n++)
								{
									A_9.a(i.bb()).b(n).d();
								}
							}
						}
						catch (MailBeeUserAbortException ex)
						{
							flag = false;
						}
						catch (MailBeeException ex3)
						{
							this.b.Add(i.h());
							if (DnsCache.Enabled && ex3 is MailBeeNetworkException && !(ex3 is MailBeeEmailProtocolException))
							{
								A_9.a(i.bb()).g();
							}
							if (A_3)
							{
								num3++;
								if (flag)
								{
									this.j = i;
									i.c(ex3);
									this.j = null;
								}
							}
							else if (!flag2)
							{
								ex = ex3;
								flag2 = true;
							}
						}
						finally
						{
							this.j = i;
							i.ha();
							this.j = null;
						}
						if (flag4)
						{
							this.d.b(string.Format(Resources.Instance.Log_SendMailSentToMXesOfDomain0, A_8[i.bb()]), null, LogMessageType.Info, this);
						}
					}
					num2 = 0;
				}
				if (flag2)
				{
					for (int num4 = k + 1; num4 < A_8.Count; num4++)
					{
						this.b.Add(global::a.d.f.a(A_8[num4], A_2));
					}
					break;
				}
			}
			if (num > 1)
			{
				base.a(A_10, -num, A_11);
			}
			this.h = null;
			if (ex != null)
			{
				throw ex;
			}
			this.d.b(string.Format(Resources.Instance.Log_SendMailSentToRecipientDomains, new object[0]), null, LogMessageType.Info, this);
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x000A1614 File Offset: 0x000A0614
		private new void a(MailMessage A_0, string A_1, EmailAddressCollection A_2, DeliveryNotificationOptions A_3, Smtp8bitDataConversion A_4, bool A_5, bool A_6, bool A_7, bool A_8, int A_9, ae A_10, global::a.d.k A_11, string A_12, global::a.n.a A_13)
		{
			global::a.d.o o = null;
			if (this.b != null && this.b.bq())
			{
				o = (global::a.d.o)this.b;
			}
			if (A_2.Count == 1)
			{
				A_8 = false;
			}
			if (A_7)
			{
				string format = A_8 ? Resources.Instance.Log_SendMailFailedRecipientsAllowed : Resources.Instance.Log_SendMailFailedRecipientsNotAllowed;
				this.d.b(string.Format(Resources.Instance.Log_SendMailWillSendViaMXLookup, new object[0]) + " " + string.Format(format, new object[0]), null, LogMessageType.Info, this);
			}
			else
			{
				this.d.b(string.Format(Resources.Instance.Log_SendMailWillTestSendViaMXLookup, new object[0]), null, LogMessageType.Info, this);
			}
			StringCollection stringCollection = global::a.d.f.a(A_2);
			global::a.g.d d = null;
			try
			{
				d = this.a(stringCollection, A_8, A_9, A_10);
			}
			finally
			{
				if (d == null)
				{
					this.b = A_2;
				}
			}
			StringCollection stringCollection2 = new StringCollection();
			for (int i = stringCollection.Count - 1; i > -1; i--)
			{
				if (d.a(i) == null)
				{
					this.b.Add(global::a.d.f.a(stringCollection[i], A_2));
					stringCollection2.Add(stringCollection[i]);
					d.RemoveAt(i);
					stringCollection.RemoveAt(i);
				}
			}
			if (o != null && o.mr() && !this.b.bf())
			{
				StringCollection a_ = stringCollection;
				if (stringCollection2.Count > 0)
				{
					a_ = global::a.d.f.a(A_2);
				}
				this.a(A_0, a_, stringCollection, stringCollection2);
			}
			if (A_5)
			{
				this.a(A_0, A_1, A_2, A_8, A_3, A_4, A_6, A_7, stringCollection, d, A_9, A_10, A_11, A_12, A_13);
				if (A_7)
				{
					this.d.b(string.Format(Resources.Instance.Log_SendMailSentViaMXLookup, new object[0]), null, LogMessageType.Info, this);
				}
				else
				{
					this.d.b(string.Format(Resources.Instance.Log_SendMailTestViaMXLookupDone, new object[0]), null, LogMessageType.Info, this);
				}
				if (o != null && o.mt() && !this.b.bf())
				{
					this.a(A_0, A_1, A_2, this.a, this.b);
				}
			}
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x000A1838 File Offset: 0x000A0838
		public void h()
		{
			this.f = false;
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x000A1844 File Offset: 0x000A0844
		private new bool a(MailBeeException A_0)
		{
			if (!(A_0 is IMailBeeSendException))
			{
				return false;
			}
			if (A_0 is MailBeeSmtpSendNegativeResponseException)
			{
				MailBeeSmtpSendNegativeResponseException ex = (MailBeeSmtpSendNegativeResponseException)A_0;
				return !ex.IsTransientError || ex.ResponseCode == 451;
			}
			return true;
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x000A1884 File Offset: 0x000A0884
		public new void a(MailMessage A_0, string A_1, EmailAddressCollection A_2, DeliveryNotificationOptions A_3, Smtp8bitDataConversion A_4, bool A_5, bool A_6, bool A_7, SendFailureThreshold A_8, int A_9, ae A_10, bool A_11, global::a.d.k A_12, string A_13, global::a.n.a A_14)
		{
			this.h();
			global::a.d.o o = null;
			if (this.b != null && this.b.bq())
			{
				o = (global::a.d.o)this.b;
			}
			SmtpSendingMessageEventArgs smtpSendingMessageEventArgs = null;
			if (o != null && o.l3() && !this.b.bf())
			{
				smtpSendingMessageEventArgs = this.a(A_0, A_1, A_2, A_3, A_12, A_13);
			}
			if (smtpSendingMessageEventArgs == null || smtpSendingMessageEventArgs.SendIt)
			{
				if (smtpSendingMessageEventArgs != null)
				{
					A_1 = smtpSendingMessageEventArgs.ActualSenderEmail;
					A_2 = smtpSendingMessageEventArgs.ActualRecipients;
					if (A_1 == null && A_0 != null)
					{
						A_1 = A_0.From.Email;
					}
					if (A_2 == null && A_0 != null)
					{
						A_2 = A_0.GetAllRecipients();
					}
				}
				if (A_7)
				{
					this.d.b(string.Format(Resources.Instance.Log_SendMailWillSend, new object[0]), null, LogMessageType.Info, this);
				}
				else
				{
					this.d.b(string.Format(Resources.Instance.Log_SendMailWillTestSend, new object[0]), null, LogMessageType.Info, this);
				}
				if (A_1 == null)
				{
					MailBeeException ex = new MailBeeInvalidArgumentException(312);
					this.a(false, A_0, A_1, A_2, ex, A_12, A_13, A_14);
					throw ex;
				}
				if (A_2 == null || A_2.Count == 0)
				{
					MailBeeException ex2 = new MailBeeInvalidArgumentException(314);
					this.a(false, A_0, A_1, A_2, ex2, A_12, A_13, A_14);
					throw ex2;
				}
				if (this.c.Count == 0 && this.d.Count == 0)
				{
					throw new MailBeeInvalidArgumentException(400);
				}
				if (A_14 != null && !A_14.b().IsMatch(A_2.ToString()))
				{
					MailBeeException ex3 = new MailBeeDataSyntaxException(45);
					this.a(false, A_0, A_1, A_2, ex3, A_12, A_13, A_14);
					throw ex3;
				}
				bool flag = this.d.i() <= this.c.i();
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = true;
				EmailAddressCollection emailAddressCollection = A_2;
				this.a = new EmailAddressCollection();
				this.b = new EmailAddressCollection();
				if (A_14 == null || A_14.c() > AddressValidationLevel.RegexCheck)
				{
					if (A_0 != null)
					{
						A_0.n();
					}
					for (int i = 0; i < 2; i++)
					{
						if (i == 0)
						{
							emailAddressCollection = A_2;
						}
						else
						{
							emailAddressCollection = this.b;
							this.b = new EmailAddressCollection();
						}
						if (this.d.Count > 0 && ((i == 0 && flag) || (i == 1 && !flag2)))
						{
							flag2 = true;
							this.a();
							this.j = this.i;
							this.i.a(base.ba());
							try
							{
								if (A_11 && !this.i.ao())
								{
									this.i.fy();
								}
								this.i.f5(A_0, A_1, emailAddressCollection, A_3, A_4, A_6, A_7, A_8, true, A_12, A_13, A_14);
								this.a.Add(this.i.r());
								this.b = this.i.m();
								flag4 = !A_11;
								if (this.b.Count == 0 || this.c.Count == 0)
								{
									break;
								}
							}
							catch (MailBeeUserAbortException)
							{
								throw;
							}
							catch (MailBeeException ex4)
							{
								this.b = emailAddressCollection;
								if (this.a(ex4) || ((i == 1 || this.c.Count == 0) && (this.a.Count == 0 || !A_7)))
								{
									this.j = null;
									this.a(false, A_0, A_1, emailAddressCollection, ex4, A_12, A_13, A_14);
									this.j = this.i;
									throw;
								}
								this.i.c(ex4);
							}
							finally
							{
								if (flag4)
								{
									this.i.ha();
								}
								this.j = null;
							}
						}
						if (this.c.Count > 0 && ((i == 0 && !flag) || (i == 1 && !flag3)))
						{
							flag3 = true;
							try
							{
								bool a_ = A_8 != SendFailureThreshold.AnyRecipientsFailed;
								this.a(A_0, A_1, emailAddressCollection, A_3, A_4, A_5, A_6, A_7, a_, A_9, A_10, A_12, A_13, A_14);
								if (this.b.Count == 0 || this.d.Count == 0)
								{
									break;
								}
							}
							catch (MailBeeUserAbortException)
							{
								throw;
							}
							catch (MailBeeException ex5)
							{
								if (this.a(ex5) || ((i == 1 || this.d.Count == 0) && (this.a.Count == 0 || !A_7)))
								{
									this.a(false, A_0, A_1, emailAddressCollection, ex5, A_12, A_13, A_14);
									throw;
								}
								base.c(ex5);
							}
						}
					}
					if (A_7)
					{
						this.d.b(string.Format(Resources.Instance.Log_SendMailDone, new object[0]), null, LogMessageType.Info, this);
					}
					else
					{
						this.d.b(string.Format(Resources.Instance.Log_SendMailTestDone, new object[0]), null, LogMessageType.Info, this);
					}
				}
				else
				{
					this.a = A_2;
				}
				this.f = true;
				if (o != null && o.md() && !this.b.bf())
				{
					this.a(A_0, A_1, A_2, this.a, this.b, A_12, A_13, A_14);
				}
			}
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x000A1D9C File Offset: 0x000A0D9C
		private new void a(bool A_0, MailMessage A_1, string A_2, EmailAddressCollection A_3, MailBeeException A_4, global::a.d.k A_5, string A_6, global::a.n.a A_7)
		{
			if (!this.f)
			{
				global::a.d.o o = null;
				if (this.b != null && this.b.bq())
				{
					o = (global::a.d.o)this.b;
				}
				if (o != null && o.mf() && !this.b.bf())
				{
					this.a(A_1, A_2, A_3, A_4, A_5, A_6, A_7);
				}
			}
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x000A1E00 File Offset: 0x000A0E00
		private new void a()
		{
			if (this.i == null)
			{
				this.i = new global::a.d.i(this.b, this, this.d, 0);
				this.i.hc(this.k);
				this.i.hd(this.l);
				this.i.a(this.d);
			}
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x000A1E64 File Offset: 0x000A0E64
		public new string a(MailMessage A_0, string A_1, string A_2, string A_3, EmailAddressCollection A_4, bool A_5, global::a.d.k A_6, string A_7, ref string A_8)
		{
			global::a.d.o o = null;
			if (this.b != null && this.b.bq())
			{
				o = (global::a.d.o)this.b;
			}
			if (A_3 == null)
			{
				A_3 = A_0.From.Email;
			}
			if (A_4 == null)
			{
				A_4 = A_0.GetAllRecipients();
			}
			string text = A_1;
			SmtpSubmittingMessageToPickupFolderEventArgs smtpSubmittingMessageToPickupFolderEventArgs = null;
			if (o != null && o.ml() && !this.b.bf())
			{
				smtpSubmittingMessageToPickupFolderEventArgs = this.b(A_0, A_3, A_4, A_1, A_2, A_6, A_7);
			}
			if (smtpSubmittingMessageToPickupFolderEventArgs == null || smtpSubmittingMessageToPickupFolderEventArgs.SubmitIt)
			{
				if (smtpSubmittingMessageToPickupFolderEventArgs != null)
				{
					A_2 = smtpSubmittingMessageToPickupFolderEventArgs.Filename;
					text = smtpSubmittingMessageToPickupFolderEventArgs.PickupFolderName;
					A_3 = smtpSubmittingMessageToPickupFolderEventArgs.ActualSenderEmail;
					A_4 = smtpSubmittingMessageToPickupFolderEventArgs.ActualRecipients;
					if (A_3 == null)
					{
						A_3 = A_0.From.Email;
					}
					if (A_4 == null)
					{
						A_4 = A_0.GetAllRecipients();
					}
				}
				else
				{
					text = A_1;
				}
				this.d.b(string.Format(Resources.Instance.Log_SendMailWillSubmitMessageToPickupFolder, new object[0]), null, LogMessageType.Info, this);
				EmailAddressCollection emailAddressCollection = global::a.d.a.a(A_0, ref A_8);
				try
				{
					A_2 = A_0.a(text, A_2, A_3, A_4, A_5);
				}
				finally
				{
					if (emailAddressCollection != null)
					{
						A_0.Bcc.Add(emailAddressCollection);
					}
				}
				this.d.b(string.Format(Resources.Instance.Log_SendMailMessageSubmittedToPickupFolderAs0, A_2), null, LogMessageType.Info, this);
				if (o != null && o.mn() && !this.b.bf())
				{
					this.a(A_0, A_3, A_4, text, A_2, A_6, A_7);
				}
				return A_2;
			}
			return null;
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x000A1FD8 File Offset: 0x000A0FD8
		public new EmailAddressCollection k()
		{
			return this.a;
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x000A1FE0 File Offset: 0x000A0FE0
		public new EmailAddressCollection e()
		{
			return this.b;
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x000A1FE8 File Offset: 0x000A0FE8
		public new SmtpServerCollection j()
		{
			return this.d;
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x000A1FF0 File Offset: 0x000A0FF0
		public new void a(SmtpServerCollection A_0)
		{
			this.d = A_0;
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x000A1FF9 File Offset: 0x000A0FF9
		public new int g()
		{
			this.a();
			return this.i.c();
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x000A200C File Offset: 0x000A100C
		public new void a(int A_0)
		{
			this.a();
			this.i.a(A_0);
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x000A2020 File Offset: 0x000A1020
		public new DnsServerCollection d()
		{
			return this.c;
		}

		// Token: 0x0600253D RID: 9533 RVA: 0x000A2028 File Offset: 0x000A1028
		public new void a(DnsServerCollection A_0)
		{
			this.c = A_0;
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x000A2031 File Offset: 0x000A1031
		public new DirectSendServerConfig f()
		{
			return this.e;
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x000A2039 File Offset: 0x000A1039
		public new void a(DirectSendServerConfig A_0)
		{
			this.e = A_0;
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x000A2042 File Offset: 0x000A1042
		public new global::a.d.i b()
		{
			return this.i;
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x000A204A File Offset: 0x000A104A
		public new bool i()
		{
			return this.f;
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x000A2052 File Offset: 0x000A1052
		public new bc c()
		{
			return this.j;
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x000A205A File Offset: 0x000A105A
		public new void a(bc A_0)
		{
			this.j = A_0;
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x000A2063 File Offset: 0x000A1063
		protected override Task f1(MailBeeException A_0)
		{
			return Task.FromResult<int>(0);
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x000A206C File Offset: 0x000A106C
		private new Task<global::a.g.d> a(StringCollection A_0, bool A_1, int A_2)
		{
			global::a.d.f.k k;
			k.c = this;
			k.d = A_0;
			k.k = A_1;
			k.e = A_2;
			k.b = AsyncTaskMethodBuilder<global::a.g.d>.Create();
			k.a = -1;
			AsyncTaskMethodBuilder<global::a.g.d> asyncTaskMethodBuilder = k.b;
			asyncTaskMethodBuilder.Start<global::a.d.f.k>(ref k);
			return k.b.Task;
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x000A20CC File Offset: 0x000A10CC
		public new Task<string[]> c(string A_0, int A_1)
		{
			global::a.d.f.s s;
			s.d = this;
			s.c = A_0;
			s.e = A_1;
			s.b = AsyncTaskMethodBuilder<string[]>.Create();
			s.a = -1;
			AsyncTaskMethodBuilder<string[]> asyncTaskMethodBuilder = s.b;
			asyncTaskMethodBuilder.Start<global::a.d.f.s>(ref s);
			return s.b.Task;
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x000A2124 File Offset: 0x000A1124
		public new Task<string[]> b(string A_0, int A_1)
		{
			global::a.d.f.d d;
			d.d = this;
			d.c = A_0;
			d.b = AsyncTaskMethodBuilder<string[]>.Create();
			d.a = -1;
			AsyncTaskMethodBuilder<string[]> asyncTaskMethodBuilder = d.b;
			asyncTaskMethodBuilder.Start<global::a.d.f.d>(ref d);
			return d.b.Task;
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x000A2174 File Offset: 0x000A1174
		public new Task<string[]> a(string A_0, int A_1)
		{
			global::a.d.f.e e;
			e.d = this;
			e.c = A_0;
			e.b = AsyncTaskMethodBuilder<string[]>.Create();
			e.a = -1;
			AsyncTaskMethodBuilder<string[]> asyncTaskMethodBuilder = e.b;
			asyncTaskMethodBuilder.Start<global::a.d.f.e>(ref e);
			return e.b.Task;
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x000A21C4 File Offset: 0x000A11C4
		public new Task<bool> b(string A_0, string A_1)
		{
			global::a.d.f.r r;
			r.c = this;
			r.d = A_0;
			r.e = A_1;
			r.b = AsyncTaskMethodBuilder<bool>.Create();
			r.a = -1;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = r.b;
			asyncTaskMethodBuilder.Start<global::a.d.f.r>(ref r);
			return r.b.Task;
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x000A221C File Offset: 0x000A121C
		private new Task<global::a.g.d> a(string A_0, string[] A_1, bool A_2, int A_3)
		{
			global::a.d.f.q q;
			q.c = this;
			q.d = A_0;
			q.e = A_1;
			q.l = A_2;
			q.f = A_3;
			q.b = AsyncTaskMethodBuilder<global::a.g.d>.Create();
			q.a = -1;
			AsyncTaskMethodBuilder<global::a.g.d> asyncTaskMethodBuilder = q.b;
			asyncTaskMethodBuilder.Start<global::a.d.f.q>(ref q);
			return q.b.Task;
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x000A2284 File Offset: 0x000A1284
		public new Task<RblStatusCollection> a(string A_0, string[] A_1, int A_2)
		{
			global::a.d.f.b b;
			b.c = this;
			b.d = A_0;
			b.e = A_1;
			b.f = A_2;
			b.b = AsyncTaskMethodBuilder<RblStatusCollection>.Create();
			b.a = -1;
			AsyncTaskMethodBuilder<RblStatusCollection> asyncTaskMethodBuilder = b.b;
			asyncTaskMethodBuilder.Start<global::a.d.f.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x000A22E4 File Offset: 0x000A12E4
		private new Task a(MailMessage A_0, string A_1, EmailAddressCollection A_2, bool A_3, DeliveryNotificationOptions A_4, Smtp8bitDataConversion A_5, bool A_6, bool A_7, StringCollection A_8, global::a.g.d A_9, int A_10, global::a.d.k A_11, string A_12, global::a.n.a A_13)
		{
			global::a.d.f.g g;
			g.c = this;
			g.l = A_0;
			g.m = A_1;
			g.h = A_2;
			g.g = A_3;
			g.o = A_4;
			g.p = A_5;
			g.q = A_6;
			g.r = A_7;
			g.d = A_8;
			g.e = A_9;
			g.f = A_10;
			g.s = A_11;
			g.t = A_12;
			g.u = A_13;
			g.b = AsyncTaskMethodBuilder.Create();
			g.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = g.b;
			asyncTaskMethodBuilder.Start<global::a.d.f.g>(ref g);
			return g.b.Task;
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x000A23A4 File Offset: 0x000A13A4
		private new Task a(MailMessage A_0, string A_1, EmailAddressCollection A_2, DeliveryNotificationOptions A_3, Smtp8bitDataConversion A_4, bool A_5, bool A_6, bool A_7, bool A_8, int A_9, global::a.d.k A_10, string A_11, global::a.n.a A_12)
		{
			global::a.d.f.p p;
			p.c = this;
			p.k = A_0;
			p.m = A_1;
			p.d = A_2;
			p.n = A_3;
			p.o = A_4;
			p.l = A_5;
			p.p = A_6;
			p.f = A_7;
			p.e = A_8;
			p.g = A_9;
			p.q = A_10;
			p.r = A_11;
			p.s = A_12;
			p.b = AsyncTaskMethodBuilder.Create();
			p.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = p.b;
			asyncTaskMethodBuilder.Start<global::a.d.f.p>(ref p);
			return p.b.Task;
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x000A245C File Offset: 0x000A145C
		public new Task a(MailMessage A_0, string A_1, EmailAddressCollection A_2, DeliveryNotificationOptions A_3, Smtp8bitDataConversion A_4, bool A_5, bool A_6, bool A_7, SendFailureThreshold A_8, int A_9, bool A_10, global::a.d.k A_11, string A_12, global::a.n.a A_13)
		{
			global::a.d.f.h h;
			h.c = this;
			h.d = A_0;
			h.e = A_1;
			h.f = A_2;
			h.g = A_3;
			h.p = A_4;
			h.v = A_5;
			h.q = A_6;
			h.j = A_7;
			h.r = A_8;
			h.w = A_9;
			h.n = A_10;
			h.h = A_11;
			h.i = A_12;
			h.k = A_13;
			h.b = AsyncTaskMethodBuilder.Create();
			h.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = h.b;
			asyncTaskMethodBuilder.Start<global::a.d.f.h>(ref h);
			return h.b.Task;
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x000A251C File Offset: 0x000A151C
		public new Task<string> a(MailMessage A_0, string A_1, string A_2, string A_3, EmailAddressCollection A_4, bool A_5, global::a.d.k A_6, string A_7, global::a.d.f.m A_8)
		{
			global::a.d.f.n n;
			n.c = this;
			n.e = A_0;
			n.g = A_1;
			n.h = A_2;
			n.d = A_3;
			n.f = A_4;
			n.m = A_5;
			n.i = A_6;
			n.j = A_7;
			n.k = A_8;
			n.b = AsyncTaskMethodBuilder<string>.Create();
			n.a = -1;
			AsyncTaskMethodBuilder<string> asyncTaskMethodBuilder = n.b;
			asyncTaskMethodBuilder.Start<global::a.d.f.n>(ref n);
			return n.b.Task;
		}

		// Token: 0x040018A9 RID: 6313
		private new EmailAddressCollection a;

		// Token: 0x040018AA RID: 6314
		private new EmailAddressCollection b;

		// Token: 0x040018AB RID: 6315
		private new DnsServerCollection c;

		// Token: 0x040018AC RID: 6316
		private new SmtpServerCollection d;

		// Token: 0x040018AD RID: 6317
		private new DirectSendServerConfig e;

		// Token: 0x040018AE RID: 6318
		private new bool f;

		// Token: 0x040018AF RID: 6319
		private new aw g;

		// Token: 0x040018B0 RID: 6320
		private aw h;

		// Token: 0x040018B1 RID: 6321
		private new global::a.d.i i;

		// Token: 0x040018B2 RID: 6322
		protected new bc j;

		// Token: 0x040018B3 RID: 6323
		private new global::a.d.f.i k;

		// Token: 0x040018B4 RID: 6324
		private new global::a.d.f.t l;

		// Token: 0x040018B5 RID: 6325
		private new global::a.d.f.j m;

		// Token: 0x040018B6 RID: 6326
		private global::a.d.f.f n;

		// Token: 0x040018B7 RID: 6327
		private global::a.d.f.l o;

		// Token: 0x040018B8 RID: 6328
		private global::a.d.f.a p;

		// Token: 0x040018B9 RID: 6329
		private global::a.d.f.o q;

		// Token: 0x040018BA RID: 6330
		private global::a.d.f.u r;

		// Token: 0x040018BB RID: 6331
		private global::a.d.f.c s;

		// Token: 0x02000429 RID: 1065
		// (Invoke) Token: 0x06002551 RID: 9553
		protected new delegate void i(SmtpMergingMessageEventArgs A_0, bc A_1);

		// Token: 0x0200042A RID: 1066
		// (Invoke) Token: 0x06002555 RID: 9557
		protected delegate void t(SmtpSendingMessageEventArgs A_0, bc A_1);

		// Token: 0x0200042B RID: 1067
		// (Invoke) Token: 0x06002559 RID: 9561
		protected new delegate void j(MailMessage A_0, string A_1, EmailAddressCollection A_2, EmailAddressCollection A_3, EmailAddressCollection A_4, bc A_5);

		// Token: 0x0200042C RID: 1068
		// (Invoke) Token: 0x0600255D RID: 9565
		protected new delegate void f(MailMessage A_0, StringCollection A_1, StringCollection A_2, StringCollection A_3, bc A_4);

		// Token: 0x0200042D RID: 1069
		// (Invoke) Token: 0x06002561 RID: 9569
		protected new delegate void l(MailMessage A_0, string A_1, EmailAddressCollection A_2, EmailAddressCollection A_3, EmailAddressCollection A_4, global::a.d.k A_5, string A_6, global::a.n.a A_7, bc A_8);

		// Token: 0x0200042E RID: 1070
		// (Invoke) Token: 0x06002565 RID: 9573
		protected new delegate void a(MailMessage A_0, string A_1, EmailAddressCollection A_2, MailBeeException A_3, global::a.d.k A_4, string A_5, global::a.n.a A_6, bc A_7);

		// Token: 0x0200042F RID: 1071
		// (Invoke) Token: 0x06002569 RID: 9577
		protected delegate void o(SmtpSubmittingMessageToPickupFolderEventArgs A_0, bc A_1);

		// Token: 0x02000430 RID: 1072
		// (Invoke) Token: 0x0600256D RID: 9581
		protected delegate void u(MailMessage A_0, string A_1, EmailAddressCollection A_2, string A_3, string A_4, global::a.d.k A_5, string A_6, bc A_7);

		// Token: 0x02000431 RID: 1073
		// (Invoke) Token: 0x06002571 RID: 9585
		protected new delegate void c(SmtpFinishingJobEventArgs A_0, bc A_1);

		// Token: 0x02000432 RID: 1074
		internal new class m
		{
			// Token: 0x06002574 RID: 9588 RVA: 0x000A25AF File Offset: 0x000A15AF
			public m(string A_0)
			{
				this.a = A_0;
			}

			// Token: 0x040018BC RID: 6332
			public string a;
		}
	}
}
