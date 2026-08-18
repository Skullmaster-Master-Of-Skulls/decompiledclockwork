using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x02000121 RID: 289
	internal class XPathNodeList : XmlNodeList
	{
		// Token: 0x0600145F RID: 5215 RVA: 0x00054024 File Offset: 0x00052224
		public XPathNodeList(XPathNodeIterator nodeIterator)
		{
			this.nodeIterator = nodeIterator;
			this.list = new List<XmlNode>();
			this.done = false;
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x00054045 File Offset: 0x00052245
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

		// Token: 0x06001461 RID: 5217 RVA: 0x00054068 File Offset: 0x00052268
		private XmlNode GetNode(XPathNavigator n)
		{
			IHasXmlNode hasXmlNode = (IHasXmlNode)n;
			return hasXmlNode.GetNode();
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x00054084 File Offset: 0x00052284
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

		// Token: 0x06001463 RID: 5219 RVA: 0x000540E9 File Offset: 0x000522E9
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

		// Token: 0x06001464 RID: 5220 RVA: 0x00054121 File Offset: 0x00052321
		public override IEnumerator GetEnumerator()
		{
			return new XmlNodeListEnumerator(this);
		}

		// Token: 0x04000589 RID: 1417
		private List<XmlNode> list;

		// Token: 0x0400058A RID: 1418
		private XPathNodeIterator nodeIterator;

		// Token: 0x0400058B RID: 1419
		private bool done;

		// Token: 0x0400058C RID: 1420
		private static readonly object[] nullparams = new object[0];
	}
}
