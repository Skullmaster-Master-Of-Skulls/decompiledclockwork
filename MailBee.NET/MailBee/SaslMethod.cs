using System;
using System.Text;
using a;

namespace MailBee
{
	// Token: 0x02000003 RID: 3
	public abstract class SaslMethod
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000031BF File Offset: 0x000021BF
		public SaslMethod()
		{
			this.r = 0;
			this.j = true;
			this.k = null;
			this.l = null;
			this.p = Global.DefaultEncoding;
			this.q = Global.DefaultEncoding;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000031F9 File Offset: 0x000021F9
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00003208 File Offset: 0x00002208
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06000007 RID: 7
		public abstract string GetSaslID();

		// Token: 0x06000008 RID: 8 RVA: 0x0000320A File Offset: 0x0000220A
		internal virtual AuthenticationMethods GetMethodEnumMember()
		{
			return AuthenticationMethods.SaslUserDefined;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00003210 File Offset: 0x00002210
		internal static AuthenticationMethods a(string A_0, SaslMethod A_1)
		{
			A_0 = A_0.ToUpper();
			if (A_1 != null && A_0 == A_1.GetSaslID())
			{
				return AuthenticationMethods.SaslUserDefined;
			}
			uint num = global::b.a(A_0);
			if (num <= 2750295684U)
			{
				if (num <= 775363725U)
				{
					if (num != 569206038U)
					{
						if (num == 775363725U)
						{
							if (A_0 == "MSN")
							{
								return AuthenticationMethods.SaslMsn;
							}
						}
					}
					else if (A_0 == "XOAUTH")
					{
						return AuthenticationMethods.SaslOAuth;
					}
				}
				else if (num != 2360752044U)
				{
					if (num == 2750295684U)
					{
						if (A_0 == "DIGEST-MD5")
						{
							return AuthenticationMethods.SaslDigestMD5;
						}
					}
				}
				else if (A_0 == "XOAUTH2")
				{
					return AuthenticationMethods.SaslOAuth2;
				}
			}
			else if (num <= 3033912771U)
			{
				if (num != 2899447378U)
				{
					if (num == 3033912771U)
					{
						if (A_0 == "CRAM-MD5")
						{
							return AuthenticationMethods.SaslCramMD5;
						}
					}
				}
				else if (A_0 == "LOGIN")
				{
					return AuthenticationMethods.SaslLogin;
				}
			}
			else if (num != 3819592391U)
			{
				if (num != 3881151788U)
				{
					if (num == 4030665388U)
					{
						if (A_0 == "GSSAPI")
						{
							return AuthenticationMethods.SaslGssApi;
						}
					}
				}
				else if (A_0 == "NTLM")
				{
					return AuthenticationMethods.SaslNtlm;
				}
			}
			else if (A_0 == "PLAIN")
			{
				return AuthenticationMethods.SaslPlain;
			}
			return AuthenticationMethods.None;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000337C File Offset: 0x0000237C
		internal static SaslMethod a(AuthenticationMethods A_0, bool A_1, SaslMethod A_2)
		{
			if (A_2 != null && (A_0 & AuthenticationMethods.SaslUserDefined) > AuthenticationMethods.None && A_2.IsSecure())
			{
				return A_2;
			}
			if ((A_0 & AuthenticationMethods.SaslCramMD5) > AuthenticationMethods.None)
			{
				return new aa();
			}
			if ((A_0 & AuthenticationMethods.SaslDigestMD5) > AuthenticationMethods.None)
			{
				return new aj();
			}
			if ((A_0 & AuthenticationMethods.SaslNtlm) > AuthenticationMethods.None)
			{
				if (!A_1)
				{
					return new j();
				}
				return new a6();
			}
			else if ((A_0 & AuthenticationMethods.SaslMsn) > AuthenticationMethods.None)
			{
				if (!A_1)
				{
					return new ba();
				}
				return new d();
			}
			else if ((A_0 & AuthenticationMethods.SaslGssApi) > AuthenticationMethods.None)
			{
				if (!A_1)
				{
					return new bi();
				}
				return new az();
			}
			else
			{
				if ((A_0 & AuthenticationMethods.SaslOAuth) > AuthenticationMethods.None)
				{
					return new n();
				}
				if ((A_0 & AuthenticationMethods.SaslOAuth2) > AuthenticationMethods.None)
				{
					return new an();
				}
				return null;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00003422 File Offset: 0x00002422
		internal static SaslMethod b(AuthenticationMethods A_0, SaslMethod A_1)
		{
			if (A_1 != null && (A_0 & AuthenticationMethods.SaslUserDefined) == AuthenticationMethods.SaslUserDefined && !A_1.IsSecure())
			{
				return A_1;
			}
			if ((A_0 & AuthenticationMethods.SaslLogin) > AuthenticationMethods.None)
			{
				return new am();
			}
			if ((A_0 & AuthenticationMethods.SaslPlain) > AuthenticationMethods.None)
			{
				return new t();
			}
			return null;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00003451 File Offset: 0x00002451
		internal static bool a(AuthenticationMethods A_0, SaslMethod A_1)
		{
			return SaslMethod.a(A_0, false, A_1) != null || SaslMethod.b(A_0, A_1) != null;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000346B File Offset: 0x0000246B
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00003473 File Offset: 0x00002473
		public bool ExpectBase64Challenge
		{
			get
			{
				return this.j;
			}
			set
			{
				this.j = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000347C File Offset: 0x0000247C
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00003484 File Offset: 0x00002484
		public byte[] ServerChallenge
		{
			get
			{
				return this.k;
			}
			set
			{
				this.k = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000348D File Offset: 0x0000248D
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00003495 File Offset: 0x00002495
		public byte[] ClientAnswer
		{
			get
			{
				return this.l;
			}
			set
			{
				this.l = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000349E File Offset: 0x0000249E
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000034A6 File Offset: 0x000024A6
		public string AccountName
		{
			get
			{
				return this.m;
			}
			set
			{
				this.m = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000034AF File Offset: 0x000024AF
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000034B7 File Offset: 0x000024B7
		public string Password
		{
			get
			{
				return this.n;
			}
			set
			{
				this.n = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000034C0 File Offset: 0x000024C0
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000034C8 File Offset: 0x000024C8
		public string AccountDomain
		{
			get
			{
				return this.o;
			}
			set
			{
				this.o = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000034D1 File Offset: 0x000024D1
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000034D9 File Offset: 0x000024D9
		public Encoding ServerChallengeEncoding
		{
			get
			{
				return this.p;
			}
			set
			{
				this.p = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000034E2 File Offset: 0x000024E2
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000034EA File Offset: 0x000024EA
		public Encoding ClientAnswerEncoding
		{
			get
			{
				return this.q;
			}
			set
			{
				this.q = value;
			}
		}

		// Token: 0x0600001D RID: 29
		public abstract void CreateNextClientAnswer();

		// Token: 0x0600001E RID: 30
		public abstract bool IsSecure();

		// Token: 0x0600001F RID: 31 RVA: 0x000034F3 File Offset: 0x000024F3
		public virtual bool IsFipsCompliant()
		{
			return true;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000034F6 File Offset: 0x000024F6
		public virtual bool RequiresCredentials()
		{
			return true;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000034F9 File Offset: 0x000024F9
		public virtual bool AccountDataIsPassword()
		{
			return false;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000034FC File Offset: 0x000024FC
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00003504 File Offset: 0x00002504
		public int Stage
		{
			get
			{
				return this.r;
			}
			set
			{
				this.r = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000350D File Offset: 0x0000250D
		public string ServerName
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00003515 File Offset: 0x00002515
		// (set) Token: 0x06000026 RID: 38 RVA: 0x0000351D File Offset: 0x0000251D
		internal string ServerNameInternal
		{
			get
			{
				return this.s;
			}
			set
			{
				this.s = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00003526 File Offset: 0x00002526
		public string ServiceName
		{
			get
			{
				return this.t;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000352E File Offset: 0x0000252E
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00003536 File Offset: 0x00002536
		internal string ServiceNameInternal
		{
			get
			{
				return this.t;
			}
			set
			{
				this.t = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000353F File Offset: 0x0000253F
		public string TargetName
		{
			get
			{
				return this.u;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00003547 File Offset: 0x00002547
		// (set) Token: 0x0600002C RID: 44 RVA: 0x0000354F File Offset: 0x0000254F
		internal virtual string TargetNameInternal
		{
			get
			{
				return this.u;
			}
			set
			{
				this.a(value);
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003558 File Offset: 0x00002558
		internal void a(string A_0)
		{
			this.u = A_0;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003564 File Offset: 0x00002564
		internal void h()
		{
			if (this.AccountDomain == null && this.AccountName != null)
			{
				int num = this.AccountName.IndexOf('\\');
				if (num > -1 && num < this.AccountName.Length - 1)
				{
					this.AccountDomain = this.AccountName.Substring(0, num);
					this.AccountName = this.AccountName.Substring(num + 1);
					return;
				}
				int num2 = this.AccountName.IndexOf('@');
				if (num2 > -1 && num2 < this.AccountName.Length - 1)
				{
					this.AccountDomain = this.AccountName.Substring(num2 + 1);
					this.AccountName = this.AccountName.Substring(0, num2);
				}
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000361A File Offset: 0x0000261A
		private string a()
		{
			return Environment.UserDomainName;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003624 File Offset: 0x00002624
		internal string e()
		{
			string text = this.AccountDomain;
			if (text == null)
			{
				try
				{
					text = (this.v ? this.a() : string.Empty);
				}
				catch (PlatformNotSupportedException)
				{
				}
			}
			return text;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00003668 File Offset: 0x00002668
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00003670 File Offset: 0x00002670
		internal bool LocalDomainIsDefault
		{
			get
			{
				return this.v;
			}
			set
			{
				this.v = value;
			}
		}

		// Token: 0x04000003 RID: 3
		internal const string a = "LOGIN";

		// Token: 0x04000004 RID: 4
		internal const string b = "PLAIN";

		// Token: 0x04000005 RID: 5
		internal const string c = "CRAM-MD5";

		// Token: 0x04000006 RID: 6
		internal const string d = "DIGEST-MD5";

		// Token: 0x04000007 RID: 7
		internal const string e = "NTLM";

		// Token: 0x04000008 RID: 8
		internal const string f = "MSN";

		// Token: 0x04000009 RID: 9
		internal const string g = "GSSAPI";

		// Token: 0x0400000A RID: 10
		internal const string h = "XOAUTH";

		// Token: 0x0400000B RID: 11
		internal const string i = "XOAUTH2";

		// Token: 0x0400000C RID: 12
		private bool j;

		// Token: 0x0400000D RID: 13
		private byte[] k;

		// Token: 0x0400000E RID: 14
		private byte[] l;

		// Token: 0x0400000F RID: 15
		private string m;

		// Token: 0x04000010 RID: 16
		private string n;

		// Token: 0x04000011 RID: 17
		private string o;

		// Token: 0x04000012 RID: 18
		private Encoding p;

		// Token: 0x04000013 RID: 19
		private Encoding q;

		// Token: 0x04000014 RID: 20
		private int r;

		// Token: 0x04000015 RID: 21
		private string s;

		// Token: 0x04000016 RID: 22
		private string t;

		// Token: 0x04000017 RID: 23
		private string u;

		// Token: 0x04000018 RID: 24
		private bool v;
	}
}
