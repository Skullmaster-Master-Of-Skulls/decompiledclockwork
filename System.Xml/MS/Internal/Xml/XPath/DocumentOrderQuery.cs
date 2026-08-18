using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000138 RID: 312
	internal sealed class DocumentOrderQuery : CacheOutputQuery
	{
		// Token: 0x060011EC RID: 4588 RVA: 0x0004EE17 File Offset: 0x0004DE17
		public DocumentOrderQuery(Query qyParent) : base(qyParent)
		{
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x0004EE20 File Offset: 0x0004DE20
		private DocumentOrderQuery(DocumentOrderQuery other) : base(other)
		{
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x0004EE2C File Offset: 0x0004DE2C
		public override object Evaluate(XPathNodeIterator context)
		{
			base.Evaluate(context);
			XPathNavigator nav;
			while ((nav = this.input.Advance()) != null)
			{
				base.Insert(this.outputBuffer, nav);
			}
			return this;
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x0004EE61 File Offset: 0x0004DE61
		public override XPathNavigator MatchNode(XPathNavigator context)
		{
			return this.input.MatchNode(context);
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x0004EE6F File Offset: 0x0004DE6F
		public override XPathNodeIterator Clone()
		{
			return new DocumentOrderQuery(this);
		}
	}
}
