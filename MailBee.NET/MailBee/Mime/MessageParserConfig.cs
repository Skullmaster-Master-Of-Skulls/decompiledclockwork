using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using a.i;

namespace MailBee.Mime
{
	// Token: 0x02000562 RID: 1378
	public class MessageParserConfig
	{
		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06002D5E RID: 11614 RVA: 0x000DAE80 File Offset: 0x000D9E80
		// (set) Token: 0x06002D5F RID: 11615 RVA: 0x000DAE88 File Offset: 0x000D9E88
		internal MailMessage Message
		{
			get
			{
				return this.a;
			}
			set
			{
				this.a = value;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06002D60 RID: 11616 RVA: 0x000DAE91 File Offset: 0x000D9E91
		// (set) Token: 0x06002D61 RID: 11617 RVA: 0x000DAE99 File Offset: 0x000D9E99
		public AHRefTagAttributes AHRefCleanup
		{
			get
			{
				return this.c;
			}
			set
			{
				this.c = value;
				this.b = true;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06002D62 RID: 11618 RVA: 0x000DAEA9 File Offset: 0x000D9EA9
		// (set) Token: 0x06002D63 RID: 11619 RVA: 0x000DAEB1 File Offset: 0x000D9EB1
		public string AHRefSuffix
		{
			get
			{
				return this.d;
			}
			set
			{
				this.d = value;
				this.b = true;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06002D64 RID: 11620 RVA: 0x000DAEC1 File Offset: 0x000D9EC1
		// (set) Token: 0x06002D65 RID: 11621 RVA: 0x000DAEC9 File Offset: 0x000D9EC9
		public HtmlMessageAutoSaving AutoSaveHtmlMode
		{
			get
			{
				return this.e;
			}
			set
			{
				this.e = value;
				this.b = true;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06002D66 RID: 11622 RVA: 0x000DAED9 File Offset: 0x000D9ED9
		public StringConversionConfig CharsetConverter
		{
			get
			{
				return this.f;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06002D67 RID: 11623 RVA: 0x000DAEE1 File Offset: 0x000D9EE1
		// (set) Token: 0x06002D68 RID: 11624 RVA: 0x000DAEE9 File Offset: 0x000D9EE9
		public CharsetMetaTagProcessing CharsetMetaTagMode
		{
			get
			{
				return this.g;
			}
			set
			{
				this.g = value;
				this.b = true;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06002D69 RID: 11625 RVA: 0x000DAEF9 File Offset: 0x000D9EF9
		// (set) Token: 0x06002D6A RID: 11626 RVA: 0x000DAF01 File Offset: 0x000D9F01
		public bool FixCrLf
		{
			get
			{
				return this.h;
			}
			set
			{
				this.h = value;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06002D6B RID: 11627 RVA: 0x000DAF0A File Offset: 0x000D9F0A
		// (set) Token: 0x06002D6C RID: 11628 RVA: 0x000DAF12 File Offset: 0x000D9F12
		public bool DatesAsUtc
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

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06002D6D RID: 11629 RVA: 0x000DAF1B File Offset: 0x000D9F1B
		// (set) Token: 0x06002D6E RID: 11630 RVA: 0x000DAF23 File Offset: 0x000D9F23
		public Encoding EncodingDefault
		{
			get
			{
				return this.j;
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				this.j = value;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06002D6F RID: 11631 RVA: 0x000DAF37 File Offset: 0x000D9F37
		// (set) Token: 0x06002D70 RID: 11632 RVA: 0x000DAF3F File Offset: 0x000D9F3F
		public Encoding EncodingOverride
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

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06002D71 RID: 11633 RVA: 0x000DAF48 File Offset: 0x000D9F48
		// (set) Token: 0x06002D72 RID: 11634 RVA: 0x000DAF50 File Offset: 0x000D9F50
		public bool HeadersAsHtml
		{
			get
			{
				return this.l;
			}
			set
			{
				this.l = value;
				this.b = true;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06002D73 RID: 11635 RVA: 0x000DAF60 File Offset: 0x000D9F60
		// (set) Token: 0x06002D74 RID: 11636 RVA: 0x000DAF68 File Offset: 0x000D9F68
		internal bool HeadersAsHtmlInternal
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

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06002D75 RID: 11637 RVA: 0x000DAF71 File Offset: 0x000D9F71
		// (set) Token: 0x06002D76 RID: 11638 RVA: 0x000DAF79 File Offset: 0x000D9F79
		public HtmlToPlainAutoConvert HtmlToPlainMode
		{
			get
			{
				return this.m;
			}
			set
			{
				this.m = value;
				this.b = true;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06002D77 RID: 11639 RVA: 0x000DAF89 File Offset: 0x000D9F89
		// (set) Token: 0x06002D78 RID: 11640 RVA: 0x000DAF91 File Offset: 0x000D9F91
		public HtmlToPlainConvertOptions HtmlToPlainOptions
		{
			get
			{
				return this.n;
			}
			set
			{
				this.n = value;
				this.b = true;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06002D79 RID: 11641 RVA: 0x000DAFA1 File Offset: 0x000D9FA1
		// (set) Token: 0x06002D7A RID: 11642 RVA: 0x000DAFA9 File Offset: 0x000D9FA9
		public HtmlToSimpleHtmlAutoConvert HtmlToSimpleHtmlMode
		{
			get
			{
				return this.o;
			}
			set
			{
				this.o = value;
				this.b = true;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06002D7B RID: 11643 RVA: 0x000DAFB9 File Offset: 0x000D9FB9
		// (set) Token: 0x06002D7C RID: 11644 RVA: 0x000DAFC1 File Offset: 0x000D9FC1
		public HtmlToSimpleHtmlConvertOptions HtmlToSimpleHtmlOptions
		{
			get
			{
				return this.p;
			}
			set
			{
				this.p = value;
				this.b = true;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06002D7D RID: 11645 RVA: 0x000DAFD1 File Offset: 0x000D9FD1
		// (set) Token: 0x06002D7E RID: 11646 RVA: 0x000DAFD9 File Offset: 0x000D9FD9
		public bool ParseHeaderOnly
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

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06002D7F RID: 11647 RVA: 0x000DAFE2 File Offset: 0x000D9FE2
		// (set) Token: 0x06002D80 RID: 11648 RVA: 0x000DAFEA File Offset: 0x000D9FEA
		public PlainToHtmlAutoConvert PlainToHtmlMode
		{
			get
			{
				return this.r;
			}
			set
			{
				this.r = value;
				this.b = true;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06002D81 RID: 11649 RVA: 0x000DAFFA File Offset: 0x000D9FFA
		// (set) Token: 0x06002D82 RID: 11650 RVA: 0x000DB002 File Offset: 0x000DA002
		public PlainToHtmlConvertOptions PlainToHtmlOptions
		{
			get
			{
				return this.s;
			}
			set
			{
				this.s = value;
				this.b = true;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06002D83 RID: 11651 RVA: 0x000DB012 File Offset: 0x000DA012
		// (set) Token: 0x06002D84 RID: 11652 RVA: 0x000DB01A File Offset: 0x000DA01A
		public string PlainToHtmlQuotationTag
		{
			get
			{
				return this.t;
			}
			set
			{
				this.t = value;
				this.b = true;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06002D85 RID: 11653 RVA: 0x000DB02A File Offset: 0x000DA02A
		// (set) Token: 0x06002D86 RID: 11654 RVA: 0x000DB032 File Offset: 0x000DA032
		public string WorkingFolder
		{
			get
			{
				return this.u;
			}
			set
			{
				this.u = value;
				this.b = true;
			}
		}

		// Token: 0x06002D87 RID: 11655 RVA: 0x000DB044 File Offset: 0x000DA044
		internal MessageParserConfig(MailMessage A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x000DB0AC File Offset: 0x000DA0AC
		public void Apply()
		{
			this.a.NeedToReparse = false;
			if (this.a.RawBody.e() <= 0)
			{
				return;
			}
			this.a.r();
			this.a.NeedToRebuild = this.b;
			this.b = false;
			global::a.i.e.a(this.m, this.n, this.a);
			global::a.i.e.a(this.o, this.p, this.a);
			if (this.o == HtmlToSimpleHtmlAutoConvert.Never || (this.o == HtmlToSimpleHtmlAutoConvert.IfHtml && this.a.BodyHtmlText != null && this.a.BodyHtmlText.Length == 0))
			{
				global::a.i.e.a(this.r, this.s, this.a, this.t);
			}
			global::a.i.e.a(this.c, this.d, this.a);
			global::a.i.e.a(this.a, this.g);
			global::a.i.e.a(this.e, this.a, this);
			if ((this.a.MimePart.SubParts == null || this.a.MimePart.SubParts.Count == 0) && this.a.MimePart.Headers["Content-Type"] == null)
			{
				string text = this.a.MimePart.PartValueAsString;
				Regex regex = global::a.i.m.h;
				Regex regex2 = global::a.i.m.i;
				Match match = null;
				Match match2 = regex.Match(text);
				int num = 0;
				if (match2.Success)
				{
					this.a.MimePart.Headers.Add("Content-Type", "text/plain", false);
					this.a.MimePart.PartValueAsString = string.Empty;
					while (match2.Success)
					{
						MimePart mimePart = this.a.MimePart;
						mimePart.PartValueAsString += text.Substring(num, match2.Index - num);
						match = regex2.Match(text, match2.Index + match2.Length);
						if (!match.Success)
						{
							string text2 = text.Substring(match2.Index + match2.Length + 2);
							byte[] data = global::a.i.h.a(MailTransferEncoding.Uue, Encoding.ASCII.GetBytes(text2));
							string value = match2.Groups["filename"].Value;
							this.a.Attachments.Add(data, value, null, global::a.i.k.e(Path.GetExtension(value)), null, NewAttachmentOptions.None, MailTransferEncoding.Base64);
							break;
						}
						string text3 = text.Substring(match2.Index + match2.Length + 2, match.Index - (match2.Index + match2.Length + 2));
						byte[] data2 = global::a.i.h.a(MailTransferEncoding.Uue, Encoding.ASCII.GetBytes(text3));
						string value2 = match2.Groups["filename"].Value;
						this.a.Attachments.Add(data2, value2, null, global::a.i.k.e(Path.GetExtension(value2)), null, NewAttachmentOptions.None, MailTransferEncoding.Base64);
						num = match.Index + match.Length;
						match2 = match2.NextMatch();
					}
					if (match.Success)
					{
						MimePart mimePart2 = this.a.MimePart;
						mimePart2.PartValueAsString += text.Substring(num);
					}
				}
			}
			foreach (string name in new string[]
			{
				"Bcc",
				"Cc",
				"Reply-To",
				"To"
			})
			{
				HeaderCollection headerCollection = this.a.Headers.Items(name);
				if (headerCollection != null && headerCollection.Count > 1 && headerCollection[0].AddressCollection != null)
				{
					for (int j = 1; j < headerCollection.Count; j++)
					{
						if (headerCollection[j].AddressCollection != null)
						{
							headerCollection[0].AddressCollection.Add(headerCollection[j].AddressCollection);
							this.a.Headers.a(headerCollection[j]);
						}
					}
				}
			}
		}

		// Token: 0x17000567 RID: 1383
		// (set) Token: 0x06002D89 RID: 11657 RVA: 0x000DB4BF File Offset: 0x000DA4BF
		internal string MessageFolderInternal
		{
			set
			{
				this.v = value;
			}
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x000DB4C8 File Offset: 0x000DA4C8
		public string GetMessageFolder()
		{
			return this.v;
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x000DB4D0 File Offset: 0x000DA4D0
		public string GetMessageIDHash(string messageID)
		{
			if (messageID == null)
			{
				messageID = this.a.MessageID;
			}
			return global::a.i.e.b(messageID);
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x000DB4E8 File Offset: 0x000DA4E8
		public void Reset()
		{
			this.c = AHRefTagAttributes.None;
			this.d = string.Empty;
			this.e = HtmlMessageAutoSaving.NoAutoSave;
			this.f = new StringConversionConfig();
			this.g = CharsetMetaTagProcessing.SetCorrectCharset;
			this.h = false;
			this.l = false;
			this.m = HtmlToPlainAutoConvert.IfNoPlain;
			this.n = HtmlToPlainConvertOptions.None;
			this.o = HtmlToSimpleHtmlAutoConvert.Never;
			this.p = HtmlToSimpleHtmlConvertOptions.None;
			this.q = false;
			this.r = PlainToHtmlAutoConvert.IfNoHtml;
			this.s = PlainToHtmlConvertOptions.None;
			this.t = string.Empty;
			this.u = string.Empty;
		}

		// Token: 0x06002D8D RID: 11661 RVA: 0x000DB575 File Offset: 0x000DA575
		public void SetHtmlOutputMode()
		{
			this.r = PlainToHtmlAutoConvert.IfNoHtml;
			this.m = HtmlToPlainAutoConvert.Never;
			this.o = HtmlToSimpleHtmlAutoConvert.Never;
			this.l = true;
		}

		// Token: 0x06002D8E RID: 11662 RVA: 0x000DB593 File Offset: 0x000DA593
		public void SetPlainOutputMode()
		{
			this.r = PlainToHtmlAutoConvert.Never;
			this.m = HtmlToPlainAutoConvert.IfNoPlain;
			this.o = HtmlToSimpleHtmlAutoConvert.Never;
			this.l = false;
		}

		// Token: 0x06002D8F RID: 11663 RVA: 0x000DB5B4 File Offset: 0x000DA5B4
		internal MessageParserConfig a(MailMessage A_0)
		{
			return new MessageParserConfig(A_0)
			{
				c = this.c,
				d = this.d,
				e = this.e,
				f = this.f.a(),
				g = this.g,
				i = this.i,
				j = this.j,
				k = this.k,
				l = this.l,
				m = this.m,
				n = this.n,
				o = this.o,
				p = this.p,
				v = this.v,
				q = this.q,
				r = this.r,
				s = this.s,
				t = this.t,
				u = this.u
			};
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06002D90 RID: 11664 RVA: 0x000DB6B0 File Offset: 0x000DA6B0
		// (set) Token: 0x06002D91 RID: 11665 RVA: 0x000DB6B8 File Offset: 0x000DA6B8
		public bool WriteUtf8ByteOrderMark
		{
			get
			{
				return this.w;
			}
			set
			{
				this.w = value;
			}
		}

		// Token: 0x04001F73 RID: 8051
		private MailMessage a;

		// Token: 0x04001F74 RID: 8052
		private bool b;

		// Token: 0x04001F75 RID: 8053
		private AHRefTagAttributes c;

		// Token: 0x04001F76 RID: 8054
		private string d = string.Empty;

		// Token: 0x04001F77 RID: 8055
		private HtmlMessageAutoSaving e;

		// Token: 0x04001F78 RID: 8056
		private StringConversionConfig f = new StringConversionConfig();

		// Token: 0x04001F79 RID: 8057
		private CharsetMetaTagProcessing g;

		// Token: 0x04001F7A RID: 8058
		private bool h;

		// Token: 0x04001F7B RID: 8059
		private bool i;

		// Token: 0x04001F7C RID: 8060
		private Encoding j = Global.DefaultEncoding;

		// Token: 0x04001F7D RID: 8061
		private Encoding k;

		// Token: 0x04001F7E RID: 8062
		private bool l;

		// Token: 0x04001F7F RID: 8063
		private HtmlToPlainAutoConvert m = HtmlToPlainAutoConvert.Never;

		// Token: 0x04001F80 RID: 8064
		private HtmlToPlainConvertOptions n;

		// Token: 0x04001F81 RID: 8065
		private HtmlToSimpleHtmlAutoConvert o = HtmlToSimpleHtmlAutoConvert.Never;

		// Token: 0x04001F82 RID: 8066
		private HtmlToSimpleHtmlConvertOptions p;

		// Token: 0x04001F83 RID: 8067
		private bool q;

		// Token: 0x04001F84 RID: 8068
		private PlainToHtmlAutoConvert r = PlainToHtmlAutoConvert.Never;

		// Token: 0x04001F85 RID: 8069
		private PlainToHtmlConvertOptions s;

		// Token: 0x04001F86 RID: 8070
		private string t = string.Empty;

		// Token: 0x04001F87 RID: 8071
		private string u = string.Empty;

		// Token: 0x04001F88 RID: 8072
		private string v;

		// Token: 0x04001F89 RID: 8073
		private bool w;
	}
}
