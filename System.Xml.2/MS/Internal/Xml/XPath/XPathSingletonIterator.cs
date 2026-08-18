using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200004C RID: 76
	internal class XPathSingletonIterator : ResetableIterator
	{
		// Token: 0x06000270 RID: 624 RVA: 0x0000A002 File Offset: 0x00008202
		public XPathSingletonIterator(XPathNavigator nav)
		{
			this.nav = nav;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000A011 File Offset: 0x00008211
		public XPathSingletonIterator(XPathNavigator nav, bool moved) : this(nav)
		{
			if (moved)
			{
				this.position = 1;
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000A024 File Offset: 0x00008224
		public XPathSingletonIterator(XPathSingletonIterator it)
		{
			this.nav = it.nav.Clone();
			this.position = it.position;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000A049 File Offset: 0x00008249
		public override XPathNodeIterator Clone()
		{
			return new XPathSingletonIterator(this);
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000A051 File Offset: 0x00008251
		public override XPathNavigator Current
		{
			get
			{
				return this.nav;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000A059 File Offset: 0x00008259
		public override int CurrentPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000A061 File Offset: 0x00008261
		public override int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000A064 File Offset: 0x00008264
		public override bool MoveNext()
		{
			if (this.position == 0)
			{
				this.position = 1;
				return true;
			}
			return false;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000A078 File Offset: 0x00008278
		public override void Reset()
		{
			this.position = 0;
		}

		// Token: 0x040000FE RID: 254
		private XPathNavigator nav;

		// Token: 0x040000FF RID: 255
		private int position;
	}
}
