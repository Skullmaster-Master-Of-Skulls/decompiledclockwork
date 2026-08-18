using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020005ED RID: 1517
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class MethodImplAttribute : Attribute
	{
		// Token: 0x060037F0 RID: 14320 RVA: 0x000BBCD8 File Offset: 0x000BACD8
		internal MethodImplAttribute(MethodImplAttributes methodImplAttributes)
		{
			MethodImplOptions methodImplOptions = MethodImplOptions.Unmanaged | MethodImplOptions.ForwardRef | MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall | MethodImplOptions.Synchronized | MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization;
			this._val = (MethodImplOptions)(methodImplAttributes & (MethodImplAttributes)methodImplOptions);
		}

		// Token: 0x060037F1 RID: 14321 RVA: 0x000BBCFA File Offset: 0x000BACFA
		public MethodImplAttribute(MethodImplOptions methodImplOptions)
		{
			this._val = methodImplOptions;
		}

		// Token: 0x060037F2 RID: 14322 RVA: 0x000BBD09 File Offset: 0x000BAD09
		public MethodImplAttribute(short value)
		{
			this._val = (MethodImplOptions)value;
		}

		// Token: 0x060037F3 RID: 14323 RVA: 0x000BBD18 File Offset: 0x000BAD18
		public MethodImplAttribute()
		{
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x060037F4 RID: 14324 RVA: 0x000BBD20 File Offset: 0x000BAD20
		public MethodImplOptions Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04001CFD RID: 7421
		internal MethodImplOptions _val;

		// Token: 0x04001CFE RID: 7422
		public MethodCodeType MethodCodeType;
	}
}
