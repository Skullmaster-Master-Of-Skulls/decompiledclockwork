using System;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000145 RID: 325
	internal sealed class IDQuery : CacheOutputQuery
	{
		// Token: 0x06001244 RID: 4676 RVA: 0x0004FDE8 File Offset: 0x0004EDE8
		public IDQuery(Query arg) : base(arg)
		{
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x0004FDF1 File Offset: 0x0004EDF1
		private IDQuery(IDQuery other) : base(other)
		{
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x0004FDFC File Offset: 0x0004EDFC
		public override object Evaluate(XPathNodeIterator context)
		{
			object obj = base.Evaluate(context);
			XPathNavigator contextNode = context.Current.Clone();
			switch (base.GetXPathType(obj))
			{
			case XPathResultType.Number:
				this.ProcessIds(contextNode, StringFunctions.toString((double)obj));
				break;
			case XPathResultType.String:
				this.ProcessIds(contextNode, (string)obj);
				break;
			case XPathResultType.Boolean:
				this.ProcessIds(contextNode, StringFunctions.toString((bool)obj));
				break;
			case XPathResultType.NodeSet:
			{
				XPathNavigator xpathNavigator;
				while ((xpathNavigator = this.input.Advance()) != null)
				{
					this.ProcessIds(contextNode, xpathNavigator.Value);
				}
				break;
			}
			case (XPathResultType)4:
				this.ProcessIds(contextNode, ((XPathNavigator)obj).Value);
				break;
			}
			return this;
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0004FEAC File Offset: 0x0004EEAC
		private void ProcessIds(XPathNavigator contextNode, string val)
		{
			string[] array = XmlConvert.SplitString(val);
			for (int i = 0; i < array.Length; i++)
			{
				if (contextNode.MoveToId(array[i]))
				{
					base.Insert(this.outputBuffer, contextNode);
				}
			}
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0004FEE8 File Offset: 0x0004EEE8
		public override XPathNavigator MatchNode(XPathNavigator context)
		{
			this.Evaluate(new XPathSingletonIterator(context, true));
			XPathNavigator xpathNavigator;
			while ((xpathNavigator = this.Advance()) != null)
			{
				if (xpathNavigator.IsSamePosition(context))
				{
					return context;
				}
			}
			return null;
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x0004FF1B File Offset: 0x0004EF1B
		public override XPathNodeIterator Clone()
		{
			return new IDQuery(this);
		}
	}
}
