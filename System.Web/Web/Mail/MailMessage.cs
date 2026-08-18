using System;
using System.Collections;
using System.Security.Permissions;
using System.Text;

namespace System.Web.Mail
{
	// Token: 0x0200078E RID: 1934
	[Obsolete("The recommended alternative is System.Net.Mail.MailMessage. http://go.microsoft.com/fwlink/?linkid=14202")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class MailMessage
	{
		// Token: 0x170017D7 RID: 6103
		// (get) Token: 0x06005CF6 RID: 23798 RVA: 0x00174CC8 File Offset: 0x00173CC8
		// (set) Token: 0x06005CF7 RID: 23799 RVA: 0x00174CD0 File Offset: 0x00173CD0
		public string From
		{
			get
			{
				return this.from;
			}
			set
			{
				this.from = value;
			}
		}

		// Token: 0x170017D8 RID: 6104
		// (get) Token: 0x06005CF8 RID: 23800 RVA: 0x00174CD9 File Offset: 0x00173CD9
		// (set) Token: 0x06005CF9 RID: 23801 RVA: 0x00174CE1 File Offset: 0x00173CE1
		public string To
		{
			get
			{
				return this.to;
			}
			set
			{
				this.to = value;
			}
		}

		// Token: 0x170017D9 RID: 6105
		// (get) Token: 0x06005CFA RID: 23802 RVA: 0x00174CEA File Offset: 0x00173CEA
		// (set) Token: 0x06005CFB RID: 23803 RVA: 0x00174CF2 File Offset: 0x00173CF2
		public string Cc
		{
			get
			{
				return this.cc;
			}
			set
			{
				this.cc = value;
			}
		}

		// Token: 0x170017DA RID: 6106
		// (get) Token: 0x06005CFC RID: 23804 RVA: 0x00174CFB File Offset: 0x00173CFB
		// (set) Token: 0x06005CFD RID: 23805 RVA: 0x00174D03 File Offset: 0x00173D03
		public string Bcc
		{
			get
			{
				return this.bcc;
			}
			set
			{
				this.bcc = value;
			}
		}

		// Token: 0x170017DB RID: 6107
		// (get) Token: 0x06005CFE RID: 23806 RVA: 0x00174D0C File Offset: 0x00173D0C
		// (set) Token: 0x06005CFF RID: 23807 RVA: 0x00174D14 File Offset: 0x00173D14
		public string Subject
		{
			get
			{
				return this.subject;
			}
			set
			{
				this.subject = value;
			}
		}

		// Token: 0x170017DC RID: 6108
		// (get) Token: 0x06005D00 RID: 23808 RVA: 0x00174D1D File Offset: 0x00173D1D
		// (set) Token: 0x06005D01 RID: 23809 RVA: 0x00174D25 File Offset: 0x00173D25
		public MailPriority Priority
		{
			get
			{
				return this.priority;
			}
			set
			{
				this.priority = value;
			}
		}

		// Token: 0x170017DD RID: 6109
		// (get) Token: 0x06005D02 RID: 23810 RVA: 0x00174D2E File Offset: 0x00173D2E
		// (set) Token: 0x06005D03 RID: 23811 RVA: 0x00174D36 File Offset: 0x00173D36
		public string UrlContentBase
		{
			get
			{
				return this.urlContentBase;
			}
			set
			{
				this.urlContentBase = value;
			}
		}

		// Token: 0x170017DE RID: 6110
		// (get) Token: 0x06005D04 RID: 23812 RVA: 0x00174D3F File Offset: 0x00173D3F
		// (set) Token: 0x06005D05 RID: 23813 RVA: 0x00174D47 File Offset: 0x00173D47
		public string UrlContentLocation
		{
			get
			{
				return this.urlContentLocation;
			}
			set
			{
				this.urlContentLocation = value;
			}
		}

		// Token: 0x170017DF RID: 6111
		// (get) Token: 0x06005D06 RID: 23814 RVA: 0x00174D50 File Offset: 0x00173D50
		// (set) Token: 0x06005D07 RID: 23815 RVA: 0x00174D58 File Offset: 0x00173D58
		public string Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = value;
			}
		}

		// Token: 0x170017E0 RID: 6112
		// (get) Token: 0x06005D08 RID: 23816 RVA: 0x00174D61 File Offset: 0x00173D61
		// (set) Token: 0x06005D09 RID: 23817 RVA: 0x00174D69 File Offset: 0x00173D69
		public MailFormat BodyFormat
		{
			get
			{
				return this.bodyFormat;
			}
			set
			{
				this.bodyFormat = value;
			}
		}

		// Token: 0x170017E1 RID: 6113
		// (get) Token: 0x06005D0A RID: 23818 RVA: 0x00174D72 File Offset: 0x00173D72
		// (set) Token: 0x06005D0B RID: 23819 RVA: 0x00174D7A File Offset: 0x00173D7A
		public Encoding BodyEncoding
		{
			get
			{
				return this.bodyEncoding;
			}
			set
			{
				this.bodyEncoding = value;
			}
		}

		// Token: 0x170017E2 RID: 6114
		// (get) Token: 0x06005D0C RID: 23820 RVA: 0x00174D83 File Offset: 0x00173D83
		public IDictionary Headers
		{
			get
			{
				return this._headers;
			}
		}

		// Token: 0x170017E3 RID: 6115
		// (get) Token: 0x06005D0D RID: 23821 RVA: 0x00174D8B File Offset: 0x00173D8B
		public IDictionary Fields
		{
			get
			{
				return this._fields;
			}
		}

		// Token: 0x170017E4 RID: 6116
		// (get) Token: 0x06005D0E RID: 23822 RVA: 0x00174D93 File Offset: 0x00173D93
		public IList Attachments
		{
			get
			{
				return this._attachments;
			}
		}

		// Token: 0x040031AC RID: 12716
		private Hashtable _headers = new Hashtable();

		// Token: 0x040031AD RID: 12717
		private Hashtable _fields = new Hashtable();

		// Token: 0x040031AE RID: 12718
		private ArrayList _attachments = new ArrayList();

		// Token: 0x040031AF RID: 12719
		private string from;

		// Token: 0x040031B0 RID: 12720
		private string to;

		// Token: 0x040031B1 RID: 12721
		private string cc;

		// Token: 0x040031B2 RID: 12722
		private string bcc;

		// Token: 0x040031B3 RID: 12723
		private string subject;

		// Token: 0x040031B4 RID: 12724
		private MailPriority priority;

		// Token: 0x040031B5 RID: 12725
		private string urlContentBase;

		// Token: 0x040031B6 RID: 12726
		private string urlContentLocation;

		// Token: 0x040031B7 RID: 12727
		private string body;

		// Token: 0x040031B8 RID: 12728
		private MailFormat bodyFormat;

		// Token: 0x040031B9 RID: 12729
		private Encoding bodyEncoding = Encoding.Default;
	}
}
