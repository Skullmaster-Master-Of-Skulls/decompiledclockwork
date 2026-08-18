using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000129 RID: 297
	internal abstract class ValueQuery : Query
	{
		// Token: 0x0600117D RID: 4477 RVA: 0x0004DD54 File Offset: 0x0004CD54
		public ValueQuery()
		{
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x0004DD5C File Offset: 0x0004CD5C
		protected ValueQuery(ValueQuery other) : base(other)
		{
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x0004DD65 File Offset: 0x0004CD65
		public sealed override void Reset()
		{
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001180 RID: 4480 RVA: 0x0004DD67 File Offset: 0x0004CD67
		public sealed override XPathNavigator Current
		{
			get
			{
				throw XPathException.Create("Xp_NodeSetExpected");
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001181 RID: 4481 RVA: 0x0004DD73 File Offset: 0x0004CD73
		public sealed override int CurrentPosition
		{
			get
			{
				throw XPathException.Create("Xp_NodeSetExpected");
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001182 RID: 4482 RVA: 0x0004DD7F File Offset: 0x0004CD7F
		public sealed override int Count
		{
			get
			{
				throw XPathException.Create("Xp_NodeSetExpected");
			}
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x0004DD8B File Offset: 0x0004CD8B
		public sealed override XPathNavigator Advance()
		{
			throw XPathException.Create("Xp_NodeSetExpected");
		}
	}
}
