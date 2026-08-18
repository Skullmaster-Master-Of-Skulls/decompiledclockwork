using System;
using System.ComponentModel;

namespace MS.Internal.Xml.Linq.ComponentModel
{
	// Token: 0x02000033 RID: 51
	internal class XTypeDescriptionProvider<T> : TypeDescriptionProvider
	{
		// Token: 0x060002AC RID: 684 RVA: 0x0000B885 File Offset: 0x00009A85
		public XTypeDescriptionProvider() : base(TypeDescriptor.GetProvider(typeof(T)))
		{
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000B89C File Offset: 0x00009A9C
		public override ICustomTypeDescriptor GetTypeDescriptor(Type type, object instance)
		{
			return new XTypeDescriptor<T>(base.GetTypeDescriptor(type, instance));
		}
	}
}
