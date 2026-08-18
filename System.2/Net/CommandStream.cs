using System;
using System.IO;
using System.Text;

namespace System.Net
{
	// Token: 0x02000198 RID: 408
	internal class CommandStream : PooledStream
	{
		// Token: 0x06000FDD RID: 4061 RVA: 0x00052F03 File Offset: 0x00051103
		internal CommandStream(ConnectionPool connectionPool, TimeSpan lifetime, bool checkLifetime) : base(connectionPool, lifetime, checkLifetime)
		{
			this.m_Decoder = this.m_Encoding.GetDecoder();
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x00052F38 File Offset: 0x00051138
		internal virtual void Abort(Exception e)
		{
			lock (this)
			{
				if (this.m_Aborted)
				{
					return;
				}
				this.m_Aborted = true;
				base.CanBePooled = false;
			}
			try
			{
				base.Close(0);
			}
			finally
			{
				if (e != null)
				{
					this.InvokeRequestCallback(e);
				}
				else
				{
					this.InvokeRequestCallback(null);
				}
			}
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x00052FB0 File Offset: 0x000511B0
		protected override void Dispose(bool disposing)
		{
			this.InvokeRequestCallback(null);
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x00052FBC File Offset: 0x000511BC
		protected void InvokeRequestCallback(object obj)
		{
			WebRequest request = this.m_Request;
			if (request != null)
			{
				request.RequestCallback(obj);
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x00052FDA File Offset: 0x000511DA
		internal bool RecoverableFailure
		{
			get
			{
				return this.m_RecoverableFailure;
			}
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x00052FE2 File Offset: 0x000511E2
		protected void MarkAsRecoverableFailure()
		{
			if (this.m_Index <= 1)
			{
				this.m_RecoverableFailure = true;
			}
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x00052FF4 File Offset: 0x000511F4
		internal Stream SubmitRequest(WebRequest request, bool async, bool readInitalResponseOnConnect)
		{
			this.ClearState();
			base.UpdateLifetime();
			CommandStream.PipelineEntry[] commands = this.BuildCommandsList(request);
			this.InitCommandPipeline(request, commands, async);
			if (readInitalResponseOnConnect && base.JustConnected)
			{
				this.m_DoSend = false;
				this.m_Index = -1;
			}
			return this.ContinueCommandPipeline();
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x0005303D File Offset: 0x0005123D
		protected virtual void ClearState()
		{
			this.InitCommandPipeline(null, null, false);
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x00053048 File Offset: 0x00051248
		protected virtual CommandStream.PipelineEntry[] BuildCommandsList(WebRequest request)
		{
			return null;
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0005304B File Offset: 0x0005124B
		protected Exception GenerateException(WebExceptionStatus status, Exception innerException)
		{
			return new WebException(NetRes.GetWebStatusString("net_connclosed", status), innerException, status, null);
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x00053060 File Offset: 0x00051260
		protected Exception GenerateException(FtpStatusCode code, string statusDescription, Exception innerException)
		{
			return new WebException(SR.GetString("net_servererror", new object[]
			{
				NetRes.GetWebStatusCodeString(code, statusDescription)
			}), innerException, WebExceptionStatus.ProtocolError, null);
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x00053084 File Offset: 0x00051284
		protected void InitCommandPipeline(WebRequest request, CommandStream.PipelineEntry[] commands, bool async)
		{
			this.m_Commands = commands;
			this.m_Index = 0;
			this.m_Request = request;
			this.m_Aborted = false;
			this.m_DoRead = true;
			this.m_DoSend = true;
			this.m_CurrentResponseDescription = null;
			this.m_Async = async;
			this.m_RecoverableFailure = false;
			this.m_AbortReason = string.Empty;
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x000530DC File Offset: 0x000512DC
		internal void CheckContinuePipeline()
		{
			if (this.m_Async)
			{
				return;
			}
			try
			{
				this.ContinueCommandPipeline();
			}
			catch (Exception e)
			{
				this.Abort(e);
			}
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x00053118 File Offset: 0x00051318
		protected Stream ContinueCommandPipeline()
		{
			bool async = this.m_Async;
			while (this.m_Index < this.m_Commands.Length)
			{
				if (this.m_DoSend)
				{
					if (this.m_Index < 0)
					{
						throw new InternalException();
					}
					byte[] bytes = this.Encoding.GetBytes(this.m_Commands[this.m_Index].Command);
					if (Logging.On)
					{
						string text = this.m_Commands[this.m_Index].Command.Substring(0, this.m_Commands[this.m_Index].Command.Length - 2);
						if (this.m_Commands[this.m_Index].HasFlag(CommandStream.PipelineEntryFlags.DontLogParameter))
						{
							int num = text.IndexOf(' ');
							if (num != -1)
							{
								text = text.Substring(0, num) + " ********";
							}
						}
						Logging.PrintInfo(Logging.Web, this, SR.GetString("net_log_sending_command", new object[]
						{
							text
						}));
					}
					try
					{
						if (async)
						{
							this.BeginWrite(bytes, 0, bytes.Length, CommandStream.m_WriteCallbackDelegate, this);
						}
						else
						{
							this.Write(bytes, 0, bytes.Length);
						}
					}
					catch (IOException)
					{
						this.MarkAsRecoverableFailure();
						throw;
					}
					catch
					{
						throw;
					}
					if (async)
					{
						return null;
					}
				}
				Stream result = null;
				bool flag = this.PostSendCommandProcessing(ref result);
				if (flag)
				{
					return result;
				}
			}
			lock (this)
			{
				this.Close();
			}
			return null;
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x000532A8 File Offset: 0x000514A8
		private bool PostSendCommandProcessing(ref Stream stream)
		{
			if (this.m_DoRead)
			{
				bool async = this.m_Async;
				int index = this.m_Index;
				CommandStream.PipelineEntry[] commands = this.m_Commands;
				try
				{
					ResponseDescription currentResponseDescription = this.ReceiveCommandResponse();
					if (async)
					{
						return true;
					}
					this.m_CurrentResponseDescription = currentResponseDescription;
				}
				catch
				{
					if (index < 0 || index >= commands.Length || commands[index].Command != "QUIT\r\n")
					{
						throw;
					}
				}
			}
			return this.PostReadCommandProcessing(ref stream);
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00053328 File Offset: 0x00051528
		private bool PostReadCommandProcessing(ref Stream stream)
		{
			if (this.m_Index >= this.m_Commands.Length)
			{
				return false;
			}
			this.m_DoSend = false;
			this.m_DoRead = false;
			CommandStream.PipelineEntry pipelineEntry;
			if (this.m_Index == -1)
			{
				pipelineEntry = null;
			}
			else
			{
				pipelineEntry = this.m_Commands[this.m_Index];
			}
			CommandStream.PipelineInstruction pipelineInstruction;
			if (this.m_CurrentResponseDescription == null && pipelineEntry.Command == "QUIT\r\n")
			{
				pipelineInstruction = CommandStream.PipelineInstruction.Advance;
			}
			else
			{
				pipelineInstruction = this.PipelineCallback(pipelineEntry, this.m_CurrentResponseDescription, false, ref stream);
			}
			if (pipelineInstruction == CommandStream.PipelineInstruction.Abort)
			{
				Exception ex;
				if (this.m_AbortReason != string.Empty)
				{
					ex = new WebException(this.m_AbortReason);
				}
				else
				{
					ex = this.GenerateException(WebExceptionStatus.ServerProtocolViolation, null);
				}
				this.Abort(ex);
				throw ex;
			}
			if (pipelineInstruction == CommandStream.PipelineInstruction.Advance)
			{
				this.m_CurrentResponseDescription = null;
				this.m_DoSend = true;
				this.m_DoRead = true;
				this.m_Index++;
			}
			else
			{
				if (pipelineInstruction == CommandStream.PipelineInstruction.Pause)
				{
					return true;
				}
				if (pipelineInstruction == CommandStream.PipelineInstruction.GiveStream)
				{
					this.m_CurrentResponseDescription = null;
					this.m_DoRead = true;
					if (this.m_Async)
					{
						this.ContinueCommandPipeline();
						this.InvokeRequestCallback(stream);
					}
					return true;
				}
				if (pipelineInstruction == CommandStream.PipelineInstruction.Reread)
				{
					this.m_CurrentResponseDescription = null;
					this.m_DoRead = true;
				}
			}
			return false;
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x00053441 File Offset: 0x00051641
		protected virtual CommandStream.PipelineInstruction PipelineCallback(CommandStream.PipelineEntry entry, ResponseDescription response, bool timeout, ref Stream stream)
		{
			return CommandStream.PipelineInstruction.Abort;
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x00053444 File Offset: 0x00051644
		private static void ReadCallback(IAsyncResult asyncResult)
		{
			ReceiveState receiveState = (ReceiveState)asyncResult.AsyncState;
			try
			{
				Stream connection = receiveState.Connection;
				int num = 0;
				try
				{
					num = connection.EndRead(asyncResult);
					if (num == 0)
					{
						receiveState.Connection.CloseSocket();
					}
				}
				catch (IOException)
				{
					receiveState.Connection.MarkAsRecoverableFailure();
					throw;
				}
				catch
				{
					throw;
				}
				receiveState.Connection.ReceiveCommandResponseCallback(receiveState, num);
			}
			catch (Exception e)
			{
				receiveState.Connection.Abort(e);
			}
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x000534D8 File Offset: 0x000516D8
		private static void WriteCallback(IAsyncResult asyncResult)
		{
			CommandStream commandStream = (CommandStream)asyncResult.AsyncState;
			try
			{
				try
				{
					commandStream.EndWrite(asyncResult);
				}
				catch (IOException)
				{
					commandStream.MarkAsRecoverableFailure();
					throw;
				}
				catch
				{
					throw;
				}
				Stream stream = null;
				if (!commandStream.PostSendCommandProcessing(ref stream))
				{
					commandStream.ContinueCommandPipeline();
				}
			}
			catch (Exception e)
			{
				commandStream.Abort(e);
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000FF0 RID: 4080 RVA: 0x00053550 File Offset: 0x00051750
		// (set) Token: 0x06000FF1 RID: 4081 RVA: 0x00053558 File Offset: 0x00051758
		protected Encoding Encoding
		{
			get
			{
				return this.m_Encoding;
			}
			set
			{
				this.m_Encoding = value;
				this.m_Decoder = this.m_Encoding.GetDecoder();
			}
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x00053572 File Offset: 0x00051772
		protected virtual bool CheckValid(ResponseDescription response, ref int validThrough, ref int completeLength)
		{
			return false;
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x00053578 File Offset: 0x00051778
		private ResponseDescription ReceiveCommandResponse()
		{
			ReceiveState receiveState = new ReceiveState(this);
			try
			{
				if (this.m_Buffer.Length > 0)
				{
					this.ReceiveCommandResponseCallback(receiveState, -1);
				}
				else
				{
					try
					{
						if (this.m_Async)
						{
							this.BeginRead(receiveState.Buffer, 0, receiveState.Buffer.Length, CommandStream.m_ReadCallbackDelegate, receiveState);
							return null;
						}
						int num = this.Read(receiveState.Buffer, 0, receiveState.Buffer.Length);
						if (num == 0)
						{
							base.CloseSocket();
						}
						this.ReceiveCommandResponseCallback(receiveState, num);
					}
					catch (IOException)
					{
						this.MarkAsRecoverableFailure();
						throw;
					}
					catch
					{
						throw;
					}
				}
			}
			catch (Exception ex)
			{
				if (ex is WebException)
				{
					throw;
				}
				throw this.GenerateException(WebExceptionStatus.ReceiveFailure, ex);
			}
			return receiveState.Resp;
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x00053648 File Offset: 0x00051848
		private void ReceiveCommandResponseCallback(ReceiveState state, int bytesRead)
		{
			int num = -1;
			for (;;)
			{
				int validThrough = state.ValidThrough;
				if (this.m_Buffer.Length > 0)
				{
					state.Resp.StatusBuffer.Append(this.m_Buffer);
					this.m_Buffer = string.Empty;
					if (!this.CheckValid(state.Resp, ref validThrough, ref num))
					{
						break;
					}
				}
				else
				{
					if (bytesRead <= 0)
					{
						goto Block_3;
					}
					char[] array = new char[this.m_Decoder.GetCharCount(state.Buffer, 0, bytesRead)];
					int chars = this.m_Decoder.GetChars(state.Buffer, 0, bytesRead, array, 0, false);
					string text = new string(array, 0, chars);
					state.Resp.StatusBuffer.Append(text);
					if (!this.CheckValid(state.Resp, ref validThrough, ref num))
					{
						goto Block_4;
					}
					if (num >= 0)
					{
						int num2 = state.Resp.StatusBuffer.Length - num;
						if (num2 > 0)
						{
							this.m_Buffer = text.Substring(text.Length - num2, num2);
						}
					}
				}
				if (num < 0)
				{
					state.ValidThrough = validThrough;
					try
					{
						if (this.m_Async)
						{
							this.BeginRead(state.Buffer, 0, state.Buffer.Length, CommandStream.m_ReadCallbackDelegate, state);
							return;
						}
						bytesRead = this.Read(state.Buffer, 0, state.Buffer.Length);
						if (bytesRead == 0)
						{
							base.CloseSocket();
						}
						continue;
					}
					catch (IOException)
					{
						this.MarkAsRecoverableFailure();
						throw;
					}
					catch
					{
						throw;
					}
					goto IL_16C;
				}
				goto IL_16C;
			}
			throw this.GenerateException(WebExceptionStatus.ServerProtocolViolation, null);
			Block_3:
			throw this.GenerateException(WebExceptionStatus.ServerProtocolViolation, null);
			Block_4:
			throw this.GenerateException(WebExceptionStatus.ServerProtocolViolation, null);
			IL_16C:
			string text2 = state.Resp.StatusBuffer.ToString();
			state.Resp.StatusDescription = text2.Substring(0, num);
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, this, SR.GetString("net_log_received_response", new object[]
				{
					text2.Substring(0, num - 2)
				}));
			}
			if (this.m_Async)
			{
				if (state.Resp != null)
				{
					this.m_CurrentResponseDescription = state.Resp;
				}
				Stream stream = null;
				if (this.PostReadCommandProcessing(ref stream))
				{
					return;
				}
				this.ContinueCommandPipeline();
			}
		}

		// Token: 0x040012FB RID: 4859
		private static readonly AsyncCallback m_WriteCallbackDelegate = new AsyncCallback(CommandStream.WriteCallback);

		// Token: 0x040012FC RID: 4860
		private static readonly AsyncCallback m_ReadCallbackDelegate = new AsyncCallback(CommandStream.ReadCallback);

		// Token: 0x040012FD RID: 4861
		private bool m_RecoverableFailure;

		// Token: 0x040012FE RID: 4862
		protected WebRequest m_Request;

		// Token: 0x040012FF RID: 4863
		protected bool m_Async;

		// Token: 0x04001300 RID: 4864
		private bool m_Aborted;

		// Token: 0x04001301 RID: 4865
		protected CommandStream.PipelineEntry[] m_Commands;

		// Token: 0x04001302 RID: 4866
		protected int m_Index;

		// Token: 0x04001303 RID: 4867
		private bool m_DoRead;

		// Token: 0x04001304 RID: 4868
		private bool m_DoSend;

		// Token: 0x04001305 RID: 4869
		private ResponseDescription m_CurrentResponseDescription;

		// Token: 0x04001306 RID: 4870
		protected string m_AbortReason;

		// Token: 0x04001307 RID: 4871
		private const int _WaitingForPipeline = 1;

		// Token: 0x04001308 RID: 4872
		private const int _CompletedPipeline = 2;

		// Token: 0x04001309 RID: 4873
		private string m_Buffer = string.Empty;

		// Token: 0x0400130A RID: 4874
		private Encoding m_Encoding = Encoding.UTF8;

		// Token: 0x0400130B RID: 4875
		private Decoder m_Decoder;

		// Token: 0x02000744 RID: 1860
		internal enum PipelineInstruction
		{
			// Token: 0x040031DE RID: 12766
			Abort,
			// Token: 0x040031DF RID: 12767
			Advance,
			// Token: 0x040031E0 RID: 12768
			Pause,
			// Token: 0x040031E1 RID: 12769
			Reread,
			// Token: 0x040031E2 RID: 12770
			GiveStream
		}

		// Token: 0x02000745 RID: 1861
		[Flags]
		internal enum PipelineEntryFlags
		{
			// Token: 0x040031E4 RID: 12772
			UserCommand = 1,
			// Token: 0x040031E5 RID: 12773
			GiveDataStream = 2,
			// Token: 0x040031E6 RID: 12774
			CreateDataConnection = 4,
			// Token: 0x040031E7 RID: 12775
			DontLogParameter = 8
		}

		// Token: 0x02000746 RID: 1862
		internal class PipelineEntry
		{
			// Token: 0x060041ED RID: 16877 RVA: 0x0011233D File Offset: 0x0011053D
			internal PipelineEntry(string command)
			{
				this.Command = command;
			}

			// Token: 0x060041EE RID: 16878 RVA: 0x0011234C File Offset: 0x0011054C
			internal PipelineEntry(string command, CommandStream.PipelineEntryFlags flags)
			{
				this.Command = command;
				this.Flags = flags;
			}

			// Token: 0x060041EF RID: 16879 RVA: 0x00112362 File Offset: 0x00110562
			internal bool HasFlag(CommandStream.PipelineEntryFlags flags)
			{
				return (this.Flags & flags) > (CommandStream.PipelineEntryFlags)0;
			}

			// Token: 0x040031E8 RID: 12776
			internal string Command;

			// Token: 0x040031E9 RID: 12777
			internal CommandStream.PipelineEntryFlags Flags;
		}
	}
}
