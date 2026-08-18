using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.Web.Http.Internal
{
	// Token: 0x0200000A RID: 10
	internal class PropertyHelper
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002D40 File Offset: 0x00000F40
		public PropertyHelper(PropertyInfo property)
		{
			this.Name = property.Name;
			this._valueGetter = PropertyHelper.MakeFastPropertyGetter(property);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002D60 File Offset: 0x00000F60
		public static Action<TDeclaringType, object> MakeFastPropertySetter<TDeclaringType>(PropertyInfo propertyInfo) where TDeclaringType : class
		{
			MethodInfo setMethod = propertyInfo.GetSetMethod();
			Type reflectedType = propertyInfo.ReflectedType;
			Type parameterType = setMethod.GetParameters()[0].ParameterType;
			Delegate firstArgument = setMethod.CreateDelegate(typeof(Action<, >).MakeGenericType(new Type[]
			{
				reflectedType,
				parameterType
			}));
			MethodInfo method = PropertyHelper._callPropertySetterOpenGenericMethod.MakeGenericMethod(new Type[]
			{
				reflectedType,
				parameterType
			});
			Delegate @delegate = Delegate.CreateDelegate(typeof(Action<TDeclaringType, object>), firstArgument, method);
			return (Action<TDeclaringType, object>)@delegate;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002DEE File Offset: 0x00000FEE
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002DF6 File Offset: 0x00000FF6
		public virtual string Name { get; protected set; }

		// Token: 0x0600003B RID: 59 RVA: 0x00002DFF File Offset: 0x00000FFF
		public object GetValue(object instance)
		{
			return this._valueGetter(instance);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002E0D File Offset: 0x0000100D
		public static PropertyHelper[] GetProperties(object instance)
		{
			return PropertyHelper.GetProperties(instance, new Func<PropertyInfo, PropertyHelper>(PropertyHelper.CreateInstance), PropertyHelper._reflectionCache);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002E28 File Offset: 0x00001028
		public static Func<object, object> MakeFastPropertyGetter(PropertyInfo propertyInfo)
		{
			MethodInfo getMethod = propertyInfo.GetGetMethod();
			Type reflectedType = getMethod.ReflectedType;
			Type returnType = getMethod.ReturnType;
			Delegate @delegate;
			if (reflectedType.IsValueType)
			{
				Delegate firstArgument = getMethod.CreateDelegate(typeof(PropertyHelper.ByRefFunc<, >).MakeGenericType(new Type[]
				{
					reflectedType,
					returnType
				}));
				MethodInfo method = PropertyHelper._callPropertyGetterByReferenceOpenGenericMethod.MakeGenericMethod(new Type[]
				{
					reflectedType,
					returnType
				});
				@delegate = Delegate.CreateDelegate(typeof(Func<object, object>), firstArgument, method);
			}
			else
			{
				Delegate firstArgument2 = getMethod.CreateDelegate(typeof(Func<, >).MakeGenericType(new Type[]
				{
					reflectedType,
					returnType
				}));
				MethodInfo method2 = PropertyHelper._callPropertyGetterOpenGenericMethod.MakeGenericMethod(new Type[]
				{
					reflectedType,
					returnType
				});
				@delegate = Delegate.CreateDelegate(typeof(Func<object, object>), firstArgument2, method2);
			}
			return (Func<object, object>)@delegate;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002F18 File Offset: 0x00001118
		private static PropertyHelper CreateInstance(PropertyInfo property)
		{
			return new PropertyHelper(property);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002F20 File Offset: 0x00001120
		private static object CallPropertyGetter<TDeclaringType, TValue>(Func<TDeclaringType, TValue> getter, object @this)
		{
			return getter((TDeclaringType)((object)@this));
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002F34 File Offset: 0x00001134
		private static object CallPropertyGetterByReference<TDeclaringType, TValue>(PropertyHelper.ByRefFunc<TDeclaringType, TValue> getter, object @this)
		{
			TDeclaringType tdeclaringType = (TDeclaringType)((object)@this);
			return getter(ref tdeclaringType);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002F55 File Offset: 0x00001155
		private static void CallPropertySetter<TDeclaringType, TValue>(Action<TDeclaringType, TValue> setter, object @this, object value)
		{
			setter((TDeclaringType)((object)@this), (TValue)((object)value));
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002F84 File Offset: 0x00001184
		protected static PropertyHelper[] GetProperties(object instance, Func<PropertyInfo, PropertyHelper> createPropertyHelper, ConcurrentDictionary<Type, PropertyHelper[]> cache)
		{
			Type type = instance.GetType();
			PropertyHelper[] array;
			if (!cache.TryGetValue(type, out array))
			{
				IEnumerable<PropertyInfo> enumerable = from prop in type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
				where prop.GetIndexParameters().Length == 0 && prop.GetMethod != null
				select prop;
				List<PropertyHelper> list = new List<PropertyHelper>();
				foreach (PropertyInfo arg in enumerable)
				{
					PropertyHelper item = createPropertyHelper(arg);
					list.Add(item);
				}
				array = list.ToArray();
				cache.TryAdd(type, array);
			}
			return array;
		}

		// Token: 0x04000008 RID: 8
		private static ConcurrentDictionary<Type, PropertyHelper[]> _reflectionCache = new ConcurrentDictionary<Type, PropertyHelper[]>();

		// Token: 0x04000009 RID: 9
		private Func<object, object> _valueGetter;

		// Token: 0x0400000A RID: 10
		private static readonly MethodInfo _callPropertyGetterOpenGenericMethod = typeof(PropertyHelper).GetMethod("CallPropertyGetter", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x0400000B RID: 11
		private static readonly MethodInfo _callPropertyGetterByReferenceOpenGenericMethod = typeof(PropertyHelper).GetMethod("CallPropertyGetterByReference", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x0400000C RID: 12
		private static readonly MethodInfo _callPropertySetterOpenGenericMethod = typeof(PropertyHelper).GetMethod("CallPropertySetter", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x0200000B RID: 11
		// (Invoke) Token: 0x06000046 RID: 70
		private delegate TValue ByRefFunc<TDeclaringType, TValue>(ref TDeclaringType arg);
	}
}
