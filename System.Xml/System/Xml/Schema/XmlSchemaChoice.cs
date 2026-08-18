using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200023C RID: 572
	public class XmlSchemaChoice : XmlSchemaGroupBase
	{
		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001B47 RID: 6983 RVA: 0x000813F5 File Offset: 0x000803F5
		[XmlElement("sequence", typeof(XmlSchemaSequence))]
		[XmlElement("any", typeof(XmlSchemaAny))]
		[XmlElement("element", typeof(XmlSchemaElement))]
		[XmlElement("choice", typeof(XmlSchemaChoice))]
		[XmlElement("group", typeof(XmlSchemaGroupRef))]
		public override XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001B48 RID: 6984 RVA: 0x000813FD File Offset: 0x000803FD
		internal override bool IsEmpty
		{
			get
			{
				return base.IsEmpty;
			}
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x00081405 File Offset: 0x00080405
		internal override void SetItems(XmlSchemaObjectCollection newItems)
		{
			this.items = newItems;
		}

		// Token: 0x04001101 RID: 4353
		private XmlSchemaObjectCollection items = new XmlSchemaObjectCollection();
	}
}
