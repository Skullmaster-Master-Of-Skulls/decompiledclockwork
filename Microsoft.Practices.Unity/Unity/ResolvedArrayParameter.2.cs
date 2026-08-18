using System;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000089 RID: 137
	public class ResolvedArrayParameter<TElement> : ResolvedArrayParameter
	{
		// Token: 0x06000280 RID: 640 RVA: 0x000084FF File Offset: 0x000066FF
		public ResolvedArrayParameter(params object[] elementValues) : base(typeof(TElement[]), typeof(TElement), elementValues)
		{
		}
	}
}
