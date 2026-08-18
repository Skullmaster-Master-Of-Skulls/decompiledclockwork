using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000495 RID: 1173
	internal enum QueryProcessingError
	{
		// Token: 0x04002466 RID: 9318
		None,
		// Token: 0x04002467 RID: 9319
		Unexpected,
		// Token: 0x04002468 RID: 9320
		TypeMismatch,
		// Token: 0x04002469 RID: 9321
		UnsupportedXmlNodeType,
		// Token: 0x0400246A RID: 9322
		NodeCountMaxExceeded,
		// Token: 0x0400246B RID: 9323
		InvalidXmlAttributes,
		// Token: 0x0400246C RID: 9324
		InvalidNavigatorPosition,
		// Token: 0x0400246D RID: 9325
		NotAtomized,
		// Token: 0x0400246E RID: 9326
		NotSupported,
		// Token: 0x0400246F RID: 9327
		InvalidBodyAccess,
		// Token: 0x04002470 RID: 9328
		InvalidNamespacePrefix
	}
}
