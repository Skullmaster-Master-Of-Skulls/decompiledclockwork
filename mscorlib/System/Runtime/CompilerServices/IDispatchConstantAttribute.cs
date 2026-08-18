using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200060C RID: 1548
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class IDispatchConstantAttribute : CustomConstantAttribute
	{
		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06003811 RID: 14353 RVA: 0x000BBE9D File Offset: 0x000BAE9D
		public override object Value
		{
			get
			{
				return new DispatchWrapper(null);
			}
		}
	}
}
