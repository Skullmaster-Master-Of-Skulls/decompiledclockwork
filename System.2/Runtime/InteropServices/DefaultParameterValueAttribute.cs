using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020003DB RID: 987
	[AttributeUsage(AttributeTargets.Parameter)]
	[__DynamicallyInvokable]
	public sealed class DefaultParameterValueAttribute : Attribute
	{
		// Token: 0x06002601 RID: 9729 RVA: 0x000B0894 File Offset: 0x000AEA94
		[__DynamicallyInvokable]
		public DefaultParameterValueAttribute(object value)
		{
			this.value = value;
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06002602 RID: 9730 RVA: 0x000B08A3 File Offset: 0x000AEAA3
		[__DynamicallyInvokable]
		public object Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this.value;
			}
		}

		// Token: 0x04002087 RID: 8327
		private object value;
	}
}
