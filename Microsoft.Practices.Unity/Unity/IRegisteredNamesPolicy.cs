using System;
using System.Collections.Generic;
using Microsoft.Practices.ObjectBuilder2;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000003 RID: 3
	public interface IRegisteredNamesPolicy : IBuilderPolicy
	{
		// Token: 0x06000001 RID: 1
		IEnumerable<string> GetRegisteredNames(Type type);
	}
}
