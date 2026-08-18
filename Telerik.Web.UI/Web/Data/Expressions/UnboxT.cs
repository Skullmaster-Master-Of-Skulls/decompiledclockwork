using System;
using System.Globalization;
using System.Reflection;

namespace Telerik.Web.Data.Expressions
{
	// Token: 0x02001BC0 RID: 7104
	internal static class UnboxT<T>
	{
		// Token: 0x06011298 RID: 70296 RVA: 0x003C8F34 File Offset: 0x003C7134
		private static Converter<object, T> Create(Type type)
		{
			if (!type.IsValueType)
			{
				return new Converter<object, T>(UnboxT<T>.ReferenceField);
			}
			if (type.IsGenericType && !type.IsGenericTypeDefinition && typeof(Nullable<>) == type.GetGenericTypeDefinition())
			{
				MethodInfo method = typeof(UnboxT<T>).GetMethod("NullableField", BindingFlags.Static | BindingFlags.NonPublic);
				MethodInfo method2 = method.MakeGenericMethod(new Type[]
				{
					type.GetGenericArguments()[0]
				});
				return (Converter<object, T>)Delegate.CreateDelegate(typeof(Converter<object, T>), method2);
			}
			return new Converter<object, T>(UnboxT<T>.ValueField);
		}

		// Token: 0x06011299 RID: 70297 RVA: 0x003C8FD0 File Offset: 0x003C71D0
		private static TElem? NullableField<TElem>(object value) where TElem : struct
		{
			if (DBNull.Value == value)
			{
				return null;
			}
			return (TElem?)value;
		}

		// Token: 0x0601129A RID: 70298 RVA: 0x003C8FF8 File Offset: 0x003C71F8
		private static T ReferenceField(object value)
		{
			if (DBNull.Value != value)
			{
				return (T)((object)value);
			}
			return default(T);
		}

		// Token: 0x0601129B RID: 70299 RVA: 0x003C9020 File Offset: 0x003C7220
		private static T ValueField(object value)
		{
			if (DBNull.Value == value)
			{
				throw new InvalidCastException(string.Format(CultureInfo.CurrentCulture, "Type: {0} cannot be casted to Nullable type", new object[]
				{
					typeof(T)
				}));
			}
			return (T)((object)value);
		}

		// Token: 0x04004CD1 RID: 19665
		internal static readonly Converter<object, T> Unbox = UnboxT<T>.Create(typeof(T));
	}
}
