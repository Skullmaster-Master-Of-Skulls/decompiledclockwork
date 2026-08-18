using System;

namespace System.Web
{
	// Token: 0x0200005D RID: 93
	internal class CustomErrorFailedErrorFormatter : ErrorFormatter
	{
		// Token: 0x0600063F RID: 1599 RVA: 0x000099DF File Offset: 0x00007BDF
		internal CustomErrorFailedErrorFormatter()
		{
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00009895 File Offset: 0x00007A95
		protected override string ErrorTitle
		{
			get
			{
				return SR.GetString("Generic_Err_Title");
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x000099E7 File Offset: 0x00007BE7
		protected override string Description
		{
			get
			{
				return HttpUtility.FormatPlainTextAsHtml(SR.GetString("CustomErrorFailed_Err_Desc"));
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x0000298D File Offset: 0x00000B8D
		protected override string MiscSectionTitle
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x0000298D File Offset: 0x00000B8D
		protected override string MiscSectionContent
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x00007722 File Offset: 0x00005922
		protected override bool ShowSourceFileInfo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x000097B7 File Offset: 0x000079B7
		internal override bool CanBeShownToAllUsers
		{
			get
			{
				return true;
			}
		}
	}
}
