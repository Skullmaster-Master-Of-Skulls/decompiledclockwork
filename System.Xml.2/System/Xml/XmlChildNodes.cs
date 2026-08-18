using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x020000FF RID: 255
	internal class XmlChildNodes : XmlNodeList
	{
		// Token: 0x06001195 RID: 4501 RVA: 0x00049E63 File Offset: 0x00048063
		public XmlChildNodes(XmlNode container)
		{
			this.container = container;
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x00049E74 File Offset: 0x00048074
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

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x00049EAC File Offset: 0x000480AC
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

		// Token: 0x06001198 RID: 4504 RVA: 0x00049ED8 File Offset: 0x000480D8
		public override IEnumerator GetEnumerator()
		{
			if (this.container.FirstChild == null)
			{
				return XmlDocument.EmptyEnumerator;
			}
			return new XmlChildEnumerator(this.container);
		}

		// Token: 0x040004D5 RID: 1237
		private XmlNode container;
	}
}
