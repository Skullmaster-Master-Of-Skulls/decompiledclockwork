using System;
using System.ComponentModel;

namespace System.Drawing.Design
{
	// Token: 0x02000073 RID: 115
	public interface IPropertyValueUIService
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000830 RID: 2096
		// (remove) Token: 0x06000831 RID: 2097
		event EventHandler PropertyUIValueItemsChanged;

		// Token: 0x06000832 RID: 2098
		void AddPropertyValueUIHandler(PropertyValueUIHandler newHandler);

		// Token: 0x06000833 RID: 2099
		PropertyValueUIItem[] GetPropertyUIValueItems(ITypeDescriptorContext context, PropertyDescriptor propDesc);

		// Token: 0x06000834 RID: 2100
		void NotifyPropertyValueUIItemsChanged();

		// Token: 0x06000835 RID: 2101
		void RemovePropertyValueUIHandler(PropertyValueUIHandler newHandler);
	}
}
