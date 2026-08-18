using System;

namespace System.Web
{
	// Token: 0x0200005C RID: 92
	internal class GenericApplicationErrorFormatter : ErrorFormatter
	{
		// Token: 0x06000632 RID: 1586 RVA: 0x00009886 File Offset: 0x00007A86
		internal GenericApplicationErrorFormatter(bool local)
		{
			this._local = local;
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x00009895 File Offset: 0x00007A95
		protected override string ErrorTitle
		{
			get
			{
				return SR.GetString("Generic_Err_Title");
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x000098A1 File Offset: 0x00007AA1
		protected override string Description
		{
			get
			{
				return SR.GetString(this._local ? "Generic_Err_Local_Desc" : "Generic_Err_Remote_Desc");
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x0000298D File Offset: 0x00000B8D
		protected override string MiscSectionTitle
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0000298D File Offset: 0x00000B8D
		protected override string MiscSectionContent
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x000098BC File Offset: 0x00007ABC
		protected override string ColoredSquareTitle
		{
			get
			{
				string @string = SR.GetString("Generic_Err_Details_Title");
				this.AdaptiveMiscContent.Add(@string);
				return @string;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x000098E4 File Offset: 0x00007AE4
		protected override string ColoredSquareDescription
		{
			get
			{
				string text = SR.GetString(this._local ? "Generic_Err_Local_Details_Desc" : "Generic_Err_Remote_Details_Desc");
				text = HttpUtility.HtmlEncode(text);
				this.AdaptiveMiscContent.Add(text);
				return text;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x00009920 File Offset: 0x00007B20
		protected override string ColoredSquareContent
		{
			get
			{
				string content = HttpUtility.HtmlEncode(SR.GetString(this._local ? "Generic_Err_Local_Details_Sample" : "Generic_Err_Remote_Details_Sample"));
				return base.WrapWithLeftToRightTextFormatIfNeeded(content);
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x00009954 File Offset: 0x00007B54
		protected override string ColoredSquare2Title
		{
			get
			{
				string @string = SR.GetString("Generic_Err_Notes_Title");
				this.AdaptiveMiscContent.Add(@string);
				return @string;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x0000997C File Offset: 0x00007B7C
		protected override string ColoredSquare2Description
		{
			get
			{
				string text = SR.GetString("Generic_Err_Notes_Desc");
				text = HttpUtility.HtmlEncode(text);
				this.AdaptiveMiscContent.Add(text);
				return text;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x000099AC File Offset: 0x00007BAC
		protected override string ColoredSquare2Content
		{
			get
			{
				string content = HttpUtility.HtmlEncode(SR.GetString(this._local ? "Generic_Err_Local_Notes_Sample" : "Generic_Err_Remote_Notes_Sample"));
				return base.WrapWithLeftToRightTextFormatIfNeeded(content);
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x00007722 File Offset: 0x00005922
		protected override bool ShowSourceFileInfo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x000097B7 File Offset: 0x000079B7
		internal override bool CanBeShownToAllUsers
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0400017D RID: 381
		private bool _local;
	}
}
