using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using NLog.Common;
using NLog.Internal;

namespace NLog.Targets
{
	// Token: 0x02000171 RID: 369
	[Target("WebService")]
	public sealed class WebServiceTarget : MethodCallTargetBase
	{
		// Token: 0x06000DDE RID: 3550 RVA: 0x00021762 File Offset: 0x0001F962
		public WebServiceTarget()
		{
			this.Protocol = WebServiceProtocol.Soap11;
			this.Encoding = new UTF8Encoding(false);
			this.IncludeBOM = new bool?(false);
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x00021789 File Offset: 0x0001F989
		public WebServiceTarget(string name) : this()
		{
			base.Name = name;
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x00021798 File Offset: 0x0001F998
		// (set) Token: 0x06000DE1 RID: 3553 RVA: 0x000217A0 File Offset: 0x0001F9A0
		public Uri Url { get; set; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x000217A9 File Offset: 0x0001F9A9
		// (set) Token: 0x06000DE3 RID: 3555 RVA: 0x000217B1 File Offset: 0x0001F9B1
		public string MethodName { get; set; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x000217BA File Offset: 0x0001F9BA
		// (set) Token: 0x06000DE5 RID: 3557 RVA: 0x000217C2 File Offset: 0x0001F9C2
		public string Namespace { get; set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x000217CB File Offset: 0x0001F9CB
		// (set) Token: 0x06000DE7 RID: 3559 RVA: 0x000217D3 File Offset: 0x0001F9D3
		[DefaultValue("Soap11")]
		public WebServiceProtocol Protocol { get; set; }

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x000217DC File Offset: 0x0001F9DC
		// (set) Token: 0x06000DE9 RID: 3561 RVA: 0x000217E4 File Offset: 0x0001F9E4
		public bool? IncludeBOM { get; set; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x000217ED File Offset: 0x0001F9ED
		// (set) Token: 0x06000DEB RID: 3563 RVA: 0x000217F5 File Offset: 0x0001F9F5
		public Encoding Encoding { get; set; }

		// Token: 0x06000DEC RID: 3564 RVA: 0x000217FE File Offset: 0x0001F9FE
		protected override void DoInvoke(object[] parameters)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0002181C File Offset: 0x0001FA1C
		protected override void DoInvoke(object[] parameters, AsyncContinuation continuation)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.BuildWebServiceUrl(parameters));
			Func<AsyncCallback, IAsyncResult> beginFunc = (AsyncCallback r) => request.BeginGetRequestStream(r, null);
			Func<IAsyncResult, Stream> getStreamFunc = new Func<IAsyncResult, Stream>(request.EndGetRequestStream);
			this.DoInvoke(parameters, continuation, request, beginFunc, getStreamFunc);
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x000219CC File Offset: 0x0001FBCC
		internal void DoInvoke(object[] parameters, AsyncContinuation continuation, HttpWebRequest request, Func<AsyncCallback, IAsyncResult> beginFunc, Func<IAsyncResult, Stream> getStreamFunc)
		{
			Stream postPayload = null;
			switch (this.Protocol)
			{
			case WebServiceProtocol.Soap11:
				postPayload = this.PrepareSoap11Request(request, parameters);
				break;
			case WebServiceProtocol.Soap12:
				postPayload = this.PrepareSoap12Request(request, parameters);
				break;
			case WebServiceProtocol.HttpPost:
				postPayload = this.PreparePostRequest(request, parameters);
				break;
			case WebServiceProtocol.HttpGet:
				this.PrepareGetRequest(request);
				break;
			}
			AsyncContinuation sendContinuation = delegate(Exception ex)
			{
				if (ex != null)
				{
					continuation(ex);
					return;
				}
				request.BeginGetResponse(delegate(IAsyncResult r)
				{
					try
					{
						using (request.EndGetResponse(r))
						{
						}
						continuation(null);
					}
					catch (Exception ex)
					{
						InternalLogger.Error(ex, "Error when sending to Webservice.");
						if (ex.MustBeRethrown())
						{
							throw;
						}
						continuation(ex);
					}
				}, null);
			};
			if (postPayload != null && postPayload.Length > 0L)
			{
				postPayload.Position = 0L;
				beginFunc(delegate(IAsyncResult result)
				{
					try
					{
						using (Stream stream = getStreamFunc(result))
						{
							WebServiceTarget.WriteStreamAndFixPreamble(postPayload, stream, this.IncludeBOM, this.Encoding);
							postPayload.Dispose();
						}
						sendContinuation(null);
					}
					catch (Exception ex)
					{
						postPayload.Dispose();
						InternalLogger.Error(ex, "Error when sending to Webservice.");
						if (ex.MustBeRethrown())
						{
							throw;
						}
						continuation(ex);
					}
				});
				return;
			}
			sendContinuation(null);
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x00021ACC File Offset: 0x0001FCCC
		private Uri BuildWebServiceUrl(object[] parameterValues)
		{
			if (this.Protocol != WebServiceProtocol.HttpGet)
			{
				return this.Url;
			}
			StringBuilder stringBuilder = new StringBuilder();
			string value = string.Empty;
			for (int i = 0; i < base.Parameters.Count; i++)
			{
				stringBuilder.Append(value);
				stringBuilder.Append(base.Parameters[i].Name);
				stringBuilder.Append("=");
				stringBuilder.Append(UrlHelper.UrlEncode(Convert.ToString(parameterValues[i], CultureInfo.InvariantCulture), false));
				value = "&";
			}
			UriBuilder uriBuilder = new UriBuilder(this.Url);
			if (uriBuilder.Query != null && uriBuilder.Query.Length > 1)
			{
				uriBuilder.Query = uriBuilder.Query.Substring(1) + "&" + stringBuilder.ToString();
			}
			else
			{
				uriBuilder.Query = stringBuilder.ToString();
			}
			return uriBuilder.Uri;
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x00021BB0 File Offset: 0x0001FDB0
		private MemoryStream PrepareSoap11Request(HttpWebRequest request, object[] parameterValues)
		{
			string value;
			if (this.Namespace.EndsWith("/", StringComparison.Ordinal))
			{
				value = this.Namespace + this.MethodName;
			}
			else
			{
				value = this.Namespace + "/" + this.MethodName;
			}
			request.Headers["SOAPAction"] = value;
			return this.PrepareSoapRequestPost(request, parameterValues, "http://schemas.xmlsoap.org/soap/envelope/", "soap");
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00021C1E File Offset: 0x0001FE1E
		private MemoryStream PrepareSoap12Request(HttpWebRequest request, object[] parameterValues)
		{
			return this.PrepareSoapRequestPost(request, parameterValues, "http://www.w3.org/2003/05/soap-envelope", "soap12");
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00021C34 File Offset: 0x0001FE34
		private MemoryStream PrepareSoapRequestPost(WebRequest request, object[] parameterValues, string soapEnvelopeNamespace, string soapname)
		{
			request.Method = "POST";
			request.ContentType = "text/xml; charset=" + this.Encoding.WebName;
			MemoryStream memoryStream = new MemoryStream();
			XmlWriter xmlWriter = XmlWriter.Create(memoryStream, new XmlWriterSettings
			{
				Encoding = this.Encoding
			});
			xmlWriter.WriteStartElement(soapname, "Envelope", soapEnvelopeNamespace);
			xmlWriter.WriteStartElement("Body", soapEnvelopeNamespace);
			xmlWriter.WriteStartElement(this.MethodName, this.Namespace);
			int num = 0;
			foreach (MethodCallParameter methodCallParameter in base.Parameters)
			{
				xmlWriter.WriteElementString(methodCallParameter.Name, Convert.ToString(parameterValues[num], CultureInfo.InvariantCulture));
				num++;
			}
			xmlWriter.WriteEndElement();
			xmlWriter.WriteEndElement();
			xmlWriter.WriteEndElement();
			xmlWriter.Flush();
			return memoryStream;
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x00021D2C File Offset: 0x0001FF2C
		private MemoryStream PreparePostRequest(HttpWebRequest request, object[] parameterValues)
		{
			request.Method = "POST";
			return this.PrepareHttpRequest(request, parameterValues);
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00021D41 File Offset: 0x0001FF41
		private void PrepareGetRequest(HttpWebRequest request)
		{
			request.Method = "GET";
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00021D50 File Offset: 0x0001FF50
		private MemoryStream PrepareHttpRequest(HttpWebRequest request, object[] parameterValues)
		{
			request.ContentType = "application/x-www-form-urlencoded; charset=" + this.Encoding.WebName;
			MemoryStream memoryStream = new MemoryStream();
			string value = string.Empty;
			StreamWriter streamWriter = new StreamWriter(memoryStream, this.Encoding);
			streamWriter.Write(string.Empty);
			int num = 0;
			foreach (MethodCallParameter methodCallParameter in base.Parameters)
			{
				streamWriter.Write(value);
				streamWriter.Write(methodCallParameter.Name);
				streamWriter.Write("=");
				streamWriter.Write(UrlHelper.UrlEncode(Convert.ToString(parameterValues[num], CultureInfo.InvariantCulture), true));
				value = "&";
				num++;
			}
			streamWriter.Flush();
			return memoryStream;
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x00021E28 File Offset: 0x00020028
		private static void WriteStreamAndFixPreamble(Stream input, Stream output, bool? writeUtf8BOM, Encoding encoding)
		{
			bool flag = writeUtf8BOM == null || !(encoding is UTF8Encoding);
			if (!flag)
			{
				bool flag2 = encoding.GetPreamble().Length == 3;
				flag = ((writeUtf8BOM.Value && flag2) || (!writeUtf8BOM.Value && !flag2));
			}
			int offset = flag ? 0 : 3;
			input.CopyWithOffset(output, offset);
		}

		// Token: 0x040003D2 RID: 978
		private const string SoapEnvelopeNamespace = "http://schemas.xmlsoap.org/soap/envelope/";

		// Token: 0x040003D3 RID: 979
		private const string Soap12EnvelopeNamespace = "http://www.w3.org/2003/05/soap-envelope";
	}
}
