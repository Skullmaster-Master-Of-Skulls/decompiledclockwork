using System;

namespace System.Web.Handlers
{
	// Token: 0x020001A6 RID: 422
	internal class TraceHandlerErrorFormatter : ErrorFormatter
	{
		// Token: 0x06001631 RID: 5681 RVA: 0x00046395 File Offset: 0x00044595
		internal TraceHandlerErrorFormatter(bool isRemote)
		{
			this._isRemote = isRemote;
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001632 RID: 5682 RVA: 0x000463A4 File Offset: 0x000445A4
		protected override string ErrorTitle
		{
			get
			{
				return SR.GetString("Trace_Error_Title");
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001633 RID: 5683 RVA: 0x000463B0 File Offset: 0x000445B0
		protected override string Description
		{
			get
			{
				if (this._isRemote)
				{
					return SR.GetString("Trace_Error_LocalOnly_Description");
				}
				return HttpUtility.HtmlEncode(SR.GetString("Trace_Error_Enabled_Description"));
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x0000298D File Offset: 0x00000B8D
		protected override string MiscSectionTitle
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001635 RID: 5685 RVA: 0x0000298D File Offset: 0x00000B8D
		protected override string MiscSectionContent
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001636 RID: 5686 RVA: 0x000463D4 File Offset: 0x000445D4
		protected override string ColoredSquareTitle
		{
			get
			{
				string @string = SR.GetString("Generic_Err_Details_Title");
				this.AdaptiveMiscContent.Add(@string);
				return @string;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001637 RID: 5687 RVA: 0x000463FC File Offset: 0x000445FC
		protected override string ColoredSquareDescription
		{
			get
			{
				string text;
				if (this._isRemote)
				{
					text = HttpUtility.HtmlEncode(SR.GetString("Trace_Error_LocalOnly_Details_Desc"));
				}
				else
				{
					text = HttpUtility.HtmlEncode(SR.GetString("Trace_Error_Enabled_Details_Desc"));
				}
				this.AdaptiveMiscContent.Add(text);
				return text;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x00046444 File Offset: 0x00044644
		protected override string ColoredSquareContent
		{
			get
			{
				string content;
				if (this._isRemote)
				{
					content = HttpUtility.HtmlEncode(SR.GetString("Trace_Error_LocalOnly_Details_Sample"));
				}
				else
				{
					content = HttpUtility.HtmlEncode(SR.GetString("Trace_Error_Enabled_Details_Sample"));
				}
				return base.WrapWithLeftToRightTextFormatIfNeeded(content);
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x00007722 File Offset: 0x00005922
		protected override bool ShowSourceFileInfo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x000097B7 File Offset: 0x000079B7
		internal override bool CanBeShownToAllUsers
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0400168B RID: 5771
		private bool _isRemote;
	}
}
