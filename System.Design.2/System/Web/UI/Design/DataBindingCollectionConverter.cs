using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x0200001F RID: 31
	[Obsolete("Use of this type is not recommended because DataBindings editing is launched via a DesignerActionList instead of the property grid. http://go.microsoft.com/fwlink/?linkid=14202")]
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class DataBindingCollectionConverter : TypeConverter
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00009BF5 File Offset: 0x00007DF5
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				return string.Empty;
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
