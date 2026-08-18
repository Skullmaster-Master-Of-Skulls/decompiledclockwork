using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x020002F7 RID: 759
	public class ColumnValidationSettings : StateManager, IColumnValidation
	{
		// Token: 0x06001A22 RID: 6690 RVA: 0x000550F1 File Offset: 0x000532F1
		private ColumnValidationSettings()
		{
			this.Required = false;
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x00055100 File Offset: 0x00053300
		public ColumnValidationSettings(bool required)
		{
			this.Required = required;
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06001A24 RID: 6692 RVA: 0x0005510F File Offset: 0x0005330F
		// (set) Token: 0x06001A25 RID: 6693 RVA: 0x00055130 File Offset: 0x00053330
		[DefaultValue(false)]
		[Description("Value that determines whether the column is required for rendering.")]
		[Category("Behavior")]
		public bool Required
		{
			get
			{
				return (bool)(base.ViewState["Required"] ?? false);
			}
			set
			{
				base.ViewState["Required"] = value;
			}
		}
	}
}
