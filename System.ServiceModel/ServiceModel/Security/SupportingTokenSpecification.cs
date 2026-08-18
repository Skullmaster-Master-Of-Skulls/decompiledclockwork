using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Tokens;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x020002CA RID: 714
	public class SupportingTokenSpecification : SecurityTokenSpecification
	{
		// Token: 0x06001709 RID: 5897 RVA: 0x000573DE File Offset: 0x000555DE
		public SupportingTokenSpecification(SecurityToken token, ReadOnlyCollection<IAuthorizationPolicy> tokenPolicies, SecurityTokenAttachmentMode attachmentMode) : this(token, tokenPolicies, attachmentMode, null)
		{
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x000573EA File Offset: 0x000555EA
		public SupportingTokenSpecification(SecurityToken token, ReadOnlyCollection<IAuthorizationPolicy> tokenPolicies, SecurityTokenAttachmentMode attachmentMode, SecurityTokenParameters tokenParameters) : base(token, tokenPolicies)
		{
			SecurityTokenAttachmentModeHelper.Validate(attachmentMode);
			this.tokenAttachmentMode = attachmentMode;
			this.tokenParameters = tokenParameters;
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x0600170B RID: 5899 RVA: 0x00057409 File Offset: 0x00055609
		public SecurityTokenAttachmentMode SecurityTokenAttachmentMode
		{
			get
			{
				return this.tokenAttachmentMode;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x0600170C RID: 5900 RVA: 0x00057411 File Offset: 0x00055611
		internal SecurityTokenParameters SecurityTokenParameters
		{
			get
			{
				return this.tokenParameters;
			}
		}

		// Token: 0x04001C0D RID: 7181
		private SecurityTokenAttachmentMode tokenAttachmentMode;

		// Token: 0x04001C0E RID: 7182
		private SecurityTokenParameters tokenParameters;
	}
}
