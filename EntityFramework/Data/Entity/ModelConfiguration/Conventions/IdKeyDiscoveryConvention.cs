using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020007FC RID: 2044
	public class IdKeyDiscoveryConvention : KeyDiscoveryConvention
	{
		// Token: 0x06005C64 RID: 23652 RVA: 0x0018EF34 File Offset: 0x0018D134
		protected override IEnumerable<EdmProperty> MatchKeyProperty(EntityType entityType, IEnumerable<EdmProperty> primitiveProperties)
		{
			Check.NotNull<EntityType>(entityType, "entityType");
			Check.NotNull<IEnumerable<EdmProperty>>(primitiveProperties, "primitiveProperties");
			IEnumerable<EdmProperty> enumerable = from p in primitiveProperties
			where "Id".Equals(p.Name, StringComparison.OrdinalIgnoreCase)
			select p;
			if (!enumerable.Any<EdmProperty>())
			{
				enumerable = from p in primitiveProperties
				where (entityType.Name + "Id").Equals(p.Name, StringComparison.OrdinalIgnoreCase)
				select p;
			}
			if (enumerable.Count<EdmProperty>() > 1)
			{
				throw Error.MultiplePropertiesMatchedAsKeys(enumerable.First<EdmProperty>().Name, entityType.Name);
			}
			return enumerable;
		}

		// Token: 0x040024A8 RID: 9384
		private const string Id = "Id";
	}
}
