using System;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace System.Web
{
	// Token: 0x0200005B RID: 91
	internal class PageForbiddenErrorFormatter : ErrorFormatter
	{
		// Token: 0x06000629 RID: 1577 RVA: 0x000097BA File Offset: 0x000079BA
		internal PageForbiddenErrorFormatter(string url) : this(url, null)
		{
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x000097C4 File Offset: 0x000079C4
		internal PageForbiddenErrorFormatter(string url, string description)
		{
			this._htmlEncodedUrl = HttpUtility.HtmlEncode(url);
			this._adaptiveMiscContent.Add(this._htmlEncodedUrl);
			this._description = description;
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x000097FC File Offset: 0x000079FC
		protected override string ErrorTitle
		{
			get
			{
				return SR.GetString("Forbidden_Type_Not_Served");
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x00009808 File Offset: 0x00007A08
		protected override string Description
		{
			get
			{
				if (this._description != null)
				{
					return this._description;
				}
				Match match = Regex.Match(this._htmlEncodedUrl, "\\.\\w+$");
				string text = string.Empty;
				if (match.Success)
				{
					text = SR.GetString("Forbidden_Extension_Incorrect", new object[]
					{
						match.ToString()
					});
				}
				return HttpUtility.FormatPlainTextAsHtml(SR.GetString("Forbidden_Extension_Desc", new object[]
				{
					text
				}));
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x0000979B File Offset: 0x0000799B
		protected override string MiscSectionTitle
		{
			get
			{
				return SR.GetString("NotFound_Requested_Url");
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x00009876 File Offset: 0x00007A76
		protected override string MiscSectionContent
		{
			get
			{
				return this._htmlEncodedUrl;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x0000987E File Offset: 0x00007A7E
		protected override StringCollection AdaptiveMiscContent
		{
			get
			{
				return this._adaptiveMiscContent;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x00007722 File Offset: 0x00005922
		protected override bool ShowSourceFileInfo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x000097B7 File Offset: 0x000079B7
		internal override bool CanBeShownToAllUsers
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0400017A RID: 378
		protected string _htmlEncodedUrl;

		// Token: 0x0400017B RID: 379
		private StringCollection _adaptiveMiscContent = new StringCollection();

		// Token: 0x0400017C RID: 380
		private string _description;
	}
}
