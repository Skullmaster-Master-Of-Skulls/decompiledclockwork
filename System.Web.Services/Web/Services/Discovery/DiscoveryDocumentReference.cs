using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Web.Services.Configuration;
using System.Web.Services.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Discovery
{
	// Token: 0x020000A2 RID: 162
	[XmlRoot("discoveryRef", Namespace = "http://schemas.xmlsoap.org/disco/")]
	public sealed class DiscoveryDocumentReference : DiscoveryReference
	{
		// Token: 0x06000445 RID: 1093 RVA: 0x00014F21 File Offset: 0x00013F21
		public DiscoveryDocumentReference()
		{
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00014F29 File Offset: 0x00013F29
		public DiscoveryDocumentReference(string href)
		{
			this.Ref = href;
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x00014F38 File Offset: 0x00013F38
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x00014F4E File Offset: 0x00013F4E
		[XmlAttribute("ref")]
		public string Ref
		{
			get
			{
				if (this.reference != null)
				{
					return this.reference;
				}
				return "";
			}
			set
			{
				this.reference = value;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x00014F58 File Offset: 0x00013F58
		[XmlIgnore]
		public override string DefaultFilename
		{
			get
			{
				string path = DiscoveryReference.FilenameFromUrl(this.Url);
				return Path.ChangeExtension(path, ".disco");
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x00014F7C File Offset: 0x00013F7C
		[XmlIgnore]
		public DiscoveryDocument Document
		{
			get
			{
				if (base.ClientProtocol == null)
				{
					throw new InvalidOperationException(Res.GetString("WebMissingClientProtocol"));
				}
				object obj = base.ClientProtocol.Documents[this.Url];
				if (obj == null)
				{
					base.Resolve();
					obj = base.ClientProtocol.Documents[this.Url];
				}
				DiscoveryDocument discoveryDocument = obj as DiscoveryDocument;
				if (discoveryDocument == null)
				{
					throw new InvalidOperationException(Res.GetString("WebInvalidDocType", new object[]
					{
						typeof(DiscoveryDocument).FullName,
						(obj == null) ? string.Empty : obj.GetType().FullName,
						this.Url
					}));
				}
				return discoveryDocument;
			}
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0001502E File Offset: 0x0001402E
		public override void WriteDocument(object document, Stream stream)
		{
			WebServicesSection.Current.DiscoveryDocumentSerializer.Serialize(new StreamWriter(stream, new UTF8Encoding(false)), document);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001504C File Offset: 0x0001404C
		public override object ReadDocument(Stream stream)
		{
			return WebServicesSection.Current.DiscoveryDocumentSerializer.Deserialize(stream);
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x0001505E File Offset: 0x0001405E
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x00015066 File Offset: 0x00014066
		[XmlIgnore]
		public override string Url
		{
			get
			{
				return this.Ref;
			}
			set
			{
				this.Ref = value;
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00015070 File Offset: 0x00014070
		private static DiscoveryDocument GetDocumentNoParse(ref string url, DiscoveryClientProtocol client)
		{
			DiscoveryDocument discoveryDocument = (DiscoveryDocument)client.Documents[url];
			if (discoveryDocument != null)
			{
				return discoveryDocument;
			}
			string contentType = null;
			Stream stream = client.Download(ref url, ref contentType);
			DiscoveryDocument result;
			try
			{
				XmlTextReader xmlTextReader = new XmlTextReader(new StreamReader(stream, RequestResponseUtils.GetEncoding(contentType)));
				xmlTextReader.WhitespaceHandling = WhitespaceHandling.Significant;
				xmlTextReader.XmlResolver = null;
				xmlTextReader.ProhibitDtd = true;
				if (!DiscoveryDocument.CanRead(xmlTextReader))
				{
					ArgumentException innerException = new ArgumentException(Res.GetString("WebInvalidFormat"));
					throw new InvalidOperationException(Res.GetString("WebMissingDocument", new object[]
					{
						url
					}), innerException);
				}
				result = DiscoveryDocument.Read(xmlTextReader);
			}
			finally
			{
				stream.Close();
			}
			return result;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00015128 File Offset: 0x00014128
		protected internal override void Resolve(string contentType, Stream stream)
		{
			DiscoveryDocument discoveryDocument = null;
			if (ContentType.IsHtml(contentType))
			{
				string text = LinkGrep.SearchForLink(stream);
				if (text == null)
				{
					throw new InvalidContentTypeException(Res.GetString("WebInvalidContentType", new object[]
					{
						contentType
					}), contentType);
				}
				string url = DiscoveryReference.UriToString(this.Url, text);
				discoveryDocument = DiscoveryDocumentReference.GetDocumentNoParse(ref url, base.ClientProtocol);
				this.Url = url;
			}
			if (discoveryDocument == null)
			{
				XmlTextReader xmlTextReader = new XmlTextReader(new StreamReader(stream, RequestResponseUtils.GetEncoding(contentType)));
				xmlTextReader.XmlResolver = null;
				xmlTextReader.WhitespaceHandling = WhitespaceHandling.Significant;
				xmlTextReader.ProhibitDtd = true;
				if (DiscoveryDocument.CanRead(xmlTextReader))
				{
					discoveryDocument = DiscoveryDocument.Read(xmlTextReader);
				}
				else
				{
					stream.Position = 0L;
					XmlTextReader xmlTextReader2 = new XmlTextReader(new StreamReader(stream, RequestResponseUtils.GetEncoding(contentType)));
					xmlTextReader2.XmlResolver = null;
					xmlTextReader2.ProhibitDtd = true;
					while (xmlTextReader2.NodeType != XmlNodeType.Element)
					{
						if (xmlTextReader2.NodeType == XmlNodeType.ProcessingInstruction)
						{
							StringBuilder stringBuilder = new StringBuilder("<pi ");
							stringBuilder.Append(xmlTextReader2.Value);
							stringBuilder.Append("/>");
							XmlTextReader xmlTextReader3 = new XmlTextReader(new StringReader(stringBuilder.ToString()));
							xmlTextReader3.XmlResolver = null;
							xmlTextReader3.ProhibitDtd = true;
							xmlTextReader3.Read();
							string text2 = xmlTextReader3["type"];
							string text3 = xmlTextReader3["alternate"];
							string text4 = xmlTextReader3["href"];
							if (text2 != null && ContentType.MatchesBase(text2, "text/xml") && text3 != null && string.Compare(text3, "yes", StringComparison.OrdinalIgnoreCase) == 0 && text4 != null)
							{
								string url2 = DiscoveryReference.UriToString(this.Url, text4);
								discoveryDocument = DiscoveryDocumentReference.GetDocumentNoParse(ref url2, base.ClientProtocol);
								this.Url = url2;
								break;
							}
						}
						xmlTextReader2.Read();
					}
				}
			}
			if (discoveryDocument == null)
			{
				Exception innerException;
				if (ContentType.IsXml(contentType))
				{
					innerException = new ArgumentException(Res.GetString("WebInvalidFormat"));
				}
				else
				{
					innerException = new InvalidContentTypeException(Res.GetString("WebInvalidContentType", new object[]
					{
						contentType
					}), contentType);
				}
				throw new InvalidOperationException(Res.GetString("WebMissingDocument", new object[]
				{
					this.Url
				}), innerException);
			}
			base.ClientProtocol.References[this.Url] = this;
			base.ClientProtocol.Documents[this.Url] = discoveryDocument;
			foreach (object obj in discoveryDocument.References)
			{
				if (obj is DiscoveryReference)
				{
					DiscoveryReference discoveryReference = (DiscoveryReference)obj;
					if (discoveryReference.Url.Length == 0)
					{
						throw new InvalidOperationException(Res.GetString("WebEmptyRef", new object[]
						{
							discoveryReference.GetType().FullName,
							this.Url
						}));
					}
					discoveryReference.Url = DiscoveryReference.UriToString(this.Url, discoveryReference.Url);
					ContractReference contractReference = discoveryReference as ContractReference;
					if (contractReference != null && contractReference.DocRef != null)
					{
						contractReference.DocRef = DiscoveryReference.UriToString(this.Url, contractReference.DocRef);
					}
					discoveryReference.ClientProtocol = base.ClientProtocol;
					base.ClientProtocol.References[discoveryReference.Url] = discoveryReference;
				}
				else
				{
					base.ClientProtocol.AdditionalInformation.Add(obj);
				}
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x000154A8 File Offset: 0x000144A8
		public void ResolveAll()
		{
			this.ResolveAll(true);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x000154B4 File Offset: 0x000144B4
		internal void ResolveAll(bool throwOnError)
		{
			try
			{
				base.Resolve();
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (throwOnError)
				{
					throw;
				}
				if (Tracing.On)
				{
					Tracing.ExceptionCatch(TraceEventType.Warning, this, "ResolveAll", ex);
				}
				return;
			}
			catch
			{
				if (throwOnError)
				{
					throw;
				}
				return;
			}
			foreach (object obj in this.Document.References)
			{
				DiscoveryDocumentReference discoveryDocumentReference = obj as DiscoveryDocumentReference;
				if (discoveryDocumentReference != null && base.ClientProtocol.Documents[discoveryDocumentReference.Url] == null)
				{
					discoveryDocumentReference.ClientProtocol = base.ClientProtocol;
					discoveryDocumentReference.ResolveAll(throwOnError);
				}
			}
		}

		// Token: 0x040003A7 RID: 935
		private string reference;
	}
}
