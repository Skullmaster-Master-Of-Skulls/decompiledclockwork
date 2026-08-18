using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Renci.SshNet.Abstractions;
using Renci.SshNet.Common;
using Renci.SshNet.Messages.Connection;

namespace Renci.SshNet.Channels
{
	// Token: 0x0200010E RID: 270
	internal class ChannelDirectTcpip : ClientChannel, IChannelDirectTcpip, IDisposable
	{
		// Token: 0x06000BB9 RID: 3001 RVA: 0x000263C0 File Offset: 0x000245C0
		public ChannelDirectTcpip(ISession session, uint localChannelNumber, uint localWindowSize, uint localPacketSize) : base(session, localChannelNumber, localWindowSize, localPacketSize)
		{
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x00012C5A File Offset: 0x00010E5A
		public override ChannelTypes ChannelType
		{
			get
			{
				return ChannelTypes.DirectTcpip;
			}
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x000263F0 File Offset: 0x000245F0
		public void Open(string remoteHost, uint port, IForwardedPort forwardedPort, Socket socket)
		{
			if (base.IsOpen)
			{
				throw new SshException("Channel is already open.");
			}
			if (!base.IsConnected)
			{
				throw new SshException("Session is not connected.");
			}
			this._socket = socket;
			this._forwardedPort = forwardedPort;
			this._forwardedPort.Closing += this.ForwardedPort_Closing;
			IPEndPoint ipendPoint = (IPEndPoint)socket.RemoteEndPoint;
			base.SendMessage(new ChannelOpenMessage(base.LocalChannelNumber, base.LocalWindowSize, base.LocalPacketSize, new DirectTcpipChannelInfo(remoteHost, port, ipendPoint.Address.ToString(), (uint)ipendPoint.Port)));
			base.WaitOnHandle(this._channelOpen);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x00026497 File Offset: 0x00024697
		private void ForwardedPort_Closing(object sender, EventArgs eventArgs)
		{
			this.ShutdownSocket(SocketShutdown.Send);
			this.CloseSocket();
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x000264A8 File Offset: 0x000246A8
		public void Bind()
		{
			if (!base.IsOpen)
			{
				return;
			}
			byte[] array = new byte[base.RemotePacketSize];
			SocketAbstraction.ReadContinuous(this._socket, array, 0, array.Length, new Action<byte[], int, int>(this.SendData));
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x000264E8 File Offset: 0x000246E8
		private void CloseSocket()
		{
			if (this._socket == null)
			{
				return;
			}
			object socketLock = this._socketLock;
			lock (socketLock)
			{
				if (this._socket != null)
				{
					this._socket.Dispose();
					this._socket = null;
				}
			}
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x00026548 File Offset: 0x00024748
		private void ShutdownSocket(SocketShutdown how)
		{
			if (this._socket == null)
			{
				return;
			}
			object socketLock = this._socketLock;
			lock (socketLock)
			{
				if (this._socket != null && this._socket.Connected)
				{
					this._socket.Shutdown(how);
				}
			}
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x000265B0 File Offset: 0x000247B0
		protected override void Close(bool wait)
		{
			IForwardedPort forwardedPort = this._forwardedPort;
			if (forwardedPort != null)
			{
				forwardedPort.Closing -= this.ForwardedPort_Closing;
				this._forwardedPort = null;
			}
			this.ShutdownSocket(SocketShutdown.Send);
			base.Close(wait);
			this.CloseSocket();
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x000265F4 File Offset: 0x000247F4
		protected override void OnData(byte[] data)
		{
			base.OnData(data);
			if (this._socket != null && this._socket.Connected)
			{
				object socketLock = this._socketLock;
				lock (socketLock)
				{
					if (this._socket != null && this._socket.Connected)
					{
						SocketAbstraction.Send(this._socket, data, 0, data.Length);
					}
				}
			}
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x00026670 File Offset: 0x00024870
		protected override void OnOpenConfirmation(uint remoteChannelNumber, uint initialWindowSize, uint maximumPacketSize)
		{
			base.OnOpenConfirmation(remoteChannelNumber, initialWindowSize, maximumPacketSize);
			this._channelOpen.Set();
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x00026687 File Offset: 0x00024887
		protected override void OnOpenFailure(uint reasonCode, string description, string language)
		{
			base.OnOpenFailure(reasonCode, description, language);
			this._channelOpen.Set();
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0002669E File Offset: 0x0002489E
		protected override void OnEof()
		{
			base.OnEof();
			this.ShutdownSocket(SocketShutdown.Send);
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x000266AD File Offset: 0x000248AD
		protected override void OnErrorOccured(Exception exp)
		{
			base.OnErrorOccured(exp);
			this.ShutdownSocket(SocketShutdown.Send);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x000266BD File Offset: 0x000248BD
		protected override void OnDisconnected()
		{
			base.OnDisconnected();
			this.ShutdownSocket(SocketShutdown.Both);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x000266CC File Offset: 0x000248CC
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				if (this._socket != null)
				{
					object socketLock = this._socketLock;
					lock (socketLock)
					{
						if (this._socket != null)
						{
							this._socket.Dispose();
							this._socket = null;
						}
					}
				}
				EventWaitHandle channelOpen = this._channelOpen;
				if (channelOpen != null)
				{
					this._channelOpen = null;
					channelOpen.Dispose();
				}
				EventWaitHandle channelData = this._channelData;
				if (channelData != null)
				{
					this._channelData = null;
					channelData.Dispose();
				}
			}
		}

		// Token: 0x0400046A RID: 1130
		private readonly object _socketLock = new object();

		// Token: 0x0400046B RID: 1131
		private EventWaitHandle _channelOpen = new AutoResetEvent(false);

		// Token: 0x0400046C RID: 1132
		private EventWaitHandle _channelData = new AutoResetEvent(false);

		// Token: 0x0400046D RID: 1133
		private IForwardedPort _forwardedPort;

		// Token: 0x0400046E RID: 1134
		private Socket _socket;
	}
}
