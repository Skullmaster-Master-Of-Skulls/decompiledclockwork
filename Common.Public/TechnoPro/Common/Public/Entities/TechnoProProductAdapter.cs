using System;
using System.Reflection;

namespace TechnoPro.Common.Public.Entities
{
	// Token: 0x020000DF RID: 223
	public static class TechnoProProductAdapter
	{
		// Token: 0x0600053E RID: 1342 RVA: 0x0000E2F4 File Offset: 0x0000C4F4
		public static string GetProductTitle(this eTechnoProProductNames product)
		{
			return product.GetProductDescriptionAttribute<ProductDescriptionAttribute>().ProductTitle;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0000E318 File Offset: 0x0000C518
		public static string GetProductDescription(this eTechnoProProductNames product)
		{
			return product.GetProductDescriptionAttribute<ProductDescriptionAttribute>().ProductDescription;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0000E33C File Offset: 0x0000C53C
		private static T GetProductDescriptionAttribute<T>(this Enum item) where T : Attribute
		{
			Type type = item.GetType();
			FieldInfo field = type.GetField(item.ToString());
			T[] array = field.GetCustomAttributes(typeof(T), false) as T[];
			return (array != null && array.Length != 0) ? array[0] : default(T);
		}
	}
}
