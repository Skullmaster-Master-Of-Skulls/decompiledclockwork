using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004DE RID: 1246
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	public sealed class InterfaceTypeAttribute : Attribute
	{
		// Token: 0x06003143 RID: 12611 RVA: 0x000A903C File Offset: 0x000A803C
		public InterfaceTypeAttribute(ComInterfaceType interfaceType)
		{
			this._val = interfaceType;
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x000A904B File Offset: 0x000A804B
		public InterfaceTypeAttribute(short interfaceType)
		{
			this._val = (ComInterfaceType)interfaceType;
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06003145 RID: 12613 RVA: 0x000A905A File Offset: 0x000A805A
		public ComInterfaceType Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x040018F6 RID: 6390
		internal ComInterfaceType _val;
	}
}
