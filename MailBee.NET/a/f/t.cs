using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MailBee;
using MailBee.ImapMail;
using MailBee.Mime;
using MailBee.Security;

namespace a.f
{
	// Token: 0x0200008E RID: 142
	internal class t : global::a.ab
	{
		// Token: 0x060004C5 RID: 1221 RVA: 0x0000DA68 File Offset: 0x0000CA68
		internal t(bo A_0, bc A_1, Logger A_2, int A_3) : base(A_0, A_1, A_2, A_3)
		{
			this.j = global::a.f.j.a();
			this.c = 0;
			this.d = null;
			this.f();
			this.l = false;
			this.n = false;
			this.o = false;
			this.p = null;
			this.q = null;
			this.r = null;
			this.w = true;
			this.v = false;
			this.x = false;
			this.u = null;
			this.y = null;
			this.z = null;
			this.aa = null;
			this.ab = null;
			this.ac = null;
			if (this.b != null)
			{
				this.y = (global::a.f.t.b)Delegate.Combine(this.y, new global::a.f.t.b(this.b));
				this.z = (global::a.f.t.u)Delegate.Combine(this.z, new global::a.f.t.u(this.a));
				this.aa = (global::a.f.t.ad)Delegate.Combine(this.aa, new global::a.f.t.ad(this.a));
				this.ab = (global::a.f.t.v)Delegate.Combine(this.ab, new global::a.f.t.v(this.a));
				this.ac = (global::a.f.t.w)Delegate.Combine(this.ac, new global::a.f.t.w(this.a));
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000DBBD File Offset: 0x0000CBBD
		protected override void ff()
		{
			base.ff();
			this.a.b(new global::a.f.d());
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000DBD5 File Offset: 0x0000CBD5
		protected internal override bf fg(bool A_0)
		{
			return new global::a.f.v(A_0, true, true, this.d);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0000DBE5 File Offset: 0x0000CBE5
		public new bf p()
		{
			return new global::a.f.v(true, true, false, null);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000DBF0 File Offset: 0x0000CBF0
		protected override global::a.u fh()
		{
			return new global::a.f.h(this);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0000DBF8 File Offset: 0x0000CBF8
		public override global::a.al fi()
		{
			return new global::a.f.p();
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0000DBFF File Offset: 0x0000CBFF
		protected override void fj()
		{
			this.f = this.k.u();
			this.c(false);
			this.e();
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0000DC1F File Offset: 0x0000CC1F
		protected override void fk()
		{
			base.fk();
			this.c = 0;
			this.d = null;
			this.l = false;
			this.n = false;
			this.o = false;
			this.x = false;
			this.f();
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0000DC58 File Offset: 0x0000CC58
		private new void f()
		{
			this.e = 0;
			this.f = 0;
			this.g = 0;
			this.h = 0L;
			this.i = 0L;
			this.j = new MessageFlagSet();
			this.k = new MessageFlagSet();
			this.m = false;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0000DCA7 File Offset: 0x0000CCA7
		public override string er()
		{
			return "IMAP";
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0000DCAE File Offset: 0x0000CCAE
		public override TopLevelProtocolType fl()
		{
			return TopLevelProtocolType.Imap;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0000DCB1 File Offset: 0x0000CCB1
		public new void a(ImapEnvelopeDownloadedEventArgs A_0)
		{
			if (this.y != null)
			{
				base.a(this.y, new object[]
				{
					A_0
				});
			}
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0000DCD4 File Offset: 0x0000CCD4
		public new void b(ImapEnvelopeDownloadedEventArgs A_0)
		{
			global::a.f.c c = (global::a.f.c)this.b;
			if (this.b.bq() && c.nj() && !this.b.bf())
			{
				c.nk(A_0);
			}
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0000DD16 File Offset: 0x0000CD16
		public new void a(int A_0, int A_1, int A_2)
		{
			if (this.z != null)
			{
				base.a(this.z, new object[]
				{
					A_0,
					A_1,
					A_2,
					this
				});
			}
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0000DD54 File Offset: 0x0000CD54
		public new void a(int A_0, int A_1, int A_2, bc A_3)
		{
			global::a.f.c c = (global::a.f.c)this.b;
			if (this.b.bq() && c.nl() && !this.b.bf())
			{
				ImapEnvelopeDataChunkReceivedEventArgs a_ = new ImapEnvelopeDataChunkReceivedEventArgs(A_0, A_1, A_2, A_3);
				c.nm(a_);
			}
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0000DDA1 File Offset: 0x0000CDA1
		public new void a(string A_0, string A_1, string A_2, string A_3)
		{
			if (this.aa != null)
			{
				base.a(this.aa, new object[]
				{
					A_0,
					A_1,
					A_2,
					A_3,
					this
				});
			}
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0000DDD4 File Offset: 0x0000CDD4
		public new void a(string A_0, string A_1, string A_2, string A_3, bc A_4)
		{
			global::a.f.c c = (global::a.f.c)this.b;
			if (this.b.bq() && c.nn() && !this.b.bf())
			{
				ImapServerStatusEventArgs a_ = new ImapServerStatusEventArgs(A_0, A_1, A_2, A_3, A_4);
				c.no(a_);
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000DE23 File Offset: 0x0000CE23
		public new void a(string A_0, int A_1, MessageFlagSet A_2)
		{
			if (this.ab != null)
			{
				base.a(this.ab, new object[]
				{
					A_0,
					A_1,
					A_2,
					this
				});
			}
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000DE58 File Offset: 0x0000CE58
		public new void a(string A_0, int A_1, MessageFlagSet A_2, bc A_3)
		{
			global::a.f.c c = (global::a.f.c)this.b;
			if (this.b.bq() && c.np() && !this.b.bf())
			{
				ImapMessageStatusEventArgs a_ = new ImapMessageStatusEventArgs(A_0, A_1, A_2, A_3);
				c.nq(a_);
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000DEA5 File Offset: 0x0000CEA5
		public void x()
		{
			if (this.ac != null)
			{
				base.a(this.ac, new object[]
				{
					this
				});
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000DEC8 File Offset: 0x0000CEC8
		public new void a(bc A_0)
		{
			global::a.f.c c = (global::a.f.c)this.b;
			if (this.b.bq() && c.nr() && !this.b.bf())
			{
				ImapIdlingEventArgs a_ = new ImapIdlingEventArgs(A_0);
				c.ns(a_);
			}
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000DF14 File Offset: 0x0000CF14
		private new void b(global::a.f.a A_0, bool A_1)
		{
			string a_ = A_1 ? A_0.e() : A_0.ToString();
			base.c(new MailBeeInvalidTextResponseException(121, base.a1(), a_, this.bg()));
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000DF50 File Offset: 0x0000CF50
		private new void b(SaslMethod A_0)
		{
			this.af();
			this.g |= global::a.f.i.a(this.h, A_0);
			this.x = (base.t("SPECIAL-USE") == null && base.t("XLIST") != null);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000DFA0 File Offset: 0x0000CFA0
		private new void c(bool A_0)
		{
			global::a.f.c c = null;
			if (this.b != null && this.b.bq())
			{
				c = (global::a.f.c)this.b;
			}
			for (int i = 0; i < base.a5().d().p().Count; i++)
			{
				global::a.f.a a = (global::a.f.a)base.a5().d().p().a(i);
				if (a.n())
				{
					bool flag = false;
					if (a.k() == null)
					{
						this.b(a, true);
					}
					else
					{
						if (a.k().ContainsKey("CAPABILITY"))
						{
							this.h = global::a.f.i.a(a.k()["CAPABILITY"] as ArrayList, this.bg());
							if (this.h == null && !flag)
							{
								this.b(a, true);
								flag = true;
							}
							this.b(this.k.r());
						}
						if (A_0)
						{
							if (a.k().ContainsKey("PERMANENTFLAGS"))
							{
								this.k = (MessageFlagSet)a.k()["PERMANENTFLAGS"];
								if (this.k == null)
								{
									this.k = new MessageFlagSet();
									if (!flag)
									{
										this.b(a, true);
										flag = true;
									}
								}
							}
							if (a.k().ContainsKey("UIDNEXT"))
							{
								this.i = (long)a.k()["UIDNEXT"];
								if (this.i < 0L && !flag)
								{
									this.b(a, true);
									flag = true;
								}
							}
							if (a.k().ContainsKey("UIDVALIDITY"))
							{
								this.h = (long)a.k()["UIDVALIDITY"];
								if (this.h < 0L && !flag)
								{
									this.b(a, true);
									flag = true;
								}
							}
							if (a.k().ContainsKey("UNSEEN"))
							{
								this.g = (int)a.k()["UNSEEN"];
								if (this.g < 0 && !flag)
								{
									this.b(a, true);
								}
							}
						}
						else if (a.l() == "PREAUTH")
						{
							this.e = true;
							this.d.b(string.Format(Resources.Instance.Log_LoggedInAs0, Resources.Instance.Log_ImapPreauthenticatedUser), null, LogMessageType.Info, this);
							if (c != null && c.b9() && !this.b.bf())
							{
								base.ag();
							}
						}
					}
				}
				else if (A_0)
				{
					string text = a.l();
					if (!(text == "FLAGS"))
					{
						if (!(text == "EXISTS"))
						{
							if (!(text == "RECENT"))
							{
								if (text == "EXPUNGE")
								{
									this.e--;
								}
							}
							else
							{
								this.f = a.h();
							}
						}
						else
						{
							this.e = a.h();
						}
					}
					else
					{
						if (a.f() == null || a.f().Count == 0)
						{
							this.j = null;
						}
						else
						{
							this.j = MessageFlagSet.a(a.f()[0] as ArrayList, this.bg());
						}
						if (this.j == null)
						{
							this.j = new MessageFlagSet();
							this.b(a, false);
						}
					}
				}
				if (a.l() == "CAPABILITY")
				{
					this.h = global::a.f.i.a(a.f(), this.bg());
					if (this.h == null)
					{
						this.b(a, false);
					}
					this.b(this.k.r());
				}
				else if (a.n() || a.l() == "FLAGS")
				{
					if (c != null && c.nn() && !this.b.bf())
					{
						if (a.n())
						{
							this.a(a.l(), a.e(), a.r(), null);
						}
						else
						{
							this.a(a.l(), a.e(), null, a.r());
						}
					}
				}
				else if (a.l() == "EXISTS" || a.l() == "RECENT" || a.l() == "EXPUNGE" || a.l() == "FETCH")
				{
					MessageFlagSet messageFlagSet = null;
					if (a.l() == "FETCH")
					{
						if (a.i())
						{
							if (this.p == null && this.q == null)
							{
								if (c == null || !c.np() || this.b.bf())
								{
									goto IL_57B;
								}
								a.b(global::a.f.s.a(a.f(), this.bg()));
							}
							else if (a.d() == null && !a.b())
							{
								a.b(global::a.f.s.a(a.f(), this.bg()));
							}
							if (a.b())
							{
								goto IL_57B;
							}
							global::a.f.t.g g = global::a.f.t.g.a(this.p, this.q, a, this.r, this.s);
							if (g != null && this.a(a, g))
							{
								goto IL_57B;
							}
						}
						if (c != null && c.np() && !this.b.bf())
						{
							messageFlagSet = (MessageFlagSet)a.d()["FLAGS"];
							if (messageFlagSet == null)
							{
								goto IL_57B;
							}
						}
					}
					if (c != null && c.np() && !this.b.bf())
					{
						this.a(a.l(), a.h(), messageFlagSet);
					}
				}
				IL_57B:;
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000E547 File Offset: 0x0000D547
		protected override void oz()
		{
			base.oz();
			this.c(this.l || this.m);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000E566 File Offset: 0x0000D566
		public override bool o0(string A_0, bool A_1)
		{
			return base.c(A_0, this.p(), A_1);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000E578 File Offset: 0x0000D578
		public new bool b(string[] A_0, bf[] A_1, bool A_2)
		{
			base.a5().d().cf();
			bool flag = base.a5().d().b;
			base.a5().d().b = true;
			try
			{
				for (int i = 0; i < A_0.Length; i++)
				{
					base.a5().d().g(A_0[i], A_1[i], 0);
				}
				base.a5().d().o(0);
			}
			finally
			{
				base.a5().d().b = flag;
			}
			this.oz();
			switch (this.a.d().m())
			{
			case global::a.af.a:
				return true;
			case global::a.af.b:
				return true;
			case global::a.af.c:
				if (A_2)
				{
					base.a5().d().s();
				}
				return false;
			case global::a.af.d:
				throw new MailBeeAbortedByRemoteHostException(55, base.a1());
			case global::a.af.e:
				base.a5().d().s();
				return false;
			default:
				return false;
			}
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000E680 File Offset: 0x0000D680
		public new bool a(string[] A_0, bf[] A_1, bool A_2)
		{
			string[] array = new string[A_0.Length];
			for (int i = 0; i < A_0.Length; i++)
			{
				array[i] = this.o2(A_0[i], A_1[i]);
			}
			return this.b(array, A_1, A_2);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000E6BC File Offset: 0x0000D6BC
		public override bool o1(string A_0, bool A_1)
		{
			return base.b(A_0, this.p(), A_1);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000E6CC File Offset: 0x0000D6CC
		public new virtual bool a(string[] A_0, bool A_1)
		{
			bf[] array = new bf[A_0.Length];
			for (int i = 0; i < A_0.Length; i++)
			{
				array[i] = this.p();
			}
			return this.a(A_0, array, A_1);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000E704 File Offset: 0x0000D704
		protected internal override string o2(string A_0, bf A_1)
		{
			if (((global::a.f.v)A_1).e)
			{
				return base.o2(A_0, A_1);
			}
			this.c++;
			this.d = "MBN" + this.c.ToString("########00000000");
			((global::a.f.v)A_1).f = this.d;
			return this.d + " " + A_0 + "\r\n";
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000E77C File Offset: 0x0000D77C
		public void t()
		{
			base.aw();
			if (!this.m)
			{
				throw new MailBeeInvalidStateException(600);
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000E798 File Offset: 0x0000D798
		private new global::a.f.a a(global::a.f.m A_0, string A_1, string A_2, bool A_3)
		{
			for (int i = 0; i < base.a5().d().p().Count; i++)
			{
				global::a.f.a a = (global::a.f.a)base.a5().d().p().a(i);
				if (a.m() == A_0)
				{
					if (A_0 == global::a.f.m.b)
					{
						if (a.g() == A_1)
						{
							return a;
						}
					}
					else if (a.l() == A_2)
					{
						return a;
					}
				}
			}
			if (A_3)
			{
				throw new MailBeeImapResponseNotFoundException(610, base.a1());
			}
			return null;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000E824 File Offset: 0x0000D824
		protected void af()
		{
			this.g = AuthenticationMethods.Regular;
			if (base.t("LOGINDISABLED") != null)
			{
				this.g = AuthenticationMethods.None;
			}
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000E841 File Offset: 0x0000D841
		public bool z()
		{
			return this.m;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000E849 File Offset: 0x0000D849
		public bool ad()
		{
			return this.n;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0000E851 File Offset: 0x0000D851
		public new int l()
		{
			return this.e;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0000E859 File Offset: 0x0000D859
		public int ac()
		{
			return this.f;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0000E861 File Offset: 0x0000D861
		public int u()
		{
			return this.g;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000E869 File Offset: 0x0000D869
		public long n()
		{
			return this.h;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0000E871 File Offset: 0x0000D871
		public long w()
		{
			return this.i;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000E879 File Offset: 0x0000D879
		public new MessageFlagSet j()
		{
			return this.j;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000E881 File Offset: 0x0000D881
		public new MessageFlagSet k()
		{
			return this.k;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0000E889 File Offset: 0x0000D889
		public new bool m()
		{
			return this.w;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000E891 File Offset: 0x0000D891
		public new void h(bool A_0)
		{
			this.w = A_0;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0000E89A File Offset: 0x0000D89A
		public bool ab()
		{
			return this.x;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000E8A2 File Offset: 0x0000D8A2
		public new void d(bool A_0)
		{
			if (base.ao() && base.t("XLIST") == null)
			{
				this.x = false;
				return;
			}
			this.x = A_0;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000E8C8 File Offset: 0x0000D8C8
		public bool ae()
		{
			return this.v;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000E8D0 File Offset: 0x0000D8D0
		public new void e(bool A_0)
		{
			if (!A_0)
			{
				this.u = null;
			}
			this.v = A_0;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000E8E3 File Offset: 0x0000D8E3
		public EnvelopeCollection v()
		{
			if (this.v)
			{
				return this.u;
			}
			return null;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000E8F8 File Offset: 0x0000D8F8
		public new string[] i(string A_0)
		{
			if (A_0 == string.Empty)
			{
				A_0 = null;
			}
			if (A_0 == null)
			{
				string[] array = new string[base.a5().d().p().Count];
				for (int i = 0; i < base.a5().d().p().Count; i++)
				{
					array[i] = base.a5().d().p().a(i).ToString();
				}
				return array;
			}
			A_0 = A_0.ToUpper();
			ArrayList arrayList = new ArrayList();
			for (int j = 0; j < base.a5().d().p().Count; j++)
			{
				global::a.f.a a = (global::a.f.a)base.a5().d().p().a(j);
				if (a.l() == A_0)
				{
					arrayList.Add(a.ToString());
				}
			}
			return (string[])arrayList.ToArray(typeof(string));
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000E9F0 File Offset: 0x0000D9F0
		protected override bool fm(string A_0, ref int A_1, SslStartupMode A_2, ref bool A_3)
		{
			if (base.fm(A_0, ref A_1, A_2, ref A_3))
			{
				A_0 = A_0.ToLower();
				if (A_0.Equals("imap.gmail.com") || A_0.Equals("imap.mail.yahoo.com") || A_0.Equals("imap-mail.outlook.com"))
				{
					A_1 = 993;
					A_3 = true;
					return true;
				}
				if (A_1 == 993)
				{
					A_3 = true;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000EA56 File Offset: 0x0000DA56
		protected override bool fn(string A_0, int A_1, SslStartupMode A_2, ref bool A_3)
		{
			if (base.fn(A_0, A_1, A_2, ref A_3))
			{
				A_0 = A_0.ToLower();
				if ((A_0.EndsWith(".office365.com") || A_0.EndsWith(".outlook.com")) && A_1 == 143)
				{
					A_3 = true;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0000EA98 File Offset: 0x0000DA98
		private new void a(SaslMethod A_0)
		{
			global::a.f.a a = this.a(global::a.f.m.c, null, "CAPABILITY", true);
			this.h = global::a.f.i.a(a.f(), this.bg());
			if (this.h == null)
			{
				this.b(a, false);
			}
			this.af();
			this.g |= global::a.f.i.a(this.h, A_0);
			this.x = (base.t("SPECIAL-USE") == null && base.t("XLIST") != null);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000EB1E File Offset: 0x0000DB1E
		public new void c(SaslMethod A_0)
		{
			this.d.b(string.Format(Resources.Instance.Log_ImapGetCapabilitiesViaCapability, new object[0]), null, LogMessageType.Info, this);
			this.o1("CAPABILITY", true);
			this.a(A_0);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000EB57 File Offset: 0x0000DB57
		public void aa()
		{
			this.c(this.k.r());
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000EB6A File Offset: 0x0000DB6A
		private new void e()
		{
			if (this.h == null)
			{
				this.aa();
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000EB7C File Offset: 0x0000DB7C
		public override void fo()
		{
			bool flag = this.k.ac() == SslStartupMode.UseStartTls || this.k.ac() == SslStartupMode.UseStartTlsIfSupported;
			this.fn(this.k.v(), this.k.w(), this.k.ac(), ref flag);
			if (flag && !this.d)
			{
				this.fp(this.k.ac() == SslStartupMode.UseStartTls);
			}
			base.fo();
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000EBF8 File Offset: 0x0000DBF8
		public override void fp(bool A_0)
		{
			if (base.t(this.j.hj()) != null)
			{
				base.fp(true);
				if (this.h == null)
				{
					this.aa();
				}
				return;
			}
			if (A_0)
			{
				throw new MailBeeProtocolExtensionNotSupportedException(130, base.a1());
			}
			this.d.b(string.Format(Resources.Instance.ErrorDesc_StartTlsNotAvailable, new object[0]), null, LogMessageType.Info, this);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000EC65 File Offset: 0x0000DC65
		private new void e(string A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000EC72 File Offset: 0x0000DC72
		private new void d(string A_0)
		{
			if (A_0 == null || A_0 == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000EC8C File Offset: 0x0000DC8C
		public new void h(string A_0)
		{
			this.d.b(string.Format(Resources.Instance.Log_ImapManageFolder0, A_0), null, LogMessageType.Info, this);
			this.e(A_0);
			this.o1("CREATE \"" + global::a.f.b.a(A_0, this.w) + "\"", true);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000ECE4 File Offset: 0x0000DCE4
		public new void j(string A_0)
		{
			this.d.b(string.Format(Resources.Instance.Log_ImapManageFolder0, A_0), null, LogMessageType.Info, this);
			this.e(A_0);
			this.o1("DELETE \"" + global::a.f.b.a(A_0, this.w) + "\"", true);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000ED3C File Offset: 0x0000DD3C
		public new void a(string A_0, string A_1)
		{
			this.d.b(string.Format(Resources.Instance.Log_ImapRenameFolder0To1, A_0, A_1), null, LogMessageType.Info, this);
			this.e(A_0);
			this.e(A_1);
			this.o1(string.Concat(new string[]
			{
				"RENAME \"",
				global::a.f.b.a(A_0, this.w),
				"\" \"",
				global::a.f.b.a(A_1, this.w),
				"\""
			}), true);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000EDC0 File Offset: 0x0000DDC0
		public new void m(string A_0)
		{
			this.d.b(string.Format(Resources.Instance.Log_ImapManageFolder0, A_0), null, LogMessageType.Info, this);
			this.e(A_0);
			this.o1("SUBSCRIBE \"" + global::a.f.b.a(A_0, this.w) + "\"", true);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000EE18 File Offset: 0x0000DE18
		public new void g(string A_0)
		{
			this.d.b(string.Format(Resources.Instance.Log_ImapManageFolder0, A_0), null, LogMessageType.Info, this);
			this.e(A_0);
			this.o1("UNSUBSCRIBE \"" + global::a.f.b.a(A_0, this.w) + "\"", true);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0000EE70 File Offset: 0x0000DE70
		public new void f(string A_0, bool A_1)
		{
			this.d.b(string.Format(Resources.Instance.Log_ImapSelectFolder0, A_0), null, LogMessageType.Info, this);
			this.e(A_0);
			string str = A_1 ? "EXAMINE" : "SELECT";
			this.l = true;
			this.f();
			try
			{
				this.o1(str + " \"" + global::a.f.b.a(A_0, this.w) + "\"", true);
				this.m = true;
			}
			finally
			{
				this.l = false;
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000EF04 File Offset: 0x0000DF04
		public new void f(bool A_0)
		{
			this.d.b(string.Format(Resources.Instance.Log_ImapCloseFolder, new object[0]), null, LogMessageType.Info, this);
			this.f();
			if (A_0)
			{
				this.o1("CLOSE", true);
				return;
			}
			if (base.t("UNSELECT") == null)
			{
				this.o1("SELECT", false);
				return;
			}
			this.o1("UNSELECT", true);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0000EF74 File Offset: 0x0000DF74
		private new void c(string A_0)
		{
			string a_ = ((UidCollection)this.b(true, "DELETED NOT UID " + A_0, null, null)).ToString();
			if (a_ != string.Empty)
			{
				this.c(a_, true, global::a.f.b.a(SystemMessageFlags.Deleted), MessageFlagAction.Remove, true);
			}
			this.d(null, false);
			if (a_ != string.Empty)
			{
				this.c(a_, true, global::a.f.b.a(SystemMessageFlags.Deleted), MessageFlagAction.Add, true);
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000EFE4 File Offset: 0x0000DFE4
		public new void d(string A_0, bool A_1)
		{
			if (A_0 == null)
			{
				this.d.b(string.Format(Resources.Instance.Log_ImapExpunge, new object[0]), null, LogMessageType.Info, this);
			}
			else
			{
				this.d.b(string.Format(Resources.Instance.Log_ImapExpunge0, A_0), null, LogMessageType.Info, this);
			}
			if (A_0 == null)
			{
				this.o1("EXPUNGE", true);
				return;
			}
			if (base.t("UIDPLUS") != null)
			{
				this.o1("UID EXPUNGE " + A_0, true);
				return;
			}
			if (A_1)
			{
				throw new MailBeeProtocolExtensionNotSupportedException(620, base.a1());
			}
			this.c(A_0);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0000F084 File Offset: 0x0000E084
		public FolderStatus n(string A_0)
		{
			this.d.b(string.Format(Resources.Instance.Log_ImapFolderStatus0, A_0), null, LogMessageType.Info, this);
			this.e(A_0);
			this.o1("STATUS \"" + global::a.f.b.a(A_0, this.w) + "\" (MESSAGES RECENT UNSEEN UIDNEXT UIDVALIDITY)", true);
			global::a.f.a a = this.a(global::a.f.m.c, null, "STATUS", true);
			FolderStatus folderStatus = FolderStatus.a(a.f(), this.bg());
			if (folderStatus == null)
			{
				throw new MailBeeInvalidTextResponseException(121, base.a1(), a.ToString(), this.bg());
			}
			return folderStatus;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000F118 File Offset: 0x0000E118
		private new FolderQuota d()
		{
			global::a.f.a a = this.a(global::a.f.m.c, null, "QUOTAROOT", true);
			ArrayList arrayList = a.f();
			string text = null;
			if (FolderQuota.a(arrayList))
			{
				text = FolderQuota.a(arrayList, this.bg());
			}
			if (arrayList == null || (arrayList != null && text == null))
			{
				throw new MailBeeInvalidTextResponseException(121, base.a1(), a.ToString(), this.bg());
			}
			if (text != null)
			{
				for (int i = 0; i < base.a5().d().p().Count; i++)
				{
					a = (global::a.f.a)base.a5().d().p().a(i);
					if (a.l() == "QUOTA")
					{
						this.d.b(string.Format(Resources.Instance.Log_ImapGettingQuotaFromList, new object[0]), null, LogMessageType.Info, this);
						FolderQuota folderQuota = FolderQuota.b(a.f(), this.bg());
						if (folderQuota == null)
						{
							throw new MailBeeInvalidTextResponseException(121, base.a1(), a.ToString(), this.bg());
						}
						if (folderQuota.QuotaName == text)
						{
							this.d.b(string.Format(Resources.Instance.Log_ImapMatchingQuotaFound, new object[0]), null, LogMessageType.Info, this);
							return folderQuota;
						}
					}
				}
				throw new MailBeeInvalidTextResponseException(124, base.a1(), a.ToString(), this.bg());
			}
			return new FolderQuota(null, -1L, -1L, -1, -1);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000F284 File Offset: 0x0000E284
		public new FolderQuota l(string A_0)
		{
			this.d.b(string.Format(Resources.Instance.Log_ImapFolderQuota0, A_0), null, LogMessageType.Info, this);
			if (A_0 == null)
			{
				A_0 = string.Empty;
			}
			if (base.t("QUOTA") == null)
			{
				throw new MailBeeProtocolExtensionNotSupportedException(621, base.a1());
			}
			this.o1("GETQUOTAROOT \"" + global::a.f.b.a(A_0, this.w) + "\"", true);
			return this.d();
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000F300 File Offset: 0x0000E300
		private new FolderCollection b(string A_0)
		{
			FolderCollection folderCollection = new FolderCollection();
			for (int i = 0; i < base.a5().d().p().Count; i++)
			{
				global::a.f.a a = (global::a.f.a)base.a5().d().p().a(i);
				if (a.l() == A_0 || (this.x && a.l() == "LIST"))
				{
					Folder folder = Folder.b(a.f(), this.bg());
					if (folder == null)
					{
						folder = new Folder();
						this.b(a, false);
					}
					else if ((folder.Flags & FolderFlags.Inbox) > FolderFlags.None && folder.RawName.ToUpper() != "INBOX")
					{
						folder.RawNameInternal = "INBOX";
					}
					folderCollection.a(folder);
				}
			}
			return folderCollection;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000F3D8 File Offset: 0x0000E3D8
		public new FolderCollection b(bool A_0, string A_1, string A_2)
		{
			this.d.b(string.Format(Resources.Instance.Log_ImapWillDownloadFoldersOf0Matching1, A_1, A_2), null, LogMessageType.Info, this);
			if (A_1 == null)
			{
				A_1 = string.Empty;
			}
			if (A_2 == null)
			{
				A_2 = "*";
			}
			string text = A_0 ? "LSUB" : (this.x ? "XLIST" : "LIST");
			this.o1(string.Concat(new string[]
			{
				text,
				" \"",
				global::a.f.b.a(A_1, this.w),
				"\" \"",
				global::a.f.b.a(A_2, this.w),
				"\""
			}), true);
			FolderCollection result = this.b(text);
			this.d.b(string.Format(Resources.Instance.Log_ImapDownloadFoldersDone, new object[0]), null, LogMessageType.Info, this);
			return result;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000F4B0 File Offset: 0x0000E4B0
		private new string a(bool A_0, bool A_1, string A_2, string A_3, string A_4)
		{
			string str;
			if (A_0)
			{
				str = (A_1 ? "UID SORT" : "SORT");
				str = str + " (" + A_4 + ") ";
			}
			else
			{
				str = (A_1 ? "UID SEARCH" : "SEARCH");
				str += " ";
			}
			if (A_0)
			{
				if (A_3 != null && A_3 != string.Empty)
				{
					str = str + A_3 + " ";
				}
				else
				{
					str += "US-ASCII ";
				}
			}
			else if (A_3 != null && A_3 != string.Empty)
			{
				str = str + "CHARSET " + A_3 + " ";
			}
			if (A_2 == null || A_2 == string.Empty)
			{
				A_2 = "ALL";
			}
			return str + A_2;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0000F57C File Offset: 0x0000E57C
		private new MessageIndexCollection a(bool A_0, bool A_1)
		{
			MessageIndexCollection messageIndexCollection = null;
			for (int i = 0; i < base.a5().d().p().Count; i++)
			{
				global::a.f.a a = (global::a.f.a)base.a5().d().p().a(i);
				if (a.l() == (A_0 ? "SORT" : "SEARCH"))
				{
					ArrayList arrayList = a.f();
					if (arrayList == null)
					{
						arrayList = new ArrayList();
					}
					if (messageIndexCollection == null)
					{
						messageIndexCollection = (A_1 ? new UidCollection(arrayList.Count) : new MessageNumberCollection(arrayList.Count));
					}
					for (int j = 0; j < arrayList.Count; j++)
					{
						string text = null;
						try
						{
							text = ((ao)arrayList[j]).a(this.bg());
						}
						catch
						{
							throw new MailBeeInvalidTextResponseException(121, base.a1(), a.ToString(), this.bg());
						}
						try
						{
							messageIndexCollection.AddIndex(text);
						}
						catch
						{
							throw new MailBeeInvalidTextResponseItemException(125, base.a1(), text, this.bg());
						}
					}
				}
			}
			if (messageIndexCollection == null)
			{
				messageIndexCollection = (A_1 ? new UidCollection(0) : new MessageNumberCollection(0));
			}
			return messageIndexCollection;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000F6C0 File Offset: 0x0000E6C0
		public new MessageIndexCollection b(bool A_0, string A_1, string A_2, string A_3)
		{
			bool flag = A_3 != null;
			if (flag)
			{
				this.d.b(string.Format(Resources.Instance.Log_ImapWillSort, new object[0]), null, LogMessageType.Info, this);
				if (base.t("SORT") == null)
				{
					throw new MailBeeProtocolExtensionNotSupportedException(623, base.a1());
				}
			}
			else
			{
				this.d.b(string.Format(Resources.Instance.Log_ImapWillSearch, new object[0]), null, LogMessageType.Info, this);
			}
			this.o1(this.a(flag, A_0, A_1, A_2, A_3), true);
			MessageIndexCollection result = this.a(flag, A_0);
			if (flag)
			{
				this.d.b(string.Format(Resources.Instance.Log_ImapSortDone, new object[0]), null, LogMessageType.Info, this);
			}
			else
			{
				this.d.b(string.Format(Resources.Instance.Log_ImapSearchDone, new object[0]), null, LogMessageType.Info, this);
			}
			return result;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000F7A4 File Offset: 0x0000E7A4
		private new bool a(global::a.f.a A_0, global::a.f.t.g A_1)
		{
			return A_0.d().ContainsKey("UID") && (A_0.d().Keys.Count != 2 || !A_0.d().ContainsKey("FLAGS") || A_1.e() <= EnvelopeParts.Flags);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0000F7F8 File Offset: 0x0000E7F8
		private new Envelope a(global::a.f.a A_0)
		{
			Envelope envelope = null;
			if (A_0.l() == "FETCH")
			{
				if (A_0.i())
				{
					if (A_0.d() == null)
					{
						A_0.b(global::a.f.s.a(A_0.f(), this.bg()));
					}
					global::a.f.t.g g = global::a.f.t.g.a(this.p, this.q, A_0, this.r, this.s);
					if (g != null && this.a(A_0, g))
					{
						envelope = global::a.f.q.a(A_0.d(), A_0.h(), g.e(), g.d(), g.b(), g.a(), g.c(), this.bg());
					}
				}
				else
				{
					envelope = new Envelope();
					envelope.a(A_0.h());
				}
			}
			return envelope;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0000F8C0 File Offset: 0x0000E8C0
		public new EnvelopeCollection b(string A_0, bool A_1, EnvelopeParts A_2, int A_3, string[] A_4, string[] A_5, bool A_6, bool A_7)
		{
			if (this.v)
			{
				this.u = null;
			}
			this.t = false;
			this.d.b(string.Format(Resources.Instance.Log_ImapWillDownloadEnvelopes, new object[0]), null, LogMessageType.Info, this);
			this.d(A_0);
			string text = A_1 ? "UID FETCH" : "FETCH";
			string a_;
			string a_2;
			string a_3;
			StringBuilder stringBuilder = new StringBuilder(global::a.f.b.a(A_2, A_3, A_4, A_5, out a_, out a_2, out a_3));
			bool flag = false;
			bool flag2 = false;
			global::a.f.d d = (global::a.f.d)this.a.d();
			if (A_6)
			{
				if (this.b != null && ((global::a.f.c)this.b).nl())
				{
					flag = true;
					global::a.f.d d2 = d;
					d2.g((global::a.f.e)Delegate.Combine(d2.i(), new global::a.f.e(this.a)));
				}
				flag2 = true;
				global::a.f.d d3 = d;
				d3.g((ay)Delegate.Combine(d3.k(), new ay(this.b)));
			}
			this.u = new EnvelopeCollection();
			EnvelopeCollection envelopeCollection = null;
			bool flag3 = false;
			this.p = new global::a.f.t.g(A_2, a_, a_2, a_3, A_5, !A_7);
			try
			{
				this.o1(string.Concat(new string[]
				{
					text,
					" ",
					A_0,
					" (",
					stringBuilder.ToString(),
					")"
				}), true);
				this.a.d().n(this.a.d().a * 8);
				envelopeCollection = this.u;
				flag3 = true;
			}
			finally
			{
				if (!flag3)
				{
					this.p = null;
				}
				if (flag2)
				{
					global::a.f.d d4 = d;
					d4.g((ay)Delegate.Remove(d4.k(), new ay(this.b)));
				}
				if (flag)
				{
					global::a.f.d d5 = d;
					d5.g((global::a.f.e)Delegate.Remove(d5.i(), new global::a.f.e(this.a)));
				}
				if (!this.v)
				{
					this.u = null;
				}
			}
			if (!flag2)
			{
				for (int i = 0; i < base.a5().d().p().Count; i++)
				{
					global::a.f.a a_4 = (global::a.f.a)base.a5().d().p().a(i);
					Envelope envelope = this.a(a_4);
					if (envelope != null)
					{
						envelopeCollection.Add(envelope);
					}
				}
			}
			this.p = null;
			for (int j = 0; j < envelopeCollection.Count; j++)
			{
				if (!envelopeCollection[j].IsValid)
				{
					MailBeeImapInvalidEnvelopeException ex = new MailBeeImapInvalidEnvelopeException(613, base.a1(), envelopeCollection[j]);
					if (A_7)
					{
						throw ex;
					}
					if (!flag2)
					{
						base.c(ex);
					}
				}
			}
			this.d.b(string.Format(Resources.Instance.Log_ImapDownloadEnvelopesDone, new object[0]), null, LogMessageType.Info, this);
			return envelopeCollection;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000FBA4 File Offset: 0x0000EBA4
		public new EnvelopeCollection b(long[] A_0, bool A_1, EnvelopeParts[] A_2, int[] A_3, string[][] A_4, string[][] A_5, bool A_6, bool A_7)
		{
			if (this.v)
			{
				this.u = null;
			}
			this.t = false;
			this.d.b(string.Format(Resources.Instance.Log_ImapWillDownloadEnvelopes, new object[0]), null, LogMessageType.Info, this);
			if (A_0 == null || A_0.Length == 0)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			if ((A_2 != null && A_2.Length != A_0.Length) || (A_3 != null && A_3.Length != A_0.Length) || (A_4 != null && A_4.Length != A_0.Length) || (A_5 != null && A_5.Length != A_0.Length))
			{
				throw new MailBeeInvalidArgumentException(20);
			}
			string text = A_1 ? "UID FETCH" : "FETCH";
			bool flag = false;
			bool flag2 = false;
			global::a.f.d d = (global::a.f.d)this.a.d();
			if (A_6)
			{
				if (this.b != null && ((global::a.f.c)this.b).nl())
				{
					flag = true;
					global::a.f.d d2 = d;
					d2.g((global::a.f.e)Delegate.Combine(d2.i(), new global::a.f.e(this.a)));
				}
				flag2 = true;
				global::a.f.d d3 = d;
				d3.g((ay)Delegate.Combine(d3.k(), new ay(this.b)));
			}
			this.u = new EnvelopeCollection();
			EnvelopeCollection envelopeCollection = null;
			bool flag3 = false;
			this.r = A_0;
			this.s = A_1;
			this.q = new global::a.f.t.g[A_0.Length];
			string[] array = new string[A_0.Length];
			for (int i = 0; i < A_0.Length; i++)
			{
				EnvelopeParts a_ = (A_2 == null) ? EnvelopeParts.Uid : A_2[i];
				int a_2 = (A_3 == null) ? 0 : A_3[i];
				string[] a_3 = (A_4 == null) ? null : A_4[i];
				string[] array2 = (A_5 == null) ? null : A_5[i];
				string a_4;
				string a_5;
				string a_6;
				StringBuilder stringBuilder = new StringBuilder(global::a.f.b.a(a_, a_2, a_3, array2, out a_4, out a_5, out a_6));
				this.q[i] = new global::a.f.t.g(a_, a_4, a_5, a_6, array2, !A_7);
				array[i] = string.Concat(new string[]
				{
					text,
					" ",
					A_0[i].ToString(),
					" (",
					stringBuilder.ToString(),
					")"
				});
			}
			try
			{
				this.a(array, true);
				this.a.d().n(this.a.d().a * 8);
				envelopeCollection = this.u;
				flag3 = true;
			}
			finally
			{
				if (!flag3)
				{
					this.q = null;
					this.r = null;
				}
				if (flag2)
				{
					global::a.f.d d4 = d;
					d4.g((ay)Delegate.Remove(d4.k(), new ay(this.b)));
				}
				if (flag)
				{
					global::a.f.d d5 = d;
					d5.g((global::a.f.e)Delegate.Remove(d5.i(), new global::a.f.e(this.a)));
				}
				if (!this.v)
				{
					this.u = null;
				}
			}
			if (!flag2)
			{
				for (int j = 0; j < base.a5().d().p().Count; j++)
				{
					global::a.f.a a_7 = (global::a.f.a)base.a5().d().p().a(j);
					Envelope envelope = this.a(a_7);
					if (envelope != null)
					{
						envelopeCollection.Add(envelope);
					}
				}
			}
			this.q = null;
			this.r = null;
			for (int k = 0; k < envelopeCollection.Count; k++)
			{
				if (!envelopeCollection[k].IsValid)
				{
					MailBeeImapInvalidEnvelopeException ex = new MailBeeImapInvalidEnvelopeException(613, base.a1(), envelopeCollection[k]);
					if (A_7)
					{
						throw ex;
					}
					if (!flag2)
					{
						base.c(ex);
					}
				}
			}
			this.d.b(string.Format(Resources.Instance.Log_ImapDownloadEnvelopesDone, new object[0]), null, LogMessageType.Info, this);
			return envelopeCollection;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000FF58 File Offset: 0x0000EF58
		public new MailMessage b(long A_0, bool A_1, int A_2, bool A_3)
		{
			if (A_0 < 0L && !A_1)
			{
				A_0 = (long)(this.e + 1) + A_0;
			}
			EnvelopeCollection envelopeCollection = this.b(A_0.ToString(), A_1, EnvelopeParts.Rfc822Size | EnvelopeParts.MessagePreview, A_2, null, null, A_3, true);
			if (envelopeCollection.Count > 0)
			{
				return envelopeCollection[0].MessagePreview;
			}
			if (this.t)
			{
				return null;
			}
			throw new MailBeeImapMessageIndexNotFoundException(611, base.a1(), A_0, A_1);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000FFC4 File Offset: 0x0000EFC4
		public new MailMessageCollection b(string A_0, bool A_1, int A_2, bool A_3)
		{
			EnvelopeCollection envelopeCollection = this.b(A_0, A_1, EnvelopeParts.Rfc822Size | EnvelopeParts.MessagePreview, A_2, null, null, A_3, true);
			MailMessageCollection mailMessageCollection = new MailMessageCollection();
			for (int i = 0; i < envelopeCollection.Count; i++)
			{
				mailMessageCollection.Add(envelopeCollection[i].MessagePreview);
			}
			return mailMessageCollection;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0001000C File Offset: 0x0000F00C
		public long s()
		{
			EnvelopeCollection envelopeCollection = this.b("1:*", false, EnvelopeParts.Rfc822Size, 0, null, null, false, false);
			long num = 0L;
			for (int i = 0; i < envelopeCollection.Count; i++)
			{
				int size = envelopeCollection[i].Size;
				if (size > -1)
				{
					num += (long)size;
				}
			}
			return num;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00010058 File Offset: 0x0000F058
		public new ImapNamespaceCollectionSet h()
		{
			this.d.b(string.Format(Resources.Instance.Log_ImapNamespace, new object[0]), null, LogMessageType.Info, this);
			if (base.t("IDLE") == null)
			{
				throw new MailBeeProtocolExtensionNotSupportedException(624, base.a1());
			}
			this.o1("NAMESPACE", true);
			global::a.f.a a = this.a(global::a.f.m.c, null, "NAMESPACE", true);
			ImapNamespaceCollectionSet imapNamespaceCollectionSet = ImapNamespaceCollectionSet.a(a.f(), this.bg());
			if (imapNamespaceCollectionSet == null)
			{
				throw new MailBeeInvalidTextResponseException(121, base.a1(), a.ToString(), this.bg());
			}
			return imapNamespaceCollectionSet;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x000100F2 File Offset: 0x0000F0F2
		public new void g(string A_0, bool A_1)
		{
			this.c(A_0, A_1, global::a.f.b.a(SystemMessageFlags.Deleted), MessageFlagAction.Add, true);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00010104 File Offset: 0x0000F104
		private new string a(string A_0, bool A_1, string A_2, MessageFlagAction A_3, bool A_4)
		{
			string text = A_1 ? "UID STORE" : "STORE";
			string text2 = string.Empty;
			switch (A_3)
			{
			case MessageFlagAction.Replace:
				text2 += "FLAGS";
				break;
			case MessageFlagAction.Add:
				text2 = "+FLAGS";
				break;
			case MessageFlagAction.Remove:
				text2 = "-FLAGS";
				break;
			case MessageFlagAction.ReplaceGmailLabel:
				text2 += "X-GM-LABELS";
				break;
			case MessageFlagAction.AddGmailLabel:
				text2 = "+X-GM-LABELS";
				break;
			case MessageFlagAction.RemoveGmailLabel:
				text2 = "-X-GM-LABELS";
				break;
			}
			if (A_4)
			{
				text2 += ".SILENT";
			}
			if (A_2 == null)
			{
				A_2 = string.Empty;
			}
			return string.Concat(new string[]
			{
				text,
				" ",
				A_0,
				" ",
				text2,
				" (",
				A_2,
				")"
			});
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x000101D8 File Offset: 0x0000F1D8
		public new void c(string A_0, bool A_1, string A_2, MessageFlagAction A_3, bool A_4)
		{
			this.d.b(string.Format(Resources.Instance.Log_ImapSetMessageFlags, new object[0]), null, LogMessageType.Info, this);
			this.d(A_0);
			this.o1(this.a(A_0, A_1, A_2, A_3, A_4), true);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00010224 File Offset: 0x0000F224
		private new string a(string A_0, string A_1, string A_2, int A_3, bool A_4)
		{
			if (A_2 == string.Empty)
			{
				A_2 = "\"" + ImapUtils.GetImapDateTimeString(DateTime.Now, true, false) + "\" ";
			}
			else if (A_2 == null)
			{
				A_2 = string.Empty;
			}
			else
			{
				A_2 = "\"" + A_2 + "\" ";
			}
			string text = (A_1 == null && A_2 == string.Empty) ? string.Empty : ("(" + A_1 + ") ");
			return string.Concat(new object[]
			{
				"APPEND \"",
				global::a.f.b.a(A_0, this.w),
				"\" ",
				text,
				A_2,
				"{",
				A_3,
				A_4 ? "+}" : "}"
			});
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x000102FC File Offset: 0x0000F2FC
		private new void b(UidPlusResult A_0)
		{
			if (A_0 != null)
			{
				if (base.t("UIDPLUS") == null)
				{
					A_0.a(false);
					return;
				}
				global::a.f.a a = this.a(global::a.f.m.b, this.d, "OK", true);
				if (!this.b(a.k()["APPENDUID"] as ArrayList, A_0))
				{
					A_0.a(true);
				}
			}
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0001035C File Offset: 0x0000F35C
		public new void a(MailMessage A_0, string A_1, string A_2, string A_3, bool A_4, UidPlusResult A_5)
		{
			this.d.b(string.Format(Resources.Instance.Log_ImapWillUploadMessageTo0, A_1), null, LogMessageType.Info, this);
			this.e(A_1);
			ao ao = A_0.n();
			if (A_4 && base.t("LITERAL+") == null)
			{
				A_4 = false;
			}
			if (!A_4)
			{
				this.o1(this.a(A_1, A_2, A_3, ao.e(), false), true);
			}
			this.a.d().cf();
			if (A_4)
			{
				global::a.f.v a_ = new global::a.f.v(false, false, false, null);
				string a_2 = this.o2(this.a(A_1, A_2, A_3, ao.e(), true), a_);
				this.a.d().h(a_2, a_);
				this.a.d().u();
			}
			bool a_3 = true;
			bool a_4 = false;
			int i = ao.b();
			int num = 0;
			while (i < ao.b() + ao.e())
			{
				int a_5;
				if (i < ao.b() + ao.e() - Global.TcpBufSize)
				{
					a_5 = Global.TcpBufSize;
				}
				else
				{
					a_5 = ao.b() + ao.e() - i;
					a_4 = true;
				}
				this.a.d().h(ao.d(), i, a_5, new global::a.f.v(false, false, true, null, a_3, a_4));
				this.a.d().u();
				a_3 = false;
				i += Global.TcpBufSize;
				num++;
			}
			base.c("\r\n", new global::a.f.v(true, true, true, this.d), true);
			this.b(A_5);
			this.d.b(string.Format(Resources.Instance.Log_ImapUploadMessageDone, new object[0]), null, LogMessageType.Info, this);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00010508 File Offset: 0x0000F508
		private new bool b(ArrayList A_0, UidPlusResult A_1)
		{
			if (A_0 == null || A_0.Count < 2)
			{
				return false;
			}
			bool result;
			try
			{
				long a_ = long.Parse(((ao)A_0[0]).a(Encoding.ASCII));
				string a_2 = ((ao)A_0[1]).a(Encoding.ASCII);
				A_1.a(true, null, a_2, a_);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0001057C File Offset: 0x0000F57C
		private new string a(string A_0, bool A_1, string A_2, UidPlusResult A_3, bool A_4)
		{
			string text;
			if (A_4)
			{
				text = (A_1 ? "UID COPY" : "COPY");
			}
			else
			{
				text = (A_1 ? "UID MOVE" : "MOVE");
			}
			return string.Concat(new string[]
			{
				text,
				" ",
				A_0,
				" \"",
				global::a.f.b.a(A_2, this.w),
				"\""
			});
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x000105EC File Offset: 0x0000F5EC
		private new void a(UidPlusResult A_0)
		{
			if (A_0 != null)
			{
				if (base.t("UIDPLUS") == null)
				{
					A_0.a(false);
					return;
				}
				global::a.f.a a = this.a(global::a.f.m.b, this.d, "OK", true);
				if (!this.a(a.k()["COPYUID"] as ArrayList, A_0))
				{
					a = this.a(global::a.f.m.c, null, "OK", false);
					if (a == null || !this.a(a.k()["COPYUID"] as ArrayList, A_0))
					{
						A_0.a(true);
					}
				}
			}
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0001067C File Offset: 0x0000F67C
		public new void b(string A_0, bool A_1, string A_2, UidPlusResult A_3, bool A_4)
		{
			if (A_4)
			{
				this.d.b(string.Format(Resources.Instance.Log_ImapWillCopyMessagesTo0, A_2), null, LogMessageType.Info, this);
			}
			else
			{
				this.d.b(string.Format(Resources.Instance.Log_ImapWillMoveMessagesTo0, A_2), null, LogMessageType.Info, this);
			}
			this.e(A_2);
			this.d(A_0);
			this.o1(this.a(A_0, A_1, A_2, A_3, A_4), true);
			this.a(A_3);
			if (A_4)
			{
				this.d.b(string.Format(Resources.Instance.Log_ImapCopyMessagesDone, new object[0]), null, LogMessageType.Info, this);
				return;
			}
			this.d.b(string.Format(Resources.Instance.Log_ImapMoveMessagesDone, new object[0]), null, LogMessageType.Info, this);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00010744 File Offset: 0x0000F744
		private new bool a(ArrayList A_0, UidPlusResult A_1)
		{
			if (A_0 == null)
			{
				A_1.a(true, string.Empty, string.Empty, -1L);
				return false;
			}
			if (A_0.Count < 3)
			{
				return false;
			}
			bool result;
			try
			{
				long a_ = long.Parse(((ao)A_0[0]).a(Encoding.ASCII));
				string a_2 = ((ao)A_0[1]).a(Encoding.ASCII);
				string a_3 = ((ao)A_0[2]).a(Encoding.ASCII);
				A_1.a(true, a_2, a_3, a_);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x000107E4 File Offset: 0x0000F7E4
		private new void b(string A_0, bool A_1)
		{
			string a_ = ((UidCollection)this.b(true, "DELETED", null, null)).ToString();
			if (a_ != string.Empty)
			{
				this.c(a_, true, global::a.f.b.a(SystemMessageFlags.Deleted), MessageFlagAction.Remove, true);
			}
			this.g(A_0, A_1);
			this.d(null, false);
			if (a_ != string.Empty)
			{
				this.c(a_, true, global::a.f.b.a(SystemMessageFlags.Deleted), MessageFlagAction.Add, true);
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00010854 File Offset: 0x0000F854
		public new void b(string A_0, bool A_1, string A_2, UidPlusResult A_3)
		{
			if (A_3 == null)
			{
				A_3 = new UidPlusResult();
			}
			if (base.t("MOVE") != null)
			{
				this.b(A_0, A_1, A_2, A_3, false);
				return;
			}
			this.d.b(string.Format(Resources.Instance.Log_ImapWillMoveMessagesTo0, A_2), null, LogMessageType.Info, this);
			this.b(A_0, A_1, A_2, A_3, true);
			if (A_3.IsSupported && A_3.SrcUidString != null && A_3.SrcUidString != string.Empty)
			{
				this.g(A_3.SrcUidString, true);
				this.d(A_3.SrcUidString, true);
			}
			else if (A_0 == "1:*")
			{
				this.g("1:*", A_1);
				this.d(null, false);
			}
			else
			{
				this.b(A_0, A_1);
			}
			this.d.b(string.Format(Resources.Instance.Log_ImapMoveMessagesDone, new object[0]), null, LogMessageType.Info, this);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00010944 File Offset: 0x0000F944
		public void q()
		{
			this.d.b(string.Format(Resources.Instance.Log_ImapWillIdle, new object[0]), null, LogMessageType.Info, this);
			if (base.t("IDLE") == null)
			{
				throw new MailBeeProtocolExtensionNotSupportedException(622, base.a1());
			}
			this.o = false;
			this.o1("IDLE", true);
			this.n = true;
			bool flag = this.b != null && this.b.bq() && ((global::a.f.c)this.b).nr();
			while (!this.o)
			{
				if (this.a.d().hm(10000))
				{
					base.a5().d().cf();
					base.a5().d().ci();
					base.a5().d().m(0);
					base.a5().d().o(0);
					this.oz();
					base.a5().d().s();
				}
				else if (flag && !this.b.bf())
				{
					this.x();
				}
			}
			this.o = false;
			this.n = false;
			this.d.b(string.Format(Resources.Instance.Log_ImapWillFinishIdling, new object[0]), null, LogMessageType.Info, this);
			global::a.f.v a_ = new global::a.f.v(true, false, true, null);
			string a_2 = this.o2("DONE", a_);
			base.c(a_2, a_, true);
			this.d.b(string.Format(Resources.Instance.Log_ImapIdleDone, new object[0]), null, LogMessageType.Info, this);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00010AE2 File Offset: 0x0000FAE2
		public new void g()
		{
			this.o = true;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00010AEC File Offset: 0x0000FAEC
		private new void a(global::a.f.a A_0, byte[] A_1, int A_2, int A_3, int A_4, bc A_5)
		{
			global::a.f.c c = (global::a.f.c)this.b;
			global::a.f.o o = (global::a.f.o)this.b;
			if (c.nl() && !o.bf() && A_0.l() == "FETCH")
			{
				this.a(A_0.h(), A_4, A_3 + A_4 - A_2);
			}
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00010B48 File Offset: 0x0000FB48
		private new void b(at A_0, bc A_1)
		{
			global::a.f.c c = (global::a.f.c)this.b;
			global::a.f.o o = (global::a.f.o)this.b;
			global::a.f.a a = (global::a.f.a)A_0;
			Envelope envelope = this.a(a);
			if (envelope != null)
			{
				global::a.f.t.g g = global::a.f.t.g.a(this.p, this.q, a, this.r, this.s);
				if (!envelope.IsValid && g != null && g.f() && c.j() && !o.bf())
				{
					MailBeeImapInvalidEnvelopeException a_ = new MailBeeImapInvalidEnvelopeException(613, base.a1(), envelope);
					base.c(a_);
				}
				if (c.nj() && !o.bf())
				{
					ImapEnvelopeDownloadedEventArgs imapEnvelopeDownloadedEventArgs = new ImapEnvelopeDownloadedEventArgs(a.h(), a.q().Length, envelope, A_1);
					this.a(imapEnvelopeDownloadedEventArgs);
					envelope = imapEnvelopeDownloadedEventArgs.DownloadedEnvelope;
					if (envelope == null)
					{
						this.t = true;
					}
				}
			}
			if (envelope != null)
			{
				this.u.Add(envelope);
				a.ag();
			}
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00010C38 File Offset: 0x0000FC38
		protected override Task fq()
		{
			global::a.f.t.q q;
			q.c = this;
			q.b = AsyncTaskMethodBuilder.Create();
			q.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = q.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.q>(ref q);
			return q.b.Task;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00010C80 File Offset: 0x0000FC80
		private new Task a(global::a.f.a A_0, bool A_1)
		{
			string a_ = A_1 ? A_0.e() : A_0.ToString();
			return base.b(new MailBeeInvalidTextResponseException(121, base.a1(), a_, this.bg()));
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00010CBC File Offset: 0x0000FCBC
		private new Task b(bool A_0)
		{
			global::a.f.t.af af;
			af.c = this;
			af.e = A_0;
			af.b = AsyncTaskMethodBuilder.Create();
			af.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = af.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.af>(ref af);
			return af.b.Task;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00010D0C File Offset: 0x0000FD0C
		private new Task a(at A_0, bc A_1)
		{
			global::a.f.t.e e;
			e.c = this;
			e.d = A_0;
			e.i = A_1;
			e.b = AsyncTaskMethodBuilder.Create();
			e.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = e.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.e>(ref e);
			return e.b.Task;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00010D64 File Offset: 0x0000FD64
		private new Task<FolderQuota> c()
		{
			global::a.f.t.m m;
			m.c = this;
			m.b = AsyncTaskMethodBuilder<FolderQuota>.Create();
			m.a = -1;
			AsyncTaskMethodBuilder<FolderQuota> asyncTaskMethodBuilder = m.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.m>(ref m);
			return m.b.Task;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00010DA9 File Offset: 0x0000FDA9
		public override Task<bool> o3(string A_0, bool A_1)
		{
			return base.d(A_0, new global::a.f.v(true, true, false, null), A_1);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00010DBC File Offset: 0x0000FDBC
		public new Task<bool> d(string[] A_0, bf[] A_1, bool A_2)
		{
			global::a.f.t.p p;
			p.c = this;
			p.d = A_0;
			p.e = A_1;
			p.h = A_2;
			p.b = AsyncTaskMethodBuilder<bool>.Create();
			p.a = -1;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = p.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.p>(ref p);
			return p.b.Task;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00010E1C File Offset: 0x0000FE1C
		public new Task<bool> c(string[] A_0, bf[] A_1, bool A_2)
		{
			string[] array = new string[A_0.Length];
			for (int i = 0; i < A_0.Length; i++)
			{
				array[i] = this.o2(A_0[i], A_1[i]);
			}
			return this.d(array, A_1, A_2);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00010E58 File Offset: 0x0000FE58
		public override Task<bool> o4(string A_0, bool A_1)
		{
			return base.a(A_0, new global::a.f.v(true, true, false, null), A_1);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00010E6C File Offset: 0x0000FE6C
		public new virtual Task<bool> b(string[] A_0, bool A_1)
		{
			bf[] array = new bf[A_0.Length];
			for (int i = 0; i < A_0.Length; i++)
			{
				array[i] = new global::a.f.v(true, true, false, null);
			}
			return this.c(A_0, array, A_1);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00010EA8 File Offset: 0x0000FEA8
		public new Task d(SaslMethod A_0)
		{
			global::a.f.t.l l;
			l.c = this;
			l.d = A_0;
			l.b = AsyncTaskMethodBuilder.Create();
			l.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = l.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.l>(ref l);
			return l.b.Task;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00010EF5 File Offset: 0x0000FEF5
		public new Task i()
		{
			return this.d(this.k.r());
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00010F08 File Offset: 0x0000FF08
		private new Task b()
		{
			if (this.h == null)
			{
				return this.i();
			}
			return Task.FromResult<int>(0);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00010F20 File Offset: 0x0000FF20
		public override Task fr()
		{
			global::a.f.t.ai ai;
			ai.c = this;
			ai.b = AsyncTaskMethodBuilder.Create();
			ai.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = ai.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.ai>(ref ai);
			return ai.b.Task;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00010F68 File Offset: 0x0000FF68
		public override Task fs(bool A_0)
		{
			global::a.f.t.d d;
			d.c = this;
			d.d = A_0;
			d.b = AsyncTaskMethodBuilder.Create();
			d.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = d.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.d>(ref d);
			return d.b.Task;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00010FB8 File Offset: 0x0000FFB8
		public new Task f(string A_0)
		{
			global::a.f.t.ag ag;
			ag.c = this;
			ag.d = A_0;
			ag.b = AsyncTaskMethodBuilder.Create();
			ag.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = ag.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.ag>(ref ag);
			return ag.b.Task;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00011008 File Offset: 0x00010008
		public Task q(string A_0)
		{
			global::a.f.t.j j;
			j.c = this;
			j.d = A_0;
			j.b = AsyncTaskMethodBuilder.Create();
			j.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = j.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.j>(ref j);
			return j.b.Task;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00011058 File Offset: 0x00010058
		public new Task b(string A_0, string A_1)
		{
			global::a.f.t.aa aa;
			aa.c = this;
			aa.d = A_0;
			aa.e = A_1;
			aa.b = AsyncTaskMethodBuilder.Create();
			aa.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = aa.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.aa>(ref aa);
			return aa.b.Task;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x000110B0 File Offset: 0x000100B0
		public new Task k(string A_0)
		{
			global::a.f.t.al al;
			al.c = this;
			al.d = A_0;
			al.b = AsyncTaskMethodBuilder.Create();
			al.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = al.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.al>(ref al);
			return al.b.Task;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00011100 File Offset: 0x00010100
		public Task r(string A_0)
		{
			global::a.f.t.s s;
			s.c = this;
			s.d = A_0;
			s.b = AsyncTaskMethodBuilder.Create();
			s.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = s.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.s>(ref s);
			return s.b.Task;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00011150 File Offset: 0x00010150
		public new Task e(string A_0, bool A_1)
		{
			global::a.f.t.z z;
			z.c = this;
			z.d = A_0;
			z.e = A_1;
			z.b = AsyncTaskMethodBuilder.Create();
			z.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = z.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.z>(ref z);
			return z.b.Task;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x000111A8 File Offset: 0x000101A8
		public new Task g(bool A_0)
		{
			global::a.f.t.c c;
			c.c = this;
			c.d = A_0;
			c.b = AsyncTaskMethodBuilder.Create();
			c.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = c.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.c>(ref c);
			return c.b.Task;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x000111F8 File Offset: 0x000101F8
		private new Task a(string A_0)
		{
			global::a.f.t.am am;
			am.c = this;
			am.d = A_0;
			am.b = AsyncTaskMethodBuilder.Create();
			am.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = am.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.am>(ref am);
			return am.b.Task;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00011248 File Offset: 0x00010248
		public new Task c(string A_0, bool A_1)
		{
			global::a.f.t.k k;
			k.d = this;
			k.c = A_0;
			k.e = A_1;
			k.b = AsyncTaskMethodBuilder.Create();
			k.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = k.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.k>(ref k);
			return k.b.Task;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x000112A0 File Offset: 0x000102A0
		public new Task<FolderStatus> p(string A_0)
		{
			global::a.f.t.ae ae;
			ae.c = this;
			ae.d = A_0;
			ae.b = AsyncTaskMethodBuilder<FolderStatus>.Create();
			ae.a = -1;
			AsyncTaskMethodBuilder<FolderStatus> asyncTaskMethodBuilder = ae.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.ae>(ref ae);
			return ae.b.Task;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x000112F0 File Offset: 0x000102F0
		public new Task<FolderQuota> o(string A_0)
		{
			global::a.f.t.o o;
			o.c = this;
			o.d = A_0;
			o.b = AsyncTaskMethodBuilder<FolderQuota>.Create();
			o.a = -1;
			AsyncTaskMethodBuilder<FolderQuota> asyncTaskMethodBuilder = o.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.o>(ref o);
			return o.b.Task;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00011340 File Offset: 0x00010340
		public new Task<FolderCollection> a(bool A_0, string A_1, string A_2)
		{
			global::a.f.t.ac ac;
			ac.c = this;
			ac.f = A_0;
			ac.d = A_1;
			ac.e = A_2;
			ac.b = AsyncTaskMethodBuilder<FolderCollection>.Create();
			ac.a = -1;
			AsyncTaskMethodBuilder<FolderCollection> asyncTaskMethodBuilder = ac.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.ac>(ref ac);
			return ac.b.Task;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x000113A0 File Offset: 0x000103A0
		public new Task<MessageIndexCollection> a(bool A_0, string A_1, string A_2, string A_3)
		{
			global::a.f.t.ab ab;
			ab.d = this;
			ab.f = A_0;
			ab.g = A_1;
			ab.h = A_2;
			ab.c = A_3;
			ab.b = AsyncTaskMethodBuilder<MessageIndexCollection>.Create();
			ab.a = -1;
			AsyncTaskMethodBuilder<MessageIndexCollection> asyncTaskMethodBuilder = ab.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.ab>(ref ab);
			return ab.b.Task;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00011408 File Offset: 0x00010408
		public new Task<EnvelopeCollection> a(string A_0, bool A_1, EnvelopeParts A_2, int A_3, string[] A_4, string[] A_5, bool A_6, bool A_7)
		{
			global::a.f.t.f f;
			f.c = this;
			f.d = A_0;
			f.e = A_1;
			f.f = A_2;
			f.g = A_3;
			f.h = A_4;
			f.i = A_5;
			f.j = A_6;
			f.k = A_7;
			f.b = AsyncTaskMethodBuilder<EnvelopeCollection>.Create();
			f.a = -1;
			AsyncTaskMethodBuilder<EnvelopeCollection> asyncTaskMethodBuilder = f.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.f>(ref f);
			return f.b.Task;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00011494 File Offset: 0x00010494
		public new Task<EnvelopeCollection> a(long[] A_0, bool A_1, EnvelopeParts[] A_2, int[] A_3, string[][] A_4, string[][] A_5, bool A_6, bool A_7)
		{
			global::a.f.t.i i;
			i.c = this;
			i.d = A_0;
			i.i = A_1;
			i.e = A_2;
			i.f = A_3;
			i.g = A_4;
			i.h = A_5;
			i.j = A_6;
			i.k = A_7;
			i.b = AsyncTaskMethodBuilder<EnvelopeCollection>.Create();
			i.a = -1;
			AsyncTaskMethodBuilder<EnvelopeCollection> asyncTaskMethodBuilder = i.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.i>(ref i);
			return i.b.Task;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00011520 File Offset: 0x00010520
		public new Task<MailMessage> a(long A_0, bool A_1, int A_2, bool A_3)
		{
			global::a.f.t.y y;
			y.e = this;
			y.c = A_0;
			y.d = A_1;
			y.f = A_2;
			y.g = A_3;
			y.b = AsyncTaskMethodBuilder<MailMessage>.Create();
			y.a = -1;
			AsyncTaskMethodBuilder<MailMessage> asyncTaskMethodBuilder = y.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.y>(ref y);
			return y.b.Task;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00011588 File Offset: 0x00010588
		public new Task<MailMessageCollection> a(string A_0, bool A_1, int A_2, bool A_3)
		{
			global::a.f.t.x x;
			x.c = this;
			x.d = A_0;
			x.e = A_1;
			x.f = A_2;
			x.g = A_3;
			x.b = AsyncTaskMethodBuilder<MailMessageCollection>.Create();
			x.a = -1;
			AsyncTaskMethodBuilder<MailMessageCollection> asyncTaskMethodBuilder = x.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.x>(ref x);
			return x.b.Task;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x000115F0 File Offset: 0x000105F0
		public new Task<long> o()
		{
			global::a.f.t.r r;
			r.c = this;
			r.b = AsyncTaskMethodBuilder<long>.Create();
			r.a = -1;
			AsyncTaskMethodBuilder<long> asyncTaskMethodBuilder = r.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.r>(ref r);
			return r.b.Task;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00011638 File Offset: 0x00010638
		public Task<ImapNamespaceCollectionSet> y()
		{
			global::a.f.t.aj aj;
			aj.c = this;
			aj.b = AsyncTaskMethodBuilder<ImapNamespaceCollectionSet>.Create();
			aj.a = -1;
			AsyncTaskMethodBuilder<ImapNamespaceCollectionSet> asyncTaskMethodBuilder = aj.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.aj>(ref aj);
			return aj.b.Task;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0001167D File Offset: 0x0001067D
		public new Task h(string A_0, bool A_1)
		{
			return this.b(A_0, A_1, global::a.f.b.a(SystemMessageFlags.Deleted), MessageFlagAction.Add, true);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00011690 File Offset: 0x00010690
		public new Task b(string A_0, bool A_1, string A_2, MessageFlagAction A_3, bool A_4)
		{
			global::a.f.t.a a;
			a.c = this;
			a.d = A_0;
			a.e = A_1;
			a.f = A_2;
			a.g = A_3;
			a.h = A_4;
			a.b = AsyncTaskMethodBuilder.Create();
			a.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = a.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00011700 File Offset: 0x00010700
		public new Task b(MailMessage A_0, string A_1, string A_2, string A_3, bool A_4, UidPlusResult A_5)
		{
			global::a.f.t.t t;
			t.c = this;
			t.e = A_0;
			t.d = A_1;
			t.g = A_2;
			t.h = A_3;
			t.f = A_4;
			t.m = A_5;
			t.b = AsyncTaskMethodBuilder.Create();
			t.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = t.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.t>(ref t);
			return t.b.Task;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00011778 File Offset: 0x00010778
		public new Task c(string A_0, bool A_1, string A_2, UidPlusResult A_3, bool A_4)
		{
			global::a.f.t.n n;
			n.d = this;
			n.f = A_0;
			n.g = A_1;
			n.e = A_2;
			n.h = A_3;
			n.c = A_4;
			n.b = AsyncTaskMethodBuilder.Create();
			n.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = n.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.n>(ref n);
			return n.b.Task;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x000117E8 File Offset: 0x000107E8
		private new Task a(string A_0, bool A_1)
		{
			global::a.f.t.ak ak;
			ak.c = this;
			ak.d = A_0;
			ak.e = A_1;
			ak.b = AsyncTaskMethodBuilder.Create();
			ak.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = ak.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.ak>(ref ak);
			return ak.b.Task;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00011840 File Offset: 0x00010840
		public new Task a(string A_0, bool A_1, string A_2, UidPlusResult A_3)
		{
			global::a.f.t.h h;
			h.d = this;
			h.e = A_0;
			h.f = A_1;
			h.g = A_2;
			h.c = A_3;
			h.b = AsyncTaskMethodBuilder.Create();
			h.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = h.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.h>(ref h);
			return h.b.Task;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x000118A8 File Offset: 0x000108A8
		public Task r()
		{
			global::a.f.t.ah ah;
			ah.c = this;
			ah.b = AsyncTaskMethodBuilder.Create();
			ah.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = ah.b;
			asyncTaskMethodBuilder.Start<global::a.f.t.ah>(ref ah);
			return ah.b.Task;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x000118ED File Offset: 0x000108ED
		[CompilerGenerated]
		[DebuggerHidden]
		private new Task a()
		{
			return base.fr();
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x000118F5 File Offset: 0x000108F5
		[CompilerGenerated]
		[DebuggerHidden]
		private new Task a(bool A_0)
		{
			return base.fs(A_0);
		}

		// Token: 0x040002BA RID: 698
		private new const string a = "MBN";

		// Token: 0x040002BB RID: 699
		private new const int b = 10000;

		// Token: 0x040002BC RID: 700
		private new int c;

		// Token: 0x040002BD RID: 701
		private new string d;

		// Token: 0x040002BE RID: 702
		private new int e;

		// Token: 0x040002BF RID: 703
		private new int f;

		// Token: 0x040002C0 RID: 704
		private new int g;

		// Token: 0x040002C1 RID: 705
		private new long h;

		// Token: 0x040002C2 RID: 706
		private new long i;

		// Token: 0x040002C3 RID: 707
		private new MessageFlagSet j;

		// Token: 0x040002C4 RID: 708
		private new MessageFlagSet k;

		// Token: 0x040002C5 RID: 709
		private new bool l;

		// Token: 0x040002C6 RID: 710
		private new bool m;

		// Token: 0x040002C7 RID: 711
		private bool n;

		// Token: 0x040002C8 RID: 712
		private new bool o;

		// Token: 0x040002C9 RID: 713
		private new global::a.f.t.g p;

		// Token: 0x040002CA RID: 714
		private global::a.f.t.g[] q;

		// Token: 0x040002CB RID: 715
		private long[] r;

		// Token: 0x040002CC RID: 716
		private new bool s;

		// Token: 0x040002CD RID: 717
		private new bool t;

		// Token: 0x040002CE RID: 718
		private EnvelopeCollection u;

		// Token: 0x040002CF RID: 719
		private bool v;

		// Token: 0x040002D0 RID: 720
		private bool w;

		// Token: 0x040002D1 RID: 721
		private bool x;

		// Token: 0x040002D2 RID: 722
		private global::a.f.t.b y;

		// Token: 0x040002D3 RID: 723
		private global::a.f.t.u z;

		// Token: 0x040002D4 RID: 724
		private global::a.f.t.ad aa;

		// Token: 0x040002D5 RID: 725
		private global::a.f.t.v ab;

		// Token: 0x040002D6 RID: 726
		private global::a.f.t.w ac;

		// Token: 0x02000090 RID: 144
		internal new class g
		{
			// Token: 0x060005A8 RID: 1448 RVA: 0x00013055 File Offset: 0x00012055
			public g(EnvelopeParts A_0, string A_1, string A_2, string A_3, string[] A_4, bool A_5)
			{
				this.a = A_0;
				this.b = A_1;
				this.c = A_2;
				this.d = A_3;
				this.e = A_4;
				this.f = A_5;
			}

			// Token: 0x060005A9 RID: 1449 RVA: 0x0001308C File Offset: 0x0001208C
			public static global::a.f.t.g a(global::a.f.t.g A_0, global::a.f.t.g[] A_1, global::a.f.a A_2, long[] A_3, bool A_4)
			{
				if (A_0 != null)
				{
					return A_0;
				}
				if (A_1 == null)
				{
					return null;
				}
				int num;
				if (A_4)
				{
					try
					{
						num = Array.IndexOf(A_3, A_2.d()["UID"]);
						goto IL_3A;
					}
					catch
					{
						return null;
					}
				}
				num = Array.IndexOf<long>(A_3, (long)A_2.h());
				IL_3A:
				if (num < 0)
				{
					return null;
				}
				return A_1[num];
			}

			// Token: 0x060005AA RID: 1450 RVA: 0x000130F0 File Offset: 0x000120F0
			public EnvelopeParts e()
			{
				return this.a;
			}

			// Token: 0x060005AB RID: 1451 RVA: 0x000130F8 File Offset: 0x000120F8
			public string d()
			{
				return this.b;
			}

			// Token: 0x060005AC RID: 1452 RVA: 0x00013100 File Offset: 0x00012100
			public string b()
			{
				return this.c;
			}

			// Token: 0x060005AD RID: 1453 RVA: 0x00013108 File Offset: 0x00012108
			public string a()
			{
				return this.d;
			}

			// Token: 0x060005AE RID: 1454 RVA: 0x00013110 File Offset: 0x00012110
			public string[] c()
			{
				return this.e;
			}

			// Token: 0x060005AF RID: 1455 RVA: 0x00013118 File Offset: 0x00012118
			public bool f()
			{
				return this.f;
			}

			// Token: 0x040002E9 RID: 745
			private EnvelopeParts a;

			// Token: 0x040002EA RID: 746
			private string b;

			// Token: 0x040002EB RID: 747
			private string c;

			// Token: 0x040002EC RID: 748
			private string d;

			// Token: 0x040002ED RID: 749
			private string[] e;

			// Token: 0x040002EE RID: 750
			private bool f;
		}

		// Token: 0x02000091 RID: 145
		// (Invoke) Token: 0x060005B1 RID: 1457
		protected new delegate void b(ImapEnvelopeDownloadedEventArgs A_0);

		// Token: 0x02000092 RID: 146
		// (Invoke) Token: 0x060005B5 RID: 1461
		protected delegate void u(int A_0, int A_1, int A_2, bc A_3);

		// Token: 0x02000093 RID: 147
		// (Invoke) Token: 0x060005B9 RID: 1465
		protected delegate void ad(string A_0, string A_1, string A_2, string A_3, bc A_4);

		// Token: 0x02000094 RID: 148
		// (Invoke) Token: 0x060005BD RID: 1469
		protected delegate void v(string A_0, int A_1, MessageFlagSet A_2, bc A_3);

		// Token: 0x02000095 RID: 149
		// (Invoke) Token: 0x060005C1 RID: 1473
		protected delegate void w(bc A_0);
	}
}
