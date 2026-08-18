using System;
using System.IO;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000059 RID: 89
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class XmlDsigExcC14NTransform : Transform
	{
		// Token: 0x0600034E RID: 846 RVA: 0x0000FDA8 File Offset: 0x0000DFA8
		public XmlDsigExcC14NTransform() : this(false, null)
		{
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000FDB2 File Offset: 0x0000DFB2
		public XmlDsigExcC14NTransform(bool includeComments) : this(includeComments, null)
		{
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000FDBC File Offset: 0x0000DFBC
		public XmlDsigExcC14NTransform(string inclusiveNamespacesPrefixList) : this(false, inclusiveNamespacesPrefixList)
		{
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000FDC8 File Offset: 0x0000DFC8
		public XmlDsigExcC14NTransform(bool includeComments, string inclusiveNamespacesPrefixList)
		{
			this._includeComments = includeComments;
			this._inclusiveNamespacesPrefixList = inclusiveNamespacesPrefixList;
			base.Algorithm = (includeComments ? "http://www.w3.org/2001/10/xml-exc-c14n#WithComments" : "http://www.w3.org/2001/10/xml-exc-c14n#");
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000FE4A File Offset: 0x0000E04A
		// (set) Token: 0x06000353 RID: 851 RVA: 0x0000FE52 File Offset: 0x0000E052
		public string InclusiveNamespacesPrefixList
		{
			get
			{
				return this._inclusiveNamespacesPrefixList;
			}
			set
			{
				this._inclusiveNamespacesPrefixList = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000FE5B File Offset: 0x0000E05B
		public override Type[] InputTypes
		{
			get
			{
				return this._inputTypes;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000FE63 File Offset: 0x0000E063
		public override Type[] OutputTypes
		{
			get
			{
				return this._outputTypes;
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000FE6C File Offset: 0x0000E06C
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
			if (nodeList != null)
			{
				foreach (object obj in nodeList)
				{
					XmlNode xmlNode = (XmlNode)obj;
					XmlElement xmlElement = xmlNode as XmlElement;
					if (xmlElement != null)
					{
						if (xmlElement.LocalName.Equals("InclusiveNamespaces") && xmlElement.NamespaceURI.Equals("http://www.w3.org/2001/10/xml-exc-c14n#") && Utils.HasAttribute(xmlElement, "PrefixList", "http://www.w3.org/2000/09/xmldsig#"))
						{
							if (!Utils.VerifyAttributes(xmlElement, "PrefixList"))
							{
								throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UnknownTransform"));
							}
							this.InclusiveNamespacesPrefixList = Utils.GetAttribute(xmlElement, "PrefixList", "http://www.w3.org/2000/09/xmldsig#");
							break;
						}
						else if (!Utils.GetAllowAdditionalSignatureNodes())
						{
							throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UnknownTransform"));
						}
					}
				}
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000FF54 File Offset: 0x0000E154
		public override void LoadInput(object obj)
		{
			XmlResolver resolver = base.ResolverSet ? this.m_xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), base.BaseURI);
			if (obj is Stream)
			{
				this._excCanonicalXml = new ExcCanonicalXml((Stream)obj, this._includeComments, this._inclusiveNamespacesPrefixList, resolver, base.BaseURI);
				return;
			}
			if (obj is XmlDocument)
			{
				this._excCanonicalXml = new ExcCanonicalXml((XmlDocument)obj, this._includeComments, this._inclusiveNamespacesPrefixList, resolver);
				return;
			}
			if (obj is XmlNodeList)
			{
				this._excCanonicalXml = new ExcCanonicalXml((XmlNodeList)obj, this._includeComments, this._inclusiveNamespacesPrefixList, resolver);
				return;
			}
			throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_IncorrectObjectType"), "obj");
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00010014 File Offset: 0x0000E214
		protected override XmlNodeList GetInnerXml()
		{
			if (this.InclusiveNamespacesPrefixList == null)
			{
				return null;
			}
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("Transform", "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(base.Algorithm))
			{
				xmlElement.SetAttribute("Algorithm", base.Algorithm);
			}
			XmlElement xmlElement2 = xmlDocument.CreateElement("InclusiveNamespaces", "http://www.w3.org/2001/10/xml-exc-c14n#");
			xmlElement2.SetAttribute("PrefixList", this.InclusiveNamespacesPrefixList);
			xmlElement.AppendChild(xmlElement2);
			return xmlElement.ChildNodes;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00010090 File Offset: 0x0000E290
		public override object GetOutput()
		{
			return new MemoryStream(this._excCanonicalXml.GetBytes());
		}

		// Token: 0x0600035A RID: 858 RVA: 0x000100A4 File Offset: 0x0000E2A4
		public override object GetOutput(Type type)
		{
			if (type != typeof(Stream) && !type.IsSubclassOf(typeof(Stream)))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_TransformIncorrectInputType"), "type");
			}
			return new MemoryStream(this._excCanonicalXml.GetBytes());
		}

		// Token: 0x0600035B RID: 859 RVA: 0x000100FA File Offset: 0x0000E2FA
		public override byte[] GetDigestedOutput(HashAlgorithm hash)
		{
			return this._excCanonicalXml.GetDigestedBytes(hash);
		}

		// Token: 0x04000460 RID: 1120
		private Type[] _inputTypes = new Type[]
		{
			typeof(Stream),
			typeof(XmlDocument),
			typeof(XmlNodeList)
		};

		// Token: 0x04000461 RID: 1121
		private Type[] _outputTypes = new Type[]
		{
			typeof(Stream)
		};

		// Token: 0x04000462 RID: 1122
		private bool _includeComments;

		// Token: 0x04000463 RID: 1123
		private string _inclusiveNamespacesPrefixList;

		// Token: 0x04000464 RID: 1124
		private ExcCanonicalXml _excCanonicalXml;
	}
}
