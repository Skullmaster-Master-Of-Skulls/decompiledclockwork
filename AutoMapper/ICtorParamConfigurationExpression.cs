using System;
using System.Linq.Expressions;

namespace AutoMapper
{
	// Token: 0x0200000F RID: 15
	public interface ICtorParamConfigurationExpression<TSource>
	{
		// Token: 0x06000067 RID: 103
		void MapFrom<TMember>(Expression<Func<TSource, TMember>> sourceMember);
	}
}
