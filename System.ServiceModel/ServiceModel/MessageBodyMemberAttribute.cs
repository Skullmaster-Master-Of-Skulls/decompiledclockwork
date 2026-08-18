using System;

namespace System.ServiceModel
{
	// Token: 0x020000D4 RID: 212
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false)]
	[__DynamicallyInvokable]
	public class MessageBodyMemberAttribute : MessageContractMemberAttribute
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003DB RID: 987 RVA: 0x000156CF File Offset: 0x000138CF
		// (set) Token: 0x060003DC RID: 988 RVA: 0x000156D7 File Offset: 0x000138D7
		[__DynamicallyInvokable]
		public int Order
		{
			[__DynamicallyInvokable]
			get
			{
				return this.order;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBeNonNegative")));
				}
				this.order = value;
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00015709 File Offset: 0x00013909
		[__DynamicallyInvokable]
		public MessageBodyMemberAttribute()
		{
		}

		// Token: 0x040009AB RID: 2475
		private int order = -1;

		// Token: 0x040009AC RID: 2476
		internal const string OrderPropertyName = "Order";
	}
}
