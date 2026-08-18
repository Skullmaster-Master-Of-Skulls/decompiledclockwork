using System;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000C4 RID: 196
	internal static class COR20Constants
	{
		// Token: 0x0400053B RID: 1339
		internal const int SizeOfCorHeader = 72;

		// Token: 0x0400053C RID: 1340
		internal const uint COR20MetadataSignature = 1112167234U;

		// Token: 0x0400053D RID: 1341
		internal const int MinimumSizeofMetadataHeader = 16;

		// Token: 0x0400053E RID: 1342
		internal const int SizeofStorageHeader = 4;

		// Token: 0x0400053F RID: 1343
		internal const int MinimumSizeofStreamHeader = 8;

		// Token: 0x04000540 RID: 1344
		internal const string StringStreamName = "#Strings";

		// Token: 0x04000541 RID: 1345
		internal const string BlobStreamName = "#Blob";

		// Token: 0x04000542 RID: 1346
		internal const string GUIDStreamName = "#GUID";

		// Token: 0x04000543 RID: 1347
		internal const string UserStringStreamName = "#US";

		// Token: 0x04000544 RID: 1348
		internal const string CompressedMetadataTableStreamName = "#~";

		// Token: 0x04000545 RID: 1349
		internal const string UncompressedMetadataTableStreamName = "#-";

		// Token: 0x04000546 RID: 1350
		internal const string MinimalDeltaMetadataTableStreamName = "#JTD";

		// Token: 0x04000547 RID: 1351
		internal const string StandalonePdbStreamName = "#Pdb";

		// Token: 0x04000548 RID: 1352
		internal const int LargeStreamHeapSize = 4096;
	}
}
