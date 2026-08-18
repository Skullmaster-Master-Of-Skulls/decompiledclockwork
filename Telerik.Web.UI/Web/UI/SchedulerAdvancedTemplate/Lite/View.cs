using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SchedulerAdvancedTemplate.Lite
{
	// Token: 0x02000819 RID: 2073
	internal class View : ViewBase
	{
		// Token: 0x170018FA RID: 6394
		// (get) Token: 0x06004C7A RID: 19578 RVA: 0x000EF910 File Offset: 0x000EDB10
		public override string SelectedTimeZone
		{
			get
			{
				RadDropDownList radDropDownList = base.TimeZones as RadDropDownList;
				if (radDropDownList != null)
				{
					return radDropDownList.SelectedValue.ToString();
				}
				return base.ParentScheduler.TimeZoneID;
			}
		}

		// Token: 0x170018FB RID: 6395
		// (get) Token: 0x06004C7B RID: 19579 RVA: 0x000EF944 File Offset: 0x000EDB44
		public override string SelectedReminder
		{
			get
			{
				RadDropDownList radDropDownList = base.Reminder as RadDropDownList;
				return radDropDownList.SelectedValue;
			}
		}

		// Token: 0x06004C7C RID: 19580 RVA: 0x000EF963 File Offset: 0x000EDB63
		public View(AdvancedTemplate owner) : base(owner)
		{
		}

		// Token: 0x06004C7D RID: 19581 RVA: 0x000EF96C File Offset: 0x000EDB6C
		protected override void CreateCloseButton()
		{
			base.CreateCloseButton();
			base.CloseButton.Text = string.Empty;
			WebControl child = new WebControl(HtmlTextWriterTag.Span)
			{
				CssClass = string.Format("{0} {1}", "p-icon", "p-i-close")
			};
			base.CloseButton.Controls.Add(child);
		}

		// Token: 0x06004C7E RID: 19582 RVA: 0x000EF9C4 File Offset: 0x000EDBC4
		protected override DataBoundControl CreateTimeZonesControl(string id)
		{
			RadDropDownList radDropDownList = this.CreateDropDownList(id);
			this.PopulateTimeZones(radDropDownList);
			return radDropDownList;
		}

		// Token: 0x06004C7F RID: 19583 RVA: 0x000EF9E4 File Offset: 0x000EDBE4
		private void PopulateTimeZones(RadDropDownList dropDownList)
		{
			dropDownList.DataSource = base.ParentScheduler.TimeZonesProvider.GetAllTimeZones();
			dropDownList.DataTextField = "DisplayName";
			dropDownList.DataValueField = "Id";
			dropDownList.DataBind();
			dropDownList.SelectedValue = (string.IsNullOrEmpty(base.Appointment.TimeZoneID) ? "UTC" : base.Appointment.TimeZoneID);
		}

		// Token: 0x06004C80 RID: 19584 RVA: 0x000EFA50 File Offset: 0x000EDC50
		protected override DataBoundControl CreateReminderControl(string id)
		{
			RadDropDownList radDropDownList = this.CreateDropDownList(id);
			this.PopulateReminderValues(radDropDownList);
			return radDropDownList;
		}

		// Token: 0x06004C81 RID: 19585 RVA: 0x000EFA70 File Offset: 0x000EDC70
		private void PopulateReminderValues(RadDropDownList dropDownList)
		{
			dropDownList.Items.AddRange(new DropDownListItem[]
			{
				new DropDownListItem(base.Localization.ReminderNone, string.Empty),
				new DropDownListItem("0 " + base.Localization.ReminderMinutes, "0"),
				new DropDownListItem("5 " + base.Localization.ReminderMinutes, "5"),
				new DropDownListItem("10 " + base.Localization.ReminderMinutes, "10"),
				new DropDownListItem("15 " + base.Localization.ReminderMinutes, "15"),
				new DropDownListItem("30 " + base.Localization.ReminderMinutes, "30"),
				new DropDownListItem("1 " + base.Localization.ReminderHour, "60"),
				new DropDownListItem("2 " + base.Localization.ReminderHours, "120"),
				new DropDownListItem("3 " + base.Localization.ReminderHours, "180"),
				new DropDownListItem("4 " + base.Localization.ReminderHours, "240"),
				new DropDownListItem("5 " + base.Localization.ReminderHours, "300"),
				new DropDownListItem("6 " + base.Localization.ReminderHours, "360"),
				new DropDownListItem("7 " + base.Localization.ReminderHours, "420"),
				new DropDownListItem("8 " + base.Localization.ReminderHours, "480"),
				new DropDownListItem("9 " + base.Localization.ReminderHours, "540"),
				new DropDownListItem("10 " + base.Localization.ReminderHours, "600"),
				new DropDownListItem("11 " + base.Localization.ReminderHours, "660"),
				new DropDownListItem("12 " + base.Localization.ReminderHours, "720"),
				new DropDownListItem("18 " + base.Localization.ReminderHours, "1080"),
				new DropDownListItem("1 " + base.Localization.ReminderDays, "1440"),
				new DropDownListItem("2 " + base.Localization.ReminderDays, "2880"),
				new DropDownListItem("3 " + base.Localization.ReminderDays, "4320"),
				new DropDownListItem("4 " + base.Localization.ReminderDays, "5760"),
				new DropDownListItem("1 " + base.Localization.ReminderWeek, "10080"),
				new DropDownListItem("2 " + base.Localization.ReminderWeeks, "20160")
			});
		}

		// Token: 0x06004C82 RID: 19586 RVA: 0x000EFDE9 File Offset: 0x000EDFE9
		protected override DataBoundControl CreateResourceControl()
		{
			return this.CreateDropDownList("");
		}

		// Token: 0x06004C83 RID: 19587 RVA: 0x000EFDF8 File Offset: 0x000EDFF8
		protected override void PopulateResourceControl(DataBoundControl resourceControl, string resType, bool addNullValue)
		{
			RadDropDownList radDropDownList = resourceControl as RadDropDownList;
			if (addNullValue)
			{
				radDropDownList.Items.Add(new DropDownListItem("-", "NULL"));
			}
			foreach (Resource resource in base.GetResources(resType))
			{
				radDropDownList.Items.Add(new DropDownListItem(resource.Text, LosSerializer.Serialize(resource.Key)));
			}
		}

		// Token: 0x06004C84 RID: 19588 RVA: 0x000EFE84 File Offset: 0x000EE084
		private RadDropDownList CreateDropDownList(string id = "")
		{
			RadDropDownList radDropDownList = new RadDropDownList
			{
				ID = id,
				EnableEmbeddedSkins = base.ParentScheduler.EnableEmbeddedSkins,
				EnableEmbeddedScripts = base.ParentScheduler.EnableEmbeddedScripts,
				RenderMode = base.ParentScheduler.ResolvedRenderMode,
				ZIndex = base.ParentScheduler.AdvancedForm.ZIndex + 20
			};
			if (radDropDownList.RuntimeSkin != base.Owner._runtimeSkin)
			{
				radDropDownList.Skin = base.Owner._runtimeSkin;
			}
			return radDropDownList;
		}

		// Token: 0x06004C85 RID: 19589 RVA: 0x000EFF18 File Offset: 0x000EE118
		protected override LinkButton CreateButton(string id, string cssClass, string commandName, string text)
		{
			string cssClass2 = string.Format("{0} {1}", cssClass, "rsButton");
			return base.CreateButton(id, cssClass2, commandName, text);
		}

		// Token: 0x06004C86 RID: 19590 RVA: 0x000EFF44 File Offset: 0x000EE144
		protected override void Reminder_DataBinding(object sender, EventArgs e)
		{
			if (base.Appointment.Reminders.Count > 0)
			{
				RadDropDownList radDropDownList = base.Reminder as RadDropDownList;
				string selectedValue = ((int)base.Appointment.Reminders[0].Trigger.TotalMinutes).ToString();
				radDropDownList.SelectedValue = selectedValue;
			}
		}

		// Token: 0x06004C87 RID: 19591 RVA: 0x000EFFA0 File Offset: 0x000EE1A0
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
				RadDropDownList radDropDownList = (RadDropDownList)sender;
				radDropDownList.SelectedValue = LosSerializer.Serialize(resourceByType.Key);
			}
		}

		// Token: 0x06004C88 RID: 19592 RVA: 0x000F0084 File Offset: 0x000EE284
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
				RadDropDownList radDropDownList = base.ResourceControls[key] as RadDropDownList;
				if (radDropDownList != null)
				{
					foreach (object obj2 in radDropDownList.Items)
					{
						DropDownListItem dropDownListItem = (DropDownListItem)obj2;
						if (dropDownListItem.Selected && dropDownListItem.Value != "NULL")
						{
							arrayList.Add(LosSerializer.Deserialize(dropDownListItem.Value));
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

		// Token: 0x0400133B RID: 4923
		private const int ComboBoxesZIndexStep = 20;
	}
}
