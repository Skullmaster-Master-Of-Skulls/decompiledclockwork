using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001322 RID: 4898
	internal class AppointmentResourceTextPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x0600CCA9 RID: 52393 RVA: 0x002D9EEB File Offset: 0x002D80EB
		public AppointmentResourceTextPropertyDescriptor(string propertyName) : base(propertyName, new Attribute[0])
		{
			this._resourceType = propertyName;
		}

		// Token: 0x0600CCAA RID: 52394 RVA: 0x002D9F01 File Offset: 0x002D8101
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x170041DA RID: 16858
		// (get) Token: 0x0600CCAB RID: 52395 RVA: 0x002D9F04 File Offset: 0x002D8104
		public override Type ComponentType
		{
			get
			{
				return typeof(Appointment);
			}
		}

		// Token: 0x0600CCAC RID: 52396 RVA: 0x002D9F10 File Offset: 0x002D8110
		public override object GetValue(object component)
		{
			Appointment appointment = (Appointment)component;
			return appointment.Resources.GetResourceByType(this._resourceType);
		}

		// Token: 0x170041DB RID: 16859
		// (get) Token: 0x0600CCAD RID: 52397 RVA: 0x002D9F35 File Offset: 0x002D8135
		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170041DC RID: 16860
		// (get) Token: 0x0600CCAE RID: 52398 RVA: 0x002D9F38 File Offset: 0x002D8138
		public override Type PropertyType
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x0600CCAF RID: 52399 RVA: 0x002D9F44 File Offset: 0x002D8144
		public override void ResetValue(object component)
		{
		}

		// Token: 0x0600CCB0 RID: 52400 RVA: 0x002D9F46 File Offset: 0x002D8146
		public override void SetValue(object component, object value)
		{
		}

		// Token: 0x0600CCB1 RID: 52401 RVA: 0x002D9F48 File Offset: 0x002D8148
		public override bool ShouldSerializeValue(object component)
		{
			return true;
		}

		// Token: 0x0400368D RID: 13965
		private string _resourceType;
	}
}
