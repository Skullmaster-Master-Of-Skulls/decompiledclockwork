using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200004F RID: 79
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class Reference
	{
		// Token: 0x0600027B RID: 635 RVA: 0x0000B10D File Offset: 0x0000930D
		public Reference()
		{
			this.m_transformChain = new TransformChain();
			this.m_refTarget = null;
			this.m_refTargetType = ReferenceTargetType.UriReference;
			this.m_cachedXml = null;
			this.m_digestMethod = SignedXml.XmlDsigDigestDefault;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000B140 File Offset: 0x00009340
		public Reference(Stream stream)
		{
			this.m_transformChain = new TransformChain();
			this.m_refTarget = stream;
			this.m_refTargetType = ReferenceTargetType.Stream;
			this.m_cachedXml = null;
			this.m_digestMethod = SignedXml.XmlDsigDigestDefault;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000B173 File Offset: 0x00009373
		public Reference(string uri)
		{
			this.m_transformChain = new TransformChain();
			this.m_refTarget = uri;
			this.m_uri = uri;
			this.m_refTargetType = ReferenceTargetType.UriReference;
			this.m_cachedXml = null;
			this.m_digestMethod = SignedXml.XmlDsigDigestDefault;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000B1AD File Offset: 0x000093AD
		internal Reference(XmlElement element)
		{
			this.m_transformChain = new TransformChain();
			this.m_refTarget = element;
			this.m_refTargetType = ReferenceTargetType.XmlElement;
			this.m_cachedXml = null;
			this.m_digestMethod = SignedXml.XmlDsigDigestDefault;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000B1E0 File Offset: 0x000093E0
		// (set) Token: 0x06000280 RID: 640 RVA: 0x0000B1E8 File Offset: 0x000093E8
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

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000B1F1 File Offset: 0x000093F1
		// (set) Token: 0x06000282 RID: 642 RVA: 0x0000B1F9 File Offset: 0x000093F9
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

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000B209 File Offset: 0x00009409
		// (set) Token: 0x06000284 RID: 644 RVA: 0x0000B211 File Offset: 0x00009411
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

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000B221 File Offset: 0x00009421
		// (set) Token: 0x06000286 RID: 646 RVA: 0x0000B229 File Offset: 0x00009429
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

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000B239 File Offset: 0x00009439
		// (set) Token: 0x06000288 RID: 648 RVA: 0x0000B241 File Offset: 0x00009441
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

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000B251 File Offset: 0x00009451
		// (set) Token: 0x0600028A RID: 650 RVA: 0x0000B26C File Offset: 0x0000946C
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

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000B27C File Offset: 0x0000947C
		internal bool CacheValid
		{
			get
			{
				return this.m_cachedXml != null;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000B287 File Offset: 0x00009487
		// (set) Token: 0x0600028D RID: 653 RVA: 0x0000B28F File Offset: 0x0000948F
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

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000B298 File Offset: 0x00009498
		internal ReferenceTargetType ReferenceTargetType
		{
			get
			{
				return this.m_refTargetType;
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000B2A0 File Offset: 0x000094A0
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

		// Token: 0x06000290 RID: 656 RVA: 0x0000B2D0 File Offset: 0x000094D0
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

		// Token: 0x06000291 RID: 657 RVA: 0x0000B418 File Offset: 0x00009618
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

		// Token: 0x06000292 RID: 658 RVA: 0x0000B888 File Offset: 0x00009A88
		public void AddTransform(Transform transform)
		{
			if (transform == null)
			{
				throw new ArgumentNullException("transform");
			}
			transform.Reference = this;
			this.TransformChain.Add(transform);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000B8AB File Offset: 0x00009AAB
		internal void UpdateHashValue(XmlDocument document, CanonicalXmlNodeList refList)
		{
			this.DigestValue = this.CalculateHashValue(document, refList);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000B8BC File Offset: 0x00009ABC
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
					goto IL_49B;
				case ReferenceTargetType.XmlElement:
					xmlResolver = (this.SignedXml.ResolverSet ? this.SignedXml.m_xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text));
					stream = this.TransformChain.TransformToOctetStream(Utils.PreProcessElementInput((XmlElement)this.m_refTarget, xmlResolver, text), xmlResolver, text);
					goto IL_49B;
				case ReferenceTargetType.UriReference:
					if (this.m_uri == null)
					{
						xmlResolver = (this.SignedXml.ResolverSet ? this.SignedXml.m_xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text));
						stream = this.TransformChain.TransformToOctetStream(null, xmlResolver, text);
						goto IL_49B;
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
						goto IL_49B;
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
							goto IL_49B;
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
								goto IL_49B;
							}
							stream = this.TransformChain.TransformToOctetStream(xmlDocument, xmlResolver, text);
							goto IL_49B;
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
									goto IL_49B;
								}
							}
						}
					}
					break;
				}
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UriNotResolved"), this.m_uri);
				IL_49B:
				stream = SignedXmlDebugLog.LogReferenceData(this, stream);
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

		// Token: 0x040003FD RID: 1021
		private string m_id;

		// Token: 0x040003FE RID: 1022
		private string m_uri;

		// Token: 0x040003FF RID: 1023
		private string m_type;

		// Token: 0x04000400 RID: 1024
		private TransformChain m_transformChain;

		// Token: 0x04000401 RID: 1025
		private string m_digestMethod;

		// Token: 0x04000402 RID: 1026
		private byte[] m_digestValue;

		// Token: 0x04000403 RID: 1027
		private HashAlgorithm m_hashAlgorithm;

		// Token: 0x04000404 RID: 1028
		private object m_refTarget;

		// Token: 0x04000405 RID: 1029
		private ReferenceTargetType m_refTargetType;

		// Token: 0x04000406 RID: 1030
		private XmlElement m_cachedXml;

		// Token: 0x04000407 RID: 1031
		private SignedXml m_signedXml;

		// Token: 0x04000408 RID: 1032
		internal CanonicalXmlNodeList m_namespaces;
	}
}
