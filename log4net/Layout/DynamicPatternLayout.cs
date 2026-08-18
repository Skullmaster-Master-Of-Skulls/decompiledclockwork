using System;
using log4net.Util;

namespace log4net.Layout
{
	// Token: 0x020000A9 RID: 169
	public class DynamicPatternLayout : PatternLayout
	{
		// Token: 0x060004F9 RID: 1273 RVA: 0x0000FE1A File Offset: 0x0000E01A
		public DynamicPatternLayout()
		{
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0000FE42 File Offset: 0x0000E042
		public DynamicPatternLayout(string pattern) : base(pattern)
		{
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x0000FE6B File Offset: 0x0000E06B
		// (set) Token: 0x060004FC RID: 1276 RVA: 0x0000FE78 File Offset: 0x0000E078
		public override string Header
		{
			get
			{
				return this.m_headerPatternString.Format();
			}
			set
			{
				base.Header = value;
				this.m_headerPatternString = new PatternString(value);
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0000FE8D File Offset: 0x0000E08D
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x0000FE9A File Offset: 0x0000E09A
		public override string Footer
		{
			get
			{
				return this.m_footerPatternString.Format();
			}
			set
			{
				base.Footer = value;
				this.m_footerPatternString = new PatternString(value);
			}
		}

		// Token: 0x04000210 RID: 528
		private PatternString m_headerPatternString = new PatternString("");

		// Token: 0x04000211 RID: 529
		private PatternString m_footerPatternString = new PatternString("");
	}
}
