using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200075B RID: 1883
	public enum ReceiveContextState
	{
		// Token: 0x04002DD5 RID: 11733
		Received,
		// Token: 0x04002DD6 RID: 11734
		Completing,
		// Token: 0x04002DD7 RID: 11735
		Completed,
		// Token: 0x04002DD8 RID: 11736
		Abandoning,
		// Token: 0x04002DD9 RID: 11737
		Abandoned,
		// Token: 0x04002DDA RID: 11738
		Faulted
	}
}
