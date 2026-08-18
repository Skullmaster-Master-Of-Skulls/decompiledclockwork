using System;

namespace System.ServiceModel.Activation
{
	// Token: 0x020005C9 RID: 1481
	internal static class ListenerConstants
	{
		// Token: 0x040029F8 RID: 10744
		public const int DefaultListenBacklog = 0;

		// Token: 0x040029F9 RID: 10745
		public const int DefaultMaxPendingAccepts = 0;

		// Token: 0x040029FA RID: 10746
		public const int DefaultMaxPendingConnections = 100;

		// Token: 0x040029FB RID: 10747
		public const string DefaultReceiveTimeoutString = "00:00:30";

		// Token: 0x040029FC RID: 10748
		public const bool DefaultTeredoEnabled = false;

		// Token: 0x040029FD RID: 10749
		public const bool DefaultPerformanceCountersEnabled = true;

		// Token: 0x040029FE RID: 10750
		public const int RegistrationMaxConcurrentSessions = 2147483647;

		// Token: 0x040029FF RID: 10751
		public const int RegistrationMaxReceivedMessageSize = 10000;

		// Token: 0x04002A00 RID: 10752
		public static readonly TimeSpan RegistrationCloseTimeout = TimeSpan.FromSeconds(2.0);

		// Token: 0x04002A01 RID: 10753
		public const int SharedConnectionBufferSize = 2500;

		// Token: 0x04002A02 RID: 10754
		public const int SharedMaxDrainSize = 65536;

		// Token: 0x04002A03 RID: 10755
		public static readonly TimeSpan SharedSendTimeout = ServiceDefaults.SendTimeout;

		// Token: 0x04002A04 RID: 10756
		public const int SharedMaxContentTypeSize = 256;

		// Token: 0x04002A05 RID: 10757
		public const int MaxRetries = 5;

		// Token: 0x04002A06 RID: 10758
		public const int MaxUriSize = 2048;

		// Token: 0x04002A07 RID: 10759
		public static readonly TimeSpan ServiceStartTimeout = TimeSpan.FromSeconds(10.0);

		// Token: 0x04002A08 RID: 10760
		public const int ServiceStopTimeout = 30000;

		// Token: 0x04002A09 RID: 10761
		public static readonly TimeSpan WasConnectTimeout = TimeSpan.FromSeconds(120.0);

		// Token: 0x04002A0A RID: 10762
		public const string GlobalPrefix = "Global\\";

		// Token: 0x04002A0B RID: 10763
		public const string MsmqActivationServiceName = "NetMsmqActivator";

		// Token: 0x04002A0C RID: 10764
		public const string NamedPipeActivationServiceName = "NetPipeActivator";

		// Token: 0x04002A0D RID: 10765
		public const string NamedPipeSharedMemoryName = "NetPipeActivator/endpoint";

		// Token: 0x04002A0E RID: 10766
		public const string TcpActivationServiceName = "NetTcpActivator";

		// Token: 0x04002A0F RID: 10767
		public const string TcpPortSharingServiceName = "NetTcpPortSharing";

		// Token: 0x04002A10 RID: 10768
		public const string TcpSharedMemoryName = "NetTcpPortSharing/endpoint";
	}
}
