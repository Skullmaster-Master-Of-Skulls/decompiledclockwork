using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020005E7 RID: 1511
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	public sealed class FixedBufferAttribute : Attribute
	{
		// Token: 0x060037E8 RID: 14312 RVA: 0x000BBC7B File Offset: 0x000BAC7B
		public FixedBufferAttribute(Type elementType, int length)
		{
			this.elementType = elementType;
			this.length = length;
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x060037E9 RID: 14313 RVA: 0x000BBC91 File Offset: 0x000BAC91
		public Type ElementType
		{
			get
			{
				return this.elementType;
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x060037EA RID: 14314 RVA: 0x000BBC99 File Offset: 0x000BAC99
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x04001CEC RID: 7404
		private Type elementType;

		// Token: 0x04001CED RID: 7405
		private int length;
	}
}
