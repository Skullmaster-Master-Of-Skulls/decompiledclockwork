using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Telerik.Web.UI.PivotGrid.Core.Fields
{
	// Token: 0x02000CB2 RID: 3250
	internal class EnumerableFieldDescriptionsExtractor : IFieldInfoExtractor
	{
		// Token: 0x060079A6 RID: 31142 RVA: 0x001BF06A File Offset: 0x001BD26A
		public EnumerableFieldDescriptionsExtractor(IEnumerable source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.source = source;
		}

		// Token: 0x060079A7 RID: 31143 RVA: 0x001BF087 File Offset: 0x001BD287
		public IEnumerable<IPivotFieldInfo> GetDescriptions()
		{
			return this.GetDescriptionsFromTypedList();
		}

		// Token: 0x060079A8 RID: 31144 RVA: 0x001BF090 File Offset: 0x001BD290
		private IEnumerable<IPivotFieldInfo> GetDescriptionsFromTypedList()
		{
			ITypedList typedList = this.source as ITypedList;
			IList<IPivotFieldInfo> result;
			if (typedList != null)
			{
				result = EnumerableFieldDescriptionsExtractor.GetDesctiptionsFromTypedList(typedList);
			}
			else
			{
				result = this.GetDescriptionsFromEnumerable();
			}
			return result;
		}

		// Token: 0x060079A9 RID: 31145 RVA: 0x001BF0C0 File Offset: 0x001BD2C0
		private static IList<IPivotFieldInfo> GetDesctiptionsFromTypedList(ITypedList typedList)
		{
			PropertyDescriptorCollection itemProperties = typedList.GetItemProperties(null);
			return EnumerableFieldDescriptionsExtractor.GetDescriptionsForPropertyCollection(itemProperties);
		}

		// Token: 0x060079AA RID: 31146 RVA: 0x001BF0DB File Offset: 0x001BD2DB
		private static IList<IPivotFieldInfo> GetDescriptionsForPropertyCollection(PropertyDescriptorCollection properties)
		{
			return EnumerableFieldDescriptionsExtractor.GetDescriptionsForPropertyDescriptors(properties.OfType<PropertyDescriptor>());
		}

		// Token: 0x060079AB RID: 31147 RVA: 0x001BF0E8 File Offset: 0x001BD2E8
		private static IList<IPivotFieldInfo> GetDescriptionsForPropertyDescriptors(IEnumerable<PropertyDescriptor> propertyDescriptors)
		{
			List<IPivotFieldInfo> list = new List<IPivotFieldInfo>();
			foreach (PropertyDescriptor propertyDescriptor in propertyDescriptors)
			{
				list.Add(new PropertyDescriptorFieldInfo(propertyDescriptor)
				{
					PreferredRole = FieldInfoHelper.GetRoleForType(propertyDescriptor.PropertyType)
				});
			}
			return list;
		}

		// Token: 0x060079AC RID: 31148 RVA: 0x001BF150 File Offset: 0x001BD350
		private static IList<IPivotFieldInfo> GetDescriptionsFromCustomTypeDescriptor(object component)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component);
			return EnumerableFieldDescriptionsExtractor.GetDescriptionsForPropertyCollection(properties);
		}

		// Token: 0x060079AD RID: 31149 RVA: 0x001BF16C File Offset: 0x001BD36C
		private IList<IPivotFieldInfo> GetDescriptionsFromEnumerable()
		{
			IList<IPivotFieldInfo> result = new List<IPivotFieldInfo>();
			EnumerableFieldDescriptionsExtractor.TypeInformation typeInformation = this.ExtractTypeInformation();
			if (!typeInformation.IsValid())
			{
				return result;
			}
			return EnumerableFieldDescriptionsExtractor.GetDescriptionsForItemType(typeInformation);
		}

		// Token: 0x060079AE RID: 31150 RVA: 0x001BF198 File Offset: 0x001BD398
		private EnumerableFieldDescriptionsExtractor.TypeInformation ExtractTypeInformation()
		{
			Type type = this.source.GetType();
			Type type2 = EnumerableFieldDescriptionsExtractor.TryGetConcreteTypeFromGenericArguments(type);
			Type propertiesProviderType = type2;
			object obj = this.TryGetFirstItemFromSource();
			if (type2 == null && obj != null)
			{
				type2 = obj.GetType();
				propertiesProviderType = obj.GetType();
			}
			return new EnumerableFieldDescriptionsExtractor.TypeInformation(type2)
			{
				PropertiesProviderType = propertiesProviderType,
				ItemInstance = obj
			};
		}

		// Token: 0x060079AF RID: 31151 RVA: 0x001BF1F8 File Offset: 0x001BD3F8
		private static Type TryGetConcreteTypeFromGenericArguments(Type sourceType)
		{
			Type type = sourceType.GetGenericArguments().FirstOrDefault<Type>();
			if (type == typeof(object))
			{
				return null;
			}
			return type;
		}

		// Token: 0x060079B0 RID: 31152 RVA: 0x001BF228 File Offset: 0x001BD428
		private object TryGetFirstItemFromSource()
		{
			object result = null;
			using (IEnumerator enumerator = this.source.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					result = obj;
				}
			}
			return result;
		}

		// Token: 0x060079B1 RID: 31153 RVA: 0x001BF27C File Offset: 0x001BD47C
		private static IList<IPivotFieldInfo> GetDescriptionsForItemType(EnumerableFieldDescriptionsExtractor.TypeInformation typeInfo)
		{
			typeInfo.DynamicPropertyAccess = false;
			Type typeFromHandle = typeof(ICustomTypeDescriptor);
			if (typeFromHandle.IsAssignableFrom(typeInfo.ItemType))
			{
				return EnumerableFieldDescriptionsExtractor.GetDescriptionsFromCustomTypeDescriptor(typeInfo.ItemInstance);
			}
			return EnumerableFieldDescriptionsExtractor.GetPropertyInfosForType(typeInfo);
		}

		// Token: 0x060079B2 RID: 31154 RVA: 0x001BF2C0 File Offset: 0x001BD4C0
		private static IList<IPivotFieldInfo> GetPropertyInfosForType(EnumerableFieldDescriptionsExtractor.TypeInformation typeInfo)
		{
			List<IPivotFieldInfo> list = new List<IPivotFieldInfo>();
			PropertyInfo[] properties = typeInfo.PropertiesProviderType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			foreach (PropertyInfo propertyInfo in properties)
			{
				Func<object, object> propertyAccess;
				if (typeInfo.DynamicPropertyAccess)
				{
					propertyAccess = BindingExpressionHelper.CreateGetValueFuncReflection(typeInfo.ItemType, propertyInfo.Name);
				}
				else
				{
					propertyAccess = BindingExpressionHelper.CreateGetValueFunc(typeInfo.ItemType, propertyInfo.Name);
				}
				list.Add(new PropertyInfoFieldInfo(propertyInfo, propertyAccess)
				{
					PreferredRole = FieldInfoHelper.GetRoleForType(propertyInfo.PropertyType)
				});
			}
			return list;
		}

		// Token: 0x04002148 RID: 8520
		private IEnumerable source;

		// Token: 0x02000CB3 RID: 3251
		private class TypeInformation
		{
			// Token: 0x060079B3 RID: 31155 RVA: 0x001BF351 File Offset: 0x001BD551
			public TypeInformation(Type itemType)
			{
				this.ItemType = itemType;
				this.PropertiesProviderType = itemType;
			}

			// Token: 0x17002731 RID: 10033
			// (get) Token: 0x060079B4 RID: 31156 RVA: 0x001BF367 File Offset: 0x001BD567
			// (set) Token: 0x060079B5 RID: 31157 RVA: 0x001BF36F File Offset: 0x001BD56F
			public object ItemInstance { get; set; }

			// Token: 0x17002732 RID: 10034
			// (get) Token: 0x060079B6 RID: 31158 RVA: 0x001BF378 File Offset: 0x001BD578
			// (set) Token: 0x060079B7 RID: 31159 RVA: 0x001BF380 File Offset: 0x001BD580
			public Type ItemType { get; set; }

			// Token: 0x17002733 RID: 10035
			// (get) Token: 0x060079B8 RID: 31160 RVA: 0x001BF389 File Offset: 0x001BD589
			// (set) Token: 0x060079B9 RID: 31161 RVA: 0x001BF391 File Offset: 0x001BD591
			public Type PropertiesProviderType { get; set; }

			// Token: 0x17002734 RID: 10036
			// (get) Token: 0x060079BA RID: 31162 RVA: 0x001BF39A File Offset: 0x001BD59A
			// (set) Token: 0x060079BB RID: 31163 RVA: 0x001BF3A2 File Offset: 0x001BD5A2
			public bool DynamicPropertyAccess { get; set; }

			// Token: 0x060079BC RID: 31164 RVA: 0x001BF3AB File Offset: 0x001BD5AB
			public bool IsValid()
			{
				return this.ItemType != null;
			}
		}
	}
}
