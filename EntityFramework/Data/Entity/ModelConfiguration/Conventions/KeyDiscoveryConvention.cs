using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020007FB RID: 2043
	public abstract class KeyDiscoveryConvention : IConceptualModelConvention<EntityType>, IConvention
	{
		// Token: 0x06005C61 RID: 23649 RVA: 0x0018EE60 File Offset: 0x0018D060
		public virtual void Apply(EntityType item, DbModel model)
		{
			Check.NotNull<EntityType>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			if (item.KeyProperties.Count > 0 || item.BaseType != null)
			{
				return;
			}
			IEnumerable<EdmProperty> enumerable = this.MatchKeyProperty(item, item.GetDeclaredPrimitiveProperties());
			foreach (EdmProperty edmProperty in enumerable)
			{
				edmProperty.Nullable = false;
				item.AddKeyMember(edmProperty);
			}
		}

		// Token: 0x06005C62 RID: 23650
		protected abstract IEnumerable<EdmProperty> MatchKeyProperty(EntityType entityType, IEnumerable<EdmProperty> primitiveProperties);
	}
}
