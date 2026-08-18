using System;
using System.ComponentModel;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x02000367 RID: 871
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[TelerikToolboxCategory("Data")]
	public class GridMobileDateTimeColumnEditor : GridMobileColumnEditorBase
	{
		// Token: 0x06001E02 RID: 7682 RVA: 0x0005D6AF File Offset: 0x0005B8AF
		public GridMobileDateTimeColumnEditor()
		{
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x0005D6B7 File Offset: 0x0005B8B7
		public GridMobileDateTimeColumnEditor(GridDateTimeColumn owner) : base(owner)
		{
			this.owner = owner;
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06001E04 RID: 7684 RVA: 0x0005D6C7 File Offset: 0x0005B8C7
		// (set) Token: 0x06001E05 RID: 7685 RVA: 0x0005D6D0 File Offset: 0x0005B8D0
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				try
				{
					DateTime date = Convert.ToDateTime(value);
					base.Text = this.FormatDateString(date, this.owner.PickerType);
				}
				catch
				{
					base.Text = string.Empty;
				}
			}
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x0005D71C File Offset: 0x0005B91C
		public override void SetOwner(IGridEditableColumn owner)
		{
			this.owner = (owner as GridDateTimeColumn);
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x0005D72C File Offset: 0x0005B92C
		private string FormatDateString(DateTime date, GridDateTimeColumnPickerType pickerType)
		{
			if (this.owner.PickerType == GridDateTimeColumnPickerType.DatePicker)
			{
				return string.Format("{0}-{1}-{2}", date.Year, date.Month.ToString().PadLeft(2, '0'), date.Day.ToString().PadLeft(2, '0'));
			}
			if (this.owner.PickerType == GridDateTimeColumnPickerType.DateTimePicker)
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
			if (this.owner.PickerType == GridDateTimeColumnPickerType.TimePicker)
			{
				return string.Format("{0}:{1}:{2}", date.Hour.ToString().PadLeft(2, '0'), date.Minute.ToString().PadLeft(2, '0'), date.Second.ToString().PadLeft(2, '0'));
			}
			return date.ToString();
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x0005D8B4 File Offset: 0x0005BAB4
		protected override void CreateControls()
		{
			base.CreateControls();
			if (this.owner.PickerType == GridDateTimeColumnPickerType.DatePicker)
			{
				base.TextBoxControl.Attributes.Add("type", "date");
			}
			else if (this.owner.PickerType == GridDateTimeColumnPickerType.DateTimePicker)
			{
				base.TextBoxControl.Attributes.Add("type", "datetime-local");
			}
			else if (this.owner.PickerType == GridDateTimeColumnPickerType.TimePicker)
			{
				base.TextBoxControl.Attributes.Add("type", "time");
			}
			if (this.owner.PickerType != GridDateTimeColumnPickerType.None)
			{
				base.TextBoxControl.Attributes.Add("min", this.FormatDateString(this.owner.MinDate, this.owner.PickerType));
				base.TextBoxControl.Attributes.Add("max", this.FormatDateString(this.owner.MaxDate, this.owner.PickerType));
			}
		}

		// Token: 0x0400076B RID: 1899
		private GridDateTimeColumn owner;
	}
}
