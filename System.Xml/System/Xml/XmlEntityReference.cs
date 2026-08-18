using System;

namespace System.Xml
{
	// Token: 0x020000E0 RID: 224
	public class XmlEntityReference : XmlLinkedNode
	{
		// Token: 0x06000DAF RID: 3503 RVA: 0x0003C454 File Offset: 0x0003B454
		protected internal XmlEntityReference(string name, XmlDocument doc) : base(doc)
		{
			if (!doc.IsLoading && name.Length > 0 && name[0] == '#')
			{
				throw new ArgumentException(Res.GetString("Xdom_InvalidCharacter_EntityReference"));
			}
			this.name = doc.NameTable.Add(name);
			doc.fEntRefNodesPresent = true;
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x0003C4AD File Offset: 0x0003B4AD
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x0003C4B5 File Offset: 0x0003B4B5
		public override string LocalName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x0003C4BD File Offset: 0x0003B4BD
		// (set) Token: 0x06000DB3 RID: 3507 RVA: 0x0003C4C0 File Offset: 0x0003B4C0
		public override string Value
		{
			get
			{
				return null;
			}
			set
			{
				throw new InvalidOperationException(Res.GetString("Xdom_EntRef_SetVal"));
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x0003C4D1 File Offset: 0x0003B4D1
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.EntityReference;
			}
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0003C4D4 File Offset: 0x0003B4D4
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateEntityReference(this.name);
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x0003C4F4 File Offset: 0x0003B4F4
		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x0003C4F7 File Offset: 0x0003B4F7
		internal override bool IsContainer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0003C4FC File Offset: 0x0003B4FC
		internal override void SetParent(XmlNode node)
		{
			base.SetParent(node);
			if (this.LastNode == null && node != null && node != this.OwnerDocument)
			{
				XmlLoader xmlLoader = new XmlLoader();
				xmlLoader.ExpandEntityReference(this);
			}
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0003C531 File Offset: 0x0003B531
		internal override void SetParentForLoad(XmlNode node)
		{
			this.SetParent(node);
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x0003C53A File Offset: 0x0003B53A
		// (set) Token: 0x06000DBB RID: 3515 RVA: 0x0003C542 File Offset: 0x0003B542
		internal override XmlLinkedNode LastNode
		{
			get
			{
				return this.lastChild;
			}
			set
			{
				this.lastChild = value;
			}
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0003C54C File Offset: 0x0003B54C
		internal override bool IsValidChildType(XmlNodeType type)
		{
			switch (type)
			{
			case XmlNodeType.Element:
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.EntityReference:
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.Comment:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				return true;
			}
			return false;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0003C5A0 File Offset: 0x0003B5A0
		public override void WriteTo(XmlWriter w)
		{
			w.WriteEntityRef(this.name);
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0003C5B0 File Offset: 0x0003B5B0
		public override void WriteContentTo(XmlWriter w)
		{
			foreach (object obj in this)
			{
				XmlNode xmlNode = (XmlNode)obj;
				xmlNode.WriteTo(w);
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x0003C604 File Offset: 0x0003B604
		public override string BaseURI
		{
			get
			{
				return this.OwnerDocument.BaseURI;
			}
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0003C614 File Offset: 0x0003B614
		private string ConstructBaseURI(string baseURI, string systemId)
		{
			if (baseURI == null)
			{
				return systemId;
			}
			int num = baseURI.LastIndexOf('/') + 1;
			string str = baseURI;
			if (num > 0 && num < baseURI.Length)
			{
				str = baseURI.Substring(0, num);
			}
			else if (num == 0)
			{
				str += "\\";
			}
			return str + systemId.Replace('\\', '/');
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x0003C66C File Offset: 0x0003B66C
		internal string ChildBaseURI
		{
			get
			{
				XmlEntity entityNode = this.OwnerDocument.GetEntityNode(this.name);
				if (entityNode == null)
				{
					return string.Empty;
				}
				if (entityNode.SystemId != null && entityNode.SystemId.Length > 0)
				{
					return this.ConstructBaseURI(entityNode.BaseURI, entityNode.SystemId);
				}
				return entityNode.BaseURI;
			}
		}

		// Token: 0x04000963 RID: 2403
		private string name;

		// Token: 0x04000964 RID: 2404
		private XmlLinkedNode lastChild;
	}
}
