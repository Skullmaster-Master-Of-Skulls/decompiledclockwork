using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000156 RID: 342
	internal class PreSiblingQuery : CacheAxisQuery
	{
		// Token: 0x060012C4 RID: 4804 RVA: 0x0005157A File Offset: 0x0005057A
		public PreSiblingQuery(Query qyInput, string name, string prefix, XPathNodeType typeTest) : base(qyInput, name, prefix, typeTest)
		{
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x00051587 File Offset: 0x00050587
		protected PreSiblingQuery(PreSiblingQuery other) : base(other)
		{
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x00051590 File Offset: 0x00050590
		private bool NotVisited(XPathNavigator nav, List<XPathNavigator> parentStk)
		{
			XPathNavigator xpathNavigator = nav.Clone();
			xpathNavigator.MoveToParent();
			for (int i = 0; i < parentStk.Count; i++)
			{
				if (xpathNavigator.IsSamePosition(parentStk[i]))
				{
					return false;
				}
			}
			parentStk.Add(xpathNavigator);
			return true;
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x000515D8 File Offset: 0x000505D8
		public override object Evaluate(XPathNodeIterator context)
		{
			base.Evaluate(context);
			List<XPathNavigator> parentStk = new List<XPathNavigator>();
			Stack<XPathNavigator> stack = new Stack<XPathNavigator>();
			while ((this.currentNode = this.qyInput.Advance()) != null)
			{
				stack.Push(this.currentNode.Clone());
			}
			while (stack.Count != 0)
			{
				XPathNavigator xpathNavigator = stack.Pop();
				if (xpathNavigator.NodeType != XPathNodeType.Attribute && xpathNavigator.NodeType != XPathNodeType.Namespace && this.NotVisited(xpathNavigator, parentStk))
				{
					XPathNavigator xpathNavigator2 = xpathNavigator.Clone();
					if (xpathNavigator2.MoveToParent())
					{
						xpathNavigator2.MoveToFirstChild();
						while (!xpathNavigator2.IsSamePosition(xpathNavigator))
						{
							if (this.matches(xpathNavigator2))
							{
								base.Insert(this.outputBuffer, xpathNavigator2);
							}
							if (!xpathNavigator2.MoveToNext())
							{
								break;
							}
						}
					}
				}
			}
			return this;
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x00051692 File Offset: 0x00050692
		public override XPathNodeIterator Clone()
		{
			return new PreSiblingQuery(this);
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x0005169A File Offset: 0x0005069A
		public override QueryProps Properties
		{
			get
			{
				return base.Properties | QueryProps.Reverse;
			}
		}
	}
}
