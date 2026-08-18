using System;

namespace System.Web.Security
{
	// Token: 0x020005D3 RID: 1491
	internal sealed class AuthStoreErrorFormatter : ErrorFormatter
	{
		// Token: 0x06004B80 RID: 19328 RVA: 0x000099DF File Offset: 0x00007BDF
		internal AuthStoreErrorFormatter()
		{
		}

		// Token: 0x06004B81 RID: 19329 RVA: 0x00100E78 File Offset: 0x000FF078
		internal static string GetErrorText()
		{
			if (AuthStoreErrorFormatter.s_errMsg != null)
			{
				return AuthStoreErrorFormatter.s_errMsg;
			}
			object obj = AuthStoreErrorFormatter.s_Lock;
			lock (obj)
			{
				if (AuthStoreErrorFormatter.s_errMsg != null)
				{
					return AuthStoreErrorFormatter.s_errMsg;
				}
				AuthStoreErrorFormatter authStoreErrorFormatter = new AuthStoreErrorFormatter();
				AuthStoreErrorFormatter.s_errMsg = authStoreErrorFormatter.GetErrorMessage();
			}
			return AuthStoreErrorFormatter.s_errMsg;
		}

		// Token: 0x17001637 RID: 5687
		// (get) Token: 0x06004B82 RID: 19330 RVA: 0x00100EE8 File Offset: 0x000FF0E8
		protected override string ErrorTitle
		{
			get
			{
				return SR.GetString("AuthStoreNotInstalled_Title");
			}
		}

		// Token: 0x17001638 RID: 5688
		// (get) Token: 0x06004B83 RID: 19331 RVA: 0x00100EF4 File Offset: 0x000FF0F4
		protected override string Description
		{
			get
			{
				return SR.GetString("AuthStoreNotInstalled_Description");
			}
		}

		// Token: 0x17001639 RID: 5689
		// (get) Token: 0x06004B84 RID: 19332 RVA: 0x0000298D File Offset: 0x00000B8D
		protected override string MiscSectionTitle
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700163A RID: 5690
		// (get) Token: 0x06004B85 RID: 19333 RVA: 0x0000298D File Offset: 0x00000B8D
		protected override string MiscSectionContent
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700163B RID: 5691
		// (get) Token: 0x06004B86 RID: 19334 RVA: 0x0000298D File Offset: 0x00000B8D
		protected override string ColoredSquareTitle
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700163C RID: 5692
		// (get) Token: 0x06004B87 RID: 19335 RVA: 0x0000298D File Offset: 0x00000B8D
		protected override string ColoredSquareContent
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700163D RID: 5693
		// (get) Token: 0x06004B88 RID: 19336 RVA: 0x00007722 File Offset: 0x00005922
		protected override bool ShowSourceFileInfo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040028B3 RID: 10419
		private static string s_errMsg = null;

		// Token: 0x040028B4 RID: 10420
		private static object s_Lock = new object();
	}
}
