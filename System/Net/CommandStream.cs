using System;
using System.IO;
using System.Text;

namespace System.Net
{
	// Token: 0x020004BA RID: 1210
	internal class CommandStream : PooledStream
	{
		// Token: 0x0600259A RID: 9626 RVA: 0x00095948 File Offset: 0x00094948
		internal CommandStream(ConnectionPool connectionPool, TimeSpan lifetime, bool checkLifetime) : base(connectionPool, lifetime, checkLifetime)
		{
			this.m_Decoder = this.m_Encoding.GetDecoder();
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x0009597C File Offset: 0x0009497C
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

		// Token: 0x0600259C RID: 9628 RVA: 0x000959EC File Offset: 0x000949EC
		protected override void Dispose(bool disposing)
		{
			this.InvokeRequestCallback(null);
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x000959F8 File Offset: 0x000949F8
		protected void InvokeRequestCallback(object obj)
		{
			WebRequest request = this.m_Request;
			if (request != null)
			{
				request.RequestCallback(obj);
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x0600259E RID: 9630 RVA: 0x00095A16 File Offset: 0x00094A16
		internal bool RecoverableFailure
		{
			get
			{
				return this.m_RecoverableFailure;
			}
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x00095A1E File Offset: 0x00094A1E
		protected void MarkAsRecoverableFailure()
		{
			if (this.m_Index <= 1)
			{
				this.m_RecoverableFailure = true;
			}
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x00095A30 File Offset: 0x00094A30
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

		// Token: 0x060025A1 RID: 9633 RVA: 0x00095A79 File Offset: 0x00094A79
		protected virtual void ClearState()
		{
			this.InitCommandPipeline(null, null, false);
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x00095A84 File Offset: 0x00094A84
		protected virtual CommandStream.PipelineEntry[] BuildCommandsList(WebRequest request)
		{
			return null;
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x00095A87 File Offset: 0x00094A87
		protected Exception GenerateException(WebExceptionStatus status, Exception innerException)
		{
			return new WebException(NetRes.GetWebStatusString("net_connclosed", status), innerException, status, null);
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x00095A9C File Offset: 0x00094A9C
		protected Exception GenerateException(FtpStatusCode code, string statusDescription, Exception innerException)
		{
			return new WebException(SR.GetString("net_servererror", new object[]
			{
				NetRes.GetWebStatusCodeString(code, statusDescription)
			}), innerException, WebExceptionStatus.ProtocolError, null);
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x00095AD0 File Offset: 0x00094AD0
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

		// Token: 0x060025A6 RID: 9638 RVA: 0x00095B28 File Offset: 0x00094B28
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
			catch
			{
				this.Abort(new Exception(SR.GetString("net_nonClsCompliantException")));
			}
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x00095B88 File Offset: 0x00094B88
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

		// Token: 0x060025A8 RID: 9640 RVA: 0x00095D0C File Offset: 0x00094D0C
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

		// Token: 0x060025A9 RID: 9641 RVA: 0x00095D8C File Offset: 0x00094D8C
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

		// Token: 0x060025AA RID: 9642 RVA: 0x00095EA5 File Offset: 0x00094EA5
		protected virtual CommandStream.PipelineInstruction PipelineCallback(CommandStream.PipelineEntry entry, ResponseDescription response, bool timeout, ref Stream stream)
		{
			return CommandStream.PipelineInstruction.Abort;
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x00095EA8 File Offset: 0x00094EA8
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

		// Token: 0x060025AC RID: 9644 RVA: 0x00095F3C File Offset: 0x00094F3C
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

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x060025AD RID: 9645 RVA: 0x00095FB4 File Offset: 0x00094FB4
		// (set) Token: 0x060025AE RID: 9646 RVA: 0x00095FBC File Offset: 0x00094FBC
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

		// Token: 0x060025AF RID: 9647 RVA: 0x00095FD6 File Offset: 0x00094FD6
		protected virtual bool CheckValid(ResponseDescription response, ref int validThrough, ref int completeLength)
		{
			return false;
		}

		// Token: 0x060025B0 RID: 9648 RVA: 0x00095FDC File Offset: 0x00094FDC
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

		// Token: 0x060025B1 RID: 9649 RVA: 0x000960AC File Offset: 0x000950AC
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
					goto IL_16A;
				}
				goto IL_16A;
			}
			throw this.GenerateException(WebExceptionStatus.ServerProtocolViolation, null);
			Block_3:
			throw this.GenerateException(WebExceptionStatus.ServerProtocolViolation, null);
			Block_4:
			throw this.GenerateException(WebExceptionStatus.ServerProtocolViolation, null);
			IL_16A:
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

		// Token: 0x04002535 RID: 9525
		private const int _WaitingForPipeline = 1;

		// Token: 0x04002536 RID: 9526
		private const int _CompletedPipeline = 2;

		// Token: 0x04002537 RID: 9527
		private static readonly AsyncCallback m_WriteCallbackDelegate = new AsyncCallback(CommandStream.WriteCallback);

		// Token: 0x04002538 RID: 9528
		private static readonly AsyncCallback m_ReadCallbackDelegate = new AsyncCallback(CommandStream.ReadCallback);

		// Token: 0x04002539 RID: 9529
		private bool m_RecoverableFailure;

		// Token: 0x0400253A RID: 9530
		protected WebRequest m_Request;

		// Token: 0x0400253B RID: 9531
		protected bool m_Async;

		// Token: 0x0400253C RID: 9532
		private bool m_Aborted;

		// Token: 0x0400253D RID: 9533
		protected CommandStream.PipelineEntry[] m_Commands;

		// Token: 0x0400253E RID: 9534
		protected int m_Index;

		// Token: 0x0400253F RID: 9535
		private bool m_DoRead;

		// Token: 0x04002540 RID: 9536
		private bool m_DoSend;

		// Token: 0x04002541 RID: 9537
		private ResponseDescription m_CurrentResponseDescription;

		// Token: 0x04002542 RID: 9538
		protected string m_AbortReason;

		// Token: 0x04002543 RID: 9539
		private string m_Buffer = string.Empty;

		// Token: 0x04002544 RID: 9540
		private Encoding m_Encoding = Encoding.UTF8;

		// Token: 0x04002545 RID: 9541
		private Decoder m_Decoder;

		// Token: 0x020004BB RID: 1211
		internal enum PipelineInstruction
		{
			// Token: 0x04002547 RID: 9543
			Abort,
			// Token: 0x04002548 RID: 9544
			Advance,
			// Token: 0x04002549 RID: 9545
			Pause,
			// Token: 0x0400254A RID: 9546
			Reread,
			// Token: 0x0400254B RID: 9547
			GiveStream
		}

		// Token: 0x020004BC RID: 1212
		[Flags]
		internal enum PipelineEntryFlags
		{
			// Token: 0x0400254D RID: 9549
			UserCommand = 1,
			// Token: 0x0400254E RID: 9550
			GiveDataStream = 2,
			// Token: 0x0400254F RID: 9551
			CreateDataConnection = 4,
			// Token: 0x04002550 RID: 9552
			DontLogParameter = 8
		}

		// Token: 0x020004BD RID: 1213
		internal class PipelineEntry
		{
			// Token: 0x060025B3 RID: 9651 RVA: 0x000962F0 File Offset: 0x000952F0
			internal PipelineEntry(string command)
			{
				this.Command = command;
			}

			// Token: 0x060025B4 RID: 9652 RVA: 0x000962FF File Offset: 0x000952FF
			internal PipelineEntry(string command, CommandStream.PipelineEntryFlags flags)
			{
				this.Command = command;
				this.Flags = flags;
			}

			// Token: 0x060025B5 RID: 9653 RVA: 0x00096315 File Offset: 0x00095315
			internal bool HasFlag(CommandStream.PipelineEntryFlags flags)
			{
				return (this.Flags & flags) != (CommandStream.PipelineEntryFlags)0;
			}

			// Token: 0x04002551 RID: 9553
			internal string Command;

			// Token: 0x04002552 RID: 9554
			internal CommandStream.PipelineEntryFlags Flags;
		}
	}
}
