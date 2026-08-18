using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x0200028E RID: 654
	internal static class ImageListUtils
	{
		// Token: 0x060018F7 RID: 6391 RVA: 0x0008BD9C File Offset: 0x00089F9C
		public static PropertyDescriptor GetImageListProperty(PropertyDescriptor currentComponent, ref object instance)
		{
			if (instance is object[])
			{
				return null;
			}
			PropertyDescriptor result = null;
			object obj = instance;
			RelatedImageListAttribute relatedImageListAttribute = currentComponent.Attributes[typeof(RelatedImageListAttribute)] as RelatedImageListAttribute;
			if (relatedImageListAttribute != null)
			{
				string[] array = relatedImageListAttribute.RelatedImageList.Split(new char[]
				{
					'.'
				});
				int num = 0;
				while (num < array.Length && obj != null)
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(obj)[array[num]];
					if (propertyDescriptor == null)
					{
						break;
					}
					if (num == array.Length - 1)
					{
						if (typeof(ImageList).IsAssignableFrom(propertyDescriptor.PropertyType))
						{
							instance = obj;
							result = propertyDescriptor;
							break;
						}
					}
					else
					{
						obj = propertyDescriptor.GetValue(obj);
					}
					num++;
				}
			}
			return result;
		}
	}
}
