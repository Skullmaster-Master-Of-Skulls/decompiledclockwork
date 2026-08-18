using System;

namespace System.Xml.Serialization
{
	// Token: 0x020001A4 RID: 420
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
	[__DynamicallyInvokable]
	public sealed class XmlSchemaProviderAttribute : Attribute
	{
		// Token: 0x06001BE9 RID: 7145 RVA: 0x000825E7 File Offset: 0x000807E7
		[__DynamicallyInvokable]
		public XmlSchemaProviderAttribute(string methodName)
		{
			this.methodName = methodName;
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001BEA RID: 7146 RVA: 0x000825F6 File Offset: 0x000807F6
		[__DynamicallyInvokable]
		public string MethodName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.methodName;
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001BEB RID: 7147 RVA: 0x000825FE File Offset: 0x000807FE
		// (set) Token: 0x06001BEC RID: 7148 RVA: 0x00082606 File Offset: 0x00080806
		[__DynamicallyInvokable]
		public bool IsAny
		{
			[__DynamicallyInvokable]
			get
			{
				return this.any;
			}
			[__DynamicallyInvokable]
			set
			{
				this.any = value;
			}
		}

		// Token: 0x04000C2E RID: 3118
		private string methodName;

		// Token: 0x04000C2F RID: 3119
		private bool any;
	}
}
