using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Telerik.Web.Data.Expressions
{
	// Token: 0x02001BB6 RID: 7094
	public static class CustomTypeDescriptorExtensions
	{
		// Token: 0x06011270 RID: 70256 RVA: 0x003C84FC File Offset: 0x003C66FC
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
		public static T Property<T>(this ICustomTypeDescriptor typeDescriptor, string propertyName)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(typeDescriptor)[propertyName];
			if (propertyDescriptor == null)
			{
				string message = string.Format(CultureInfo.CurrentCulture, "Property with specified name: {0} cannot be found on type: {1}", new object[]
				{
					propertyName,
					typeDescriptor.GetType()
				});
				throw new ArgumentException(message, "propertyName");
			}
			return UnboxT<T>.Unbox(propertyDescriptor.GetValue(typeDescriptor));
		}
	}
}
