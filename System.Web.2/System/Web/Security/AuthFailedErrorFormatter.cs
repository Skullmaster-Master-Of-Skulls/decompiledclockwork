using System;

namespace System.Web.Security
{
	// Token: 0x020005D5 RID: 1493
	internal class AuthFailedErrorFormatter : ErrorFormatter
	{
		// Token: 0x06004B93 RID: 19347 RVA: 0x000099DF File Offset: 0x00007BDF
		internal AuthFailedErrorFormatter()
		{
		}

		// Token: 0x06004B94 RID: 19348 RVA: 0x0010109C File Offset: 0x000FF29C
		internal static string GetErrorText()
		{
			if (AuthFailedErrorFormatter._strErrorText != null)
			{
				return AuthFailedErrorFormatter._strErrorText;
			}
			object syncObject = AuthFailedErrorFormatter._syncObject;
			lock (syncObject)
			{
				if (AuthFailedErrorFormatter._strErrorText == null)
				{
					AuthFailedErrorFormatter._strErrorText = new AuthFailedErrorFormatter().GetErrorMessage();
				}
			}
			return AuthFailedErrorFormatter._strErrorText;
		}

		// Token: 0x1700163E RID: 5694
		// (get) Token: 0x06004B95 RID: 19349 RVA: 0x00101100 File Offset: 0x000FF300
		protected override string ErrorTitle
		{
			get
			{
				return SR.GetString("Assess_Denied_Title");
			}
		}

		// Token: 0x1700163F RID: 5695
		// (get) Token: 0x06004B96 RID: 19350 RVA: 0x0010110C File Offset: 0x000FF30C
		protected override string Description
		{
			get
			{
				return SR.GetString("Assess_Denied_Description1");
			}
		}

		// Token: 0x17001640 RID: 5696
		// (get) Token: 0x06004B97 RID: 19351 RVA: 0x00101118 File Offset: 0x000FF318
		protected override string MiscSectionTitle
		{
			get
			{
				return SR.GetString("Assess_Denied_MiscTitle1");
			}
		}

		// Token: 0x17001641 RID: 5697
		// (get) Token: 0x06004B98 RID: 19352 RVA: 0x00101124 File Offset: 0x000FF324
		protected override string MiscSectionContent
		{
			get
			{
				string @string = SR.GetString("Assess_Denied_MiscContent1");
				this.AdaptiveMiscContent.Add(@string);
				return @string;
			}
		}

		// Token: 0x17001642 RID: 5698
		// (get) Token: 0x06004B99 RID: 19353 RVA: 0x0000298D File Offset: 0x00000B8D
		protected override string ColoredSquareTitle
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001643 RID: 5699
		// (get) Token: 0x06004B9A RID: 19354 RVA: 0x0000298D File Offset: 0x00000B8D
		protected override string ColoredSquareContent
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001644 RID: 5700
		// (get) Token: 0x06004B9B RID: 19355 RVA: 0x00007722 File Offset: 0x00005922
		protected override bool ShowSourceFileInfo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040028B6 RID: 10422
		private static string _strErrorText;

		// Token: 0x040028B7 RID: 10423
		private static object _syncObject = new object();
	}
}
