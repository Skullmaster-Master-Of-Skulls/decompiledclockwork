using System;
using System.Security.Permissions;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x020002A2 RID: 674
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class XmlSchemaObject
	{
		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x0600273B RID: 10043 RVA: 0x000CF6A8 File Offset: 0x000CD8A8
		// (set) Token: 0x0600273C RID: 10044 RVA: 0x000CF6B0 File Offset: 0x000CD8B0
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

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x0600273D RID: 10045 RVA: 0x000CF6B9 File Offset: 0x000CD8B9
		// (set) Token: 0x0600273E RID: 10046 RVA: 0x000CF6C1 File Offset: 0x000CD8C1
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

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x0600273F RID: 10047 RVA: 0x000CF6CA File Offset: 0x000CD8CA
		// (set) Token: 0x06002740 RID: 10048 RVA: 0x000CF6D2 File Offset: 0x000CD8D2
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

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06002741 RID: 10049 RVA: 0x000CF6DB File Offset: 0x000CD8DB
		// (set) Token: 0x06002742 RID: 10050 RVA: 0x000CF6E3 File Offset: 0x000CD8E3
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

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06002743 RID: 10051 RVA: 0x000CF6EC File Offset: 0x000CD8EC
		// (set) Token: 0x06002744 RID: 10052 RVA: 0x000CF707 File Offset: 0x000CD907
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

		// Token: 0x06002745 RID: 10053 RVA: 0x000CF710 File Offset: 0x000CD910
		internal virtual void OnAdd(XmlSchemaObjectCollection container, object item)
		{
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x000CF712 File Offset: 0x000CD912
		internal virtual void OnRemove(XmlSchemaObjectCollection container, object item)
		{
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x000CF714 File Offset: 0x000CD914
		internal virtual void OnClear(XmlSchemaObjectCollection container)
		{
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06002748 RID: 10056 RVA: 0x000CF716 File Offset: 0x000CD916
		// (set) Token: 0x06002749 RID: 10057 RVA: 0x000CF719 File Offset: 0x000CD919
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

		// Token: 0x0600274A RID: 10058 RVA: 0x000CF71B File Offset: 0x000CD91B
		internal virtual void SetUnhandledAttributes(XmlAttribute[] moreAttributes)
		{
		}

		// Token: 0x0600274B RID: 10059 RVA: 0x000CF71D File Offset: 0x000CD91D
		internal virtual void AddAnnotation(XmlSchemaAnnotation annotation)
		{
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x0600274C RID: 10060 RVA: 0x000CF71F File Offset: 0x000CD91F
		// (set) Token: 0x0600274D RID: 10061 RVA: 0x000CF722 File Offset: 0x000CD922
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

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x0600274E RID: 10062 RVA: 0x000CF724 File Offset: 0x000CD924
		// (set) Token: 0x0600274F RID: 10063 RVA: 0x000CF72C File Offset: 0x000CD92C
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

		// Token: 0x06002750 RID: 10064 RVA: 0x000CF735 File Offset: 0x000CD935
		internal virtual XmlSchemaObject Clone()
		{
			return (XmlSchemaObject)base.MemberwiseClone();
		}

		// Token: 0x0400111E RID: 4382
		private int lineNum;

		// Token: 0x0400111F RID: 4383
		private int linePos;

		// Token: 0x04001120 RID: 4384
		private string sourceUri;

		// Token: 0x04001121 RID: 4385
		private XmlSerializerNamespaces namespaces;

		// Token: 0x04001122 RID: 4386
		private XmlSchemaObject parent;

		// Token: 0x04001123 RID: 4387
		private bool isProcessing;
	}
}
