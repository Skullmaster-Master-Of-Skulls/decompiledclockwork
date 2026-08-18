using System;
using System.IdentityModel.Selectors;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x020002C8 RID: 712
	internal class SupportingTokenProviderSpecification
	{
		// Token: 0x060016FD RID: 5885 RVA: 0x000572CC File Offset: 0x000554CC
		public SupportingTokenProviderSpecification(SecurityTokenProvider tokenProvider, SecurityTokenAttachmentMode attachmentMode, SecurityTokenParameters tokenParameters)
		{
			if (tokenProvider == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenProvider");
			}
			SecurityTokenAttachmentModeHelper.Validate(attachmentMode);
			if (tokenParameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenParameters");
			}
			this.tokenProvider = tokenProvider;
			this.tokenAttachmentMode = attachmentMode;
			this.tokenParameters = tokenParameters;
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060016FE RID: 5886 RVA: 0x00057320 File Offset: 0x00055520
		public SecurityTokenProvider TokenProvider
		{
			get
			{
				return this.tokenProvider;
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x00057328 File Offset: 0x00055528
		public SecurityTokenAttachmentMode SecurityTokenAttachmentMode
		{
			get
			{
				return this.tokenAttachmentMode;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x00057330 File Offset: 0x00055530
		public SecurityTokenParameters TokenParameters
		{
			get
			{
				return this.tokenParameters;
			}
		}

		// Token: 0x04001C05 RID: 7173
		private SecurityTokenAttachmentMode tokenAttachmentMode;

		// Token: 0x04001C06 RID: 7174
		private SecurityTokenProvider tokenProvider;

		// Token: 0x04001C07 RID: 7175
		private SecurityTokenParameters tokenParameters;
	}
}
