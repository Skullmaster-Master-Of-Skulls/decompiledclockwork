using System;
using System.IO;
using System.Net.Sockets;
using OracleInternal.Common;

namespace OracleInternal.Network
{
	// Token: 0x02000173 RID: 371
	internal interface ITransportAdapter
	{
		// Token: 0x06000E6F RID: 3695
		void Connect(ConnectionOption conOption);

		// Token: 0x06000E70 RID: 3696
		void Listen(ConnectionOption conOption);

		// Token: 0x06000E71 RID: 3697
		ITransportAdapter Answer(ConnectionOption conOption);

		// Token: 0x06000E72 RID: 3698
		void Disconnect();

		// Token: 0x06000E73 RID: 3699
		Stream GetStream();

		// Token: 0x06000E74 RID: 3700
		Socket GetSocket();

		// Token: 0x06000E75 RID: 3701
		bool UrgentDataSupported();

		// Token: 0x06000E76 RID: 3702
		void SendUrgent(byte[] data, int offset, int length);

		// Token: 0x06000E77 RID: 3703
		void Send(OraBuf OB);

		// Token: 0x06000E78 RID: 3704
		void BeginAsyncReceives(OraBuf.AsyncReceiveCallback Callback, int AsyncBufferSize);

		// Token: 0x170002A7 RID: 679
		// (set) Token: 0x06000E79 RID: 3705
		ConOraBufPool OraBufPool { set; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000E7A RID: 3706
		bool Connected { get; }

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000E7B RID: 3707
		bool NeedReneg { get; }

		// Token: 0x06000E7C RID: 3708
		void Renegotiate(ConnectionOption conOption);
	}
}
