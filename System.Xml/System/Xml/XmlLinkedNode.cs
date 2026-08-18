using System;

namespace System.Xml
{
	// Token: 0x020000CD RID: 205
	public abstract class XmlLinkedNode : XmlNode
	{
		// Token: 0x06000C3D RID: 3133 RVA: 0x00037A72 File Offset: 0x00036A72
		internal XmlLinkedNode()
		{
			this.next = null;
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00037A81 File Offset: 0x00036A81
		internal XmlLinkedNode(XmlDocument doc) : base(doc)
		{
			this.next = null;
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x00037A94 File Offset: 0x00036A94
		public override XmlNode PreviousSibling
		{
			get
			{
				XmlNode parentNode = this.ParentNode;
				if (parentNode != null)
				{
					XmlNode xmlNode;
					XmlNode nextSibling;
					for (xmlNode = parentNode.FirstChild; xmlNode != null; xmlNode = nextSibling)
					{
						nextSibling = xmlNode.NextSibling;
						if (nextSibling == this)
						{
							break;
						}
					}
					return xmlNode;
				}
				return null;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x00037AC8 File Offset: 0x00036AC8
		public override XmlNode NextSibling
		{
			get
			{
				XmlNode parentNode = this.ParentNode;
				if (parentNode != null && this.next != parentNode.FirstChild)
				{
					return this.next;
				}
				return null;
			}
		}

		// Token: 0x040008F1 RID: 2289
		internal XmlLinkedNode next;
	}
}
