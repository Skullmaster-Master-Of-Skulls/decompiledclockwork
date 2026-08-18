using System;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000236 RID: 566
	public class XmlSchemaAny : XmlSchemaParticle
	{
		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001AF4 RID: 6900 RVA: 0x00080DB9 File Offset: 0x0007FDB9
		// (set) Token: 0x06001AF5 RID: 6901 RVA: 0x00080DC1 File Offset: 0x0007FDC1
		[XmlAttribute("namespace")]
		public string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.ns = value;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001AF6 RID: 6902 RVA: 0x00080DCA File Offset: 0x0007FDCA
		// (set) Token: 0x06001AF7 RID: 6903 RVA: 0x00080DD2 File Offset: 0x0007FDD2
		[XmlAttribute("processContents")]
		[DefaultValue(XmlSchemaContentProcessing.None)]
		public XmlSchemaContentProcessing ProcessContents
		{
			get
			{
				return this.processContents;
			}
			set
			{
				this.processContents = value;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001AF8 RID: 6904 RVA: 0x00080DDB File Offset: 0x0007FDDB
		[XmlIgnore]
		internal NamespaceList NamespaceList
		{
			get
			{
				return this.namespaceList;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x00080DE3 File Offset: 0x0007FDE3
		[XmlIgnore]
		internal string ResolvedNamespace
		{
			get
			{
				if (this.ns == null || this.ns.Length == 0)
				{
					return "##any";
				}
				return this.ns;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001AFA RID: 6906 RVA: 0x00080E06 File Offset: 0x0007FE06
		[XmlIgnore]
		internal XmlSchemaContentProcessing ProcessContentsCorrect
		{
			get
			{
				if (this.processContents != XmlSchemaContentProcessing.None)
				{
					return this.processContents;
				}
				return XmlSchemaContentProcessing.Strict;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001AFB RID: 6907 RVA: 0x00080E18 File Offset: 0x0007FE18
		internal override string NameString
		{
			get
			{
				switch (this.namespaceList.Type)
				{
				case NamespaceList.ListType.Any:
					return "##any:*";
				case NamespaceList.ListType.Other:
					return "##other:*";
				case NamespaceList.ListType.Set:
				{
					StringBuilder stringBuilder = new StringBuilder();
					int num = 1;
					foreach (object obj in this.namespaceList.Enumerate)
					{
						string str = (string)obj;
						stringBuilder.Append(str + ":*");
						if (num < this.namespaceList.Enumerate.Count)
						{
							stringBuilder.Append(" ");
						}
						num++;
					}
					return stringBuilder.ToString();
				}
				default:
					return string.Empty;
				}
			}
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x00080EF0 File Offset: 0x0007FEF0
		internal void BuildNamespaceList(string targetNamespace)
		{
			if (this.ns != null)
			{
				this.namespaceList = new NamespaceList(this.ns, targetNamespace);
				return;
			}
			this.namespaceList = new NamespaceList();
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x00080F18 File Offset: 0x0007FF18
		internal void BuildNamespaceListV1Compat(string targetNamespace)
		{
			if (this.ns != null)
			{
				this.namespaceList = new NamespaceListV1Compat(this.ns, targetNamespace);
				return;
			}
			this.namespaceList = new NamespaceList();
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x00080F40 File Offset: 0x0007FF40
		internal bool Allows(XmlQualifiedName qname)
		{
			return this.namespaceList.Allows(qname.Namespace);
		}

		// Token: 0x040010E4 RID: 4324
		private string ns;

		// Token: 0x040010E5 RID: 4325
		private XmlSchemaContentProcessing processContents;

		// Token: 0x040010E6 RID: 4326
		private NamespaceList namespaceList;
	}
}
