using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000789 RID: 1929
	internal static class EncoderDefaults
	{
		// Token: 0x06004994 RID: 18836 RVA: 0x0010E9DE File Offset: 0x0010CBDE
		internal static bool IsDefaultReaderQuotas(XmlDictionaryReaderQuotas quotas)
		{
			return quotas.ModifiedQuotas == (XmlDictionaryReaderQuotaTypes)0;
		}

		// Token: 0x04002E42 RID: 11842
		internal const int MaxReadPoolSize = 64;

		// Token: 0x04002E43 RID: 11843
		internal const int MaxWritePoolSize = 16;

		// Token: 0x04002E44 RID: 11844
		internal const int MaxDepth = 32;

		// Token: 0x04002E45 RID: 11845
		internal const int MaxStringContentLength = 8192;

		// Token: 0x04002E46 RID: 11846
		internal const int MaxArrayLength = 16384;

		// Token: 0x04002E47 RID: 11847
		internal const int MaxBytesPerRead = 4096;

		// Token: 0x04002E48 RID: 11848
		internal const int MaxNameTableCharCount = 16384;

		// Token: 0x04002E49 RID: 11849
		internal const int BufferedReadDefaultMaxDepth = 128;

		// Token: 0x04002E4A RID: 11850
		internal const int BufferedReadDefaultMaxStringContentLength = 2147483647;

		// Token: 0x04002E4B RID: 11851
		internal const int BufferedReadDefaultMaxArrayLength = 2147483647;

		// Token: 0x04002E4C RID: 11852
		internal const int BufferedReadDefaultMaxBytesPerRead = 2147483647;

		// Token: 0x04002E4D RID: 11853
		internal const int BufferedReadDefaultMaxNameTableCharCount = 2147483647;

		// Token: 0x04002E4E RID: 11854
		internal const CompressionFormat DefaultCompressionFormat = CompressionFormat.None;

		// Token: 0x04002E4F RID: 11855
		internal static readonly XmlDictionaryReaderQuotas ReaderQuotas = new XmlDictionaryReaderQuotas();
	}
}
