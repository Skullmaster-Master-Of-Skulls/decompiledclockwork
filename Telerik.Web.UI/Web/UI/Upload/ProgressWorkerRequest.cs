using System;
using System.Web;

namespace Telerik.Web.UI.Upload
{
	// Token: 0x02001B6F RID: 7023
	internal class ProgressWorkerRequest : HttpWorkerRequest
	{
		// Token: 0x1700531A RID: 21274
		// (get) Token: 0x0601102D RID: 69677 RVA: 0x003C2225 File Offset: 0x003C0425
		private byte[] Boundary
		{
			get
			{
				return this._boundary;
			}
		}

		// Token: 0x1700531B RID: 21275
		// (get) Token: 0x0601102E RID: 69678 RVA: 0x003C222D File Offset: 0x003C042D
		internal RequestStateStore RequestStateStore
		{
			get
			{
				return this._requestStateStore;
			}
		}

		// Token: 0x1700531C RID: 21276
		// (get) Token: 0x0601102F RID: 69679 RVA: 0x003C2235 File Offset: 0x003C0435
		private RequestParser Parser
		{
			get
			{
				if (this._parser == null)
				{
					this._parser = new RequestParser(this.Boundary, this._request.ContentEncoding, this.RequestStateStore);
				}
				return this._parser;
			}
		}

		// Token: 0x06011030 RID: 69680 RVA: 0x003C2267 File Offset: 0x003C0467
		protected virtual void UpdateProgress(byte[] buffer, int validBytes)
		{
			this.Parser.Parse(buffer, validBytes);
			if (this.RequestStateStore.CurrentRequestBytesCount >= this._request.ContentLength)
			{
				this.RequestStateStore.UploadComplete = true;
			}
		}

		// Token: 0x06011031 RID: 69681 RVA: 0x003C229C File Offset: 0x003C049C
		private byte[] GetBoundary(HttpRequest request)
		{
			string contentType = request.ContentType;
			int num = contentType.IndexOf("boundary=");
			if (num <= 0)
			{
				return null;
			}
			return request.ContentEncoding.GetBytes("--" + contentType.Substring(num + "boundary=".Length));
		}

		// Token: 0x06011032 RID: 69682 RVA: 0x003C22EC File Offset: 0x003C04EC
		public override byte[] GetPreloadedEntityBody()
		{
			byte[] preloadedEntityBody = this._originalWorkerRequest.GetPreloadedEntityBody();
			if (preloadedEntityBody != null)
			{
				this.UpdateProgress(preloadedEntityBody, preloadedEntityBody.Length);
			}
			return preloadedEntityBody;
		}

		// Token: 0x06011033 RID: 69683 RVA: 0x003C2314 File Offset: 0x003C0514
		public override int GetPreloadedEntityBody(byte[] buffer, int offset)
		{
			int preloadedEntityBody = this._originalWorkerRequest.GetPreloadedEntityBody(buffer, offset);
			this.UpdateProgress(buffer, preloadedEntityBody);
			return preloadedEntityBody;
		}

		// Token: 0x06011034 RID: 69684 RVA: 0x003C2338 File Offset: 0x003C0538
		public override int ReadEntityBody(byte[] buffer, int offset, int size)
		{
			int num = this._originalWorkerRequest.ReadEntityBody(buffer, offset, size);
			this.UpdateProgress(buffer, num);
			return num;
		}

		// Token: 0x06011035 RID: 69685 RVA: 0x003C2360 File Offset: 0x003C0560
		public override int ReadEntityBody(byte[] buffer, int size)
		{
			int num = this._originalWorkerRequest.ReadEntityBody(buffer, size);
			this.UpdateProgress(buffer, num);
			return num;
		}

		// Token: 0x06011036 RID: 69686 RVA: 0x003C2384 File Offset: 0x003C0584
		public override void CloseConnection()
		{
			this._originalWorkerRequest.CloseConnection();
		}

		// Token: 0x06011037 RID: 69687 RVA: 0x003C2391 File Offset: 0x003C0591
		public override string GetAppPath()
		{
			return this._originalWorkerRequest.GetAppPath();
		}

		// Token: 0x06011038 RID: 69688 RVA: 0x003C239E File Offset: 0x003C059E
		public override string GetAppPathTranslated()
		{
			return this._originalWorkerRequest.GetAppPathTranslated();
		}

		// Token: 0x06011039 RID: 69689 RVA: 0x003C23AB File Offset: 0x003C05AB
		public override string GetAppPoolID()
		{
			return this._originalWorkerRequest.GetAppPoolID();
		}

		// Token: 0x0601103A RID: 69690 RVA: 0x003C23B8 File Offset: 0x003C05B8
		public override long GetBytesRead()
		{
			return this._originalWorkerRequest.GetBytesRead();
		}

		// Token: 0x0601103B RID: 69691 RVA: 0x003C23C5 File Offset: 0x003C05C5
		public override byte[] GetClientCertificate()
		{
			return this._originalWorkerRequest.GetClientCertificate();
		}

		// Token: 0x0601103C RID: 69692 RVA: 0x003C23D2 File Offset: 0x003C05D2
		public override byte[] GetClientCertificateBinaryIssuer()
		{
			return this._originalWorkerRequest.GetClientCertificateBinaryIssuer();
		}

		// Token: 0x0601103D RID: 69693 RVA: 0x003C23DF File Offset: 0x003C05DF
		public override int GetClientCertificateEncoding()
		{
			return this._originalWorkerRequest.GetClientCertificateEncoding();
		}

		// Token: 0x0601103E RID: 69694 RVA: 0x003C23EC File Offset: 0x003C05EC
		public override byte[] GetClientCertificatePublicKey()
		{
			return this._originalWorkerRequest.GetClientCertificatePublicKey();
		}

		// Token: 0x0601103F RID: 69695 RVA: 0x003C23F9 File Offset: 0x003C05F9
		public override DateTime GetClientCertificateValidFrom()
		{
			return this._originalWorkerRequest.GetClientCertificateValidFrom();
		}

		// Token: 0x06011040 RID: 69696 RVA: 0x003C2406 File Offset: 0x003C0606
		public override DateTime GetClientCertificateValidUntil()
		{
			return this._originalWorkerRequest.GetClientCertificateValidUntil();
		}

		// Token: 0x06011041 RID: 69697 RVA: 0x003C2413 File Offset: 0x003C0613
		public override long GetConnectionID()
		{
			return this._originalWorkerRequest.GetConnectionID();
		}

		// Token: 0x06011042 RID: 69698 RVA: 0x003C2420 File Offset: 0x003C0620
		public override string GetFilePath()
		{
			return this._originalWorkerRequest.GetFilePath();
		}

		// Token: 0x06011043 RID: 69699 RVA: 0x003C242D File Offset: 0x003C062D
		public override string GetFilePathTranslated()
		{
			return this._originalWorkerRequest.GetFilePathTranslated();
		}

		// Token: 0x06011044 RID: 69700 RVA: 0x003C243A File Offset: 0x003C063A
		public override int GetHashCode()
		{
			return this._originalWorkerRequest.GetHashCode();
		}

		// Token: 0x06011045 RID: 69701 RVA: 0x003C2447 File Offset: 0x003C0647
		public override string GetKnownRequestHeader(int index)
		{
			return this._originalWorkerRequest.GetKnownRequestHeader(index);
		}

		// Token: 0x06011046 RID: 69702 RVA: 0x003C2455 File Offset: 0x003C0655
		public override string GetPathInfo()
		{
			return this._originalWorkerRequest.GetPathInfo();
		}

		// Token: 0x06011047 RID: 69703 RVA: 0x003C2462 File Offset: 0x003C0662
		public override int GetPreloadedEntityBodyLength()
		{
			return this._originalWorkerRequest.GetPreloadedEntityBodyLength();
		}

		// Token: 0x06011048 RID: 69704 RVA: 0x003C246F File Offset: 0x003C066F
		public override string GetProtocol()
		{
			return this._originalWorkerRequest.GetProtocol();
		}

		// Token: 0x06011049 RID: 69705 RVA: 0x003C247C File Offset: 0x003C067C
		public override byte[] GetQueryStringRawBytes()
		{
			return this._originalWorkerRequest.GetQueryStringRawBytes();
		}

		// Token: 0x0601104A RID: 69706 RVA: 0x003C2489 File Offset: 0x003C0689
		public override string GetRemoteName()
		{
			return this._originalWorkerRequest.GetRemoteName();
		}

		// Token: 0x0601104B RID: 69707 RVA: 0x003C2496 File Offset: 0x003C0696
		public override int GetRequestReason()
		{
			return this._originalWorkerRequest.GetRequestReason();
		}

		// Token: 0x0601104C RID: 69708 RVA: 0x003C24A3 File Offset: 0x003C06A3
		public override string GetServerName()
		{
			return this._originalWorkerRequest.GetServerName();
		}

		// Token: 0x0601104D RID: 69709 RVA: 0x003C24B0 File Offset: 0x003C06B0
		public override string GetServerVariable(string name)
		{
			return this._originalWorkerRequest.GetServerVariable(name);
		}

		// Token: 0x0601104E RID: 69710 RVA: 0x003C24BE File Offset: 0x003C06BE
		public override int GetTotalEntityBodyLength()
		{
			return this._originalWorkerRequest.GetTotalEntityBodyLength();
		}

		// Token: 0x0601104F RID: 69711 RVA: 0x003C24CB File Offset: 0x003C06CB
		public override string GetUnknownRequestHeader(string name)
		{
			return this._originalWorkerRequest.GetUnknownRequestHeader(name);
		}

		// Token: 0x06011050 RID: 69712 RVA: 0x003C24D9 File Offset: 0x003C06D9
		public override string[][] GetUnknownRequestHeaders()
		{
			return this._originalWorkerRequest.GetUnknownRequestHeaders();
		}

		// Token: 0x06011051 RID: 69713 RVA: 0x003C24E6 File Offset: 0x003C06E6
		public override long GetUrlContextID()
		{
			return this._originalWorkerRequest.GetUrlContextID();
		}

		// Token: 0x06011052 RID: 69714 RVA: 0x003C24F3 File Offset: 0x003C06F3
		public override IntPtr GetUserToken()
		{
			return this._originalWorkerRequest.GetUserToken();
		}

		// Token: 0x06011053 RID: 69715 RVA: 0x003C2500 File Offset: 0x003C0700
		public override IntPtr GetVirtualPathToken()
		{
			return this._originalWorkerRequest.GetVirtualPathToken();
		}

		// Token: 0x06011054 RID: 69716 RVA: 0x003C250D File Offset: 0x003C070D
		public override bool HeadersSent()
		{
			return this._originalWorkerRequest.HeadersSent();
		}

		// Token: 0x06011055 RID: 69717 RVA: 0x003C251A File Offset: 0x003C071A
		public override bool IsClientConnected()
		{
			return this._originalWorkerRequest.IsClientConnected();
		}

		// Token: 0x06011056 RID: 69718 RVA: 0x003C2527 File Offset: 0x003C0727
		public override bool IsEntireEntityBodyIsPreloaded()
		{
			return this._originalWorkerRequest.IsEntireEntityBodyIsPreloaded();
		}

		// Token: 0x06011057 RID: 69719 RVA: 0x003C2534 File Offset: 0x003C0734
		public override bool IsSecure()
		{
			return this._originalWorkerRequest.IsSecure();
		}

		// Token: 0x1700531D RID: 21277
		// (get) Token: 0x06011058 RID: 69720 RVA: 0x003C2541 File Offset: 0x003C0741
		public override string MachineConfigPath
		{
			get
			{
				return this._originalWorkerRequest.MachineConfigPath;
			}
		}

		// Token: 0x1700531E RID: 21278
		// (get) Token: 0x06011059 RID: 69721 RVA: 0x003C254E File Offset: 0x003C074E
		public override string MachineInstallDirectory
		{
			get
			{
				return this._originalWorkerRequest.MachineInstallDirectory;
			}
		}

		// Token: 0x0601105A RID: 69722 RVA: 0x003C255B File Offset: 0x003C075B
		public override string MapPath(string virtualPath)
		{
			return this._originalWorkerRequest.MapPath(virtualPath);
		}

		// Token: 0x1700531F RID: 21279
		// (get) Token: 0x0601105B RID: 69723 RVA: 0x003C2569 File Offset: 0x003C0769
		public override Guid RequestTraceIdentifier
		{
			get
			{
				return this._originalWorkerRequest.RequestTraceIdentifier;
			}
		}

		// Token: 0x17005320 RID: 21280
		// (get) Token: 0x0601105C RID: 69724 RVA: 0x003C2576 File Offset: 0x003C0776
		public override string RootWebConfigPath
		{
			get
			{
				return this._originalWorkerRequest.RootWebConfigPath;
			}
		}

		// Token: 0x0601105D RID: 69725 RVA: 0x003C2583 File Offset: 0x003C0783
		public override void SendCalculatedContentLength(int contentLength)
		{
			this._originalWorkerRequest.SendCalculatedContentLength(contentLength);
		}

		// Token: 0x0601105E RID: 69726 RVA: 0x003C2591 File Offset: 0x003C0791
		public override void SendCalculatedContentLength(long contentLength)
		{
			this._originalWorkerRequest.SendCalculatedContentLength(contentLength);
		}

		// Token: 0x0601105F RID: 69727 RVA: 0x003C259F File Offset: 0x003C079F
		public override void SendResponseFromMemory(IntPtr data, int length)
		{
			this._originalWorkerRequest.SendResponseFromMemory(data, length);
		}

		// Token: 0x06011060 RID: 69728 RVA: 0x003C25AE File Offset: 0x003C07AE
		public override void SetEndOfSendNotification(HttpWorkerRequest.EndOfSendNotification callback, object extraData)
		{
			this._originalWorkerRequest.SetEndOfSendNotification(callback, extraData);
		}

		// Token: 0x06011061 RID: 69729 RVA: 0x003C25BD File Offset: 0x003C07BD
		public ProgressWorkerRequest(HttpWorkerRequest wr, HttpRequest request)
		{
			this._originalWorkerRequest = wr;
			this._request = request;
			this._boundary = this.GetBoundary(this._request);
			this._requestStateStore = new RequestStateStore(this._request.ContentEncoding);
		}

		// Token: 0x06011062 RID: 69730 RVA: 0x003C25FB File Offset: 0x003C07FB
		public override void EndOfRequest()
		{
			this._originalWorkerRequest.EndOfRequest();
		}

		// Token: 0x06011063 RID: 69731 RVA: 0x003C2608 File Offset: 0x003C0808
		public override void FlushResponse(bool finalFlush)
		{
			this._originalWorkerRequest.FlushResponse(finalFlush);
		}

		// Token: 0x06011064 RID: 69732 RVA: 0x003C2616 File Offset: 0x003C0816
		public override string GetHttpVerbName()
		{
			return this._originalWorkerRequest.GetHttpVerbName();
		}

		// Token: 0x06011065 RID: 69733 RVA: 0x003C2623 File Offset: 0x003C0823
		public override string GetHttpVersion()
		{
			return this._originalWorkerRequest.GetHttpVersion();
		}

		// Token: 0x06011066 RID: 69734 RVA: 0x003C2630 File Offset: 0x003C0830
		public override string GetLocalAddress()
		{
			return this._originalWorkerRequest.GetLocalAddress();
		}

		// Token: 0x06011067 RID: 69735 RVA: 0x003C263D File Offset: 0x003C083D
		public override int GetLocalPort()
		{
			return this._originalWorkerRequest.GetLocalPort();
		}

		// Token: 0x06011068 RID: 69736 RVA: 0x003C264A File Offset: 0x003C084A
		public override string GetQueryString()
		{
			return this._originalWorkerRequest.GetQueryString();
		}

		// Token: 0x06011069 RID: 69737 RVA: 0x003C2657 File Offset: 0x003C0857
		public override string GetRawUrl()
		{
			return this._originalWorkerRequest.GetRawUrl();
		}

		// Token: 0x0601106A RID: 69738 RVA: 0x003C2664 File Offset: 0x003C0864
		public override string GetRemoteAddress()
		{
			return this._originalWorkerRequest.GetRemoteAddress();
		}

		// Token: 0x0601106B RID: 69739 RVA: 0x003C2671 File Offset: 0x003C0871
		public override int GetRemotePort()
		{
			return this._originalWorkerRequest.GetRemotePort();
		}

		// Token: 0x0601106C RID: 69740 RVA: 0x003C267E File Offset: 0x003C087E
		public override string GetUriPath()
		{
			return this._originalWorkerRequest.GetUriPath();
		}

		// Token: 0x0601106D RID: 69741 RVA: 0x003C268B File Offset: 0x003C088B
		public override void SendKnownResponseHeader(int index, string value)
		{
			this._originalWorkerRequest.SendKnownResponseHeader(index, value);
		}

		// Token: 0x0601106E RID: 69742 RVA: 0x003C269A File Offset: 0x003C089A
		public override void SendResponseFromFile(IntPtr handle, long offset, long length)
		{
			this._originalWorkerRequest.SendResponseFromFile(handle, offset, length);
		}

		// Token: 0x0601106F RID: 69743 RVA: 0x003C26AA File Offset: 0x003C08AA
		public override void SendResponseFromFile(string filename, long offset, long length)
		{
			this._originalWorkerRequest.SendResponseFromFile(filename, offset, length);
		}

		// Token: 0x06011070 RID: 69744 RVA: 0x003C26BA File Offset: 0x003C08BA
		public override void SendResponseFromMemory(byte[] data, int length)
		{
			this._originalWorkerRequest.SendResponseFromMemory(data, length);
		}

		// Token: 0x06011071 RID: 69745 RVA: 0x003C26C9 File Offset: 0x003C08C9
		public override void SendStatus(int statusCode, string statusDescription)
		{
			this._originalWorkerRequest.SendStatus(statusCode, statusDescription);
		}

		// Token: 0x06011072 RID: 69746 RVA: 0x003C26D8 File Offset: 0x003C08D8
		public override void SendUnknownResponseHeader(string name, string value)
		{
			this._originalWorkerRequest.SendUnknownResponseHeader(name, value);
		}

		// Token: 0x04004C27 RID: 19495
		private byte[] _boundary;

		// Token: 0x04004C28 RID: 19496
		private RequestParser _parser;

		// Token: 0x04004C29 RID: 19497
		private RequestStateStore _requestStateStore;

		// Token: 0x04004C2A RID: 19498
		public HttpWorkerRequest _originalWorkerRequest;

		// Token: 0x04004C2B RID: 19499
		private HttpRequest _request;
	}
}
