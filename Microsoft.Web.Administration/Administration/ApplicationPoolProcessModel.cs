using System;
using System.Globalization;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000010 RID: 16
	public sealed class ApplicationPoolProcessModel : ConfigurationElement
	{
		// Token: 0x060000D2 RID: 210 RVA: 0x00004322 File Offset: 0x00003322
		internal ApplicationPoolProcessModel()
		{
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x0000432A File Offset: 0x0000332A
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x0000433C File Offset: 0x0000333C
		public ProcessModelIdentityType IdentityType
		{
			get
			{
				return (ProcessModelIdentityType)base.GetAttributeValue("identityType");
			}
			set
			{
				base["identityType"] = (int)value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x0000434F File Offset: 0x0000334F
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00004361 File Offset: 0x00003361
		public TimeSpan IdleTimeout
		{
			get
			{
				return (TimeSpan)base.GetAttributeValue("idleTimeout");
			}
			set
			{
				base["idleTimeout"] = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004374 File Offset: 0x00003374
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00004386 File Offset: 0x00003386
		public bool LoadUserProfile
		{
			get
			{
				return (bool)base.GetAttributeValue("loadUserProfile");
			}
			set
			{
				base["loadUserProfile"] = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004399 File Offset: 0x00003399
		// (set) Token: 0x060000DA RID: 218 RVA: 0x000043AC File Offset: 0x000033AC
		public long MaxProcesses
		{
			get
			{
				return (long)base.GetAttributeValue("maxProcesses");
			}
			set
			{
				if (value < 1L || value > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture, Resources.UIntArgumentOutOfRange, new object[]
					{
						"MaxProcesses",
						1,
						int.MaxValue
					}));
				}
				base["maxProcesses"] = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00004418 File Offset: 0x00003418
		// (set) Token: 0x060000DC RID: 220 RVA: 0x0000442A File Offset: 0x0000342A
		public bool PingingEnabled
		{
			get
			{
				return (bool)base.GetAttributeValue("pingingEnabled");
			}
			set
			{
				base["pingingEnabled"] = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060000DD RID: 221 RVA: 0x0000443D File Offset: 0x0000343D
		// (set) Token: 0x060000DE RID: 222 RVA: 0x0000444F File Offset: 0x0000344F
		public TimeSpan PingInterval
		{
			get
			{
				return (TimeSpan)base.GetAttributeValue("pingInterval");
			}
			set
			{
				base["pingInterval"] = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00004462 File Offset: 0x00003462
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00004474 File Offset: 0x00003474
		public TimeSpan PingResponseTime
		{
			get
			{
				return (TimeSpan)base.GetAttributeValue("pingResponseTime");
			}
			set
			{
				base["pingResponseTime"] = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004487 File Offset: 0x00003487
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00004499 File Offset: 0x00003499
		public string Password
		{
			get
			{
				return (string)base.GetAttributeValue("password");
			}
			set
			{
				base["password"] = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000044A7 File Offset: 0x000034A7
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x000044B9 File Offset: 0x000034B9
		public TimeSpan ShutdownTimeLimit
		{
			get
			{
				return (TimeSpan)base.GetAttributeValue("shutdownTimeLimit");
			}
			set
			{
				base["shutdownTimeLimit"] = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x000044CC File Offset: 0x000034CC
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x000044DE File Offset: 0x000034DE
		public TimeSpan StartupTimeLimit
		{
			get
			{
				return (TimeSpan)base.GetAttributeValue("startupTimeLimit");
			}
			set
			{
				base["startupTimeLimit"] = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x000044F1 File Offset: 0x000034F1
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00004503 File Offset: 0x00003503
		public string UserName
		{
			get
			{
				return (string)base.GetAttributeValue("userName");
			}
			set
			{
				base["userName"] = value;
			}
		}
	}
}
