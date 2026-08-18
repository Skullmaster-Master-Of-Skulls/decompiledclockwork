using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace Telerik.Web.UI
{
	// Token: 0x02001994 RID: 6548
	internal class ItemPropertiesDescriptor
	{
		// Token: 0x17004C83 RID: 19587
		// (get) Token: 0x0600FD75 RID: 64885 RVA: 0x0038F39A File Offset: 0x0038D59A
		// (set) Token: 0x0600FD76 RID: 64886 RVA: 0x0038F3A2 File Offset: 0x0038D5A2
		protected object FirstItem { get; set; }

		// Token: 0x0600FD77 RID: 64887 RVA: 0x0038F3AB File Offset: 0x0038D5AB
		public ItemPropertiesDescriptor(IEnumerable collection)
		{
			this._collection = collection;
		}

		// Token: 0x0600FD78 RID: 64888 RVA: 0x0038F3BC File Offset: 0x0038D5BC
		public PropertyDescriptorCollection Process()
		{
			PropertyDescriptorCollection result = null;
			if (!this.TryExtractPropertiesForTypedList(this._collection, out result))
			{
				Type itemType = this.GetItemType(this._collection);
				if (itemType != null && itemType != typeof(object))
				{
					if (this.IsCustomTypeDescriptor(itemType) && this.FirstItem != null)
					{
						return ((ICustomTypeDescriptor)this.FirstItem).GetProperties();
					}
					if (RadListView.IsBindableType(itemType))
					{
						return new PropertyDescriptorCollection(new ItemPropertiesDescriptor.ListViewPropertyDescriptor[]
						{
							new ItemPropertiesDescriptor.ListViewPropertyDescriptor("Item", true, itemType)
						});
					}
					return TypeDescriptor.GetProperties(itemType);
				}
				else
				{
					if (itemType == null)
					{
						if (this.TryExtractFromEntityCollection(this._collection, out result))
						{
							return result;
						}
						if (this.TryExtractFromOpenAccess(this._collection, out result))
						{
							return result;
						}
					}
					this.TryExtractTypeFromDataReader(this._collection, out result);
				}
			}
			return result;
		}

		// Token: 0x0600FD79 RID: 64889 RVA: 0x0038F491 File Offset: 0x0038D691
		private bool IsCustomTypeDescriptor(Type t)
		{
			return Array.IndexOf<Type>(t.GetInterfaces(), typeof(ICustomTypeDescriptor)) > -1;
		}

		// Token: 0x0600FD7A RID: 64890 RVA: 0x0038F4AC File Offset: 0x0038D6AC
		protected Type GetItemType(IEnumerable source)
		{
			this.FirstItem = null;
			Type type;
			if (!this.TryExtractTypeFromGenericCollection(source, out type))
			{
				this.TryExtractTypeFromArray(source, out type);
			}
			if (type == null || (type != null && this.IsCustomTypeDescriptor(type)))
			{
				IEnumerator enumerator = source.GetEnumerator();
				if (enumerator != null && enumerator.MoveNext())
				{
					if (enumerator.Current != null)
					{
						this.FirstItem = enumerator.Current;
						type = this.FirstItem.GetType();
					}
					ListViewEnumerableHelper.TryReset(enumerator);
				}
			}
			return type;
		}

		// Token: 0x0600FD7B RID: 64891 RVA: 0x0038F530 File Offset: 0x0038D730
		private bool TryExtractFromOpenAccess(IEnumerable source, out PropertyDescriptorCollection properties)
		{
			properties = null;
			if (!source.GetType().FullName.Contains("Telerik.OpenAccess.RT.DataSource.OpenAccessDataSourceView+PureEnumerable"))
			{
				return false;
			}
			Type[] genericArguments = source.GetType().GetGenericArguments();
			if (genericArguments.Length > 0)
			{
				properties = TypeDescriptor.GetProperties(genericArguments[0]);
				return true;
			}
			return false;
		}

		// Token: 0x0600FD7C RID: 64892 RVA: 0x0038F578 File Offset: 0x0038D778
		private bool TryExtractFromEntityCollection(IEnumerable source, out PropertyDescriptorCollection properties)
		{
			properties = null;
			if (!source.GetType().Name.Contains("EntityDataSourceWrapperCollection"))
			{
				return false;
			}
			MethodBase method = source.GetType().GetMethod("GetItemProperties");
			object[] parameters = new object[1];
			properties = (method.Invoke(source, parameters) as PropertyDescriptorCollection);
			return true;
		}

		// Token: 0x0600FD7D RID: 64893 RVA: 0x0038F5C8 File Offset: 0x0038D7C8
		private bool TryExtractTypeFromDataReader(IEnumerable source, out PropertyDescriptorCollection properties)
		{
			properties = null;
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			try
			{
				IDataReader dataReader = source as IDataReader;
				if (dataReader != null)
				{
					for (int i = 0; i < dataReader.FieldCount; i++)
					{
						Type fieldType = dataReader.GetFieldType(i);
						string name = dataReader.GetName(i);
						ItemPropertiesDescriptor.ListViewPropertyDescriptor item = new ItemPropertiesDescriptor.ListViewPropertyDescriptor(name, false, fieldType);
						list.Add(item);
					}
				}
				properties = new PropertyDescriptorCollection(list.ToArray());
			}
			catch (InvalidOperationException)
			{
				properties = null;
			}
			return properties != null;
		}

		// Token: 0x0600FD7E RID: 64894 RVA: 0x0038F64C File Offset: 0x0038D84C
		private bool TryExtractTypeFromArray(IEnumerable source, out Type itemType)
		{
			itemType = null;
			Type type = source.GetType();
			if (type.HasElementType)
			{
				itemType = type.GetElementType();
			}
			Type[] types = new Type[]
			{
				typeof(int)
			};
			PropertyInfo property = type.GetProperty("Item", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, null, types, null);
			if (itemType == null && property != null)
			{
				itemType = property.PropertyType;
			}
			return itemType != null;
		}

		// Token: 0x0600FD7F RID: 64895 RVA: 0x0038F6C0 File Offset: 0x0038D8C0
		private bool TryExtractPropertiesForTypedList(IEnumerable source, out PropertyDescriptorCollection propertyDescriptors)
		{
			propertyDescriptors = null;
			ITypedList typedList = source as ITypedList;
			if (typedList != null)
			{
				propertyDescriptors = typedList.GetItemProperties(new PropertyDescriptor[0]);
			}
			return propertyDescriptors != null;
		}

		// Token: 0x0600FD80 RID: 64896 RVA: 0x0038F6F0 File Offset: 0x0038D8F0
		private bool TryExtractTypeFromGenericCollection(IEnumerable source, out Type itemType)
		{
			itemType = null;
			if (source.GetType().IsGenericType && (!source.GetType().IsNested || source.GetType().IsGenericTypeDefinition))
			{
				Type[] genericArguments = source.GetType().GetGenericArguments();
				if (genericArguments.Length == 1)
				{
					itemType = genericArguments[0];
				}
			}
			return itemType != null;
		}

		// Token: 0x040047F5 RID: 18421
		private IEnumerable _collection;

		// Token: 0x02001995 RID: 6549
		private class ListViewPropertyDescriptor : PropertyDescriptor
		{
			// Token: 0x0600FD81 RID: 64897 RVA: 0x0038F746 File Offset: 0x0038D946
			public ListViewPropertyDescriptor(string propertyName, bool readOnly, Type propertyType) : base(propertyName, null)
			{
				this._isReadOnly = readOnly;
				this._dataType = propertyType;
			}

			// Token: 0x0600FD82 RID: 64898 RVA: 0x0038F75E File Offset: 0x0038D95E
			public override bool CanResetValue(object component)
			{
				throw new NotImplementedException();
			}

			// Token: 0x17004C84 RID: 19588
			// (get) Token: 0x0600FD83 RID: 64899 RVA: 0x0038F765 File Offset: 0x0038D965
			public override Type ComponentType
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x0600FD84 RID: 64900 RVA: 0x0038F76C File Offset: 0x0038D96C
			public override object GetValue(object component)
			{
				throw new NotImplementedException();
			}

			// Token: 0x17004C85 RID: 19589
			// (get) Token: 0x0600FD85 RID: 64901 RVA: 0x0038F773 File Offset: 0x0038D973
			public override bool IsReadOnly
			{
				get
				{
					return this._isReadOnly;
				}
			}

			// Token: 0x17004C86 RID: 19590
			// (get) Token: 0x0600FD86 RID: 64902 RVA: 0x0038F77B File Offset: 0x0038D97B
			public override Type PropertyType
			{
				get
				{
					return this._dataType;
				}
			}

			// Token: 0x0600FD87 RID: 64903 RVA: 0x0038F783 File Offset: 0x0038D983
			public override void ResetValue(object component)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600FD88 RID: 64904 RVA: 0x0038F78A File Offset: 0x0038D98A
			public override void SetValue(object component, object value)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600FD89 RID: 64905 RVA: 0x0038F791 File Offset: 0x0038D991
			public override bool ShouldSerializeValue(object component)
			{
				throw new NotImplementedException();
			}

			// Token: 0x040047F7 RID: 18423
			private bool _isReadOnly;

			// Token: 0x040047F8 RID: 18424
			private Type _dataType;
		}
	}
}
