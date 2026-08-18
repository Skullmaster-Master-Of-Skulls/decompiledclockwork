using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020005F0 RID: 1520
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class RequiredAttributeAttribute : Attribute
	{
		// Token: 0x060037F7 RID: 14327 RVA: 0x000BBD38 File Offset: 0x000BAD38
		public RequiredAttributeAttribute(Type requiredContract)
		{
			this.requiredContract = requiredContract;
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x060037F8 RID: 14328 RVA: 0x000BBD47 File Offset: 0x000BAD47
		public Type RequiredContract
		{
			get
			{
				return this.requiredContract;
			}
		}

		// Token: 0x04001CFF RID: 7423
		private Type requiredContract;
	}
}
