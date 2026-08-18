using System;

namespace System.Xml
{
	// Token: 0x02000110 RID: 272
	public abstract class XmlLinkedNode : XmlNode
	{
		// Token: 0x060012FC RID: 4860 RVA: 0x0004E5CF File Offset: 0x0004C7CF
		internal XmlLinkedNode()
		{
			this.next = null;
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0004E5DE File Offset: 0x0004C7DE
		internal XmlLinkedNode(XmlDocument doc) : base(doc)
		{
			this.next = null;
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x0004E5F0 File Offset: 0x0004C7F0
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

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060012FF RID: 4863 RVA: 0x0004E624 File Offset: 0x0004C824
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

		// Token: 0x0400054A RID: 1354
		internal XmlLinkedNode next;
	}
}
