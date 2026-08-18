using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000056 RID: 86
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public abstract class Transform
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000F808 File Offset: 0x0000DA08
		// (set) Token: 0x06000329 RID: 809 RVA: 0x0000F810 File Offset: 0x0000DA10
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

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000F819 File Offset: 0x0000DA19
		// (set) Token: 0x0600032B RID: 811 RVA: 0x0000F821 File Offset: 0x0000DA21
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

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000F82A File Offset: 0x0000DA2A
		// (set) Token: 0x0600032D RID: 813 RVA: 0x0000F832 File Offset: 0x0000DA32
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

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000F83B File Offset: 0x0000DA3B
		// (set) Token: 0x06000330 RID: 816 RVA: 0x0000F843 File Offset: 0x0000DA43
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

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000F85C File Offset: 0x0000DA5C
		// (set) Token: 0x06000331 RID: 817 RVA: 0x0000F84C File Offset: 0x0000DA4C
		[ComVisible(false)]
		public XmlResolver Resolver
		{
			internal get
			{
				return this.m_xmlResolver;
			}
			set
			{
				this.m_xmlResolver = value;
				this.m_bResolverSet = true;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000F864 File Offset: 0x0000DA64
		internal bool ResolverSet
		{
			get
			{
				return this.m_bResolverSet;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000334 RID: 820
		public abstract Type[] InputTypes { get; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000335 RID: 821
		public abstract Type[] OutputTypes { get; }

		// Token: 0x06000336 RID: 822 RVA: 0x0000F86C File Offset: 0x0000DA6C
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

		// Token: 0x06000337 RID: 823 RVA: 0x0000F8B8 File Offset: 0x0000DAB8
		public XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000F8D9 File Offset: 0x0000DAD9
		internal XmlElement GetXml(XmlDocument document)
		{
			return this.GetXml(document, "Transform");
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000F8E8 File Offset: 0x0000DAE8
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

		// Token: 0x0600033A RID: 826
		public abstract void LoadInnerXml(XmlNodeList nodeList);

		// Token: 0x0600033B RID: 827
		protected abstract XmlNodeList GetInnerXml();

		// Token: 0x0600033C RID: 828
		public abstract void LoadInput(object obj);

		// Token: 0x0600033D RID: 829
		public abstract object GetOutput();

		// Token: 0x0600033E RID: 830
		public abstract object GetOutput(Type type);

		// Token: 0x0600033F RID: 831 RVA: 0x0000F980 File Offset: 0x0000DB80
		[ComVisible(false)]
		public virtual byte[] GetDigestedOutput(HashAlgorithm hash)
		{
			return hash.ComputeHash((Stream)this.GetOutput(typeof(Stream)));
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000F9A0 File Offset: 0x0000DBA0
		// (set) Token: 0x06000341 RID: 833 RVA: 0x0000F9E0 File Offset: 0x0000DBE0
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

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000F9EC File Offset: 0x0000DBEC
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

		// Token: 0x04000454 RID: 1108
		private string m_algorithm;

		// Token: 0x04000455 RID: 1109
		private string m_baseUri;

		// Token: 0x04000456 RID: 1110
		internal XmlResolver m_xmlResolver;

		// Token: 0x04000457 RID: 1111
		private bool m_bResolverSet;

		// Token: 0x04000458 RID: 1112
		private SignedXml m_signedXml;

		// Token: 0x04000459 RID: 1113
		private Reference m_reference;

		// Token: 0x0400045A RID: 1114
		private Hashtable m_propagatedNamespaces;

		// Token: 0x0400045B RID: 1115
		private XmlElement m_context;
	}
}
