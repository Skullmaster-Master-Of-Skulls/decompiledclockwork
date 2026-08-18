using System;
using System.Linq.Expressions;

namespace System.Web.Http.Internal
{
	// Token: 0x020000EC RID: 236
	internal static class TypeActivator
	{
		// Token: 0x060005F0 RID: 1520 RVA: 0x000138A4 File Offset: 0x00011AA4
		public static Func<TBase> Create<TBase>(Type instanceType) where TBase : class
		{
			NewExpression body = Expression.New(instanceType);
			return Expression.Lambda<Func<TBase>>(body, new ParameterExpression[0]).Compile();
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x000138C9 File Offset: 0x00011AC9
		public static Func<TInstance> Create<TInstance>() where TInstance : class
		{
			return TypeActivator.Create<TInstance>(typeof(TInstance));
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x000138DA File Offset: 0x00011ADA
		public static Func<object> Create(Type instanceType)
		{
			return TypeActivator.Create<object>(instanceType);
		}
	}
}
