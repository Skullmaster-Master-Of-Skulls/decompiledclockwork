using System;
using System.Collections.Specialized;

namespace System.Web
{
	// Token: 0x0200005A RID: 90
	internal class PageNotFoundErrorFormatter : ErrorFormatter
	{
		// Token: 0x06000621 RID: 1569 RVA: 0x0000974D File Offset: 0x0000794D
		internal PageNotFoundErrorFormatter(string url)
		{
			this._htmlEncodedUrl = HttpUtility.HtmlEncode(url);
			this._adaptiveMiscContent.Add(this._htmlEncodedUrl);
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x0000977E File Offset: 0x0000797E
		protected override string ErrorTitle
		{
			get
			{
				return SR.GetString("NotFound_Resource_Not_Found");
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x0000978A File Offset: 0x0000798A
		protected override string Description
		{
			get
			{
				return HttpUtility.FormatPlainTextAsHtml(SR.GetString("NotFound_Http_404"));
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x0000979B File Offset: 0x0000799B
		protected override string MiscSectionTitle
		{
			get
			{
				return SR.GetString("NotFound_Requested_Url");
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x000097A7 File Offset: 0x000079A7
		protected override string MiscSectionContent
		{
			get
			{
				return this._htmlEncodedUrl;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x000097AF File Offset: 0x000079AF
		protected override StringCollection AdaptiveMiscContent
		{
			get
			{
				return this._adaptiveMiscContent;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x00007722 File Offset: 0x00005922
		protected override bool ShowSourceFileInfo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x000097B7 File Offset: 0x000079B7
		internal override bool CanBeShownToAllUsers
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000178 RID: 376
		protected string _htmlEncodedUrl;

		// Token: 0x04000179 RID: 377
		private StringCollection _adaptiveMiscContent = new StringCollection();
	}
}
