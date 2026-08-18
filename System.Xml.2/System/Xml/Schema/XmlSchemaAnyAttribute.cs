using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200026F RID: 623
	public class XmlSchemaAnyAttribute : XmlSchemaAnnotated
	{
		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06002589 RID: 9609 RVA: 0x000CCE1F File Offset: 0x000CB01F
		// (set) Token: 0x0600258A RID: 9610 RVA: 0x000CCE27 File Offset: 0x000CB027
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

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x0600258B RID: 9611 RVA: 0x000CCE30 File Offset: 0x000CB030
		// (set) Token: 0x0600258C RID: 9612 RVA: 0x000CCE38 File Offset: 0x000CB038
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

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x0600258D RID: 9613 RVA: 0x000CCE41 File Offset: 0x000CB041
		[XmlIgnore]
		internal NamespaceList NamespaceList
		{
			get
			{
				return this.namespaceList;
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x0600258E RID: 9614 RVA: 0x000CCE49 File Offset: 0x000CB049
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

		// Token: 0x0600258F RID: 9615 RVA: 0x000CCE5B File Offset: 0x000CB05B
		internal void BuildNamespaceList(string targetNamespace)
		{
			if (this.ns != null)
			{
				this.namespaceList = new NamespaceList(this.ns, targetNamespace);
				return;
			}
			this.namespaceList = new NamespaceList();
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x000CCE83 File Offset: 0x000CB083
		internal void BuildNamespaceListV1Compat(string targetNamespace)
		{
			if (this.ns != null)
			{
				this.namespaceList = new NamespaceListV1Compat(this.ns, targetNamespace);
				return;
			}
			this.namespaceList = new NamespaceList();
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x000CCEAB File Offset: 0x000CB0AB
		internal bool Allows(XmlQualifiedName qname)
		{
			return this.namespaceList.Allows(qname.Namespace);
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x000CCEBE File Offset: 0x000CB0BE
		internal static bool IsSubset(XmlSchemaAnyAttribute sub, XmlSchemaAnyAttribute super)
		{
			return NamespaceList.IsSubset(sub.NamespaceList, super.NamespaceList);
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x000CCED4 File Offset: 0x000CB0D4
		internal static XmlSchemaAnyAttribute Intersection(XmlSchemaAnyAttribute o1, XmlSchemaAnyAttribute o2, bool v1Compat)
		{
			NamespaceList namespaceList = NamespaceList.Intersection(o1.NamespaceList, o2.NamespaceList, v1Compat);
			if (namespaceList != null)
			{
				return new XmlSchemaAnyAttribute
				{
					namespaceList = namespaceList,
					ProcessContents = o1.ProcessContents,
					Annotation = o1.Annotation
				};
			}
			return null;
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x000CCF20 File Offset: 0x000CB120
		internal static XmlSchemaAnyAttribute Union(XmlSchemaAnyAttribute o1, XmlSchemaAnyAttribute o2, bool v1Compat)
		{
			NamespaceList namespaceList = NamespaceList.Union(o1.NamespaceList, o2.NamespaceList, v1Compat);
			if (namespaceList != null)
			{
				return new XmlSchemaAnyAttribute
				{
					namespaceList = namespaceList,
					processContents = o1.processContents,
					Annotation = o1.Annotation
				};
			}
			return null;
		}

		// Token: 0x0400106F RID: 4207
		private string ns;

		// Token: 0x04001070 RID: 4208
		private XmlSchemaContentProcessing processContents;

		// Token: 0x04001071 RID: 4209
		private NamespaceList namespaceList;
	}
}
