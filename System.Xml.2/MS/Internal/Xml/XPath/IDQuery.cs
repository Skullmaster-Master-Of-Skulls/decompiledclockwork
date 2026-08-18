using System;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000024 RID: 36
	internal sealed class IDQuery : CacheOutputQuery
	{
		// Token: 0x060000EF RID: 239 RVA: 0x000045AB File Offset: 0x000027AB
		public IDQuery(Query arg) : base(arg)
		{
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000045B4 File Offset: 0x000027B4
		private IDQuery(IDQuery other) : base(other)
		{
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000045C0 File Offset: 0x000027C0
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

		// Token: 0x060000F2 RID: 242 RVA: 0x00004670 File Offset: 0x00002870
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

		// Token: 0x060000F3 RID: 243 RVA: 0x000046AC File Offset: 0x000028AC
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

		// Token: 0x060000F4 RID: 244 RVA: 0x000046DF File Offset: 0x000028DF
		public override XPathNodeIterator Clone()
		{
			return new IDQuery(this);
		}
	}
}
