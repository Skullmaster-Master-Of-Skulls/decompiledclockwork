using System;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200026E RID: 622
	public class XmlSchemaAny : XmlSchemaParticle
	{
		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x0600257D RID: 9597 RVA: 0x000CCC7F File Offset: 0x000CAE7F
		// (set) Token: 0x0600257E RID: 9598 RVA: 0x000CCC87 File Offset: 0x000CAE87
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

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x0600257F RID: 9599 RVA: 0x000CCC90 File Offset: 0x000CAE90
		// (set) Token: 0x06002580 RID: 9600 RVA: 0x000CCC98 File Offset: 0x000CAE98
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

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06002581 RID: 9601 RVA: 0x000CCCA1 File Offset: 0x000CAEA1
		[XmlIgnore]
		internal NamespaceList NamespaceList
		{
			get
			{
				return this.namespaceList;
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06002582 RID: 9602 RVA: 0x000CCCA9 File Offset: 0x000CAEA9
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

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06002583 RID: 9603 RVA: 0x000CCCCC File Offset: 0x000CAECC
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

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06002584 RID: 9604 RVA: 0x000CCCE0 File Offset: 0x000CAEE0
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

		// Token: 0x06002585 RID: 9605 RVA: 0x000CCDB4 File Offset: 0x000CAFB4
		internal void BuildNamespaceList(string targetNamespace)
		{
			if (this.ns != null)
			{
				this.namespaceList = new NamespaceList(this.ns, targetNamespace);
				return;
			}
			this.namespaceList = new NamespaceList();
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x000CCDDC File Offset: 0x000CAFDC
		internal void BuildNamespaceListV1Compat(string targetNamespace)
		{
			if (this.ns != null)
			{
				this.namespaceList = new NamespaceListV1Compat(this.ns, targetNamespace);
				return;
			}
			this.namespaceList = new NamespaceList();
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x000CCE04 File Offset: 0x000CB004
		internal bool Allows(XmlQualifiedName qname)
		{
			return this.namespaceList.Allows(qname.Namespace);
		}

		// Token: 0x0400106C RID: 4204
		private string ns;

		// Token: 0x0400106D RID: 4205
		private XmlSchemaContentProcessing processContents;

		// Token: 0x0400106E RID: 4206
		private NamespaceList namespaceList;
	}
}
