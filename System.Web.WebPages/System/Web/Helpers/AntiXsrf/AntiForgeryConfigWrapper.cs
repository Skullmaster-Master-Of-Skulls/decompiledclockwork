using System;

namespace System.Web.Helpers.AntiXsrf
{
	// Token: 0x0200002C RID: 44
	internal sealed class AntiForgeryConfigWrapper : IAntiForgeryConfig
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00004AFB File Offset: 0x00002CFB
		public IAntiForgeryAdditionalDataProvider AdditionalDataProvider
		{
			get
			{
				return AntiForgeryConfig.AdditionalDataProvider;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00004B02 File Offset: 0x00002D02
		public string CookieName
		{
			get
			{
				return AntiForgeryConfig.CookieName;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00004B09 File Offset: 0x00002D09
		public string FormFieldName
		{
			get
			{
				return "__RequestVerificationToken";
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00004B10 File Offset: 0x00002D10
		public bool RequireSSL
		{
			get
			{
				return AntiForgeryConfig.RequireSsl;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00004B17 File Offset: 0x00002D17
		public bool SuppressIdentityHeuristicChecks
		{
			get
			{
				return AntiForgeryConfig.SuppressIdentityHeuristicChecks;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00004B1E File Offset: 0x00002D1E
		public string UniqueClaimTypeIdentifier
		{
			get
			{
				return AntiForgeryConfig.UniqueClaimTypeIdentifier;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00004B25 File Offset: 0x00002D25
		public bool SuppressXFrameOptionsHeader
		{
			get
			{
				return AntiForgeryConfig.SuppressXFrameOptionsHeader;
			}
		}
	}
}
