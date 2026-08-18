using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000F2 RID: 242
	internal class XPathNodeList : XmlNodeList
	{
		// Token: 0x06000ECA RID: 3786 RVA: 0x00040C3C File Offset: 0x0003FC3C
		public XPathNodeList(XPathNodeIterator nodeIterator)
		{
			this.nodeIterator = nodeIterator;
			this.list = new List<XmlNode>();
			this.done = false;
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x00040C5D File Offset: 0x0003FC5D
		public override int Count
		{
			get
			{
				if (!this.done)
				{
					this.ReadUntil(int.MaxValue);
				}
				return this.list.Count;
			}
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x00040C80 File Offset: 0x0003FC80
		private XmlNode GetNode(XPathNavigator n)
		{
			IHasXmlNode hasXmlNode = (IHasXmlNode)n;
			return hasXmlNode.GetNode();
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x00040C9C File Offset: 0x0003FC9C
		internal int ReadUntil(int index)
		{
			int num = this.list.Count;
			while (!this.done && num <= index)
			{
				if (!this.nodeIterator.MoveNext())
				{
					this.done = true;
					break;
				}
				XmlNode node = this.GetNode(this.nodeIterator.Current);
				if (node != null)
				{
					this.list.Add(node);
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x00040D01 File Offset: 0x0003FD01
		public override XmlNode Item(int index)
		{
			if (this.list.Count <= index)
			{
				this.ReadUntil(index);
			}
			if (index < 0 || this.list.Count <= index)
			{
				return null;
			}
			return this.list[index];
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x00040D39 File Offset: 0x0003FD39
		public override IEnumerator GetEnumerator()
		{
			return new XmlNodeListEnumerator(this);
		}

		// Token: 0x040009A9 RID: 2473
		private List<XmlNode> list;

		// Token: 0x040009AA RID: 2474
		private XPathNodeIterator nodeIterator;

		// Token: 0x040009AB RID: 2475
		private bool done;

		// Token: 0x040009AC RID: 2476
		private static readonly object[] nullparams = new object[0];
	}
}
