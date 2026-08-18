using System;

namespace System.Xml
{
	// Token: 0x0200010C RID: 268
	public class XmlEntity : XmlNode
	{
		// Token: 0x060012CE RID: 4814 RVA: 0x0004E1C0 File Offset: 0x0004C3C0
		internal XmlEntity(string name, string strdata, string publicId, string systemId, string notationName, XmlDocument doc) : base(doc)
		{
			this.name = doc.NameTable.Add(name);
			this.publicId = publicId;
			this.systemId = systemId;
			this.notationName = notationName;
			this.unparsedReplacementStr = strdata;
			this.childrenFoliating = false;
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x0004E20D File Offset: 0x0004C40D
		public override XmlNode CloneNode(bool deep)
		{
			throw new InvalidOperationException(Res.GetString("Xdom_Node_Cloning"));
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x0004E21E File Offset: 0x0004C41E
		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x0004E221 File Offset: 0x0004C421
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x0004E229 File Offset: 0x0004C429
		public override string LocalName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x0004E231 File Offset: 0x0004C431
		// (set) Token: 0x060012D4 RID: 4820 RVA: 0x0004E239 File Offset: 0x0004C439
		public override string InnerText
		{
			get
			{
				return base.InnerText;
			}
			set
			{
				throw new InvalidOperationException(Res.GetString("Xdom_Ent_Innertext"));
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x0004E24A File Offset: 0x0004C44A
		internal override bool IsContainer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x0004E250 File Offset: 0x0004C450
		// (set) Token: 0x060012D7 RID: 4823 RVA: 0x0004E287 File Offset: 0x0004C487
		internal override XmlLinkedNode LastNode
		{
			get
			{
				if (this.lastChild == null && !this.childrenFoliating)
				{
					this.childrenFoliating = true;
					XmlLoader xmlLoader = new XmlLoader();
					xmlLoader.ExpandEntity(this);
				}
				return this.lastChild;
			}
			set
			{
				this.lastChild = value;
			}
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x0004E290 File Offset: 0x0004C490
		internal override bool IsValidChildType(XmlNodeType type)
		{
			return type == XmlNodeType.Text || type == XmlNodeType.Element || type == XmlNodeType.ProcessingInstruction || type == XmlNodeType.Comment || type == XmlNodeType.CDATA || type == XmlNodeType.Whitespace || type == XmlNodeType.SignificantWhitespace || type == XmlNodeType.EntityReference;
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x0004E2B6 File Offset: 0x0004C4B6
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Entity;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x0004E2B9 File Offset: 0x0004C4B9
		public string PublicId
		{
			get
			{
				return this.publicId;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x0004E2C1 File Offset: 0x0004C4C1
		public string SystemId
		{
			get
			{
				return this.systemId;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x0004E2C9 File Offset: 0x0004C4C9
		public string NotationName
		{
			get
			{
				return this.notationName;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x0004E2D1 File Offset: 0x0004C4D1
		public override string OuterXml
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x060012DE RID: 4830 RVA: 0x0004E2D8 File Offset: 0x0004C4D8
		// (set) Token: 0x060012DF RID: 4831 RVA: 0x0004E2DF File Offset: 0x0004C4DF
		public override string InnerXml
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new InvalidOperationException(Res.GetString("Xdom_Set_InnerXml"));
			}
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x0004E2F0 File Offset: 0x0004C4F0
		public override void WriteTo(XmlWriter w)
		{
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x0004E2F2 File Offset: 0x0004C4F2
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x060012E2 RID: 4834 RVA: 0x0004E2F4 File Offset: 0x0004C4F4
		public override string BaseURI
		{
			get
			{
				return this.baseURI;
			}
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x0004E2FC File Offset: 0x0004C4FC
		internal void SetBaseURI(string inBaseURI)
		{
			this.baseURI = inBaseURI;
		}

		// Token: 0x0400053B RID: 1339
		private string publicId;

		// Token: 0x0400053C RID: 1340
		private string systemId;

		// Token: 0x0400053D RID: 1341
		private string notationName;

		// Token: 0x0400053E RID: 1342
		private string name;

		// Token: 0x0400053F RID: 1343
		private string unparsedReplacementStr;

		// Token: 0x04000540 RID: 1344
		private string baseURI;

		// Token: 0x04000541 RID: 1345
		private XmlLinkedNode lastChild;

		// Token: 0x04000542 RID: 1346
		private bool childrenFoliating;
	}
}
