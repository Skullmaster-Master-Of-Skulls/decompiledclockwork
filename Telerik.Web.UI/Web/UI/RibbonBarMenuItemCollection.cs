using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000F49 RID: 3913
	public class RibbonBarMenuItemCollection : List<RibbonBarMenuItem>, IRibbonBarSubComponent
	{
		// Token: 0x17002F40 RID: 12096
		// (get) Token: 0x06009546 RID: 38214 RVA: 0x0021607B File Offset: 0x0021427B
		// (set) Token: 0x06009547 RID: 38215 RVA: 0x00216083 File Offset: 0x00214283
		public IRibbonBarSubComponent Container { get; internal set; }

		// Token: 0x17002F41 RID: 12097
		// (get) Token: 0x06009548 RID: 38216 RVA: 0x0021608C File Offset: 0x0021428C
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

		// Token: 0x17002F42 RID: 12098
		// (get) Token: 0x06009549 RID: 38217 RVA: 0x002160A3 File Offset: 0x002142A3
		// (set) Token: 0x0600954A RID: 38218 RVA: 0x002160AC File Offset: 0x002142AC
		public WebControl ParentWebControl
		{
			get
			{
				return this._parentWebControl;
			}
			internal set
			{
				this._parentWebControl = value;
				foreach (RibbonBarMenuItem ribbonBarMenuItem in this)
				{
					ribbonBarMenuItem.ParentWebControl = this._parentWebControl;
				}
			}
		}

		// Token: 0x0600954B RID: 38219 RVA: 0x00216108 File Offset: 0x00214308
		public new void Add(RibbonBarMenuItem item)
		{
			base.Add(item);
			this.OnItemAdded(item);
		}

		// Token: 0x0600954C RID: 38220 RVA: 0x00216118 File Offset: 0x00214318
		public new void Insert(int index, RibbonBarMenuItem item)
		{
			base.Insert(index, item);
			this.OnItemAdded(item);
		}

		// Token: 0x0600954D RID: 38221 RVA: 0x00216129 File Offset: 0x00214329
		public new void Remove(RibbonBarMenuItem item)
		{
			base.Remove(item);
			item.Container = null;
		}

		// Token: 0x0600954E RID: 38222 RVA: 0x0021613A File Offset: 0x0021433A
		private void OnItemAdded(RibbonBarMenuItem item)
		{
			item.Container = this;
			if (this.ParentWebControl != null)
			{
				item.ParentWebControl = this.ParentWebControl;
			}
		}

		// Token: 0x04002AB7 RID: 10935
		private WebControl _parentWebControl;
	}
}
