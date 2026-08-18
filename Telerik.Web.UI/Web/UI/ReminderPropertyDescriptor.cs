using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000F99 RID: 3993
	internal class ReminderPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x060098E3 RID: 39139 RVA: 0x002219B5 File Offset: 0x0021FBB5
		public ReminderPropertyDescriptor() : base("Reminder", new Attribute[0])
		{
		}

		// Token: 0x060098E4 RID: 39140 RVA: 0x002219C8 File Offset: 0x0021FBC8
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x17003069 RID: 12393
		// (get) Token: 0x060098E5 RID: 39141 RVA: 0x002219CB File Offset: 0x0021FBCB
		public override Type ComponentType
		{
			get
			{
				return typeof(Appointment);
			}
		}

		// Token: 0x060098E6 RID: 39142 RVA: 0x002219D8 File Offset: 0x0021FBD8
		public override object GetValue(object component)
		{
			Appointment appointment = (Appointment)component;
			if (appointment.Reminders.Count > 0)
			{
				return ((int)appointment.Reminders[0].Trigger.TotalMinutes).ToString();
			}
			return "";
		}

		// Token: 0x1700306A RID: 12394
		// (get) Token: 0x060098E7 RID: 39143 RVA: 0x00221A22 File Offset: 0x0021FC22
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700306B RID: 12395
		// (get) Token: 0x060098E8 RID: 39144 RVA: 0x00221A25 File Offset: 0x0021FC25
		public override Type PropertyType
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x060098E9 RID: 39145 RVA: 0x00221A31 File Offset: 0x0021FC31
		public override void ResetValue(object component)
		{
		}

		// Token: 0x060098EA RID: 39146 RVA: 0x00221A34 File Offset: 0x0021FC34
		public override void SetValue(object component, object value)
		{
			Appointment appointment = (Appointment)component;
			int num = int.Parse(value.ToString());
			if (appointment.Reminders.Count > 0)
			{
				appointment.Reminders[0].Trigger = TimeSpan.FromMinutes((double)num);
			}
			else
			{
				appointment.Reminders.Add(new Reminder(num));
			}
			this.OnValueChanged(component, EventArgs.Empty);
		}

		// Token: 0x060098EB RID: 39147 RVA: 0x00221A99 File Offset: 0x0021FC99
		public override bool ShouldSerializeValue(object component)
		{
			return true;
		}
	}
}
