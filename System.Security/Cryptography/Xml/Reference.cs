using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000A6 RID: 166
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class Reference
	{
		// Token: 0x0600033C RID: 828 RVA: 0x00010E06 File Offset: 0x0000FE06
		public Reference()
		{
			this.m_transformChain = new TransformChain();
			this.m_refTarget = null;
			this.m_refTargetType = ReferenceTargetType.UriReference;
			this.m_cachedXml = null;
			this.m_digestMethod = "http://www.w3.org/2000/09/xmldsig#sha1";
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00010E39 File Offset: 0x0000FE39
		public Reference(Stream stream)
		{
			this.m_transformChain = new TransformChain();
			this.m_refTarget = stream;
			this.m_refTargetType = ReferenceTargetType.Stream;
			this.m_cachedXml = null;
			this.m_digestMethod = "http://www.w3.org/2000/09/xmldsig#sha1";
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00010E6C File Offset: 0x0000FE6C
		public Reference(string uri)
		{
			this.m_transformChain = new TransformChain();
			this.m_refTarget = uri;
			this.m_uri = uri;
			this.m_refTargetType = ReferenceTargetType.UriReference;
			this.m_cachedXml = null;
			this.m_digestMethod = "http://www.w3.org/2000/09/xmldsig#sha1";
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00010EA6 File Offset: 0x0000FEA6
		internal Reference(XmlElement element)
		{
			this.m_transformChain = new TransformChain();
			this.m_refTarget = element;
			this.m_refTargetType = ReferenceTargetType.XmlElement;
			this.m_cachedXml = null;
			this.m_digestMethod = "http://www.w3.org/2000/09/xmldsig#sha1";
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00010ED9 File Offset: 0x0000FED9
		// (set) Token: 0x06000341 RID: 833 RVA: 0x00010EE1 File Offset: 0x0000FEE1
		public string Id
		{
			get
			{
				return this.m_id;
			}
			set
			{
				this.m_id = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00010EEA File Offset: 0x0000FEEA
		// (set) Token: 0x06000343 RID: 835 RVA: 0x00010EF2 File Offset: 0x0000FEF2
		public string Uri
		{
			get
			{
				return this.m_uri;
			}
			set
			{
				this.m_uri = value;
				this.m_cachedXml = null;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00010F02 File Offset: 0x0000FF02
		// (set) Token: 0x06000345 RID: 837 RVA: 0x00010F0A File Offset: 0x0000FF0A
		public string Type
		{
			get
			{
				return this.m_type;
			}
			set
			{
				this.m_type = value;
				this.m_cachedXml = null;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00010F1A File Offset: 0x0000FF1A
		// (set) Token: 0x06000347 RID: 839 RVA: 0x00010F22 File Offset: 0x0000FF22
		public string DigestMethod
		{
			get
			{
				return this.m_digestMethod;
			}
			set
			{
				this.m_digestMethod = value;
				this.m_cachedXml = null;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00010F32 File Offset: 0x0000FF32
		// (set) Token: 0x06000349 RID: 841 RVA: 0x00010F3A File Offset: 0x0000FF3A
		public byte[] DigestValue
		{
			get
			{
				return this.m_digestValue;
			}
			set
			{
				this.m_digestValue = value;
				this.m_cachedXml = null;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600034A RID: 842 RVA: 0x00010F4A File Offset: 0x0000FF4A
		// (set) Token: 0x0600034B RID: 843 RVA: 0x00010F65 File Offset: 0x0000FF65
		public TransformChain TransformChain
		{
			get
			{
				if (this.m_transformChain == null)
				{
					this.m_transformChain = new TransformChain();
				}
				return this.m_transformChain;
			}
			[ComVisible(false)]
			set
			{
				this.m_transformChain = value;
				this.m_cachedXml = null;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00010F75 File Offset: 0x0000FF75
		internal bool CacheValid
		{
			get
			{
				return this.m_cachedXml != null;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00010F83 File Offset: 0x0000FF83
		// (set) Token: 0x0600034E RID: 846 RVA: 0x00010F8B File Offset: 0x0000FF8B
		internal SignedXml SignedXml
		{
			get
			{
				return this.m_signedXml;
			}
			set
			{
				this.m_signedXml = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00010F94 File Offset: 0x0000FF94
		internal ReferenceTargetType ReferenceTargetType
		{
			get
			{
				return this.m_refTargetType;
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00010F9C File Offset: 0x0000FF9C
		public XmlElement GetXml()
		{
			if (this.CacheValid)
			{
				return this.m_cachedXml;
			}
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00010FCC File Offset: 0x0000FFCC
		internal XmlElement GetXml(XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement("Reference", "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(this.m_id))
			{
				xmlElement.SetAttribute("Id", this.m_id);
			}
			if (this.m_uri != null)
			{
				xmlElement.SetAttribute("URI", this.m_uri);
			}
			if (!string.IsNullOrEmpty(this.m_type))
			{
				xmlElement.SetAttribute("Type", this.m_type);
			}
			if (this.TransformChain.Count != 0)
			{
				xmlElement.AppendChild(this.TransformChain.GetXml(document, "http://www.w3.org/2000/09/xmldsig#"));
			}
			if (string.IsNullOrEmpty(this.m_digestMethod))
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_DigestMethodRequired"));
			}
			XmlElement xmlElement2 = document.CreateElement("DigestMethod", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement2.SetAttribute("Algorithm", this.m_digestMethod);
			xmlElement.AppendChild(xmlElement2);
			if (this.DigestValue == null)
			{
				if (this.m_hashAlgorithm.Hash == null)
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_DigestValueRequired"));
				}
				this.DigestValue = this.m_hashAlgorithm.Hash;
			}
			XmlElement xmlElement3 = document.CreateElement("DigestValue", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement3.AppendChild(document.CreateTextNode(Convert.ToBase64String(this.m_digestValue)));
			xmlElement.AppendChild(xmlElement3);
			return xmlElement;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00011114 File Offset: 0x00010114
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.m_id = Utils.GetAttribute(value, "Id", "http://www.w3.org/2000/09/xmldsig#");
			this.m_uri = Utils.GetAttribute(value, "URI", "http://www.w3.org/2000/09/xmldsig#");
			this.m_type = Utils.GetAttribute(value, "Type", "http://www.w3.org/2000/09/xmldsig#");
			if (!Utils.VerifyAttributes(value, new string[]
			{
				"Id",
				"URI",
				"Type"
			}))
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "Reference");
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			bool flag = false;
			this.TransformChain = new TransformChain();
			XmlNodeList xmlNodeList = value.SelectNodes("ds:Transforms", xmlNamespaceManager);
			if (xmlNodeList != null && xmlNodeList.Count != 0)
			{
				if (!Utils.GetAllowAdditionalSignatureNodes() && xmlNodeList.Count > 1)
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "Reference/Transforms");
				}
				flag = true;
				XmlElement xmlElement = xmlNodeList[0] as XmlElement;
				if (!Utils.VerifyAttributes(xmlElement, null))
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "Reference/Transforms");
				}
				XmlNodeList xmlNodeList2 = xmlElement.SelectNodes("ds:Transform", xmlNamespaceManager);
				if (xmlNodeList2 != null)
				{
					if (!Utils.GetAllowAdditionalSignatureNodes() && xmlNodeList2.Count != xmlElement.SelectNodes("*").Count)
					{
						throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "Reference/Transforms");
					}
					if ((long)xmlNodeList2.Count > Utils.GetMaxTransformsPerReference())
					{
						throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "Reference/Transforms");
					}
					foreach (object obj in xmlNodeList2)
					{
						XmlNode xmlNode = (XmlNode)obj;
						XmlElement xmlElement2 = xmlNode as XmlElement;
						string attribute = Utils.GetAttribute(xmlElement2, "Algorithm", "http://www.w3.org/2000/09/xmldsig#");
						if ((attribute == null && !Utils.GetSkipSignatureAttributeEnforcement()) || !Utils.VerifyAttributes(xmlElement2, "Algorithm"))
						{
							throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UnknownTransform"));
						}
						Transform transform = Utils.CreateFromName<Transform>(attribute);
						if (transform == null)
						{
							throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UnknownTransform"));
						}
						this.AddTransform(transform);
						transform.LoadInnerXml(xmlElement2.ChildNodes);
						if (transform is XmlDsigEnvelopedSignatureTransform)
						{
							XmlNode xmlNode2 = xmlElement2.SelectSingleNode("ancestor::ds:Signature[1]", xmlNamespaceManager);
							XmlNodeList xmlNodeList3 = xmlElement2.SelectNodes("//ds:Signature", xmlNamespaceManager);
							if (xmlNodeList3 != null)
							{
								int num = 0;
								foreach (object obj2 in xmlNodeList3)
								{
									XmlNode xmlNode3 = (XmlNode)obj2;
									num++;
									if (xmlNode3 == xmlNode2)
									{
										((XmlDsigEnvelopedSignatureTransform)transform).SignaturePosition = num;
										break;
									}
								}
							}
						}
					}
				}
			}
			XmlNodeList xmlNodeList4 = value.SelectNodes("ds:DigestMethod", xmlNamespaceManager);
			if (xmlNodeList4 == null || xmlNodeList4.Count == 0 || (!Utils.GetAllowAdditionalSignatureNodes() && xmlNodeList4.Count > 1))
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "Reference/DigestMethod");
			}
			XmlElement element = xmlNodeList4[0] as XmlElement;
			this.m_digestMethod = Utils.GetAttribute(element, "Algorithm", "http://www.w3.org/2000/09/xmldsig#");
			if ((this.m_digestMethod == null && !Utils.GetSkipSignatureAttributeEnforcement()) || !Utils.VerifyAttributes(element, "Algorithm"))
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "Reference/DigestMethod");
			}
			XmlNodeList xmlNodeList5 = value.SelectNodes("ds:DigestValue", xmlNamespaceManager);
			if (xmlNodeList5 == null || xmlNodeList5.Count == 0 || (!Utils.GetAllowAdditionalSignatureNodes() && xmlNodeList5.Count > 1))
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "Reference/DigestValue");
			}
			XmlElement xmlElement3 = xmlNodeList5[0] as XmlElement;
			this.m_digestValue = Convert.FromBase64String(Utils.DiscardWhiteSpaces(xmlElement3.InnerText));
			if (!Utils.VerifyAttributes(xmlElement3, null))
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "Reference/DigestValue");
			}
			int num2 = flag ? 3 : 2;
			if (!Utils.GetAllowAdditionalSignatureNodes() && value.SelectNodes("*").Count != num2)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "Reference");
			}
			this.m_cachedXml = value;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0001158C File Offset: 0x0001058C
		public void AddTransform(Transform transform)
		{
			if (transform == null)
			{
				throw new ArgumentNullException("transform");
			}
			transform.Reference = this;
			this.TransformChain.Add(transform);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x000115AF File Offset: 0x000105AF
		internal void UpdateHashValue(XmlDocument document, CanonicalXmlNodeList refList)
		{
			this.DigestValue = this.CalculateHashValue(document, refList);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x000115C0 File Offset: 0x000105C0
		internal byte[] CalculateHashValue(XmlDocument document, CanonicalXmlNodeList refList)
		{
			this.m_hashAlgorithm = Utils.CreateFromName<HashAlgorithm>(this.m_digestMethod);
			if (this.m_hashAlgorithm == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_CreateHashAlgorithmFailed"));
			}
			string text = (document == null) ? (Environment.CurrentDirectory + "\\") : document.BaseURI;
			Stream stream = null;
			WebResponse webResponse = null;
			Stream stream2 = null;
			XmlResolver xmlResolver = null;
			byte[] result = null;
			try
			{
				switch (this.m_refTargetType)
				{
				case ReferenceTargetType.Stream:
					xmlResolver = (this.SignedXml.ResolverSet ? this.SignedXml.m_xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text));
					stream = this.TransformChain.TransformToOctetStream((Stream)this.m_refTarget, xmlResolver, text);
					goto IL_4A5;
				case ReferenceTargetType.XmlElement:
					xmlResolver = (this.SignedXml.ResolverSet ? this.SignedXml.m_xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text));
					stream = this.TransformChain.TransformToOctetStream(Utils.PreProcessElementInput((XmlElement)this.m_refTarget, xmlResolver, text), xmlResolver, text);
					goto IL_4A5;
				case ReferenceTargetType.UriReference:
					if (this.m_uri == null)
					{
						xmlResolver = (this.SignedXml.ResolverSet ? this.SignedXml.m_xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text));
						stream = this.TransformChain.TransformToOctetStream(null, xmlResolver, text);
						goto IL_4A5;
					}
					if (this.m_uri.Length == 0)
					{
						if (document == null)
						{
							throw new CryptographicException(string.Format(CultureInfo.CurrentCulture, SecurityResources.GetResourceString("Cryptography_Xml_SelfReferenceRequiresContext"), new object[]
							{
								this.m_uri
							}));
						}
						xmlResolver = (this.SignedXml.ResolverSet ? this.SignedXml.m_xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text));
						XmlDocument document2 = Utils.DiscardComments(Utils.PreProcessDocumentInput(document, xmlResolver, text));
						stream = this.TransformChain.TransformToOctetStream(document2, xmlResolver, text);
						goto IL_4A5;
					}
					else if (this.m_uri[0] == '#')
					{
						bool flag = true;
						string idFromLocalUri = Utils.GetIdFromLocalUri(this.m_uri, out flag);
						if (idFromLocalUri == "xpointer(/)")
						{
							if (document == null)
							{
								throw new CryptographicException(string.Format(CultureInfo.CurrentCulture, SecurityResources.GetResourceString("Cryptography_Xml_SelfReferenceRequiresContext"), new object[]
								{
									this.m_uri
								}));
							}
							xmlResolver = (this.SignedXml.ResolverSet ? this.SignedXml.m_xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text));
							stream = this.TransformChain.TransformToOctetStream(Utils.PreProcessDocumentInput(document, xmlResolver, text), xmlResolver, text);
							goto IL_4A5;
						}
						else
						{
							XmlElement xmlElement = this.SignedXml.GetIdElement(document, idFromLocalUri);
							if (xmlElement != null)
							{
								this.m_namespaces = Utils.GetPropagatedAttributes(xmlElement.ParentNode as XmlElement);
							}
							if (xmlElement == null && refList != null)
							{
								foreach (object obj in refList)
								{
									XmlNode xmlNode = (XmlNode)obj;
									XmlElement xmlElement2 = xmlNode as XmlElement;
									if (xmlElement2 != null && Utils.HasAttribute(xmlElement2, "Id", "http://www.w3.org/2000/09/xmldsig#") && Utils.GetAttribute(xmlElement2, "Id", "http://www.w3.org/2000/09/xmldsig#").Equals(idFromLocalUri))
									{
										xmlElement = xmlElement2;
										if (this.m_signedXml.m_context != null)
										{
											this.m_namespaces = Utils.GetPropagatedAttributes(this.m_signedXml.m_context);
											break;
										}
										break;
									}
								}
							}
							if (xmlElement == null)
							{
								throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidReference"));
							}
							XmlDocument xmlDocument = Utils.PreProcessElementInput(xmlElement, xmlResolver, text);
							Utils.AddNamespaces(xmlDocument.DocumentElement, this.m_namespaces);
							xmlResolver = (this.SignedXml.ResolverSet ? this.SignedXml.m_xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text));
							if (flag)
							{
								XmlDocument document3 = Utils.DiscardComments(xmlDocument);
								stream = this.TransformChain.TransformToOctetStream(document3, xmlResolver, text);
								goto IL_4A5;
							}
							stream = this.TransformChain.TransformToOctetStream(xmlDocument, xmlResolver, text);
							goto IL_4A5;
						}
					}
					else
					{
						if (!Utils.AllowDetachedSignature())
						{
							throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UriNotResolved"), this.m_uri);
						}
						Uri uri = new Uri(this.m_uri, UriKind.RelativeOrAbsolute);
						if (!uri.IsAbsoluteUri)
						{
							uri = new Uri(new Uri(text), uri);
						}
						WebRequest webRequest = WebRequest.Create(uri);
						if (webRequest != null)
						{
							webResponse = webRequest.GetResponse();
							if (webResponse != null)
							{
								stream2 = webResponse.GetResponseStream();
								if (stream2 != null)
								{
									xmlResolver = (this.SignedXml.ResolverSet ? this.SignedXml.m_xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text));
									stream = this.TransformChain.TransformToOctetStream(stream2, xmlResolver, this.m_uri);
									goto IL_4A5;
								}
							}
						}
					}
					break;
				}
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UriNotResolved"), this.m_uri);
				IL_4A5:
				result = this.m_hashAlgorithm.ComputeHash(stream);
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
				if (webResponse != null)
				{
					webResponse.Close();
				}
				if (stream2 != null)
				{
					stream2.Close();
				}
			}
			return result;
		}

		// Token: 0x04000513 RID: 1299
		private string m_id;

		// Token: 0x04000514 RID: 1300
		private string m_uri;

		// Token: 0x04000515 RID: 1301
		private string m_type;

		// Token: 0x04000516 RID: 1302
		private TransformChain m_transformChain;

		// Token: 0x04000517 RID: 1303
		private string m_digestMethod;

		// Token: 0x04000518 RID: 1304
		private byte[] m_digestValue;

		// Token: 0x04000519 RID: 1305
		private HashAlgorithm m_hashAlgorithm;

		// Token: 0x0400051A RID: 1306
		private object m_refTarget;

		// Token: 0x0400051B RID: 1307
		private ReferenceTargetType m_refTargetType;

		// Token: 0x0400051C RID: 1308
		private XmlElement m_cachedXml;

		// Token: 0x0400051D RID: 1309
		private SignedXml m_signedXml;

		// Token: 0x0400051E RID: 1310
		internal CanonicalXmlNodeList m_namespaces;
	}
}
