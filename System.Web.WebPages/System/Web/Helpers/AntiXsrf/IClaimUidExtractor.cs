using System;
using System.Security.Principal;

namespace System.Web.Helpers.AntiXsrf
{
	// Token: 0x0200001E RID: 30
	internal interface IClaimUidExtractor
	{
		// Token: 0x060000F4 RID: 244
		BinaryBlob ExtractClaimUid(IIdentity identity);
	}
}
