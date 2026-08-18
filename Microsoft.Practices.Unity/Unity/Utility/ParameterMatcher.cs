using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Practices.Unity.Utility
{
	// Token: 0x020000AC RID: 172
	public class ParameterMatcher
	{
		// Token: 0x06000362 RID: 866 RVA: 0x0000AEE3 File Offset: 0x000090E3
		public ParameterMatcher(IEnumerable<InjectionParameterValue> parametersToMatch)
		{
			this.parametersToMatch = new List<InjectionParameterValue>(parametersToMatch);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000AEF8 File Offset: 0x000090F8
		public virtual bool Matches(IEnumerable<Type> candidate)
		{
			List<Type> list = new List<Type>(candidate);
			if (this.parametersToMatch.Count == list.Count)
			{
				for (int i = 0; i < this.parametersToMatch.Count; i++)
				{
					if (!this.parametersToMatch[i].MatchesType(list[i]))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000AF5C File Offset: 0x0000915C
		public virtual bool Matches(IEnumerable<ParameterInfo> candidate)
		{
			return this.Matches(from pi in candidate
			select pi.ParameterType);
		}

		// Token: 0x040000C3 RID: 195
		private readonly List<InjectionParameterValue> parametersToMatch;
	}
}
