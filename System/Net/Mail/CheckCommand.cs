using System;
using System.Net.Mime;

namespace System.Net.Mail
{
	// Token: 0x020006BC RID: 1724
	internal static class CheckCommand
	{
		// Token: 0x06003559 RID: 13657 RVA: 0x000E32E8 File Offset: 0x000E22E8
		internal static IAsyncResult BeginSend(SmtpConnection conn, AsyncCallback callback, object state)
		{
			MultiAsyncResult multiAsyncResult = new MultiAsyncResult(conn, callback, state);
			multiAsyncResult.Enter();
			IAsyncResult asyncResult = conn.BeginFlush(CheckCommand.onWrite, multiAsyncResult);
			if (asyncResult.CompletedSynchronously)
			{
				conn.EndFlush(asyncResult);
				multiAsyncResult.Leave();
			}
			SmtpReplyReader nextReplyReader = conn.Reader.GetNextReplyReader();
			multiAsyncResult.Enter();
			IAsyncResult asyncResult2 = nextReplyReader.BeginReadLine(CheckCommand.onReadLine, multiAsyncResult);
			if (asyncResult2.CompletedSynchronously)
			{
				LineInfo lineInfo = nextReplyReader.EndReadLine(asyncResult2);
				if (!(multiAsyncResult.Result is Exception))
				{
					multiAsyncResult.Result = lineInfo;
				}
				multiAsyncResult.Leave();
			}
			multiAsyncResult.CompleteSequence();
			return multiAsyncResult;
		}

		// Token: 0x0600355A RID: 13658 RVA: 0x000E3380 File Offset: 0x000E2380
		internal static object EndSend(IAsyncResult result, out string response)
		{
			object obj = MultiAsyncResult.End(result);
			if (obj is Exception)
			{
				throw (Exception)obj;
			}
			LineInfo lineInfo = (LineInfo)obj;
			response = lineInfo.Line;
			return lineInfo.StatusCode;
		}

		// Token: 0x0600355B RID: 13659 RVA: 0x000E33C0 File Offset: 0x000E23C0
		private static void OnReadLine(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				MultiAsyncResult multiAsyncResult = (MultiAsyncResult)result.AsyncState;
				try
				{
					SmtpConnection smtpConnection = (SmtpConnection)multiAsyncResult.Context;
					LineInfo lineInfo = smtpConnection.Reader.CurrentReader.EndReadLine(result);
					if (!(multiAsyncResult.Result is Exception))
					{
						multiAsyncResult.Result = lineInfo;
					}
					multiAsyncResult.Leave();
				}
				catch (Exception result2)
				{
					multiAsyncResult.Leave(result2);
				}
				catch
				{
					multiAsyncResult.Leave(new Exception(SR.GetString("net_nonClsCompliantException")));
				}
			}
		}

		// Token: 0x0600355C RID: 13660 RVA: 0x000E3460 File Offset: 0x000E2460
		private static void OnWrite(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				MultiAsyncResult multiAsyncResult = (MultiAsyncResult)result.AsyncState;
				try
				{
					SmtpConnection smtpConnection = (SmtpConnection)multiAsyncResult.Context;
					smtpConnection.EndFlush(result);
					multiAsyncResult.Leave();
				}
				catch (Exception result2)
				{
					multiAsyncResult.Leave(result2);
				}
				catch
				{
					multiAsyncResult.Leave(new Exception(SR.GetString("net_nonClsCompliantException")));
				}
			}
		}

		// Token: 0x0600355D RID: 13661 RVA: 0x000E34DC File Offset: 0x000E24DC
		internal static SmtpStatusCode Send(SmtpConnection conn, out string response)
		{
			conn.Flush();
			SmtpReplyReader nextReplyReader = conn.Reader.GetNextReplyReader();
			LineInfo lineInfo = nextReplyReader.ReadLine();
			response = lineInfo.Line;
			nextReplyReader.Close();
			return lineInfo.StatusCode;
		}

		// Token: 0x040030D7 RID: 12503
		private static AsyncCallback onReadLine = new AsyncCallback(CheckCommand.OnReadLine);

		// Token: 0x040030D8 RID: 12504
		private static AsyncCallback onWrite = new AsyncCallback(CheckCommand.OnWrite);
	}
}
