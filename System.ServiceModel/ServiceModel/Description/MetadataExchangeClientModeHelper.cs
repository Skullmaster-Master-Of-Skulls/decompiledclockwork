using System;
using System.ComponentModel;

namespace System.ServiceModel.Description
{
	// Token: 0x020003E5 RID: 997
	internal static class MetadataExchangeClientModeHelper
	{
		// Token: 0x060025A8 RID: 9640 RVA: 0x00087535 File Offset: 0x00085735
		public static bool IsDefined(MetadataExchangeClientMode x)
		{
			return x == MetadataExchangeClientMode.MetadataExchange || x == MetadataExchangeClientMode.HttpGet;
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x00087540 File Offset: 0x00085740
		public static void Validate(MetadataExchangeClientMode value)
		{
			if (!MetadataExchangeClientModeHelper.IsDefined(value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, typeof(MetadataExchangeClientMode)));
			}
		}
	}
}
