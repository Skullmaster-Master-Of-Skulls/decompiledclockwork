using System;
using System.Reflection;

namespace Microsoft.Practices.Unity.Utility
{
	// Token: 0x020000AE RID: 174
	public class ParameterReflectionHelper : ReflectionHelper
	{
		// Token: 0x06000372 RID: 882 RVA: 0x0000B20C File Offset: 0x0000940C
		public ParameterReflectionHelper(ParameterInfo parameter) : base(ParameterReflectionHelper.TypeFromParameterInfo(parameter))
		{
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000B21A File Offset: 0x0000941A
		private static Type TypeFromParameterInfo(ParameterInfo parameter)
		{
			Guard.ArgumentNotNull(parameter, "parameter");
			return parameter.ParameterType;
		}
	}
}
