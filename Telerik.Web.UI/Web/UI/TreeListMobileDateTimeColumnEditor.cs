using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x0200095C RID: 2396
	[TelerikToolboxCategory("Data")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	public class TreeListMobileDateTimeColumnEditor : TreeListMobileColumnEditorBase
	{
		// Token: 0x06005B31 RID: 23345 RVA: 0x00115711 File Offset: 0x00113911
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public TreeListMobileDateTimeColumnEditor(TreeListDateTimeColumn column) : base(column)
		{
			this.InitializeMobileEditor();
		}

		// Token: 0x06005B32 RID: 23346 RVA: 0x00115720 File Offset: 0x00113920
		public override void Initialize(TreeListEditableItem editItem, Control container)
		{
			base.MobileEditor.ID = this.GenerateControlID();
			container.Controls.Add(base.MobileEditor);
		}

		// Token: 0x06005B33 RID: 23347 RVA: 0x00115744 File Offset: 0x00113944
		private string FormatDateString(DateTime date, TreeListDateTimeColumnPickerType pickerType)
		{
			TreeListDateTimeColumn treeListDateTimeColumn = base.Column as TreeListDateTimeColumn;
			if (treeListDateTimeColumn.PickerType == TreeListDateTimeColumnPickerType.DatePicker)
			{
				return string.Format("{0}-{1}-{2}", date.Year, date.Month.ToString().PadLeft(2, '0'), date.Day.ToString().PadLeft(2, '0'));
			}
			if (treeListDateTimeColumn.PickerType == TreeListDateTimeColumnPickerType.DateTimePicker)
			{
				return string.Format("{0}-{1}-{2}T{3}:{4}:{5}", new object[]
				{
					date.Year,
					date.Month.ToString().PadLeft(2, '0'),
					date.Day.ToString().PadLeft(2, '0'),
					date.Hour.ToString().PadLeft(2, '0'),
					date.Minute.ToString().PadLeft(2, '0'),
					date.Second.ToString().PadLeft(2, '0')
				});
			}
			if (treeListDateTimeColumn.PickerType == TreeListDateTimeColumnPickerType.TimePicker)
			{
				return string.Format("{0}:{1}:{2}", date.Hour.ToString().PadLeft(2, '0'), date.Minute.ToString().PadLeft(2, '0'), date.Second.ToString().PadLeft(2, '0'));
			}
			return date.ToString();
		}

		// Token: 0x06005B34 RID: 23348 RVA: 0x001158CC File Offset: 0x00113ACC
		protected override void InitializeMobileEditor()
		{
			base.MobileEditor = new TextBox();
			base.MobileEditor.Attributes.Add("type", "datetime");
			TreeListDateTimeColumn treeListDateTimeColumn = base.Column as TreeListDateTimeColumn;
			if (treeListDateTimeColumn.PickerType == TreeListDateTimeColumnPickerType.DatePicker)
			{
				base.MobileEditor.Attributes.Add("type", "date");
			}
			else if (treeListDateTimeColumn.PickerType == TreeListDateTimeColumnPickerType.DateTimePicker)
			{
				base.MobileEditor.Attributes.Add("type", "datetime-local");
			}
			else if (treeListDateTimeColumn.PickerType == TreeListDateTimeColumnPickerType.TimePicker)
			{
				base.MobileEditor.Attributes.Add("type", "time");
			}
			if (treeListDateTimeColumn.PickerType == TreeListDateTimeColumnPickerType.None)
			{
				base.MobileEditor.Attributes.Add("min", this.FormatDateString(treeListDateTimeColumn.MinDate, treeListDateTimeColumn.PickerType));
				base.MobileEditor.Attributes.Add("max", this.FormatDateString(treeListDateTimeColumn.MaxDate, treeListDateTimeColumn.PickerType));
			}
		}
	}
}
