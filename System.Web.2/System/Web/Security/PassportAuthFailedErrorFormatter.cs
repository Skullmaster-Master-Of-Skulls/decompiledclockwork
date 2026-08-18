using System;

namespace System.Web.Security
{
	// Token: 0x020005ED RID: 1517
	internal class PassportAuthFailedErrorFormatter : ErrorFormatter
	{
		// Token: 0x17001684 RID: 5764
		// (get) Token: 0x06004C73 RID: 19571 RVA: 0x00105348 File Offset: 0x00103548
		protected override string ErrorTitle
		{
			get
			{
				return SR.GetString("PassportAuthFailed_Title");
			}
		}

		// Token: 0x17001685 RID: 5765
		// (get) Token: 0x06004C74 RID: 19572 RVA: 0x00105354 File Offset: 0x00103554
		protected override string Description
		{
			get
			{
				return SR.GetString("PassportAuthFailed_Description");
			}
		}

		// Token: 0x17001686 RID: 5766
		// (get) Token: 0x06004C75 RID: 19573 RVA: 0x00101100 File Offset: 0x000FF300
		protected override string MiscSectionTitle
		{
			get
			{
				return SR.GetString("Assess_Denied_Title");
			}
		}

		// Token: 0x17001687 RID: 5767
		// (get) Token: 0x06004C76 RID: 19574 RVA: 0x0000298D File Offset: 0x00000B8D
		protected override string MiscSectionContent
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001688 RID: 5768
		// (get) Token: 0x06004C77 RID: 19575 RVA: 0x0000298D File Offset: 0x00000B8D
		protected override string ColoredSquareTitle
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001689 RID: 5769
		// (get) Token: 0x06004C78 RID: 19576 RVA: 0x0000298D File Offset: 0x00000B8D
		protected override string ColoredSquareContent
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700168A RID: 5770
		// (get) Token: 0x06004C79 RID: 19577 RVA: 0x00007722 File Offset: 0x00005922
		protected override bool ShowSourceFileInfo
		{
			get
			{
				return false;
			}
		}
	}
}
