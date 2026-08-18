using System;

namespace System.ServiceModel.Description
{
	// Token: 0x0200041E RID: 1054
	[Flags]
	public enum ServiceContractGenerationOptions
	{
		// Token: 0x04002234 RID: 8756
		None = 0,
		// Token: 0x04002235 RID: 8757
		AsynchronousMethods = 1,
		// Token: 0x04002236 RID: 8758
		ChannelInterface = 2,
		// Token: 0x04002237 RID: 8759
		InternalTypes = 4,
		// Token: 0x04002238 RID: 8760
		ClientClass = 8,
		// Token: 0x04002239 RID: 8761
		TypedMessages = 16,
		// Token: 0x0400223A RID: 8762
		EventBasedAsynchronousMethods = 32,
		// Token: 0x0400223B RID: 8763
		TaskBasedAsynchronousMethod = 64
	}
}
