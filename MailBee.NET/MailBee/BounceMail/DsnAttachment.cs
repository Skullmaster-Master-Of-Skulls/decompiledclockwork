using System;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using a;
using a.i;
using a.k;
using MailBee.Mime;

namespace MailBee.BounceMail
{
	// Token: 0x0200007A RID: 122
	public class DsnAttachment
	{
		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000A6B7 File Offset: 0x000096B7
		public StringDictionary Items
		{
			get
			{
				return this.c.b();
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0000A6C4 File Offset: 0x000096C4
		public DsnRecipientCollection Recipients
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0000A6CC File Offset: 0x000096CC
		public string OriginalEnvelopeID
		{
			get
			{
				if (this.c.b("Original-Envelope-Id") != null)
				{
					return this.c.b("Original-Envelope-Id");
				}
				if (this.d.Count > 0)
				{
					return this.d[0].Items["Original-Message-ID"];
				}
				return null;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x0000A727 File Offset: 0x00009727
		public string ReportingMtaType
		{
			get
			{
				return this.c.d("Reporting-MTA");
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0000A739 File Offset: 0x00009739
		public string ReportingMtaName
		{
			get
			{
				return this.c.a("Reporting-MTA");
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000A74B File Offset: 0x0000974B
		public string DsnGatewayType
		{
			get
			{
				return this.c.d("DSN-Gateway");
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0000A75D File Offset: 0x0000975D
		public string DsnGatewayName
		{
			get
			{
				return this.c.a("DSN-Gateway");
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000A76F File Offset: 0x0000976F
		public string ReceivedFromMtaType
		{
			get
			{
				return this.c.d("Received-From-MTA");
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0000A781 File Offset: 0x00009781
		public string ReceivedFromMtaName
		{
			get
			{
				return this.c.a("Received-From-MTA");
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000A793 File Offset: 0x00009793
		public string ArrivalDateAsString
		{
			get
			{
				return this.c.b("Arrival-Date");
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0000A7A5 File Offset: 0x000097A5
		public DateTime ArrivalDate
		{
			get
			{
				if (this.c.b("Arrival-Date") == null)
				{
					return DateTime.MinValue;
				}
				return global::a.i.k.a(this.c.b("Arrival-Date").Trim(), global::a.i.g.b);
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000A7DC File Offset: 0x000097DC
		public DsnAttachment(Attachment dsnAttach, Encoding enc)
		{
			if (dsnAttach == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (enc == null)
			{
				enc = bb.a(dsnAttach.AsMimePart.Charset);
			}
			if (dsnAttach.ContentType == null || (dsnAttach.ContentType.ToLower() != "message/delivery-status" && dsnAttach.ContentType.ToLower() != "message/disposition-notification"))
			{
				throw new MailBeeInvalidArgumentException(20);
			}
			this.a = dsnAttach;
			this.b = enc;
			this.c = new global::a.k.e();
			this.d = new DsnRecipientCollection();
			string[] array = Regex.Split(Regex.Replace(Regex.Replace(bb.d(this.ToString()), "\r\n +", " "), "\r\n\t+", ""), "\r\n\r\n");
			if (array[0].IndexOf("Reporting-MTA:") != -1 && array[0].IndexOf("Action:") == -1)
			{
				foreach (string text in Regex.Split(array[0], "\r\n"))
				{
					if (text != string.Empty)
					{
						string[] array3 = text.Split(new char[]
						{
							':'
						}, 2);
						if (array3.Length == 2)
						{
							this.c.a(array3[0], array3[1]);
						}
					}
				}
			}
			for (int j = (this.c.a() == 0) ? 0 : 1; j < array.Length; j++)
			{
				if (array[j].Trim() != string.Empty)
				{
					this.d.a(new DsnRecipient(this, array[j]));
				}
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000A96C File Offset: 0x0000996C
		public override string ToString()
		{
			return this.b.GetString(this.a.GetData());
		}

		// Token: 0x040001B6 RID: 438
		private Attachment a;

		// Token: 0x040001B7 RID: 439
		private Encoding b;

		// Token: 0x040001B8 RID: 440
		private global::a.k.e c;

		// Token: 0x040001B9 RID: 441
		private DsnRecipientCollection d;

		// Token: 0x040001BA RID: 442
		private const string e = "Original-Envelope-Id";

		// Token: 0x040001BB RID: 443
		private const string f = "Original-Message-ID";

		// Token: 0x040001BC RID: 444
		private const string g = "Reporting-MTA";

		// Token: 0x040001BD RID: 445
		private const string h = "DSN-Gateway";

		// Token: 0x040001BE RID: 446
		private const string i = "Received-From-MTA";

		// Token: 0x040001BF RID: 447
		private const string j = "Arrival-Date";
	}
}
