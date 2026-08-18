using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.CommandTrees;
using System.Data.Mapping.ViewGeneration;
using System.Data.Metadata.Edm;
using System.Linq;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x02000094 RID: 148
	internal class ExplicitDiscriminatorMap
	{
		// Token: 0x060009E2 RID: 2530 RVA: 0x00035A08 File Offset: 0x00033C08
		internal ExplicitDiscriminatorMap(DiscriminatorMap template)
		{
			this.m_typeMap = template.TypeMap;
			this.m_discriminatorProperty = template.Discriminator.Property;
			this.m_properties = (from propertyValuePair in template.PropertyMap
			select propertyValuePair.Key).ToList<EdmProperty>().AsReadOnly();
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x00035A72 File Offset: 0x00033C72
		internal ReadOnlyCollection<KeyValuePair<object, EntityType>> TypeMap
		{
			get
			{
				return this.m_typeMap;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x00035A7A File Offset: 0x00033C7A
		internal EdmMember DiscriminatorProperty
		{
			get
			{
				return this.m_discriminatorProperty;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x00035A82 File Offset: 0x00033C82
		internal ReadOnlyCollection<EdmProperty> Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00035A8C File Offset: 0x00033C8C
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

		// Token: 0x040008A4 RID: 2212
		private readonly ReadOnlyCollection<KeyValuePair<object, EntityType>> m_typeMap;

		// Token: 0x040008A5 RID: 2213
		private readonly EdmMember m_discriminatorProperty;

		// Token: 0x040008A6 RID: 2214
		private readonly ReadOnlyCollection<EdmProperty> m_properties;
	}
}
