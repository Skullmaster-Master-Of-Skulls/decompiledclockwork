using System;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using a.i;
using a.k;

namespace MailBee.BounceMail
{
	// Token: 0x0200007C RID: 124
	public class DsnRecipient
	{
		// Token: 0x17000246 RID: 582
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000A984 File Offset: 0x00009984
		public bool IsLinked
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x17000247 RID: 583
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x0000A98C File Offset: 0x0000998C
		internal bool IsLinkedInternal
		{
			set
			{
				this.e = value;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0000A995 File Offset: 0x00009995
		public StringDictionary Items
		{
			get
			{
				return this.c.b();
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0000A9A2 File Offset: 0x000099A2
		public string OriginalRecipientType
		{
			get
			{
				return this.c.d("Original-Recipient");
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0000A9B4 File Offset: 0x000099B4
		public string OriginalRecipientAddress
		{
			get
			{
				return this.c.c("Original-Recipient");
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000A9C6 File Offset: 0x000099C6
		public string FinalRecipientType
		{
			get
			{
				return this.c.d("Final-Recipient");
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0000A9D8 File Offset: 0x000099D8
		public string FinalRecipientAddress
		{
			get
			{
				return this.c.c("Final-Recipient");
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000A9EA File Offset: 0x000099EA
		public DsnAction Action
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x0000A9F2 File Offset: 0x000099F2
		public string Status
		{
			get
			{
				return this.c.b("Status");
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x0000AA04 File Offset: 0x00009A04
		public string RemoteMtaType
		{
			get
			{
				return this.c.d("RemoteMTA");
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x0000AA16 File Offset: 0x00009A16
		public string RemoteMtaName
		{
			get
			{
				return this.c.a("RemoteMTA");
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0000AA28 File Offset: 0x00009A28
		public string DiagnosticCodeType
		{
			get
			{
				return this.c.d("Diagnostic-Code");
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0000AA3A File Offset: 0x00009A3A
		public string DiagnosticCode
		{
			get
			{
				return this.c.a("Diagnostic-Code");
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0000AA4C File Offset: 0x00009A4C
		public string LastAttemptDateAsString
		{
			get
			{
				return this.c.b("Last-Attempt-Date");
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000AA5E File Offset: 0x00009A5E
		public DateTime LastAttemptDate
		{
			get
			{
				if (this.c.b("Last-Attempt-Date") == null)
				{
					return DateTime.MinValue;
				}
				return global::a.i.k.a(this.c.b("Last-Attempt-Date"), global::a.i.g.b);
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000AA8E File Offset: 0x00009A8E
		public string WillRetryUntilAsString
		{
			get
			{
				return this.c.b("Will-Retry-Until");
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000AAA0 File Offset: 0x00009AA0
		public DateTime WillRetryUntil
		{
			get
			{
				if (this.c.b("Will-Retry-Until") == null)
				{
					return DateTime.MinValue;
				}
				return global::a.i.k.a(this.c.b("Will-Retry-Until"), global::a.i.g.b);
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000AAD0 File Offset: 0x00009AD0
		internal DsnRecipient(DsnAttachment A_0, string A_1)
		{
			this.a = A_0;
			this.b = A_1;
			this.c = new global::a.k.e();
			foreach (string text in Regex.Split(A_1.Replace("\r\n", "\n"), "\n"))
			{
				if (text != string.Empty)
				{
					string[] array2 = text.Split(new char[]
					{
						':'
					}, 2);
					if (array2.Length == 2)
					{
						this.c.a(array2[0], array2[1]);
					}
				}
			}
			if (this.c.b("Action") != null)
			{
				string text2 = this.c.b("Action").ToLower();
				if (!(text2 == "failed"))
				{
					if (!(text2 == "delayed"))
					{
						if (!(text2 == "delivered"))
						{
							if (!(text2 == "relayed"))
							{
								if (!(text2 == "expanded"))
								{
									this.d = DsnAction.Unknown;
								}
								else
								{
									this.d = DsnAction.Expanded;
								}
							}
							else
							{
								this.d = DsnAction.Relayed;
							}
						}
						else
						{
							this.d = DsnAction.Delivered;
						}
					}
					else
					{
						this.d = DsnAction.Delayed;
					}
				}
				else
				{
					this.d = DsnAction.Failed;
				}
			}
			else
			{
				this.d = DsnAction.Unknown;
			}
			if (this.c.b("Disposition") != null && (this.c.b("Disposition").IndexOf("displayed") != -1 || this.c.b("Disposition").IndexOf("MDN-sent-automatically") != -1))
			{
				this.d = DsnAction.Delivered;
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000AC64 File Offset: 0x00009C64
		public override string ToString()
		{
			return this.b;
		}

		// Token: 0x040001C7 RID: 455
		private DsnAttachment a;

		// Token: 0x040001C8 RID: 456
		private string b;

		// Token: 0x040001C9 RID: 457
		private global::a.k.e c;

		// Token: 0x040001CA RID: 458
		private DsnAction d;

		// Token: 0x040001CB RID: 459
		private bool e;

		// Token: 0x040001CC RID: 460
		private const string f = "Original-Recipient";

		// Token: 0x040001CD RID: 461
		private const string g = "Final-Recipient";

		// Token: 0x040001CE RID: 462
		private const string h = "Action";

		// Token: 0x040001CF RID: 463
		private const string i = "Status";

		// Token: 0x040001D0 RID: 464
		private const string j = "RemoteMTA";

		// Token: 0x040001D1 RID: 465
		private const string k = "Diagnostic-Code";

		// Token: 0x040001D2 RID: 466
		private const string l = "Last-Attempt-Date";

		// Token: 0x040001D3 RID: 467
		private const string m = "Will-Retry-Until";

		// Token: 0x040001D4 RID: 468
		private const string n = "Disposition";
	}
}
