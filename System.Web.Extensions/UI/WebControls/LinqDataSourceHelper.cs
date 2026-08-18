using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000A0 RID: 160
	internal class LinqDataSourceHelper
	{
		// Token: 0x06000717 RID: 1815 RVA: 0x0001CE70 File Offset: 0x0001B070
		public static bool EnumerableContentEquals(IEnumerable enumerableA, IEnumerable enumerableB)
		{
			IEnumerator enumerator = enumerableA.GetEnumerator();
			IEnumerator enumerator2 = enumerableB.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator2.MoveNext())
				{
					return false;
				}
				object obj = enumerator.Current;
				object obj2 = enumerator2.Current;
				if (obj == null)
				{
					if (obj2 != null)
					{
						return false;
					}
				}
				else if (!obj.Equals(obj2))
				{
					return false;
				}
			}
			return !enumerator2.MoveNext();
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0001CECC File Offset: 0x0001B0CC
		public static Type FindGenericEnumerableType(Type type)
		{
			while (type != null && type != typeof(object) && type != typeof(string))
			{
				if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
				{
					return type;
				}
				foreach (Type type2 in type.GetInterfaces())
				{
					Type type3 = LinqDataSourceHelper.FindGenericEnumerableType(type2);
					if (type3 != null)
					{
						return type3;
					}
				}
				type = type.BaseType;
			}
			return null;
		}
	}
}
