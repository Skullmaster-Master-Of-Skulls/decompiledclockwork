using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200078C RID: 1932
	internal static class BinaryEncoderDefaults
	{
		// Token: 0x17001283 RID: 4739
		// (get) Token: 0x0600499A RID: 18842 RVA: 0x0010EBBE File Offset: 0x0010CDBE
		internal static EnvelopeVersion EnvelopeVersion
		{
			get
			{
				return EnvelopeVersion.Soap12;
			}
		}

		// Token: 0x17001284 RID: 4740
		// (get) Token: 0x0600499B RID: 18843 RVA: 0x0010EBC5 File Offset: 0x0010CDC5
		internal static BinaryVersion BinaryVersion
		{
			get
			{
				return BinaryVersion.Version1;
			}
		}

		// Token: 0x04002E56 RID: 11862
		internal const int MaxSessionSize = 2048;
	}
}
