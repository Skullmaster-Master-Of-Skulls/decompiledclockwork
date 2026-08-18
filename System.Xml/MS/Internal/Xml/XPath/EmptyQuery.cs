using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000139 RID: 313
	internal sealed class EmptyQuery : Query
	{
		// Token: 0x060011F1 RID: 4593 RVA: 0x0004EE77 File Offset: 0x0004DE77
		public override XPathNavigator Advance()
		{
			return null;
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x0004EE7A File Offset: 0x0004DE7A
		public override XPathNodeIterator Clone()
		{
			return this;
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x0004EE7D File Offset: 0x0004DE7D
		public override object Evaluate(XPathNodeIterator context)
		{
			return this;
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x0004EE80 File Offset: 0x0004DE80
		public override int CurrentPosition
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x0004EE83 File Offset: 0x0004DE83
		public override int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x0004EE86 File Offset: 0x0004DE86
		public override QueryProps Properties
		{
			get
			{
				return (QueryProps)23;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x0004EE8A File Offset: 0x0004DE8A
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x0004EE8D File Offset: 0x0004DE8D
		public override void Reset()
		{
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x0004EE8F File Offset: 0x0004DE8F
		public override XPathNavigator Current
		{
			get
			{
				return null;
			}
		}
	}
}
