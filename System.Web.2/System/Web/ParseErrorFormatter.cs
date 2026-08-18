using System;
using System.Collections.Specialized;

namespace System.Web
{
	// Token: 0x02000060 RID: 96
	internal class ParseErrorFormatter : FormatterWithFileInfo
	{
		// Token: 0x06000658 RID: 1624 RVA: 0x0000A3E0 File Offset: 0x000085E0
		internal ParseErrorFormatter(HttpParseException e, string virtualPath, string sourceCode, int line, string message) : base(virtualPath, null, sourceCode, line)
		{
			this._excep = e;
			this._message = HttpUtility.FormatPlainTextAsHtml(message);
			this._adaptiveMiscContent.Add(this._message);
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x0000A41E File Offset: 0x0000861E
		protected override Exception Exception
		{
			get
			{
				return this._excep;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x0000A426 File Offset: 0x00008626
		protected override string ErrorTitle
		{
			get
			{
				return SR.GetString("Parser_Error");
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x0000A432 File Offset: 0x00008632
		protected override string Description
		{
			get
			{
				return SR.GetString("Parser_Desc");
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x0000A43E File Offset: 0x0000863E
		protected override string MiscSectionTitle
		{
			get
			{
				return SR.GetString("Parser_Error_Message");
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x0000A44A File Offset: 0x0000864A
		protected override string MiscSectionContent
		{
			get
			{
				return this._message;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x0000A452 File Offset: 0x00008652
		protected override string ColoredSquareTitle
		{
			get
			{
				return SR.GetString("Parser_Source_Error");
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x0000A45E File Offset: 0x0000865E
		protected override StringCollection AdaptiveMiscContent
		{
			get
			{
				return this._adaptiveMiscContent;
			}
		}

		// Token: 0x04000188 RID: 392
		protected string _message;

		// Token: 0x04000189 RID: 393
		private HttpParseException _excep;

		// Token: 0x0400018A RID: 394
		private StringCollection _adaptiveMiscContent = new StringCollection();
	}
}
