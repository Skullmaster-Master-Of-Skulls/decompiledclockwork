using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000F47 RID: 3911
	public class RibbonBarGroupCollection : List<RibbonBarGroup>, IRibbonBarSubComponent
	{
		// Token: 0x17002F3A RID: 12090
		// (get) Token: 0x06009531 RID: 38193 RVA: 0x00215E3C File Offset: 0x0021403C
		// (set) Token: 0x06009532 RID: 38194 RVA: 0x00215E44 File Offset: 0x00214044
		public IRibbonBarSubComponent Container { get; internal set; }

		// Token: 0x17002F3B RID: 12091
		// (get) Token: 0x06009533 RID: 38195 RVA: 0x00215E4D File Offset: 0x0021404D
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

		// Token: 0x17002F3C RID: 12092
		// (get) Token: 0x06009534 RID: 38196 RVA: 0x00215E64 File Offset: 0x00214064
		// (set) Token: 0x06009535 RID: 38197 RVA: 0x00215E6C File Offset: 0x0021406C
		public WebControl ParentWebControl
		{
			get
			{
				return this._parentWebControl;
			}
			internal set
			{
				this._parentWebControl = value;
				foreach (RibbonBarGroup ribbonBarGroup in this)
				{
					ribbonBarGroup.ParentWebControl = this._parentWebControl;
				}
			}
		}

		// Token: 0x06009536 RID: 38198 RVA: 0x00215EC8 File Offset: 0x002140C8
		public new void Add(RibbonBarGroup group)
		{
			base.Add(group);
			this.OnGroupAdd(group);
		}

		// Token: 0x06009537 RID: 38199 RVA: 0x00215ED8 File Offset: 0x002140D8
		public new void AddRange(IEnumerable<RibbonBarGroup> collection)
		{
			base.AddRange(collection);
			foreach (RibbonBarGroup group in collection)
			{
				this.OnGroupAdd(group);
			}
		}

		// Token: 0x06009538 RID: 38200 RVA: 0x00215F28 File Offset: 0x00214128
		public new void Insert(int index, RibbonBarGroup group)
		{
			base.Insert(index, group);
			this.OnGroupAdd(group);
		}

		// Token: 0x06009539 RID: 38201 RVA: 0x00215F39 File Offset: 0x00214139
		public new void Remove(RibbonBarGroup group)
		{
			base.Remove(group);
			group.Container = null;
		}

		// Token: 0x0600953A RID: 38202 RVA: 0x00215F4A File Offset: 0x0021414A
		private void OnGroupAdd(RibbonBarGroup group)
		{
			group.Container = this;
			if (this.ParentWebControl != null)
			{
				group.ParentWebControl = this.ParentWebControl;
			}
		}

		// Token: 0x04002AB3 RID: 10931
		private WebControl _parentWebControl;
	}
}
