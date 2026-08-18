using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000778 RID: 1912
	internal static class EncoderHelpers
	{
		// Token: 0x06004911 RID: 18705 RVA: 0x0010D970 File Offset: 0x0010BB70
		internal static XmlDictionaryReaderQuotas GetBufferedReadQuotas(XmlDictionaryReaderQuotas encoderQuotas)
		{
			XmlDictionaryReaderQuotas xmlDictionaryReaderQuotas = new XmlDictionaryReaderQuotas();
			encoderQuotas.CopyTo(xmlDictionaryReaderQuotas);
			if (EncoderHelpers.IsDefaultQuota(xmlDictionaryReaderQuotas, XmlDictionaryReaderQuotaTypes.MaxStringContentLength))
			{
				xmlDictionaryReaderQuotas.MaxStringContentLength = int.MaxValue;
			}
			if (EncoderHelpers.IsDefaultQuota(xmlDictionaryReaderQuotas, XmlDictionaryReaderQuotaTypes.MaxArrayLength))
			{
				xmlDictionaryReaderQuotas.MaxArrayLength = int.MaxValue;
			}
			if (EncoderHelpers.IsDefaultQuota(xmlDictionaryReaderQuotas, XmlDictionaryReaderQuotaTypes.MaxBytesPerRead))
			{
				xmlDictionaryReaderQuotas.MaxBytesPerRead = int.MaxValue;
			}
			if (EncoderHelpers.IsDefaultQuota(xmlDictionaryReaderQuotas, XmlDictionaryReaderQuotaTypes.MaxNameTableCharCount))
			{
				xmlDictionaryReaderQuotas.MaxNameTableCharCount = int.MaxValue;
			}
			if (EncoderHelpers.IsDefaultQuota(xmlDictionaryReaderQuotas, XmlDictionaryReaderQuotaTypes.MaxDepth))
			{
				xmlDictionaryReaderQuotas.MaxDepth = 128;
			}
			return xmlDictionaryReaderQuotas;
		}

		// Token: 0x06004912 RID: 18706 RVA: 0x0010D9F0 File Offset: 0x0010BBF0
		private static bool IsDefaultQuota(XmlDictionaryReaderQuotas quotas, XmlDictionaryReaderQuotaTypes quotaType)
		{
			if (quotaType <= XmlDictionaryReaderQuotaTypes.MaxArrayLength)
			{
				if (quotaType - XmlDictionaryReaderQuotaTypes.MaxDepth > 1 && quotaType != XmlDictionaryReaderQuotaTypes.MaxArrayLength)
				{
					return false;
				}
			}
			else if (quotaType != XmlDictionaryReaderQuotaTypes.MaxBytesPerRead && quotaType != XmlDictionaryReaderQuotaTypes.MaxNameTableCharCount)
			{
				return false;
			}
			return (quotas.ModifiedQuotas & quotaType) == (XmlDictionaryReaderQuotaTypes)0;
		}
	}
}
