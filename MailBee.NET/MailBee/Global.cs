using System;
using System.Globalization;
using System.Text;
using a;
using MailBee.DnsMX;

namespace MailBee
{
	// Token: 0x02000030 RID: 48
	public class Global
	{
		// Token: 0x06000133 RID: 307 RVA: 0x00007A61 File Offset: 0x00006A61
		private Global()
		{
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00007A69 File Offset: 0x00006A69
		internal static bool IsWindows
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00007A6C File Offset: 0x00006A6C
		public static DnsServerCollection DnsServers
		{
			get
			{
				return Global.a;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00007A73 File Offset: 0x00006A73
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00007A7F File Offset: 0x00006A7F
		public static string LicenseKey
		{
			get
			{
				return Resources.Instance.LicenseKeyIsWriteOnlyWarning;
			}
			set
			{
				Global.u = bn.a(value, typeof(Global));
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00007A98 File Offset: 0x00006A98
		internal static void a(Type A_0, string A_1)
		{
			if (Global.u == null || !bn.a(Global.u.f(), A_0))
			{
				object obj = Global.v;
				lock (obj)
				{
					Global.u = bn.a(A_1, A_0);
				}
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00007AF8 File Offset: 0x00006AF8
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00007AFF File Offset: 0x00006AFF
		public static bool AutodetectPortAndSslMode
		{
			get
			{
				return Global.b;
			}
			set
			{
				Global.b = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00007B07 File Offset: 0x00006B07
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00007B0E File Offset: 0x00006B0E
		public static int DefaultTimeout
		{
			get
			{
				return Global.c;
			}
			set
			{
				if (value < 0)
				{
					throw new MailBeeInvalidArgumentException(23);
				}
				Global.c = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00007B22 File Offset: 0x00006B22
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00007B29 File Offset: 0x00006B29
		public static string PrivateDataCover
		{
			get
			{
				return Global.d;
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				Global.d = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00007B3C File Offset: 0x00006B3C
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00007B43 File Offset: 0x00006B43
		public static int MaxMultiLineDataLength
		{
			get
			{
				return Global.e;
			}
			set
			{
				if (value < 0)
				{
					throw new MailBeeInvalidArgumentException(23);
				}
				Global.e = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00007B57 File Offset: 0x00006B57
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00007B5E File Offset: 0x00006B5E
		public static int DnsPort
		{
			get
			{
				return Global.f;
			}
			set
			{
				if (value > 65535 || value < 0)
				{
					throw new MailBeeInvalidArgumentException(23);
				}
				Global.f = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00007B7A File Offset: 0x00006B7A
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00007B81 File Offset: 0x00006B81
		public static int DnsMaxFailureCount
		{
			get
			{
				return Global.g;
			}
			set
			{
				if (value < 1)
				{
					throw new MailBeeInvalidArgumentException(23);
				}
				Global.g = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00007B95 File Offset: 0x00006B95
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00007B9C File Offset: 0x00006B9C
		public static int DnsNextAttemptInterval
		{
			get
			{
				return Global.h;
			}
			set
			{
				if (value < 0)
				{
					throw new MailBeeInvalidArgumentException(23);
				}
				Global.h = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00007BB0 File Offset: 0x00006BB0
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00007BB7 File Offset: 0x00006BB7
		public static CultureInfo DefaultCulture
		{
			get
			{
				return Global.i;
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				Global.i = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00007BCA File Offset: 0x00006BCA
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00007BD1 File Offset: 0x00006BD1
		public static Encoding DefaultEncoding
		{
			get
			{
				return Global.j;
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				Global.j = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00007BE4 File Offset: 0x00006BE4
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00007BEB File Offset: 0x00006BEB
		public static string DefaultServerName
		{
			get
			{
				return Global.k;
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				Global.k = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00007BFE File Offset: 0x00006BFE
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00007C05 File Offset: 0x00006C05
		public static int TcpBufSize
		{
			get
			{
				return Global.l;
			}
			set
			{
				if (value < 1)
				{
					throw new MailBeeInvalidArgumentException(23);
				}
				Global.l = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00007C19 File Offset: 0x00006C19
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00007C29 File Offset: 0x00006C29
		public static bool Pipelining
		{
			get
			{
				return !Global.n && Global.m;
			}
			set
			{
				Global.m = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00007C31 File Offset: 0x00006C31
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00007C38 File Offset: 0x00006C38
		public static bool SafeMode
		{
			get
			{
				return Global.n;
			}
			set
			{
				Global.n = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00007C40 File Offset: 0x00006C40
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00007C47 File Offset: 0x00006C47
		public static bool FipsMode
		{
			get
			{
				return Global.o;
			}
			set
			{
				Global.o = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00007C4F File Offset: 0x00006C4F
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00007C56 File Offset: 0x00006C56
		public static bool FixBadDates
		{
			get
			{
				return Global.p;
			}
			set
			{
				Global.p = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00007C5E File Offset: 0x00006C5E
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00007C65 File Offset: 0x00006C65
		public static string LocalSmtpMXServerName
		{
			get
			{
				return Global.q;
			}
			set
			{
				Global.q = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00007C6D File Offset: 0x00006C6D
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00007C74 File Offset: 0x00006C74
		public static bool PreserveMimePartOrder
		{
			get
			{
				return Global.r;
			}
			set
			{
				Global.r = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00007C7C File Offset: 0x00006C7C
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00007C83 File Offset: 0x00006C83
		public static int UnwrappedLineLengthLimit
		{
			get
			{
				return Global.s;
			}
			set
			{
				if (value < 76 || value > 255)
				{
					throw new MailBeeInvalidArgumentException(23);
				}
				Global.s = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00007CA0 File Offset: 0x00006CA0
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00007CA7 File Offset: 0x00006CA7
		public static bool PreferIPv4Hosts
		{
			get
			{
				return Global.t;
			}
			set
			{
				Global.t = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00007CAF File Offset: 0x00006CAF
		public static string Version
		{
			get
			{
				return "11.2.0.590";
			}
		}

		// Token: 0x04000133 RID: 307
		private static DnsServerCollection a = new DnsServerCollection();

		// Token: 0x04000134 RID: 308
		private static bool b = true;

		// Token: 0x04000135 RID: 309
		internal static int c = 20000;

		// Token: 0x04000136 RID: 310
		private static string d = "********";

		// Token: 0x04000137 RID: 311
		private static int e = 128;

		// Token: 0x04000138 RID: 312
		private static int f = 53;

		// Token: 0x04000139 RID: 313
		private static int g = 3;

		// Token: 0x0400013A RID: 314
		private static int h = 1000;

		// Token: 0x0400013B RID: 315
		private static CultureInfo i = CultureInfo.InvariantCulture;

		// Token: 0x0400013C RID: 316
		private static Encoding j = Encoding.Default;

		// Token: 0x0400013D RID: 317
		private static string k = "localhost";

		// Token: 0x0400013E RID: 318
		private static int l = 65536;

		// Token: 0x0400013F RID: 319
		private static bool m = true;

		// Token: 0x04000140 RID: 320
		private static bool n = false;

		// Token: 0x04000141 RID: 321
		private static bool o = false;

		// Token: 0x04000142 RID: 322
		private static bool p = false;

		// Token: 0x04000143 RID: 323
		private static string q = null;

		// Token: 0x04000144 RID: 324
		private static bool r = true;

		// Token: 0x04000145 RID: 325
		private static int s = 76;

		// Token: 0x04000146 RID: 326
		private static bool t = true;

		// Token: 0x04000147 RID: 327
		internal static bm u = null;

		// Token: 0x04000148 RID: 328
		private static object v = new object();

		// Token: 0x04000149 RID: 329
		internal const string w = ".NET 4.5";

		// Token: 0x0400014A RID: 330
		internal const string x = "590";

		// Token: 0x0400014B RID: 331
		internal const string y = "11.2.0";

		// Token: 0x0400014C RID: 332
		internal const string z = "11.2.0.590";

		// Token: 0x0400014D RID: 333
		internal const string aa = "11.2.0 build 590 for .NET 4.5";
	}
}
