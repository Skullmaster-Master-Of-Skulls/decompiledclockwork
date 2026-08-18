using System;

namespace System.ComponentModel
{
	// Token: 0x0200055F RID: 1375
	public interface ICustomTypeDescriptor
	{
		// Token: 0x06003397 RID: 13207
		AttributeCollection GetAttributes();

		// Token: 0x06003398 RID: 13208
		string GetClassName();

		// Token: 0x06003399 RID: 13209
		string GetComponentName();

		// Token: 0x0600339A RID: 13210
		TypeConverter GetConverter();

		// Token: 0x0600339B RID: 13211
		EventDescriptor GetDefaultEvent();

		// Token: 0x0600339C RID: 13212
		PropertyDescriptor GetDefaultProperty();

		// Token: 0x0600339D RID: 13213
		object GetEditor(Type editorBaseType);

		// Token: 0x0600339E RID: 13214
		EventDescriptorCollection GetEvents();

		// Token: 0x0600339F RID: 13215
		EventDescriptorCollection GetEvents(Attribute[] attributes);

		// Token: 0x060033A0 RID: 13216
		PropertyDescriptorCollection GetProperties();

		// Token: 0x060033A1 RID: 13217
		PropertyDescriptorCollection GetProperties(Attribute[] attributes);

		// Token: 0x060033A2 RID: 13218
		object GetPropertyOwner(PropertyDescriptor pd);
	}
}
