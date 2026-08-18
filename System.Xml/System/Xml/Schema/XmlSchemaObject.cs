using System;
using System.Security.Permissions;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200022D RID: 557
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class XmlSchemaObject
	{
		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001A6E RID: 6766 RVA: 0x0007FDAF File Offset: 0x0007EDAF
		// (set) Token: 0x06001A6F RID: 6767 RVA: 0x0007FDB7 File Offset: 0x0007EDB7
		[XmlIgnore]
		public int LineNumber
		{
			get
			{
				return this.lineNum;
			}
			set
			{
				this.lineNum = value;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001A70 RID: 6768 RVA: 0x0007FDC0 File Offset: 0x0007EDC0
		// (set) Token: 0x06001A71 RID: 6769 RVA: 0x0007FDC8 File Offset: 0x0007EDC8
		[XmlIgnore]
		public int LinePosition
		{
			get
			{
				return this.linePos;
			}
			set
			{
				this.linePos = value;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001A72 RID: 6770 RVA: 0x0007FDD1 File Offset: 0x0007EDD1
		// (set) Token: 0x06001A73 RID: 6771 RVA: 0x0007FDD9 File Offset: 0x0007EDD9
		[XmlIgnore]
		public string SourceUri
		{
			get
			{
				return this.sourceUri;
			}
			set
			{
				this.sourceUri = value;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001A74 RID: 6772 RVA: 0x0007FDE2 File Offset: 0x0007EDE2
		// (set) Token: 0x06001A75 RID: 6773 RVA: 0x0007FDEA File Offset: 0x0007EDEA
		[XmlIgnore]
		public XmlSchemaObject Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001A76 RID: 6774 RVA: 0x0007FDF3 File Offset: 0x0007EDF3
		// (set) Token: 0x06001A77 RID: 6775 RVA: 0x0007FE0E File Offset: 0x0007EE0E
		[XmlNamespaceDeclarations]
		public XmlSerializerNamespaces Namespaces
		{
			get
			{
				if (this.namespaces == null)
				{
					this.namespaces = new XmlSerializerNamespaces();
				}
				return this.namespaces;
			}
			set
			{
				this.namespaces = value;
			}
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x0007FE17 File Offset: 0x0007EE17
		internal virtual void OnAdd(XmlSchemaObjectCollection container, object item)
		{
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x0007FE19 File Offset: 0x0007EE19
		internal virtual void OnRemove(XmlSchemaObjectCollection container, object item)
		{
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x0007FE1B File Offset: 0x0007EE1B
		internal virtual void OnClear(XmlSchemaObjectCollection container)
		{
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001A7B RID: 6779 RVA: 0x0007FE1D File Offset: 0x0007EE1D
		// (set) Token: 0x06001A7C RID: 6780 RVA: 0x0007FE20 File Offset: 0x0007EE20
		[XmlIgnore]
		internal virtual string IdAttribute
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x0007FE22 File Offset: 0x0007EE22
		internal virtual void SetUnhandledAttributes(XmlAttribute[] moreAttributes)
		{
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x0007FE24 File Offset: 0x0007EE24
		internal virtual void AddAnnotation(XmlSchemaAnnotation annotation)
		{
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001A7F RID: 6783 RVA: 0x0007FE26 File Offset: 0x0007EE26
		// (set) Token: 0x06001A80 RID: 6784 RVA: 0x0007FE29 File Offset: 0x0007EE29
		[XmlIgnore]
		internal virtual string NameAttribute
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001A81 RID: 6785 RVA: 0x0007FE2B File Offset: 0x0007EE2B
		// (set) Token: 0x06001A82 RID: 6786 RVA: 0x0007FE33 File Offset: 0x0007EE33
		[XmlIgnore]
		internal bool IsProcessing
		{
			get
			{
				return this.isProcessing;
			}
			set
			{
				this.isProcessing = value;
			}
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x0007FE3C File Offset: 0x0007EE3C
		internal virtual XmlSchemaObject Clone()
		{
			return (XmlSchemaObject)base.MemberwiseClone();
		}

		// Token: 0x040010AE RID: 4270
		private int lineNum;

		// Token: 0x040010AF RID: 4271
		private int linePos;

		// Token: 0x040010B0 RID: 4272
		private string sourceUri;

		// Token: 0x040010B1 RID: 4273
		private XmlSerializerNamespaces namespaces;

		// Token: 0x040010B2 RID: 4274
		private XmlSchemaObject parent;

		// Token: 0x040010B3 RID: 4275
		private bool isProcessing;
	}
}
