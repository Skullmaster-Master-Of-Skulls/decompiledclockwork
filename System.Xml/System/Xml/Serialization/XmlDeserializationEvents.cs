using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000336 RID: 822
	public struct XmlDeserializationEvents
	{
		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x0600282D RID: 10285 RVA: 0x000D057C File Offset: 0x000CF57C
		// (set) Token: 0x0600282E RID: 10286 RVA: 0x000D0584 File Offset: 0x000CF584
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

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x0600282F RID: 10287 RVA: 0x000D058D File Offset: 0x000CF58D
		// (set) Token: 0x06002830 RID: 10288 RVA: 0x000D0595 File Offset: 0x000CF595
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

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06002831 RID: 10289 RVA: 0x000D059E File Offset: 0x000CF59E
		// (set) Token: 0x06002832 RID: 10290 RVA: 0x000D05A6 File Offset: 0x000CF5A6
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

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06002833 RID: 10291 RVA: 0x000D05AF File Offset: 0x000CF5AF
		// (set) Token: 0x06002834 RID: 10292 RVA: 0x000D05B7 File Offset: 0x000CF5B7
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

		// Token: 0x04001673 RID: 5747
		private XmlNodeEventHandler onUnknownNode;

		// Token: 0x04001674 RID: 5748
		private XmlAttributeEventHandler onUnknownAttribute;

		// Token: 0x04001675 RID: 5749
		private XmlElementEventHandler onUnknownElement;

		// Token: 0x04001676 RID: 5750
		private UnreferencedObjectEventHandler onUnreferencedObject;

		// Token: 0x04001677 RID: 5751
		internal object sender;
	}
}
