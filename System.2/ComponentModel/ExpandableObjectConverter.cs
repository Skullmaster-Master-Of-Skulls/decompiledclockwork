using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000552 RID: 1362
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class ExpandableObjectConverter : TypeConverter
	{
		// Token: 0x06003345 RID: 13125 RVA: 0x000E3CF6 File Offset: 0x000E1EF6
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(value, attributes);
		}

		// Token: 0x06003346 RID: 13126 RVA: 0x000E3CFF File Offset: 0x000E1EFF
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
