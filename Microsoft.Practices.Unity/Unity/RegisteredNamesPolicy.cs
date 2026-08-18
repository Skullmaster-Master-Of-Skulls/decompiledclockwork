using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000004 RID: 4
	internal class RegisteredNamesPolicy : IRegisteredNamesPolicy, IBuilderPolicy
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020D0 File Offset: 0x000002D0
		public RegisteredNamesPolicy(NamedTypesRegistry registry)
		{
			this.registry = registry;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020EA File Offset: 0x000002EA
		public IEnumerable<string> GetRegisteredNames(Type type)
		{
			return from s in this.registry.GetKeys(type)
			where !string.IsNullOrEmpty(s)
			select s;
		}

		// Token: 0x04000001 RID: 1
		private NamedTypesRegistry registry;
	}
}
