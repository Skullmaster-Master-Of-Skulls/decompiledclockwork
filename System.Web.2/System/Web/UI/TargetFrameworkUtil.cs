using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;

namespace System.Web.UI
{
	// Token: 0x0200030A RID: 778
	internal static class TargetFrameworkUtil
	{
		// Token: 0x060023D9 RID: 9177 RVA: 0x00075170 File Offset: 0x00073370
		private static TargetFrameworkUtil.MemberCache GetMemberCache(Type type)
		{
			TargetFrameworkUtil.MemberCache memberCache = null;
			if (!TargetFrameworkUtil.s_memberCache.TryGetValue(type, out memberCache))
			{
				memberCache = new TargetFrameworkUtil.MemberCache();
				TargetFrameworkUtil.s_memberCache.TryAdd(type, memberCache);
			}
			return memberCache;
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x000751A2 File Offset: 0x000733A2
		private static Tuple<string, int> MakeTuple(string name, BindingFlags bindingAttr)
		{
			return new Tuple<string, int>(name, (int)bindingAttr);
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x060023DB RID: 9179 RVA: 0x000751AC File Offset: 0x000733AC
		private static TypeDescriptionProviderService TypeDescriptionProviderService
		{
			get
			{
				if (TargetFrameworkUtil.DesignerHost == null)
				{
					return null;
				}
				return TargetFrameworkUtil.DesignerHost.GetService(typeof(TypeDescriptionProviderService)) as TypeDescriptionProviderService;
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x060023DC RID: 9180 RVA: 0x000751DD File Offset: 0x000733DD
		// (set) Token: 0x060023DD RID: 9181 RVA: 0x000751E4 File Offset: 0x000733E4
		internal static IDesignerHost DesignerHost { get; set; }

		// Token: 0x17000A06 RID: 2566
		// (set) Token: 0x060023DE RID: 9182 RVA: 0x000751EC File Offset: 0x000733EC
		internal static ClientBuildManagerTypeDescriptionProviderBridge CBMTypeDescriptionProviderBridge
		{
			set
			{
				TargetFrameworkUtil.s_cbmTdpBridge = value;
			}
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x000751F4 File Offset: 0x000733F4
		private static TypeDescriptionProvider GetTargetFrameworkProvider(object obj)
		{
			TypeDescriptionProviderService typeDescriptionProviderService = TargetFrameworkUtil.TypeDescriptionProviderService;
			if (typeDescriptionProviderService != null)
			{
				return typeDescriptionProviderService.GetProvider(obj);
			}
			return null;
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x00075214 File Offset: 0x00073414
		private static TypeDescriptionProvider GetTargetFrameworkProvider(Type type)
		{
			TypeDescriptionProviderService typeDescriptionProviderService = TargetFrameworkUtil.TypeDescriptionProviderService;
			if (typeDescriptionProviderService != null)
			{
				return typeDescriptionProviderService.GetProvider(type);
			}
			return null;
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x00075234 File Offset: 0x00073434
		private static ICustomTypeDescriptor GetTypeDescriptor(Type type)
		{
			TypeDescriptionProvider targetFrameworkProvider = TargetFrameworkUtil.GetTargetFrameworkProvider(type);
			if (targetFrameworkProvider != null)
			{
				ICustomTypeDescriptor typeDescriptor = targetFrameworkProvider.GetTypeDescriptor(type);
				if (typeDescriptor != null)
				{
					return typeDescriptor;
				}
			}
			return null;
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x0007525C File Offset: 0x0007345C
		private static ICustomTypeDescriptor GetTypeDescriptor(object obj)
		{
			TypeDescriptionProvider targetFrameworkProvider = TargetFrameworkUtil.GetTargetFrameworkProvider(obj);
			if (targetFrameworkProvider != null)
			{
				ICustomTypeDescriptor typeDescriptor = targetFrameworkProvider.GetTypeDescriptor(obj);
				if (typeDescriptor != null)
				{
					return typeDescriptor;
				}
			}
			return null;
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x00075284 File Offset: 0x00073484
		private static Type GetReflectionType(Type type)
		{
			if (type == null)
			{
				return null;
			}
			TypeDescriptionProvider targetFrameworkProvider = TargetFrameworkUtil.GetTargetFrameworkProvider(type);
			if (targetFrameworkProvider != null)
			{
				return targetFrameworkProvider.GetReflectionType(type);
			}
			return type;
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x000752B0 File Offset: 0x000734B0
		private static Type[] GetReflectionTypes(Type[] types)
		{
			if (types == null)
			{
				return null;
			}
			IEnumerable<Type> source = from t in types
			select TargetFrameworkUtil.GetReflectionType(t);
			return source.ToArray<Type>();
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x000752F0 File Offset: 0x000734F0
		internal static PropertyInfo GetProperty(Type type, string name, BindingFlags bindingAttr, Type returnType = null, Type[] types = null, bool throwAmbiguousMatchException = false)
		{
			if (types == null)
			{
				types = Type.EmptyTypes;
			}
			if (TargetFrameworkUtil.SkipCache || returnType != null || types != Type.EmptyTypes)
			{
				return TargetFrameworkUtil.GetPropertyHelper(type, name, bindingAttr, returnType, types, throwAmbiguousMatchException);
			}
			PropertyInfo propertyInfo = null;
			TargetFrameworkUtil.MemberCache memberCache = TargetFrameworkUtil.GetMemberCache(type);
			Tuple<string, int> key = TargetFrameworkUtil.MakeTuple(name, bindingAttr);
			if (!memberCache.Properties.TryGetValue(key, out propertyInfo))
			{
				propertyInfo = TargetFrameworkUtil.GetPropertyHelper(type, name, bindingAttr, returnType, types, throwAmbiguousMatchException);
				memberCache.Properties.TryAdd(key, propertyInfo);
			}
			return propertyInfo;
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x00075370 File Offset: 0x00073570
		private static PropertyInfo GetPropertyHelper(Type type, string name, BindingFlags bindingAttr, Type returnType, Type[] types, bool throwAmbiguousMatchException)
		{
			try
			{
				bool flag;
				if (TargetFrameworkUtil.s_cbmTdpBridge != null && TargetFrameworkUtil.IsFrameworkType(type))
				{
					Type typeToUseForCBMBridge = TargetFrameworkUtil.GetTypeToUseForCBMBridge(type);
					flag = TargetFrameworkUtil.s_cbmTdpBridge.HasProperty(typeToUseForCBMBridge, name, bindingAttr, returnType, types);
				}
				else
				{
					Type reflectionType = TargetFrameworkUtil.GetReflectionType(type);
					Type reflectionType2 = TargetFrameworkUtil.GetReflectionType(returnType);
					Type[] reflectionTypes = TargetFrameworkUtil.GetReflectionTypes(types);
					PropertyInfo property = reflectionType.GetProperty(name, bindingAttr, null, reflectionType2, reflectionTypes, null);
					flag = (property != null);
				}
				if (flag)
				{
					return type.GetProperty(name, bindingAttr, null, returnType, types, null);
				}
			}
			catch (AmbiguousMatchException)
			{
				if (throwAmbiguousMatchException)
				{
					throw;
				}
				return TargetFrameworkUtil.GetMostSpecificProperty(type, name, bindingAttr, returnType, types);
			}
			return null;
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x00075418 File Offset: 0x00073618
		internal static FieldInfo GetField(Type type, string name, BindingFlags bindingAttr)
		{
			if (TargetFrameworkUtil.SkipCache)
			{
				return TargetFrameworkUtil.GetFieldInfo(type, name, bindingAttr);
			}
			FieldInfo fieldInfo = null;
			TargetFrameworkUtil.MemberCache memberCache = TargetFrameworkUtil.GetMemberCache(type);
			Tuple<string, int> key = TargetFrameworkUtil.MakeTuple(name, bindingAttr);
			if (!memberCache.Fields.TryGetValue(key, out fieldInfo))
			{
				fieldInfo = TargetFrameworkUtil.GetFieldInfo(type, name, bindingAttr);
				memberCache.Fields.TryAdd(key, fieldInfo);
			}
			return fieldInfo;
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x00075470 File Offset: 0x00073670
		private static FieldInfo GetFieldInfo(Type type, string name, BindingFlags bindingAttr)
		{
			if (TargetFrameworkUtil.s_cbmTdpBridge != null && TargetFrameworkUtil.IsFrameworkType(type))
			{
				Type typeToUseForCBMBridge = TargetFrameworkUtil.GetTypeToUseForCBMBridge(type);
				bool flag = TargetFrameworkUtil.s_cbmTdpBridge.HasField(typeToUseForCBMBridge, name, bindingAttr);
				if (flag)
				{
					return type.GetField(name, bindingAttr);
				}
				return null;
			}
			else
			{
				Type reflectionType = TargetFrameworkUtil.GetReflectionType(type);
				FieldInfo field = reflectionType.GetField(name, bindingAttr);
				if (field != null)
				{
					return type.GetField(name, bindingAttr);
				}
				return null;
			}
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x000754D4 File Offset: 0x000736D4
		internal static EventInfo GetEvent(Type type, string name)
		{
			if (TargetFrameworkUtil.SkipCache)
			{
				return TargetFrameworkUtil.GetEventInfo(type, name);
			}
			EventInfo eventInfo = null;
			TargetFrameworkUtil.MemberCache memberCache = TargetFrameworkUtil.GetMemberCache(type);
			if (!memberCache.Events.TryGetValue(name, out eventInfo))
			{
				eventInfo = TargetFrameworkUtil.GetEventInfo(type, name);
				memberCache.Events.TryAdd(name, eventInfo);
			}
			return eventInfo;
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x00075520 File Offset: 0x00073720
		private static EventInfo GetEventInfo(Type type, string name)
		{
			if (TargetFrameworkUtil.s_cbmTdpBridge != null && TargetFrameworkUtil.IsFrameworkType(type))
			{
				Type typeToUseForCBMBridge = TargetFrameworkUtil.GetTypeToUseForCBMBridge(type);
				bool flag = TargetFrameworkUtil.s_cbmTdpBridge.HasEvent(typeToUseForCBMBridge, name);
				if (flag)
				{
					return type.GetEvent(name);
				}
				return null;
			}
			else
			{
				Type reflectionType = TargetFrameworkUtil.GetReflectionType(type);
				EventInfo @event = reflectionType.GetEvent(name);
				if (@event != null)
				{
					return type.GetEvent(name);
				}
				return null;
			}
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x00075580 File Offset: 0x00073780
		internal static PropertyDescriptorCollection GetProperties(Type type)
		{
			if (TargetFrameworkUtil.SkipCache)
			{
				return TargetFrameworkUtil.GetPropertyDescriptorCollection(type);
			}
			PropertyDescriptorCollection propertyDescriptorCollection = null;
			if (!TargetFrameworkUtil.s_typePropertyDescriptorCollectionDict.TryGetValue(type, out propertyDescriptorCollection))
			{
				propertyDescriptorCollection = TargetFrameworkUtil.GetPropertyDescriptorCollection(type);
				TargetFrameworkUtil.s_typePropertyDescriptorCollectionDict.TryAdd(type, propertyDescriptorCollection);
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x000755C4 File Offset: 0x000737C4
		private static PropertyDescriptorCollection GetPropertyDescriptorCollection(Type type)
		{
			if (TargetFrameworkUtil.s_cbmTdpBridge != null && TargetFrameworkUtil.IsFrameworkType(type))
			{
				return TargetFrameworkUtil.GetFilteredPropertyDescriptorCollection(type, null);
			}
			ICustomTypeDescriptor typeDescriptor = TargetFrameworkUtil.GetTypeDescriptor(type);
			if (typeDescriptor != null)
			{
				return typeDescriptor.GetProperties();
			}
			return TypeDescriptor.GetProperties(type);
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x00075600 File Offset: 0x00073800
		internal static PropertyDescriptorCollection GetProperties(object obj)
		{
			if (TargetFrameworkUtil.SkipCache)
			{
				return TargetFrameworkUtil.GetPropertyDescriptorCollection(obj);
			}
			PropertyDescriptorCollection propertyDescriptorCollection = null;
			if (!TargetFrameworkUtil.s_objectPropertyDescriptorCollectionDict.TryGetValue(obj, out propertyDescriptorCollection))
			{
				propertyDescriptorCollection = TargetFrameworkUtil.GetPropertyDescriptorCollection(obj);
				TargetFrameworkUtil.s_objectPropertyDescriptorCollectionDict.TryAdd(obj, propertyDescriptorCollection);
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x00075644 File Offset: 0x00073844
		private static PropertyDescriptorCollection GetPropertyDescriptorCollection(object obj)
		{
			Type type = obj.GetType();
			if (TargetFrameworkUtil.s_cbmTdpBridge != null && TargetFrameworkUtil.IsFrameworkType(type))
			{
				return TargetFrameworkUtil.GetFilteredPropertyDescriptorCollection(type, obj);
			}
			ICustomTypeDescriptor typeDescriptor = TargetFrameworkUtil.GetTypeDescriptor(obj);
			if (typeDescriptor != null)
			{
				return typeDescriptor.GetProperties();
			}
			return TypeDescriptor.GetProperties(obj);
		}

		// Token: 0x060023EF RID: 9199 RVA: 0x00075688 File Offset: 0x00073888
		private static PropertyDescriptorCollection GetFilteredPropertyDescriptorCollection(Type objectType, object instance)
		{
			PropertyDescriptorCollection propertyDescriptors = null;
			if (instance != null)
			{
				propertyDescriptors = TypeDescriptor.GetProperties(instance);
			}
			else
			{
				if (!(objectType != null))
				{
					throw new ArgumentException("At least one argument should be non-null");
				}
				propertyDescriptors = TypeDescriptor.GetProperties(objectType);
			}
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
			Type typeToUseForCBMBridge = TargetFrameworkUtil.GetTypeToUseForCBMBridge(objectType);
			string[] filteredProperties = TargetFrameworkUtil.s_cbmTdpBridge.GetFilteredProperties(typeToUseForCBMBridge, bindingFlags);
			IEnumerable<PropertyDescriptor> source = from p in filteredProperties
			let d = propertyDescriptors[p]
			where d != null
			select d;
			return new PropertyDescriptorCollection(source.ToArray<PropertyDescriptor>());
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x00075754 File Offset: 0x00073954
		internal static EventDescriptorCollection GetEvents(Type type)
		{
			if (TargetFrameworkUtil.SkipCache)
			{
				return TargetFrameworkUtil.GetEventDescriptorCollection(type);
			}
			EventDescriptorCollection eventDescriptorCollection = null;
			if (!TargetFrameworkUtil.s_eventDescriptorCollectionDict.TryGetValue(type, out eventDescriptorCollection))
			{
				eventDescriptorCollection = TargetFrameworkUtil.GetEventDescriptorCollection(type);
				TargetFrameworkUtil.s_eventDescriptorCollectionDict.TryAdd(type, eventDescriptorCollection);
			}
			return eventDescriptorCollection;
		}

		// Token: 0x060023F1 RID: 9201 RVA: 0x00075798 File Offset: 0x00073998
		private static EventDescriptorCollection GetEventDescriptorCollection(Type type)
		{
			if (TargetFrameworkUtil.s_cbmTdpBridge != null && TargetFrameworkUtil.IsFrameworkType(type))
			{
				return TargetFrameworkUtil.GetFilteredEventDescriptorCollection(type, null);
			}
			ICustomTypeDescriptor typeDescriptor = TargetFrameworkUtil.GetTypeDescriptor(type);
			if (typeDescriptor != null)
			{
				return typeDescriptor.GetEvents();
			}
			return TypeDescriptor.GetEvents(type);
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x000757D4 File Offset: 0x000739D4
		private static EventDescriptorCollection GetFilteredEventDescriptorCollection(Type objectType, object instance)
		{
			EventDescriptorCollection eventDescriptors = null;
			if (instance != null)
			{
				eventDescriptors = TypeDescriptor.GetEvents(instance);
			}
			else
			{
				if (!(objectType != null))
				{
					throw new ArgumentException("At least one argument should be non-null");
				}
				eventDescriptors = TypeDescriptor.GetEvents(objectType);
			}
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
			Type typeToUseForCBMBridge = TargetFrameworkUtil.GetTypeToUseForCBMBridge(objectType);
			string[] filteredEvents = TargetFrameworkUtil.s_cbmTdpBridge.GetFilteredEvents(typeToUseForCBMBridge, bindingFlags);
			IEnumerable<EventDescriptor> source = from e in filteredEvents
			let d = eventDescriptors[e]
			where d != null
			select d;
			return new EventDescriptorCollection(source.ToArray<EventDescriptor>());
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x000758A0 File Offset: 0x00073AA0
		internal static AttributeCollection GetAttributes(Type type)
		{
			ICustomTypeDescriptor typeDescriptor = TargetFrameworkUtil.GetTypeDescriptor(type);
			if (typeDescriptor != null)
			{
				return typeDescriptor.GetAttributes();
			}
			return TypeDescriptor.GetAttributes(type);
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x000758C4 File Offset: 0x00073AC4
		internal static object[] GetCustomAttributes(Type type, Type attributeType, bool inherit)
		{
			Type reflectionType = TargetFrameworkUtil.GetReflectionType(type);
			return reflectionType.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x000758E0 File Offset: 0x00073AE0
		internal static string TypeNameConverter(Type type)
		{
			string result = null;
			if (type != null)
			{
				Type reflectionType = TargetFrameworkUtil.GetReflectionType(type);
				if (reflectionType != null)
				{
					result = reflectionType.AssemblyQualifiedName;
				}
			}
			return result;
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x00075910 File Offset: 0x00073B10
		private static bool IsFrameworkType(Type type)
		{
			bool flag;
			if (!TargetFrameworkUtil.s_isFrameworkType.TryGetValue(type, out flag))
			{
				Assembly assembly = type.Assembly;
				string text;
				ReferenceAssemblyType pathToReferenceAssembly = AssemblyResolver.GetPathToReferenceAssembly(assembly, out text);
				flag = (pathToReferenceAssembly != ReferenceAssemblyType.NonFrameworkAssembly);
				TargetFrameworkUtil.s_isFrameworkType.TryAdd(type, flag);
			}
			return flag;
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x00075954 File Offset: 0x00073B54
		private static PropertyInfo GetMostSpecificProperty(Type type, string name, BindingFlags additionalFlags, Type returnType, Type[] types)
		{
			BindingFlags bindingFlags = BindingFlags.DeclaredOnly;
			bindingFlags |= additionalFlags;
			Type type2 = type;
			while (type2 != null)
			{
				PropertyInfo property = TargetFrameworkUtil.GetProperty(type2, name, bindingFlags, returnType, types, true);
				if (property != null)
				{
					return property;
				}
				type2 = type2.BaseType;
			}
			return null;
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x00075994 File Offset: 0x00073B94
		private static Type GetTypeToUseForCBMBridge(Type type)
		{
			if (!type.IsGenericType)
			{
				return type;
			}
			return type.GetGenericTypeDefinition();
		}

		// Token: 0x060023F9 RID: 9209 RVA: 0x000759A8 File Offset: 0x00073BA8
		internal static bool HasMethod(Type type, string name, BindingFlags bindingAttr)
		{
			bool result;
			if (TargetFrameworkUtil.s_cbmTdpBridge != null && TargetFrameworkUtil.IsFrameworkType(type))
			{
				Type typeToUseForCBMBridge = TargetFrameworkUtil.GetTypeToUseForCBMBridge(type);
				result = TargetFrameworkUtil.s_cbmTdpBridge.HasMethod(typeToUseForCBMBridge, name, bindingAttr);
			}
			else
			{
				Type reflectionType = TargetFrameworkUtil.GetReflectionType(type);
				MethodInfo method = reflectionType.GetMethod(name, bindingAttr);
				result = (method != null);
			}
			return result;
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x060023FA RID: 9210 RVA: 0x000759F6 File Offset: 0x00073BF6
		private static bool SkipCache
		{
			get
			{
				return TargetFrameworkUtil.s_cbmTdpBridge == null;
			}
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x00075A00 File Offset: 0x00073C00
		internal static bool IsSupportedType(Type type)
		{
			TypeDescriptionProvider typeDescriptionProvider = TargetFrameworkUtil.GetTargetFrameworkProvider(type);
			if (typeDescriptionProvider == null)
			{
				typeDescriptionProvider = TypeDescriptor.GetProvider(type);
			}
			return typeDescriptionProvider.IsSupportedType(type);
		}

		// Token: 0x04001CD9 RID: 7385
		private static ConcurrentDictionary<Type, TargetFrameworkUtil.MemberCache> s_memberCache = new ConcurrentDictionary<Type, TargetFrameworkUtil.MemberCache>();

		// Token: 0x04001CDA RID: 7386
		private static ClientBuildManagerTypeDescriptionProviderBridge s_cbmTdpBridge;

		// Token: 0x04001CDB RID: 7387
		private static ConcurrentDictionary<Type, PropertyDescriptorCollection> s_typePropertyDescriptorCollectionDict = new ConcurrentDictionary<Type, PropertyDescriptorCollection>();

		// Token: 0x04001CDC RID: 7388
		private static ConcurrentDictionary<object, PropertyDescriptorCollection> s_objectPropertyDescriptorCollectionDict = new ConcurrentDictionary<object, PropertyDescriptorCollection>();

		// Token: 0x04001CDD RID: 7389
		private static ConcurrentDictionary<Type, EventDescriptorCollection> s_eventDescriptorCollectionDict = new ConcurrentDictionary<Type, EventDescriptorCollection>();

		// Token: 0x04001CDE RID: 7390
		private static ConcurrentDictionary<Type, bool> s_isFrameworkType = new ConcurrentDictionary<Type, bool>();

		// Token: 0x02000986 RID: 2438
		private class MemberCache
		{
			// Token: 0x06006A4F RID: 27215 RVA: 0x000030B5 File Offset: 0x000012B5
			internal MemberCache()
			{
			}

			// Token: 0x17001D3E RID: 7486
			// (get) Token: 0x06006A50 RID: 27216 RVA: 0x0017BC02 File Offset: 0x00179E02
			internal ConcurrentDictionary<string, EventInfo> Events
			{
				get
				{
					if (this._events == null)
					{
						this._events = new ConcurrentDictionary<string, EventInfo>();
					}
					return this._events;
				}
			}

			// Token: 0x17001D3F RID: 7487
			// (get) Token: 0x06006A51 RID: 27217 RVA: 0x0017BC1D File Offset: 0x00179E1D
			internal ConcurrentDictionary<Tuple<string, int>, FieldInfo> Fields
			{
				get
				{
					if (this._fields == null)
					{
						this._fields = new ConcurrentDictionary<Tuple<string, int>, FieldInfo>();
					}
					return this._fields;
				}
			}

			// Token: 0x17001D40 RID: 7488
			// (get) Token: 0x06006A52 RID: 27218 RVA: 0x0017BC38 File Offset: 0x00179E38
			internal ConcurrentDictionary<Tuple<string, int>, PropertyInfo> Properties
			{
				get
				{
					if (this._properties == null)
					{
						this._properties = new ConcurrentDictionary<Tuple<string, int>, PropertyInfo>();
					}
					return this._properties;
				}
			}

			// Token: 0x040038BD RID: 14525
			private ConcurrentDictionary<string, EventInfo> _events;

			// Token: 0x040038BE RID: 14526
			private ConcurrentDictionary<Tuple<string, int>, FieldInfo> _fields;

			// Token: 0x040038BF RID: 14527
			private ConcurrentDictionary<Tuple<string, int>, PropertyInfo> _properties;
		}
	}
}
