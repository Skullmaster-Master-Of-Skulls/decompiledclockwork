using System;

namespace System.Data.Objects
{
	// Token: 0x0200013B RID: 315
	internal struct StateManagerValue
	{
		// Token: 0x060016E6 RID: 5862 RVA: 0x0004C485 File Offset: 0x0004A685
		internal StateManagerValue(StateManagerMemberMetadata metadata, object instance, object value)
		{
			this.memberMetadata = metadata;
			this.userObject = instance;
			this.originalValue = value;
		}

		// Token: 0x04000A64 RID: 2660
		internal StateManagerMemberMetadata memberMetadata;

		// Token: 0x04000A65 RID: 2661
		internal object userObject;

		// Token: 0x04000A66 RID: 2662
		internal object originalValue;
	}
}
