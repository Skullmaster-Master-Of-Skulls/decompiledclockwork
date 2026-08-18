using System;

namespace System.ComponentModel
{
	// Token: 0x0200055C RID: 1372
	[Obsolete("This interface has been deprecated. Add a TypeDescriptionProvider to handle type TypeDescriptor.ComObjectType instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
	public interface IComNativeDescriptorHandler
	{
		// Token: 0x06003383 RID: 13187
		AttributeCollection GetAttributes(object component);

		// Token: 0x06003384 RID: 13188
		string GetClassName(object component);

		// Token: 0x06003385 RID: 13189
		TypeConverter GetConverter(object component);

		// Token: 0x06003386 RID: 13190
		EventDescriptor GetDefaultEvent(object component);

		// Token: 0x06003387 RID: 13191
		PropertyDescriptor GetDefaultProperty(object component);

		// Token: 0x06003388 RID: 13192
		object GetEditor(object component, Type baseEditorType);

		// Token: 0x06003389 RID: 13193
		string GetName(object component);

		// Token: 0x0600338A RID: 13194
		EventDescriptorCollection GetEvents(object component);

		// Token: 0x0600338B RID: 13195
		EventDescriptorCollection GetEvents(object component, Attribute[] attributes);

		// Token: 0x0600338C RID: 13196
		PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes);

		// Token: 0x0600338D RID: 13197
		object GetPropertyValue(object component, string propertyName, ref bool success);

		// Token: 0x0600338E RID: 13198
		object GetPropertyValue(object component, int dispid, ref bool success);
	}
}
