using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001AB0 RID: 6832
	public class DefaultSiteMapLevelSetting : SiteMapLevelSetting
	{
		// Token: 0x0601083B RID: 67643 RVA: 0x003B0411 File Offset: 0x003AE611
		public DefaultSiteMapLevelSetting()
		{
		}

		// Token: 0x0601083C RID: 67644 RVA: 0x003B0419 File Offset: 0x003AE619
		public DefaultSiteMapLevelSetting(int level) : base(level)
		{
		}

		// Token: 0x0601083D RID: 67645 RVA: 0x003B0422 File Offset: 0x003AE622
		public DefaultSiteMapLevelSetting(SiteMapLayout layout) : base(-1, layout)
		{
		}

		// Token: 0x17005040 RID: 20544
		// (get) Token: 0x0601083E RID: 67646 RVA: 0x003B042C File Offset: 0x003AE62C
		// (set) Token: 0x0601083F RID: 67647 RVA: 0x003B042F File Offset: 0x003AE62F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int Level
		{
			get
			{
				return -1;
			}
			set
			{
			}
		}
	}
}
