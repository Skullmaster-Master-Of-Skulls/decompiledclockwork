using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x020002CC RID: 716
	public static class ListBindingHelper
	{
		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06002BC7 RID: 11207 RVA: 0x000C50C0 File Offset: 0x000C32C0
		private static Attribute[] BrowsableAttributeList
		{
			get
			{
				if (ListBindingHelper.browsableAttribute == null)
				{
					ListBindingHelper.browsableAttribute = new Attribute[]
					{
						new BrowsableAttribute(true)
					};
				}
				return ListBindingHelper.browsableAttribute;
			}
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x000C50E2 File Offset: 0x000C32E2
		public static object GetList(object list)
		{
			if (list is IListSource)
			{
				return (list as IListSource).GetList();
			}
			return list;
		}

		// Token: 0x06002BC9 RID: 11209 RVA: 0x000C50FC File Offset: 0x000C32FC
		public static object GetList(object dataSource, string dataMember)
		{
			dataSource = ListBindingHelper.GetList(dataSource);
			if (dataSource == null || dataSource is Type || string.IsNullOrEmpty(dataMember))
			{
				return dataSource;
			}
			PropertyDescriptorCollection listItemProperties = ListBindingHelper.GetListItemProperties(dataSource);
			PropertyDescriptor propertyDescriptor = listItemProperties.Find(dataMember, true);
			if (propertyDescriptor == null)
			{
				throw new ArgumentException(SR.GetString("DataSourceDataMemberPropNotFound", new object[]
				{
					dataMember
				}));
			}
			object obj;
			if (dataSource is ICurrencyManagerProvider)
			{
				CurrencyManager currencyManager = (dataSource as ICurrencyManagerProvider).CurrencyManager;
				obj = ((currencyManager != null && currencyManager.Position >= 0 && currencyManager.Position <= currencyManager.Count - 1) ? currencyManager.Current : null);
			}
			else if (dataSource is IEnumerable)
			{
				obj = ListBindingHelper.GetFirstItemByEnumerable(dataSource as IEnumerable);
			}
			else
			{
				obj = dataSource;
			}
			if (obj != null)
			{
				return propertyDescriptor.GetValue(obj);
			}
			return null;
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x000C51C0 File Offset: 0x000C33C0
		public static string GetListName(object list, PropertyDescriptor[] listAccessors)
		{
			if (list == null)
			{
				return string.Empty;
			}
			ITypedList typedList = list as ITypedList;
			string result;
			if (typedList != null)
			{
				result = typedList.GetListName(listAccessors);
			}
			else
			{
				Type type2;
				if (listAccessors == null || listAccessors.Length == 0)
				{
					Type type = list as Type;
					if (type != null)
					{
						type2 = type;
					}
					else
					{
						type2 = list.GetType();
					}
				}
				else
				{
					PropertyDescriptor propertyDescriptor = listAccessors[0];
					type2 = propertyDescriptor.PropertyType;
				}
				result = ListBindingHelper.GetListNameFromType(type2);
			}
			return result;
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x000C5224 File Offset: 0x000C3424
		public static PropertyDescriptorCollection GetListItemProperties(object list)
		{
			if (list == null)
			{
				return new PropertyDescriptorCollection(null);
			}
			PropertyDescriptorCollection result;
			if (list is Type)
			{
				result = ListBindingHelper.GetListItemPropertiesByType(list as Type);
			}
			else
			{
				object list2 = ListBindingHelper.GetList(list);
				if (list2 is ITypedList)
				{
					result = (list2 as ITypedList).GetItemProperties(null);
				}
				else if (list2 is IEnumerable)
				{
					result = ListBindingHelper.GetListItemPropertiesByEnumerable(list2 as IEnumerable);
				}
				else
				{
					result = TypeDescriptor.GetProperties(list2);
				}
			}
			return result;
		}

		// Token: 0x06002BCC RID: 11212 RVA: 0x000C5290 File Offset: 0x000C3490
		public static PropertyDescriptorCollection GetListItemProperties(object list, PropertyDescriptor[] listAccessors)
		{
			PropertyDescriptorCollection result;
			if (listAccessors == null || listAccessors.Length == 0)
			{
				result = ListBindingHelper.GetListItemProperties(list);
			}
			else if (list is Type)
			{
				result = ListBindingHelper.GetListItemPropertiesByType(list as Type, listAccessors);
			}
			else
			{
				object list2 = ListBindingHelper.GetList(list);
				if (list2 is ITypedList)
				{
					result = (list2 as ITypedList).GetItemProperties(listAccessors);
				}
				else if (list2 is IEnumerable)
				{
					result = ListBindingHelper.GetListItemPropertiesByEnumerable(list2 as IEnumerable, listAccessors);
				}
				else
				{
					result = ListBindingHelper.GetListItemPropertiesByInstance(list2, listAccessors, 0);
				}
			}
			return result;
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x000C5304 File Offset: 0x000C3504
		public static PropertyDescriptorCollection GetListItemProperties(object dataSource, string dataMember, PropertyDescriptor[] listAccessors)
		{
			dataSource = ListBindingHelper.GetList(dataSource);
			if (!string.IsNullOrEmpty(dataMember))
			{
				PropertyDescriptorCollection listItemProperties = ListBindingHelper.GetListItemProperties(dataSource);
				PropertyDescriptor propertyDescriptor = listItemProperties.Find(dataMember, true);
				if (propertyDescriptor == null)
				{
					throw new ArgumentException(SR.GetString("DataSourceDataMemberPropNotFound", new object[]
					{
						dataMember
					}));
				}
				int num = (listAccessors == null) ? 1 : (listAccessors.Length + 1);
				PropertyDescriptor[] array = new PropertyDescriptor[num];
				array[0] = propertyDescriptor;
				for (int i = 1; i < num; i++)
				{
					array[i] = listAccessors[i - 1];
				}
				listAccessors = array;
			}
			return ListBindingHelper.GetListItemProperties(dataSource, listAccessors);
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x000C538C File Offset: 0x000C358C
		public static Type GetListItemType(object list)
		{
			if (list == null)
			{
				return null;
			}
			if (list is Type && typeof(IListSource).IsAssignableFrom(list as Type))
			{
				list = ListBindingHelper.CreateInstanceOfType(list as Type);
			}
			list = ListBindingHelper.GetList(list);
			Type type = (list is Type) ? (list as Type) : list.GetType();
			object obj = (list is Type) ? null : list;
			Type result;
			if (typeof(Array).IsAssignableFrom(type))
			{
				result = type.GetElementType();
			}
			else
			{
				PropertyInfo typedIndexer = ListBindingHelper.GetTypedIndexer(type);
				if (typedIndexer != null)
				{
					result = typedIndexer.PropertyType;
				}
				else if (obj is IEnumerable)
				{
					result = ListBindingHelper.GetListItemTypeByEnumerable(obj as IEnumerable);
				}
				else
				{
					result = type;
				}
			}
			return result;
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x000C5448 File Offset: 0x000C3648
		private static object CreateInstanceOfType(Type type)
		{
			object result = null;
			Exception ex = null;
			try
			{
				result = SecurityUtils.SecureCreateInstance(type);
			}
			catch (TargetInvocationException ex2)
			{
				ex = ex2;
			}
			catch (MethodAccessException ex3)
			{
				ex = ex3;
			}
			catch (MissingMethodException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				throw new NotSupportedException(SR.GetString("BindingSourceInstanceError"), ex);
			}
			return result;
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x000C54B0 File Offset: 0x000C36B0
		public static Type GetListItemType(object dataSource, string dataMember)
		{
			if (dataSource == null)
			{
				return typeof(object);
			}
			if (string.IsNullOrEmpty(dataMember))
			{
				return ListBindingHelper.GetListItemType(dataSource);
			}
			PropertyDescriptorCollection listItemProperties = ListBindingHelper.GetListItemProperties(dataSource);
			if (listItemProperties == null)
			{
				return typeof(object);
			}
			PropertyDescriptor propertyDescriptor = listItemProperties.Find(dataMember, true);
			if (propertyDescriptor == null || propertyDescriptor.PropertyType is ICustomTypeDescriptor)
			{
				return typeof(object);
			}
			return ListBindingHelper.GetListItemType(propertyDescriptor.PropertyType);
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x000C5520 File Offset: 0x000C3720
		private static string GetListNameFromType(Type type)
		{
			string name;
			if (typeof(Array).IsAssignableFrom(type))
			{
				name = type.GetElementType().Name;
			}
			else if (typeof(IList).IsAssignableFrom(type))
			{
				PropertyInfo typedIndexer = ListBindingHelper.GetTypedIndexer(type);
				if (typedIndexer != null)
				{
					name = typedIndexer.PropertyType.Name;
				}
				else
				{
					name = type.Name;
				}
			}
			else
			{
				name = type.Name;
			}
			return name;
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x000C5590 File Offset: 0x000C3790
		private static PropertyDescriptorCollection GetListItemPropertiesByType(Type type, PropertyDescriptor[] listAccessors)
		{
			PropertyDescriptorCollection listItemPropertiesByType;
			if (listAccessors == null || listAccessors.Length == 0)
			{
				listItemPropertiesByType = ListBindingHelper.GetListItemPropertiesByType(type);
			}
			else
			{
				listItemPropertiesByType = ListBindingHelper.GetListItemPropertiesByType(type, listAccessors, 0);
			}
			return listItemPropertiesByType;
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x000C55BC File Offset: 0x000C37BC
		private static PropertyDescriptorCollection GetListItemPropertiesByType(Type type, PropertyDescriptor[] listAccessors, int startIndex)
		{
			Type propertyType = listAccessors[startIndex].PropertyType;
			startIndex++;
			PropertyDescriptorCollection result;
			if (startIndex >= listAccessors.Length)
			{
				result = ListBindingHelper.GetListItemProperties(propertyType);
			}
			else
			{
				result = ListBindingHelper.GetListItemPropertiesByType(propertyType, listAccessors, startIndex);
			}
			return result;
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x000C55F4 File Offset: 0x000C37F4
		private static PropertyDescriptorCollection GetListItemPropertiesByEnumerable(IEnumerable iEnumerable, PropertyDescriptor[] listAccessors, int startIndex)
		{
			object obj = null;
			object firstItemByEnumerable = ListBindingHelper.GetFirstItemByEnumerable(iEnumerable);
			if (firstItemByEnumerable != null)
			{
				obj = ListBindingHelper.GetList(listAccessors[startIndex].GetValue(firstItemByEnumerable));
			}
			PropertyDescriptorCollection result;
			if (obj == null)
			{
				result = ListBindingHelper.GetListItemPropertiesByType(listAccessors[startIndex].PropertyType, listAccessors, startIndex);
			}
			else
			{
				startIndex++;
				IEnumerable enumerable = obj as IEnumerable;
				if (enumerable != null)
				{
					if (startIndex == listAccessors.Length)
					{
						result = ListBindingHelper.GetListItemPropertiesByEnumerable(enumerable);
					}
					else
					{
						result = ListBindingHelper.GetListItemPropertiesByEnumerable(enumerable, listAccessors, startIndex);
					}
				}
				else
				{
					result = ListBindingHelper.GetListItemPropertiesByInstance(obj, listAccessors, startIndex);
				}
			}
			return result;
		}

		// Token: 0x06002BD5 RID: 11221 RVA: 0x000C5668 File Offset: 0x000C3868
		private static PropertyDescriptorCollection GetListItemPropertiesByEnumerable(IEnumerable enumerable, PropertyDescriptor[] listAccessors)
		{
			PropertyDescriptorCollection result;
			if (listAccessors == null || listAccessors.Length == 0)
			{
				result = ListBindingHelper.GetListItemPropertiesByEnumerable(enumerable);
			}
			else
			{
				ITypedList typedList = enumerable as ITypedList;
				if (typedList != null)
				{
					result = typedList.GetItemProperties(listAccessors);
				}
				else
				{
					result = ListBindingHelper.GetListItemPropertiesByEnumerable(enumerable, listAccessors, 0);
				}
			}
			return result;
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x000C56A8 File Offset: 0x000C38A8
		private static Type GetListItemTypeByEnumerable(IEnumerable iEnumerable)
		{
			object firstItemByEnumerable = ListBindingHelper.GetFirstItemByEnumerable(iEnumerable);
			if (firstItemByEnumerable == null)
			{
				return typeof(object);
			}
			return firstItemByEnumerable.GetType();
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x000C56D0 File Offset: 0x000C38D0
		private static PropertyDescriptorCollection GetListItemPropertiesByInstance(object target, PropertyDescriptor[] listAccessors, int startIndex)
		{
			PropertyDescriptorCollection result;
			if (listAccessors != null && listAccessors.Length > startIndex)
			{
				object value = listAccessors[startIndex].GetValue(target);
				if (value == null)
				{
					result = ListBindingHelper.GetListItemPropertiesByType(listAccessors[startIndex].PropertyType, listAccessors, startIndex);
				}
				else
				{
					PropertyDescriptor[] array = null;
					if (listAccessors.Length > startIndex + 1)
					{
						int num = listAccessors.Length - (startIndex + 1);
						array = new PropertyDescriptor[num];
						for (int i = 0; i < num; i++)
						{
							array[i] = listAccessors[startIndex + 1 + i];
						}
					}
					result = ListBindingHelper.GetListItemProperties(value, array);
				}
			}
			else
			{
				result = TypeDescriptor.GetProperties(target, ListBindingHelper.BrowsableAttributeList);
			}
			return result;
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x000C5754 File Offset: 0x000C3954
		private static bool IsListBasedType(Type type)
		{
			if (typeof(IList).IsAssignableFrom(type) || typeof(ITypedList).IsAssignableFrom(type) || typeof(IListSource).IsAssignableFrom(type))
			{
				return true;
			}
			if (type.IsGenericType && !type.IsGenericTypeDefinition && typeof(IList<>).IsAssignableFrom(type.GetGenericTypeDefinition()))
			{
				return true;
			}
			foreach (Type type2 in type.GetInterfaces())
			{
				if (type2.IsGenericType && typeof(IList<>).IsAssignableFrom(type2.GetGenericTypeDefinition()))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x000C5800 File Offset: 0x000C3A00
		private static PropertyInfo GetTypedIndexer(Type type)
		{
			PropertyInfo propertyInfo = null;
			if (!ListBindingHelper.IsListBasedType(type))
			{
				return null;
			}
			PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < properties.Length; i++)
			{
				if (properties[i].GetIndexParameters().Length != 0 && properties[i].PropertyType != typeof(object))
				{
					propertyInfo = properties[i];
					if (propertyInfo.Name == "Item")
					{
						break;
					}
				}
			}
			return propertyInfo;
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x000C586B File Offset: 0x000C3A6B
		private static PropertyDescriptorCollection GetListItemPropertiesByType(Type type)
		{
			return TypeDescriptor.GetProperties(ListBindingHelper.GetListItemType(type), ListBindingHelper.BrowsableAttributeList);
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x000C5880 File Offset: 0x000C3A80
		private static PropertyDescriptorCollection GetListItemPropertiesByEnumerable(IEnumerable enumerable)
		{
			PropertyDescriptorCollection propertyDescriptorCollection = null;
			Type type = enumerable.GetType();
			if (typeof(Array).IsAssignableFrom(type))
			{
				propertyDescriptorCollection = TypeDescriptor.GetProperties(type.GetElementType(), ListBindingHelper.BrowsableAttributeList);
			}
			else
			{
				ITypedList typedList = enumerable as ITypedList;
				if (typedList != null)
				{
					propertyDescriptorCollection = typedList.GetItemProperties(null);
				}
				else
				{
					PropertyInfo typedIndexer = ListBindingHelper.GetTypedIndexer(type);
					if (typedIndexer != null && !typeof(ICustomTypeDescriptor).IsAssignableFrom(typedIndexer.PropertyType))
					{
						Type propertyType = typedIndexer.PropertyType;
						propertyDescriptorCollection = TypeDescriptor.GetProperties(propertyType, ListBindingHelper.BrowsableAttributeList);
					}
				}
			}
			if (propertyDescriptorCollection == null)
			{
				object firstItemByEnumerable = ListBindingHelper.GetFirstItemByEnumerable(enumerable);
				if (enumerable is string)
				{
					propertyDescriptorCollection = TypeDescriptor.GetProperties(enumerable, ListBindingHelper.BrowsableAttributeList);
				}
				else if (firstItemByEnumerable == null)
				{
					propertyDescriptorCollection = new PropertyDescriptorCollection(null);
				}
				else
				{
					propertyDescriptorCollection = TypeDescriptor.GetProperties(firstItemByEnumerable, ListBindingHelper.BrowsableAttributeList);
					if (!(enumerable is IList) && (propertyDescriptorCollection == null || propertyDescriptorCollection.Count == 0))
					{
						propertyDescriptorCollection = TypeDescriptor.GetProperties(enumerable, ListBindingHelper.BrowsableAttributeList);
					}
				}
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x000C5968 File Offset: 0x000C3B68
		private static object GetFirstItemByEnumerable(IEnumerable enumerable)
		{
			object result = null;
			if (enumerable is IList)
			{
				IList list = enumerable as IList;
				result = ((list.Count > 0) ? list[0] : null);
			}
			else
			{
				try
				{
					IEnumerator enumerator = enumerable.GetEnumerator();
					enumerator.Reset();
					if (enumerator.MoveNext())
					{
						result = enumerator.Current;
					}
					enumerator.Reset();
				}
				catch (NotSupportedException)
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0400125C RID: 4700
		private static Attribute[] browsableAttribute;
	}
}
