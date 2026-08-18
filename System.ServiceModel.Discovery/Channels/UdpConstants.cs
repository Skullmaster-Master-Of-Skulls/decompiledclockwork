using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000002 RID: 2
	internal static class UdpConstants
	{
		// Token: 0x04000001 RID: 1
		public const int MaxMessageSizeOverIPv4 = 65507;

		// Token: 0x04000002 RID: 2
		public const int MaxTimeToLive = 255;

		// Token: 0x04000003 RID: 3
		public const int MinReceiveBufferSize = 1;

		// Token: 0x04000004 RID: 4
		public const int MinTimeToLive = 0;

		// Token: 0x04000005 RID: 5
		public const int PendingReceiveCountPerProcessor = 2;

		// Token: 0x04000006 RID: 6
		public const string Scheme = "soap.udp";

		// Token: 0x020000C3 RID: 195
		internal static class Defaults
		{
			// Token: 0x040001D5 RID: 469
			public static readonly TimeSpan ReceiveTimeout = TimeSpan.FromMinutes(1.0);

			// Token: 0x040001D6 RID: 470
			public static readonly TimeSpan SendTimeout = TimeSpan.FromMinutes(1.0);

			// Token: 0x040001D7 RID: 471
			public const int DuplicateMessageHistoryLength = 0;

			// Token: 0x040001D8 RID: 472
			public const int InterfaceIndex = -1;

			// Token: 0x040001D9 RID: 473
			public const int MaxPendingMessageCount = 32;

			// Token: 0x040001DA RID: 474
			public const long MaxReceivedMessageSize = 65536L;

			// Token: 0x040001DB RID: 475
			public const int SocketReceiveBufferSize = 65536;

			// Token: 0x040001DC RID: 476
			public const int TimeToLive = 1;

			// Token: 0x040001DD RID: 477
			public static MessageEncoderFactory MessageEncoderFactory = new TextMessageEncodingBindingElement().CreateMessageEncoderFactory();
		}
	}
}
