using System;

namespace System.Web.Security
{
	// Token: 0x020005DC RID: 1500
	internal class FileAccessFailedErrorFormatter : ErrorFormatter
	{
		// Token: 0x06004BC1 RID: 19393 RVA: 0x001021F5 File Offset: 0x001003F5
		internal FileAccessFailedErrorFormatter(string strFile)
		{
			this._strFile = strFile;
			if (this._strFile == null)
			{
				this._strFile = string.Empty;
			}
		}

		// Token: 0x17001646 RID: 5702
		// (get) Token: 0x06004BC2 RID: 19394 RVA: 0x00101100 File Offset: 0x000FF300
		protected override string ErrorTitle
		{
			get
			{
				return SR.GetString("Assess_Denied_Title");
			}
		}

		// Token: 0x17001647 RID: 5703
		// (get) Token: 0x06004BC3 RID: 19395 RVA: 0x00102217 File Offset: 0x00100417
		protected override string Description
		{
			get
			{
				return SR.GetString("Assess_Denied_Description3");
			}
		}

		// Token: 0x17001648 RID: 5704
		// (get) Token: 0x06004BC4 RID: 19396 RVA: 0x00102223 File Offset: 0x00100423
		protected override string MiscSectionTitle
		{
			get
			{
				return SR.GetString("Assess_Denied_Section_Title3");
			}
		}

		// Token: 0x17001649 RID: 5705
		// (get) Token: 0x06004BC5 RID: 19397 RVA: 0x00102230 File Offset: 0x00100430
		protected override string MiscSectionContent
		{
			get
			{
				string @string;
				if (this._strFile.Length > 0)
				{
					@string = SR.GetString("Assess_Denied_Misc_Content3", new object[]
					{
						HttpRuntime.GetSafePath(this._strFile)
					});
				}
				else
				{
					@string = SR.GetString("Assess_Denied_Misc_Content3_2");
				}
				this.AdaptiveMiscContent.Add(@string);
				return @string;
			}
		}

		// Token: 0x1700164A RID: 5706
		// (get) Token: 0x06004BC6 RID: 19398 RVA: 0x0000298D File Offset: 0x00000B8D
		protected override string ColoredSquareTitle
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700164B RID: 5707
		// (get) Token: 0x06004BC7 RID: 19399 RVA: 0x0000298D File Offset: 0x00000B8D
		protected override string ColoredSquareContent
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700164C RID: 5708
		// (get) Token: 0x06004BC8 RID: 19400 RVA: 0x00007722 File Offset: 0x00005922
		protected override bool ShowSourceFileInfo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040028D0 RID: 10448
		private string _strFile;
	}
}
