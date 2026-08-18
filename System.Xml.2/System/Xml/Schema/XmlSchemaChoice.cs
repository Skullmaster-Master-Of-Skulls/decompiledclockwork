using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000274 RID: 628
	public class XmlSchemaChoice : XmlSchemaGroupBase
	{
		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x060025CF RID: 9679 RVA: 0x000CD2C9 File Offset: 0x000CB4C9
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

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x060025D0 RID: 9680 RVA: 0x000CD2D1 File Offset: 0x000CB4D1
		internal override bool IsEmpty
		{
			get
			{
				return base.IsEmpty;
			}
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x000CD2D9 File Offset: 0x000CB4D9
		internal override void SetItems(XmlSchemaObjectCollection newItems)
		{
			this.items = newItems;
		}

		// Token: 0x04001088 RID: 4232
		private XmlSchemaObjectCollection items = new XmlSchemaObjectCollection();
	}
}
