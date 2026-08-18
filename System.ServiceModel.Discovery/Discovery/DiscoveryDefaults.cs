using System;
using System.Globalization;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200001C RID: 28
	internal static class DiscoveryDefaults
	{
		// Token: 0x0400005E RID: 94
		public static readonly TimeSpan DiscoveryOperationDuration = TimeSpan.Parse("00:00:20", CultureInfo.InvariantCulture);

		// Token: 0x0400005F RID: 95
		public static readonly Uri ScopeMatchBy = FindCriteria.ScopeMatchByPrefix;

		// Token: 0x04000060 RID: 96
		public const string DiscoveryOperationDurationString = "00:00:20";

		// Token: 0x04000061 RID: 97
		public const int DuplicateMessageHistoryLength = 2056;

		// Token: 0x020000CD RID: 205
		public static class Udp
		{
			// Token: 0x060007E6 RID: 2022 RVA: 0x00014BB4 File Offset: 0x00012DB4
			public static UdpTransportBindingElement CreateUdpTransportBindingElement()
			{
				return new UdpTransportBindingElement
				{
					RetransmissionSettings = 
					{
						MaxUnicastRetransmitCount = 1,
						MaxMulticastRetransmitCount = 2,
						DelayLowerBound = TimeSpan.FromMilliseconds(50.0),
						DelayUpperBound = TimeSpan.FromMilliseconds(250.0),
						MaxDelayPerRetransmission = TimeSpan.FromMilliseconds(500.0)
					},
					DuplicateMessageHistoryLength = 4112,
					ManualAddressing = true
				};
			}

			// Token: 0x040001F6 RID: 502
			public static readonly Uri IPv4MulticastAddress = new Uri("soap.udp://239.255.255.250:3702");

			// Token: 0x040001F7 RID: 503
			public static readonly Uri IPv6MulticastAddress = new Uri("soap.udp://[FF02::C]:3702");

			// Token: 0x040001F8 RID: 504
			public static readonly TimeSpan AppMaxDelay = TimeSpan.Parse("00:00:00.500", CultureInfo.InvariantCulture);

			// Token: 0x040001F9 RID: 505
			public const string AppMaxDelayString = "00:00:00.500";

			// Token: 0x040001FA RID: 506
			public const int DuplicateMessageHistoryLength = 4112;

			// Token: 0x040001FB RID: 507
			public const int MaxUnicastRetransmitCount = 1;

			// Token: 0x040001FC RID: 508
			public const int MaxMulticastRetransmitCount = 2;
		}
	}
}
