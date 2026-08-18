using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000154 RID: 340
	internal sealed class ParentQuery : CacheAxisQuery
	{
		// Token: 0x060012BA RID: 4794 RVA: 0x0005138C File Offset: 0x0005038C
		public ParentQuery(Query qyInput, string Name, string Prefix, XPathNodeType Type) : base(qyInput, Name, Prefix, Type)
		{
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00051399 File Offset: 0x00050399
		private ParentQuery(ParentQuery other) : base(other)
		{
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x000513A4 File Offset: 0x000503A4
		public override object Evaluate(XPathNodeIterator context)
		{
			base.Evaluate(context);
			XPathNavigator xpathNavigator;
			while ((xpathNavigator = this.qyInput.Advance()) != null)
			{
				xpathNavigator = xpathNavigator.Clone();
				if (xpathNavigator.MoveToParent() && this.matches(xpathNavigator))
				{
					base.Insert(this.outputBuffer, xpathNavigator);
				}
			}
			return this;
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x000513F1 File Offset: 0x000503F1
		public override XPathNodeIterator Clone()
		{
			return new ParentQuery(this);
		}
	}
}
