using System;
using System.Globalization;

namespace System.ServiceModel.Security
{
	// Token: 0x020002F0 RID: 752
	internal static class SecuritySessionClientSettings
	{
		// Token: 0x04001C5F RID: 7263
		internal const string defaultKeyRenewalIntervalString = "10:00:00";

		// Token: 0x04001C60 RID: 7264
		internal const string defaultKeyRolloverIntervalString = "00:05:00";

		// Token: 0x04001C61 RID: 7265
		internal static readonly TimeSpan defaultKeyRenewalInterval = TimeSpan.Parse("10:00:00", CultureInfo.InvariantCulture);

		// Token: 0x04001C62 RID: 7266
		internal static readonly TimeSpan defaultKeyRolloverInterval = TimeSpan.Parse("00:05:00", CultureInfo.InvariantCulture);

		// Token: 0x04001C63 RID: 7267
		internal const bool defaultTolerateTransportFailures = true;
	}
}
