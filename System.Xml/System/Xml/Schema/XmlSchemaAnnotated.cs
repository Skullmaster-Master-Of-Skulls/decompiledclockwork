using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200022F RID: 559
	public class XmlSchemaAnnotated : XmlSchemaObject
	{
		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001AC9 RID: 6857 RVA: 0x000809B4 File Offset: 0x0007F9B4
		// (set) Token: 0x06001ACA RID: 6858 RVA: 0x000809BC File Offset: 0x0007F9BC
		[XmlAttribute("id", DataType = "ID")]
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001ACB RID: 6859 RVA: 0x000809C5 File Offset: 0x0007F9C5
		// (set) Token: 0x06001ACC RID: 6860 RVA: 0x000809CD File Offset: 0x0007F9CD
		[XmlElement("annotation", typeof(XmlSchemaAnnotation))]
		public XmlSchemaAnnotation Annotation
		{
			get
			{
				return this.annotation;
			}
			set
			{
				this.annotation = value;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001ACD RID: 6861 RVA: 0x000809D6 File Offset: 0x0007F9D6
		// (set) Token: 0x06001ACE RID: 6862 RVA: 0x000809DE File Offset: 0x0007F9DE
		[XmlAnyAttribute]
		public XmlAttribute[] UnhandledAttributes
		{
			get
			{
				return this.moreAttributes;
			}
			set
			{
				this.moreAttributes = value;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001ACF RID: 6863 RVA: 0x000809E7 File Offset: 0x0007F9E7
		// (set) Token: 0x06001AD0 RID: 6864 RVA: 0x000809EF File Offset: 0x0007F9EF
		[XmlIgnore]
		internal override string IdAttribute
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x000809F8 File Offset: 0x0007F9F8
		internal override void SetUnhandledAttributes(XmlAttribute[] moreAttributes)
		{
			this.moreAttributes = moreAttributes;
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x00080A01 File Offset: 0x0007FA01
		internal override void AddAnnotation(XmlSchemaAnnotation annotation)
		{
			this.annotation = annotation;
		}

		// Token: 0x040010D5 RID: 4309
		private string id;

		// Token: 0x040010D6 RID: 4310
		private XmlSchemaAnnotation annotation;

		// Token: 0x040010D7 RID: 4311
		private XmlAttribute[] moreAttributes;
	}
}
