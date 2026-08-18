using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000F48 RID: 3912
	public class RibbonBarItemCollection : List<RibbonBarItem>, IRibbonBarSubComponent
	{
		// Token: 0x17002F3D RID: 12093
		// (get) Token: 0x0600953C RID: 38204 RVA: 0x00215F6F File Offset: 0x0021416F
		// (set) Token: 0x0600953D RID: 38205 RVA: 0x00215F77 File Offset: 0x00214177
		public IRibbonBarSubComponent Container { get; internal set; }

		// Token: 0x17002F3E RID: 12094
		// (get) Token: 0x0600953E RID: 38206 RVA: 0x00215F80 File Offset: 0x00214180
		public RadRibbonBar RibbonBar
		{
			get
			{
				if (this.Container == null)
				{
					return null;
				}
				return this.Container.RibbonBar;
			}
		}

		// Token: 0x17002F3F RID: 12095
		// (get) Token: 0x0600953F RID: 38207 RVA: 0x00215F97 File Offset: 0x00214197
		// (set) Token: 0x06009540 RID: 38208 RVA: 0x00215FA0 File Offset: 0x002141A0
		public WebControl ParentWebControl
		{
			get
			{
				return this._parentWebControl;
			}
			internal set
			{
				this._parentWebControl = value;
				foreach (RibbonBarItem ribbonBarItem in this)
				{
					ribbonBarItem.ParentWebControl = this._parentWebControl;
					ribbonBarItem.Group = (this._parentWebControl as RibbonBarGroup);
				}
			}
		}

		// Token: 0x06009541 RID: 38209 RVA: 0x0021600C File Offset: 0x0021420C
		public new void Add(RibbonBarItem item)
		{
			base.Add(item);
			this.OnItemAdded(item);
		}

		// Token: 0x06009542 RID: 38210 RVA: 0x0021601C File Offset: 0x0021421C
		public new void Insert(int index, RibbonBarItem item)
		{
			base.Insert(index, item);
			this.OnItemAdded(item);
		}

		// Token: 0x06009543 RID: 38211 RVA: 0x0021602D File Offset: 0x0021422D
		public new void Remove(RibbonBarItem item)
		{
			base.Remove(item);
			item.Container = null;
			item.Group = null;
		}

		// Token: 0x06009544 RID: 38212 RVA: 0x00216045 File Offset: 0x00214245
		private void OnItemAdded(RibbonBarItem item)
		{
			item.Container = this;
			if (this.ParentWebControl != null)
			{
				item.ParentWebControl = this.ParentWebControl;
				item.Group = (this.ParentWebControl as RibbonBarGroup);
			}
		}

		// Token: 0x04002AB5 RID: 10933
		private WebControl _parentWebControl;
	}
}
