using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000F43 RID: 3907
	public class RibbonBarButtonCollection : List<RibbonBarButton>, IRibbonBarSubComponent
	{
		// Token: 0x17002F2D RID: 12077
		// (get) Token: 0x06009501 RID: 38145 RVA: 0x002152FC File Offset: 0x002134FC
		// (set) Token: 0x06009502 RID: 38146 RVA: 0x00215304 File Offset: 0x00213504
		public IRibbonBarSubComponent Container { get; internal set; }

		// Token: 0x17002F2E RID: 12078
		// (get) Token: 0x06009503 RID: 38147 RVA: 0x0021530D File Offset: 0x0021350D
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

		// Token: 0x17002F2F RID: 12079
		// (get) Token: 0x06009504 RID: 38148 RVA: 0x00215324 File Offset: 0x00213524
		// (set) Token: 0x06009505 RID: 38149 RVA: 0x0021532C File Offset: 0x0021352C
		public WebControl ParentWebControl
		{
			get
			{
				return this._parentWebControl;
			}
			internal set
			{
				this._parentWebControl = value;
				foreach (RibbonBarButton ribbonBarButton in this)
				{
					ribbonBarButton.ParentWebControl = this._parentWebControl;
					IRibbonBarGroupHostedItem ribbonBarGroupHostedItem = this._parentWebControl as IRibbonBarGroupHostedItem;
					if (ribbonBarGroupHostedItem != null)
					{
						ribbonBarButton.Group = ribbonBarGroupHostedItem.Group;
					}
				}
			}
		}

		// Token: 0x06009506 RID: 38150 RVA: 0x002153A4 File Offset: 0x002135A4
		public new void Add(RibbonBarButton button)
		{
			base.Add(button);
			this.OnButtonAdd(button);
		}

		// Token: 0x06009507 RID: 38151 RVA: 0x002153B4 File Offset: 0x002135B4
		public new void Insert(int index, RibbonBarButton button)
		{
			base.Insert(index, button);
			this.OnButtonAdd(button);
		}

		// Token: 0x06009508 RID: 38152 RVA: 0x002153C5 File Offset: 0x002135C5
		public new void Remove(RibbonBarButton button)
		{
			base.Remove(button);
			button.Container = null;
			button.Group = null;
		}

		// Token: 0x06009509 RID: 38153 RVA: 0x002153E0 File Offset: 0x002135E0
		private void OnButtonAdd(RibbonBarButton button)
		{
			button.Container = this;
			if (this.Container != null)
			{
				IRibbonBarGroupHostedItem ribbonBarGroupHostedItem = this.Container.ParentWebControl as IRibbonBarGroupHostedItem;
				if (ribbonBarGroupHostedItem != null)
				{
					button.Group = ribbonBarGroupHostedItem.Group;
				}
			}
		}

		// Token: 0x04002AA3 RID: 10915
		private WebControl _parentWebControl;
	}
}
