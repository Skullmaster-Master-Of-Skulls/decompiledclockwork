using System;
using System.Globalization;
using System.IO;
using a;

namespace MailBee.Mime
{
	// Token: 0x02000569 RID: 1385
	public class TextBodyPart
	{
		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06002DE8 RID: 11752 RVA: 0x000DD7DD File Offset: 0x000DC7DD
		public MimePart AsMimePart
		{
			get
			{
				if (this.e != null && !this.a.n())
				{
					this.a.PartValueAsBytes = bb.a(this.Charset).GetBytes(this.e);
				}
				return this.a;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06002DE9 RID: 11753 RVA: 0x000DD81B File Offset: 0x000DC81B
		public bool IsICalendar
		{
			get
			{
				return this.AsMimePart.ContentType != null && this.AsMimePart.ContentType.ToLower().StartsWith("text/calendar");
			}
		}

		// Token: 0x06002DEA RID: 11754 RVA: 0x000DD846 File Offset: 0x000DC846
		public TextReader GetAsTextReader()
		{
			return new StringReader(this.Text);
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06002DEB RID: 11755 RVA: 0x000DD853 File Offset: 0x000DC853
		// (set) Token: 0x06002DEC RID: 11756 RVA: 0x000DD85B File Offset: 0x000DC85B
		internal bool IsCollectionMember
		{
			get
			{
				return this.b;
			}
			set
			{
				this.b = value;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06002DED RID: 11757 RVA: 0x000DD864 File Offset: 0x000DC864
		public bool IsOriginal
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06002DEE RID: 11758 RVA: 0x000DD86C File Offset: 0x000DC86C
		// (set) Token: 0x06002DEF RID: 11759 RVA: 0x000DD874 File Offset: 0x000DC874
		internal TextBodyPartCollection RootCollection
		{
			get
			{
				return this.d;
			}
			set
			{
				this.d = value;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06002DF0 RID: 11760 RVA: 0x000DD87D File Offset: 0x000DC87D
		// (set) Token: 0x06002DF1 RID: 11761 RVA: 0x000DD885 File Offset: 0x000DC885
		public string Charset
		{
			get
			{
				return this.CharsetInternal;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				this.CharsetInternal = value;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06002DF2 RID: 11762 RVA: 0x000DD898 File Offset: 0x000DC898
		// (set) Token: 0x06002DF3 RID: 11763 RVA: 0x000DD8A5 File Offset: 0x000DC8A5
		internal string CharsetInternal
		{
			get
			{
				return this.a.CharsetInternal;
			}
			set
			{
				this.a.CharsetInternal = value;
				this.a.d();
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06002DF4 RID: 11764 RVA: 0x000DD8BE File Offset: 0x000DC8BE
		// (set) Token: 0x06002DF5 RID: 11765 RVA: 0x000DD8CB File Offset: 0x000DC8CB
		public MailTransferEncoding TransferEncoding
		{
			get
			{
				return this.a.MimePartTransferEncoding;
			}
			set
			{
				this.a.MimePartTransferEncoding = value;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06002DF6 RID: 11766 RVA: 0x000DD8D9 File Offset: 0x000DC8D9
		public HeaderCollection Headers
		{
			get
			{
				return this.a.Headers;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06002DF7 RID: 11767 RVA: 0x000DD8E6 File Offset: 0x000DC8E6
		// (set) Token: 0x06002DF8 RID: 11768 RVA: 0x000DD8F3 File Offset: 0x000DC8F3
		internal bool NeedToRebuild
		{
			get
			{
				return this.a.NeedToRebuild;
			}
			set
			{
				this.a.NeedToRebuild = value;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06002DF9 RID: 11769 RVA: 0x000DD904 File Offset: 0x000DC904
		internal bool IsPlain
		{
			get
			{
				string text = this.AsMimePart.ContentType;
				if (text != null)
				{
					text = text.ToLower();
				}
				return text == "text/plain";
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06002DFA RID: 11770 RVA: 0x000DD932 File Offset: 0x000DC932
		// (set) Token: 0x06002DFB RID: 11771 RVA: 0x000DD954 File Offset: 0x000DC954
		public string Text
		{
			get
			{
				if (this.e == null)
				{
					this.e = this.a.PartValueAsString;
				}
				return this.e;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (!this.b && this.d != null)
				{
					if (this.IsPlain)
					{
						this.d.c(this);
					}
					else
					{
						this.d.Add(this);
					}
				}
				this.e = value;
				this.a.d();
				this.NeedToRebuild = true;
			}
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x000DD9B8 File Offset: 0x000DC9B8
		internal void a(byte[] A_0)
		{
			this.a.PartValueAsBytes = A_0;
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x000DD9C6 File Offset: 0x000DC9C6
		internal TextBodyPart(string A_0) : this(A_0, null, false)
		{
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x000DD9D4 File Offset: 0x000DC9D4
		internal TextBodyPart(string A_0, HeaderCollection A_1, bool A_2)
		{
			this.a = new MimePart(null);
			if (this.a.Headers["Content-Type"] == null)
			{
				Header header = Header.a(string.Format(CultureInfo.InvariantCulture, "{0}: {1}", new object[]
				{
					"Content-Type",
					A_0
				}));
				if (header != null)
				{
					this.a.Headers.b(header);
				}
			}
			else
			{
				this.a.ContentTypeHeader.Value = A_0;
			}
			if (this.a.Headers["Content-Transfer-Encoding"] == null)
			{
				this.a.Headers.Add("Content-Transfer-Encoding", "quoted-printable", false);
				this.a.MimePartTransferEncoding = MailTransferEncoding.QuotedPrintable;
			}
			if (A_2)
			{
				this.a.Headers.Clear();
			}
			if (A_1 != null)
			{
				this.a.Headers.a(A_1);
			}
		}

		// Token: 0x06002DFF RID: 11775 RVA: 0x000DDABF File Offset: 0x000DCABF
		internal TextBodyPart(MimePart A_0, bool A_1)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("part == null");
			}
			this.a = A_0;
			this.c = A_1;
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x000DDAE3 File Offset: 0x000DCAE3
		public TextBodyPart(MimePart src) : this(src, false)
		{
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x000DDAF0 File Offset: 0x000DCAF0
		internal TextBodyPart d()
		{
			TextBodyPart textBodyPart = new TextBodyPart(this.Headers["Content-Type"], null, true);
			foreach (object obj in this.Headers)
			{
				Header a_ = (Header)obj;
				textBodyPart.Headers.b(a_);
			}
			textBodyPart.Charset = this.Charset;
			textBodyPart.Text = this.Text;
			textBodyPart.TransferEncoding = this.TransferEncoding;
			return textBodyPart;
		}

		// Token: 0x04001FAA RID: 8106
		private MimePart a;

		// Token: 0x04001FAB RID: 8107
		private bool b;

		// Token: 0x04001FAC RID: 8108
		private bool c;

		// Token: 0x04001FAD RID: 8109
		private TextBodyPartCollection d;

		// Token: 0x04001FAE RID: 8110
		private string e;
	}
}
