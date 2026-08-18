using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Renci.SshNet.Abstractions;
using Renci.SshNet.Channels;
using Renci.SshNet.Common;

namespace Renci.SshNet
{
	// Token: 0x0200002C RID: 44
	public class ShellStream : Stream
	{
		// Token: 0x14000044 RID: 68
		// (add) Token: 0x0600033A RID: 826 RVA: 0x0000C8BC File Offset: 0x0000AABC
		// (remove) Token: 0x0600033B RID: 827 RVA: 0x0000C8F4 File Offset: 0x0000AAF4
		public event EventHandler<ShellDataEventArgs> DataReceived;

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x0600033C RID: 828 RVA: 0x0000C92C File Offset: 0x0000AB2C
		// (remove) Token: 0x0600033D RID: 829 RVA: 0x0000C964 File Offset: 0x0000AB64
		public event EventHandler<ExceptionEventArgs> ErrorOccurred;

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000C99C File Offset: 0x0000AB9C
		public bool DataAvailable
		{
			get
			{
				Queue<byte> incoming = this._incoming;
				bool result;
				lock (incoming)
				{
					result = (this._incoming.Count > 0);
				}
				return result;
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000C9E8 File Offset: 0x0000ABE8
		internal ShellStream(ISession session, string terminalName, uint columns, uint rows, uint width, uint height, IDictionary<TerminalModes, uint> terminalModeValues)
		{
			this._encoding = session.ConnectionInfo.Encoding;
			this._session = session;
			this._incoming = new Queue<byte>();
			this._outgoing = new Queue<byte>();
			this._channel = this._session.CreateChannelSession();
			this._channel.DataReceived += this.Channel_DataReceived;
			this._channel.Closed += this.Channel_Closed;
			this._session.Disconnected += this.Session_Disconnected;
			this._session.ErrorOccured += this.Session_ErrorOccured;
			this._channel.Open();
			this._channel.SendPseudoTerminalRequest(terminalName, columns, rows, width, height, terminalModeValues);
			this._channel.SendShellRequest();
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000CACF File Offset: 0x0000ACCF
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000CAD2 File Offset: 0x0000ACD2
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000CACF File Offset: 0x0000ACCF
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000CAD5 File Offset: 0x0000ACD5
		public override void Flush()
		{
			if (this._channel == null)
			{
				throw new ObjectDisposedException("ShellStream");
			}
			this._channel.SendData(this._outgoing.ToArray());
			this._outgoing.Clear();
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000CB0C File Offset: 0x0000AD0C
		public override long Length
		{
			get
			{
				Queue<byte> incoming = this._incoming;
				long result;
				lock (incoming)
				{
					result = (long)this._incoming.Count;
				}
				return result;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000CB54 File Offset: 0x0000AD54
		// (set) Token: 0x06000346 RID: 838 RVA: 0x0000CB58 File Offset: 0x0000AD58
		public override long Position
		{
			get
			{
				return 0L;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000CB60 File Offset: 0x0000AD60
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			Queue<byte> incoming = this._incoming;
			lock (incoming)
			{
				while (num < count && this._incoming.Count > 0)
				{
					buffer[offset + num] = this._incoming.Dequeue();
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000CB58 File Offset: 0x0000AD58
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000CB58 File Offset: 0x0000AD58
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000CBC8 File Offset: 0x0000ADC8
		public override void Write(byte[] buffer, int offset, int count)
		{
			foreach (byte item in buffer.Take(offset, count))
			{
				if (this._outgoing.Count < 1024)
				{
					this._outgoing.Enqueue(item);
				}
				else
				{
					this.Flush();
				}
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000CC16 File Offset: 0x0000AE16
		public void Expect(params ExpectAction[] expectActions)
		{
			this.Expect(TimeSpan.Zero, expectActions);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000CC24 File Offset: 0x0000AE24
		public void Expect(TimeSpan timeout, params ExpectAction[] expectActions)
		{
			bool flag = false;
			string text = string.Empty;
			for (;;)
			{
				Queue<byte> incoming = this._incoming;
				lock (incoming)
				{
					if (this._incoming.Count > 0)
					{
						text = this._encoding.GetString(this._incoming.ToArray(), 0, this._incoming.Count);
					}
					if (text.Length > 0)
					{
						foreach (ExpectAction expectAction in expectActions)
						{
							Match match = expectAction.Expect.Match(text);
							if (match.Success)
							{
								string obj = text.Substring(0, match.Index + match.Length);
								int num = 0;
								while (num < match.Index + match.Length && this._incoming.Count > 0)
								{
									this._incoming.Dequeue();
									num++;
								}
								expectAction.Action(obj);
								flag = true;
							}
						}
					}
				}
				if (!flag)
				{
					if (timeout.Ticks > 0L)
					{
						if (!this._dataReceived.WaitOne(timeout))
						{
							break;
						}
					}
					else
					{
						this._dataReceived.WaitOne();
					}
				}
				if (flag)
				{
					return;
				}
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000CD70 File Offset: 0x0000AF70
		public IAsyncResult BeginExpect(params ExpectAction[] expectActions)
		{
			return this.BeginExpect(TimeSpan.Zero, null, null, expectActions);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000CD80 File Offset: 0x0000AF80
		public IAsyncResult BeginExpect(AsyncCallback callback, params ExpectAction[] expectActions)
		{
			return this.BeginExpect(TimeSpan.Zero, callback, null, expectActions);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000CD90 File Offset: 0x0000AF90
		public IAsyncResult BeginExpect(AsyncCallback callback, object state, params ExpectAction[] expectActions)
		{
			return this.BeginExpect(TimeSpan.Zero, callback, state, expectActions);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000CDA0 File Offset: 0x0000AFA0
		public IAsyncResult BeginExpect(TimeSpan timeout, AsyncCallback callback, object state, params ExpectAction[] expectActions)
		{
			string text = string.Empty;
			ExpectAsyncResult asyncResult = new ExpectAsyncResult(callback, state);
			ThreadAbstraction.ExecuteThread(delegate
			{
				string text = null;
				try
				{
					for (;;)
					{
						Queue<byte> incoming = this._incoming;
						lock (incoming)
						{
							if (this._incoming.Count > 0)
							{
								text = this._encoding.GetString(this._incoming.ToArray(), 0, this._incoming.Count);
							}
							if (text.Length > 0)
							{
								foreach (ExpectAction expectAction in expectActions)
								{
									Match match = expectAction.Expect.Match(text);
									if (match.Success)
									{
										string text2 = text.Substring(0, match.Index + match.Length);
										int num = 0;
										while (num < match.Index + match.Length && this._incoming.Count > 0)
										{
											this._incoming.Dequeue();
											num++;
										}
										expectAction.Action(text2);
										if (callback != null)
										{
											callback(asyncResult);
										}
										text = text2;
									}
								}
							}
						}
						if (text != null)
						{
							goto IL_1AB;
						}
						if (timeout.Ticks > 0L)
						{
							if (!this._dataReceived.WaitOne(timeout))
							{
								break;
							}
						}
						else
						{
							this._dataReceived.WaitOne();
						}
					}
					if (callback != null)
					{
						callback(asyncResult);
					}
					IL_1AB:
					asyncResult.SetAsCompleted(text, true);
				}
				catch (Exception exception)
				{
					asyncResult.SetAsCompleted(exception, true);
				}
			});
			return asyncResult;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000CE04 File Offset: 0x0000B004
		public string EndExpect(IAsyncResult asyncResult)
		{
			ExpectAsyncResult expectAsyncResult = asyncResult as ExpectAsyncResult;
			if (expectAsyncResult == null || expectAsyncResult.EndInvokeCalled)
			{
				throw new ArgumentException("Either the IAsyncResult object did not come from the corresponding async method on this type, or EndExecute was called multiple times with the same IAsyncResult.");
			}
			return expectAsyncResult.EndInvoke();
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000CE34 File Offset: 0x0000B034
		public string Expect(string text)
		{
			return this.Expect(new Regex(Regex.Escape(text)), Session.InfiniteTimeSpan);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000CE4C File Offset: 0x0000B04C
		public string Expect(string text, TimeSpan timeout)
		{
			return this.Expect(new Regex(Regex.Escape(text)), timeout);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000CE60 File Offset: 0x0000B060
		public string Expect(Regex regex)
		{
			return this.Expect(regex, TimeSpan.Zero);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000CE70 File Offset: 0x0000B070
		public string Expect(Regex regex, TimeSpan timeout)
		{
			string text = string.Empty;
			for (;;)
			{
				Queue<byte> incoming = this._incoming;
				lock (incoming)
				{
					if (this._incoming.Count > 0)
					{
						text = this._encoding.GetString(this._incoming.ToArray(), 0, this._incoming.Count);
					}
					Match match = regex.Match(text);
					if (match.Success)
					{
						int num = 0;
						while (num < match.Index + match.Length && this._incoming.Count > 0)
						{
							this._incoming.Dequeue();
							num++;
						}
						return text;
					}
				}
				if (timeout.Ticks > 0L)
				{
					if (!this._dataReceived.WaitOne(timeout))
					{
						break;
					}
				}
				else
				{
					this._dataReceived.WaitOne();
				}
			}
			return null;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000CF5C File Offset: 0x0000B15C
		public string ReadLine()
		{
			return this.ReadLine(TimeSpan.Zero);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000CF6C File Offset: 0x0000B16C
		public string ReadLine(TimeSpan timeout)
		{
			string text = string.Empty;
			for (;;)
			{
				Queue<byte> incoming = this._incoming;
				lock (incoming)
				{
					if (this._incoming.Count > 0)
					{
						text = this._encoding.GetString(this._incoming.ToArray(), 0, this._incoming.Count);
					}
					int num = text.IndexOf("\r\n", StringComparison.Ordinal);
					if (num >= 0)
					{
						text = text.Substring(0, num);
						int byteCount = this._encoding.GetByteCount(text + "\r\n");
						for (int i = 0; i < byteCount; i++)
						{
							this._incoming.Dequeue();
						}
						return text;
					}
				}
				if (timeout.Ticks > 0L)
				{
					if (!this._dataReceived.WaitOne(timeout))
					{
						break;
					}
				}
				else
				{
					this._dataReceived.WaitOne();
				}
			}
			return null;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000D060 File Offset: 0x0000B260
		public string Read()
		{
			Queue<byte> incoming = this._incoming;
			string @string;
			lock (incoming)
			{
				@string = this._encoding.GetString(this._incoming.ToArray(), 0, this._incoming.Count);
				this._incoming.Clear();
			}
			return @string;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000D0CC File Offset: 0x0000B2CC
		public void Write(string text)
		{
			if (text == null)
			{
				return;
			}
			if (this._channel == null)
			{
				throw new ObjectDisposedException("ShellStream");
			}
			byte[] bytes = this._encoding.GetBytes(text);
			this._channel.SendData(bytes);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000D10C File Offset: 0x0000B30C
		public void WriteLine(string line)
		{
			string text = string.Format("{0}{1}", line, "\r");
			this.Write(text);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000D134 File Offset: 0x0000B334
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (this._isDisposed)
			{
				return;
			}
			if (disposing)
			{
				this.UnsubscribeFromSessionEvents(this._session);
				if (this._channel != null)
				{
					this._channel.DataReceived -= this.Channel_DataReceived;
					this._channel.Closed -= this.Channel_Closed;
					this._channel.Dispose();
					this._channel = null;
				}
				if (this._dataReceived != null)
				{
					this._dataReceived.Dispose();
					this._dataReceived = null;
				}
				this._isDisposed = true;
				return;
			}
			this.UnsubscribeFromSessionEvents(this._session);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000D1D6 File Offset: 0x0000B3D6
		private void UnsubscribeFromSessionEvents(ISession session)
		{
			if (session == null)
			{
				return;
			}
			session.Disconnected -= this.Session_Disconnected;
			session.ErrorOccured -= this.Session_ErrorOccured;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000D200 File Offset: 0x0000B400
		private void Session_ErrorOccured(object sender, ExceptionEventArgs e)
		{
			this.OnRaiseError(e);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000D209 File Offset: 0x0000B409
		private void Session_Disconnected(object sender, EventArgs e)
		{
			if (this._channel != null)
			{
				this._channel.Close();
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000D21E File Offset: 0x0000B41E
		private void Channel_Closed(object sender, ChannelEventArgs e)
		{
			base.Dispose();
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000D228 File Offset: 0x0000B428
		private void Channel_DataReceived(object sender, ChannelDataEventArgs e)
		{
			Queue<byte> incoming = this._incoming;
			lock (incoming)
			{
				foreach (byte item in e.Data)
				{
					this._incoming.Enqueue(item);
				}
			}
			if (this._dataReceived != null)
			{
				this._dataReceived.Set();
			}
			this.OnDataReceived(e.Data);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000D2AC File Offset: 0x0000B4AC
		private void OnRaiseError(ExceptionEventArgs e)
		{
			EventHandler<ExceptionEventArgs> errorOccurred = this.ErrorOccurred;
			if (errorOccurred != null)
			{
				errorOccurred(this, e);
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000D2CC File Offset: 0x0000B4CC
		private void OnDataReceived(byte[] data)
		{
			EventHandler<ShellDataEventArgs> dataReceived = this.DataReceived;
			if (dataReceived != null)
			{
				dataReceived(this, new ShellDataEventArgs(data));
			}
		}

		// Token: 0x040000F5 RID: 245
		private const string CrLf = "\r\n";

		// Token: 0x040000F6 RID: 246
		private const int BufferSize = 1024;

		// Token: 0x040000F7 RID: 247
		private readonly ISession _session;

		// Token: 0x040000F8 RID: 248
		private readonly Encoding _encoding;

		// Token: 0x040000F9 RID: 249
		private readonly Queue<byte> _incoming;

		// Token: 0x040000FA RID: 250
		private readonly Queue<byte> _outgoing;

		// Token: 0x040000FB RID: 251
		private IChannelSession _channel;

		// Token: 0x040000FC RID: 252
		private AutoResetEvent _dataReceived = new AutoResetEvent(false);

		// Token: 0x040000FD RID: 253
		private bool _isDisposed;
	}
}
