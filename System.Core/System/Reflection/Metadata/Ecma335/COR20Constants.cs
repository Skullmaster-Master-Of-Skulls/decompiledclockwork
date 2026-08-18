using System;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200006B RID: 107
	internal static class COR20Constants
	{
		// Token: 0x040003A0 RID: 928
		internal const int SizeOfCorHeader = 72;

		// Token: 0x040003A1 RID: 929
		internal const uint COR20MetadataSignature = 1112167234U;

		// Token: 0x040003A2 RID: 930
		internal const int MinimumSizeofMetadataHeader = 16;

		// Token: 0x040003A3 RID: 931
		internal const int SizeofStorageHeader = 4;

		// Token: 0x040003A4 RID: 932
		internal const int MinimumSizeofStreamHeader = 8;

		// Token: 0x040003A5 RID: 933
		internal const string StringStreamName = "#Strings";

		// Token: 0x040003A6 RID: 934
		internal const string BlobStreamName = "#Blob";

		// Token: 0x040003A7 RID: 935
		internal const string GUIDStreamName = "#GUID";

		// Token: 0x040003A8 RID: 936
		internal const string UserStringStreamName = "#US";

		// Token: 0x040003A9 RID: 937
		internal const string CompressedMetadataTableStreamName = "#~";

		// Token: 0x040003AA RID: 938
		internal const string UncompressedMetadataTableStreamName = "#-";

		// Token: 0x040003AB RID: 939
		internal const string MinimalDeltaMetadataTableStreamName = "#JTD";

		// Token: 0x040003AC RID: 940
		internal const string StandalonePdbStreamName = "#Pdb";

		// Token: 0x040003AD RID: 941
		internal const int LargeStreamHeapSize = 4096;
	}
}
