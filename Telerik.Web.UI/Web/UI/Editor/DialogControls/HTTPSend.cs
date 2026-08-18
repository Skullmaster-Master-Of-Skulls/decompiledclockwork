using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x020019EB RID: 6635
	public class HTTPSend : IDisposable
	{
		// Token: 0x060100B6 RID: 65718 RVA: 0x00399BBC File Offset: 0x00397DBC
		public void SendTextAsFile(string content, string filename)
		{
			Uri requestUri = new Uri(this.URL);
			this.coRequest = (HttpWebRequest)WebRequest.Create(requestUri);
			this.coRequest.ProtocolVersion = this.TransferHttpVersion;
			this.coRequest.Method = "POST";
			this.coRequest.UserAgent = HttpContext.Current.Request.UserAgent;
			this.coRequest.ContentType = "multipart/form-data; boundary=" + this.BeginBoundary;
			this.coRequest.Headers.Add("Cache-Control", "no-cache");
			this.coRequest.KeepAlive = this._keepAlive;
			this.coRequest.Pipelined = this._pipelined;
			this.coRequest.SendChunked = this._chunked;
			if (this.Credentials != null)
			{
				this.coRequest.Credentials = this.Credentials;
			}
			if (this.Certificate != null)
			{
				this.coRequest.ClientCertificates.Add(this.Certificate);
			}
			string formFields = this.GetFormFields();
			string fileHeader = this.GetFileHeader(filename);
			string fileTrailer = this.GetFileTrailer();
			byte[] bytes = Encoding.UTF8.GetBytes(content);
			this.coRequest.ContentLength = (long)(formFields.Length + fileHeader.Length + fileTrailer.Length + bytes.Length);
			Stream stream = null;
			try
			{
				stream = this.GetStream();
				HTTPSend.WriteString(stream, formFields);
				HTTPSend.WriteString(stream, fileHeader);
				stream.Write(bytes, 0, bytes.Length);
				HTTPSend.WriteString(stream, fileTrailer);
				this.GetResponse();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
			}
			this.coRequest = null;
		}

		// Token: 0x060100B7 RID: 65719 RVA: 0x00399D78 File Offset: 0x00397F78
		public void SendFile(string filename)
		{
			Uri requestUri = new Uri(this.URL);
			this.coRequest = (HttpWebRequest)WebRequest.Create(requestUri);
			this.coRequest.ProtocolVersion = this.TransferHttpVersion;
			this.coRequest.Method = "POST";
			this.coRequest.ContentType = "multipart/form-data; boundary=" + this.BeginBoundary;
			this.coRequest.Headers.Add("Cache-Control", "no-cache");
			this.coRequest.KeepAlive = this._keepAlive;
			this.coRequest.Pipelined = this._pipelined;
			this.coRequest.SendChunked = this._chunked;
			if (this.Credentials != null)
			{
				this.coRequest.Credentials = this.Credentials;
			}
			if (this.Certificate != null)
			{
				this.coRequest.ClientCertificates.Add(this.Certificate);
			}
			string formFields = this.GetFormFields();
			string fileHeader = this.GetFileHeader(filename);
			string fileTrailer = this.GetFileTrailer();
			FileInfo fileInfo = new FileInfo(filename);
			this.coRequest.ContentLength = (long)(formFields.Length + fileHeader.Length + fileTrailer.Length) + fileInfo.Length;
			Stream stream = null;
			try
			{
				stream = this.GetStream();
				HTTPSend.WriteString(stream, formFields);
				HTTPSend.WriteString(stream, fileHeader);
				this.WriteFile(stream, filename);
				HTTPSend.WriteString(stream, fileTrailer);
				this.GetResponse();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
			}
			this.coRequest = null;
		}

		// Token: 0x17004D7B RID: 19835
		// (get) Token: 0x060100B8 RID: 65720 RVA: 0x00399F14 File Offset: 0x00398114
		// (set) Token: 0x060100B9 RID: 65721 RVA: 0x00399F1C File Offset: 0x0039811C
		public Version TransferHttpVersion
		{
			get
			{
				return this.coHttpVersion;
			}
			set
			{
				this.coHttpVersion = value;
			}
		}

		// Token: 0x17004D7C RID: 19836
		// (get) Token: 0x060100BA RID: 65722 RVA: 0x00399F25 File Offset: 0x00398125
		// (set) Token: 0x060100BB RID: 65723 RVA: 0x00399F2D File Offset: 0x0039812D
		public string FileContentType
		{
			get
			{
				return this.coFileContentType;
			}
			set
			{
				this.coFileContentType = value;
			}
		}

		// Token: 0x17004D7D RID: 19837
		// (get) Token: 0x060100BC RID: 65724 RVA: 0x00399F36 File Offset: 0x00398136
		// (set) Token: 0x060100BD RID: 65725 RVA: 0x00399F3E File Offset: 0x0039813E
		public string BeginBoundary
		{
			get
			{
				return this._beginBoundary;
			}
			set
			{
				this._beginBoundary = value;
				this.ContentBoundary = "--" + this.BeginBoundary;
				this.EndingBoundary = this.ContentBoundary + "--";
			}
		}

		// Token: 0x17004D7E RID: 19838
		// (get) Token: 0x060100BE RID: 65726 RVA: 0x00399F73 File Offset: 0x00398173
		// (set) Token: 0x060100BF RID: 65727 RVA: 0x00399F7B File Offset: 0x0039817B
		public string ContentBoundary
		{
			get
			{
				return this._contentBoundary;
			}
			set
			{
				this._contentBoundary = value;
			}
		}

		// Token: 0x17004D7F RID: 19839
		// (get) Token: 0x060100C0 RID: 65728 RVA: 0x00399F84 File Offset: 0x00398184
		// (set) Token: 0x060100C1 RID: 65729 RVA: 0x00399F8C File Offset: 0x0039818C
		public string EndingBoundary
		{
			get
			{
				return this._endingBoundary;
			}
			set
			{
				this._endingBoundary = value;
			}
		}

		// Token: 0x17004D80 RID: 19840
		// (get) Token: 0x060100C2 RID: 65730 RVA: 0x00399F95 File Offset: 0x00398195
		// (set) Token: 0x060100C3 RID: 65731 RVA: 0x00399F9D File Offset: 0x0039819D
		public StringBuilder ResponseText
		{
			get
			{
				return this._responseText;
			}
			set
			{
				this._responseText = value;
			}
		}

		// Token: 0x17004D81 RID: 19841
		// (get) Token: 0x060100C4 RID: 65732 RVA: 0x00399FA6 File Offset: 0x003981A6
		// (set) Token: 0x060100C5 RID: 65733 RVA: 0x00399FAE File Offset: 0x003981AE
		public string URL
		{
			get
			{
				return this._url;
			}
			set
			{
				this._url = value;
			}
		}

		// Token: 0x17004D82 RID: 19842
		// (get) Token: 0x060100C6 RID: 65734 RVA: 0x00399FB7 File Offset: 0x003981B7
		// (set) Token: 0x060100C7 RID: 65735 RVA: 0x00399FBF File Offset: 0x003981BF
		public int BufferSize
		{
			get
			{
				return this._bufferSize;
			}
			set
			{
				this._bufferSize = value;
			}
		}

		// Token: 0x17004D83 RID: 19843
		// (get) Token: 0x060100C8 RID: 65736 RVA: 0x00399FC8 File Offset: 0x003981C8
		// (set) Token: 0x060100C9 RID: 65737 RVA: 0x00399FD0 File Offset: 0x003981D0
		public ICredentials Credentials
		{
			get
			{
				return this._credentials;
			}
			set
			{
				this._credentials = value;
			}
		}

		// Token: 0x17004D84 RID: 19844
		// (get) Token: 0x060100CA RID: 65738 RVA: 0x00399FD9 File Offset: 0x003981D9
		// (set) Token: 0x060100CB RID: 65739 RVA: 0x00399FE1 File Offset: 0x003981E1
		public X509Certificate Certificate
		{
			get
			{
				return this._certificate;
			}
			set
			{
				this._certificate = value;
			}
		}

		// Token: 0x17004D85 RID: 19845
		// (get) Token: 0x060100CC RID: 65740 RVA: 0x00399FEA File Offset: 0x003981EA
		// (set) Token: 0x060100CD RID: 65741 RVA: 0x00399FF2 File Offset: 0x003981F2
		public bool KeepAlive
		{
			get
			{
				return this._keepAlive;
			}
			set
			{
				this._keepAlive = value;
			}
		}

		// Token: 0x17004D86 RID: 19846
		// (get) Token: 0x060100CE RID: 65742 RVA: 0x00399FFB File Offset: 0x003981FB
		// (set) Token: 0x060100CF RID: 65743 RVA: 0x0039A003 File Offset: 0x00398203
		public bool Expect100
		{
			get
			{
				return this._expect100;
			}
			set
			{
				this._expect100 = value;
			}
		}

		// Token: 0x17004D87 RID: 19847
		// (get) Token: 0x060100D0 RID: 65744 RVA: 0x0039A00C File Offset: 0x0039820C
		// (set) Token: 0x060100D1 RID: 65745 RVA: 0x0039A014 File Offset: 0x00398214
		public bool Pipelined
		{
			get
			{
				return this._pipelined;
			}
			set
			{
				this._pipelined = value;
			}
		}

		// Token: 0x17004D88 RID: 19848
		// (get) Token: 0x060100D2 RID: 65746 RVA: 0x0039A01D File Offset: 0x0039821D
		// (set) Token: 0x060100D3 RID: 65747 RVA: 0x0039A025 File Offset: 0x00398225
		public bool Chunked
		{
			get
			{
				return this._chunked;
			}
			set
			{
				this._chunked = value;
			}
		}

		// Token: 0x060100D4 RID: 65748 RVA: 0x0039A030 File Offset: 0x00398230
		public HTTPSend(string url)
		{
			this.URL = url;
			this.coFormFields = new Hashtable();
			this.ResponseText = new StringBuilder();
			this.BufferSize = 10240;
			this.BeginBoundary = "ou812--------------8c405ee4e38917c";
			this.TransferHttpVersion = HttpVersion.Version11;
			this.FileContentType = "text/html";
		}

		// Token: 0x060100D5 RID: 65749 RVA: 0x0039A08C File Offset: 0x0039828C
		public void SetFilename(string path)
		{
			this.coFileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
		}

		// Token: 0x060100D6 RID: 65750 RVA: 0x0039A09C File Offset: 0x0039829C
		public void SetField(string name, string value)
		{
			this.coFormFields[name] = value;
		}

		// Token: 0x060100D7 RID: 65751 RVA: 0x0039A0AB File Offset: 0x003982AB
		public void SetHeader(string name, string value)
		{
			this.coRequest.Headers.Add(name, value);
		}

		// Token: 0x060100D8 RID: 65752 RVA: 0x0039A0C0 File Offset: 0x003982C0
		public Stream GetStream()
		{
			Stream requestStream;
			if (this.coFileStream == null)
			{
				requestStream = this.coRequest.GetRequestStream();
			}
			else
			{
				requestStream = this.coFileStream;
			}
			return requestStream;
		}

		// Token: 0x060100D9 RID: 65753 RVA: 0x0039A0F0 File Offset: 0x003982F0
		public void GetResponse()
		{
			if (this.coFileStream != null)
			{
				return;
			}
			WebResponse webResponse = null;
			try
			{
				webResponse = this.coRequest.GetResponse();
			}
			catch (WebException ex)
			{
				webResponse = ex.Response;
			}
			if (webResponse != null)
			{
				Stream responseStream = webResponse.GetResponseStream();
				StreamReader streamReader = new StreamReader(responseStream);
				this.ResponseText.Length = 0;
				for (string value = streamReader.ReadLine(); value != null; value = streamReader.ReadLine())
				{
					this.ResponseText.Append(value);
				}
				webResponse.Close();
				return;
			}
			throw new ArgumentNullException("HTTPSend: Error retrieving server response");
		}

		// Token: 0x060100DA RID: 65754 RVA: 0x0039A188 File Offset: 0x00398388
		public string GetFormFields()
		{
			string text = "";
			IDictionaryEnumerator enumerator = this.coFormFields.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					this.ContentBoundary,
					Environment.NewLine,
					"Content-Disposition: form-data; name=\"",
					enumerator.Key.ToString(),
					"\"",
					Environment.NewLine,
					Environment.NewLine,
					enumerator.Value.ToString(),
					Environment.NewLine
				});
			}
			return text;
		}

		// Token: 0x060100DB RID: 65755 RVA: 0x0039A220 File Offset: 0x00398420
		public string GetFileHeader(string filename)
		{
			return string.Concat(new string[]
			{
				this.ContentBoundary,
				Environment.NewLine,
				"Content-Disposition: form-data; name=\"uploaded_file\"; filename=\"",
				Path.GetFileName(filename),
				"\"",
				Environment.NewLine,
				"Content-type: ",
				this.FileContentType,
				Environment.NewLine,
				Environment.NewLine
			});
		}

		// Token: 0x060100DC RID: 65756 RVA: 0x0039A28F File Offset: 0x0039848F
		public string GetFileTrailer()
		{
			return Environment.NewLine + this.EndingBoundary;
		}

		// Token: 0x060100DD RID: 65757 RVA: 0x0039A2A4 File Offset: 0x003984A4
		public static void WriteString(Stream output, string data)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(data);
			output.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x060100DE RID: 65758 RVA: 0x0039A2C8 File Offset: 0x003984C8
		public void WriteFile(Stream output, string filename)
		{
			FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
			fileStream.Seek(0L, SeekOrigin.Begin);
			byte[] buffer = new byte[this.BufferSize];
			for (int i = fileStream.Read(buffer, 0, this.BufferSize); i > 0; i = fileStream.Read(buffer, 0, this.BufferSize))
			{
				output.Write(buffer, 0, i);
			}
			fileStream.Close();
		}

		// Token: 0x060100DF RID: 65759 RVA: 0x0039A327 File Offset: 0x00398527
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.coFileStream != null)
			{
				this.coFileStream.Close();
			}
		}

		// Token: 0x060100E0 RID: 65760 RVA: 0x0039A33F File Offset: 0x0039853F
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x040048AC RID: 18604
		private const string CONTENT_DISP = "Content-Disposition: form-data; name=";

		// Token: 0x040048AD RID: 18605
		private readonly Hashtable coFormFields;

		// Token: 0x040048AE RID: 18606
		private HttpWebRequest coRequest;

		// Token: 0x040048AF RID: 18607
		private Stream coFileStream;

		// Token: 0x040048B0 RID: 18608
		private Version coHttpVersion;

		// Token: 0x040048B1 RID: 18609
		private string coFileContentType;

		// Token: 0x040048B2 RID: 18610
		private string _beginBoundary;

		// Token: 0x040048B3 RID: 18611
		private string _contentBoundary;

		// Token: 0x040048B4 RID: 18612
		private string _endingBoundary;

		// Token: 0x040048B5 RID: 18613
		private StringBuilder _responseText;

		// Token: 0x040048B6 RID: 18614
		private string _url;

		// Token: 0x040048B7 RID: 18615
		private int _bufferSize;

		// Token: 0x040048B8 RID: 18616
		private ICredentials _credentials;

		// Token: 0x040048B9 RID: 18617
		private X509Certificate _certificate;

		// Token: 0x040048BA RID: 18618
		private bool _keepAlive;

		// Token: 0x040048BB RID: 18619
		private bool _expect100;

		// Token: 0x040048BC RID: 18620
		private bool _pipelined;

		// Token: 0x040048BD RID: 18621
		private bool _chunked;
	}
}
