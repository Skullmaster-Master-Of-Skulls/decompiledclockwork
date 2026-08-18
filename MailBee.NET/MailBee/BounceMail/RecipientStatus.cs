using System;
using System.Collections;
using System.Collections.Specialized;
using a.k;

namespace MailBee.BounceMail
{
	// Token: 0x02000081 RID: 129
	public class RecipientStatus
	{
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0000AC96 File Offset: 0x00009C96
		public CommonType Common
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x17000259 RID: 601
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x0000AC9E File Offset: 0x00009C9E
		internal CommonType CommonInternal
		{
			set
			{
				this.b = value;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0000ACA7 File Offset: 0x00009CA7
		public DetailedType Detailed
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x1700025B RID: 603
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x0000ACAF File Offset: 0x00009CAF
		internal DetailedType DetailedInternal
		{
			set
			{
				this.c = value;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0000ACB8 File Offset: 0x00009CB8
		public string UserDefined
		{
			get
			{
				return this.a.c(this.d);
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000ACCB File Offset: 0x00009CCB
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x0000ACD4 File Offset: 0x00009CD4
		internal string Type
		{
			get
			{
				return this.d;
			}
			set
			{
				this.d = value;
				if (RecipientStatus.m.ContainsKey(this.d))
				{
					this.c = (DetailedType)((object[])RecipientStatus.m[this.d])[0];
					this.b = (CommonType)RecipientStatus.l[((object[])RecipientStatus.m[this.d])[1]];
				}
				else if (RecipientStatus.l.ContainsKey(this.d))
				{
					this.b = (CommonType)RecipientStatus.l[this.d];
				}
				else
				{
					this.c = DetailedType.UserDefined;
					this.b = CommonType.UserDefined;
				}
				this.g = (this.b == CommonType.Undeliverable || this.b == CommonType.Blocked || this.c == DetailedType.AddressChanged);
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000ADAB File Offset: 0x00009DAB
		public DsnRecipient DsnInfo
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x1700025F RID: 607
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x0000ADB3 File Offset: 0x00009DB3
		internal DsnRecipient DsnInternal
		{
			set
			{
				this.e = value;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0000ADBC File Offset: 0x00009DBC
		public string Description
		{
			get
			{
				switch (this.i)
				{
				case RecipientStatusSource.DsnThenText:
					if (this.HasDsn && this.e.Status != null)
					{
						return this.e.Status + " " + this.e.DiagnosticCode;
					}
					return this.f;
				case RecipientStatusSource.Dsn:
					if (this.HasDsn)
					{
						return this.e.Status + " " + this.e.DiagnosticCode;
					}
					return null;
				case RecipientStatusSource.TextThenDsn:
					if (!this.HasText)
					{
						return this.e.Status + " " + this.e.DiagnosticCode;
					}
					return this.f;
				case RecipientStatusSource.Text:
					if (!this.HasText)
					{
						return null;
					}
					return this.f;
				default:
					return null;
				}
			}
		}

		// Token: 0x17000261 RID: 609
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x0000AE95 File Offset: 0x00009E95
		internal string DescriptionFromTemplate
		{
			set
			{
				this.f = value;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0000AEA0 File Offset: 0x00009EA0
		public bool IsBounced
		{
			get
			{
				switch (this.i)
				{
				case RecipientStatusSource.DsnThenText:
				case RecipientStatusSource.Dsn:
					if (this.HasDsn && this.e.Status != null)
					{
						return this.e.Action == DsnAction.Failed || this.e.Action == DsnAction.Delayed;
					}
					return this.g;
				case RecipientStatusSource.TextThenDsn:
				case RecipientStatusSource.Text:
					if (!this.HasText)
					{
						return this.e.Action == DsnAction.Failed || this.e.Action == DsnAction.Delayed;
					}
					return this.g;
				default:
					return this.g;
				}
			}
		}

		// Token: 0x17000263 RID: 611
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x0000AF39 File Offset: 0x00009F39
		internal bool IsBouncedInternal
		{
			set
			{
				this.g = value;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0000AF44 File Offset: 0x00009F44
		public string EmailAddress
		{
			get
			{
				switch (this.i)
				{
				case RecipientStatusSource.DsnThenText:
					if (!this.HasDsn)
					{
						return this.h;
					}
					if (this.e.OriginalRecipientAddress == null)
					{
						return this.e.FinalRecipientAddress;
					}
					return this.e.OriginalRecipientAddress;
				case RecipientStatusSource.Dsn:
					if (!this.HasDsn)
					{
						return null;
					}
					if (this.e.OriginalRecipientAddress == null)
					{
						return this.e.FinalRecipientAddress;
					}
					return this.e.OriginalRecipientAddress;
				case RecipientStatusSource.TextThenDsn:
					if (this.HasText)
					{
						return this.h;
					}
					if (this.e.OriginalRecipientAddress == null)
					{
						return this.e.FinalRecipientAddress;
					}
					return this.e.OriginalRecipientAddress;
				case RecipientStatusSource.Text:
					if (!this.HasText)
					{
						return null;
					}
					return this.h;
				default:
					return null;
				}
			}
		}

		// Token: 0x17000265 RID: 613
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x0000B01C File Offset: 0x0000A01C
		internal string EmailAddressFromTemplate
		{
			set
			{
				this.h = value;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x0000B025 File Offset: 0x0000A025
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x0000B02D File Offset: 0x0000A02D
		public RecipientStatusSource Source
		{
			get
			{
				return this.i;
			}
			set
			{
				this.i = value;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0000B036 File Offset: 0x0000A036
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x0000B03E File Offset: 0x0000A03E
		internal string Keyword
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

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000B047 File Offset: 0x0000A047
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x0000B04F File Offset: 0x0000A04F
		internal StringDictionary MatchedKeywords
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

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0000B058 File Offset: 0x0000A058
		internal bool HasText
		{
			get
			{
				return this.e == null || this.e.IsLinked;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000B06F File Offset: 0x0000A06F
		internal bool HasDsn
		{
			get
			{
				return this.e != null;
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000B07C File Offset: 0x0000A07C
		static RecipientStatus()
		{
			if (RecipientStatus.l == null)
			{
				RecipientStatus.l = new Hashtable();
				RecipientStatus.l.Add("undeliv", CommonType.Undeliverable);
				RecipientStatus.l.Add("block", CommonType.Blocked);
				RecipientStatus.l.Add("info", CommonType.Information);
				RecipientStatus.l.Add("warn", CommonType.Warning);
				RecipientStatus.l.Add("rcpt", CommonType.Receipt);
				RecipientStatus.l.Add("unknown", CommonType.Unknown);
			}
			if (RecipientStatus.m == null)
			{
				RecipientStatus.m = new Hashtable();
				RecipientStatus.m.Add("hard", new object[]
				{
					DetailedType.Hard,
					"undeliv"
				});
				RecipientStatus.m.Add("soft", new object[]
				{
					DetailedType.Soft,
					"undeliv"
				});
				RecipientStatus.m.Add("spam", new object[]
				{
					DetailedType.Spam,
					"block"
				});
				RecipientStatus.m.Add("virus", new object[]
				{
					DetailedType.Virus,
					"block"
				});
				RecipientStatus.m.Add("challenge", new object[]
				{
					DetailedType.ChallengeResponse,
					"block"
				});
				RecipientStatus.m.Add("otherblock", new object[]
				{
					DetailedType.OtherBlocked,
					"block"
				});
				RecipientStatus.m.Add("auto", new object[]
				{
					DetailedType.AutoReply,
					"info"
				});
				RecipientStatus.m.Add("change", new object[]
				{
					DetailedType.AddressChanged,
					"info"
				});
				RecipientStatus.m.Add("modif", new object[]
				{
					DetailedType.Modified,
					"info"
				});
				RecipientStatus.m.Add("fw", new object[]
				{
					DetailedType.Forwarded,
					"info"
				});
				RecipientStatus.m.Add("subscr", new object[]
				{
					DetailedType.Subscribe,
					"info"
				});
				RecipientStatus.m.Add("unsubscr", new object[]
				{
					DetailedType.Unsubscribe,
					"info"
				});
				RecipientStatus.m.Add("temp", new object[]
				{
					DetailedType.Temporary,
					"warn"
				});
				RecipientStatus.m.Add("deliv", new object[]
				{
					DetailedType.Delivered,
					"rcpt"
				});
				RecipientStatus.m.Add("read", new object[]
				{
					DetailedType.Read,
					"rcpt"
				});
				RecipientStatus.m.Add("unknown", new object[]
				{
					DetailedType.Unknown,
					"unknown"
				});
			}
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000B396 File Offset: 0x0000A396
		internal RecipientStatus(c A_0)
		{
			this.a = A_0;
		}

		// Token: 0x040001F4 RID: 500
		private c a;

		// Token: 0x040001F5 RID: 501
		private CommonType b = CommonType.Unknown;

		// Token: 0x040001F6 RID: 502
		private DetailedType c = DetailedType.Unknown;

		// Token: 0x040001F7 RID: 503
		private string d;

		// Token: 0x040001F8 RID: 504
		private DsnRecipient e;

		// Token: 0x040001F9 RID: 505
		private string f = string.Empty;

		// Token: 0x040001FA RID: 506
		private bool g;

		// Token: 0x040001FB RID: 507
		private string h = string.Empty;

		// Token: 0x040001FC RID: 508
		private RecipientStatusSource i;

		// Token: 0x040001FD RID: 509
		private string j;

		// Token: 0x040001FE RID: 510
		private StringDictionary k;

		// Token: 0x040001FF RID: 511
		private static Hashtable l;

		// Token: 0x04000200 RID: 512
		private static Hashtable m;
	}
}
