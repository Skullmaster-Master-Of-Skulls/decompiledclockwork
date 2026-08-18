using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x02000108 RID: 264
	internal class XmlElementList : XmlNodeList
	{
		// Token: 0x060012B2 RID: 4786 RVA: 0x0004DAFC File Offset: 0x0004BCFC
		private XmlElementList(XmlNode parent)
		{
			this.rootNode = parent;
			this.curInd = -1;
			this.curElem = this.rootNode;
			this.changeCount = 0;
			this.empty = false;
			this.atomized = true;
			this.matchCount = -1;
			this.listener = new WeakReference(new XmlElementListListener(parent.Document, this));
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x0004DB5C File Offset: 0x0004BD5C
		~XmlElementList()
		{
			this.Dispose(false);
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x0004DB8C File Offset: 0x0004BD8C
		internal void ConcurrencyCheck(XmlNodeChangedEventArgs args)
		{
			if (!this.atomized)
			{
				XmlNameTable nameTable = this.rootNode.Document.NameTable;
				this.localName = nameTable.Add(this.localName);
				this.namespaceURI = nameTable.Add(this.namespaceURI);
				this.atomized = true;
			}
			if (this.IsMatch(args.Node))
			{
				this.changeCount++;
				this.curInd = -1;
				this.curElem = this.rootNode;
				if (args.Action == XmlNodeChangedAction.Insert)
				{
					this.empty = false;
				}
			}
			this.matchCount = -1;
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x0004DC24 File Offset: 0x0004BE24
		internal XmlElementList(XmlNode parent, string name) : this(parent)
		{
			XmlNameTable nameTable = parent.Document.NameTable;
			this.asterisk = nameTable.Add("*");
			this.name = nameTable.Add(name);
			this.localName = null;
			this.namespaceURI = null;
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x0004DC70 File Offset: 0x0004BE70
		internal XmlElementList(XmlNode parent, string localName, string namespaceURI) : this(parent)
		{
			XmlNameTable nameTable = parent.Document.NameTable;
			this.asterisk = nameTable.Add("*");
			this.localName = nameTable.Get(localName);
			this.namespaceURI = nameTable.Get(namespaceURI);
			if (this.localName == null || this.namespaceURI == null)
			{
				this.empty = true;
				this.atomized = false;
				this.localName = localName;
				this.namespaceURI = namespaceURI;
			}
			this.name = null;
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x060012B7 RID: 4791 RVA: 0x0004DCEE File Offset: 0x0004BEEE
		internal int ChangeCount
		{
			get
			{
				return this.changeCount;
			}
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x0004DCF8 File Offset: 0x0004BEF8
		private XmlNode NextElemInPreOrder(XmlNode curNode)
		{
			XmlNode xmlNode = curNode.FirstChild;
			if (xmlNode == null)
			{
				xmlNode = curNode;
				while (xmlNode != null && xmlNode != this.rootNode && xmlNode.NextSibling == null)
				{
					xmlNode = xmlNode.ParentNode;
				}
				if (xmlNode != null && xmlNode != this.rootNode)
				{
					xmlNode = xmlNode.NextSibling;
				}
			}
			if (xmlNode == this.rootNode)
			{
				xmlNode = null;
			}
			return xmlNode;
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x0004DD50 File Offset: 0x0004BF50
		private XmlNode PrevElemInPreOrder(XmlNode curNode)
		{
			XmlNode xmlNode = curNode.PreviousSibling;
			while (xmlNode != null && xmlNode.LastChild != null)
			{
				xmlNode = xmlNode.LastChild;
			}
			if (xmlNode == null)
			{
				xmlNode = curNode.ParentNode;
			}
			if (xmlNode == this.rootNode)
			{
				xmlNode = null;
			}
			return xmlNode;
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x0004DD90 File Offset: 0x0004BF90
		private bool IsMatch(XmlNode curNode)
		{
			if (curNode.NodeType == XmlNodeType.Element)
			{
				if (this.name != null)
				{
					if (Ref.Equal(this.name, this.asterisk) || Ref.Equal(curNode.Name, this.name))
					{
						return true;
					}
				}
				else if ((Ref.Equal(this.localName, this.asterisk) || Ref.Equal(curNode.LocalName, this.localName)) && (Ref.Equal(this.namespaceURI, this.asterisk) || curNode.NamespaceURI == this.namespaceURI))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x0004DE28 File Offset: 0x0004C028
		private XmlNode GetMatchingNode(XmlNode n, bool bNext)
		{
			XmlNode xmlNode = n;
			do
			{
				if (bNext)
				{
					xmlNode = this.NextElemInPreOrder(xmlNode);
				}
				else
				{
					xmlNode = this.PrevElemInPreOrder(xmlNode);
				}
			}
			while (xmlNode != null && !this.IsMatch(xmlNode));
			return xmlNode;
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x0004DE5C File Offset: 0x0004C05C
		private XmlNode GetNthMatchingNode(XmlNode n, bool bNext, int nCount)
		{
			XmlNode xmlNode = n;
			for (int i = 0; i < nCount; i++)
			{
				xmlNode = this.GetMatchingNode(xmlNode, bNext);
				if (xmlNode == null)
				{
					return null;
				}
			}
			return xmlNode;
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x0004DE88 File Offset: 0x0004C088
		public XmlNode GetNextNode(XmlNode n)
		{
			if (this.empty)
			{
				return null;
			}
			XmlNode n2 = (n == null) ? this.rootNode : n;
			return this.GetMatchingNode(n2, true);
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x0004DEB4 File Offset: 0x0004C0B4
		public override XmlNode Item(int index)
		{
			if (this.rootNode == null || index < 0)
			{
				return null;
			}
			if (this.empty)
			{
				return null;
			}
			if (this.curInd == index)
			{
				return this.curElem;
			}
			int num = index - this.curInd;
			bool bNext = num > 0;
			if (num < 0)
			{
				num = -num;
			}
			XmlNode nthMatchingNode;
			if ((nthMatchingNode = this.GetNthMatchingNode(this.curElem, bNext, num)) != null)
			{
				this.curInd = index;
				this.curElem = nthMatchingNode;
				return this.curElem;
			}
			return null;
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x0004DF28 File Offset: 0x0004C128
		public override int Count
		{
			get
			{
				if (this.empty)
				{
					return 0;
				}
				if (this.matchCount < 0)
				{
					int num = 0;
					int num2 = this.changeCount;
					XmlNode matchingNode = this.rootNode;
					while ((matchingNode = this.GetMatchingNode(matchingNode, true)) != null)
					{
						num++;
					}
					if (num2 != this.changeCount)
					{
						return num;
					}
					this.matchCount = num;
				}
				return this.matchCount;
			}
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x0004DF82 File Offset: 0x0004C182
		public override IEnumerator GetEnumerator()
		{
			if (this.empty)
			{
				return new XmlEmptyElementListEnumerator(this);
			}
			return new XmlElementListEnumerator(this);
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x0004DF99 File Offset: 0x0004C199
		protected override void PrivateDisposeNodeList()
		{
			GC.SuppressFinalize(this);
			this.Dispose(true);
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x0004DFA8 File Offset: 0x0004C1A8
		protected virtual void Dispose(bool disposing)
		{
			if (this.listener != null)
			{
				XmlElementListListener xmlElementListListener = (XmlElementListListener)this.listener.Target;
				if (xmlElementListListener != null)
				{
					xmlElementListListener.Unregister();
				}
				this.listener = null;
			}
		}

		// Token: 0x04000529 RID: 1321
		private string asterisk;

		// Token: 0x0400052A RID: 1322
		private int changeCount;

		// Token: 0x0400052B RID: 1323
		private string name;

		// Token: 0x0400052C RID: 1324
		private string localName;

		// Token: 0x0400052D RID: 1325
		private string namespaceURI;

		// Token: 0x0400052E RID: 1326
		private XmlNode rootNode;

		// Token: 0x0400052F RID: 1327
		private int curInd;

		// Token: 0x04000530 RID: 1328
		private XmlNode curElem;

		// Token: 0x04000531 RID: 1329
		private bool empty;

		// Token: 0x04000532 RID: 1330
		private bool atomized;

		// Token: 0x04000533 RID: 1331
		private int matchCount;

		// Token: 0x04000534 RID: 1332
		private WeakReference listener;
	}
}
