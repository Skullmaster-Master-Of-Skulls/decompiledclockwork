using System;

namespace System.Xml.Serialization
{
	// Token: 0x020001B8 RID: 440
	public struct XmlDeserializationEvents
	{
		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001E78 RID: 7800 RVA: 0x000A78CD File Offset: 0x000A5ACD
		// (set) Token: 0x06001E79 RID: 7801 RVA: 0x000A78D5 File Offset: 0x000A5AD5
		public XmlNodeEventHandler OnUnknownNode
		{
			get
			{
				return this.onUnknownNode;
			}
			set
			{
				this.onUnknownNode = value;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001E7A RID: 7802 RVA: 0x000A78DE File Offset: 0x000A5ADE
		// (set) Token: 0x06001E7B RID: 7803 RVA: 0x000A78E6 File Offset: 0x000A5AE6
		public XmlAttributeEventHandler OnUnknownAttribute
		{
			get
			{
				return this.onUnknownAttribute;
			}
			set
			{
				this.onUnknownAttribute = value;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001E7C RID: 7804 RVA: 0x000A78EF File Offset: 0x000A5AEF
		// (set) Token: 0x06001E7D RID: 7805 RVA: 0x000A78F7 File Offset: 0x000A5AF7
		public XmlElementEventHandler OnUnknownElement
		{
			get
			{
				return this.onUnknownElement;
			}
			set
			{
				this.onUnknownElement = value;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001E7E RID: 7806 RVA: 0x000A7900 File Offset: 0x000A5B00
		// (set) Token: 0x06001E7F RID: 7807 RVA: 0x000A7908 File Offset: 0x000A5B08
		public UnreferencedObjectEventHandler OnUnreferencedObject
		{
			get
			{
				return this.onUnreferencedObject;
			}
			set
			{
				this.onUnreferencedObject = value;
			}
		}

		// Token: 0x04000CDB RID: 3291
		private XmlNodeEventHandler onUnknownNode;

		// Token: 0x04000CDC RID: 3292
		private XmlAttributeEventHandler onUnknownAttribute;

		// Token: 0x04000CDD RID: 3293
		private XmlElementEventHandler onUnknownElement;

		// Token: 0x04000CDE RID: 3294
		private UnreferencedObjectEventHandler onUnreferencedObject;

		// Token: 0x04000CDF RID: 3295
		internal object sender;
	}
}
