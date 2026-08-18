using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x020000B5 RID: 181
	public class ButtonListItem : StateManager
	{
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x0001C197 File Offset: 0x0001A397
		// (set) Token: 0x0600073B RID: 1851 RVA: 0x0001C19F File Offset: 0x0001A39F
		internal RadButtonList Parent { get; set; }

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600073C RID: 1852 RVA: 0x0001C1A8 File Offset: 0x0001A3A8
		// (remove) Token: 0x0600073D RID: 1853 RVA: 0x0001C1E0 File Offset: 0x0001A3E0
		internal event ItemSelectedEventHandler ItemSelected;

		// Token: 0x0600073E RID: 1854 RVA: 0x0001C215 File Offset: 0x0001A415
		internal void OnItemSelected(EventArgs e)
		{
			if (this.ItemSelected != null)
			{
				this.ItemSelected(this, e);
			}
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0001C22C File Offset: 0x0001A42C
		public ButtonListItem()
		{
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0001C234 File Offset: 0x0001A434
		public ButtonListItem(string text) : this()
		{
			this.Text = text;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0001C243 File Offset: 0x0001A443
		public ButtonListItem(string text, string value) : this()
		{
			this.Text = text;
			this.Value = value;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0001C259 File Offset: 0x0001A459
		public ButtonListItem(string text, string value, bool enabled) : this()
		{
			this.Text = text;
			this.Value = value;
			this.Enabled = enabled;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001C276 File Offset: 0x0001A476
		public ButtonListItem(string text, string value, bool enabled, bool selected) : this()
		{
			this.Text = text;
			this.Value = value;
			this.Enabled = enabled;
			this.Selected = selected;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001C29B File Offset: 0x0001A49B
		public ButtonListItem(string text, string value, bool enabled, bool selected, string toolTip) : this()
		{
			this.Text = text;
			this.Value = value;
			this.Enabled = enabled;
			this.Selected = selected;
			this.ToolTip = toolTip;
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x0001C2C8 File Offset: 0x0001A4C8
		// (set) Token: 0x06000746 RID: 1862 RVA: 0x0001C2DA File Offset: 0x0001A4DA
		[Localizable(true)]
		[Description("The text content of the item.")]
		[DefaultValue("")]
		[Category("Behavior")]
		public string Text
		{
			get
			{
				return base.GetViewStateValue<string>("Text", string.Empty);
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x0001C2ED File Offset: 0x0001A4ED
		// (set) Token: 0x06000748 RID: 1864 RVA: 0x0001C2FF File Offset: 0x0001A4FF
		[Category("Behavior")]
		[DefaultValue("")]
		[Description("Gets or sets the value of the list item. ")]
		public string Value
		{
			get
			{
				return base.GetViewStateValue<string>("Value", string.Empty);
			}
			set
			{
				base.ViewState["Value"] = value;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x0001C312 File Offset: 0x0001A512
		// (set) Token: 0x0600074A RID: 1866 RVA: 0x0001C320 File Offset: 0x0001A520
		[Description("Gets or sets the selected/checked state of the list item. ")]
		[DefaultValue(false)]
		[Category("Behavior")]
		public bool Selected
		{
			get
			{
				return base.GetViewStateValue<bool>("Selected", false);
			}
			set
			{
				if (value && this.Parent != null)
				{
					this.OnItemSelected(EventArgs.Empty);
				}
				base.ViewState["Selected"] = value;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x0001C34E File Offset: 0x0001A54E
		// (set) Token: 0x0600074C RID: 1868 RVA: 0x0001C35C File Offset: 0x0001A55C
		[DefaultValue(true)]
		[Description("Gets or sets the enabled state of the list item.")]
		[Category("Behavior")]
		public bool Enabled
		{
			get
			{
				return base.GetViewStateValue<bool>("Enabled", true);
			}
			set
			{
				base.ViewState["Enabled"] = value;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x0001C374 File Offset: 0x0001A574
		// (set) Token: 0x0600074E RID: 1870 RVA: 0x0001C386 File Offset: 0x0001A586
		[Description("Gets or sets the tooltip text of the list item.")]
		[DefaultValue("")]
		[Category("Behavior")]
		public string ToolTip
		{
			get
			{
				return base.GetViewStateValue<string>("ToolTip", string.Empty);
			}
			set
			{
				base.ViewState["ToolTip"] = value;
			}
		}
	}
}
