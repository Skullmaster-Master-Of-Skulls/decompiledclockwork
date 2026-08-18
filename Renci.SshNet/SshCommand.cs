using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using Renci.SshNet.Abstractions;
using Renci.SshNet.Channels;
using Renci.SshNet.Common;
using Renci.SshNet.Messages.Connection;
using Renci.SshNet.Messages.Transport;

namespace Renci.SshNet
{
	// Token: 0x0200002E RID: 46
	public class SshCommand : IDisposable
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000D68F File Offset: 0x0000B88F
		// (set) Token: 0x0600037F RID: 895 RVA: 0x0000D697 File Offset: 0x0000B897
		public string CommandText { get; private set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000D6A0 File Offset: 0x0000B8A0
		// (set) Token: 0x06000381 RID: 897 RVA: 0x0000D6A8 File Offset: 0x0000B8A8
		public TimeSpan CommandTimeout { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000D6B1 File Offset: 0x0000B8B1
		// (set) Token: 0x06000383 RID: 899 RVA: 0x0000D6B9 File Offset: 0x0000B8B9
		public int ExitStatus { get; private set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000D6C2 File Offset: 0x0000B8C2
		// (set) Token: 0x06000385 RID: 901 RVA: 0x0000D6CA File Offset: 0x0000B8CA
		public Stream OutputStream { get; private set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0000D6D3 File Offset: 0x0000B8D3
		// (set) Token: 0x06000387 RID: 903 RVA: 0x0000D6DB File Offset: 0x0000B8DB
		public Stream ExtendedOutputStream { get; private set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0000D6E4 File Offset: 0x0000B8E4
		public string Result
		{
			get
			{
				if (this._result == null)
				{
					this._result = new StringBuilder();
				}
				if (this.OutputStream != null && this.OutputStream.Length > 0L)
				{
					StreamReader streamReader = new StreamReader(this.OutputStream, this._encoding);
					this._result.Append(streamReader.ReadToEnd());
				}
				return this._result.ToString();
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000D74C File Offset: 0x0000B94C
		public string Error
		{
			get
			{
				if (this._hasError)
				{
					if (this._error == null)
					{
						this._error = new StringBuilder();
					}
					if (this.ExtendedOutputStream != null && this.ExtendedOutputStream.Length > 0L)
					{
						StreamReader streamReader = new StreamReader(this.ExtendedOutputStream, this._encoding);
						this._error.Append(streamReader.ReadToEnd());
					}
					return this._error.ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000D7C0 File Offset: 0x0000B9C0
		internal SshCommand(ISession session, string commandText, Encoding encoding)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (commandText == null)
			{
				throw new ArgumentNullException("commandText");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			this._session = session;
			this.CommandText = commandText;
			this._encoding = encoding;
			this.CommandTimeout = Session.InfiniteTimeSpan;
			this._sessionErrorOccuredWaitHandle = new AutoResetEvent(false);
			this._session.Disconnected += this.Session_Disconnected;
			this._session.ErrorOccured += this.Session_ErrorOccured;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000D862 File Offset: 0x0000BA62
		public IAsyncResult BeginExecute()
		{
			return this.BeginExecute(null, null);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000D86C File Offset: 0x0000BA6C
		public IAsyncResult BeginExecute(AsyncCallback callback)
		{
			return this.BeginExecute(callback, null);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000D878 File Offset: 0x0000BA78
		public IAsyncResult BeginExecute(AsyncCallback callback, object state)
		{
			if (this._asyncResult != null && !this._asyncResult.EndCalled)
			{
				throw new InvalidOperationException("Asynchronous operation is already in progress.");
			}
			this._asyncResult = new CommandAsyncResult
			{
				AsyncWaitHandle = new ManualResetEvent(false),
				IsCompleted = false,
				AsyncState = state
			};
			if (this._channel != null)
			{
				throw new SshException("Invalid operation.");
			}
			if (string.IsNullOrEmpty(this.CommandText))
			{
				throw new ArgumentException("CommandText property is empty.");
			}
			Stream outputStream = this.OutputStream;
			if (outputStream != null)
			{
				outputStream.Dispose();
				this.OutputStream = null;
			}
			Stream extendedOutputStream = this.ExtendedOutputStream;
			if (extendedOutputStream != null)
			{
				extendedOutputStream.Dispose();
				this.ExtendedOutputStream = null;
			}
			this.OutputStream = new PipeStream();
			this.ExtendedOutputStream = new PipeStream();
			this._result = null;
			this._error = null;
			this._callback = callback;
			this._channel = this.CreateChannel();
			this._channel.Open();
			this._channel.SendExecRequest(this.CommandText);
			return this._asyncResult;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000D97D File Offset: 0x0000BB7D
		public IAsyncResult BeginExecute(string commandText, AsyncCallback callback, object state)
		{
			this.CommandText = commandText;
			return this.BeginExecute(callback, state);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000D990 File Offset: 0x0000BB90
		public string EndExecute(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			CommandAsyncResult commandAsyncResult = asyncResult as CommandAsyncResult;
			if (commandAsyncResult == null || this._asyncResult != commandAsyncResult)
			{
				throw new ArgumentException(string.Format("The {0} object was not returned from the corresponding asynchronous method on this class.", typeof(IAsyncResult).Name));
			}
			object endExecuteLock = this._endExecuteLock;
			string result;
			lock (endExecuteLock)
			{
				if (commandAsyncResult.EndCalled)
				{
					throw new ArgumentException("EndExecute can only be called once for each asynchronous operation.");
				}
				this.WaitOnHandle(this._asyncResult.AsyncWaitHandle);
				if (this._channel.IsOpen)
				{
					this._channel.Close();
				}
				this.UnsubscribeFromEventsAndDisposeChannel(this._channel);
				this._channel = null;
				commandAsyncResult.EndCalled = true;
				result = this.Result;
			}
			return result;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000DA68 File Offset: 0x0000BC68
		public string Execute()
		{
			return this.EndExecute(this.BeginExecute(null, null));
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000DA78 File Offset: 0x0000BC78
		public void CancelAsync()
		{
			if (this._channel != null && this._channel.IsOpen && this._asyncResult != null)
			{
				this._channel.Close();
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000DAA2 File Offset: 0x0000BCA2
		public string Execute(string commandText)
		{
			this.CommandText = commandText;
			return this.Execute();
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000DAB4 File Offset: 0x0000BCB4
		private IChannelSession CreateChannel()
		{
			IChannelSession channelSession = this._session.CreateChannelSession();
			channelSession.DataReceived += this.Channel_DataReceived;
			channelSession.ExtendedDataReceived += this.Channel_ExtendedDataReceived;
			channelSession.RequestReceived += this.Channel_RequestReceived;
			channelSession.Closed += this.Channel_Closed;
			return channelSession;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000DB14 File Offset: 0x0000BD14
		private void Session_Disconnected(object sender, EventArgs e)
		{
			if (this._isDisposed)
			{
				return;
			}
			this._exception = new SshConnectionException("An established connection was aborted by the software in your host machine.", DisconnectReason.ConnectionLost);
			this._sessionErrorOccuredWaitHandle.Set();
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000DB3D File Offset: 0x0000BD3D
		private void Session_ErrorOccured(object sender, ExceptionEventArgs e)
		{
			if (this._isDisposed)
			{
				return;
			}
			this._exception = e.Exception;
			this._sessionErrorOccuredWaitHandle.Set();
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000DB60 File Offset: 0x0000BD60
		private void Channel_Closed(object sender, ChannelEventArgs e)
		{
			Stream outputStream = this.OutputStream;
			if (outputStream != null)
			{
				outputStream.Flush();
			}
			Stream extendedOutputStream = this.ExtendedOutputStream;
			if (extendedOutputStream != null)
			{
				extendedOutputStream.Flush();
			}
			this._asyncResult.IsCompleted = true;
			if (this._callback != null)
			{
				ThreadAbstraction.ExecuteThread(delegate
				{
					this._callback(this._asyncResult);
				});
			}
			((EventWaitHandle)this._asyncResult.AsyncWaitHandle).Set();
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000DBC8 File Offset: 0x0000BDC8
		private void Channel_RequestReceived(object sender, ChannelRequestEventArgs e)
		{
			ExitStatusRequestInfo exitStatusRequestInfo = e.Info as ExitStatusRequestInfo;
			if (exitStatusRequestInfo != null)
			{
				this.ExitStatus = (int)exitStatusRequestInfo.ExitStatus;
				if (exitStatusRequestInfo.WantReply)
				{
					ChannelSuccessMessage message = new ChannelSuccessMessage(this._channel.LocalChannelNumber);
					this._session.SendMessage(message);
					return;
				}
			}
			else if (e.Info.WantReply)
			{
				ChannelFailureMessage message2 = new ChannelFailureMessage(this._channel.LocalChannelNumber);
				this._session.SendMessage(message2);
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000DC40 File Offset: 0x0000BE40
		private void Channel_ExtendedDataReceived(object sender, ChannelExtendedDataEventArgs e)
		{
			if (this.ExtendedOutputStream != null)
			{
				this.ExtendedOutputStream.Write(e.Data, 0, e.Data.Length);
				this.ExtendedOutputStream.Flush();
			}
			if (e.DataTypeCode == 1U)
			{
				this._hasError = true;
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000DC80 File Offset: 0x0000BE80
		private void Channel_DataReceived(object sender, ChannelDataEventArgs e)
		{
			if (this.OutputStream != null)
			{
				this.OutputStream.Write(e.Data, 0, e.Data.Length);
				this.OutputStream.Flush();
			}
			if (this._asyncResult != null)
			{
				CommandAsyncResult asyncResult = this._asyncResult;
				lock (asyncResult)
				{
					this._asyncResult.BytesReceived += e.Data.Length;
				}
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000DD0C File Offset: 0x0000BF0C
		private void WaitOnHandle(WaitHandle waitHandle)
		{
			int num = WaitHandle.WaitAny(new WaitHandle[]
			{
				this._sessionErrorOccuredWaitHandle,
				waitHandle
			}, this.CommandTimeout);
			if (num == 0)
			{
				throw this._exception;
			}
			if (num != 258)
			{
				return;
			}
			throw new SshOperationTimeoutException(string.Format(CultureInfo.CurrentCulture, "Command '{0}' has timed out.", new object[]
			{
				this.CommandText
			}));
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000DD70 File Offset: 0x0000BF70
		private void UnsubscribeFromEventsAndDisposeChannel(IChannel channel)
		{
			if (channel == null)
			{
				return;
			}
			channel.DataReceived -= this.Channel_DataReceived;
			channel.ExtendedDataReceived -= this.Channel_ExtendedDataReceived;
			channel.RequestReceived -= this.Channel_RequestReceived;
			channel.Closed -= this.Channel_Closed;
			channel.Dispose();
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000DDCF File Offset: 0x0000BFCF
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000DDE0 File Offset: 0x0000BFE0
		protected virtual void Dispose(bool disposing)
		{
			if (this._isDisposed)
			{
				return;
			}
			if (disposing)
			{
				ISession session = this._session;
				if (session != null)
				{
					session.Disconnected -= this.Session_Disconnected;
					session.ErrorOccured -= this.Session_ErrorOccured;
					this._session = null;
				}
				IChannelSession channel = this._channel;
				if (channel != null)
				{
					this.UnsubscribeFromEventsAndDisposeChannel(channel);
					this._channel = null;
				}
				Stream outputStream = this.OutputStream;
				if (outputStream != null)
				{
					outputStream.Dispose();
					this.OutputStream = null;
				}
				Stream extendedOutputStream = this.ExtendedOutputStream;
				if (extendedOutputStream != null)
				{
					extendedOutputStream.Dispose();
					this.ExtendedOutputStream = null;
				}
				EventWaitHandle sessionErrorOccuredWaitHandle = this._sessionErrorOccuredWaitHandle;
				if (sessionErrorOccuredWaitHandle != null)
				{
					sessionErrorOccuredWaitHandle.Dispose();
					this._sessionErrorOccuredWaitHandle = null;
				}
				this._isDisposed = true;
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000DE98 File Offset: 0x0000C098
		~SshCommand()
		{
			this.Dispose(false);
		}

		// Token: 0x04000103 RID: 259
		private ISession _session;

		// Token: 0x04000104 RID: 260
		private readonly Encoding _encoding;

		// Token: 0x04000105 RID: 261
		private IChannelSession _channel;

		// Token: 0x04000106 RID: 262
		private CommandAsyncResult _asyncResult;

		// Token: 0x04000107 RID: 263
		private AsyncCallback _callback;

		// Token: 0x04000108 RID: 264
		private EventWaitHandle _sessionErrorOccuredWaitHandle;

		// Token: 0x04000109 RID: 265
		private Exception _exception;

		// Token: 0x0400010A RID: 266
		private bool _hasError;

		// Token: 0x0400010B RID: 267
		private readonly object _endExecuteLock = new object();

		// Token: 0x04000111 RID: 273
		private StringBuilder _result;

		// Token: 0x04000112 RID: 274
		private StringBuilder _error;

		// Token: 0x04000113 RID: 275
		private bool _isDisposed;
	}
}
