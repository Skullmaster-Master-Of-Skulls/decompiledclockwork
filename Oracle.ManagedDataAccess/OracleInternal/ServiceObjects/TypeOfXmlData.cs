using System;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001BF RID: 447
	internal enum TypeOfXmlData
	{
		// Token: 0x04001392 RID: 5010
		NoData,
		// Token: 0x04001393 RID: 5011
		String = 2,
		// Token: 0x04001394 RID: 5012
		Clob = 4,
		// Token: 0x04001395 RID: 5013
		Chars = 16,
		// Token: 0x04001396 RID: 5014
		XmlDoc = 32,
		// Token: 0x04001397 RID: 5015
		StringAndXmlDoc = 34,
		// Token: 0x04001398 RID: 5016
		ClobAndString = 6,
		// Token: 0x04001399 RID: 5017
		BlobWithText = 64,
		// Token: 0x0400139A RID: 5018
		BlobWithTextAndString = 66,
		// Token: 0x0400139B RID: 5019
		BlobCSX = 128,
		// Token: 0x0400139C RID: 5020
		BlobCSXAndString = 130
	}
}
