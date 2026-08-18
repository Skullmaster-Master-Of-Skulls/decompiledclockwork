using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020007D1 RID: 2001
	public abstract class RibbonBarCollectionBase<T> : List<T>, IRibbonBarSubComponent where T : RibbonBarCollectionItemBase
	{
		// Token: 0x1700168A RID: 5770
		// (get) Token: 0x060045CF RID: 17871 RVA: 0x000DBAF4 File Offset: 0x000D9CF4
		// (set) Token: 0x060045D0 RID: 17872 RVA: 0x000DBAFC File Offset: 0x000D9CFC
		public IRibbonBarSubComponent Container { get; internal set; }

		// Token: 0x1700168B RID: 5771
		// (get) Token: 0x060045D1 RID: 17873 RVA: 0x000DBB05 File Offset: 0x000D9D05
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

		// Token: 0x1700168C RID: 5772
		// (get) Token: 0x060045D2 RID: 17874 RVA: 0x000DBB1C File Offset: 0x000D9D1C
		// (set) Token: 0x060045D3 RID: 17875 RVA: 0x000DBB24 File Offset: 0x000D9D24
		public WebControl ParentWebControl
		{
			get
			{
				return this._parentWebControl;
			}
			internal set
			{
				this._parentWebControl = value;
				foreach (T t in this)
				{
					t.ParentWebControl = this._parentWebControl;
				}
			}
		}

		// Token: 0x060045D4 RID: 17876 RVA: 0x000DBB88 File Offset: 0x000D9D88
		public new void Add(T item)
		{
			base.Add(item);
			this.OnCategoryAdd(item);
		}

		// Token: 0x060045D5 RID: 17877 RVA: 0x000DBB98 File Offset: 0x000D9D98
		public new void Insert(int index, T item)
		{
			base.Insert(index, item);
			this.OnCategoryAdd(item);
		}

		// Token: 0x060045D6 RID: 17878 RVA: 0x000DBBA9 File Offset: 0x000D9DA9
		public new void Remove(T item)
		{
			base.Remove(item);
			item.Container = null;
		}

		// Token: 0x060045D7 RID: 17879 RVA: 0x000DBBC1 File Offset: 0x000D9DC1
		private void OnCategoryAdd(T item)
		{
			item.Container = this;
			if (this.ParentWebControl != null)
			{
				item.ParentWebControl = this.ParentWebControl;
			}
		}

		// Token: 0x04001212 RID: 4626
		private WebControl _parentWebControl;
	}
}
