using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace AutoMapper
{
	// Token: 0x02000017 RID: 23
	public interface IMappingExpression<TSource, TDestination>
	{
		// Token: 0x0600009B RID: 155
		IMappingExpression<TSource, TDestination> ForMember(Expression<Func<TDestination, object>> destinationMember, Action<IMemberConfigurationExpression<TSource>> memberOptions);

		// Token: 0x0600009C RID: 156
		IMappingExpression<TSource, TDestination> ForMember(string name, Action<IMemberConfigurationExpression<TSource>> memberOptions);

		// Token: 0x0600009D RID: 157
		void ForAllMembers(Action<IMemberConfigurationExpression<TSource>> memberOptions);

		// Token: 0x0600009E RID: 158
		IMappingExpression<TSource, TDestination> IgnoreAllPropertiesWithAnInaccessibleSetter();

		// Token: 0x0600009F RID: 159
		IMappingExpression<TSource, TDestination> IgnoreAllSourcePropertiesWithAnInaccessibleSetter();

		// Token: 0x060000A0 RID: 160
		IMappingExpression<TSource, TDestination> Include<TOtherSource, TOtherDestination>() where TOtherSource : TSource where TOtherDestination : TDestination;

		// Token: 0x060000A1 RID: 161
		IMappingExpression<TSource, TDestination> IncludeBase<TSourceBase, TDestinationBase>();

		// Token: 0x060000A2 RID: 162
		IMappingExpression<TSource, TDestination> Include(Type derivedSourceType, Type derivedDestinationType);

		// Token: 0x060000A3 RID: 163
		IMappingExpression<TSource, TDestination> WithProfile(string profileName);

		// Token: 0x060000A4 RID: 164
		void ProjectUsing(Expression<Func<TSource, TDestination>> projectionExpression);

		// Token: 0x060000A5 RID: 165
		void ConvertUsing(Func<TSource, TDestination> mappingFunction);

		// Token: 0x060000A6 RID: 166
		void ConvertUsing(Func<ResolutionContext, TDestination> mappingFunction);

		// Token: 0x060000A7 RID: 167
		void ConvertUsing(Func<ResolutionContext, TSource, TDestination> mappingFunction);

		// Token: 0x060000A8 RID: 168
		void ConvertUsing(ITypeConverter<TSource, TDestination> converter);

		// Token: 0x060000A9 RID: 169
		void ConvertUsing<TTypeConverter>() where TTypeConverter : ITypeConverter<TSource, TDestination>;

		// Token: 0x060000AA RID: 170
		IMappingExpression<TSource, TDestination> BeforeMap(Action<TSource, TDestination> beforeFunction);

		// Token: 0x060000AB RID: 171
		IMappingExpression<TSource, TDestination> BeforeMap<TMappingAction>() where TMappingAction : IMappingAction<TSource, TDestination>;

		// Token: 0x060000AC RID: 172
		IMappingExpression<TSource, TDestination> AfterMap(Action<TSource, TDestination> afterFunction);

		// Token: 0x060000AD RID: 173
		IMappingExpression<TSource, TDestination> AfterMap<TMappingAction>() where TMappingAction : IMappingAction<TSource, TDestination>;

		// Token: 0x060000AE RID: 174
		IMappingExpression<TSource, TDestination> ConstructUsing(Func<TSource, TDestination> ctor);

		// Token: 0x060000AF RID: 175
		IMappingExpression<TSource, TDestination> ConstructProjectionUsing(Expression<Func<TSource, TDestination>> ctor);

		// Token: 0x060000B0 RID: 176
		IMappingExpression<TSource, TDestination> ConstructUsing(Func<ResolutionContext, TDestination> ctor);

		// Token: 0x060000B1 RID: 177
		void As<T>();

		// Token: 0x060000B2 RID: 178
		IMappingExpression<TSource, TDestination> MaxDepth(int depth);

		// Token: 0x060000B3 RID: 179
		IMappingExpression<TSource, TDestination> ConstructUsingServiceLocator();

		// Token: 0x060000B4 RID: 180
		IMappingExpression<TDestination, TSource> ReverseMap();

		// Token: 0x060000B5 RID: 181
		IMappingExpression<TSource, TDestination> ForSourceMember(Expression<Func<TSource, object>> sourceMember, Action<ISourceMemberConfigurationExpression> memberOptions);

		// Token: 0x060000B6 RID: 182
		IMappingExpression<TSource, TDestination> ForSourceMember(string sourceMemberName, Action<ISourceMemberConfigurationExpression> memberOptions);

		// Token: 0x060000B7 RID: 183
		IMappingExpression<TSource, TDestination> Substitute(Func<TSource, object> substituteFunc);

		// Token: 0x060000B8 RID: 184
		IMappingExpression<TSource, TDestination> ForCtorParam(string ctorParamName, Action<ICtorParamConfigurationExpression<TSource>> paramOptions);

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B9 RID: 185
		[EditorBrowsable(EditorBrowsableState.Never)]
		TypeMap TypeMap { get; }
	}
}
