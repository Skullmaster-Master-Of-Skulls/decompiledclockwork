using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009DE RID: 2526
	internal interface ICompressedMessageEncoder
	{
		// Token: 0x17001814 RID: 6164
		// (get) Token: 0x060063C5 RID: 25541
		bool CompressionEnabled { get; }

		// Token: 0x060063C6 RID: 25542
		void SetSessionContentType(string contentType);

		// Token: 0x060063C7 RID: 25543
		void AddCompressedMessageProperties(Message message, string supportedCompressionTypes);
	}
}
