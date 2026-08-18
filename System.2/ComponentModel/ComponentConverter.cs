using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200052D RID: 1325
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class ComponentConverter : ReferenceConverter
	{
		// Token: 0x06003223 RID: 12835 RVA: 0x000E0D0E File Offset: 0x000DEF0E
		public ComponentConverter(Type type) : base(type)
		{
		}

		// Token: 0x06003224 RID: 12836 RVA: 0x000E0D17 File Offset: 0x000DEF17
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(value, attributes);
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x000E0D20 File Offset: 0x000DEF20
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
