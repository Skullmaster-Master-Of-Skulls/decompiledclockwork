using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200016F RID: 367
	internal class XPathSelectionIterator : ResetableIterator
	{
		// Token: 0x060013A2 RID: 5026 RVA: 0x0005548D File Offset: 0x0005448D
		internal XPathSelectionIterator(XPathNavigator nav, Query query)
		{
			this.nav = nav.Clone();
			this.query = query;
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x000554A8 File Offset: 0x000544A8
		protected XPathSelectionIterator(XPathSelectionIterator it)
		{
			this.nav = it.nav.Clone();
			this.query = (Query)it.query.Clone();
			this.position = it.position;
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x000554E3 File Offset: 0x000544E3
		public override void Reset()
		{
			this.query.Reset();
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x000554F0 File Offset: 0x000544F0
		public override bool MoveNext()
		{
			XPathNavigator xpathNavigator = this.query.Advance();
			if (xpathNavigator != null)
			{
				this.position++;
				if (!this.nav.MoveTo(xpathNavigator))
				{
					this.nav = xpathNavigator.Clone();
				}
				return true;
			}
			return false;
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x00055537 File Offset: 0x00054537
		public override int Count
		{
			get
			{
				return this.query.Count;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060013A7 RID: 5031 RVA: 0x00055544 File Offset: 0x00054544
		public override XPathNavigator Current
		{
			get
			{
				return this.nav;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x0005554C File Offset: 0x0005454C
		public override int CurrentPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x00055554 File Offset: 0x00054554
		public override XPathNodeIterator Clone()
		{
			return new XPathSelectionIterator(this);
		}

		// Token: 0x04000C2E RID: 3118
		private XPathNavigator nav;

		// Token: 0x04000C2F RID: 3119
		private Query query;

		// Token: 0x04000C30 RID: 3120
		private int position;
	}
}
