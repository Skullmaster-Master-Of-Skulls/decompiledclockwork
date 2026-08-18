using System;
using System.IdentityModel.Selectors;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x020002C9 RID: 713
	internal class SupportingTokenAuthenticatorSpecification
	{
		// Token: 0x06001701 RID: 5889 RVA: 0x00057338 File Offset: 0x00055538
		public SupportingTokenAuthenticatorSpecification(SecurityTokenAuthenticator tokenAuthenticator, SecurityTokenResolver securityTokenResolver, SecurityTokenAttachmentMode attachmentMode, SecurityTokenParameters tokenParameters) : this(tokenAuthenticator, securityTokenResolver, attachmentMode, tokenParameters, false)
		{
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x00057348 File Offset: 0x00055548
		internal SupportingTokenAuthenticatorSpecification(SecurityTokenAuthenticator tokenAuthenticator, SecurityTokenResolver securityTokenResolver, SecurityTokenAttachmentMode attachmentMode, SecurityTokenParameters tokenParameters, bool isTokenOptional)
		{
			if (tokenAuthenticator == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenAuthenticator");
			}
			SecurityTokenAttachmentModeHelper.Validate(attachmentMode);
			if (tokenParameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenParameters");
			}
			this.tokenAuthenticator = tokenAuthenticator;
			this.tokenResolver = securityTokenResolver;
			this.tokenAttachmentMode = attachmentMode;
			this.tokenParameters = tokenParameters;
			this.isTokenOptional = isTokenOptional;
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x000573AD File Offset: 0x000555AD
		public SecurityTokenAuthenticator TokenAuthenticator
		{
			get
			{
				return this.tokenAuthenticator;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001704 RID: 5892 RVA: 0x000573B5 File Offset: 0x000555B5
		public SecurityTokenResolver TokenResolver
		{
			get
			{
				return this.tokenResolver;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x000573BD File Offset: 0x000555BD
		public SecurityTokenAttachmentMode SecurityTokenAttachmentMode
		{
			get
			{
				return this.tokenAttachmentMode;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001706 RID: 5894 RVA: 0x000573C5 File Offset: 0x000555C5
		public SecurityTokenParameters TokenParameters
		{
			get
			{
				return this.tokenParameters;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001707 RID: 5895 RVA: 0x000573CD File Offset: 0x000555CD
		// (set) Token: 0x06001708 RID: 5896 RVA: 0x000573D5 File Offset: 0x000555D5
		internal bool IsTokenOptional
		{
			get
			{
				return this.isTokenOptional;
			}
			set
			{
				this.isTokenOptional = value;
			}
		}

		// Token: 0x04001C08 RID: 7176
		private SecurityTokenAttachmentMode tokenAttachmentMode;

		// Token: 0x04001C09 RID: 7177
		private SecurityTokenAuthenticator tokenAuthenticator;

		// Token: 0x04001C0A RID: 7178
		private SecurityTokenResolver tokenResolver;

		// Token: 0x04001C0B RID: 7179
		private SecurityTokenParameters tokenParameters;

		// Token: 0x04001C0C RID: 7180
		private bool isTokenOptional;
	}
}
