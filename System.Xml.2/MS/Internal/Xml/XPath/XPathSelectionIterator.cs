using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200004A RID: 74
	internal class XPathSelectionIterator : ResetableIterator
	{
		// Token: 0x06000264 RID: 612 RVA: 0x00009ED1 File Offset: 0x000080D1
		internal XPathSelectionIterator(XPathNavigator nav, Query query)
		{
			this.nav = nav.Clone();
			this.query = query;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00009EEC File Offset: 0x000080EC
		protected XPathSelectionIterator(XPathSelectionIterator it)
		{
			this.nav = it.nav.Clone();
			this.query = (Query)it.query.Clone();
			this.position = it.position;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00009F27 File Offset: 0x00008127
		public override void Reset()
		{
			this.query.Reset();
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00009F34 File Offset: 0x00008134
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

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00009F7B File Offset: 0x0000817B
		public override int Count
		{
			get
			{
				return this.query.Count;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00009F88 File Offset: 0x00008188
		public override XPathNavigator Current
		{
			get
			{
				return this.nav;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600026A RID: 618 RVA: 0x00009F90 File Offset: 0x00008190
		public override int CurrentPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00009F98 File Offset: 0x00008198
		public override XPathNodeIterator Clone()
		{
			return new XPathSelectionIterator(this);
		}

		// Token: 0x040000FB RID: 251
		private XPathNavigator nav;

		// Token: 0x040000FC RID: 252
		private Query query;

		// Token: 0x040000FD RID: 253
		private int position;
	}
}
