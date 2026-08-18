using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004ED RID: 1261
	internal sealed class MetadataPropertyCollection : MetadataCollection<MetadataProperty>
	{
		// Token: 0x06002F00 RID: 12032 RVA: 0x000E08B7 File Offset: 0x000DEAB7
		internal MetadataPropertyCollection(MetadataItem item) : base(MetadataPropertyCollection.GetSystemMetadataProperties(item))
		{
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x000E08C8 File Offset: 0x000DEAC8
		private static IEnumerable<MetadataProperty> GetSystemMetadataProperties(MetadataItem item)
		{
			Type type = item.GetType();
			MetadataPropertyCollection.ItemTypeInformation itemTypeInformation = MetadataPropertyCollection.GetItemTypeInformation(type);
			return itemTypeInformation.GetItemAttributes(item);
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x000E08EA File Offset: 0x000DEAEA
		private static MetadataPropertyCollection.ItemTypeInformation GetItemTypeInformation(Type clrType)
		{
			return MetadataPropertyCollection._itemTypeMemoizer.Evaluate(clrType);
		}

		// Token: 0x040011D5 RID: 4565
		private static readonly Memoizer<Type, MetadataPropertyCollection.ItemTypeInformation> _itemTypeMemoizer = new Memoizer<Type, MetadataPropertyCollection.ItemTypeInformation>((Type clrType) => new MetadataPropertyCollection.ItemTypeInformation(clrType), null);

		// Token: 0x020004EE RID: 1262
		private class ItemTypeInformation
		{
			// Token: 0x06002F05 RID: 12037 RVA: 0x000E0929 File Offset: 0x000DEB29
			internal ItemTypeInformation(Type clrType)
			{
				this._itemProperties = MetadataPropertyCollection.ItemTypeInformation.GetItemProperties(clrType);
			}

			// Token: 0x06002F06 RID: 12038 RVA: 0x000E0ADC File Offset: 0x000DECDC
			internal IEnumerable<MetadataProperty> GetItemAttributes(MetadataItem item)
			{
				foreach (MetadataPropertyCollection.ItemPropertyInfo propertyInfo in this._itemProperties)
				{
					yield return propertyInfo.GetMetadataProperty(item);
				}
				yield break;
			}

			// Token: 0x06002F07 RID: 12039 RVA: 0x000E0B00 File Offset: 0x000DED00
			private static List<MetadataPropertyCollection.ItemPropertyInfo> GetItemProperties(Type clrType)
			{
				List<MetadataPropertyCollection.ItemPropertyInfo> list = new List<MetadataPropertyCollection.ItemPropertyInfo>();
				foreach (PropertyInfo propertyInfo in clrType.GetInstanceProperties())
				{
					foreach (MetadataPropertyAttribute attribute in propertyInfo.GetCustomAttributes(false))
					{
						list.Add(new MetadataPropertyCollection.ItemPropertyInfo(propertyInfo, attribute));
					}
				}
				return list;
			}

			// Token: 0x040011D7 RID: 4567
			private readonly List<MetadataPropertyCollection.ItemPropertyInfo> _itemProperties;
		}

		// Token: 0x020004EF RID: 1263
		private class ItemPropertyInfo
		{
			// Token: 0x06002F08 RID: 12040 RVA: 0x000E0B98 File Offset: 0x000DED98
			internal ItemPropertyInfo(PropertyInfo propertyInfo, MetadataPropertyAttribute attribute)
			{
				this._propertyInfo = propertyInfo;
				this._attribute = attribute;
			}

			// Token: 0x06002F09 RID: 12041 RVA: 0x000E0BAE File Offset: 0x000DEDAE
			internal MetadataProperty GetMetadataProperty(MetadataItem item)
			{
				return new MetadataProperty(this._propertyInfo.Name, this._attribute.Type, this._attribute.IsCollectionType, new MetadataPropertyValue(this._propertyInfo, item));
			}

			// Token: 0x040011D8 RID: 4568
			private readonly MetadataPropertyAttribute _attribute;

			// Token: 0x040011D9 RID: 4569
			private readonly PropertyInfo _propertyInfo;
		}
	}
}
