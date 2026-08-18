using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Renci.SshNet.Abstractions;
using Renci.SshNet.Channels;
using Renci.SshNet.Common;

namespace Renci.SshNet
{
	// Token: 0x0200002B RID: 43
	public class Shell : IDisposable
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000C14B File Offset: 0x0000A34B
		// (set) Token: 0x0600031F RID: 799 RVA: 0x0000C153 File Offset: 0x0000A353
		public bool IsStarted { get; private set; }

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06000320 RID: 800 RVA: 0x0000C15C File Offset: 0x0000A35C
		// (remove) Token: 0x06000321 RID: 801 RVA: 0x0000C194 File Offset: 0x0000A394
		public event EventHandler<EventArgs> Starting;

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x06000322 RID: 802 RVA: 0x0000C1CC File Offset: 0x0000A3CC
		// (remove) Token: 0x06000323 RID: 803 RVA: 0x0000C204 File Offset: 0x0000A404
		public event EventHandler<EventArgs> Started;

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06000324 RID: 804 RVA: 0x0000C23C File Offset: 0x0000A43C
		// (remove) Token: 0x06000325 RID: 805 RVA: 0x0000C274 File Offset: 0x0000A474
		public event EventHandler<EventArgs> Stopping;

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06000326 RID: 806 RVA: 0x0000C2AC File Offset: 0x0000A4AC
		// (remove) Token: 0x06000327 RID: 807 RVA: 0x0000C2E4 File Offset: 0x0000A4E4
		public event EventHandler<EventArgs> Stopped;

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x06000328 RID: 808 RVA: 0x0000C31C File Offset: 0x0000A51C
		// (remove) Token: 0x06000329 RID: 809 RVA: 0x0000C354 File Offset: 0x0000A554
		public event EventHandler<ExceptionEventArgs> ErrorOccurred;

		// Token: 0x0600032A RID: 810 RVA: 0x0000C38C File Offset: 0x0000A58C
		internal Shell(ISession session, Stream input, Stream output, Stream extendedOutput, string terminalName, uint columns, uint rows, uint width, uint height, IDictionary<TerminalModes, uint> terminalModes, int bufferSize)
		{
			this._session = session;
			this._input = input;
			this._outputStream = output;
			this._extendedOutputStream = extendedOutput;
			this._terminalName = terminalName;
			this._columns = columns;
			this._rows = rows;
			this._width = width;
			this._height = height;
			this._terminalModes = terminalModes;
			this._bufferSize = bufferSize;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000C3F4 File Offset: 0x0000A5F4
		public void Start()
		{
			if (this.IsStarted)
			{
				throw new SshException("Shell is started.");
			}
			if (this.Starting != null)
			{
				this.Starting(this, new EventArgs());
			}
			this._channel = this._session.CreateChannelSession();
			this._channel.DataReceived += this.Channel_DataReceived;
			this._channel.ExtendedDataReceived += this.Channel_ExtendedDataReceived;
			this._channel.Closed += this.Channel_Closed;
			this._session.Disconnected += this.Session_Disconnected;
			this._session.ErrorOccured += this.Session_ErrorOccured;
			this._channel.Open();
			this._channel.SendPseudoTerminalRequest(this._terminalName, this._columns, this._rows, this._width, this._height, this._terminalModes);
			this._channel.SendShellRequest();
			this._channelClosedWaitHandle = new AutoResetEvent(false);
			this._dataReaderTaskCompleted = new ManualResetEvent(false);
			ThreadAbstraction.ExecuteThread(delegate
			{
				try
				{
					byte[] buffer = new byte[this._bufferSize];
					AsyncCallback <>9__1;
					while (this._channel.IsOpen)
					{
						Stream input = this._input;
						byte[] buffer2 = buffer;
						int offset = 0;
						int count = buffer.Length;
						AsyncCallback callback;
						if ((callback = <>9__1) == null)
						{
							callback = (<>9__1 = delegate(IAsyncResult result)
							{
								if (this._input == null)
								{
									return;
								}
								int size = this._input.EndRead(result);
								this._channel.SendData(buffer, 0, size);
							});
						}
						IAsyncResult asyncResult = input.BeginRead(buffer2, offset, count, callback, null);
						WaitHandle.WaitAny(new WaitHandle[]
						{
							asyncResult.AsyncWaitHandle,
							this._channelClosedWaitHandle
						});
						if (!asyncResult.IsCompleted)
						{
							break;
						}
					}
				}
				catch (Exception exception)
				{
					this.RaiseError(new ExceptionEventArgs(exception));
				}
				finally
				{
					this._dataReaderTaskCompleted.Set();
				}
			});
			this.IsStarted = true;
			if (this.Started != null)
			{
				this.Started(this, new EventArgs());
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000C541 File Offset: 0x0000A741
		public void Stop()
		{
			if (!this.IsStarted)
			{
				throw new SshException("Shell is not started.");
			}
			if (this._channel != null)
			{
				this._channel.Close();
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000C569 File Offset: 0x0000A769
		private void Session_ErrorOccured(object sender, ExceptionEventArgs e)
		{
			this.RaiseError(e);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000C574 File Offset: 0x0000A774
		private void RaiseError(ExceptionEventArgs e)
		{
			EventHandler<ExceptionEventArgs> errorOccurred = this.ErrorOccurred;
			if (errorOccurred != null)
			{
				errorOccurred(this, e);
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000C593 File Offset: 0x0000A793
		private void Session_Disconnected(object sender, EventArgs e)
		{
			this.Stop();
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000C59B File Offset: 0x0000A79B
		private void Channel_ExtendedDataReceived(object sender, ChannelExtendedDataEventArgs e)
		{
			if (this._extendedOutputStream != null)
			{
				this._extendedOutputStream.Write(e.Data, 0, e.Data.Length);
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000C5BF File Offset: 0x0000A7BF
		private void Channel_DataReceived(object sender, ChannelDataEventArgs e)
		{
			if (this._outputStream != null)
			{
				this._outputStream.Write(e.Data, 0, e.Data.Length);
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000C5E4 File Offset: 0x0000A7E4
		private void Channel_Closed(object sender, ChannelEventArgs e)
		{
			if (this.Stopping != null)
			{
				ThreadAbstraction.ExecuteThread(delegate
				{
					this.Stopping(this, new EventArgs());
				});
			}
			if (this._channel.IsOpen)
			{
				this._channel.Close();
			}
			this._channelClosedWaitHandle.Set();
			this._input.Dispose();
			this._input = null;
			this._dataReaderTaskCompleted.WaitOne(this._session.ConnectionInfo.Timeout);
			this._dataReaderTaskCompleted.Dispose();
			this._dataReaderTaskCompleted = null;
			this._channel.DataReceived -= this.Channel_DataReceived;
			this._channel.ExtendedDataReceived -= this.Channel_ExtendedDataReceived;
			this._channel.Closed -= this.Channel_Closed;
			this.UnsubscribeFromSessionEvents(this._session);
			if (this.Stopped != null)
			{
				ThreadAbstraction.ExecuteThread(delegate
				{
					this.Stopped(this, new EventArgs());
				});
			}
			this._channel = null;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000C6DF File Offset: 0x0000A8DF
		private void UnsubscribeFromSessionEvents(ISession session)
		{
			if (session == null)
			{
				return;
			}
			session.Disconnected -= this.Session_Disconnected;
			session.ErrorOccured -= this.Session_ErrorOccured;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000C709 File Offset: 0x0000A909
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000C718 File Offset: 0x0000A918
		protected virtual void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			if (disposing)
			{
				this.UnsubscribeFromSessionEvents(this._session);
				EventWaitHandle channelClosedWaitHandle = this._channelClosedWaitHandle;
				if (channelClosedWaitHandle != null)
				{
					channelClosedWaitHandle.Dispose();
					this._channelClosedWaitHandle = null;
				}
				IChannelSession channel = this._channel;
				if (channel != null)
				{
					channel.Dispose();
					this._channel = null;
				}
				EventWaitHandle dataReaderTaskCompleted = this._dataReaderTaskCompleted;
				if (dataReaderTaskCompleted != null)
				{
					dataReaderTaskCompleted.Dispose();
					this._dataReaderTaskCompleted = null;
				}
				this._disposed = true;
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000C78C File Offset: 0x0000A98C
		~Shell()
		{
			this.Dispose(false);
		}

		// Token: 0x040000E0 RID: 224
		private readonly ISession _session;

		// Token: 0x040000E1 RID: 225
		private IChannelSession _channel;

		// Token: 0x040000E2 RID: 226
		private EventWaitHandle _channelClosedWaitHandle;

		// Token: 0x040000E3 RID: 227
		private Stream _input;

		// Token: 0x040000E4 RID: 228
		private readonly string _terminalName;

		// Token: 0x040000E5 RID: 229
		private readonly uint _columns;

		// Token: 0x040000E6 RID: 230
		private readonly uint _rows;

		// Token: 0x040000E7 RID: 231
		private readonly uint _width;

		// Token: 0x040000E8 RID: 232
		private readonly uint _height;

		// Token: 0x040000E9 RID: 233
		private readonly IDictionary<TerminalModes, uint> _terminalModes;

		// Token: 0x040000EA RID: 234
		private EventWaitHandle _dataReaderTaskCompleted;

		// Token: 0x040000EB RID: 235
		private readonly Stream _outputStream;

		// Token: 0x040000EC RID: 236
		private readonly Stream _extendedOutputStream;

		// Token: 0x040000ED RID: 237
		private readonly int _bufferSize;

		// Token: 0x040000F4 RID: 244
		private bool _disposed;
	}
}
