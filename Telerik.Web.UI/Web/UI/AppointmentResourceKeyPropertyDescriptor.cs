using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001321 RID: 4897
	internal class AppointmentResourceKeyPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x0600CCA0 RID: 52384 RVA: 0x002D9E3C File Offset: 0x002D803C
		public AppointmentResourceKeyPropertyDescriptor(string propertyName, string resourceType) : base(propertyName, new Attribute[0])
		{
			this._resourceType = resourceType;
		}

		// Token: 0x0600CCA1 RID: 52385 RVA: 0x002D9E52 File Offset: 0x002D8052
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x170041D7 RID: 16855
		// (get) Token: 0x0600CCA2 RID: 52386 RVA: 0x002D9E55 File Offset: 0x002D8055
		public override Type ComponentType
		{
			get
			{
				return typeof(Appointment);
			}
		}

		// Token: 0x0600CCA3 RID: 52387 RVA: 0x002D9E64 File Offset: 0x002D8064
		public override object GetValue(object component)
		{
			Appointment appointment = (Appointment)component;
			Resource resourceByType = appointment.Resources.GetResourceByType(this._resourceType);
			if (!(resourceByType != null))
			{
				return null;
			}
			return resourceByType.Key;
		}

		// Token: 0x170041D8 RID: 16856
		// (get) Token: 0x0600CCA4 RID: 52388 RVA: 0x002D9E9B File Offset: 0x002D809B
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170041D9 RID: 16857
		// (get) Token: 0x0600CCA5 RID: 52389 RVA: 0x002D9E9E File Offset: 0x002D809E
		public override Type PropertyType
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x0600CCA6 RID: 52390 RVA: 0x002D9EAA File Offset: 0x002D80AA
		public override void ResetValue(object component)
		{
		}

		// Token: 0x0600CCA7 RID: 52391 RVA: 0x002D9EAC File Offset: 0x002D80AC
		public override void SetValue(object component, object value)
		{
			Appointment appointment = (Appointment)component;
			appointment.Resources.GetResourceByType(this._resourceType).Key = value.ToString();
			this.OnValueChanged(component, EventArgs.Empty);
		}

		// Token: 0x0600CCA8 RID: 52392 RVA: 0x002D9EE8 File Offset: 0x002D80E8
		public override bool ShouldSerializeValue(object component)
		{
			return true;
		}

		// Token: 0x0400368C RID: 13964
		private string _resourceType;
	}
}
