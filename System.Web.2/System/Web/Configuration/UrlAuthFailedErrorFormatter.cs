using System;
using System.Collections.Specialized;

namespace System.Web.Configuration
{
	// Token: 0x02000768 RID: 1896
	internal class UrlAuthFailedErrorFormatter : ErrorFormatter
	{
		// Token: 0x06005B66 RID: 23398 RVA: 0x0013D200 File Offset: 0x0013B400
		internal UrlAuthFailedErrorFormatter()
		{
		}

		// Token: 0x06005B67 RID: 23399 RVA: 0x0013D213 File Offset: 0x0013B413
		internal static string GetErrorText()
		{
			return UrlAuthFailedErrorFormatter.GetErrorText(HttpContext.Current);
		}

		// Token: 0x06005B68 RID: 23400 RVA: 0x0013D220 File Offset: 0x0013B420
		internal static string GetErrorText(HttpContext context)
		{
			bool isCustomErrorEnabled = context.IsCustomErrorEnabled;
			return new UrlAuthFailedErrorFormatter().GetErrorMessage(context, isCustomErrorEnabled);
		}

		// Token: 0x17001ACB RID: 6859
		// (get) Token: 0x06005B69 RID: 23401 RVA: 0x00101100 File Offset: 0x000FF300
		protected override string ErrorTitle
		{
			get
			{
				return SR.GetString("Assess_Denied_Title");
			}
		}

		// Token: 0x17001ACC RID: 6860
		// (get) Token: 0x06005B6A RID: 23402 RVA: 0x0013D240 File Offset: 0x0013B440
		protected override string Description
		{
			get
			{
				return SR.GetString("Assess_Denied_Description2");
			}
		}

		// Token: 0x17001ACD RID: 6861
		// (get) Token: 0x06005B6B RID: 23403 RVA: 0x0013D24C File Offset: 0x0013B44C
		protected override string MiscSectionTitle
		{
			get
			{
				return SR.GetString("Assess_Denied_Section_Title2");
			}
		}

		// Token: 0x17001ACE RID: 6862
		// (get) Token: 0x06005B6C RID: 23404 RVA: 0x0013D258 File Offset: 0x0013B458
		protected override string MiscSectionContent
		{
			get
			{
				string text = HttpUtility.FormatPlainTextAsHtml(SR.GetString("Assess_Denied_Misc_Content2"));
				this.AdaptiveMiscContent.Add(text);
				return text;
			}
		}

		// Token: 0x17001ACF RID: 6863
		// (get) Token: 0x06005B6D RID: 23405 RVA: 0x0000298D File Offset: 0x00000B8D
		protected override string ColoredSquareTitle
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001AD0 RID: 6864
		// (get) Token: 0x06005B6E RID: 23406 RVA: 0x0000298D File Offset: 0x00000B8D
		protected override string ColoredSquareContent
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001AD1 RID: 6865
		// (get) Token: 0x06005B6F RID: 23407 RVA: 0x0013D283 File Offset: 0x0013B483
		protected override StringCollection AdaptiveMiscContent
		{
			get
			{
				return this._adaptiveMiscContent;
			}
		}

		// Token: 0x17001AD2 RID: 6866
		// (get) Token: 0x06005B70 RID: 23408 RVA: 0x00007722 File Offset: 0x00005922
		protected override bool ShowSourceFileInfo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04003038 RID: 12344
		private StringCollection _adaptiveMiscContent = new StringCollection();
	}
}
