using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.Configuration.ConfigurationHelpers
{
	// Token: 0x0200003C RID: 60
	public static class TypeResolver
	{
		// Token: 0x060001DC RID: 476 RVA: 0x0000671C File Offset: 0x0000491C
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public static void SetAliases(UnityConfigurationSection section)
		{
			Guard.ArgumentNotNull(section, "section");
			TypeResolver.impl = new TypeResolverImpl(from e in section.TypeAliases
			select new KeyValuePair<string, string>(e.Alias, e.TypeName), section.Namespaces.Select((NamespaceElement e) => e.Name), section.Assemblies.Select((AssemblyElement e) => e.Name));
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000067B6 File Offset: 0x000049B6
		public static Type ResolveType(string typeNameOrAlias)
		{
			return TypeResolver.impl.ResolveType(typeNameOrAlias, true);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000067C4 File Offset: 0x000049C4
		public static Type ResolveType(string typeNameOrAlias, bool throwIfResolveFails)
		{
			return TypeResolver.impl.ResolveType(typeNameOrAlias, throwIfResolveFails);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000067D2 File Offset: 0x000049D2
		public static Type ResolveTypeWithDefault(string typeNameOrAlias, Type defaultValue)
		{
			return TypeResolver.impl.ResolveTypeWithDefault(typeNameOrAlias, defaultValue, true);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000067E1 File Offset: 0x000049E1
		public static Type ResolveTypeWithDefault(string typeNameOrAlias, Type defaultValue, bool throwIfResolveFails)
		{
			return TypeResolver.impl.ResolveTypeWithDefault(typeNameOrAlias, defaultValue, throwIfResolveFails);
		}

		// Token: 0x0400006A RID: 106
		[ThreadStatic]
		private static TypeResolverImpl impl;
	}
}
