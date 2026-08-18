using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Net
{
	// Token: 0x020004C8 RID: 1224
	internal class ConnectionReturnResult
	{
		// Token: 0x060025C0 RID: 9664 RVA: 0x00096486 File Offset: 0x00095486
		internal ConnectionReturnResult()
		{
			this.m_Context = new List<ConnectionReturnResult.RequestContext>(5);
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x0009649A File Offset: 0x0009549A
		internal ConnectionReturnResult(int capacity)
		{
			this.m_Context = new List<ConnectionReturnResult.RequestContext>(capacity);
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x060025C2 RID: 9666 RVA: 0x000964AE File Offset: 0x000954AE
		internal bool IsNotEmpty
		{
			get
			{
				return this.m_Context.Count != 0;
			}
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x000964C1 File Offset: 0x000954C1
		internal static void Add(ref ConnectionReturnResult returnResult, HttpWebRequest request, CoreResponseData coreResponseData)
		{
			if (coreResponseData == null)
			{
				throw new InternalException();
			}
			if (returnResult == null)
			{
				returnResult = new ConnectionReturnResult();
			}
			returnResult.m_Context.Add(new ConnectionReturnResult.RequestContext(request, coreResponseData));
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x000964EA File Offset: 0x000954EA
		internal static void AddExceptionRange(ref ConnectionReturnResult returnResult, HttpWebRequest[] requests, Exception exception)
		{
			ConnectionReturnResult.AddExceptionRange(ref returnResult, requests, exception, exception);
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x000964F8 File Offset: 0x000954F8
		internal static void AddExceptionRange(ref ConnectionReturnResult returnResult, HttpWebRequest[] requests, Exception exception, Exception firstRequestException)
		{
			if (exception == null)
			{
				throw new InternalException();
			}
			if (returnResult == null)
			{
				returnResult = new ConnectionReturnResult(requests.Length);
			}
			for (int i = 0; i < requests.Length; i++)
			{
				if (i == 0)
				{
					returnResult.m_Context.Add(new ConnectionReturnResult.RequestContext(requests[i], firstRequestException));
				}
				else
				{
					returnResult.m_Context.Add(new ConnectionReturnResult.RequestContext(requests[i], exception));
				}
			}
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x0009655C File Offset: 0x0009555C
		internal static void SetResponses(ConnectionReturnResult returnResult)
		{
			if (returnResult == null)
			{
				return;
			}
			for (int i = 0; i < returnResult.m_Context.Count; i++)
			{
				try
				{
					HttpWebRequest request = returnResult.m_Context[i].Request;
					request.SetAndOrProcessResponse(returnResult.m_Context[i].CoreResponse);
				}
				catch (Exception)
				{
					returnResult.m_Context.RemoveRange(0, i + 1);
					if (returnResult.m_Context.Count > 0)
					{
						ThreadPool.UnsafeQueueUserWorkItem(ConnectionReturnResult.s_InvokeConnectionCallback, returnResult);
					}
					throw;
				}
			}
			returnResult.m_Context.Clear();
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x000965F8 File Offset: 0x000955F8
		private static void InvokeConnectionCallback(object objectReturnResult)
		{
			ConnectionReturnResult responses = (ConnectionReturnResult)objectReturnResult;
			ConnectionReturnResult.SetResponses(responses);
		}

		// Token: 0x04002582 RID: 9602
		private static readonly WaitCallback s_InvokeConnectionCallback = new WaitCallback(ConnectionReturnResult.InvokeConnectionCallback);

		// Token: 0x04002583 RID: 9603
		private List<ConnectionReturnResult.RequestContext> m_Context;

		// Token: 0x020004C9 RID: 1225
		private struct RequestContext
		{
			// Token: 0x060025C9 RID: 9673 RVA: 0x00096625 File Offset: 0x00095625
			internal RequestContext(HttpWebRequest request, object coreResponse)
			{
				this.Request = request;
				this.CoreResponse = coreResponse;
			}

			// Token: 0x04002584 RID: 9604
			internal HttpWebRequest Request;

			// Token: 0x04002585 RID: 9605
			internal object CoreResponse;
		}
	}
}
