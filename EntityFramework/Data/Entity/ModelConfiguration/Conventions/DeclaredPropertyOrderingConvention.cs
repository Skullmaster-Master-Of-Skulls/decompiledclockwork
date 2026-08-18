using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.ModelConfiguration.Mappers;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020007F7 RID: 2039
	public class DeclaredPropertyOrderingConvention : IConceptualModelConvention<EntityType>, IConvention
	{
		// Token: 0x06005C4F RID: 23631 RVA: 0x0018E2E4 File Offset: 0x0018C4E4
		public virtual void Apply(EntityType item, DbModel model)
		{
			Check.NotNull<EntityType>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			if (item.BaseType == null)
			{
				foreach (EdmProperty member in item.KeyProperties)
				{
					item.RemoveMember(member);
					item.AddKeyMember(member);
				}
				using (IEnumerator<PropertyInfo> enumerator2 = new PropertyFilter(DbModelBuilderVersion.Latest).GetProperties(item.GetClrType(), false, null, null, true).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						PropertyInfo p = enumerator2.Current;
						EdmProperty edmProperty = item.DeclaredProperties.SingleOrDefault((EdmProperty ep) => ep.Name == p.Name);
						if (edmProperty != null && !item.KeyProperties.Contains(edmProperty))
						{
							item.RemoveMember(edmProperty);
							item.AddMember(edmProperty);
						}
					}
				}
			}
		}
	}
}
