using System;
using System.Net;
using System.Net.Sockets;
using Renci.SshNet.Abstractions;
using Renci.SshNet.Common;
using Renci.SshNet.Messages.Connection;

namespace Renci.SshNet.Channels
{
	// Token: 0x0200010F RID: 271
	internal class ChannelForwardedTcpip : ServerChannel, IChannelForwardedTcpip, IDisposable
	{
		// Token: 0x06000BC8 RID: 3016 RVA: 0x00026760 File Offset: 0x00024960
		internal ChannelForwardedTcpip(ISession session, uint localChannelNumber, uint localWindowSize, uint localPacketSize, uint remoteChannelNumber, uint remoteWindowSize, uint remotePacketSize) : base(session, localChannelNumber, localWindowSize, localPacketSize, remoteChannelNumber, remoteWindowSize, remotePacketSize)
		{
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x00012297 File Offset: 0x00010497
		public override ChannelTypes ChannelType
		{
			get
			{
				return ChannelTypes.ForwardedTcpip;
			}
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x00026780 File Offset: 0x00024980
		public void Bind(IPEndPoint remoteEndpoint, IForwardedPort forwardedPort)
		{
			if (!base.IsConnected)
			{
				throw new SshException("Session is not connected.");
			}
			this._forwardedPort = forwardedPort;
			this._forwardedPort.Closing += this.ForwardedPort_Closing;
			try
			{
				this._socket = SocketAbstraction.Connect(remoteEndpoint, base.ConnectionInfo.Timeout);
				base.SendMessage(new ChannelOpenConfirmationMessage(base.RemoteChannelNumber, base.LocalWindowSize, base.LocalPacketSize, base.LocalChannelNumber));
			}
			catch (Exception ex)
			{
				base.SendMessage(new ChannelOpenFailureMessage(base.RemoteChannelNumber, ex.ToString(), 2U, "en"));
				throw;
			}
			byte[] array = new byte[base.RemotePacketSize];
			SocketAbstraction.ReadContinuous(this._socket, array, 0, array.Length, new Action<byte[], int, int>(this.SendData));
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x00026854 File Offset: 0x00024A54
		protected override void OnErrorOccured(Exception exp)
		{
			base.OnErrorOccured(exp);
			this.ShutdownSocket(SocketShutdown.Send);
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x00026864 File Offset: 0x00024A64
		private void ForwardedPort_Closing(object sender, EventArgs eventArgs)
		{
			this.ShutdownSocket(SocketShutdown.Send);
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x00026870 File Offset: 0x00024A70
		private void ShutdownSocket(SocketShutdown how)
		{
			if (this._socket == null || !this._socket.Connected)
			{
				return;
			}
			object socketShutdownAndCloseLock = this._socketShutdownAndCloseLock;
			lock (socketShutdownAndCloseLock)
			{
				Socket socket = this._socket;
				if (socket != null && socket.Connected)
				{
					socket.Shutdown(how);
				}
			}
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x000268DC File Offset: 0x00024ADC
		private void CloseSocket()
		{
			if (this._socket == null)
			{
				return;
			}
			object socketShutdownAndCloseLock = this._socketShutdownAndCloseLock;
			lock (socketShutdownAndCloseLock)
			{
				Socket socket = this._socket;
				if (socket != null)
				{
					socket.Dispose();
					this._socket = null;
				}
			}
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x00026938 File Offset: 0x00024B38
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

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0002697C File Offset: 0x00024B7C
		protected override void OnData(byte[] data)
		{
			base.OnData(data);
			Socket socket = this._socket;
			if (socket != null && socket.Connected)
			{
				SocketAbstraction.Send(socket, data, 0, data.Length);
			}
		}

		// Token: 0x0400046F RID: 1135
		private readonly object _socketShutdownAndCloseLock = new object();

		// Token: 0x04000470 RID: 1136
		private Socket _socket;

		// Token: 0x04000471 RID: 1137
		private IForwardedPort _forwardedPort;
	}
}
