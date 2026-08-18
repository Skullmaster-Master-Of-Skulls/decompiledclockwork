using System;

namespace System.ServiceModel
{
	// Token: 0x020000D0 RID: 208
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
	[__DynamicallyInvokable]
	public sealed class DataContractFormatAttribute : Attribute
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0001557B File Offset: 0x0001377B
		// (set) Token: 0x060003CC RID: 972 RVA: 0x00015583 File Offset: 0x00013783
		[__DynamicallyInvokable]
		public OperationFormatStyle Style
		{
			[__DynamicallyInvokable]
			get
			{
				return this.style;
			}
			[__DynamicallyInvokable]
			set
			{
				XmlSerializerFormatAttribute.ValidateOperationFormatStyle(this.style);
				this.style = value;
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00015597 File Offset: 0x00013797
		[__DynamicallyInvokable]
		public DataContractFormatAttribute()
		{
		}

		// Token: 0x0400099F RID: 2463
		private OperationFormatStyle style;
	}
}
