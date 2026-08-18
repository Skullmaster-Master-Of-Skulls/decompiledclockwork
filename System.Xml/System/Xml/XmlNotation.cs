using System;

namespace System.Xml
{
	// Token: 0x020000EC RID: 236
	public class XmlNotation : XmlNode
	{
		// Token: 0x06000E79 RID: 3705 RVA: 0x000405A9 File Offset: 0x0003F5A9
		internal XmlNotation(string name, string publicId, string systemId, XmlDocument doc) : base(doc)
		{
			this.name = doc.NameTable.Add(name);
			this.publicId = publicId;
			this.systemId = systemId;
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x000405D4 File Offset: 0x0003F5D4
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x000405DC File Offset: 0x0003F5DC
		public override string LocalName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x000405E4 File Offset: 0x0003F5E4
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Notation;
			}
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x000405E8 File Offset: 0x0003F5E8
		public override XmlNode CloneNode(bool deep)
		{
			throw new InvalidOperationException(Res.GetString("Xdom_Node_Cloning"));
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x000405F9 File Offset: 0x0003F5F9
		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000E7F RID: 3711 RVA: 0x000405FC File Offset: 0x0003F5FC
		public string PublicId
		{
			get
			{
				return this.publicId;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x00040604 File Offset: 0x0003F604
		public string SystemId
		{
			get
			{
				return this.systemId;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x0004060C File Offset: 0x0003F60C
		public override string OuterXml
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000E82 RID: 3714 RVA: 0x00040613 File Offset: 0x0003F613
		// (set) Token: 0x06000E83 RID: 3715 RVA: 0x0004061A File Offset: 0x0003F61A
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

		// Token: 0x06000E84 RID: 3716 RVA: 0x0004062B File Offset: 0x0003F62B
		public override void WriteTo(XmlWriter w)
		{
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0004062D File Offset: 0x0003F62D
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x040009A3 RID: 2467
		private string publicId;

		// Token: 0x040009A4 RID: 2468
		private string systemId;

		// Token: 0x040009A5 RID: 2469
		private string name;
	}
}
