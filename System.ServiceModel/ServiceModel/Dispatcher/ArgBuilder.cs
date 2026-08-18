using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020005A6 RID: 1446
	internal class ArgBuilder
	{
		// Token: 0x0600387F RID: 14463 RVA: 0x000D9B1E File Offset: 0x000D7D1E
		internal ArgBuilder(int index, Type argType)
		{
			this.Index = index;
			this.ArgType = argType;
		}

		// Token: 0x04002992 RID: 10642
		internal int Index;

		// Token: 0x04002993 RID: 10643
		internal Type ArgType;
	}
}
