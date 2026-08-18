using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000AC RID: 172
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public abstract class Transform
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00013FAC File Offset: 0x00012FAC
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x00013FB4 File Offset: 0x00012FB4
		internal string BaseURI
		{
			get
			{
				return this.m_baseUri;
			}
			set
			{
				this.m_baseUri = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00013FBD File Offset: 0x00012FBD
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x00013FC5 File Offset: 0x00012FC5
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

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00013FCE File Offset: 0x00012FCE
		// (set) Token: 0x060003CB RID: 971 RVA: 0x00013FD6 File Offset: 0x00012FD6
		internal Reference Reference
		{
			get
			{
				return this.m_reference;
			}
			set
			{
				this.m_reference = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00013FE7 File Offset: 0x00012FE7
		// (set) Token: 0x060003CE RID: 974 RVA: 0x00013FEF File Offset: 0x00012FEF
		public string Algorithm
		{
			get
			{
				return this.m_algorithm;
			}
			set
			{
				this.m_algorithm = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (set) Token: 0x060003CF RID: 975 RVA: 0x00013FF8 File Offset: 0x00012FF8
		[ComVisible(false)]
		public XmlResolver Resolver
		{
			set
			{
				this.m_xmlResolver = value;
				this.m_bResolverSet = true;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00014008 File Offset: 0x00013008
		internal bool ResolverSet
		{
			get
			{
				return this.m_bResolverSet;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060003D1 RID: 977
		public abstract Type[] InputTypes { get; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060003D2 RID: 978
		public abstract Type[] OutputTypes { get; }

		// Token: 0x060003D3 RID: 979 RVA: 0x00014010 File Offset: 0x00013010
		internal bool AcceptsType(Type inputType)
		{
			if (this.InputTypes != null)
			{
				for (int i = 0; i < this.InputTypes.Length; i++)
				{
					if (inputType == this.InputTypes[i] || inputType.IsSubclassOf(this.InputTypes[i]))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00014058 File Offset: 0x00013058
		public XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00014079 File Offset: 0x00013079
		internal XmlElement GetXml(XmlDocument document)
		{
			return this.GetXml(document, "Transform");
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00014088 File Offset: 0x00013088
		internal XmlElement GetXml(XmlDocument document, string name)
		{
			XmlElement xmlElement = document.CreateElement(name, "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(this.Algorithm))
			{
				xmlElement.SetAttribute("Algorithm", this.Algorithm);
			}
			XmlNodeList innerXml = this.GetInnerXml();
			if (innerXml != null)
			{
				foreach (object obj in innerXml)
				{
					XmlNode node = (XmlNode)obj;
					xmlElement.AppendChild(document.ImportNode(node, true));
				}
			}
			return xmlElement;
		}

		// Token: 0x060003D7 RID: 983
		public abstract void LoadInnerXml(XmlNodeList nodeList);

		// Token: 0x060003D8 RID: 984
		protected abstract XmlNodeList GetInnerXml();

		// Token: 0x060003D9 RID: 985
		public abstract void LoadInput(object obj);

		// Token: 0x060003DA RID: 986
		public abstract object GetOutput();

		// Token: 0x060003DB RID: 987
		public abstract object GetOutput(Type type);

		// Token: 0x060003DC RID: 988 RVA: 0x00014120 File Offset: 0x00013120
		[ComVisible(false)]
		public virtual byte[] GetDigestedOutput(HashAlgorithm hash)
		{
			return hash.ComputeHash((Stream)this.GetOutput(typeof(Stream)));
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00014140 File Offset: 0x00013140
		// (set) Token: 0x060003DE RID: 990 RVA: 0x00014180 File Offset: 0x00013180
		[ComVisible(false)]
		public XmlElement Context
		{
			get
			{
				if (this.m_context != null)
				{
					return this.m_context;
				}
				Reference reference = this.Reference;
				SignedXml signedXml = (reference == null) ? this.SignedXml : reference.SignedXml;
				if (signedXml == null)
				{
					return null;
				}
				return signedXml.m_context;
			}
			set
			{
				this.m_context = value;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0001418C File Offset: 0x0001318C
		[ComVisible(false)]
		public Hashtable PropagatedNamespaces
		{
			get
			{
				if (this.m_propagatedNamespaces != null)
				{
					return this.m_propagatedNamespaces;
				}
				Reference reference = this.Reference;
				SignedXml signedXml = (reference == null) ? this.SignedXml : reference.SignedXml;
				if (reference != null && (reference.ReferenceTargetType != ReferenceTargetType.UriReference || reference.Uri == null || reference.Uri.Length == 0 || reference.Uri[0] != '#'))
				{
					this.m_propagatedNamespaces = new Hashtable(0);
					return this.m_propagatedNamespaces;
				}
				CanonicalXmlNodeList canonicalXmlNodeList = null;
				if (reference != null)
				{
					canonicalXmlNodeList = reference.m_namespaces;
				}
				else if (signedXml.m_context != null)
				{
					canonicalXmlNodeList = Utils.GetPropagatedAttributes(signedXml.m_context);
				}
				if (canonicalXmlNodeList == null)
				{
					this.m_propagatedNamespaces = new Hashtable(0);
					return this.m_propagatedNamespaces;
				}
				this.m_propagatedNamespaces = new Hashtable(canonicalXmlNodeList.Count);
				foreach (object obj in canonicalXmlNodeList)
				{
					XmlNode xmlNode = (XmlNode)obj;
					string key = (xmlNode.Prefix.Length > 0) ? (xmlNode.Prefix + ":" + xmlNode.LocalName) : xmlNode.LocalName;
					if (!this.m_propagatedNamespaces.Contains(key))
					{
						this.m_propagatedNamespaces.Add(key, xmlNode.Value);
					}
				}
				return this.m_propagatedNamespaces;
			}
		}

		// Token: 0x04000561 RID: 1377
		private string m_algorithm;

		// Token: 0x04000562 RID: 1378
		private string m_baseUri;

		// Token: 0x04000563 RID: 1379
		internal XmlResolver m_xmlResolver;

		// Token: 0x04000564 RID: 1380
		private bool m_bResolverSet;

		// Token: 0x04000565 RID: 1381
		private SignedXml m_signedXml;

		// Token: 0x04000566 RID: 1382
		private Reference m_reference;

		// Token: 0x04000567 RID: 1383
		private Hashtable m_propagatedNamespaces;

		// Token: 0x04000568 RID: 1384
		private XmlElement m_context;
	}
}
