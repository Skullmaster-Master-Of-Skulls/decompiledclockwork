using System;
using System.Linq.Expressions;

namespace AutoMapper
{
	// Token: 0x02000024 RID: 36
	public interface IResolutionExpression<TSource> : IResolutionExpression
	{
		// Token: 0x0600011B RID: 283
		void FromMember(Expression<Func<TSource, object>> sourceMember);
	}
}
