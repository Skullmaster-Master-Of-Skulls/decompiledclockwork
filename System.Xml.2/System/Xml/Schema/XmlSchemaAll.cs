using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200026B RID: 619
	public class XmlSchemaAll : XmlSchemaGroupBase
	{
		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06002565 RID: 9573 RVA: 0x000CCB8C File Offset: 0x000CAD8C
		[XmlElement("element", typeof(XmlSchemaElement))]
		public override XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06002566 RID: 9574 RVA: 0x000CCB94 File Offset: 0x000CAD94
		internal override bool IsEmpty
		{
			get
			{
				return base.IsEmpty || this.items.Count == 0;
			}
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x000CCBAE File Offset: 0x000CADAE
		internal override void SetItems(XmlSchemaObjectCollection newItems)
		{
			this.items = newItems;
		}

		// Token: 0x04001065 RID: 4197
		private XmlSchemaObjectCollection items = new XmlSchemaObjectCollection();
	}
}
