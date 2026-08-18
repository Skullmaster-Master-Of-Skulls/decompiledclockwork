using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020011F1 RID: 4593
	public class TreeListDateTimeColumnEditor : TreeListColumnEditor
	{
		// Token: 0x0600BD97 RID: 48535 RVA: 0x0029FBC6 File Offset: 0x0029DDC6
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public TreeListDateTimeColumnEditor(TreeListEditableColumn column) : base(column)
		{
			this.InitializeDatePickerControl();
		}

		// Token: 0x0600BD98 RID: 48536 RVA: 0x0029FBD8 File Offset: 0x0029DDD8
		public override void Initialize(TreeListEditableItem editItem, Control container)
		{
			TreeListDateTimeColumn treeListDateTimeColumn = base.Column as TreeListDateTimeColumn;
			if (treeListDateTimeColumn.PickerType != TreeListDateTimeColumnPickerType.None)
			{
				this.DatePickerControl.ID = this.GenerateControlID();
				this.DatePickerControl.MinDate = treeListDateTimeColumn.MinDate;
				this.DatePickerControl.MaxDate = treeListDateTimeColumn.MaxDate;
				this.DatePickerControl.EnableEmbeddedSkins = treeListDateTimeColumn.Owner.EnableEmbeddedSkins;
				this.DatePickerControl.EnableAriaSupport = base.Column.Owner.EnableAriaSupport;
				this.DatePickerControl.PreRender += this.RadDatePickerControl_PreRender;
				if (base.Column.Owner.ResolvedRenderMode == RenderMode.Mobile)
				{
					this.DatePickerControl.ZIndex = 6000;
				}
				container.Controls.Add(this.DatePickerControl);
				if (treeListDateTimeColumn.PickerType == TreeListDateTimeColumnPickerType.TimePicker)
				{
					RadTimePicker radTimePicker = this.DatePickerControl as RadTimePicker;
					radTimePicker.TimeView.TimeFormat = treeListDateTimeColumn.GetDateTimeFormat(radTimePicker.TimeView.TimeFormat);
					radTimePicker.SharedTimeView = treeListDateTimeColumn.GetSharedTimeView();
				}
				if (treeListDateTimeColumn.PickerType == TreeListDateTimeColumnPickerType.DateTimePicker)
				{
					RadDateTimePicker radDateTimePicker = this.DatePickerControl as RadDateTimePicker;
					radDateTimePicker.TimeView.TimeFormat = treeListDateTimeColumn.GetDateTimeFormat(radDateTimePicker.TimeView.TimeFormat);
					radDateTimePicker.SharedTimeView = treeListDateTimeColumn.GetSharedTimeView();
				}
				else
				{
					this.DatePickerControl.DateInput.DateFormat = treeListDateTimeColumn.GetDateTimeFormat(this.DatePickerControl.DateInput.DateFormat);
				}
				if (!string.IsNullOrEmpty(treeListDateTimeColumn.EditDataFormatString))
				{
					this.DatePickerControl.DateInput.DateFormat = treeListDateTimeColumn.EditDataFormatString;
				}
				this.DatePickerControl.SharedCalendar = treeListDateTimeColumn.GetSharedCalendar();
				return;
			}
			this.DateInputControl = new RadDateInput();
			this.DateInputControl.ID = string.Format("RDI{0}", this.GenerateControlID());
			this.DateInputControl.MinDate = treeListDateTimeColumn.MinDate;
			this.DateInputControl.MaxDate = treeListDateTimeColumn.MaxDate;
			this.DateInputControl.EnableEmbeddedSkins = treeListDateTimeColumn.Owner.EnableEmbeddedSkins;
			this.DateInputControl.PreRender += this.DateInputControl_PreRender;
			container.Controls.Add(this.DateInputControl);
			if (string.IsNullOrEmpty(treeListDateTimeColumn.EditDataFormatString))
			{
				this.DateInputControl.DateFormat = treeListDateTimeColumn.GetDateTimeFormat(this.DateInputControl.DateFormat);
				return;
			}
			this.DateInputControl.DateFormat = treeListDateTimeColumn.EditDataFormatString;
		}

		// Token: 0x0600BD99 RID: 48537 RVA: 0x0029FE43 File Offset: 0x0029E043
		private void DateInputControl_PreRender(object sender, EventArgs e)
		{
			((ISkinnableControl)sender).Skin = base.Column.Owner.RuntimeSkin;
		}

		// Token: 0x0600BD9A RID: 48538 RVA: 0x0029FE60 File Offset: 0x0029E060
		private void RadDatePickerControl_PreRender(object sender, EventArgs e)
		{
			((ISkinnableControl)sender).Skin = base.Column.Owner.RuntimeSkin;
		}

		// Token: 0x0600BD9B RID: 48539 RVA: 0x0029FE80 File Offset: 0x0029E080
		protected virtual void InitializeDatePickerControl()
		{
			TreeListDateTimeColumn treeListDateTimeColumn = base.Column as TreeListDateTimeColumn;
			this.DatePickerControl = TreeListDateTimeColumnHelper.InstantiatePickerFactory(treeListDateTimeColumn.PickerType);
			this.DatePickerControl.RenderMode = treeListDateTimeColumn.Owner.RenderMode;
			this.DateInputControl = new RadDateInput();
			this.DateInputControl.RenderMode = treeListDateTimeColumn.Owner.RenderMode;
		}

		// Token: 0x17003D2C RID: 15660
		// (get) Token: 0x0600BD9C RID: 48540 RVA: 0x0029FEE1 File Offset: 0x0029E0E1
		// (set) Token: 0x0600BD9D RID: 48541 RVA: 0x0029FEE9 File Offset: 0x0029E0E9
		public RadDateInput DateInputControl { get; private set; }

		// Token: 0x17003D2D RID: 15661
		// (get) Token: 0x0600BD9E RID: 48542 RVA: 0x0029FEF2 File Offset: 0x0029E0F2
		// (set) Token: 0x0600BD9F RID: 48543 RVA: 0x0029FEFA File Offset: 0x0029E0FA
		public RadDatePicker DatePickerControl { get; private set; }

		// Token: 0x0600BDA0 RID: 48544 RVA: 0x0029FF04 File Offset: 0x0029E104
		public override void SetValues(IEnumerable values)
		{
			object obj = TreeListColumnEditor.GetFirstValueFromEnumerable(values);
			TreeListDateTimeColumn treeListDateTimeColumn = base.Column as TreeListDateTimeColumn;
			object namingContainer;
			if (treeListDateTimeColumn.PickerType != TreeListDateTimeColumnPickerType.None)
			{
				namingContainer = this.DatePickerControl.NamingContainer;
			}
			else
			{
				namingContainer = this.DateInputControl.NamingContainer;
			}
			if (string.IsNullOrWhiteSpace(base.Column.DefaultInsertValue) && (namingContainer is TreeListEditFormInsertItem || namingContainer is TreeListDataInsertItem) && (base.Column.Owner.IsUsingModelBinding || !string.IsNullOrWhiteSpace(base.Column.Owner.ItemType)))
			{
				obj = null;
			}
			DateTime? dateTime = null;
			if (obj != null && !(obj is DBNull))
			{
				if (obj is DateTime)
				{
					dateTime = new DateTime?((DateTime)obj);
				}
				else if (obj is IConvertible)
				{
					dateTime = new DateTime?(Convert.ToDateTime(obj));
				}
				else
				{
					dateTime = new DateTime?(DateTime.Parse(obj.ToString()));
				}
			}
			if (dateTime != null)
			{
				this.SetValueToControl(dateTime);
				return;
			}
			this.SetValueToControl(obj);
		}

		// Token: 0x0600BDA1 RID: 48545 RVA: 0x002A0128 File Offset: 0x0029E328
		public override IEnumerable GetValues()
		{
			TreeListDateTimeColumn column = base.Column as TreeListDateTimeColumn;
			if (column.PickerType != TreeListDateTimeColumnPickerType.None)
			{
				yield return this.DatePickerControl.DbSelectedDate;
			}
			yield return this.DateInputControl.DbSelectedDate;
			yield break;
		}

		// Token: 0x0600BDA2 RID: 48546 RVA: 0x002A0148 File Offset: 0x0029E348
		private void SetValueToControl(object value)
		{
			TreeListDateTimeColumn treeListDateTimeColumn = base.Column as TreeListDateTimeColumn;
			if (treeListDateTimeColumn.PickerType != TreeListDateTimeColumnPickerType.None)
			{
				this.DatePickerControl.DbSelectedDate = value;
				return;
			}
			this.DateInputControl.DbSelectedDate = value;
		}
	}
}
