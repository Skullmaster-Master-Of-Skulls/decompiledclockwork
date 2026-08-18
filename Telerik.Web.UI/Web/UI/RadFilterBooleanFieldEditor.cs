using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001895 RID: 6293
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	public class RadFilterBooleanFieldEditor : RadFilterDataFieldEditor
	{
		// Token: 0x1700495F RID: 18783
		// (get) Token: 0x0600F35E RID: 62302 RVA: 0x00375F99 File Offset: 0x00374199
		// (set) Token: 0x0600F35F RID: 62303 RVA: 0x00375FA5 File Offset: 0x003741A5
		[Browsable(false)]
		public override Type DataType
		{
			get
			{
				return typeof(bool);
			}
			set
			{
				if (RadFilterTypeHelper.GetNonNullableType(value) != typeof(bool))
				{
					throw new ArgumentException("DataType must be Boolean", "value");
				}
				base.DataType = value;
			}
		}

		// Token: 0x0600F360 RID: 62304 RVA: 0x00375FD8 File Offset: 0x003741D8
		public override void InitializeEditor(Control container)
		{
			this.checkBoxControl = new CheckBox();
			this.checkBoxControl.InputAttributes["title"] = this.ToolTip;
			this.checkBoxControl.CssClass = "rfCheckBox";
			container.Controls.Add(this.checkBoxControl);
		}

		// Token: 0x0600F361 RID: 62305 RVA: 0x0037602C File Offset: 0x0037422C
		public override ArrayList ExtractValues()
		{
			return new ArrayList
			{
				this.checkBoxControl.Checked
			};
		}

		// Token: 0x0600F362 RID: 62306 RVA: 0x00376057 File Offset: 0x00374257
		public override void SetEditorValues(ArrayList values)
		{
			if (values != null && values.Count > 0)
			{
				this.checkBoxControl.Checked = (values[0] != null && (bool)values[0]);
			}
		}

		// Token: 0x0600F363 RID: 62307 RVA: 0x00376088 File Offset: 0x00374288
		internal override WebControl GetFirstInputControl(Control container)
		{
			return this.checkBoxControl;
		}

		// Token: 0x040045D2 RID: 17874
		private CheckBox checkBoxControl;
	}
}
