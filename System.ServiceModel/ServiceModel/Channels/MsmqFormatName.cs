using System;
using System.Text;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008FC RID: 2300
	internal static class MsmqFormatName
	{
		// Token: 0x060057D6 RID: 22486 RVA: 0x00142A61 File Offset: 0x00140C61
		public static string ToSystemMessagingQueueName(string formatName)
		{
			return "FORMATNAME:" + formatName;
		}

		// Token: 0x060057D7 RID: 22487 RVA: 0x00142A70 File Offset: 0x00140C70
		public static string FromQueuePath(string queuePath)
		{
			int capacity = 256;
			StringBuilder stringBuilder = new StringBuilder(capacity);
			int num = UnsafeNativeMethods.MQPathNameToFormatName(queuePath, stringBuilder, ref capacity);
			if (-1072824289 == num)
			{
				stringBuilder = new StringBuilder(capacity);
				num = UnsafeNativeMethods.MQPathNameToFormatName(queuePath, stringBuilder, ref capacity);
			}
			if (num != 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MsmqException(SR.GetString("MsmqPathLookupError", new object[]
				{
					queuePath,
					MsmqError.GetErrorString(num)
				}), num));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040035F9 RID: 13817
		private const string systemMessagingLabelPrefix = "LABEL:";

		// Token: 0x040035FA RID: 13818
		private const string systemMessagingFormatNamePrefix = "FORMATNAME:";
	}
}
