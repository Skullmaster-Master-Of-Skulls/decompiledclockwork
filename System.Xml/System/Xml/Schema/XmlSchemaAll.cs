using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000234 RID: 564
	public class XmlSchemaAll : XmlSchemaGroupBase
	{
		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x00080D24 File Offset: 0x0007FD24
		[XmlElement("element", typeof(XmlSchemaElement))]
		public override XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x00080D2C File Offset: 0x0007FD2C
		internal override bool IsEmpty
		{
			get
			{
				return base.IsEmpty || this.items.Count == 0;
			}
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x00080D46 File Offset: 0x0007FD46
		internal override void SetItems(XmlSchemaObjectCollection newItems)
		{
			this.items = newItems;
		}

		// Token: 0x040010E0 RID: 4320
		private XmlSchemaObjectCollection items = new XmlSchemaObjectCollection();
	}
}
