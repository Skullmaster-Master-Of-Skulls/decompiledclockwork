using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Web.Resources;

namespace System.Web.Script.Serialization
{
	// Token: 0x02000102 RID: 258
	internal static class ObjectConverter
	{
		// Token: 0x06000DC3 RID: 3523 RVA: 0x000305D4 File Offset: 0x0002E7D4
		private static bool AddItemToList(IList oldList, IList newList, Type elementType, JavaScriptSerializer serializer, bool throwOnError)
		{
			foreach (object o in oldList)
			{
				object value;
				if (!ObjectConverter.ConvertObjectToTypeMain(o, elementType, serializer, throwOnError, out value))
				{
					return false;
				}
				newList.Add(value);
			}
			return true;
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0003063C File Offset: 0x0002E83C
		private static bool AssignToPropertyOrField(object propertyValue, object o, string memberName, JavaScriptSerializer serializer, bool throwOnError)
		{
			IDictionary dictionary = o as IDictionary;
			if (dictionary == null)
			{
				Type type = o.GetType();
				PropertyInfo property = type.GetProperty(memberName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
				if (property != null)
				{
					MethodInfo setMethod = property.GetSetMethod();
					if (setMethod != null)
					{
						if (!ObjectConverter.ConvertObjectToTypeMain(propertyValue, property.PropertyType, serializer, throwOnError, out propertyValue))
						{
							return false;
						}
						try
						{
							setMethod.Invoke(o, new object[]
							{
								propertyValue
							});
							return true;
						}
						catch
						{
							if (throwOnError)
							{
								throw;
							}
							return false;
						}
					}
				}
				FieldInfo field = type.GetField(memberName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
				if (field != null)
				{
					if (!ObjectConverter.ConvertObjectToTypeMain(propertyValue, field.FieldType, serializer, throwOnError, out propertyValue))
					{
						return false;
					}
					try
					{
						field.SetValue(o, propertyValue);
						return true;
					}
					catch
					{
						if (throwOnError)
						{
							throw;
						}
						return false;
					}
				}
				return true;
			}
			if (!ObjectConverter.ConvertObjectToTypeMain(propertyValue, null, serializer, throwOnError, out propertyValue))
			{
				return false;
			}
			dictionary[memberName] = propertyValue;
			return true;
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x00030738 File Offset: 0x0002E938
		private static bool ConvertDictionaryToObject(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer, bool throwOnError, out object convertedObject)
		{
			Type type2 = type;
			string text = null;
			object obj = dictionary;
			object obj2;
			if (dictionary.TryGetValue("__type", out obj2))
			{
				if (!ObjectConverter.ConvertObjectToTypeMain(obj2, typeof(string), serializer, throwOnError, out obj2))
				{
					convertedObject = false;
					return false;
				}
				text = (string)obj2;
				if (text != null)
				{
					if (serializer.TypeResolver != null)
					{
						type2 = serializer.TypeResolver.ResolveType(text);
						if (type2 == null)
						{
							if (throwOnError)
							{
								throw new InvalidOperationException();
							}
							convertedObject = null;
							return false;
						}
					}
					dictionary.Remove("__type");
				}
			}
			JavaScriptConverter javaScriptConverter = null;
			if (type2 != null && serializer.ConverterExistsForType(type2, out javaScriptConverter))
			{
				try
				{
					convertedObject = javaScriptConverter.Deserialize(dictionary, type2, serializer);
					return true;
				}
				catch
				{
					if (throwOnError)
					{
						throw;
					}
					convertedObject = null;
					return false;
				}
			}
			if (text != null || ObjectConverter.IsClientInstantiatableType(type2, serializer))
			{
				obj = Activator.CreateInstance(type2);
			}
			List<string> list = new List<string>(dictionary.Keys);
			if (ObjectConverter.IsGenericDictionary(type))
			{
				Type type3 = type.GetGenericArguments()[0];
				if (type3 != typeof(string) && type3 != typeof(object))
				{
					if (throwOnError)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.JSON_DictionaryTypeNotSupported, new object[]
						{
							type.FullName
						}));
					}
					convertedObject = null;
					return false;
				}
				else
				{
					Type type4 = type.GetGenericArguments()[1];
					IDictionary dictionary2 = null;
					if (ObjectConverter.IsClientInstantiatableType(type, serializer))
					{
						dictionary2 = (IDictionary)Activator.CreateInstance(type);
					}
					else
					{
						Type type5 = ObjectConverter._dictionaryGenericType.MakeGenericType(new Type[]
						{
							type3,
							type4
						});
						dictionary2 = (IDictionary)Activator.CreateInstance(type5);
					}
					if (dictionary2 != null)
					{
						foreach (string key in list)
						{
							object value;
							if (!ObjectConverter.ConvertObjectToTypeMain(dictionary[key], type4, serializer, throwOnError, out value))
							{
								convertedObject = null;
								return false;
							}
							dictionary2[key] = value;
						}
						convertedObject = dictionary2;
						return true;
					}
				}
			}
			if (!(type != null) || type.IsAssignableFrom(obj.GetType()))
			{
				foreach (string text2 in list)
				{
					object propertyValue = dictionary[text2];
					if (!ObjectConverter.AssignToPropertyOrField(propertyValue, obj, text2, serializer, throwOnError))
					{
						convertedObject = null;
						return false;
					}
				}
				convertedObject = obj;
				return true;
			}
			if (!throwOnError)
			{
				convertedObject = null;
				return false;
			}
			ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, ObjectConverter.s_emptyTypeArray, null);
			if (constructor == null)
			{
				throw new MissingMethodException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.JSON_NoConstructor, new object[]
				{
					type.FullName
				}));
			}
			throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.JSON_DeserializerTypeMismatch, new object[]
			{
				type.FullName
			}));
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x00030A38 File Offset: 0x0002EC38
		internal static object ConvertObjectToType(object o, Type type, JavaScriptSerializer serializer)
		{
			object result;
			ObjectConverter.ConvertObjectToTypeMain(o, type, serializer, true, out result);
			return result;
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00030A54 File Offset: 0x0002EC54
		private static bool ConvertObjectToTypeMain(object o, Type type, JavaScriptSerializer serializer, bool throwOnError, out object convertedObject)
		{
			if (o == null)
			{
				if (type == typeof(char))
				{
					convertedObject = '\0';
					return true;
				}
				if (!ObjectConverter.IsNonNullableValueType(type))
				{
					convertedObject = null;
					return true;
				}
				if (throwOnError)
				{
					throw new InvalidOperationException(AtlasWeb.JSON_ValueTypeCannotBeNull);
				}
				convertedObject = null;
				return false;
			}
			else
			{
				if (o.GetType() == type)
				{
					convertedObject = o;
					return true;
				}
				return ObjectConverter.ConvertObjectToTypeInternal(o, type, serializer, throwOnError, out convertedObject);
			}
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x00030AC4 File Offset: 0x0002ECC4
		private static bool ConvertObjectToTypeInternal(object o, Type type, JavaScriptSerializer serializer, bool throwOnError, out object convertedObject)
		{
			IDictionary<string, object> dictionary = o as IDictionary<string, object>;
			if (dictionary != null)
			{
				return ObjectConverter.ConvertDictionaryToObject(dictionary, type, serializer, throwOnError, out convertedObject);
			}
			IList list = o as IList;
			if (list != null)
			{
				IList list2;
				if (ObjectConverter.ConvertListToObject(list, type, serializer, throwOnError, out list2))
				{
					convertedObject = list2;
					return true;
				}
				convertedObject = null;
				return false;
			}
			else
			{
				if (type == null || o.GetType() == type)
				{
					convertedObject = o;
					return true;
				}
				TypeConverter converter = TypeDescriptor.GetConverter(type);
				if (converter.CanConvertFrom(o.GetType()))
				{
					try
					{
						convertedObject = converter.ConvertFrom(null, CultureInfo.InvariantCulture, o);
						return true;
					}
					catch
					{
						if (throwOnError)
						{
							throw;
						}
						convertedObject = null;
						return false;
					}
				}
				if (converter.CanConvertFrom(typeof(string)))
				{
					try
					{
						string text;
						if (o is DateTime)
						{
							text = ((DateTime)o).ToUniversalTime().ToString("u", CultureInfo.InvariantCulture);
						}
						else
						{
							TypeConverter converter2 = TypeDescriptor.GetConverter(o);
							text = converter2.ConvertToInvariantString(o);
						}
						convertedObject = converter.ConvertFromInvariantString(text);
						return true;
					}
					catch
					{
						if (throwOnError)
						{
							throw;
						}
						convertedObject = null;
						return false;
					}
				}
				if (type.IsAssignableFrom(o.GetType()))
				{
					convertedObject = o;
					return true;
				}
				if (throwOnError)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.JSON_CannotConvertObjectToType, new object[]
					{
						o.GetType(),
						type
					}));
				}
				convertedObject = null;
				return false;
			}
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x00030C3C File Offset: 0x0002EE3C
		private static bool ConvertListToObject(IList list, Type type, JavaScriptSerializer serializer, bool throwOnError, out IList convertedList)
		{
			if (type == null || type == typeof(object) || ObjectConverter.IsArrayListCompatible(type))
			{
				Type type2 = typeof(object);
				if (type != null && type != typeof(object))
				{
					type2 = type.GetElementType();
				}
				ArrayList arrayList = new ArrayList();
				if (!ObjectConverter.AddItemToList(list, arrayList, type2, serializer, throwOnError))
				{
					convertedList = null;
					return false;
				}
				if (type == typeof(ArrayList) || type == typeof(IEnumerable) || type == typeof(IList) || type == typeof(ICollection))
				{
					convertedList = arrayList;
					return true;
				}
				convertedList = arrayList.ToArray(type2);
				return true;
			}
			else
			{
				if (type.IsGenericType && type.GetGenericArguments().Length == 1)
				{
					Type type3 = type.GetGenericArguments()[0];
					Type type4 = ObjectConverter._enumerableGenericType.MakeGenericType(new Type[]
					{
						type3
					});
					if (type4.IsAssignableFrom(type))
					{
						Type type5 = ObjectConverter._listGenericType.MakeGenericType(new Type[]
						{
							type3
						});
						IList list2;
						if (ObjectConverter.IsClientInstantiatableType(type, serializer) && typeof(IList).IsAssignableFrom(type))
						{
							list2 = (IList)Activator.CreateInstance(type);
						}
						else if (type5.IsAssignableFrom(type))
						{
							if (throwOnError)
							{
								throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.JSON_CannotCreateListType, new object[]
								{
									type.FullName
								}));
							}
							convertedList = null;
							return false;
						}
						else
						{
							list2 = (IList)Activator.CreateInstance(type5);
						}
						if (!ObjectConverter.AddItemToList(list, list2, type3, serializer, throwOnError))
						{
							convertedList = null;
							return false;
						}
						convertedList = list2;
						return true;
					}
				}
				else if (ObjectConverter.IsClientInstantiatableType(type, serializer) && typeof(IList).IsAssignableFrom(type))
				{
					IList list3 = (IList)Activator.CreateInstance(type);
					if (!ObjectConverter.AddItemToList(list, list3, null, serializer, throwOnError))
					{
						convertedList = null;
						return false;
					}
					convertedList = list3;
					return true;
				}
				if (throwOnError)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.JSON_ArrayTypeNotSupported, new object[]
					{
						type.FullName
					}));
				}
				convertedList = null;
				return false;
			}
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x00030E64 File Offset: 0x0002F064
		private static bool IsArrayListCompatible(Type type)
		{
			return type.IsArray || type == typeof(ArrayList) || type == typeof(IEnumerable) || type == typeof(IList) || type == typeof(ICollection);
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x00030EC4 File Offset: 0x0002F0C4
		internal static bool IsClientInstantiatableType(Type t, JavaScriptSerializer serializer)
		{
			if (t == null || t.IsAbstract || t.IsInterface || t.IsArray)
			{
				return false;
			}
			if (t == typeof(object))
			{
				return false;
			}
			JavaScriptConverter javaScriptConverter = null;
			if (serializer.ConverterExistsForType(t, out javaScriptConverter))
			{
				return true;
			}
			if (t.IsValueType)
			{
				return true;
			}
			ConstructorInfo constructor = t.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, ObjectConverter.s_emptyTypeArray, null);
			return !(constructor == null);
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x00030F40 File Offset: 0x0002F140
		private static bool IsGenericDictionary(Type type)
		{
			return type != null && type.IsGenericType && (typeof(IDictionary).IsAssignableFrom(type) || type.GetGenericTypeDefinition() == ObjectConverter._idictionaryGenericType) && type.GetGenericArguments().Length == 2;
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x00030F8F File Offset: 0x0002F18F
		private static bool IsNonNullableValueType(Type type)
		{
			return type != null && type.IsValueType && (!type.IsGenericType || !(type.GetGenericTypeDefinition() == typeof(Nullable<>)));
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00030FC6 File Offset: 0x0002F1C6
		internal static bool TryConvertObjectToType(object o, Type type, JavaScriptSerializer serializer, out object convertedObject)
		{
			return ObjectConverter.ConvertObjectToTypeMain(o, type, serializer, false, out convertedObject);
		}

		// Token: 0x040003DE RID: 990
		private static readonly Type[] s_emptyTypeArray = new Type[0];

		// Token: 0x040003DF RID: 991
		private static Type _listGenericType = typeof(List<>);

		// Token: 0x040003E0 RID: 992
		private static Type _enumerableGenericType = typeof(IEnumerable<>);

		// Token: 0x040003E1 RID: 993
		private static Type _dictionaryGenericType = typeof(Dictionary<, >);

		// Token: 0x040003E2 RID: 994
		private static Type _idictionaryGenericType = typeof(IDictionary<, >);
	}
}
