using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Mappers
{
	// Token: 0x02000822 RID: 2082
	internal sealed class AttributeMapper
	{
		// Token: 0x06005D8C RID: 23948 RVA: 0x00194194 File Offset: 0x00192394
		public AttributeMapper(AttributeProvider attributeProvider)
		{
			this._attributeProvider = attributeProvider;
		}

		// Token: 0x06005D8D RID: 23949 RVA: 0x001941A3 File Offset: 0x001923A3
		public void Map(PropertyInfo propertyInfo, ICollection<MetadataProperty> annotations)
		{
			annotations.SetClrAttributes(this._attributeProvider.GetAttributes(propertyInfo).ToList<Attribute>());
		}

		// Token: 0x06005D8E RID: 23950 RVA: 0x001941BC File Offset: 0x001923BC
		public void Map(Type type, ICollection<MetadataProperty> annotations)
		{
			annotations.SetClrAttributes(this._attributeProvider.GetAttributes(type).ToList<Attribute>());
		}

		// Token: 0x040024F5 RID: 9461
		private readonly AttributeProvider _attributeProvider;
	}
}
