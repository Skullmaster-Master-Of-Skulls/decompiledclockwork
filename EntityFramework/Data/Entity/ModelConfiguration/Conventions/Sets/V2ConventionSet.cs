using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.ModelConfiguration.Conventions.Sets
{
	// Token: 0x02000729 RID: 1833
	internal static class V2ConventionSet
	{
		// Token: 0x06004B63 RID: 19299 RVA: 0x00161A24 File Offset: 0x0015FC24
		[SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
		static V2ConventionSet()
		{
			List<IConvention> list = new List<IConvention>(V1ConventionSet.Conventions.StoreModelConventions);
			int index = list.FindIndex((IConvention c) => c.GetType() == typeof(ColumnOrderingConvention));
			list[index] = new ColumnOrderingConventionStrict();
			V2ConventionSet._conventions = new ConventionSet(V1ConventionSet.Conventions.ConfigurationConventions, V1ConventionSet.Conventions.ConceptualModelConventions, V1ConventionSet.Conventions.ConceptualToStoreMappingConventions, list);
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06004B64 RID: 19300 RVA: 0x00161A9A File Offset: 0x0015FC9A
		public static ConventionSet Conventions
		{
			get
			{
				return V2ConventionSet._conventions;
			}
		}

		// Token: 0x04001B72 RID: 7026
		private static readonly ConventionSet _conventions;
	}
}
