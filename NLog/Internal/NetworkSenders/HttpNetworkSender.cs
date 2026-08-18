using System;
using System.IO;
using System.Net;
using NLog.Common;

namespace NLog.Internal.NetworkSenders
{
	// Token: 0x0200009B RID: 155
	internal class HttpNetworkSender : NetworkSender
	{
		// Token: 0x06000503 RID: 1283 RVA: 0x0000AC16 File Offset: 0x00008E16
		public HttpNetworkSender(string url) : base(url)
		{
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000AD1C File Offset: 0x00008F1C
		protected override void DoSend(byte[] bytes, int offset, int length, AsyncContinuation asyncContinuation)
		{
			WebRequest webRequest = WebRequest.Create(new Uri(base.Address));
			webRequest.Method = "POST";
			AsyncCallback onResponse = delegate(IAsyncResult r)
			{
				try
				{
					using (webRequest.EndGetResponse(r))
					{
					}
					asyncContinuation(null);
				}
				catch (Exception exception)
				{
					if (exception.MustBeRethrown())
					{
						throw;
					}
					asyncContinuation(exception);
				}
			};
			AsyncCallback callback = delegate(IAsyncResult r)
			{
				try
				{
					using (Stream stream = webRequest.EndGetRequestStream(r))
					{
						stream.Write(bytes, offset, length);
					}
					webRequest.BeginGetResponse(onResponse, null);
				}
				catch (Exception exception)
				{
					if (exception.MustBeRethrown())
					{
						throw;
					}
					asyncContinuation(exception);
				}
			};
			webRequest.BeginGetRequestStream(callback, null);
		}
	}
}
