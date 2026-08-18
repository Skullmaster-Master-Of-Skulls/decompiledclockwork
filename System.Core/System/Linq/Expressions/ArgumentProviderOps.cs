using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000240 RID: 576
	internal static class ArgumentProviderOps
	{
		// Token: 0x0600152E RID: 5422 RVA: 0x000480BC File Offset: 0x000462BC
		internal static T[] Map<T>(this IArgumentProvider collection, Func<Expression, T> select)
		{
			int num = collection.ArgumentCount;
			T[] array = new T[num];
			num = 0;
			for (int i = 0; i < num; i++)
			{
				array[i] = select(collection.GetArgument(i));
			}
			return array;
		}
	}
}
