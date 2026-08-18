using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x0200002A RID: 42
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class DataSourceBooleanViewSchemaConverter : DataSourceViewSchemaConverter
	{
		// Token: 0x06000151 RID: 337 RVA: 0x0000C344 File Offset: 0x0000A544
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return this.GetStandardValues(context, typeof(bool));
		}
	}
}
