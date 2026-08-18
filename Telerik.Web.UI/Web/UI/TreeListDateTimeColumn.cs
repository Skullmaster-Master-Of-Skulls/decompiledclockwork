using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001203 RID: 4611
	public class TreeListDateTimeColumn : TreeListBoundColumn
	{
		// Token: 0x0600BE87 RID: 48775 RVA: 0x002A347C File Offset: 0x002A167C
		internal string GetDateTimeFormat(string format)
		{
			if (!string.IsNullOrEmpty(this.DataFormatString))
			{
				return format;
			}
			if (this.PickerType == TreeListDateTimeColumnPickerType.DatePicker)
			{
				return "d";
			}
			if (this.PickerType == TreeListDateTimeColumnPickerType.TimePicker)
			{
				return "t";
			}
			return "G";
		}

		// Token: 0x17003D76 RID: 15734
		// (get) Token: 0x0600BE88 RID: 48776 RVA: 0x002A34B0 File Offset: 0x002A16B0
		// (set) Token: 0x0600BE89 RID: 48777 RVA: 0x002A34D0 File Offset: 0x002A16D0
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[Description("TreeListDateTimeColumn_EditDataFormatString")]
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string EditDataFormatString
		{
			get
			{
				return (string)(base.ViewState["EditDataFormatString"] ?? string.Empty);
			}
			set
			{
				base.ViewState["EditDataFormatString"] = value;
			}
		}

		// Token: 0x17003D77 RID: 15735
		// (get) Token: 0x0600BE8A RID: 48778 RVA: 0x002A34E4 File Offset: 0x002A16E4
		// (set) Token: 0x0600BE8B RID: 48779 RVA: 0x002A3512 File Offset: 0x002A1712
		[DefaultValue(typeof(TreeListDateTimeColumnPickerType), "DatePicker")]
		[NotifyParentProperty(true)]
		public TreeListDateTimeColumnPickerType PickerType
		{
			get
			{
				object obj = base.ViewState["PickerType"];
				if (obj == null)
				{
					obj = TreeListDateTimeColumnPickerType.DatePicker;
				}
				return (TreeListDateTimeColumnPickerType)obj;
			}
			set
			{
				base.ViewState["PickerType"] = value;
			}
		}

		// Token: 0x17003D78 RID: 15736
		// (get) Token: 0x0600BE8C RID: 48780 RVA: 0x002A352C File Offset: 0x002A172C
		// (set) Token: 0x0600BE8D RID: 48781 RVA: 0x002A355E File Offset: 0x002A175E
		[DefaultValue(typeof(DateTime), "1/1/1900")]
		[NotifyParentProperty(true)]
		public DateTime MinDate
		{
			get
			{
				object obj = base.ViewState["MinDate"] ?? TreeListDateTimeColumnHelper.DefaultMinDateTimeValue;
				return (DateTime)obj;
			}
			set
			{
				base.ViewState["MinDate"] = value;
			}
		}

		// Token: 0x17003D79 RID: 15737
		// (get) Token: 0x0600BE8E RID: 48782 RVA: 0x002A3578 File Offset: 0x002A1778
		// (set) Token: 0x0600BE8F RID: 48783 RVA: 0x002A35AA File Offset: 0x002A17AA
		[DefaultValue(typeof(DateTime), "12/31/2099")]
		[NotifyParentProperty(true)]
		public DateTime MaxDate
		{
			get
			{
				object obj = base.ViewState["MaxDate"] ?? TreeListDateTimeColumnHelper.DefaultMaxDateTimeValue;
				return (DateTime)obj;
			}
			set
			{
				base.ViewState["MaxDate"] = value;
			}
		}

		// Token: 0x0600BE90 RID: 48784 RVA: 0x002A35C4 File Offset: 0x002A17C4
		internal RadTimeView GetSharedTimeView()
		{
			RadTimeView radTimeView = base.Owner.FindControl(TreeListDateTimeColumn._sharedTimeViewName) as RadTimeView;
			if (radTimeView == null)
			{
				Panel panel = new Panel();
				panel.ID = "SharedTimeViewContainer";
				base.Owner.Controls.Add(panel);
				radTimeView = new RadTimeView();
				radTimeView.ID = TreeListDateTimeColumn._sharedTimeViewName;
				panel.Controls.Add(radTimeView);
				radTimeView.EnableEmbeddedSkins = base.Owner.EnableEmbeddedSkins;
				radTimeView.EnableAriaSupport = base.Owner.EnableAriaSupport;
				radTimeView.PreRender += this.sharedTimeView_PreRender;
				panel.Style["display"] = "none";
				radTimeView.Visible = !base.Owner.IsDesignMode;
			}
			return radTimeView;
		}

		// Token: 0x0600BE91 RID: 48785 RVA: 0x002A368A File Offset: 0x002A188A
		private void sharedTimeView_PreRender(object sender, EventArgs e)
		{
			((RadTimeView)sender).Skin = base.Owner.RuntimeSkin;
		}

		// Token: 0x0600BE92 RID: 48786 RVA: 0x002A36A4 File Offset: 0x002A18A4
		internal RadCalendar GetSharedCalendar()
		{
			RadCalendar radCalendar = base.Owner.FindControl(TreeListDateTimeColumn._sharedCalendarName) as RadCalendar;
			if (radCalendar == null)
			{
				Panel panel = new Panel();
				panel.ID = "SharedCalendarContainer";
				base.Owner.Controls.Add(panel);
				radCalendar = new RadCalendar();
				radCalendar.ID = TreeListDateTimeColumn._sharedCalendarName;
				panel.Controls.Add(radCalendar);
				radCalendar.RangeMinDate = this.MinDate;
				radCalendar.RangeMaxDate = this.MaxDate;
				radCalendar.EnableEmbeddedSkins = base.Owner.EnableEmbeddedSkins;
				radCalendar.EnableAriaSupport = base.Owner.EnableAriaSupport;
				radCalendar.PreRender += this.sharedCalendar_PreRender;
				radCalendar.RenderMode = base.Owner.RenderMode;
				panel.Style["display"] = "none";
				radCalendar.Visible = !base.Owner.IsDesignMode;
			}
			return radCalendar;
		}

		// Token: 0x0600BE93 RID: 48787 RVA: 0x002A3793 File Offset: 0x002A1993
		private void sharedCalendar_PreRender(object sender, EventArgs e)
		{
			((RadCalendar)sender).Skin = base.Owner.RuntimeSkin;
		}

		// Token: 0x0600BE94 RID: 48788 RVA: 0x002A37AB File Offset: 0x002A19AB
		public override ITreeListColumnEditor CreateDefaultColumnEditor()
		{
			if (base.Owner.ResolvedRenderMode == RenderMode.Mobile && base.UseNativeEditorsInMobileMode)
			{
				return new TreeListMobileDateTimeColumnEditor(this);
			}
			return new TreeListDateTimeColumnEditor(this);
		}

		// Token: 0x04003215 RID: 12821
		private static readonly string _sharedTimeViewName = "TreeListDateTimeColumnSharedTimeView";

		// Token: 0x04003216 RID: 12822
		private static readonly string _sharedCalendarName = "TreeListDateTimeColumnSharedSharedCalendar";
	}
}
