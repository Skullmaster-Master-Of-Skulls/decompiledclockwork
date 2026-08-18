using System;
using System.Net.Mime;

namespace System.Net.Mail
{
	// Token: 0x0200027C RID: 636
	internal static class CheckCommand
	{
		// Token: 0x060017F9 RID: 6137 RVA: 0x0007A6C8 File Offset: 0x000788C8
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

		// Token: 0x060017FA RID: 6138 RVA: 0x0007A760 File Offset: 0x00078960
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

		// Token: 0x060017FB RID: 6139 RVA: 0x0007A7A0 File Offset: 0x000789A0
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
			}
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x0007A81C File Offset: 0x00078A1C
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
			}
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x0007A874 File Offset: 0x00078A74
		internal static SmtpStatusCode Send(SmtpConnection conn, out string response)
		{
			conn.Flush();
			SmtpReplyReader nextReplyReader = conn.Reader.GetNextReplyReader();
			LineInfo lineInfo = nextReplyReader.ReadLine();
			response = lineInfo.Line;
			nextReplyReader.Close();
			return lineInfo.StatusCode;
		}

		// Token: 0x04001825 RID: 6181
		private static AsyncCallback onReadLine = new AsyncCallback(CheckCommand.OnReadLine);

		// Token: 0x04001826 RID: 6182
		private static AsyncCallback onWrite = new AsyncCallback(CheckCommand.OnWrite);
	}
}
