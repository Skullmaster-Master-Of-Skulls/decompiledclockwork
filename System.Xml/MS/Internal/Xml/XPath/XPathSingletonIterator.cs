using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000171 RID: 369
	internal class XPathSingletonIterator : ResetableIterator
	{
		// Token: 0x060013AE RID: 5038 RVA: 0x000555BE File Offset: 0x000545BE
		public XPathSingletonIterator(XPathNavigator nav)
		{
			this.nav = nav;
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x000555CD File Offset: 0x000545CD
		public XPathSingletonIterator(XPathNavigator nav, bool moved) : this(nav)
		{
			if (moved)
			{
				this.position = 1;
			}
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x000555E0 File Offset: 0x000545E0
		public XPathSingletonIterator(XPathSingletonIterator it)
		{
			this.nav = it.nav.Clone();
			this.position = it.position;
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x00055605 File Offset: 0x00054605
		public override XPathNodeIterator Clone()
		{
			return new XPathSingletonIterator(this);
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x0005560D File Offset: 0x0005460D
		public override XPathNavigator Current
		{
			get
			{
				return this.nav;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x00055615 File Offset: 0x00054615
		public override int CurrentPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x0005561D File Offset: 0x0005461D
		public override int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x00055620 File Offset: 0x00054620
		public override bool MoveNext()
		{
			if (this.position == 0)
			{
				this.position = 1;
				return true;
			}
			return false;
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x00055634 File Offset: 0x00054634
		public override void Reset()
		{
			this.position = 0;
		}

		// Token: 0x04000C31 RID: 3121
		private XPathNavigator nav;

		// Token: 0x04000C32 RID: 3122
		private int position;
	}
}
