using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000237 RID: 567
	public class XmlSchemaAnyAttribute : XmlSchemaAnnotated
	{
		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001B00 RID: 6912 RVA: 0x00080F5B File Offset: 0x0007FF5B
		// (set) Token: 0x06001B01 RID: 6913 RVA: 0x00080F63 File Offset: 0x0007FF63
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

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001B02 RID: 6914 RVA: 0x00080F6C File Offset: 0x0007FF6C
		// (set) Token: 0x06001B03 RID: 6915 RVA: 0x00080F74 File Offset: 0x0007FF74
		[DefaultValue(XmlSchemaContentProcessing.None)]
		[XmlAttribute("processContents")]
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

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001B04 RID: 6916 RVA: 0x00080F7D File Offset: 0x0007FF7D
		[XmlIgnore]
		internal NamespaceList NamespaceList
		{
			get
			{
				return this.namespaceList;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001B05 RID: 6917 RVA: 0x00080F85 File Offset: 0x0007FF85
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

		// Token: 0x06001B06 RID: 6918 RVA: 0x00080F97 File Offset: 0x0007FF97
		internal void BuildNamespaceList(string targetNamespace)
		{
			if (this.ns != null)
			{
				this.namespaceList = new NamespaceList(this.ns, targetNamespace);
				return;
			}
			this.namespaceList = new NamespaceList();
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x00080FBF File Offset: 0x0007FFBF
		internal void BuildNamespaceListV1Compat(string targetNamespace)
		{
			if (this.ns != null)
			{
				this.namespaceList = new NamespaceListV1Compat(this.ns, targetNamespace);
				return;
			}
			this.namespaceList = new NamespaceList();
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x00080FE7 File Offset: 0x0007FFE7
		internal bool Allows(XmlQualifiedName qname)
		{
			return this.namespaceList.Allows(qname.Namespace);
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x00080FFA File Offset: 0x0007FFFA
		internal static bool IsSubset(XmlSchemaAnyAttribute sub, XmlSchemaAnyAttribute super)
		{
			return NamespaceList.IsSubset(sub.NamespaceList, super.NamespaceList);
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x00081010 File Offset: 0x00080010
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

		// Token: 0x06001B0B RID: 6923 RVA: 0x0008105C File Offset: 0x0008005C
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

		// Token: 0x040010E7 RID: 4327
		private string ns;

		// Token: 0x040010E8 RID: 4328
		private XmlSchemaContentProcessing processContents;

		// Token: 0x040010E9 RID: 4329
		private NamespaceList namespaceList;
	}
}
