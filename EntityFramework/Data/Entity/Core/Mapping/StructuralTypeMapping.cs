using System;
using System.Collections.ObjectModel;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020001DE RID: 478
	public abstract class StructuralTypeMapping : MappingItem
	{
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060010E1 RID: 4321
		public abstract ReadOnlyCollection<PropertyMapping> PropertyMappings { get; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060010E2 RID: 4322
		public abstract ReadOnlyCollection<ConditionPropertyMapping> Conditions { get; }

		// Token: 0x060010E3 RID: 4323
		public abstract void AddPropertyMapping(PropertyMapping propertyMapping);

		// Token: 0x060010E4 RID: 4324
		public abstract void RemovePropertyMapping(PropertyMapping propertyMapping);

		// Token: 0x060010E5 RID: 4325
		public abstract void AddCondition(ConditionPropertyMapping condition);

		// Token: 0x060010E6 RID: 4326
		public abstract void RemoveCondition(ConditionPropertyMapping condition);
	}
}
