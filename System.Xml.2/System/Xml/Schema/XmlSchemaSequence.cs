using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x020002A8 RID: 680
	public class XmlSchemaSequence : XmlSchemaGroupBase
	{
		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x0600278B RID: 10123 RVA: 0x000CFE31 File Offset: 0x000CE031
		[XmlElement("element", typeof(XmlSchemaElement))]
		[XmlElement("group", typeof(XmlSchemaGroupRef))]
		[XmlElement("choice", typeof(XmlSchemaChoice))]
		[XmlElement("sequence", typeof(XmlSchemaSequence))]
		[XmlElement("any", typeof(XmlSchemaAny))]
		public override XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x0600278C RID: 10124 RVA: 0x000CFE39 File Offset: 0x000CE039
		internal override bool IsEmpty
		{
			get
			{
				return base.IsEmpty || this.items.Count == 0;
			}
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x000CFE53 File Offset: 0x000CE053
		internal override void SetItems(XmlSchemaObjectCollection newItems)
		{
			this.items = newItems;
		}

		// Token: 0x04001130 RID: 4400
		private XmlSchemaObjectCollection items = new XmlSchemaObjectCollection();
	}
}
