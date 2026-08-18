using System;
using NLog.Config;
using NLog.Layouts;

namespace NLog.Targets
{
	// Token: 0x0200014B RID: 331
	public abstract class TargetWithLayoutHeaderAndFooter : TargetWithLayout
	{
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x0001B811 File Offset: 0x00019A11
		// (set) Token: 0x06000BD2 RID: 3026 RVA: 0x0001B820 File Offset: 0x00019A20
		[RequiredParameter]
		public override Layout Layout
		{
			get
			{
				return this.LHF.Layout;
			}
			set
			{
				if (value is LayoutWithHeaderAndFooter)
				{
					base.Layout = value;
					return;
				}
				if (this.LHF == null)
				{
					this.LHF = new LayoutWithHeaderAndFooter
					{
						Layout = value
					};
					return;
				}
				this.LHF.Layout = value;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x0001B866 File Offset: 0x00019A66
		// (set) Token: 0x06000BD4 RID: 3028 RVA: 0x0001B873 File Offset: 0x00019A73
		public Layout Footer
		{
			get
			{
				return this.LHF.Footer;
			}
			set
			{
				this.LHF.Footer = value;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x0001B881 File Offset: 0x00019A81
		// (set) Token: 0x06000BD6 RID: 3030 RVA: 0x0001B88E File Offset: 0x00019A8E
		public Layout Header
		{
			get
			{
				return this.LHF.Header;
			}
			set
			{
				this.LHF.Header = value;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x0001B89C File Offset: 0x00019A9C
		// (set) Token: 0x06000BD8 RID: 3032 RVA: 0x0001B8A9 File Offset: 0x00019AA9
		private LayoutWithHeaderAndFooter LHF
		{
			get
			{
				return (LayoutWithHeaderAndFooter)base.Layout;
			}
			set
			{
				base.Layout = value;
			}
		}
	}
}
