using System;

namespace System.Xml
{
	// Token: 0x020000DF RID: 223
	public class XmlEntity : XmlNode
	{
		// Token: 0x06000D99 RID: 3481 RVA: 0x0003C30C File Offset: 0x0003B30C
		internal XmlEntity(string name, string strdata, string publicId, string systemId, string notationName, XmlDocument doc) : base(doc)
		{
			this.name = doc.NameTable.Add(name);
			this.publicId = publicId;
			this.systemId = systemId;
			this.notationName = notationName;
			this.unparsedReplacementStr = strdata;
			this.childrenFoliating = false;
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0003C359 File Offset: 0x0003B359
		public override XmlNode CloneNode(bool deep)
		{
			throw new InvalidOperationException(Res.GetString("Xdom_Node_Cloning"));
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x0003C36A File Offset: 0x0003B36A
		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x0003C36D File Offset: 0x0003B36D
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x0003C375 File Offset: 0x0003B375
		public override string LocalName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x0003C37D File Offset: 0x0003B37D
		// (set) Token: 0x06000D9F RID: 3487 RVA: 0x0003C385 File Offset: 0x0003B385
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

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x0003C396 File Offset: 0x0003B396
		internal override bool IsContainer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x0003C39C File Offset: 0x0003B39C
		// (set) Token: 0x06000DA2 RID: 3490 RVA: 0x0003C3D3 File Offset: 0x0003B3D3
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

		// Token: 0x06000DA3 RID: 3491 RVA: 0x0003C3DC File Offset: 0x0003B3DC
		internal override bool IsValidChildType(XmlNodeType type)
		{
			return type == XmlNodeType.Text || type == XmlNodeType.Element || type == XmlNodeType.ProcessingInstruction || type == XmlNodeType.Comment || type == XmlNodeType.CDATA || type == XmlNodeType.Whitespace || type == XmlNodeType.SignificantWhitespace || type == XmlNodeType.EntityReference;
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x0003C402 File Offset: 0x0003B402
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Entity;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x0003C405 File Offset: 0x0003B405
		public string PublicId
		{
			get
			{
				return this.publicId;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x0003C40D File Offset: 0x0003B40D
		public string SystemId
		{
			get
			{
				return this.systemId;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x0003C415 File Offset: 0x0003B415
		public string NotationName
		{
			get
			{
				return this.notationName;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x0003C41D File Offset: 0x0003B41D
		public override string OuterXml
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x0003C424 File Offset: 0x0003B424
		// (set) Token: 0x06000DAA RID: 3498 RVA: 0x0003C42B File Offset: 0x0003B42B
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

		// Token: 0x06000DAB RID: 3499 RVA: 0x0003C43C File Offset: 0x0003B43C
		public override void WriteTo(XmlWriter w)
		{
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x0003C43E File Offset: 0x0003B43E
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x0003C440 File Offset: 0x0003B440
		public override string BaseURI
		{
			get
			{
				return this.baseURI;
			}
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0003C448 File Offset: 0x0003B448
		internal void SetBaseURI(string inBaseURI)
		{
			this.baseURI = inBaseURI;
		}

		// Token: 0x0400095B RID: 2395
		private string publicId;

		// Token: 0x0400095C RID: 2396
		private string systemId;

		// Token: 0x0400095D RID: 2397
		private string notationName;

		// Token: 0x0400095E RID: 2398
		private string name;

		// Token: 0x0400095F RID: 2399
		private string unparsedReplacementStr;

		// Token: 0x04000960 RID: 2400
		private string baseURI;

		// Token: 0x04000961 RID: 2401
		private XmlLinkedNode lastChild;

		// Token: 0x04000962 RID: 2402
		private bool childrenFoliating;
	}
}
