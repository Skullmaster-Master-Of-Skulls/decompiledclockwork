using System;
using System.Collections.ObjectModel;

namespace System.ServiceModel.Security
{
	// Token: 0x020002C3 RID: 707
	internal struct MergedSupportingTokenAuthenticatorSpecification
	{
		// Token: 0x04001BF8 RID: 7160
		public Collection<SupportingTokenAuthenticatorSpecification> SupportingTokenAuthenticators;

		// Token: 0x04001BF9 RID: 7161
		public bool ExpectSignedTokens;

		// Token: 0x04001BFA RID: 7162
		public bool ExpectEndorsingTokens;

		// Token: 0x04001BFB RID: 7163
		public bool ExpectBasicTokens;
	}
}
