using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009DF RID: 2527
	internal interface ITransportCompressionSupport
	{
		// Token: 0x060063C8 RID: 25544
		bool IsCompressionFormatSupported(CompressionFormat compressionFormat);
	}
}
