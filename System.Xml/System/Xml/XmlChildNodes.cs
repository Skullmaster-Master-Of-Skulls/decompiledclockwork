using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x020000D2 RID: 210
	internal class XmlChildNodes : XmlNodeList
	{
		// Token: 0x06000C66 RID: 3174 RVA: 0x00038033 File Offset: 0x00037033
		public XmlChildNodes(XmlNode container)
		{
			this.container = container;
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x00038044 File Offset: 0x00037044
		public override XmlNode Item(int i)
		{
			if (i < 0)
			{
				return null;
			}
			XmlNode xmlNode = this.container.FirstChild;
			while (xmlNode != null)
			{
				if (i == 0)
				{
					return xmlNode;
				}
				xmlNode = xmlNode.NextSibling;
				i--;
			}
			return null;
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x0003807C File Offset: 0x0003707C
		public override int Count
		{
			get
			{
				int num = 0;
				for (XmlNode xmlNode = this.container.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x000380A8 File Offset: 0x000370A8
		public override IEnumerator GetEnumerator()
		{
			if (this.container.FirstChild == null)
			{
				return XmlDocument.EmptyEnumerator;
			}
			return new XmlChildEnumerator(this.container);
		}

		// Token: 0x040008F6 RID: 2294
		private XmlNode container;
	}
}
