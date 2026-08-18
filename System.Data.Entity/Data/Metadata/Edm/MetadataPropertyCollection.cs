using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Reflection;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001E5 RID: 485
	internal sealed class MetadataPropertyCollection : MetadataCollection<MetadataProperty>
	{
		// Token: 0x060020AE RID: 8366 RVA: 0x0007249A File Offset: 0x0007069A
		internal MetadataPropertyCollection(MetadataItem item) : base(MetadataPropertyCollection.GetSystemMetadataProperties(item))
		{
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x000724A8 File Offset: 0x000706A8
		private static IEnumerable<MetadataProperty> GetSystemMetadataProperties(MetadataItem item)
		{
			EntityUtil.CheckArgumentNull<MetadataItem>(item, "item");
			Type type = item.GetType();
			MetadataPropertyCollection.ItemTypeInformation itemTypeInformation = MetadataPropertyCollection.GetItemTypeInformation(type);
			return itemTypeInformation.GetItemAttributes(item);
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x000724D6 File Offset: 0x000706D6
		private static MetadataPropertyCollection.ItemTypeInformation GetItemTypeInformation(Type clrType)
		{
			return MetadataPropertyCollection.s_itemTypeMemoizer.Evaluate(clrType);
		}

		// Token: 0x04000E56 RID: 3670
		private static readonly Memoizer<Type, MetadataPropertyCollection.ItemTypeInformation> s_itemTypeMemoizer = new Memoizer<Type, MetadataPropertyCollection.ItemTypeInformation>((Type clrType) => new MetadataPropertyCollection.ItemTypeInformation(clrType), null);

		// Token: 0x0200051D RID: 1309
		private class ItemTypeInformation
		{
			// Token: 0x06003DEA RID: 15850 RVA: 0x000E753C File Offset: 0x000E573C
			internal ItemTypeInformation(Type clrType)
			{
				this._itemProperties = MetadataPropertyCollection.ItemTypeInformation.GetItemProperties(clrType);
			}

			// Token: 0x06003DEB RID: 15851 RVA: 0x000E7550 File Offset: 0x000E5750
			internal IEnumerable<MetadataProperty> GetItemAttributes(MetadataItem item)
			{
				foreach (MetadataPropertyCollection.ItemPropertyInfo itemPropertyInfo in this._itemProperties)
				{
					yield return itemPropertyInfo.GetMetadataProperty(item);
				}
				List<MetadataPropertyCollection.ItemPropertyInfo>.Enumerator enumerator = default(List<MetadataPropertyCollection.ItemPropertyInfo>.Enumerator);
				yield break;
				yield break;
			}

			// Token: 0x06003DEC RID: 15852 RVA: 0x000E7568 File Offset: 0x000E5768
			private static List<MetadataPropertyCollection.ItemPropertyInfo> GetItemProperties(Type clrType)
			{
				List<MetadataPropertyCollection.ItemPropertyInfo> list = new List<MetadataPropertyCollection.ItemPropertyInfo>();
				foreach (PropertyInfo propertyInfo in clrType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
				{
					foreach (MetadataPropertyAttribute attribute in propertyInfo.GetCustomAttributes(typeof(MetadataPropertyAttribute), false))
					{
						list.Add(new MetadataPropertyCollection.ItemPropertyInfo(propertyInfo, attribute));
					}
				}
				return list;
			}

			// Token: 0x04001B40 RID: 6976
			private readonly List<MetadataPropertyCollection.ItemPropertyInfo> _itemProperties;
		}

		// Token: 0x0200051E RID: 1310
		private class ItemPropertyInfo
		{
			// Token: 0x06003DED RID: 15853 RVA: 0x000E75D7 File Offset: 0x000E57D7
			internal ItemPropertyInfo(PropertyInfo propertyInfo, MetadataPropertyAttribute attribute)
			{
				this._propertyInfo = propertyInfo;
				this._attribute = attribute;
			}

			// Token: 0x06003DEE RID: 15854 RVA: 0x000E75ED File Offset: 0x000E57ED
			internal MetadataProperty GetMetadataProperty(MetadataItem item)
			{
				return new MetadataProperty(this._propertyInfo.Name, this._attribute.Type, this._attribute.IsCollectionType, new MetadataPropertyValue(this._propertyInfo, item));
			}

			// Token: 0x04001B41 RID: 6977
			private readonly MetadataPropertyAttribute _attribute;

			// Token: 0x04001B42 RID: 6978
			private readonly PropertyInfo _propertyInfo;
		}
	}
}
