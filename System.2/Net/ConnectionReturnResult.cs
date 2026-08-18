using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Net
{
	// Token: 0x020001A2 RID: 418
	internal class ConnectionReturnResult
	{
		// Token: 0x06000FFE RID: 4094 RVA: 0x0005397F File Offset: 0x00051B7F
		internal ConnectionReturnResult()
		{
			this.m_Context = new List<ConnectionReturnResult.RequestContext>(5);
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x00053993 File Offset: 0x00051B93
		internal ConnectionReturnResult(int capacity)
		{
			this.m_Context = new List<ConnectionReturnResult.RequestContext>(capacity);
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001000 RID: 4096 RVA: 0x000539A7 File Offset: 0x00051BA7
		internal bool IsNotEmpty
		{
			get
			{
				return this.m_Context.Count != 0;
			}
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x000539B7 File Offset: 0x00051BB7
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

		// Token: 0x06001002 RID: 4098 RVA: 0x000539E0 File Offset: 0x00051BE0
		internal static void AddExceptionRange(ref ConnectionReturnResult returnResult, HttpWebRequest[] requests, Exception exception)
		{
			ConnectionReturnResult.AddExceptionRange(ref returnResult, requests, 0, exception, exception);
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x000539EC File Offset: 0x00051BEC
		internal static void AddExceptionRange(ref ConnectionReturnResult returnResult, HttpWebRequest[] requests, int abortedPipelinedRequestIndex, Exception exception, Exception firstRequestException)
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
				if (i == abortedPipelinedRequestIndex)
				{
					returnResult.m_Context.Add(new ConnectionReturnResult.RequestContext(requests[i], firstRequestException));
				}
				else
				{
					returnResult.m_Context.Add(new ConnectionReturnResult.RequestContext(requests[i], exception));
				}
			}
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x00053A50 File Offset: 0x00051C50
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
				catch (Exception ex)
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

		// Token: 0x06001005 RID: 4101 RVA: 0x00053AEC File Offset: 0x00051CEC
		private static void InvokeConnectionCallback(object objectReturnResult)
		{
			ConnectionReturnResult responses = (ConnectionReturnResult)objectReturnResult;
			ConnectionReturnResult.SetResponses(responses);
		}

		// Token: 0x04001338 RID: 4920
		private static readonly WaitCallback s_InvokeConnectionCallback = new WaitCallback(ConnectionReturnResult.InvokeConnectionCallback);

		// Token: 0x04001339 RID: 4921
		private List<ConnectionReturnResult.RequestContext> m_Context;

		// Token: 0x02000747 RID: 1863
		private struct RequestContext
		{
			// Token: 0x060041F0 RID: 16880 RVA: 0x0011236F File Offset: 0x0011056F
			internal RequestContext(HttpWebRequest request, object coreResponse)
			{
				this.Request = request;
				this.CoreResponse = coreResponse;
			}

			// Token: 0x040031EA RID: 12778
			internal HttpWebRequest Request;

			// Token: 0x040031EB RID: 12779
			internal object CoreResponse;
		}
	}
}
