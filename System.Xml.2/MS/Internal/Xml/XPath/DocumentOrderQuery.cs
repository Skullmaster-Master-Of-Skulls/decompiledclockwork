using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000018 RID: 24
	internal sealed class DocumentOrderQuery : CacheOutputQuery
	{
		// Token: 0x06000097 RID: 151 RVA: 0x0000363D File Offset: 0x0000183D
		public DocumentOrderQuery(Query qyParent) : base(qyParent)
		{
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003646 File Offset: 0x00001846
		private DocumentOrderQuery(DocumentOrderQuery other) : base(other)
		{
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003650 File Offset: 0x00001850
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

		// Token: 0x0600009A RID: 154 RVA: 0x00003685 File Offset: 0x00001885
		public override XPathNavigator MatchNode(XPathNavigator context)
		{
			return this.input.MatchNode(context);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003693 File Offset: 0x00001893
		public override XPathNodeIterator Clone()
		{
			return new DocumentOrderQuery(this);
		}
	}
}
