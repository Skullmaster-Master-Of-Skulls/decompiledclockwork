using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000323 RID: 803
	internal class RadioButtonDesigner : ButtonBaseDesigner
	{
		// Token: 0x06001FCE RID: 8142 RVA: 0x000C0DE4 File Offset: 0x000BEFE4
		public override void InitializeNewComponent(IDictionary defaultValues)
		{
			base.InitializeNewComponent(defaultValues);
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["TabStop"];
			if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(bool) && !propertyDescriptor.IsReadOnly && propertyDescriptor.IsBrowsable)
			{
				propertyDescriptor.SetValue(base.Component, true);
			}
		}
	}
}
