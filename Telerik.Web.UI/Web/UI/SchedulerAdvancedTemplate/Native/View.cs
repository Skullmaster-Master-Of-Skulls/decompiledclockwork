using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Scheduling;

namespace Telerik.Web.UI.SchedulerAdvancedTemplate.Native
{
	// Token: 0x0200081A RID: 2074
	internal class View : ViewBase
	{
		// Token: 0x170018FC RID: 6396
		// (get) Token: 0x06004C89 RID: 19593 RVA: 0x000F0264 File Offset: 0x000EE464
		public override DateTime StartDateValue
		{
			get
			{
				return DateTime.Parse((base.StartDate as GenericHtmlInputControl).Value);
			}
		}

		// Token: 0x170018FD RID: 6397
		// (get) Token: 0x06004C8A RID: 19594 RVA: 0x000F027B File Offset: 0x000EE47B
		public override TimeSpan StartTimeValue
		{
			get
			{
				return TimeSpan.Parse((base.StartTime as GenericHtmlInputControl).Value);
			}
		}

		// Token: 0x170018FE RID: 6398
		// (get) Token: 0x06004C8B RID: 19595 RVA: 0x000F0292 File Offset: 0x000EE492
		public override DateTime EndDateValue
		{
			get
			{
				return DateTime.Parse((base.EndDate as GenericHtmlInputControl).Value);
			}
		}

		// Token: 0x170018FF RID: 6399
		// (get) Token: 0x06004C8C RID: 19596 RVA: 0x000F02A9 File Offset: 0x000EE4A9
		public override TimeSpan EndTimeValue
		{
			get
			{
				return TimeSpan.Parse((base.EndTime as GenericHtmlInputControl).Value);
			}
		}

		// Token: 0x17001900 RID: 6400
		// (get) Token: 0x06004C8D RID: 19597 RVA: 0x000F02C0 File Offset: 0x000EE4C0
		public override string SelectedTimeZone
		{
			get
			{
				DropDownList dropDownList = base.TimeZones as DropDownList;
				if (dropDownList != null)
				{
					return dropDownList.SelectedValue.ToString();
				}
				return base.ParentScheduler.TimeZoneID;
			}
		}

		// Token: 0x17001901 RID: 6401
		// (get) Token: 0x06004C8E RID: 19598 RVA: 0x000F02F4 File Offset: 0x000EE4F4
		public override string SelectedReminder
		{
			get
			{
				DropDownList dropDownList = base.Reminder as DropDownList;
				return dropDownList.SelectedValue;
			}
		}

		// Token: 0x17001902 RID: 6402
		// (get) Token: 0x06004C8F RID: 19599 RVA: 0x000F0313 File Offset: 0x000EE513
		public override string SubjectText
		{
			get
			{
				return (base.Subject as TextBox).Text;
			}
		}

		// Token: 0x17001903 RID: 6403
		// (get) Token: 0x06004C90 RID: 19600 RVA: 0x000F0325 File Offset: 0x000EE525
		public override string DescriptionText
		{
			get
			{
				return (base.Description as TextBox).Text;
			}
		}

		// Token: 0x17001904 RID: 6404
		// (get) Token: 0x06004C91 RID: 19601 RVA: 0x000F0337 File Offset: 0x000EE537
		// (set) Token: 0x06004C92 RID: 19602 RVA: 0x000F033F File Offset: 0x000EE53F
		public LinkButton DeleteButton { get; set; }

		// Token: 0x06004C93 RID: 19603 RVA: 0x000F0348 File Offset: 0x000EE548
		public View(AdvancedTemplate owner) : base(owner)
		{
		}

		// Token: 0x06004C94 RID: 19604 RVA: 0x000F0351 File Offset: 0x000EE551
		protected override void CreateAppointmentRecurrenceControls()
		{
			base.CreateAppointmentRecurrenceControls();
			base.ResetExceptions.CssClass = "rsButton rfbFull";
		}

		// Token: 0x06004C95 RID: 19605 RVA: 0x000F0388 File Offset: 0x000EE588
		protected override WebControl CreateSubjectTextBox()
		{
			TextBox textBox = new TextBox
			{
				ID = "Subject",
				TextMode = (base.ParentScheduler.HasDescriptionField ? TextBoxMode.SingleLine : TextBoxMode.MultiLine),
				Rows = 5,
				Columns = 50,
				CssClass = "rfbFull"
			};
			textBox.DataBinding += delegate(object sender, EventArgs e)
			{
				((TextBox)sender).Text = base.Owner.Appointment.Subject;
			};
			return textBox;
		}

		// Token: 0x06004C96 RID: 19606 RVA: 0x000F03EC File Offset: 0x000EE5EC
		protected override DataBoundControl CreateTimeZonesControl(string id)
		{
			DropDownList dropDownList = this.CreateDropDownList(id);
			this.PopulateTimeZones(dropDownList);
			return dropDownList;
		}

		// Token: 0x06004C97 RID: 19607 RVA: 0x000F040C File Offset: 0x000EE60C
		private void PopulateTimeZones(DropDownList dropDownList)
		{
			dropDownList.DataSource = base.ParentScheduler.TimeZonesProvider.GetAllTimeZones();
			dropDownList.DataTextField = "DisplayName";
			dropDownList.DataValueField = "Id";
			dropDownList.DataBind();
			dropDownList.SelectedValue = (string.IsNullOrEmpty(base.Appointment.TimeZoneID) ? "UTC" : base.Appointment.TimeZoneID);
		}

		// Token: 0x06004C98 RID: 19608 RVA: 0x000F0478 File Offset: 0x000EE678
		protected override DataBoundControl CreateReminderControl(string id)
		{
			DropDownList dropDownList = this.CreateDropDownList(id);
			this.PopulateReminderValues(dropDownList);
			return dropDownList;
		}

		// Token: 0x06004C99 RID: 19609 RVA: 0x000F0498 File Offset: 0x000EE698
		private void PopulateReminderValues(DropDownList dropDownList)
		{
			dropDownList.Items.AddRange(new ListItem[]
			{
				new ListItem(base.Localization.ReminderNone, string.Empty),
				new ListItem("0 " + base.Localization.ReminderMinutes, "0"),
				new ListItem("5 " + base.Localization.ReminderMinutes, "5"),
				new ListItem("10 " + base.Localization.ReminderMinutes, "10"),
				new ListItem("15 " + base.Localization.ReminderMinutes, "15"),
				new ListItem("30 " + base.Localization.ReminderMinutes, "30"),
				new ListItem("1 " + base.Localization.ReminderHour, "60"),
				new ListItem("2 " + base.Localization.ReminderHours, "120"),
				new ListItem("3 " + base.Localization.ReminderHours, "180"),
				new ListItem("4 " + base.Localization.ReminderHours, "240"),
				new ListItem("5 " + base.Localization.ReminderHours, "300"),
				new ListItem("6 " + base.Localization.ReminderHours, "360"),
				new ListItem("7 " + base.Localization.ReminderHours, "420"),
				new ListItem("8 " + base.Localization.ReminderHours, "480"),
				new ListItem("9 " + base.Localization.ReminderHours, "540"),
				new ListItem("10 " + base.Localization.ReminderHours, "600"),
				new ListItem("11 " + base.Localization.ReminderHours, "660"),
				new ListItem("12 " + base.Localization.ReminderHours, "720"),
				new ListItem("18 " + base.Localization.ReminderHours, "1080"),
				new ListItem("1 " + base.Localization.ReminderDays, "1440"),
				new ListItem("2 " + base.Localization.ReminderDays, "2880"),
				new ListItem("3 " + base.Localization.ReminderDays, "4320"),
				new ListItem("4 " + base.Localization.ReminderDays, "5760"),
				new ListItem("1 " + base.Localization.ReminderWeek, "10080"),
				new ListItem("2 " + base.Localization.ReminderWeeks, "20160")
			});
		}

		// Token: 0x06004C9A RID: 19610 RVA: 0x000F0814 File Offset: 0x000EEA14
		protected override WebControl CreateAttributeTextBox(string id)
		{
			return new TextBox
			{
				ID = id,
				CssClass = "rfbFull"
			};
		}

		// Token: 0x06004C9B RID: 19611 RVA: 0x000F0854 File Offset: 0x000EEA54
		protected override WebControl CreateDescriptionTextBox()
		{
			TextBox textBox = new TextBox
			{
				ID = "Description",
				TextMode = TextBoxMode.MultiLine,
				Rows = 5,
				Columns = 50,
				CssClass = "rfbFull"
			};
			textBox.DataBinding += delegate(object sender, EventArgs e)
			{
				((TextBox)sender).Text = base.Appointment.Description;
			};
			return textBox;
		}

		// Token: 0x06004C9C RID: 19612 RVA: 0x000F08A8 File Offset: 0x000EEAA8
		protected override DataBoundControl CreateResourceControl()
		{
			return new DropDownList
			{
				CssClass = "rfbFull"
			};
		}

		// Token: 0x06004C9D RID: 19613 RVA: 0x000F08C8 File Offset: 0x000EEAC8
		protected override void PopulateResourceControl(DataBoundControl resourceControl, string resType, bool addNullValue)
		{
			DropDownList dropDownList = resourceControl as DropDownList;
			if (addNullValue)
			{
				dropDownList.Items.Add(new ListItem("-", "NULL"));
			}
			foreach (Resource resource in base.GetResources(resType))
			{
				dropDownList.Items.Add(new ListItem(resource.Text, LosSerializer.Serialize(resource.Key)));
			}
		}

		// Token: 0x06004C9E RID: 19614 RVA: 0x000F0954 File Offset: 0x000EEB54
		protected override Control CreateDatePicker(string id)
		{
			GenericHtmlInputControl genericHtmlInputControl = new GenericHtmlInputControl("date")
			{
				ID = id
			};
			string value = string.Format("{0} {1}", "rsAdvDatePicker", "rfbLarge");
			genericHtmlInputControl.Attributes.Add("class", value);
			return genericHtmlInputControl;
		}

		// Token: 0x06004C9F RID: 19615 RVA: 0x000F099C File Offset: 0x000EEB9C
		protected override Control CreateTimePicker(string id)
		{
			GenericHtmlInputControl genericHtmlInputControl = new GenericHtmlInputControl("time")
			{
				ID = id
			};
			string value = string.Format("{0} {1}", "rsAdvTimePicker", "rfbSmall");
			genericHtmlInputControl.Attributes.Add("class", value);
			return genericHtmlInputControl;
		}

		// Token: 0x06004CA0 RID: 19616 RVA: 0x000F09E4 File Offset: 0x000EEBE4
		private DropDownList CreateDropDownList(string id)
		{
			return new DropDownList
			{
				ID = id,
				CssClass = "rfbFull"
			};
		}

		// Token: 0x06004CA1 RID: 19617 RVA: 0x000F0A0C File Offset: 0x000EEC0C
		public override void CreateEditButtons()
		{
			base.CreateEditButtons();
			if (base.ParentScheduler.UsingWebServiceBinding || base.Appointment.AllowDelete)
			{
				this.DeleteButton = this.CreateButton("DeleteButton", "rsAdvEditDelete", "Delete", base.Localization.Delete);
				this.DeleteButton.ValidationGroup = base.ParentScheduler.ValidationGroup;
			}
		}

		// Token: 0x06004CA2 RID: 19618 RVA: 0x000F0A78 File Offset: 0x000EEC78
		protected override LinkButton CreateButton(string id, string cssClass, string commandName, string text)
		{
			string cssClass2 = string.Format("{0} {1}", cssClass, "rsButton");
			return base.CreateButton(id, cssClass2, commandName, text);
		}

		// Token: 0x06004CA3 RID: 19619 RVA: 0x000F0AA4 File Offset: 0x000EECA4
		protected override void StartDatePicker_DataBinding(object sender, EventArgs e)
		{
			((GenericHtmlInputControl)sender).Value = (base.HasTimeZoneOffset ? base.ParentScheduler.UtcToDisplay(base.Appointment.Start).ToString("yyyy-MM-dd") : base.Appointment.StartLocal.ToString("yyyy-MM-dd"));
		}

		// Token: 0x06004CA4 RID: 19620 RVA: 0x000F0B04 File Offset: 0x000EED04
		protected override void StartTimePicker_DataBinding(object sender, EventArgs e)
		{
			((GenericHtmlInputControl)sender).Value = (base.HasTimeZoneOffset ? base.ParentScheduler.UtcToDisplay(base.Appointment.Start).ToString("HH:mm") : base.Appointment.StartLocal.ToString("HH:mm"));
		}

		// Token: 0x06004CA5 RID: 19621 RVA: 0x000F0B64 File Offset: 0x000EED64
		protected override void EndDatePicker_DataBinding(object sender, EventArgs e)
		{
			DateTime utcDate = base.HasTimeZoneOffset ? base.Appointment.End : base.Appointment.EndLocal;
			if (base.Owner.IsAllDayAppointment(base.Appointment))
			{
				utcDate = utcDate.AddDays(-1.0);
			}
			((GenericHtmlInputControl)sender).Value = (base.HasTimeZoneOffset ? base.ParentScheduler.UtcToDisplay(utcDate).ToString("yyyy-MM-dd") : utcDate.ToString("yyyy-MM-dd"));
		}

		// Token: 0x06004CA6 RID: 19622 RVA: 0x000F0BF0 File Offset: 0x000EEDF0
		protected override void EndTimePicker_DataBinding(object sender, EventArgs e)
		{
			((GenericHtmlInputControl)sender).Value = (base.HasTimeZoneOffset ? base.ParentScheduler.UtcToDisplay(base.Appointment.End).ToString("HH:mm") : base.Appointment.EndLocal.ToString("HH:mm"));
		}

		// Token: 0x06004CA7 RID: 19623 RVA: 0x000F0C50 File Offset: 0x000EEE50
		protected override void Reminder_DataBinding(object sender, EventArgs e)
		{
			if (base.Appointment.Reminders.Count > 0)
			{
				DropDownList dropDownList = base.Reminder as DropDownList;
				string value = ((int)base.Appointment.Reminders[0].Trigger.TotalMinutes).ToString();
				ListItem listItem = dropDownList.Items.FindByValue(value);
				if (listItem != null)
				{
					listItem.Selected = true;
				}
			}
		}

		// Token: 0x06004CA8 RID: 19624 RVA: 0x000F0CBC File Offset: 0x000EEEBC
		protected override void AttributeControl_DataBinding(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			textBox.Text = base.Appointment.Attributes[textBox.ID.Substring("Attr".Length)];
		}

		// Token: 0x06004CA9 RID: 19625 RVA: 0x000F0CFC File Offset: 0x000EEEFC
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
				DropDownList dropDownList = (DropDownList)sender;
				dropDownList.SelectedValue = LosSerializer.Serialize(resourceByType.Key);
			}
		}

		// Token: 0x06004CAA RID: 19626 RVA: 0x000F0DE0 File Offset: 0x000EEFE0
		public override void ExtractAttributeValues(IDictionary target)
		{
			foreach (string key in base.AttributeControls.Keys)
			{
				TextBox textBox = base.AttributeControls[key] as TextBox;
				target[key] = textBox.Text;
			}
		}

		// Token: 0x06004CAB RID: 19627 RVA: 0x000F0E4C File Offset: 0x000EF04C
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
				DropDownList dropDownList = base.ResourceControls[key] as DropDownList;
				if (dropDownList != null)
				{
					foreach (object obj2 in dropDownList.Items)
					{
						ListItem listItem2 = (ListItem)obj2;
						if (listItem2.Selected && listItem2.Value != "NULL")
						{
							arrayList.Add(LosSerializer.Deserialize(listItem2.Value));
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
	}
}
