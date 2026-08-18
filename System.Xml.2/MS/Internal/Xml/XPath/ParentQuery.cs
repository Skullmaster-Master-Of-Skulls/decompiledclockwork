using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000031 RID: 49
	internal sealed class ParentQuery : CacheAxisQuery
	{
		// Token: 0x06000174 RID: 372 RVA: 0x00005D47 File Offset: 0x00003F47
		public ParentQuery(Query qyInput, string Name, string Prefix, XPathNodeType Type) : base(qyInput, Name, Prefix, Type)
		{
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00005D54 File Offset: 0x00003F54
		private ParentQuery(ParentQuery other) : base(other)
		{
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005D60 File Offset: 0x00003F60
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

		// Token: 0x06000177 RID: 375 RVA: 0x00005DAD File Offset: 0x00003FAD
		public override XPathNodeIterator Clone()
		{
			return new ParentQuery(this);
		}
	}
}
