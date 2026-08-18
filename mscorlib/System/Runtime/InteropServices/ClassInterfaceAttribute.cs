using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004E1 RID: 1249
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, Inherited = false)]
	public sealed class ClassInterfaceAttribute : Attribute
	{
		// Token: 0x06003148 RID: 12616 RVA: 0x000A9079 File Offset: 0x000A8079
		public ClassInterfaceAttribute(ClassInterfaceType classInterfaceType)
		{
			this._val = classInterfaceType;
		}

		// Token: 0x06003149 RID: 12617 RVA: 0x000A9088 File Offset: 0x000A8088
		public ClassInterfaceAttribute(short classInterfaceType)
		{
			this._val = (ClassInterfaceType)classInterfaceType;
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x0600314A RID: 12618 RVA: 0x000A9097 File Offset: 0x000A8097
		public ClassInterfaceType Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x040018FC RID: 6396
		internal ClassInterfaceType _val;
	}
}
