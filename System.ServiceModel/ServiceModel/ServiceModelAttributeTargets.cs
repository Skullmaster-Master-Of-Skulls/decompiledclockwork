using System;

namespace System.ServiceModel
{
	// Token: 0x020000E2 RID: 226
	internal static class ServiceModelAttributeTargets
	{
		// Token: 0x040009FE RID: 2558
		public const AttributeTargets ServiceContract = AttributeTargets.Class | AttributeTargets.Interface;

		// Token: 0x040009FF RID: 2559
		public const AttributeTargets OperationContract = AttributeTargets.Method;

		// Token: 0x04000A00 RID: 2560
		public const AttributeTargets MessageContract = AttributeTargets.Class | AttributeTargets.Struct;

		// Token: 0x04000A01 RID: 2561
		public const AttributeTargets MessageMember = AttributeTargets.Property | AttributeTargets.Field;

		// Token: 0x04000A02 RID: 2562
		public const AttributeTargets Parameter = AttributeTargets.Parameter | AttributeTargets.ReturnValue;

		// Token: 0x04000A03 RID: 2563
		public const AttributeTargets ServiceBehavior = AttributeTargets.Class;

		// Token: 0x04000A04 RID: 2564
		public const AttributeTargets CallbackBehavior = AttributeTargets.Class;

		// Token: 0x04000A05 RID: 2565
		public const AttributeTargets ClientBehavior = AttributeTargets.Interface;

		// Token: 0x04000A06 RID: 2566
		public const AttributeTargets ContractBehavior = AttributeTargets.Class | AttributeTargets.Interface;

		// Token: 0x04000A07 RID: 2567
		public const AttributeTargets OperationBehavior = AttributeTargets.Method;
	}
}
