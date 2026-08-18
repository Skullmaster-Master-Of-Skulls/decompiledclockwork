using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000033 RID: 51
	internal class PreSiblingQuery : CacheAxisQuery
	{
		// Token: 0x0600017E RID: 382 RVA: 0x00005F36 File Offset: 0x00004136
		public PreSiblingQuery(Query qyInput, string name, string prefix, XPathNodeType typeTest) : base(qyInput, name, prefix, typeTest)
		{
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00005F43 File Offset: 0x00004143
		protected PreSiblingQuery(PreSiblingQuery other) : base(other)
		{
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00005F4C File Offset: 0x0000414C
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

		// Token: 0x06000181 RID: 385 RVA: 0x00005F94 File Offset: 0x00004194
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
						bool flag = xpathNavigator2.MoveToFirstChild();
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

		// Token: 0x06000182 RID: 386 RVA: 0x00006054 File Offset: 0x00004254
		public override XPathNodeIterator Clone()
		{
			return new PreSiblingQuery(this);
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000605C File Offset: 0x0000425C
		public override QueryProps Properties
		{
			get
			{
				return base.Properties | QueryProps.Reverse;
			}
		}
	}
}
