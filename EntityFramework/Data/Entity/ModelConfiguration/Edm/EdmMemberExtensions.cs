using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x0200081A RID: 2074
	internal static class EdmMemberExtensions
	{
		// Token: 0x06005D4F RID: 23887 RVA: 0x00192B40 File Offset: 0x00190D40
		public static PropertyInfo GetClrPropertyInfo(this EdmMember property)
		{
			return property.Annotations.GetClrPropertyInfo();
		}

		// Token: 0x06005D50 RID: 23888 RVA: 0x00192B4D File Offset: 0x00190D4D
		public static void SetClrPropertyInfo(this EdmMember property, PropertyInfo propertyInfo)
		{
			property.GetMetadataProperties().SetClrPropertyInfo(propertyInfo);
		}

		// Token: 0x06005D51 RID: 23889 RVA: 0x00192B5C File Offset: 0x00190D5C
		public static IEnumerable<T> GetClrAttributes<T>(this EdmMember property) where T : Attribute
		{
			IList<Attribute> clrAttributes = property.Annotations.GetClrAttributes();
			if (clrAttributes == null)
			{
				return Enumerable.Empty<T>();
			}
			return clrAttributes.OfType<T>();
		}
	}
}
