using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	// Token: 0x020001BE RID: 446
	internal static class RuntimeComponentFilter
	{
		// Token: 0x0600102A RID: 4138 RVA: 0x0005B68C File Offset: 0x0005988C
		public static void FilterProperties(IDictionary properties, ICollection makeReadWrite, ICollection makeBrowsable)
		{
			RuntimeComponentFilter.FilterProperties(properties, makeReadWrite, makeBrowsable, null);
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0005B698 File Offset: 0x00059898
		public static void FilterProperties(IDictionary properties, ICollection makeReadWrite, ICollection makeBrowsable, bool[] browsableSettings)
		{
			if (makeReadWrite != null)
			{
				foreach (object obj in makeReadWrite)
				{
					string key = (string)obj;
					PropertyDescriptor propertyDescriptor = properties[key] as PropertyDescriptor;
					if (propertyDescriptor != null)
					{
						properties[key] = TypeDescriptor.CreateProperty(propertyDescriptor.ComponentType, propertyDescriptor, new Attribute[]
						{
							ReadOnlyAttribute.No
						});
					}
				}
			}
			if (makeBrowsable != null)
			{
				int num = -1;
				foreach (object obj2 in makeBrowsable)
				{
					string key2 = (string)obj2;
					PropertyDescriptor propertyDescriptor2 = properties[key2] as PropertyDescriptor;
					num++;
					if (propertyDescriptor2 != null)
					{
						Attribute attribute;
						if (browsableSettings == null || browsableSettings[num])
						{
							attribute = BrowsableAttribute.Yes;
						}
						else
						{
							attribute = BrowsableAttribute.No;
						}
						properties[key2] = TypeDescriptor.CreateProperty(propertyDescriptor2.ComponentType, propertyDescriptor2, new Attribute[]
						{
							attribute
						});
					}
				}
			}
		}
	}
}
