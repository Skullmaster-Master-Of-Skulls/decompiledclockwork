using System;

namespace System.Web.Handlers
{
	// Token: 0x0200027E RID: 638
	internal class TraceHandlerErrorFormatter : ErrorFormatter
	{
		// Token: 0x06002104 RID: 8452 RVA: 0x000911DD File Offset: 0x000901DD
		internal TraceHandlerErrorFormatter(bool isRemote)
		{
			this._isRemote = isRemote;
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06002105 RID: 8453 RVA: 0x000911EC File Offset: 0x000901EC
		protected override string ErrorTitle
		{
			get
			{
				return SR.GetString("Trace_Error_Title");
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06002106 RID: 8454 RVA: 0x000911F8 File Offset: 0x000901F8
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

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06002107 RID: 8455 RVA: 0x0009121C File Offset: 0x0009021C
		protected override string MiscSectionTitle
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06002108 RID: 8456 RVA: 0x0009121F File Offset: 0x0009021F
		protected override string MiscSectionContent
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06002109 RID: 8457 RVA: 0x00091224 File Offset: 0x00090224
		protected override string ColoredSquareTitle
		{
			get
			{
				string @string = SR.GetString("Generic_Err_Details_Title");
				this.AdaptiveMiscContent.Add(@string);
				return @string;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x0600210A RID: 8458 RVA: 0x0009124C File Offset: 0x0009024C
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

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x0600210B RID: 8459 RVA: 0x00091294 File Offset: 0x00090294
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

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x0600210C RID: 8460 RVA: 0x000912D2 File Offset: 0x000902D2
		protected override bool ShowSourceFileInfo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x0600210D RID: 8461 RVA: 0x000912D5 File Offset: 0x000902D5
		internal override bool CanBeShownToAllUsers
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001AED RID: 6893
		private bool _isRemote;
	}
}
