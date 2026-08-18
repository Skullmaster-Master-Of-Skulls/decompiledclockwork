using System;

namespace Microsoft.Practices.Unity
{
	// Token: 0x0200009E RID: 158
	public class ResolvedParameter<TParameter> : ResolvedParameter
	{
		// Token: 0x060002D0 RID: 720 RVA: 0x0000939A File Offset: 0x0000759A
		public ResolvedParameter() : base(typeof(TParameter))
		{
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000093AC File Offset: 0x000075AC
		public ResolvedParameter(string name) : base(typeof(TParameter), name)
		{
		}
	}
}
