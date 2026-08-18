using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004DF RID: 1247
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComDefaultInterfaceAttribute : Attribute
	{
		// Token: 0x06003146 RID: 12614 RVA: 0x000A9062 File Offset: 0x000A8062
		public ComDefaultInterfaceAttribute(Type defaultInterface)
		{
			this._val = defaultInterface;
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06003147 RID: 12615 RVA: 0x000A9071 File Offset: 0x000A8071
		public Type Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x040018F7 RID: 6391
		internal Type _val;
	}
}
