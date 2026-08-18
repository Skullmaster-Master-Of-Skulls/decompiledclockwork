using System;
using System.Collections.Generic;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000139 RID: 313
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	[__DynamicallyInvokable]
	public sealed class DynamicAttribute : Attribute
	{
		// Token: 0x06000A29 RID: 2601 RVA: 0x00024979 File Offset: 0x00022B79
		[__DynamicallyInvokable]
		public DynamicAttribute()
		{
			this._transformFlags = new bool[]
			{
				true
			};
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00024991 File Offset: 0x00022B91
		[__DynamicallyInvokable]
		public DynamicAttribute(bool[] transformFlags)
		{
			if (transformFlags == null)
			{
				throw new ArgumentNullException("transformFlags");
			}
			this._transformFlags = transformFlags;
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x000249AE File Offset: 0x00022BAE
		[__DynamicallyInvokable]
		public IList<bool> TransformFlags
		{
			[__DynamicallyInvokable]
			get
			{
				return Array.AsReadOnly<bool>(this._transformFlags);
			}
		}

		// Token: 0x04000761 RID: 1889
		private readonly bool[] _transformFlags;
	}
}
