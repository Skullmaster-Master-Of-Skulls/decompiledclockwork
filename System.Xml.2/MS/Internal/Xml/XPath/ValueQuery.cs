using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200003D RID: 61
	internal abstract class ValueQuery : Query
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x00007AA1 File Offset: 0x00005CA1
		public ValueQuery()
		{
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00007AA9 File Offset: 0x00005CA9
		protected ValueQuery(ValueQuery other) : base(other)
		{
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00007AB2 File Offset: 0x00005CB2
		public sealed override void Reset()
		{
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00007AB4 File Offset: 0x00005CB4
		public sealed override XPathNavigator Current
		{
			get
			{
				throw XPathException.Create("Xp_NodeSetExpected");
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00007AC0 File Offset: 0x00005CC0
		public sealed override int CurrentPosition
		{
			get
			{
				throw XPathException.Create("Xp_NodeSetExpected");
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00007ACC File Offset: 0x00005CCC
		public sealed override int Count
		{
			get
			{
				throw XPathException.Create("Xp_NodeSetExpected");
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00007AD8 File Offset: 0x00005CD8
		public sealed override XPathNavigator Advance()
		{
			throw XPathException.Create("Xp_NodeSetExpected");
		}
	}
}
