using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.Web.WebPages
{
	// Token: 0x02000052 RID: 82
	internal class PropertyHelper
	{
		// Token: 0x060001F2 RID: 498 RVA: 0x000080BB File Offset: 0x000062BB
		public PropertyHelper(PropertyInfo property)
		{
			this.Name = property.Name;
			this._valueGetter = PropertyHelper.MakeFastPropertyGetter(property);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000080DC File Offset: 0x000062DC
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

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000816A File Offset: 0x0000636A
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00008172 File Offset: 0x00006372
		public virtual string Name { get; protected set; }

		// Token: 0x060001F6 RID: 502 RVA: 0x0000817B File Offset: 0x0000637B
		public object GetValue(object instance)
		{
			return this._valueGetter(instance);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00008189 File Offset: 0x00006389
		public static PropertyHelper[] GetProperties(object instance)
		{
			return PropertyHelper.GetProperties(instance, new Func<PropertyInfo, PropertyHelper>(PropertyHelper.CreateInstance), PropertyHelper._reflectionCache);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000081A4 File Offset: 0x000063A4
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

		// Token: 0x060001F9 RID: 505 RVA: 0x00008294 File Offset: 0x00006494
		private static PropertyHelper CreateInstance(PropertyInfo property)
		{
			return new PropertyHelper(property);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000829C File Offset: 0x0000649C
		private static object CallPropertyGetter<TDeclaringType, TValue>(Func<TDeclaringType, TValue> getter, object @this)
		{
			return getter((TDeclaringType)((object)@this));
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000082B0 File Offset: 0x000064B0
		private static object CallPropertyGetterByReference<TDeclaringType, TValue>(PropertyHelper.ByRefFunc<TDeclaringType, TValue> getter, object @this)
		{
			TDeclaringType tdeclaringType = (TDeclaringType)((object)@this);
			return getter(ref tdeclaringType);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000082D1 File Offset: 0x000064D1
		private static void CallPropertySetter<TDeclaringType, TValue>(Action<TDeclaringType, TValue> setter, object @this, object value)
		{
			setter((TDeclaringType)((object)@this), (TValue)((object)value));
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00008300 File Offset: 0x00006500
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

		// Token: 0x040000A2 RID: 162
		private static ConcurrentDictionary<Type, PropertyHelper[]> _reflectionCache = new ConcurrentDictionary<Type, PropertyHelper[]>();

		// Token: 0x040000A3 RID: 163
		private Func<object, object> _valueGetter;

		// Token: 0x040000A4 RID: 164
		private static readonly MethodInfo _callPropertyGetterOpenGenericMethod = typeof(PropertyHelper).GetMethod("CallPropertyGetter", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x040000A5 RID: 165
		private static readonly MethodInfo _callPropertyGetterByReferenceOpenGenericMethod = typeof(PropertyHelper).GetMethod("CallPropertyGetterByReference", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x040000A6 RID: 166
		private static readonly MethodInfo _callPropertySetterOpenGenericMethod = typeof(PropertyHelper).GetMethod("CallPropertySetter", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x02000053 RID: 83
		// (Invoke) Token: 0x06000201 RID: 513
		private delegate TValue ByRefFunc<TDeclaringType, TValue>(ref TDeclaringType arg);
	}
}
