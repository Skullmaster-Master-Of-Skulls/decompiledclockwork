using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Mapping.ViewGeneration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000639 RID: 1593
	internal class ExplicitDiscriminatorMap
	{
		// Token: 0x06003E9D RID: 16029 RVA: 0x0011F5E0 File Offset: 0x0011D7E0
		internal ExplicitDiscriminatorMap(DiscriminatorMap template)
		{
			this.m_typeMap = template.TypeMap;
			this.m_discriminatorProperty = template.Discriminator.Property;
			this.m_properties = new ReadOnlyCollection<EdmProperty>((from propertyValuePair in template.PropertyMap
			select propertyValuePair.Key).ToList<EdmProperty>());
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06003E9E RID: 16030 RVA: 0x0011F648 File Offset: 0x0011D848
		internal ReadOnlyCollection<KeyValuePair<object, EntityType>> TypeMap
		{
			get
			{
				return this.m_typeMap;
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06003E9F RID: 16031 RVA: 0x0011F650 File Offset: 0x0011D850
		internal EdmMember DiscriminatorProperty
		{
			get
			{
				return this.m_discriminatorProperty;
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06003EA0 RID: 16032 RVA: 0x0011F658 File Offset: 0x0011D858
		internal ReadOnlyCollection<EdmProperty> Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x06003EA1 RID: 16033 RVA: 0x0011F660 File Offset: 0x0011D860
		internal object GetTypeId(EntityType entityType)
		{
			object result = null;
			foreach (KeyValuePair<object, EntityType> keyValuePair in this.TypeMap)
			{
				if (keyValuePair.Value.EdmEquals(entityType))
				{
					result = keyValuePair.Key;
					break;
				}
			}
			return result;
		}

		// Token: 0x0400176C RID: 5996
		private readonly ReadOnlyCollection<KeyValuePair<object, EntityType>> m_typeMap;

		// Token: 0x0400176D RID: 5997
		private readonly EdmMember m_discriminatorProperty;

		// Token: 0x0400176E RID: 5998
		private readonly ReadOnlyCollection<EdmProperty> m_properties;
	}
}
