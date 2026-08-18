using System;

namespace System.Xml
{
	// Token: 0x0200011B RID: 283
	public class XmlNotation : XmlNode
	{
		// Token: 0x0600140E RID: 5134 RVA: 0x000539CF File Offset: 0x00051BCF
		internal XmlNotation(string name, string publicId, string systemId, XmlDocument doc) : base(doc)
		{
			this.name = doc.NameTable.Add(name);
			this.publicId = publicId;
			this.systemId = systemId;
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x000539FA File Offset: 0x00051BFA
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x00053A02 File Offset: 0x00051C02
		public override string LocalName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x00053A0A File Offset: 0x00051C0A
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Notation;
			}
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x00053A0E File Offset: 0x00051C0E
		public override XmlNode CloneNode(bool deep)
		{
			throw new InvalidOperationException(Res.GetString("Xdom_Node_Cloning"));
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x00053A1F File Offset: 0x00051C1F
		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x00053A22 File Offset: 0x00051C22
		public string PublicId
		{
			get
			{
				return this.publicId;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x00053A2A File Offset: 0x00051C2A
		public string SystemId
		{
			get
			{
				return this.systemId;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x00053A32 File Offset: 0x00051C32
		public override string OuterXml
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001417 RID: 5143 RVA: 0x00053A39 File Offset: 0x00051C39
		// (set) Token: 0x06001418 RID: 5144 RVA: 0x00053A40 File Offset: 0x00051C40
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

		// Token: 0x06001419 RID: 5145 RVA: 0x00053A51 File Offset: 0x00051C51
		public override void WriteTo(XmlWriter w)
		{
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x00053A53 File Offset: 0x00051C53
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x04000583 RID: 1411
		private string publicId;

		// Token: 0x04000584 RID: 1412
		private string systemId;

		// Token: 0x04000585 RID: 1413
		private string name;
	}
}
