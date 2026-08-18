using System;
using System.Globalization;
using System.Text;
using System.Threading;
using Renci.SshNet.Channels;
using Renci.SshNet.Common;

namespace Renci.SshNet
{
	// Token: 0x02000030 RID: 48
	internal abstract class SubsystemSession : ISubsystemSession, IDisposable
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000E496 File Offset: 0x0000C696
		// (set) Token: 0x060003AC RID: 940 RVA: 0x0000E49E File Offset: 0x0000C69E
		private protected TimeSpan OperationTimeout { protected get; private set; }

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x060003AD RID: 941 RVA: 0x0000E4A8 File Offset: 0x0000C6A8
		// (remove) Token: 0x060003AE RID: 942 RVA: 0x0000E4E0 File Offset: 0x0000C6E0
		public event EventHandler<ExceptionEventArgs> ErrorOccurred;

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x060003AF RID: 943 RVA: 0x0000E518 File Offset: 0x0000C718
		// (remove) Token: 0x060003B0 RID: 944 RVA: 0x0000E550 File Offset: 0x0000C750
		public event EventHandler<EventArgs> Disconnected;

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000E585 File Offset: 0x0000C785
		internal IChannelSession Channel
		{
			get
			{
				this.EnsureNotDisposed();
				return this._channel;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000E593 File Offset: 0x0000C793
		public bool IsOpen
		{
			get
			{
				return this._channel != null && this._channel.IsOpen;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000E5AA File Offset: 0x0000C7AA
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x0000E5B2 File Offset: 0x0000C7B2
		private protected Encoding Encoding { protected get; private set; }

		// Token: 0x060003B5 RID: 949 RVA: 0x0000E5BC File Offset: 0x0000C7BC
		protected SubsystemSession(ISession session, string subsystemName, TimeSpan operationTimeout, Encoding encoding)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (subsystemName == null)
			{
				throw new ArgumentNullException("subsystemName");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			this._session = session;
			this._subsystemName = subsystemName;
			this.OperationTimeout = operationTimeout;
			this.Encoding = encoding;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000E63C File Offset: 0x0000C83C
		public void Connect()
		{
			this.EnsureNotDisposed();
			if (this.IsOpen)
			{
				throw new InvalidOperationException("The session is already connected.");
			}
			this._errorOccuredWaitHandle.Reset();
			this._sessionDisconnectedWaitHandle.Reset();
			this._sessionDisconnectedWaitHandle.Reset();
			this._channelClosedWaitHandle.Reset();
			this._session.ErrorOccured += this.Session_ErrorOccured;
			this._session.Disconnected += this.Session_Disconnected;
			this._channel = this._session.CreateChannelSession();
			this._channel.DataReceived += this.Channel_DataReceived;
			this._channel.Exception += this.Channel_Exception;
			this._channel.Closed += this.Channel_Closed;
			this._channel.Open();
			this._channel.SendSubsystemRequest(this._subsystemName);
			this.OnChannelOpen();
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000E73C File Offset: 0x0000C93C
		public void Disconnect()
		{
			this.UnsubscribeFromSessionEvents(this._session);
			IChannelSession channel = this._channel;
			if (channel != null)
			{
				channel.DataReceived -= this.Channel_DataReceived;
				channel.Exception -= this.Channel_Exception;
				channel.Closed -= this.Channel_Closed;
				channel.Close();
				channel.Dispose();
				this._channel = null;
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000E7A8 File Offset: 0x0000C9A8
		public void SendData(byte[] data)
		{
			this.EnsureNotDisposed();
			this.EnsureSessionIsOpen();
			this._channel.SendData(data);
		}

		// Token: 0x060003B9 RID: 953
		protected abstract void OnChannelOpen();

		// Token: 0x060003BA RID: 954
		protected abstract void OnDataReceived(byte[] data);

		// Token: 0x060003BB RID: 955 RVA: 0x0000E7C4 File Offset: 0x0000C9C4
		protected void RaiseError(Exception error)
		{
			this._exception = error;
			EventWaitHandle errorOccuredWaitHandle = this._errorOccuredWaitHandle;
			if (errorOccuredWaitHandle != null)
			{
				errorOccuredWaitHandle.Set();
			}
			this.SignalErrorOccurred(error);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000E7F0 File Offset: 0x0000C9F0
		private void Channel_DataReceived(object sender, ChannelDataEventArgs e)
		{
			try
			{
				this.OnDataReceived(e.Data);
			}
			catch (Exception error)
			{
				this.RaiseError(error);
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000E828 File Offset: 0x0000CA28
		private void Channel_Exception(object sender, ExceptionEventArgs e)
		{
			this.RaiseError(e.Exception);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000E838 File Offset: 0x0000CA38
		private void Channel_Closed(object sender, ChannelEventArgs e)
		{
			EventWaitHandle channelClosedWaitHandle = this._channelClosedWaitHandle;
			if (channelClosedWaitHandle != null)
			{
				channelClosedWaitHandle.Set();
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000E858 File Offset: 0x0000CA58
		public void WaitOnHandle(WaitHandle waitHandle, TimeSpan operationTimeout)
		{
			int num = WaitHandle.WaitAny(new WaitHandle[]
			{
				this._errorOccuredWaitHandle,
				this._sessionDisconnectedWaitHandle,
				this._channelClosedWaitHandle,
				waitHandle
			}, operationTimeout);
			switch (num)
			{
			case 0:
				throw this._exception;
			case 1:
				throw new SshException("Connection was closed by the server.");
			case 2:
				throw new SshException("Channel was closed.");
			default:
				if (num != 258)
				{
					return;
				}
				throw new SshOperationTimeoutException(string.Format(CultureInfo.CurrentCulture, "Operation has timed out.", new object[0]));
			}
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000E8E4 File Offset: 0x0000CAE4
		private void Session_Disconnected(object sender, EventArgs e)
		{
			EventWaitHandle sessionDisconnectedWaitHandle = this._sessionDisconnectedWaitHandle;
			if (sessionDisconnectedWaitHandle != null)
			{
				sessionDisconnectedWaitHandle.Set();
			}
			this.SignalDisconnected();
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000E828 File Offset: 0x0000CA28
		private void Session_ErrorOccured(object sender, ExceptionEventArgs e)
		{
			this.RaiseError(e.Exception);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000E908 File Offset: 0x0000CB08
		private void SignalErrorOccurred(Exception error)
		{
			EventHandler<ExceptionEventArgs> errorOccurred = this.ErrorOccurred;
			if (errorOccurred != null)
			{
				errorOccurred(this, new ExceptionEventArgs(error));
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000E92C File Offset: 0x0000CB2C
		private void SignalDisconnected()
		{
			EventHandler<EventArgs> disconnected = this.Disconnected;
			if (disconnected != null)
			{
				disconnected(this, new EventArgs());
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000E94F File Offset: 0x0000CB4F
		private void EnsureSessionIsOpen()
		{
			if (!this.IsOpen)
			{
				throw new InvalidOperationException("The session is not open.");
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000E964 File Offset: 0x0000CB64
		private void UnsubscribeFromSessionEvents(ISession session)
		{
			if (session == null)
			{
				return;
			}
			session.Disconnected -= this.Session_Disconnected;
			session.ErrorOccured -= this.Session_ErrorOccured;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000E98E File Offset: 0x0000CB8E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000E9A0 File Offset: 0x0000CBA0
		protected virtual void Dispose(bool disposing)
		{
			if (this._isDisposed)
			{
				return;
			}
			if (disposing)
			{
				this.Disconnect();
				this._session = null;
				EventWaitHandle errorOccuredWaitHandle = this._errorOccuredWaitHandle;
				if (errorOccuredWaitHandle != null)
				{
					errorOccuredWaitHandle.Dispose();
					this._errorOccuredWaitHandle = null;
				}
				EventWaitHandle sessionDisconnectedWaitHandle = this._sessionDisconnectedWaitHandle;
				if (sessionDisconnectedWaitHandle != null)
				{
					sessionDisconnectedWaitHandle.Dispose();
					this._sessionDisconnectedWaitHandle = null;
				}
				EventWaitHandle channelClosedWaitHandle = this._channelClosedWaitHandle;
				if (channelClosedWaitHandle != null)
				{
					channelClosedWaitHandle.Dispose();
					this._channelClosedWaitHandle = null;
				}
				this._isDisposed = true;
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000EA14 File Offset: 0x0000CC14
		~SubsystemSession()
		{
			this.Dispose(false);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000EA44 File Offset: 0x0000CC44
		private void EnsureNotDisposed()
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x0400011A RID: 282
		private ISession _session;

		// Token: 0x0400011B RID: 283
		private readonly string _subsystemName;

		// Token: 0x0400011C RID: 284
		private IChannelSession _channel;

		// Token: 0x0400011D RID: 285
		private Exception _exception;

		// Token: 0x0400011E RID: 286
		private EventWaitHandle _errorOccuredWaitHandle = new ManualResetEvent(false);

		// Token: 0x0400011F RID: 287
		private EventWaitHandle _sessionDisconnectedWaitHandle = new ManualResetEvent(false);

		// Token: 0x04000120 RID: 288
		private EventWaitHandle _channelClosedWaitHandle = new ManualResetEvent(false);

		// Token: 0x04000125 RID: 293
		private bool _isDisposed;
	}
}
