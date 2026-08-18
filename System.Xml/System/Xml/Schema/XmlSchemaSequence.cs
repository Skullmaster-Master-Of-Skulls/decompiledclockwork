using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000273 RID: 627
	public class XmlSchemaSequence : XmlSchemaGroupBase
	{
		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001CF2 RID: 7410 RVA: 0x00083DB0 File Offset: 0x00082DB0
		[XmlElement("group", typeof(XmlSchemaGroupRef))]
		[XmlElement("choice", typeof(XmlSchemaChoice))]
		[XmlElement("any", typeof(XmlSchemaAny))]
		[XmlElement("element", typeof(XmlSchemaElement))]
		[XmlElement("sequence", typeof(XmlSchemaSequence))]
		public override XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001CF3 RID: 7411 RVA: 0x00083DB8 File Offset: 0x00082DB8
		internal override bool IsEmpty
		{
			get
			{
				return base.IsEmpty || this.items.Count == 0;
			}
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x00083DD2 File Offset: 0x00082DD2
		internal override void SetItems(XmlSchemaObjectCollection newItems)
		{
			this.items = newItems;
		}

		// Token: 0x040011BA RID: 4538
		private XmlSchemaObjectCollection items = new XmlSchemaObjectCollection();
	}
}
