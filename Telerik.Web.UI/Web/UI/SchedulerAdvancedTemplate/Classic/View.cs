using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SchedulerAdvancedTemplate.Classic
{
	// Token: 0x02000817 RID: 2071
	internal class View : ViewBase
	{
		// Token: 0x170018F8 RID: 6392
		// (get) Token: 0x06004C69 RID: 19561 RVA: 0x000EEF88 File Offset: 0x000ED188
		public override string SelectedTimeZone
		{
			get
			{
				RadComboBox radComboBox = base.TimeZones as RadComboBox;
				if (radComboBox != null)
				{
					return radComboBox.SelectedValue.ToString();
				}
				return base.ParentScheduler.TimeZoneID;
			}
		}

		// Token: 0x170018F9 RID: 6393
		// (get) Token: 0x06004C6A RID: 19562 RVA: 0x000EEFBC File Offset: 0x000ED1BC
		public override string SelectedReminder
		{
			get
			{
				RadComboBox radComboBox = base.Reminder as RadComboBox;
				return radComboBox.SelectedValue;
			}
		}

		// Token: 0x06004C6B RID: 19563 RVA: 0x000EEFDB File Offset: 0x000ED1DB
		public View(AdvancedTemplate owner) : base(owner)
		{
		}

		// Token: 0x06004C6C RID: 19564 RVA: 0x000EEFE4 File Offset: 0x000ED1E4
		protected override WebControl CreateSubjectTextBox()
		{
			WebControl webControl = base.CreateSubjectTextBox();
			((RadTextBox)webControl).Rows = 5;
			return webControl;
		}

		// Token: 0x06004C6D RID: 19565 RVA: 0x000EF008 File Offset: 0x000ED208
		protected override Control CreateDatePicker(string id)
		{
			RadDatePicker radDatePicker = base.CreateDatePicker(id) as RadDatePicker;
			radDatePicker.Width = 83;
			radDatePicker.DatePopupButton.Visible = false;
			return radDatePicker;
		}

		// Token: 0x06004C6E RID: 19566 RVA: 0x000EF03C File Offset: 0x000ED23C
		protected override Control CreateTimePicker(string id)
		{
			RadTimePicker radTimePicker = base.CreateTimePicker(id) as RadTimePicker;
			radTimePicker.Width = 85;
			radTimePicker.TimePopupButton.Visible = false;
			return radTimePicker;
		}

		// Token: 0x06004C6F RID: 19567 RVA: 0x000EF070 File Offset: 0x000ED270
		protected override DataBoundControl CreateTimeZonesControl(string id)
		{
			RadComboBox radComboBox = this.CreateComboBox(id);
			radComboBox.Width = Unit.Pixel(230);
			this.PopulateTimeZones(radComboBox);
			return radComboBox;
		}

		// Token: 0x06004C70 RID: 19568 RVA: 0x000EF0A0 File Offset: 0x000ED2A0
		private void PopulateTimeZones(RadComboBox comboBox)
		{
			comboBox.DataSource = base.ParentScheduler.TimeZonesProvider.GetAllTimeZones();
			comboBox.DataTextField = "DisplayName";
			comboBox.DataValueField = "Id";
			comboBox.DataBind();
			comboBox.SelectedValue = (string.IsNullOrEmpty(base.Appointment.TimeZoneID) ? "UTC" : base.Appointment.TimeZoneID);
		}

		// Token: 0x06004C71 RID: 19569 RVA: 0x000EF10C File Offset: 0x000ED30C
		protected override DataBoundControl CreateReminderControl(string id)
		{
			RadComboBox radComboBox = this.CreateComboBox(id);
			radComboBox.Width = Unit.Pixel(120);
			this.PopulateReminderValues(radComboBox);
			return radComboBox;
		}

		// Token: 0x06004C72 RID: 19570 RVA: 0x000EF138 File Offset: 0x000ED338
		private void PopulateReminderValues(RadComboBox comboBox)
		{
			comboBox.Items.AddRange(new RadComboBoxItem[]
			{
				new RadComboBoxItem(base.Localization.ReminderNone, string.Empty),
				new RadComboBoxItem("0 " + base.Localization.ReminderMinutes, "0"),
				new RadComboBoxItem("5 " + base.Localization.ReminderMinutes, "5"),
				new RadComboBoxItem("10 " + base.Localization.ReminderMinutes, "10"),
				new RadComboBoxItem("15 " + base.Localization.ReminderMinutes, "15"),
				new RadComboBoxItem("30 " + base.Localization.ReminderMinutes, "30"),
				new RadComboBoxItem("1 " + base.Localization.ReminderHour, "60"),
				new RadComboBoxItem("2 " + base.Localization.ReminderHours, "120"),
				new RadComboBoxItem("3 " + base.Localization.ReminderHours, "180"),
				new RadComboBoxItem("4 " + base.Localization.ReminderHours, "240"),
				new RadComboBoxItem("5 " + base.Localization.ReminderHours, "300"),
				new RadComboBoxItem("6 " + base.Localization.ReminderHours, "360"),
				new RadComboBoxItem("7 " + base.Localization.ReminderHours, "420"),
				new RadComboBoxItem("8 " + base.Localization.ReminderHours, "480"),
				new RadComboBoxItem("9 " + base.Localization.ReminderHours, "540"),
				new RadComboBoxItem("10 " + base.Localization.ReminderHours, "600"),
				new RadComboBoxItem("11 " + base.Localization.ReminderHours, "660"),
				new RadComboBoxItem("12 " + base.Localization.ReminderHours, "720"),
				new RadComboBoxItem("18 " + base.Localization.ReminderHours, "1080"),
				new RadComboBoxItem("1 " + base.Localization.ReminderDays, "1440"),
				new RadComboBoxItem("2 " + base.Localization.ReminderDays, "2880"),
				new RadComboBoxItem("3 " + base.Localization.ReminderDays, "4320"),
				new RadComboBoxItem("4 " + base.Localization.ReminderDays, "5760"),
				new RadComboBoxItem("1 " + base.Localization.ReminderWeek, "10080"),
				new RadComboBoxItem("2 " + base.Localization.ReminderWeeks, "20160")
			});
		}

		// Token: 0x06004C73 RID: 19571 RVA: 0x000EF4B1 File Offset: 0x000ED6B1
		protected override DataBoundControl CreateResourceControl()
		{
			return this.CreateComboBox("");
		}

		// Token: 0x06004C74 RID: 19572 RVA: 0x000EF4C0 File Offset: 0x000ED6C0
		protected override void PopulateResourceControl(DataBoundControl resourceControl, string resType, bool addNullValue)
		{
			RadComboBox radComboBox = resourceControl as RadComboBox;
			if (addNullValue)
			{
				radComboBox.Items.Add(new RadComboBoxItem("-", "NULL"));
			}
			foreach (Resource resource in base.GetResources(resType))
			{
				radComboBox.Items.Add(new RadComboBoxItem(resource.Text, LosSerializer.Serialize(resource.Key)));
			}
		}

		// Token: 0x06004C75 RID: 19573 RVA: 0x000EF54C File Offset: 0x000ED74C
		private RadComboBox CreateComboBox(string id = "")
		{
			RadComboBox radComboBox = new RadComboBox
			{
				ID = id,
				EnableEmbeddedSkins = base.ParentScheduler.EnableEmbeddedSkins,
				EnableEmbeddedScripts = base.ParentScheduler.EnableEmbeddedScripts,
				RenderMode = base.ParentScheduler.ResolvedRenderMode,
				ZIndex = base.ParentScheduler.AdvancedForm.ZIndex + 20
			};
			if (radComboBox.RuntimeSkin != base.Owner._runtimeSkin)
			{
				radComboBox.Skin = base.Owner._runtimeSkin;
			}
			return radComboBox;
		}

		// Token: 0x06004C76 RID: 19574 RVA: 0x000EF5E0 File Offset: 0x000ED7E0
		protected override void Reminder_DataBinding(object sender, EventArgs e)
		{
			if (base.Appointment.Reminders.Count > 0)
			{
				RadComboBox radComboBox = base.Reminder as RadComboBox;
				string value = ((int)base.Appointment.Reminders[0].Trigger.TotalMinutes).ToString();
				RadComboBoxItem radComboBoxItem = radComboBox.Items.FindItemByValue(value);
				if (radComboBoxItem != null)
				{
					radComboBoxItem.Selected = true;
				}
			}
		}

		// Token: 0x06004C77 RID: 19575 RVA: 0x000EF64C File Offset: 0x000ED84C
		protected override void ResourceControl_DataBound(object sender, EventArgs e)
		{
			DataBoundControl dataBoundControl = (DataBoundControl)sender;
			string text = dataBoundControl.ID.Substring(3);
			ResourceType resourceType = base.ParentScheduler.ResourceTypes.FindByName(text);
			if (resourceType.AllowMultipleValues)
			{
				SchedulerCheckBoxList schedulerCheckBoxList = (SchedulerCheckBoxList)sender;
				using (IEnumerator<Resource> enumerator = base.Appointment.Resources.GetResourcesByType(text).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Resource resource = enumerator.Current;
						schedulerCheckBoxList.Items.FindByValue(LosSerializer.Serialize(resource.Key)).Selected = true;
					}
					return;
				}
			}
			Resource resourceByType = base.Appointment.Resources.GetResourceByType(text);
			if (resourceByType != null)
			{
				RadComboBox radComboBox = (RadComboBox)sender;
				radComboBox.SelectedValue = LosSerializer.Serialize(resourceByType.Key);
			}
		}

		// Token: 0x06004C78 RID: 19576 RVA: 0x000EF730 File Offset: 0x000ED930
		public override void ExtractResourceValues(IDictionary target)
		{
			foreach (string key in base.ResourceControls.Keys)
			{
				ArrayList arrayList = new ArrayList();
				SchedulerCheckBoxList schedulerCheckBoxList = base.ResourceControls[key] as SchedulerCheckBoxList;
				if (schedulerCheckBoxList != null)
				{
					foreach (object obj in schedulerCheckBoxList.Items)
					{
						ListItem listItem = (ListItem)obj;
						if (listItem.Selected && listItem.Value != "NULL")
						{
							arrayList.Add(LosSerializer.Deserialize(listItem.Value));
						}
					}
				}
				RadComboBox radComboBox = base.ResourceControls[key] as RadComboBox;
				if (radComboBox != null)
				{
					foreach (object obj2 in radComboBox.Items)
					{
						RadComboBoxItem radComboBoxItem = (RadComboBoxItem)obj2;
						if (radComboBoxItem.Selected && radComboBoxItem.Value != "NULL")
						{
							arrayList.Add(LosSerializer.Deserialize(radComboBoxItem.Value));
						}
					}
				}
				switch (arrayList.Count)
				{
				case 0:
					target[key] = string.Empty;
					break;
				case 1:
					target[key] = arrayList[0];
					break;
				default:
					target[key] = arrayList.ToArray();
					break;
				}
			}
		}

		// Token: 0x0400133A RID: 4922
		private const int ComboBoxesZIndexStep = 20;
	}
}
