using System;

namespace System.Xml
{
	// Token: 0x0200010D RID: 269
	public class XmlEntityReference : XmlLinkedNode
	{
		// Token: 0x060012E4 RID: 4836 RVA: 0x0004E308 File Offset: 0x0004C508
		protected internal XmlEntityReference(string name, XmlDocument doc) : base(doc)
		{
			if (!doc.IsLoading && name.Length > 0 && name[0] == '#')
			{
				throw new ArgumentException(Res.GetString("Xdom_InvalidCharacter_EntityReference"));
			}
			this.name = doc.NameTable.Add(name);
			doc.fEntRefNodesPresent = true;
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x060012E5 RID: 4837 RVA: 0x0004E361 File Offset: 0x0004C561
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x0004E369 File Offset: 0x0004C569
		public override string LocalName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x060012E7 RID: 4839 RVA: 0x0004E371 File Offset: 0x0004C571
		// (set) Token: 0x060012E8 RID: 4840 RVA: 0x0004E374 File Offset: 0x0004C574
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

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x0004E385 File Offset: 0x0004C585
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.EntityReference;
			}
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x0004E388 File Offset: 0x0004C588
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateEntityReference(this.name);
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060012EB RID: 4843 RVA: 0x0004E3A8 File Offset: 0x0004C5A8
		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x0004E3AB File Offset: 0x0004C5AB
		internal override bool IsContainer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x0004E3B0 File Offset: 0x0004C5B0
		internal override void SetParent(XmlNode node)
		{
			base.SetParent(node);
			if (this.LastNode == null && node != null && node != this.OwnerDocument)
			{
				XmlLoader xmlLoader = new XmlLoader();
				xmlLoader.ExpandEntityReference(this);
			}
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x0004E3E5 File Offset: 0x0004C5E5
		internal override void SetParentForLoad(XmlNode node)
		{
			this.SetParent(node);
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060012EF RID: 4847 RVA: 0x0004E3EE File Offset: 0x0004C5EE
		// (set) Token: 0x060012F0 RID: 4848 RVA: 0x0004E3F6 File Offset: 0x0004C5F6
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

		// Token: 0x060012F1 RID: 4849 RVA: 0x0004E400 File Offset: 0x0004C600
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

		// Token: 0x060012F2 RID: 4850 RVA: 0x0004E452 File Offset: 0x0004C652
		public override void WriteTo(XmlWriter w)
		{
			w.WriteEntityRef(this.name);
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x0004E460 File Offset: 0x0004C660
		public override void WriteContentTo(XmlWriter w)
		{
			foreach (object obj in this)
			{
				XmlNode xmlNode = (XmlNode)obj;
				xmlNode.WriteTo(w);
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060012F4 RID: 4852 RVA: 0x0004E4B4 File Offset: 0x0004C6B4
		public override string BaseURI
		{
			get
			{
				return this.OwnerDocument.BaseURI;
			}
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x0004E4C4 File Offset: 0x0004C6C4
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

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x0004E51C File Offset: 0x0004C71C
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

		// Token: 0x04000543 RID: 1347
		private string name;

		// Token: 0x04000544 RID: 1348
		private XmlLinkedNode lastChild;
	}
}
