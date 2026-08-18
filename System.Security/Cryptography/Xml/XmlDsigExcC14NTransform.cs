using System;
using System.IO;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000AF RID: 175
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class XmlDsigExcC14NTransform : Transform
	{
		// Token: 0x060003EB RID: 1003 RVA: 0x00014547 File Offset: 0x00013547
		public XmlDsigExcC14NTransform() : this(false, null)
		{
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00014551 File Offset: 0x00013551
		public XmlDsigExcC14NTransform(bool includeComments) : this(includeComments, null)
		{
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0001455B File Offset: 0x0001355B
		public XmlDsigExcC14NTransform(string inclusiveNamespacesPrefixList) : this(false, inclusiveNamespacesPrefixList)
		{
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00014568 File Offset: 0x00013568
		public XmlDsigExcC14NTransform(bool includeComments, string inclusiveNamespacesPrefixList)
		{
			this._includeComments = includeComments;
			this._inclusiveNamespacesPrefixList = inclusiveNamespacesPrefixList;
			base.Algorithm = (includeComments ? "http://www.w3.org/2001/10/xml-exc-c14n#WithComments" : "http://www.w3.org/2001/10/xml-exc-c14n#");
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x000145EE File Offset: 0x000135EE
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x000145F6 File Offset: 0x000135F6
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

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x000145FF File Offset: 0x000135FF
		public override Type[] InputTypes
		{
			get
			{
				return this._inputTypes;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00014607 File Offset: 0x00013607
		public override Type[] OutputTypes
		{
			get
			{
				return this._outputTypes;
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00014610 File Offset: 0x00013610
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

		// Token: 0x060003F4 RID: 1012 RVA: 0x000146F8 File Offset: 0x000136F8
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

		// Token: 0x060003F5 RID: 1013 RVA: 0x000147B8 File Offset: 0x000137B8
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

		// Token: 0x060003F6 RID: 1014 RVA: 0x00014834 File Offset: 0x00013834
		public override object GetOutput()
		{
			return new MemoryStream(this._excCanonicalXml.GetBytes());
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00014848 File Offset: 0x00013848
		public override object GetOutput(Type type)
		{
			if (type != typeof(Stream) && !type.IsSubclassOf(typeof(Stream)))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_TransformIncorrectInputType"), "type");
			}
			return new MemoryStream(this._excCanonicalXml.GetBytes());
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00014899 File Offset: 0x00013899
		public override byte[] GetDigestedOutput(HashAlgorithm hash)
		{
			return this._excCanonicalXml.GetDigestedBytes(hash);
		}

		// Token: 0x0400056D RID: 1389
		private Type[] _inputTypes = new Type[]
		{
			typeof(Stream),
			typeof(XmlDocument),
			typeof(XmlNodeList)
		};

		// Token: 0x0400056E RID: 1390
		private Type[] _outputTypes = new Type[]
		{
			typeof(Stream)
		};

		// Token: 0x0400056F RID: 1391
		private bool _includeComments;

		// Token: 0x04000570 RID: 1392
		private string _inclusiveNamespacesPrefixList;

		// Token: 0x04000571 RID: 1393
		private ExcCanonicalXml _excCanonicalXml;
	}
}
