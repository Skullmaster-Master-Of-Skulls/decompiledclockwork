using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net.Cache;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000484 RID: 1156
	[ComVisible(true)]
	public class WebClient : Component
	{
		// Token: 0x0600230F RID: 8975 RVA: 0x000894E4 File Offset: 0x000884E4
		private void InitWebClientAsync()
		{
			if (!this.m_InitWebClientAsync)
			{
				this.openReadOperationCompleted = new SendOrPostCallback(this.OpenReadOperationCompleted);
				this.openWriteOperationCompleted = new SendOrPostCallback(this.OpenWriteOperationCompleted);
				this.downloadStringOperationCompleted = new SendOrPostCallback(this.DownloadStringOperationCompleted);
				this.downloadDataOperationCompleted = new SendOrPostCallback(this.DownloadDataOperationCompleted);
				this.downloadFileOperationCompleted = new SendOrPostCallback(this.DownloadFileOperationCompleted);
				this.uploadStringOperationCompleted = new SendOrPostCallback(this.UploadStringOperationCompleted);
				this.uploadDataOperationCompleted = new SendOrPostCallback(this.UploadDataOperationCompleted);
				this.uploadFileOperationCompleted = new SendOrPostCallback(this.UploadFileOperationCompleted);
				this.uploadValuesOperationCompleted = new SendOrPostCallback(this.UploadValuesOperationCompleted);
				this.reportDownloadProgressChanged = new SendOrPostCallback(this.ReportDownloadProgressChanged);
				this.reportUploadProgressChanged = new SendOrPostCallback(this.ReportUploadProgressChanged);
				this.m_Progress = new WebClient.ProgressData();
				this.m_InitWebClientAsync = true;
			}
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x000895D4 File Offset: 0x000885D4
		private void ClearWebClientState()
		{
			if (this.AnotherCallInProgress(Interlocked.Increment(ref this.m_CallNesting)))
			{
				this.CompleteWebClientState();
				throw new NotSupportedException(SR.GetString("net_webclient_no_concurrent_io_allowed"));
			}
			this.m_ContentLength = -1L;
			this.m_WebResponse = null;
			this.m_WebRequest = null;
			this.m_Method = null;
			this.m_Cancelled = false;
			if (this.m_Progress != null)
			{
				this.m_Progress.Reset();
			}
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x00089641 File Offset: 0x00088641
		private void CompleteWebClientState()
		{
			Interlocked.Decrement(ref this.m_CallNesting);
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06002312 RID: 8978 RVA: 0x0008964F File Offset: 0x0008864F
		// (set) Token: 0x06002313 RID: 8979 RVA: 0x00089657 File Offset: 0x00088657
		public Encoding Encoding
		{
			get
			{
				return this.m_Encoding;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Encoding");
				}
				this.m_Encoding = value;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06002314 RID: 8980 RVA: 0x0008966E File Offset: 0x0008866E
		// (set) Token: 0x06002315 RID: 8981 RVA: 0x00089690 File Offset: 0x00088690
		public string BaseAddress
		{
			get
			{
				if (!(this.m_baseAddress == null))
				{
					return this.m_baseAddress.ToString();
				}
				return string.Empty;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					this.m_baseAddress = null;
					return;
				}
				try
				{
					this.m_baseAddress = new Uri(value);
				}
				catch (UriFormatException innerException)
				{
					throw new ArgumentException(SR.GetString("net_webclient_invalid_baseaddress"), "value", innerException);
				}
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06002316 RID: 8982 RVA: 0x000896E8 File Offset: 0x000886E8
		// (set) Token: 0x06002317 RID: 8983 RVA: 0x000896F0 File Offset: 0x000886F0
		public ICredentials Credentials
		{
			get
			{
				return this.m_credentials;
			}
			set
			{
				this.m_credentials = value;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06002318 RID: 8984 RVA: 0x000896F9 File Offset: 0x000886F9
		// (set) Token: 0x06002319 RID: 8985 RVA: 0x0008970B File Offset: 0x0008870B
		public bool UseDefaultCredentials
		{
			get
			{
				return this.m_credentials is SystemNetworkCredential;
			}
			set
			{
				this.m_credentials = (value ? CredentialCache.DefaultCredentials : null);
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x0600231A RID: 8986 RVA: 0x0008971E File Offset: 0x0008871E
		// (set) Token: 0x0600231B RID: 8987 RVA: 0x0008973A File Offset: 0x0008873A
		public WebHeaderCollection Headers
		{
			get
			{
				if (this.m_headers == null)
				{
					this.m_headers = new WebHeaderCollection(WebHeaderCollectionType.WebRequest);
				}
				return this.m_headers;
			}
			set
			{
				this.m_headers = value;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x0600231C RID: 8988 RVA: 0x00089743 File Offset: 0x00088743
		// (set) Token: 0x0600231D RID: 8989 RVA: 0x0008975E File Offset: 0x0008875E
		public NameValueCollection QueryString
		{
			get
			{
				if (this.m_requestParameters == null)
				{
					this.m_requestParameters = new NameValueCollection();
				}
				return this.m_requestParameters;
			}
			set
			{
				this.m_requestParameters = value;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x0600231E RID: 8990 RVA: 0x00089767 File Offset: 0x00088767
		public WebHeaderCollection ResponseHeaders
		{
			get
			{
				if (this.m_WebResponse != null)
				{
					return this.m_WebResponse.Headers;
				}
				return null;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x0600231F RID: 8991 RVA: 0x0008977E File Offset: 0x0008877E
		// (set) Token: 0x06002320 RID: 8992 RVA: 0x0008979E File Offset: 0x0008879E
		public IWebProxy Proxy
		{
			get
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
				if (!this.m_ProxySet)
				{
					return WebRequest.InternalDefaultWebProxy;
				}
				return this.m_Proxy;
			}
			set
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
				this.m_Proxy = value;
				this.m_ProxySet = true;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06002321 RID: 8993 RVA: 0x000897B8 File Offset: 0x000887B8
		// (set) Token: 0x06002322 RID: 8994 RVA: 0x000897C0 File Offset: 0x000887C0
		public RequestCachePolicy CachePolicy
		{
			get
			{
				return this.m_CachePolicy;
			}
			set
			{
				this.m_CachePolicy = value;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06002323 RID: 8995 RVA: 0x000897C9 File Offset: 0x000887C9
		public bool IsBusy
		{
			get
			{
				return this.m_AsyncOp != null;
			}
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x000897D8 File Offset: 0x000887D8
		protected virtual WebRequest GetWebRequest(Uri address)
		{
			WebRequest webRequest = WebRequest.Create(address);
			this.CopyHeadersTo(webRequest);
			if (this.Credentials != null)
			{
				webRequest.Credentials = this.Credentials;
			}
			if (this.m_Method != null)
			{
				webRequest.Method = this.m_Method;
			}
			if (this.m_ContentLength != -1L)
			{
				webRequest.ContentLength = this.m_ContentLength;
			}
			if (this.m_ProxySet)
			{
				webRequest.Proxy = this.m_Proxy;
			}
			if (this.m_CachePolicy != null)
			{
				webRequest.CachePolicy = this.m_CachePolicy;
			}
			return webRequest;
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x0008985C File Offset: 0x0008885C
		protected virtual WebResponse GetWebResponse(WebRequest request)
		{
			WebResponse response = request.GetResponse();
			this.m_WebResponse = response;
			return response;
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x00089878 File Offset: 0x00088878
		protected virtual WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
		{
			WebResponse webResponse = request.EndGetResponse(result);
			this.m_WebResponse = webResponse;
			return webResponse;
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x00089895 File Offset: 0x00088895
		public byte[] DownloadData(string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.DownloadData(this.GetUri(address));
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x000898B4 File Offset: 0x000888B4
		public byte[] DownloadData(Uri address)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "DownloadData", address);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			this.ClearWebClientState();
			byte[] result;
			try
			{
				WebRequest webRequest;
				byte[] array = this.DownloadDataInternal(address, out webRequest);
				if (Logging.On)
				{
					Logging.Exit(Logging.Web, this, "DownloadData", array);
				}
				result = array;
			}
			finally
			{
				this.CompleteWebClientState();
			}
			return result;
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x00089934 File Offset: 0x00088934
		private byte[] DownloadDataInternal(Uri address, out WebRequest request)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "DownloadData", address);
			}
			request = null;
			byte[] result;
			try
			{
				request = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				byte[] array = this.DownloadBits(request, null, null, null);
				result = array;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				WebClient.AbortRequest(request);
				throw ex;
			}
			catch
			{
				Exception ex2 = new WebException(SR.GetString("net_webclient"), new Exception(SR.GetString("net_nonClsCompliantException")));
				WebClient.AbortRequest(request);
				throw ex2;
			}
			return result;
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x00089A14 File Offset: 0x00088A14
		public void DownloadFile(string address, string fileName)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			this.DownloadFile(this.GetUri(address), fileName);
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x00089A34 File Offset: 0x00088A34
		public void DownloadFile(Uri address, string fileName)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "DownloadFile", address + ", " + fileName);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			WebRequest request = null;
			FileStream fileStream = null;
			bool flag = false;
			this.ClearWebClientState();
			try
			{
				fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
				request = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				this.DownloadBits(request, fileStream, null, null);
				flag = true;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				WebClient.AbortRequest(request);
				throw ex;
			}
			catch
			{
				Exception ex2 = new WebException(SR.GetString("net_webclient"), new Exception(SR.GetString("net_nonClsCompliantException")));
				WebClient.AbortRequest(request);
				throw ex2;
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
					if (!flag)
					{
						File.Delete(fileName);
					}
					fileStream = null;
				}
				this.CompleteWebClientState();
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, this, "DownloadFile", "");
			}
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x00089B94 File Offset: 0x00088B94
		public Stream OpenRead(string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.OpenRead(this.GetUri(address));
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x00089BB4 File Offset: 0x00088BB4
		public Stream OpenRead(Uri address)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "OpenRead", address);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			WebRequest request = null;
			this.ClearWebClientState();
			Stream result;
			try
			{
				request = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				WebResponse webResponse = this.m_WebResponse = this.GetWebResponse(request);
				Stream responseStream = webResponse.GetResponseStream();
				if (Logging.On)
				{
					Logging.Exit(Logging.Web, this, "OpenRead", responseStream);
				}
				result = responseStream;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				WebClient.AbortRequest(request);
				throw ex;
			}
			catch
			{
				Exception ex2 = new WebException(SR.GetString("net_webclient"), new Exception(SR.GetString("net_nonClsCompliantException")));
				WebClient.AbortRequest(request);
				throw ex2;
			}
			finally
			{
				this.CompleteWebClientState();
			}
			return result;
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x00089CE4 File Offset: 0x00088CE4
		public Stream OpenWrite(string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.OpenWrite(this.GetUri(address), null);
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x00089D02 File Offset: 0x00088D02
		public Stream OpenWrite(Uri address)
		{
			return this.OpenWrite(address, null);
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x00089D0C File Offset: 0x00088D0C
		public Stream OpenWrite(string address, string method)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.OpenWrite(this.GetUri(address), method);
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x00089D2C File Offset: 0x00088D2C
		public Stream OpenWrite(Uri address, string method)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "OpenWrite", address + ", " + method);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			WebRequest webRequest = null;
			this.ClearWebClientState();
			Stream result;
			try
			{
				this.m_Method = method;
				webRequest = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				WebClient.WebClientWriteStream webClientWriteStream = new WebClient.WebClientWriteStream(webRequest.GetRequestStream(), webRequest, this);
				if (Logging.On)
				{
					Logging.Exit(Logging.Web, this, "OpenWrite", webClientWriteStream);
				}
				result = webClientWriteStream;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				WebClient.AbortRequest(webRequest);
				throw ex;
			}
			catch
			{
				Exception ex2 = new WebException(SR.GetString("net_webclient"), new Exception(SR.GetString("net_nonClsCompliantException")));
				WebClient.AbortRequest(webRequest);
				throw ex2;
			}
			finally
			{
				this.CompleteWebClientState();
			}
			return result;
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x00089E6C File Offset: 0x00088E6C
		public byte[] UploadData(string address, byte[] data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadData(this.GetUri(address), null, data);
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x00089E8B File Offset: 0x00088E8B
		public byte[] UploadData(Uri address, byte[] data)
		{
			return this.UploadData(address, null, data);
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x00089E96 File Offset: 0x00088E96
		public byte[] UploadData(string address, string method, byte[] data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadData(this.GetUri(address), method, data);
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x00089EB8 File Offset: 0x00088EB8
		public byte[] UploadData(Uri address, string method, byte[] data)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "UploadData", address + ", " + method);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			this.ClearWebClientState();
			byte[] result;
			try
			{
				WebRequest webRequest;
				byte[] array = this.UploadDataInternal(address, method, data, out webRequest);
				if (Logging.On)
				{
					Logging.Exit(Logging.Web, this, "UploadData", array);
				}
				result = array;
			}
			finally
			{
				this.CompleteWebClientState();
			}
			return result;
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x00089F5C File Offset: 0x00088F5C
		private byte[] UploadDataInternal(Uri address, string method, byte[] data, out WebRequest request)
		{
			request = null;
			byte[] result;
			try
			{
				this.m_Method = method;
				this.m_ContentLength = (long)data.Length;
				request = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				this.UploadBits(request, null, data, null, null, null, null);
				byte[] array = this.DownloadBits(request, null, null, null);
				result = array;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				WebClient.AbortRequest(request);
				throw ex;
			}
			catch
			{
				Exception ex2 = new WebException(SR.GetString("net_webclient"), new Exception(SR.GetString("net_nonClsCompliantException")));
				WebClient.AbortRequest(request);
				throw ex2;
			}
			return result;
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x0008A048 File Offset: 0x00089048
		private void OpenFileInternal(bool needsHeaderAndBoundary, string fileName, ref FileStream fs, ref byte[] buffer, ref byte[] formHeaderBytes, ref byte[] boundaryBytes)
		{
			fileName = Path.GetFullPath(fileName);
			if (this.m_headers == null)
			{
				this.m_headers = new WebHeaderCollection(WebHeaderCollectionType.WebRequest);
			}
			string text = this.m_headers["Content-Type"];
			if (text != null)
			{
				if (text.ToLower(CultureInfo.InvariantCulture).StartsWith("multipart/"))
				{
					throw new WebException(SR.GetString("net_webclient_Multipart"));
				}
			}
			else
			{
				text = "application/octet-stream";
			}
			fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
			int num = 8192;
			this.m_ContentLength = -1L;
			if (this.m_Method.ToUpper(CultureInfo.InvariantCulture) == "POST")
			{
				if (needsHeaderAndBoundary)
				{
					string text2 = "---------------------" + DateTime.Now.Ticks.ToString("x", NumberFormatInfo.InvariantInfo);
					this.m_headers["Content-Type"] = "multipart/form-data; boundary=" + text2;
					string s = string.Concat(new string[]
					{
						"--",
						text2,
						"\r\nContent-Disposition: form-data; name=\"file\"; filename=\"",
						Path.GetFileName(fileName),
						"\"\r\nContent-Type: ",
						text,
						"\r\n\r\n"
					});
					formHeaderBytes = Encoding.UTF8.GetBytes(s);
					boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + text2 + "--\r\n");
				}
				else
				{
					formHeaderBytes = new byte[0];
					boundaryBytes = new byte[0];
				}
				if (fs.CanSeek)
				{
					this.m_ContentLength = fs.Length + (long)formHeaderBytes.Length + (long)boundaryBytes.Length;
					num = (int)Math.Min(8192L, fs.Length);
				}
			}
			else
			{
				this.m_headers["Content-Type"] = text;
				formHeaderBytes = null;
				boundaryBytes = null;
				if (fs.CanSeek)
				{
					this.m_ContentLength = fs.Length;
					num = (int)Math.Min(8192L, fs.Length);
				}
			}
			buffer = new byte[num];
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x0008A243 File Offset: 0x00089243
		public byte[] UploadFile(string address, string fileName)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadFile(this.GetUri(address), fileName);
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x0008A261 File Offset: 0x00089261
		public byte[] UploadFile(Uri address, string fileName)
		{
			return this.UploadFile(address, null, fileName);
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x0008A26C File Offset: 0x0008926C
		public byte[] UploadFile(string address, string method, string fileName)
		{
			return this.UploadFile(this.GetUri(address), method, fileName);
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x0008A280 File Offset: 0x00089280
		public byte[] UploadFile(Uri address, string method, string fileName)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "UploadFile", address + ", " + method);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			FileStream fileStream = null;
			WebRequest request = null;
			this.ClearWebClientState();
			byte[] result;
			try
			{
				this.m_Method = method;
				byte[] header = null;
				byte[] footer = null;
				byte[] buffer = null;
				Uri uri = this.GetUri(address);
				bool needsHeaderAndBoundary = uri.Scheme != Uri.UriSchemeFile;
				this.OpenFileInternal(needsHeaderAndBoundary, fileName, ref fileStream, ref buffer, ref header, ref footer);
				request = (this.m_WebRequest = this.GetWebRequest(uri));
				this.UploadBits(request, fileStream, buffer, header, footer, null, null);
				byte[] array = this.DownloadBits(request, null, null, null);
				if (Logging.On)
				{
					Logging.Exit(Logging.Web, this, "UploadFile", array);
				}
				result = array;
			}
			catch (Exception ex)
			{
				if (fileStream != null)
				{
					fileStream.Close();
					fileStream = null;
				}
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				WebClient.AbortRequest(request);
				throw ex;
			}
			catch
			{
				if (fileStream != null)
				{
					fileStream.Close();
					fileStream = null;
				}
				Exception ex2 = new WebException(SR.GetString("net_webclient"), new Exception(SR.GetString("net_nonClsCompliantException")));
				WebClient.AbortRequest(request);
				throw ex2;
			}
			finally
			{
				this.CompleteWebClientState();
			}
			return result;
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x0008A458 File Offset: 0x00089458
		private byte[] UploadValuesInternal(NameValueCollection data)
		{
			if (this.m_headers == null)
			{
				this.m_headers = new WebHeaderCollection(WebHeaderCollectionType.WebRequest);
			}
			string text = this.m_headers["Content-Type"];
			if (text != null && string.Compare(text, "application/x-www-form-urlencoded", StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new WebException(SR.GetString("net_webclient_ContentType"));
			}
			this.m_headers["Content-Type"] = "application/x-www-form-urlencoded";
			string value = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text2 in data.AllKeys)
			{
				stringBuilder.Append(value);
				stringBuilder.Append(WebClient.UrlEncode(text2));
				stringBuilder.Append("=");
				stringBuilder.Append(WebClient.UrlEncode(data[text2]));
				value = "&";
			}
			byte[] bytes = Encoding.ASCII.GetBytes(stringBuilder.ToString());
			this.m_ContentLength = (long)bytes.Length;
			return bytes;
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x0008A546 File Offset: 0x00089546
		public byte[] UploadValues(string address, NameValueCollection data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadValues(this.GetUri(address), null, data);
		}

		// Token: 0x0600233E RID: 9022 RVA: 0x0008A565 File Offset: 0x00089565
		public byte[] UploadValues(Uri address, NameValueCollection data)
		{
			return this.UploadValues(address, null, data);
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x0008A570 File Offset: 0x00089570
		public byte[] UploadValues(string address, string method, NameValueCollection data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadValues(this.GetUri(address), method, data);
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x0008A590 File Offset: 0x00089590
		public byte[] UploadValues(Uri address, string method, NameValueCollection data)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "UploadValues", address + ", " + method);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			WebRequest request = null;
			this.ClearWebClientState();
			byte[] result;
			try
			{
				byte[] buffer = this.UploadValuesInternal(data);
				this.m_Method = method;
				request = (this.m_WebRequest = this.GetWebRequest(this.GetUri(address)));
				this.UploadBits(request, null, buffer, null, null, null, null);
				byte[] array = this.DownloadBits(request, null, null, null);
				if (Logging.On)
				{
					Logging.Exit(Logging.Web, this, "UploadValues", address + ", " + method);
				}
				result = array;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				WebClient.AbortRequest(request);
				throw ex;
			}
			catch
			{
				Exception ex2 = new WebException(SR.GetString("net_webclient"), new Exception(SR.GetString("net_nonClsCompliantException")));
				WebClient.AbortRequest(request);
				throw ex2;
			}
			finally
			{
				this.CompleteWebClientState();
			}
			return result;
		}

		// Token: 0x06002341 RID: 9025 RVA: 0x0008A700 File Offset: 0x00089700
		public string UploadString(string address, string data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadString(this.GetUri(address), null, data);
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x0008A71F File Offset: 0x0008971F
		public string UploadString(Uri address, string data)
		{
			return this.UploadString(address, null, data);
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x0008A72A File Offset: 0x0008972A
		public string UploadString(string address, string method, string data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadString(this.GetUri(address), method, data);
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x0008A74C File Offset: 0x0008974C
		public string UploadString(Uri address, string method, string data)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "UploadString", address + ", " + method);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			this.ClearWebClientState();
			string result;
			try
			{
				byte[] bytes = this.Encoding.GetBytes(data);
				WebRequest request;
				byte[] bytes2 = this.UploadDataInternal(address, method, bytes, out request);
				string @string = this.GuessDownloadEncoding(request).GetString(bytes2);
				if (Logging.On)
				{
					Logging.Exit(Logging.Web, this, "UploadString", @string);
				}
				result = @string;
			}
			finally
			{
				this.CompleteWebClientState();
			}
			return result;
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x0008A810 File Offset: 0x00089810
		public string DownloadString(string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.DownloadString(this.GetUri(address));
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x0008A830 File Offset: 0x00089830
		public string DownloadString(Uri address)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "DownloadString", address);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			this.ClearWebClientState();
			string result;
			try
			{
				WebRequest request;
				byte[] bytes = this.DownloadDataInternal(address, out request);
				string @string = this.GuessDownloadEncoding(request).GetString(bytes);
				if (Logging.On)
				{
					Logging.Exit(Logging.Web, this, "DownloadString", @string);
				}
				result = @string;
			}
			finally
			{
				this.CompleteWebClientState();
			}
			return result;
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x0008A8BC File Offset: 0x000898BC
		private static void AbortRequest(WebRequest request)
		{
			try
			{
				if (request != null)
				{
					request.Abort();
				}
			}
			catch (Exception ex)
			{
				if (ex is OutOfMemoryException || ex is StackOverflowException || ex is ThreadAbortException)
				{
					throw;
				}
			}
			catch
			{
			}
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x0008A910 File Offset: 0x00089910
		private void CopyHeadersTo(WebRequest request)
		{
			if (this.m_headers != null && request is HttpWebRequest)
			{
				string text = this.m_headers["Accept"];
				string text2 = this.m_headers["Connection"];
				string text3 = this.m_headers["Content-Type"];
				string text4 = this.m_headers["Expect"];
				string text5 = this.m_headers["Referer"];
				string text6 = this.m_headers["User-Agent"];
				this.m_headers.RemoveInternal("Accept");
				this.m_headers.RemoveInternal("Connection");
				this.m_headers.RemoveInternal("Content-Type");
				this.m_headers.RemoveInternal("Expect");
				this.m_headers.RemoveInternal("Referer");
				this.m_headers.RemoveInternal("User-Agent");
				request.Headers = this.m_headers;
				if (text != null && text.Length > 0)
				{
					((HttpWebRequest)request).Accept = text;
				}
				if (text2 != null && text2.Length > 0)
				{
					((HttpWebRequest)request).Connection = text2;
				}
				if (text3 != null && text3.Length > 0)
				{
					((HttpWebRequest)request).ContentType = text3;
				}
				if (text4 != null && text4.Length > 0)
				{
					((HttpWebRequest)request).Expect = text4;
				}
				if (text5 != null && text5.Length > 0)
				{
					((HttpWebRequest)request).Referer = text5;
				}
				if (text6 != null && text6.Length > 0)
				{
					((HttpWebRequest)request).UserAgent = text6;
				}
			}
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x0008AAA0 File Offset: 0x00089AA0
		private Uri GetUri(string path)
		{
			Uri address;
			if (this.m_baseAddress != null)
			{
				if (!Uri.TryCreate(this.m_baseAddress, path, out address))
				{
					return new Uri(Path.GetFullPath(path));
				}
			}
			else if (!Uri.TryCreate(path, UriKind.Absolute, out address))
			{
				return new Uri(Path.GetFullPath(path));
			}
			return this.GetUri(address);
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x0008AAF8 File Offset: 0x00089AF8
		private Uri GetUri(Uri address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			Uri uri = address;
			if (!address.IsAbsoluteUri && this.m_baseAddress != null && !Uri.TryCreate(this.m_baseAddress, address, out uri))
			{
				return address;
			}
			if ((uri.Query == null || uri.Query == string.Empty) && this.m_requestParameters != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				string str = string.Empty;
				for (int i = 0; i < this.m_requestParameters.Count; i++)
				{
					stringBuilder.Append(str + this.m_requestParameters.AllKeys[i] + "=" + this.m_requestParameters[i]);
					str = "&";
				}
				uri = new UriBuilder(uri)
				{
					Query = stringBuilder.ToString()
				}.Uri;
			}
			return uri;
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x0008ABD8 File Offset: 0x00089BD8
		private static void DownloadBitsResponseCallback(IAsyncResult result)
		{
			WebClient.DownloadBitsState downloadBitsState = (WebClient.DownloadBitsState)result.AsyncState;
			WebRequest request = downloadBitsState.Request;
			Exception ex = null;
			try
			{
				WebResponse webResponse = downloadBitsState.WebClient.GetWebResponse(request, result);
				downloadBitsState.WebClient.m_WebResponse = webResponse;
				downloadBitsState.SetResponse(webResponse);
			}
			catch (Exception ex2)
			{
				if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
				{
					throw;
				}
				ex = ex2;
				if (!(ex2 is WebException) && !(ex2 is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex2);
				}
				WebClient.AbortRequest(request);
				if (downloadBitsState != null && downloadBitsState.WriteStream != null)
				{
					downloadBitsState.WriteStream.Close();
				}
			}
			finally
			{
				if (ex != null)
				{
					downloadBitsState.CompletionDelegate(null, ex, downloadBitsState.AsyncOp);
				}
			}
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x0008ACB8 File Offset: 0x00089CB8
		private static void DownloadBitsReadCallback(IAsyncResult result)
		{
			WebClient.DownloadBitsState state = (WebClient.DownloadBitsState)result.AsyncState;
			WebClient.DownloadBitsReadCallbackState(state, result);
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x0008ACD8 File Offset: 0x00089CD8
		private static void DownloadBitsReadCallbackState(WebClient.DownloadBitsState state, IAsyncResult result)
		{
			Stream readStream = state.ReadStream;
			Exception ex = null;
			bool flag = false;
			try
			{
				int num = 0;
				if (readStream != null && readStream != Stream.Null)
				{
					num = readStream.EndRead(result);
				}
				flag = state.RetrieveBytes(ref num);
			}
			catch (Exception ex2)
			{
				flag = true;
				if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
				{
					throw;
				}
				ex = ex2;
				state.InnerBuffer = null;
				if (!(ex2 is WebException) && !(ex2 is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex2);
				}
				WebClient.AbortRequest(state.Request);
				if (state != null && state.WriteStream != null)
				{
					state.WriteStream.Close();
				}
			}
			finally
			{
				if (flag)
				{
					if (ex == null)
					{
						state.Close();
					}
					state.CompletionDelegate(state.InnerBuffer, ex, state.AsyncOp);
				}
			}
		}

		// Token: 0x0600234E RID: 9038 RVA: 0x0008ADC8 File Offset: 0x00089DC8
		private byte[] DownloadBits(WebRequest request, Stream writeStream, CompletionDelegate completionDelegate, AsyncOperation asyncOp)
		{
			WebClient.DownloadBitsState downloadBitsState = new WebClient.DownloadBitsState(request, writeStream, completionDelegate, asyncOp, this.m_Progress, this);
			if (downloadBitsState.Async)
			{
				request.BeginGetResponse(new AsyncCallback(WebClient.DownloadBitsResponseCallback), downloadBitsState);
				return null;
			}
			WebResponse response = this.m_WebResponse = this.GetWebResponse(request);
			int num = downloadBitsState.SetResponse(response);
			bool flag;
			do
			{
				flag = downloadBitsState.RetrieveBytes(ref num);
			}
			while (!flag);
			downloadBitsState.Close();
			return downloadBitsState.InnerBuffer;
		}

		// Token: 0x0600234F RID: 9039 RVA: 0x0008AE3C File Offset: 0x00089E3C
		private static void UploadBitsRequestCallback(IAsyncResult result)
		{
			WebClient.UploadBitsState uploadBitsState = (WebClient.UploadBitsState)result.AsyncState;
			WebRequest request = uploadBitsState.Request;
			Exception ex = null;
			try
			{
				Stream requestStream = request.EndGetRequestStream(result);
				uploadBitsState.SetRequestStream(requestStream);
			}
			catch (Exception ex2)
			{
				if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
				{
					throw;
				}
				ex = ex2;
				if (!(ex2 is WebException) && !(ex2 is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex2);
				}
				WebClient.AbortRequest(request);
				if (uploadBitsState != null && uploadBitsState.ReadStream != null)
				{
					uploadBitsState.ReadStream.Close();
				}
			}
			finally
			{
				if (ex != null)
				{
					uploadBitsState.CompletionDelegate(null, ex, uploadBitsState.AsyncOp);
				}
			}
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x0008AF08 File Offset: 0x00089F08
		private static void UploadBitsWriteCallback(IAsyncResult result)
		{
			WebClient.UploadBitsState uploadBitsState = (WebClient.UploadBitsState)result.AsyncState;
			Stream writeStream = uploadBitsState.WriteStream;
			Exception ex = null;
			bool flag = false;
			try
			{
				writeStream.EndWrite(result);
				flag = uploadBitsState.WriteBytes();
			}
			catch (Exception ex2)
			{
				flag = true;
				if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
				{
					throw;
				}
				ex = ex2;
				if (!(ex2 is WebException) && !(ex2 is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex2);
				}
				WebClient.AbortRequest(uploadBitsState.Request);
				if (uploadBitsState != null && uploadBitsState.ReadStream != null)
				{
					uploadBitsState.ReadStream.Close();
				}
			}
			finally
			{
				if (flag)
				{
					if (ex == null)
					{
						uploadBitsState.Close();
					}
					uploadBitsState.CompletionDelegate(null, ex, uploadBitsState.AsyncOp);
				}
			}
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x0008AFE8 File Offset: 0x00089FE8
		private void UploadBits(WebRequest request, Stream readStream, byte[] buffer, byte[] header, byte[] footer, CompletionDelegate completionDelegate, AsyncOperation asyncOp)
		{
			if (request.RequestUri.Scheme == Uri.UriSchemeFile)
			{
				footer = (header = null);
			}
			WebClient.UploadBitsState uploadBitsState = new WebClient.UploadBitsState(request, readStream, buffer, header, footer, completionDelegate, asyncOp, this.m_Progress, this);
			if (uploadBitsState.Async)
			{
				request.BeginGetRequestStream(new AsyncCallback(WebClient.UploadBitsRequestCallback), uploadBitsState);
				return;
			}
			Stream requestStream = request.GetRequestStream();
			uploadBitsState.SetRequestStream(requestStream);
			while (!uploadBitsState.WriteBytes())
			{
			}
			uploadBitsState.Close();
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x0008B070 File Offset: 0x0008A070
		private Encoding GuessDownloadEncoding(WebRequest request)
		{
			try
			{
				string text;
				if ((text = request.ContentType) == null)
				{
					return this.Encoding;
				}
				text = text.ToLower(CultureInfo.InvariantCulture);
				string[] array = text.Split(new char[]
				{
					';',
					'=',
					' '
				});
				bool flag = false;
				foreach (string text2 in array)
				{
					if (text2 == "charset")
					{
						flag = true;
					}
					else if (flag)
					{
						return Encoding.GetEncoding(text2);
					}
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
			}
			catch
			{
			}
			return this.Encoding;
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x0008B140 File Offset: 0x0008A140
		private string MapToDefaultMethod(Uri address)
		{
			Uri uri;
			if (!address.IsAbsoluteUri && this.m_baseAddress != null)
			{
				uri = new Uri(this.m_baseAddress, address);
			}
			else
			{
				uri = address;
			}
			if (uri.Scheme.ToLower(CultureInfo.InvariantCulture) == "ftp")
			{
				return "STOR";
			}
			return "POST";
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x0008B19B File Offset: 0x0008A19B
		private static string UrlEncode(string str)
		{
			if (str == null)
			{
				return null;
			}
			return WebClient.UrlEncode(str, Encoding.UTF8);
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x0008B1AD File Offset: 0x0008A1AD
		private static string UrlEncode(string str, Encoding e)
		{
			if (str == null)
			{
				return null;
			}
			return Encoding.ASCII.GetString(WebClient.UrlEncodeToBytes(str, e));
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x0008B1C8 File Offset: 0x0008A1C8
		private static byte[] UrlEncodeToBytes(string str, Encoding e)
		{
			if (str == null)
			{
				return null;
			}
			byte[] bytes = e.GetBytes(str);
			return WebClient.UrlEncodeBytesToBytesInternal(bytes, 0, bytes.Length, false);
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x0008B1F0 File Offset: 0x0008A1F0
		private static byte[] UrlEncodeBytesToBytesInternal(byte[] bytes, int offset, int count, bool alwaysCreateReturnValue)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < count; i++)
			{
				char c = (char)bytes[offset + i];
				if (c == ' ')
				{
					num++;
				}
				else if (!WebClient.IsSafe(c))
				{
					num2++;
				}
			}
			if (!alwaysCreateReturnValue && num == 0 && num2 == 0)
			{
				return bytes;
			}
			byte[] array = new byte[count + num2 * 2];
			int num3 = 0;
			for (int j = 0; j < count; j++)
			{
				byte b = bytes[offset + j];
				char c2 = (char)b;
				if (WebClient.IsSafe(c2))
				{
					array[num3++] = b;
				}
				else if (c2 == ' ')
				{
					array[num3++] = 43;
				}
				else
				{
					array[num3++] = 37;
					array[num3++] = (byte)WebClient.IntToHex(b >> 4 & 15);
					array[num3++] = (byte)WebClient.IntToHex((int)(b & 15));
				}
			}
			return array;
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x0008B2C5 File Offset: 0x0008A2C5
		private static char IntToHex(int n)
		{
			if (n <= 9)
			{
				return (char)(n + 48);
			}
			return (char)(n - 10 + 97);
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x0008B2DC File Offset: 0x0008A2DC
		private static bool IsSafe(char ch)
		{
			if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9'))
			{
				return true;
			}
			if (ch != '!')
			{
				switch (ch)
				{
				case '\'':
				case '(':
				case ')':
				case '*':
				case '-':
				case '.':
					return true;
				case '+':
				case ',':
					break;
				default:
					if (ch == '_')
					{
						return true;
					}
					break;
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x0008B341 File Offset: 0x0008A341
		private void InvokeOperationCompleted(AsyncOperation asyncOp, SendOrPostCallback callback, AsyncCompletedEventArgs eventArgs)
		{
			if (Interlocked.CompareExchange<AsyncOperation>(ref this.m_AsyncOp, null, asyncOp) == asyncOp)
			{
				this.CompleteWebClientState();
				asyncOp.PostOperationCompleted(callback, eventArgs);
			}
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x0008B361 File Offset: 0x0008A361
		private bool AnotherCallInProgress(int callNesting)
		{
			return callNesting > 1;
		}

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x0600235C RID: 9052 RVA: 0x0008B367 File Offset: 0x0008A367
		// (remove) Token: 0x0600235D RID: 9053 RVA: 0x0008B380 File Offset: 0x0008A380
		public event OpenReadCompletedEventHandler OpenReadCompleted;

		// Token: 0x0600235E RID: 9054 RVA: 0x0008B399 File Offset: 0x0008A399
		protected virtual void OnOpenReadCompleted(OpenReadCompletedEventArgs e)
		{
			if (this.OpenReadCompleted != null)
			{
				this.OpenReadCompleted(this, e);
			}
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x0008B3B0 File Offset: 0x0008A3B0
		private void OpenReadOperationCompleted(object arg)
		{
			this.OnOpenReadCompleted((OpenReadCompletedEventArgs)arg);
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x0008B3C0 File Offset: 0x0008A3C0
		private void OpenReadAsyncCallback(IAsyncResult result)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)result;
			AsyncOperation asyncOperation = (AsyncOperation)lazyAsyncResult.AsyncState;
			WebRequest request = (WebRequest)lazyAsyncResult.AsyncObject;
			Stream result2 = null;
			Exception exception = null;
			try
			{
				WebResponse webResponse = this.m_WebResponse = this.GetWebResponse(request, result);
				result2 = webResponse.GetResponseStream();
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				exception = ex;
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					exception = new WebException(SR.GetString("net_webclient"), ex);
				}
			}
			catch
			{
				exception = new WebException(SR.GetString("net_webclient"), new Exception(SR.GetString("net_nonClsCompliantException")));
			}
			OpenReadCompletedEventArgs eventArgs = new OpenReadCompletedEventArgs(result2, exception, this.m_Cancelled, asyncOperation.UserSuppliedState);
			this.InvokeOperationCompleted(asyncOperation, this.openReadOperationCompleted, eventArgs);
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x0008B4C0 File Offset: 0x0008A4C0
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void OpenReadAsync(Uri address)
		{
			this.OpenReadAsync(address, null);
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x0008B4CC File Offset: 0x0008A4CC
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void OpenReadAsync(Uri address, object userToken)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "OpenReadAsync", address);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			this.InitWebClientAsync();
			this.ClearWebClientState();
			AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(userToken);
			this.m_AsyncOp = asyncOperation;
			try
			{
				WebRequest webRequest = this.m_WebRequest = this.GetWebRequest(this.GetUri(address));
				webRequest.BeginGetResponse(new AsyncCallback(this.OpenReadAsyncCallback), asyncOperation);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				OpenReadCompletedEventArgs eventArgs = new OpenReadCompletedEventArgs(null, ex, this.m_Cancelled, asyncOperation.UserSuppliedState);
				this.InvokeOperationCompleted(asyncOperation, this.openReadOperationCompleted, eventArgs);
			}
			catch
			{
				Exception exception = new WebException(SR.GetString("net_webclient"), new Exception(SR.GetString("net_nonClsCompliantException")));
				OpenReadCompletedEventArgs eventArgs2 = new OpenReadCompletedEventArgs(null, exception, this.m_Cancelled, asyncOperation.UserSuppliedState);
				this.InvokeOperationCompleted(asyncOperation, this.openReadOperationCompleted, eventArgs2);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, this, "OpenReadAsync", null);
			}
		}

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06002363 RID: 9059 RVA: 0x0008B630 File Offset: 0x0008A630
		// (remove) Token: 0x06002364 RID: 9060 RVA: 0x0008B649 File Offset: 0x0008A649
		public event OpenWriteCompletedEventHandler OpenWriteCompleted;

		// Token: 0x06002365 RID: 9061 RVA: 0x0008B662 File Offset: 0x0008A662
		protected virtual void OnOpenWriteCompleted(OpenWriteCompletedEventArgs e)
		{
			if (this.OpenWriteCompleted != null)
			{
				this.OpenWriteCompleted(this, e);
			}
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x0008B679 File Offset: 0x0008A679
		private void OpenWriteOperationCompleted(object arg)
		{
			this.OnOpenWriteCompleted((OpenWriteCompletedEventArgs)arg);
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x0008B688 File Offset: 0x0008A688
		private void OpenWriteAsyncCallback(IAsyncResult result)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)result;
			AsyncOperation asyncOperation = (AsyncOperation)lazyAsyncResult.AsyncState;
			WebRequest webRequest = (WebRequest)lazyAsyncResult.AsyncObject;
			WebClient.WebClientWriteStream result2 = null;
			Exception exception = null;
			try
			{
				result2 = new WebClient.WebClientWriteStream(webRequest.EndGetRequestStream(result), webRequest, this);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				exception = ex;
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					exception = new WebException(SR.GetString("net_webclient"), ex);
				}
			}
			catch
			{
				exception = new WebException(SR.GetString("net_webclient"), new Exception(SR.GetString("net_nonClsCompliantException")));
			}
			OpenWriteCompletedEventArgs eventArgs = new OpenWriteCompletedEventArgs(result2, exception, this.m_Cancelled, asyncOperation.UserSuppliedState);
			this.InvokeOperationCompleted(asyncOperation, this.openWriteOperationCompleted, eventArgs);
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x0008B778 File Offset: 0x0008A778
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void OpenWriteAsync(Uri address)
		{
			this.OpenWriteAsync(address, null, null);
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x0008B783 File Offset: 0x0008A783
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void OpenWriteAsync(Uri address, string method)
		{
			this.OpenWriteAsync(address, method, null);
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x0008B790 File Offset: 0x0008A790
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void OpenWriteAsync(Uri address, string method, object userToken)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "OpenWriteAsync", address + ", " + method);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			this.InitWebClientAsync();
			this.ClearWebClientState();
			AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(userToken);
			this.m_AsyncOp = asyncOperation;
			try
			{
				this.m_Method = method;
				WebRequest webRequest = this.m_WebRequest = this.GetWebRequest(this.GetUri(address));
				webRequest.BeginGetRequestStream(new AsyncCallback(this.OpenWriteAsyncCallback), asyncOperation);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				OpenWriteCompletedEventArgs eventArgs = new OpenWriteCompletedEventArgs(null, ex, this.m_Cancelled, asyncOperation.UserSuppliedState);
				this.InvokeOperationCompleted(asyncOperation, this.openWriteOperationCompleted, eventArgs);
			}
			catch
			{
				Exception exception = new WebException(SR.GetString("net_webclient"), new Exception(SR.GetString("net_nonClsCompliantException")));
				OpenWriteCompletedEventArgs eventArgs2 = new OpenWriteCompletedEventArgs(null, exception, this.m_Cancelled, asyncOperation.UserSuppliedState);
				this.InvokeOperationCompleted(asyncOperation, this.openWriteOperationCompleted, eventArgs2);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, this, "OpenWriteAsync", null);
			}
		}

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x0600236B RID: 9067 RVA: 0x0008B910 File Offset: 0x0008A910
		// (remove) Token: 0x0600236C RID: 9068 RVA: 0x0008B929 File Offset: 0x0008A929
		public event DownloadStringCompletedEventHandler DownloadStringCompleted;

		// Token: 0x0600236D RID: 9069 RVA: 0x0008B942 File Offset: 0x0008A942
		protected virtual void OnDownloadStringCompleted(DownloadStringCompletedEventArgs e)
		{
			if (this.DownloadStringCompleted != null)
			{
				this.DownloadStringCompleted(this, e);
			}
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x0008B959 File Offset: 0x0008A959
		private void DownloadStringOperationCompleted(object arg)
		{
			this.OnDownloadStringCompleted((DownloadStringCompletedEventArgs)arg);
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x0008B968 File Offset: 0x0008A968
		private void DownloadStringAsyncCallback(byte[] returnBytes, Exception exception, AsyncOperation asyncOp)
		{
			string result = null;
			try
			{
				if (returnBytes != null)
				{
					result = this.GuessDownloadEncoding(this.m_WebRequest).GetString(returnBytes);
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				exception = ex;
			}
			catch
			{
				exception = new Exception(SR.GetString("net_nonClsCompliantException"));
			}
			DownloadStringCompletedEventArgs eventArgs = new DownloadStringCompletedEventArgs(result, exception, this.m_Cancelled, asyncOp.UserSuppliedState);
			this.InvokeOperationCompleted(asyncOp, this.downloadStringOperationCompleted, eventArgs);
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x0008BA04 File Offset: 0x0008AA04
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void DownloadStringAsync(Uri address)
		{
			this.DownloadStringAsync(address, null);
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x0008BA10 File Offset: 0x0008AA10
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void DownloadStringAsync(Uri address, object userToken)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "DownloadStringAsync", address);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			this.InitWebClientAsync();
			this.ClearWebClientState();
			AsyncOperation asyncOp = AsyncOperationManager.CreateOperation(userToken);
			this.m_AsyncOp = asyncOp;
			try
			{
				WebRequest request = this.m_WebRequest = this.GetWebRequest(this.GetUri(address));
				this.DownloadBits(request, null, new CompletionDelegate(this.DownloadStringAsyncCallback), asyncOp);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				this.DownloadStringAsyncCallback(null, ex, asyncOp);
			}
			catch
			{
				Exception exception = new WebException(SR.GetString("net_webclient"), new Exception(SR.GetString("net_nonClsCompliantException")));
				this.DownloadStringAsyncCallback(null, exception, asyncOp);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, this, "DownloadStringAsync", "");
			}
		}

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06002372 RID: 9074 RVA: 0x0008BB40 File Offset: 0x0008AB40
		// (remove) Token: 0x06002373 RID: 9075 RVA: 0x0008BB59 File Offset: 0x0008AB59
		public event DownloadDataCompletedEventHandler DownloadDataCompleted;

		// Token: 0x06002374 RID: 9076 RVA: 0x0008BB72 File Offset: 0x0008AB72
		protected virtual void OnDownloadDataCompleted(DownloadDataCompletedEventArgs e)
		{
			if (this.DownloadDataCompleted != null)
			{
				this.DownloadDataCompleted(this, e);
			}
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x0008BB89 File Offset: 0x0008AB89
		private void DownloadDataOperationCompleted(object arg)
		{
			this.OnDownloadDataCompleted((DownloadDataCompletedEventArgs)arg);
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x0008BB98 File Offset: 0x0008AB98
		private void DownloadDataAsyncCallback(byte[] returnBytes, Exception exception, AsyncOperation asyncOp)
		{
			DownloadDataCompletedEventArgs eventArgs = new DownloadDataCompletedEventArgs(returnBytes, exception, this.m_Cancelled, asyncOp.UserSuppliedState);
			this.InvokeOperationCompleted(asyncOp, this.downloadDataOperationCompleted, eventArgs);
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x0008BBC7 File Offset: 0x0008ABC7
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void DownloadDataAsync(Uri address)
		{
			this.DownloadDataAsync(address, null);
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x0008BBD4 File Offset: 0x0008ABD4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void DownloadDataAsync(Uri address, object userToken)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "DownloadDataAsync", address);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			this.InitWebClientAsync();
			this.ClearWebClientState();
			AsyncOperation asyncOp = AsyncOperationManager.CreateOperation(userToken);
			this.m_AsyncOp = asyncOp;
			try
			{
				WebRequest request = this.m_WebRequest = this.GetWebRequest(this.GetUri(address));
				this.DownloadBits(request, null, new CompletionDelegate(this.DownloadDataAsyncCallback), asyncOp);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				this.DownloadDataAsyncCallback(null, ex, asyncOp);
			}
			catch
			{
				Exception exception = new WebException(SR.GetString("net_webclient"), new Exception(SR.GetString("net_nonClsCompliantException")));
				this.DownloadDataAsyncCallback(null, exception, asyncOp);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, this, "DownloadDataAsync", null);
			}
		}

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06002379 RID: 9081 RVA: 0x0008BD00 File Offset: 0x0008AD00
		// (remove) Token: 0x0600237A RID: 9082 RVA: 0x0008BD19 File Offset: 0x0008AD19
		public event AsyncCompletedEventHandler DownloadFileCompleted;

		// Token: 0x0600237B RID: 9083 RVA: 0x0008BD32 File Offset: 0x0008AD32
		protected virtual void OnDownloadFileCompleted(AsyncCompletedEventArgs e)
		{
			if (this.DownloadFileCompleted != null)
			{
				this.DownloadFileCompleted(this, e);
			}
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x0008BD49 File Offset: 0x0008AD49
		private void DownloadFileOperationCompleted(object arg)
		{
			this.OnDownloadFileCompleted((AsyncCompletedEventArgs)arg);
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x0008BD58 File Offset: 0x0008AD58
		private void DownloadFileAsyncCallback(byte[] returnBytes, Exception exception, AsyncOperation asyncOp)
		{
			AsyncCompletedEventArgs eventArgs = new AsyncCompletedEventArgs(exception, this.m_Cancelled, asyncOp.UserSuppliedState);
			this.InvokeOperationCompleted(asyncOp, this.downloadFileOperationCompleted, eventArgs);
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x0008BD86 File Offset: 0x0008AD86
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void DownloadFileAsync(Uri address, string fileName)
		{
			this.DownloadFileAsync(address, fileName, null);
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x0008BD94 File Offset: 0x0008AD94
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void DownloadFileAsync(Uri address, string fileName, object userToken)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "DownloadFileAsync", address);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			FileStream fileStream = null;
			this.InitWebClientAsync();
			this.ClearWebClientState();
			AsyncOperation asyncOp = AsyncOperationManager.CreateOperation(userToken);
			this.m_AsyncOp = asyncOp;
			try
			{
				fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
				WebRequest request = this.m_WebRequest = this.GetWebRequest(this.GetUri(address));
				this.DownloadBits(request, fileStream, new CompletionDelegate(this.DownloadFileAsyncCallback), asyncOp);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (fileStream != null)
				{
					fileStream.Close();
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				this.DownloadFileAsyncCallback(null, ex, asyncOp);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, this, "DownloadFileAsync", null);
			}
		}

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x06002380 RID: 9088 RVA: 0x0008BEAC File Offset: 0x0008AEAC
		// (remove) Token: 0x06002381 RID: 9089 RVA: 0x0008BEC5 File Offset: 0x0008AEC5
		public event UploadStringCompletedEventHandler UploadStringCompleted;

		// Token: 0x06002382 RID: 9090 RVA: 0x0008BEDE File Offset: 0x0008AEDE
		protected virtual void OnUploadStringCompleted(UploadStringCompletedEventArgs e)
		{
			if (this.UploadStringCompleted != null)
			{
				this.UploadStringCompleted(this, e);
			}
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x0008BEF5 File Offset: 0x0008AEF5
		private void UploadStringOperationCompleted(object arg)
		{
			this.OnUploadStringCompleted((UploadStringCompletedEventArgs)arg);
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x0008BF04 File Offset: 0x0008AF04
		private void UploadStringAsyncWriteCallback(byte[] returnBytes, Exception exception, AsyncOperation asyncOp)
		{
			if (exception != null)
			{
				UploadStringCompletedEventArgs eventArgs = new UploadStringCompletedEventArgs(null, exception, this.m_Cancelled, asyncOp.UserSuppliedState);
				this.InvokeOperationCompleted(asyncOp, this.uploadStringOperationCompleted, eventArgs);
			}
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x0008BF38 File Offset: 0x0008AF38
		private void UploadStringAsyncReadCallback(byte[] returnBytes, Exception exception, AsyncOperation asyncOp)
		{
			string result = null;
			try
			{
				if (returnBytes != null)
				{
					result = this.GuessDownloadEncoding(this.m_WebRequest).GetString(returnBytes);
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				exception = ex;
			}
			catch
			{
				exception = new Exception(SR.GetString("net_nonClsCompliantException"));
			}
			UploadStringCompletedEventArgs eventArgs = new UploadStringCompletedEventArgs(result, exception, this.m_Cancelled, asyncOp.UserSuppliedState);
			this.InvokeOperationCompleted(asyncOp, this.uploadStringOperationCompleted, eventArgs);
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x0008BFD4 File Offset: 0x0008AFD4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadStringAsync(Uri address, string data)
		{
			this.UploadStringAsync(address, null, data, null);
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x0008BFE0 File Offset: 0x0008AFE0
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadStringAsync(Uri address, string method, string data)
		{
			this.UploadStringAsync(address, method, data, null);
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x0008BFEC File Offset: 0x0008AFEC
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadStringAsync(Uri address, string method, string data, object userToken)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "UploadStringAsync", address);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			this.InitWebClientAsync();
			this.ClearWebClientState();
			AsyncOperation asyncOp = AsyncOperationManager.CreateOperation(userToken);
			this.m_AsyncOp = asyncOp;
			try
			{
				byte[] bytes = this.Encoding.GetBytes(data);
				this.m_Method = method;
				this.m_ContentLength = (long)bytes.Length;
				WebRequest request = this.m_WebRequest = this.GetWebRequest(this.GetUri(address));
				this.UploadBits(request, null, bytes, null, null, new CompletionDelegate(this.UploadStringAsyncWriteCallback), asyncOp);
				this.DownloadBits(request, null, new CompletionDelegate(this.UploadStringAsyncReadCallback), asyncOp);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				this.UploadStringAsyncWriteCallback(null, ex, asyncOp);
			}
			catch
			{
				Exception exception = new WebException(SR.GetString("net_webclient"), new Exception(SR.GetString("net_nonClsCompliantException")));
				this.UploadStringAsyncWriteCallback(null, exception, asyncOp);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, this, "UploadStringAsync", null);
			}
		}

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x06002389 RID: 9097 RVA: 0x0008C16C File Offset: 0x0008B16C
		// (remove) Token: 0x0600238A RID: 9098 RVA: 0x0008C185 File Offset: 0x0008B185
		public event UploadDataCompletedEventHandler UploadDataCompleted;

		// Token: 0x0600238B RID: 9099 RVA: 0x0008C19E File Offset: 0x0008B19E
		protected virtual void OnUploadDataCompleted(UploadDataCompletedEventArgs e)
		{
			if (this.UploadDataCompleted != null)
			{
				this.UploadDataCompleted(this, e);
			}
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x0008C1B5 File Offset: 0x0008B1B5
		private void UploadDataOperationCompleted(object arg)
		{
			this.OnUploadDataCompleted((UploadDataCompletedEventArgs)arg);
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x0008C1C4 File Offset: 0x0008B1C4
		private void UploadDataAsyncWriteCallback(byte[] returnBytes, Exception exception, AsyncOperation asyncOp)
		{
			if (exception != null)
			{
				UploadDataCompletedEventArgs eventArgs = new UploadDataCompletedEventArgs(returnBytes, exception, this.m_Cancelled, asyncOp.UserSuppliedState);
				this.InvokeOperationCompleted(asyncOp, this.uploadDataOperationCompleted, eventArgs);
			}
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x0008C1F8 File Offset: 0x0008B1F8
		private void UploadDataAsyncReadCallback(byte[] returnBytes, Exception exception, AsyncOperation asyncOp)
		{
			UploadDataCompletedEventArgs eventArgs = new UploadDataCompletedEventArgs(returnBytes, exception, this.m_Cancelled, asyncOp.UserSuppliedState);
			this.InvokeOperationCompleted(asyncOp, this.uploadDataOperationCompleted, eventArgs);
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x0008C227 File Offset: 0x0008B227
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadDataAsync(Uri address, byte[] data)
		{
			this.UploadDataAsync(address, null, data, null);
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x0008C233 File Offset: 0x0008B233
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadDataAsync(Uri address, string method, byte[] data)
		{
			this.UploadDataAsync(address, method, data, null);
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x0008C240 File Offset: 0x0008B240
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadDataAsync(Uri address, string method, byte[] data, object userToken)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "UploadDataAsync", address + ", " + method);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			this.InitWebClientAsync();
			this.ClearWebClientState();
			AsyncOperation asyncOp = AsyncOperationManager.CreateOperation(userToken);
			this.m_AsyncOp = asyncOp;
			try
			{
				this.m_Method = method;
				this.m_ContentLength = (long)data.Length;
				WebRequest request = this.m_WebRequest = this.GetWebRequest(this.GetUri(address));
				this.UploadBits(request, null, data, null, null, new CompletionDelegate(this.UploadDataAsyncWriteCallback), asyncOp);
				this.DownloadBits(request, null, new CompletionDelegate(this.UploadDataAsyncReadCallback), asyncOp);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				this.UploadDataAsyncWriteCallback(null, ex, asyncOp);
			}
			catch
			{
				Exception exception = new WebException(SR.GetString("net_webclient"), new Exception(SR.GetString("net_nonClsCompliantException")));
				this.UploadDataAsyncWriteCallback(null, exception, asyncOp);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, this, "UploadDataAsync", null);
			}
		}

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x06002392 RID: 9106 RVA: 0x0008C3BC File Offset: 0x0008B3BC
		// (remove) Token: 0x06002393 RID: 9107 RVA: 0x0008C3D5 File Offset: 0x0008B3D5
		public event UploadFileCompletedEventHandler UploadFileCompleted;

		// Token: 0x06002394 RID: 9108 RVA: 0x0008C3EE File Offset: 0x0008B3EE
		protected virtual void OnUploadFileCompleted(UploadFileCompletedEventArgs e)
		{
			if (this.UploadFileCompleted != null)
			{
				this.UploadFileCompleted(this, e);
			}
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x0008C405 File Offset: 0x0008B405
		private void UploadFileOperationCompleted(object arg)
		{
			this.OnUploadFileCompleted((UploadFileCompletedEventArgs)arg);
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x0008C414 File Offset: 0x0008B414
		private void UploadFileAsyncWriteCallback(byte[] returnBytes, Exception exception, AsyncOperation asyncOp)
		{
			if (exception != null)
			{
				UploadFileCompletedEventArgs eventArgs = new UploadFileCompletedEventArgs(returnBytes, exception, this.m_Cancelled, asyncOp.UserSuppliedState);
				this.InvokeOperationCompleted(asyncOp, this.uploadFileOperationCompleted, eventArgs);
			}
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x0008C448 File Offset: 0x0008B448
		private void UploadFileAsyncReadCallback(byte[] returnBytes, Exception exception, AsyncOperation asyncOp)
		{
			UploadFileCompletedEventArgs eventArgs = new UploadFileCompletedEventArgs(returnBytes, exception, this.m_Cancelled, asyncOp.UserSuppliedState);
			this.InvokeOperationCompleted(asyncOp, this.uploadFileOperationCompleted, eventArgs);
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x0008C477 File Offset: 0x0008B477
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadFileAsync(Uri address, string fileName)
		{
			this.UploadFileAsync(address, null, fileName, null);
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x0008C483 File Offset: 0x0008B483
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadFileAsync(Uri address, string method, string fileName)
		{
			this.UploadFileAsync(address, method, fileName, null);
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x0008C490 File Offset: 0x0008B490
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadFileAsync(Uri address, string method, string fileName, object userToken)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "UploadFileAsync", address + ", " + method);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			this.InitWebClientAsync();
			this.ClearWebClientState();
			AsyncOperation asyncOp = AsyncOperationManager.CreateOperation(userToken);
			this.m_AsyncOp = asyncOp;
			FileStream fileStream = null;
			try
			{
				this.m_Method = method;
				byte[] header = null;
				byte[] footer = null;
				byte[] buffer = null;
				Uri uri = this.GetUri(address);
				bool needsHeaderAndBoundary = uri.Scheme != Uri.UriSchemeFile;
				this.OpenFileInternal(needsHeaderAndBoundary, fileName, ref fileStream, ref buffer, ref header, ref footer);
				WebRequest request = this.m_WebRequest = this.GetWebRequest(uri);
				this.UploadBits(request, fileStream, buffer, header, footer, new CompletionDelegate(this.UploadFileAsyncWriteCallback), asyncOp);
				this.DownloadBits(request, null, new CompletionDelegate(this.UploadFileAsyncReadCallback), asyncOp);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (fileStream != null)
				{
					fileStream.Close();
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				this.UploadFileAsyncWriteCallback(null, ex, asyncOp);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, this, "UploadFileAsync", null);
			}
		}

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x0600239B RID: 9115 RVA: 0x0008C610 File Offset: 0x0008B610
		// (remove) Token: 0x0600239C RID: 9116 RVA: 0x0008C629 File Offset: 0x0008B629
		public event UploadValuesCompletedEventHandler UploadValuesCompleted;

		// Token: 0x0600239D RID: 9117 RVA: 0x0008C642 File Offset: 0x0008B642
		protected virtual void OnUploadValuesCompleted(UploadValuesCompletedEventArgs e)
		{
			if (this.UploadValuesCompleted != null)
			{
				this.UploadValuesCompleted(this, e);
			}
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x0008C659 File Offset: 0x0008B659
		private void UploadValuesOperationCompleted(object arg)
		{
			this.OnUploadValuesCompleted((UploadValuesCompletedEventArgs)arg);
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x0008C668 File Offset: 0x0008B668
		private void UploadValuesAsyncWriteCallback(byte[] returnBytes, Exception exception, AsyncOperation asyncOp)
		{
			if (exception != null)
			{
				UploadValuesCompletedEventArgs eventArgs = new UploadValuesCompletedEventArgs(returnBytes, exception, this.m_Cancelled, asyncOp.UserSuppliedState);
				this.InvokeOperationCompleted(asyncOp, this.uploadValuesOperationCompleted, eventArgs);
			}
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x0008C69C File Offset: 0x0008B69C
		private void UploadValuesAsyncReadCallback(byte[] returnBytes, Exception exception, AsyncOperation asyncOp)
		{
			UploadValuesCompletedEventArgs eventArgs = new UploadValuesCompletedEventArgs(returnBytes, exception, this.m_Cancelled, asyncOp.UserSuppliedState);
			this.InvokeOperationCompleted(asyncOp, this.uploadValuesOperationCompleted, eventArgs);
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x0008C6CB File Offset: 0x0008B6CB
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadValuesAsync(Uri address, NameValueCollection data)
		{
			this.UploadValuesAsync(address, null, data, null);
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x0008C6D7 File Offset: 0x0008B6D7
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadValuesAsync(Uri address, string method, NameValueCollection data)
		{
			this.UploadValuesAsync(address, method, data, null);
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x0008C6E4 File Offset: 0x0008B6E4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void UploadValuesAsync(Uri address, string method, NameValueCollection data, object userToken)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "UploadValuesAsync", address + ", " + method);
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (method == null)
			{
				method = this.MapToDefaultMethod(address);
			}
			this.InitWebClientAsync();
			this.ClearWebClientState();
			AsyncOperation asyncOp = AsyncOperationManager.CreateOperation(userToken);
			this.m_AsyncOp = asyncOp;
			try
			{
				byte[] buffer = this.UploadValuesInternal(data);
				this.m_Method = method;
				WebRequest request = this.m_WebRequest = this.GetWebRequest(this.GetUri(address));
				this.UploadBits(request, null, buffer, null, null, new CompletionDelegate(this.UploadValuesAsyncWriteCallback), asyncOp);
				this.DownloadBits(request, null, new CompletionDelegate(this.UploadValuesAsyncReadCallback), asyncOp);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is WebException) && !(ex is SecurityException))
				{
					ex = new WebException(SR.GetString("net_webclient"), ex);
				}
				this.UploadValuesAsyncWriteCallback(null, ex, asyncOp);
			}
			catch
			{
				Exception exception = new WebException(SR.GetString("net_webclient"), new Exception(SR.GetString("net_nonClsCompliantException")));
				this.UploadValuesAsyncWriteCallback(null, exception, asyncOp);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, this, "UploadValuesAsync", null);
			}
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x0008C860 File Offset: 0x0008B860
		public void CancelAsync()
		{
			WebRequest webRequest = this.m_WebRequest;
			this.m_Cancelled = true;
			WebClient.AbortRequest(webRequest);
		}

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x060023A5 RID: 9125 RVA: 0x0008C881 File Offset: 0x0008B881
		// (remove) Token: 0x060023A6 RID: 9126 RVA: 0x0008C89A File Offset: 0x0008B89A
		public event DownloadProgressChangedEventHandler DownloadProgressChanged;

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x060023A7 RID: 9127 RVA: 0x0008C8B3 File Offset: 0x0008B8B3
		// (remove) Token: 0x060023A8 RID: 9128 RVA: 0x0008C8CC File Offset: 0x0008B8CC
		public event UploadProgressChangedEventHandler UploadProgressChanged;

		// Token: 0x060023A9 RID: 9129 RVA: 0x0008C8E5 File Offset: 0x0008B8E5
		protected virtual void OnDownloadProgressChanged(DownloadProgressChangedEventArgs e)
		{
			if (this.DownloadProgressChanged != null)
			{
				this.DownloadProgressChanged(this, e);
			}
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x0008C8FC File Offset: 0x0008B8FC
		protected virtual void OnUploadProgressChanged(UploadProgressChangedEventArgs e)
		{
			if (this.UploadProgressChanged != null)
			{
				this.UploadProgressChanged(this, e);
			}
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x0008C913 File Offset: 0x0008B913
		private void ReportDownloadProgressChanged(object arg)
		{
			this.OnDownloadProgressChanged((DownloadProgressChangedEventArgs)arg);
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x0008C921 File Offset: 0x0008B921
		private void ReportUploadProgressChanged(object arg)
		{
			this.OnUploadProgressChanged((UploadProgressChangedEventArgs)arg);
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x0008C930 File Offset: 0x0008B930
		private void PostProgressChanged(AsyncOperation asyncOp, WebClient.ProgressData progress)
		{
			if (asyncOp != null && progress.BytesSent + progress.BytesReceived > 0L)
			{
				int progressPercentage;
				if (progress.HasUploadPhase)
				{
					if (progress.TotalBytesToReceive < 0L && progress.BytesReceived == 0L)
					{
						progressPercentage = ((progress.TotalBytesToSend < 0L) ? 0 : ((progress.TotalBytesToSend == 0L) ? 50 : ((int)(50L * progress.BytesSent / progress.TotalBytesToSend))));
					}
					else
					{
						progressPercentage = ((progress.TotalBytesToSend < 0L) ? 50 : ((progress.TotalBytesToReceive == 0L) ? 100 : ((int)(50L * progress.BytesReceived / progress.TotalBytesToReceive + 50L))));
					}
					asyncOp.Post(this.reportUploadProgressChanged, new UploadProgressChangedEventArgs(progressPercentage, asyncOp.UserSuppliedState, progress.BytesSent, progress.TotalBytesToSend, progress.BytesReceived, progress.TotalBytesToReceive));
					return;
				}
				progressPercentage = ((progress.TotalBytesToReceive < 0L) ? 0 : ((progress.TotalBytesToReceive == 0L) ? 100 : ((int)(100L * progress.BytesReceived / progress.TotalBytesToReceive))));
				asyncOp.Post(this.reportDownloadProgressChanged, new DownloadProgressChangedEventArgs(progressPercentage, asyncOp.UserSuppliedState, progress.BytesReceived, progress.TotalBytesToReceive));
			}
		}

		// Token: 0x04002419 RID: 9241
		private const int DefaultCopyBufferLength = 8192;

		// Token: 0x0400241A RID: 9242
		private const int DefaultDownloadBufferLength = 65536;

		// Token: 0x0400241B RID: 9243
		private const string DefaultUploadFileContentType = "application/octet-stream";

		// Token: 0x0400241C RID: 9244
		private const string UploadFileContentType = "multipart/form-data";

		// Token: 0x0400241D RID: 9245
		private const string UploadValuesContentType = "application/x-www-form-urlencoded";

		// Token: 0x0400241E RID: 9246
		private Uri m_baseAddress;

		// Token: 0x0400241F RID: 9247
		private ICredentials m_credentials;

		// Token: 0x04002420 RID: 9248
		private WebHeaderCollection m_headers;

		// Token: 0x04002421 RID: 9249
		private NameValueCollection m_requestParameters;

		// Token: 0x04002422 RID: 9250
		private WebResponse m_WebResponse;

		// Token: 0x04002423 RID: 9251
		private WebRequest m_WebRequest;

		// Token: 0x04002424 RID: 9252
		private Encoding m_Encoding = Encoding.Default;

		// Token: 0x04002425 RID: 9253
		private string m_Method;

		// Token: 0x04002426 RID: 9254
		private long m_ContentLength = -1L;

		// Token: 0x04002427 RID: 9255
		private bool m_InitWebClientAsync;

		// Token: 0x04002428 RID: 9256
		private bool m_Cancelled;

		// Token: 0x04002429 RID: 9257
		private WebClient.ProgressData m_Progress;

		// Token: 0x0400242A RID: 9258
		private IWebProxy m_Proxy;

		// Token: 0x0400242B RID: 9259
		private bool m_ProxySet;

		// Token: 0x0400242C RID: 9260
		private RequestCachePolicy m_CachePolicy;

		// Token: 0x0400242D RID: 9261
		private int m_CallNesting;

		// Token: 0x0400242E RID: 9262
		private AsyncOperation m_AsyncOp;

		// Token: 0x04002430 RID: 9264
		private SendOrPostCallback openReadOperationCompleted;

		// Token: 0x04002432 RID: 9266
		private SendOrPostCallback openWriteOperationCompleted;

		// Token: 0x04002434 RID: 9268
		private SendOrPostCallback downloadStringOperationCompleted;

		// Token: 0x04002436 RID: 9270
		private SendOrPostCallback downloadDataOperationCompleted;

		// Token: 0x04002438 RID: 9272
		private SendOrPostCallback downloadFileOperationCompleted;

		// Token: 0x0400243A RID: 9274
		private SendOrPostCallback uploadStringOperationCompleted;

		// Token: 0x0400243C RID: 9276
		private SendOrPostCallback uploadDataOperationCompleted;

		// Token: 0x0400243E RID: 9278
		private SendOrPostCallback uploadFileOperationCompleted;

		// Token: 0x04002440 RID: 9280
		private SendOrPostCallback uploadValuesOperationCompleted;

		// Token: 0x04002443 RID: 9283
		private SendOrPostCallback reportDownloadProgressChanged;

		// Token: 0x04002444 RID: 9284
		private SendOrPostCallback reportUploadProgressChanged;

		// Token: 0x02000485 RID: 1157
		private class ProgressData
		{
			// Token: 0x060023AE RID: 9134 RVA: 0x0008CA5C File Offset: 0x0008BA5C
			internal void Reset()
			{
				this.BytesSent = 0L;
				this.TotalBytesToSend = -1L;
				this.BytesReceived = 0L;
				this.TotalBytesToReceive = -1L;
				this.HasUploadPhase = false;
			}

			// Token: 0x04002445 RID: 9285
			internal long BytesSent;

			// Token: 0x04002446 RID: 9286
			internal long TotalBytesToSend = -1L;

			// Token: 0x04002447 RID: 9287
			internal long BytesReceived;

			// Token: 0x04002448 RID: 9288
			internal long TotalBytesToReceive = -1L;

			// Token: 0x04002449 RID: 9289
			internal bool HasUploadPhase;
		}

		// Token: 0x02000486 RID: 1158
		private class DownloadBitsState
		{
			// Token: 0x060023B0 RID: 9136 RVA: 0x0008CA9D File Offset: 0x0008BA9D
			internal DownloadBitsState(WebRequest request, Stream writeStream, CompletionDelegate completionDelegate, AsyncOperation asyncOp, WebClient.ProgressData progress, WebClient webClient)
			{
				this.WriteStream = writeStream;
				this.Request = request;
				this.AsyncOp = asyncOp;
				this.CompletionDelegate = completionDelegate;
				this.WebClient = webClient;
				this.Progress = progress;
			}

			// Token: 0x1700075E RID: 1886
			// (get) Token: 0x060023B1 RID: 9137 RVA: 0x0008CAD2 File Offset: 0x0008BAD2
			internal bool Async
			{
				get
				{
					return this.AsyncOp != null;
				}
			}

			// Token: 0x060023B2 RID: 9138 RVA: 0x0008CAE0 File Offset: 0x0008BAE0
			internal int SetResponse(WebResponse response)
			{
				this.ContentLength = response.ContentLength;
				if (this.ContentLength == -1L || this.ContentLength > 65536L)
				{
					this.Length = 65536L;
				}
				else
				{
					this.Length = this.ContentLength;
				}
				if (this.WriteStream == null)
				{
					if (this.ContentLength > 2147483647L)
					{
						throw new WebException(SR.GetString("net_webstatus_MessageLengthLimitExceeded"), WebExceptionStatus.MessageLengthLimitExceeded);
					}
					this.SgBuffers = new ScatterGatherBuffers(this.Length);
				}
				this.InnerBuffer = new byte[(int)this.Length];
				this.ReadStream = response.GetResponseStream();
				if (this.Async && response.ContentLength >= 0L)
				{
					this.Progress.TotalBytesToReceive = response.ContentLength;
				}
				if (this.Async)
				{
					if (this.ReadStream == null || this.ReadStream == Stream.Null)
					{
						WebClient.DownloadBitsReadCallbackState(this, null);
					}
					else
					{
						this.ReadStream.BeginRead(this.InnerBuffer, this.Offset, (int)this.Length - this.Offset, new AsyncCallback(WebClient.DownloadBitsReadCallback), this);
					}
					return -1;
				}
				if (this.ReadStream == null || this.ReadStream == Stream.Null)
				{
					return 0;
				}
				return this.ReadStream.Read(this.InnerBuffer, this.Offset, (int)this.Length - this.Offset);
			}

			// Token: 0x060023B3 RID: 9139 RVA: 0x0008CC40 File Offset: 0x0008BC40
			internal bool RetrieveBytes(ref int bytesRetrieved)
			{
				if (bytesRetrieved > 0)
				{
					if (this.WriteStream != null)
					{
						this.WriteStream.Write(this.InnerBuffer, 0, bytesRetrieved);
					}
					else
					{
						this.SgBuffers.Write(this.InnerBuffer, 0, bytesRetrieved);
					}
					if (this.Async)
					{
						this.Progress.BytesReceived += (long)bytesRetrieved;
					}
					if ((long)this.Offset != this.ContentLength)
					{
						if (this.Async)
						{
							this.WebClient.PostProgressChanged(this.AsyncOp, this.Progress);
							this.ReadStream.BeginRead(this.InnerBuffer, this.Offset, (int)this.Length - this.Offset, new AsyncCallback(WebClient.DownloadBitsReadCallback), this);
						}
						else
						{
							bytesRetrieved = this.ReadStream.Read(this.InnerBuffer, this.Offset, (int)this.Length - this.Offset);
						}
						return false;
					}
				}
				if (this.Async)
				{
					if (this.Progress.TotalBytesToReceive < 0L)
					{
						this.Progress.TotalBytesToReceive = this.Progress.BytesReceived;
					}
					this.WebClient.PostProgressChanged(this.AsyncOp, this.Progress);
				}
				if (this.ReadStream != null)
				{
					this.ReadStream.Close();
				}
				if (this.WriteStream != null)
				{
					this.WriteStream.Close();
				}
				else if (this.WriteStream == null)
				{
					byte[] array = new byte[this.SgBuffers.Length];
					if (this.SgBuffers.Length > 0)
					{
						BufferOffsetSize[] buffers = this.SgBuffers.GetBuffers();
						int num = 0;
						foreach (BufferOffsetSize bufferOffsetSize in buffers)
						{
							Buffer.BlockCopy(bufferOffsetSize.Buffer, 0, array, num, bufferOffsetSize.Size);
							num += bufferOffsetSize.Size;
						}
					}
					this.InnerBuffer = array;
				}
				return true;
			}

			// Token: 0x060023B4 RID: 9140 RVA: 0x0008CE0C File Offset: 0x0008BE0C
			internal void Close()
			{
				if (this.WriteStream != null)
				{
					this.WriteStream.Close();
				}
				if (this.ReadStream != null)
				{
					this.ReadStream.Close();
				}
			}

			// Token: 0x0400244A RID: 9290
			internal WebClient WebClient;

			// Token: 0x0400244B RID: 9291
			internal Stream WriteStream;

			// Token: 0x0400244C RID: 9292
			internal byte[] InnerBuffer;

			// Token: 0x0400244D RID: 9293
			internal AsyncOperation AsyncOp;

			// Token: 0x0400244E RID: 9294
			internal WebRequest Request;

			// Token: 0x0400244F RID: 9295
			internal CompletionDelegate CompletionDelegate;

			// Token: 0x04002450 RID: 9296
			internal Stream ReadStream;

			// Token: 0x04002451 RID: 9297
			internal ScatterGatherBuffers SgBuffers;

			// Token: 0x04002452 RID: 9298
			internal long ContentLength;

			// Token: 0x04002453 RID: 9299
			internal long Length;

			// Token: 0x04002454 RID: 9300
			internal int Offset;

			// Token: 0x04002455 RID: 9301
			internal WebClient.ProgressData Progress;
		}

		// Token: 0x02000487 RID: 1159
		private class UploadBitsState
		{
			// Token: 0x060023B5 RID: 9141 RVA: 0x0008CE34 File Offset: 0x0008BE34
			internal UploadBitsState(WebRequest request, Stream readStream, byte[] buffer, byte[] header, byte[] footer, CompletionDelegate completionDelegate, AsyncOperation asyncOp, WebClient.ProgressData progress, WebClient webClient)
			{
				this.InnerBuffer = buffer;
				this.Header = header;
				this.Footer = footer;
				this.ReadStream = readStream;
				this.Request = request;
				this.AsyncOp = asyncOp;
				this.CompletionDelegate = completionDelegate;
				if (this.AsyncOp != null)
				{
					this.Progress = progress;
					this.Progress.HasUploadPhase = true;
					this.Progress.TotalBytesToSend = ((request.ContentLength < 0L) ? -1L : request.ContentLength);
				}
				this.WebClient = webClient;
			}

			// Token: 0x1700075F RID: 1887
			// (get) Token: 0x060023B6 RID: 9142 RVA: 0x0008CEBF File Offset: 0x0008BEBF
			internal bool FileUpload
			{
				get
				{
					return this.ReadStream != null;
				}
			}

			// Token: 0x17000760 RID: 1888
			// (get) Token: 0x060023B7 RID: 9143 RVA: 0x0008CECD File Offset: 0x0008BECD
			internal bool Async
			{
				get
				{
					return this.AsyncOp != null;
				}
			}

			// Token: 0x060023B8 RID: 9144 RVA: 0x0008CEDC File Offset: 0x0008BEDC
			internal void SetRequestStream(Stream writeStream)
			{
				this.WriteStream = writeStream;
				byte[] array;
				if (this.Header != null)
				{
					array = this.Header;
					this.Header = null;
				}
				else
				{
					array = new byte[0];
				}
				if (this.Async)
				{
					this.Progress.BytesSent += (long)array.Length;
					this.WriteStream.BeginWrite(array, 0, array.Length, new AsyncCallback(WebClient.UploadBitsWriteCallback), this);
					return;
				}
				this.WriteStream.Write(array, 0, array.Length);
			}

			// Token: 0x060023B9 RID: 9145 RVA: 0x0008CF60 File Offset: 0x0008BF60
			internal bool WriteBytes()
			{
				if (this.Async)
				{
					this.WebClient.PostProgressChanged(this.AsyncOp, this.Progress);
				}
				int num2;
				byte[] buffer;
				if (this.FileUpload)
				{
					int num = 0;
					if (this.InnerBuffer != null)
					{
						num = this.ReadStream.Read(this.InnerBuffer, 0, this.InnerBuffer.Length);
						if (num <= 0)
						{
							this.ReadStream.Close();
							this.InnerBuffer = null;
						}
					}
					if (this.InnerBuffer != null)
					{
						num2 = num;
						buffer = this.InnerBuffer;
					}
					else
					{
						if (this.Footer == null)
						{
							return true;
						}
						num2 = this.Footer.Length;
						buffer = this.Footer;
						this.Footer = null;
					}
				}
				else
				{
					if (this.InnerBuffer == null)
					{
						return true;
					}
					num2 = this.InnerBuffer.Length;
					buffer = this.InnerBuffer;
					this.InnerBuffer = null;
				}
				if (this.Async)
				{
					this.Progress.BytesSent += (long)num2;
					this.WriteStream.BeginWrite(buffer, 0, num2, new AsyncCallback(WebClient.UploadBitsWriteCallback), this);
				}
				else
				{
					this.WriteStream.Write(buffer, 0, num2);
				}
				return false;
			}

			// Token: 0x060023BA RID: 9146 RVA: 0x0008D075 File Offset: 0x0008C075
			internal void Close()
			{
				if (this.WriteStream != null)
				{
					this.WriteStream.Close();
				}
				if (this.ReadStream != null)
				{
					this.ReadStream.Close();
				}
			}

			// Token: 0x04002456 RID: 9302
			internal WebClient WebClient;

			// Token: 0x04002457 RID: 9303
			internal Stream WriteStream;

			// Token: 0x04002458 RID: 9304
			internal byte[] InnerBuffer;

			// Token: 0x04002459 RID: 9305
			internal byte[] Header;

			// Token: 0x0400245A RID: 9306
			internal byte[] Footer;

			// Token: 0x0400245B RID: 9307
			internal AsyncOperation AsyncOp;

			// Token: 0x0400245C RID: 9308
			internal WebRequest Request;

			// Token: 0x0400245D RID: 9309
			internal CompletionDelegate CompletionDelegate;

			// Token: 0x0400245E RID: 9310
			internal Stream ReadStream;

			// Token: 0x0400245F RID: 9311
			internal long Length;

			// Token: 0x04002460 RID: 9312
			internal int Offset;

			// Token: 0x04002461 RID: 9313
			internal WebClient.ProgressData Progress;
		}

		// Token: 0x02000488 RID: 1160
		private class WebClientWriteStream : Stream
		{
			// Token: 0x060023BB RID: 9147 RVA: 0x0008D09D File Offset: 0x0008C09D
			public WebClientWriteStream(Stream stream, WebRequest request, WebClient webClient)
			{
				this.m_request = request;
				this.m_stream = stream;
				this.m_WebClient = webClient;
			}

			// Token: 0x17000761 RID: 1889
			// (get) Token: 0x060023BC RID: 9148 RVA: 0x0008D0BA File Offset: 0x0008C0BA
			public override bool CanRead
			{
				get
				{
					return this.m_stream.CanRead;
				}
			}

			// Token: 0x17000762 RID: 1890
			// (get) Token: 0x060023BD RID: 9149 RVA: 0x0008D0C7 File Offset: 0x0008C0C7
			public override bool CanSeek
			{
				get
				{
					return this.m_stream.CanSeek;
				}
			}

			// Token: 0x17000763 RID: 1891
			// (get) Token: 0x060023BE RID: 9150 RVA: 0x0008D0D4 File Offset: 0x0008C0D4
			public override bool CanWrite
			{
				get
				{
					return this.m_stream.CanWrite;
				}
			}

			// Token: 0x17000764 RID: 1892
			// (get) Token: 0x060023BF RID: 9151 RVA: 0x0008D0E1 File Offset: 0x0008C0E1
			public override bool CanTimeout
			{
				get
				{
					return this.m_stream.CanTimeout;
				}
			}

			// Token: 0x17000765 RID: 1893
			// (get) Token: 0x060023C0 RID: 9152 RVA: 0x0008D0EE File Offset: 0x0008C0EE
			// (set) Token: 0x060023C1 RID: 9153 RVA: 0x0008D0FB File Offset: 0x0008C0FB
			public override int ReadTimeout
			{
				get
				{
					return this.m_stream.ReadTimeout;
				}
				set
				{
					this.m_stream.ReadTimeout = value;
				}
			}

			// Token: 0x17000766 RID: 1894
			// (get) Token: 0x060023C2 RID: 9154 RVA: 0x0008D109 File Offset: 0x0008C109
			// (set) Token: 0x060023C3 RID: 9155 RVA: 0x0008D116 File Offset: 0x0008C116
			public override int WriteTimeout
			{
				get
				{
					return this.m_stream.WriteTimeout;
				}
				set
				{
					this.m_stream.WriteTimeout = value;
				}
			}

			// Token: 0x17000767 RID: 1895
			// (get) Token: 0x060023C4 RID: 9156 RVA: 0x0008D124 File Offset: 0x0008C124
			public override long Length
			{
				get
				{
					return this.m_stream.Length;
				}
			}

			// Token: 0x17000768 RID: 1896
			// (get) Token: 0x060023C5 RID: 9157 RVA: 0x0008D131 File Offset: 0x0008C131
			// (set) Token: 0x060023C6 RID: 9158 RVA: 0x0008D13E File Offset: 0x0008C13E
			public override long Position
			{
				get
				{
					return this.m_stream.Position;
				}
				set
				{
					this.m_stream.Position = value;
				}
			}

			// Token: 0x060023C7 RID: 9159 RVA: 0x0008D14C File Offset: 0x0008C14C
			[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
			{
				return this.m_stream.BeginRead(buffer, offset, size, callback, state);
			}

			// Token: 0x060023C8 RID: 9160 RVA: 0x0008D160 File Offset: 0x0008C160
			[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
			{
				return this.m_stream.BeginWrite(buffer, offset, size, callback, state);
			}

			// Token: 0x060023C9 RID: 9161 RVA: 0x0008D174 File Offset: 0x0008C174
			protected override void Dispose(bool disposing)
			{
				try
				{
					if (disposing)
					{
						this.m_stream.Close();
						this.m_WebClient.GetWebResponse(this.m_request).Close();
					}
				}
				finally
				{
					base.Dispose(disposing);
				}
			}

			// Token: 0x060023CA RID: 9162 RVA: 0x0008D1C0 File Offset: 0x0008C1C0
			public override int EndRead(IAsyncResult result)
			{
				return this.m_stream.EndRead(result);
			}

			// Token: 0x060023CB RID: 9163 RVA: 0x0008D1CE File Offset: 0x0008C1CE
			public override void EndWrite(IAsyncResult result)
			{
				this.m_stream.EndWrite(result);
			}

			// Token: 0x060023CC RID: 9164 RVA: 0x0008D1DC File Offset: 0x0008C1DC
			public override void Flush()
			{
				this.m_stream.Flush();
			}

			// Token: 0x060023CD RID: 9165 RVA: 0x0008D1E9 File Offset: 0x0008C1E9
			public override int Read(byte[] buffer, int offset, int count)
			{
				return this.m_stream.Read(buffer, offset, count);
			}

			// Token: 0x060023CE RID: 9166 RVA: 0x0008D1F9 File Offset: 0x0008C1F9
			public override long Seek(long offset, SeekOrigin origin)
			{
				return this.m_stream.Seek(offset, origin);
			}

			// Token: 0x060023CF RID: 9167 RVA: 0x0008D208 File Offset: 0x0008C208
			public override void SetLength(long value)
			{
				this.m_stream.SetLength(value);
			}

			// Token: 0x060023D0 RID: 9168 RVA: 0x0008D216 File Offset: 0x0008C216
			public override void Write(byte[] buffer, int offset, int count)
			{
				this.m_stream.Write(buffer, offset, count);
			}

			// Token: 0x04002462 RID: 9314
			private WebRequest m_request;

			// Token: 0x04002463 RID: 9315
			private Stream m_stream;

			// Token: 0x04002464 RID: 9316
			private WebClient m_WebClient;
		}
	}
}
