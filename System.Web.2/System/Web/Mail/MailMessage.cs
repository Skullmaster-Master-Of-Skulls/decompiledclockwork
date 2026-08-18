using System;
using System.Collections;
using System.Text;

namespace System.Web.Mail
{
	// Token: 0x0200011B RID: 283
	[Obsolete("The recommended alternative is System.Net.Mail.MailMessage. http://go.microsoft.com/fwlink/?linkid=14202")]
	public class MailMessage
	{
		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x000308F4 File Offset: 0x0002EAF4
		// (set) Token: 0x06001155 RID: 4437 RVA: 0x000308FC File Offset: 0x0002EAFC
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

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x00030905 File Offset: 0x0002EB05
		// (set) Token: 0x06001157 RID: 4439 RVA: 0x0003090D File Offset: 0x0002EB0D
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

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x00030916 File Offset: 0x0002EB16
		// (set) Token: 0x06001159 RID: 4441 RVA: 0x0003091E File Offset: 0x0002EB1E
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

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x00030927 File Offset: 0x0002EB27
		// (set) Token: 0x0600115B RID: 4443 RVA: 0x0003092F File Offset: 0x0002EB2F
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

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x00030938 File Offset: 0x0002EB38
		// (set) Token: 0x0600115D RID: 4445 RVA: 0x00030940 File Offset: 0x0002EB40
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

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x00030949 File Offset: 0x0002EB49
		// (set) Token: 0x0600115F RID: 4447 RVA: 0x00030951 File Offset: 0x0002EB51
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

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x0003095A File Offset: 0x0002EB5A
		// (set) Token: 0x06001161 RID: 4449 RVA: 0x00030962 File Offset: 0x0002EB62
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

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x0003096B File Offset: 0x0002EB6B
		// (set) Token: 0x06001163 RID: 4451 RVA: 0x00030973 File Offset: 0x0002EB73
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

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x0003097C File Offset: 0x0002EB7C
		// (set) Token: 0x06001165 RID: 4453 RVA: 0x00030984 File Offset: 0x0002EB84
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

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x0003098D File Offset: 0x0002EB8D
		// (set) Token: 0x06001167 RID: 4455 RVA: 0x00030995 File Offset: 0x0002EB95
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

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x0003099E File Offset: 0x0002EB9E
		// (set) Token: 0x06001169 RID: 4457 RVA: 0x000309A6 File Offset: 0x0002EBA6
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

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x000309AF File Offset: 0x0002EBAF
		public IDictionary Headers
		{
			get
			{
				return this._headers;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x000309B7 File Offset: 0x0002EBB7
		public IDictionary Fields
		{
			get
			{
				return this._fields;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x0600116C RID: 4460 RVA: 0x000309BF File Offset: 0x0002EBBF
		public IList Attachments
		{
			get
			{
				return this._attachments;
			}
		}

		// Token: 0x040013CB RID: 5067
		private Hashtable _headers = new Hashtable();

		// Token: 0x040013CC RID: 5068
		private Hashtable _fields = new Hashtable();

		// Token: 0x040013CD RID: 5069
		private ArrayList _attachments = new ArrayList();

		// Token: 0x040013CE RID: 5070
		private string from;

		// Token: 0x040013CF RID: 5071
		private string to;

		// Token: 0x040013D0 RID: 5072
		private string cc;

		// Token: 0x040013D1 RID: 5073
		private string bcc;

		// Token: 0x040013D2 RID: 5074
		private string subject;

		// Token: 0x040013D3 RID: 5075
		private MailPriority priority;

		// Token: 0x040013D4 RID: 5076
		private string urlContentBase;

		// Token: 0x040013D5 RID: 5077
		private string urlContentLocation;

		// Token: 0x040013D6 RID: 5078
		private string body;

		// Token: 0x040013D7 RID: 5079
		private MailFormat bodyFormat;

		// Token: 0x040013D8 RID: 5080
		private Encoding bodyEncoding = Encoding.Default;
	}
}
