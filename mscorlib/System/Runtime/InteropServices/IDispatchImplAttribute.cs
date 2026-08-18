using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004EA RID: 1258
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, Inherited = false)]
	[Obsolete("This attribute is deprecated and will be removed in a future version.", false)]
	[ComVisible(true)]
	public sealed class IDispatchImplAttribute : Attribute
	{
		// Token: 0x06003157 RID: 12631 RVA: 0x000A9127 File Offset: 0x000A8127
		public IDispatchImplAttribute(IDispatchImplType implType)
		{
			this._val = implType;
		}

		// Token: 0x06003158 RID: 12632 RVA: 0x000A9136 File Offset: 0x000A8136
		public IDispatchImplAttribute(short implType)
		{
			this._val = (IDispatchImplType)implType;
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06003159 RID: 12633 RVA: 0x000A9145 File Offset: 0x000A8145
		public IDispatchImplType Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04001906 RID: 6406
		internal IDispatchImplType _val;
	}
}
