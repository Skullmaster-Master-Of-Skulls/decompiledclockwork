using System;
using System.Threading;
using Renci.SshNet.Channels;
using Renci.SshNet.Common;
using Renci.SshNet.Messages;
using Renci.SshNet.Messages.Authentication;
using Renci.SshNet.Messages.Connection;

namespace Renci.SshNet
{
	// Token: 0x02000014 RID: 20
	internal interface ISession : IDisposable
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000CC RID: 204
		IConnectionInfo ConnectionInfo { get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000CD RID: 205
		bool IsConnected { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000CE RID: 206
		SemaphoreLight SessionSemaphore { get; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000CF RID: 207
		WaitHandle MessageListenerCompleted { get; }

		// Token: 0x060000D0 RID: 208
		void Connect();

		// Token: 0x060000D1 RID: 209
		IChannelSession CreateChannelSession();

		// Token: 0x060000D2 RID: 210
		IChannelDirectTcpip CreateChannelDirectTcpip();

		// Token: 0x060000D3 RID: 211
		IChannelForwardedTcpip CreateChannelForwardedTcpip(uint remoteChannelNumber, uint remoteWindowSize, uint remoteChannelDataPacketSize);

		// Token: 0x060000D4 RID: 212
		void Disconnect();

		// Token: 0x060000D5 RID: 213
		void OnDisconnecting();

		// Token: 0x060000D6 RID: 214
		void RegisterMessage(string messageName);

		// Token: 0x060000D7 RID: 215
		void SendMessage(Message message);

		// Token: 0x060000D8 RID: 216
		bool TrySendMessage(Message message);

		// Token: 0x060000D9 RID: 217
		void UnRegisterMessage(string messageName);

		// Token: 0x060000DA RID: 218
		void WaitOnHandle(WaitHandle waitHandle);

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060000DB RID: 219
		// (remove) Token: 0x060000DC RID: 220
		event EventHandler<MessageEventArgs<ChannelCloseMessage>> ChannelCloseReceived;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060000DD RID: 221
		// (remove) Token: 0x060000DE RID: 222
		event EventHandler<MessageEventArgs<ChannelDataMessage>> ChannelDataReceived;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060000DF RID: 223
		// (remove) Token: 0x060000E0 RID: 224
		event EventHandler<MessageEventArgs<ChannelEofMessage>> ChannelEofReceived;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060000E1 RID: 225
		// (remove) Token: 0x060000E2 RID: 226
		event EventHandler<MessageEventArgs<ChannelExtendedDataMessage>> ChannelExtendedDataReceived;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060000E3 RID: 227
		// (remove) Token: 0x060000E4 RID: 228
		event EventHandler<MessageEventArgs<ChannelFailureMessage>> ChannelFailureReceived;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060000E5 RID: 229
		// (remove) Token: 0x060000E6 RID: 230
		event EventHandler<MessageEventArgs<ChannelOpenConfirmationMessage>> ChannelOpenConfirmationReceived;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060000E7 RID: 231
		// (remove) Token: 0x060000E8 RID: 232
		event EventHandler<MessageEventArgs<ChannelOpenFailureMessage>> ChannelOpenFailureReceived;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060000E9 RID: 233
		// (remove) Token: 0x060000EA RID: 234
		event EventHandler<MessageEventArgs<ChannelOpenMessage>> ChannelOpenReceived;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060000EB RID: 235
		// (remove) Token: 0x060000EC RID: 236
		event EventHandler<MessageEventArgs<ChannelRequestMessage>> ChannelRequestReceived;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060000ED RID: 237
		// (remove) Token: 0x060000EE RID: 238
		event EventHandler<MessageEventArgs<ChannelSuccessMessage>> ChannelSuccessReceived;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060000EF RID: 239
		// (remove) Token: 0x060000F0 RID: 240
		event EventHandler<MessageEventArgs<ChannelWindowAdjustMessage>> ChannelWindowAdjustReceived;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060000F1 RID: 241
		// (remove) Token: 0x060000F2 RID: 242
		event EventHandler<EventArgs> Disconnected;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060000F3 RID: 243
		// (remove) Token: 0x060000F4 RID: 244
		event EventHandler<ExceptionEventArgs> ErrorOccured;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060000F5 RID: 245
		// (remove) Token: 0x060000F6 RID: 246
		event EventHandler<HostKeyEventArgs> HostKeyReceived;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060000F7 RID: 247
		// (remove) Token: 0x060000F8 RID: 248
		event EventHandler<MessageEventArgs<RequestSuccessMessage>> RequestSuccessReceived;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060000F9 RID: 249
		// (remove) Token: 0x060000FA RID: 250
		event EventHandler<MessageEventArgs<RequestFailureMessage>> RequestFailureReceived;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060000FB RID: 251
		// (remove) Token: 0x060000FC RID: 252
		event EventHandler<MessageEventArgs<BannerMessage>> UserAuthenticationBannerReceived;
	}
}
