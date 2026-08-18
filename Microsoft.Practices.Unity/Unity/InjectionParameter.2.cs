using System;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000086 RID: 134
	public class InjectionParameter<TParameter> : InjectionParameter
	{
		// Token: 0x06000270 RID: 624 RVA: 0x000080F8 File Offset: 0x000062F8
		public InjectionParameter(TParameter parameterValue) : base(typeof(TParameter), parameterValue)
		{
		}
	}
}
