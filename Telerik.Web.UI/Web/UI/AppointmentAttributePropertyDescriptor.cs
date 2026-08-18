using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200131F RID: 4895
	internal class AppointmentAttributePropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x0600CC80 RID: 52352 RVA: 0x002D907C File Offset: 0x002D727C
		public AppointmentAttributePropertyDescriptor(string propertyName) : base(propertyName, new Attribute[0])
		{
		}

		// Token: 0x0600CC81 RID: 52353 RVA: 0x002D908B File Offset: 0x002D728B
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x170041D4 RID: 16852
		// (get) Token: 0x0600CC82 RID: 52354 RVA: 0x002D908E File Offset: 0x002D728E
		public override Type ComponentType
		{
			get
			{
				return typeof(Appointment);
			}
		}

		// Token: 0x0600CC83 RID: 52355 RVA: 0x002D909C File Offset: 0x002D729C
		public override object GetValue(object component)
		{
			Appointment appointment = (Appointment)component;
			return appointment.Attributes[this.Name];
		}

		// Token: 0x170041D5 RID: 16853
		// (get) Token: 0x0600CC84 RID: 52356 RVA: 0x002D90C1 File Offset: 0x002D72C1
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170041D6 RID: 16854
		// (get) Token: 0x0600CC85 RID: 52357 RVA: 0x002D90C4 File Offset: 0x002D72C4
		public override Type PropertyType
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x0600CC86 RID: 52358 RVA: 0x002D90D0 File Offset: 0x002D72D0
		public override void ResetValue(object component)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600CC87 RID: 52359 RVA: 0x002D90D8 File Offset: 0x002D72D8
		public override void SetValue(object component, object value)
		{
			Appointment appointment = (Appointment)component;
			appointment.Attributes[this.Name] = value.ToString();
		}

		// Token: 0x0600CC88 RID: 52360 RVA: 0x002D9103 File Offset: 0x002D7303
		public override bool ShouldSerializeValue(object component)
		{
			throw new NotImplementedException();
		}
	}
}
