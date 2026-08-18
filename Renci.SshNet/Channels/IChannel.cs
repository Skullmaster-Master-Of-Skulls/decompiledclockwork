using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Channels
{
	// Token: 0x02000109 RID: 265
	internal interface IChannel : IDisposable
	{
		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06000B43 RID: 2883
		// (remove) Token: 0x06000B44 RID: 2884
		event EventHandler<ChannelDataEventArgs> DataReceived;

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x06000B45 RID: 2885
		// (remove) Token: 0x06000B46 RID: 2886
		event EventHandler<ExceptionEventArgs> Exception;

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06000B47 RID: 2887
		// (remove) Token: 0x06000B48 RID: 2888
		event EventHandler<ChannelExtendedDataEventArgs> ExtendedDataReceived;

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06000B49 RID: 2889
		// (remove) Token: 0x06000B4A RID: 2890
		event EventHandler<ChannelRequestEventArgs> RequestReceived;

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06000B4B RID: 2891
		// (remove) Token: 0x06000B4C RID: 2892
		event EventHandler<ChannelEventArgs> Closed;

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000B4D RID: 2893
		uint LocalChannelNumber { get; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000B4E RID: 2894
		uint LocalPacketSize { get; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000B4F RID: 2895
		uint RemotePacketSize { get; }

		// Token: 0x06000B50 RID: 2896
		void Close();

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000B51 RID: 2897
		bool IsOpen { get; }

		// Token: 0x06000B52 RID: 2898
		void SendData(byte[] data);

		// Token: 0x06000B53 RID: 2899
		void SendData(byte[] data, int offset, int size);

		// Token: 0x06000B54 RID: 2900
		void SendEof();
	}
}
