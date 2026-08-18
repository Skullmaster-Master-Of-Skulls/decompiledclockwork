using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace AutoMapper
{
	// Token: 0x02000016 RID: 22
	public interface IMappingExpression
	{
		// Token: 0x06000082 RID: 130
		IMappingExpression ForCtorParam(string ctorParamName, Action<ICtorParamConfigurationExpression<object>> paramOptions);

		// Token: 0x06000083 RID: 131
		IMappingExpression ReverseMap();

		// Token: 0x06000084 RID: 132
		IMappingExpression Substitute(Func<object, object> substituteFunc);

		// Token: 0x06000085 RID: 133
		IMappingExpression ConstructUsingServiceLocator();

		// Token: 0x06000086 RID: 134
		IMappingExpression MaxDepth(int depth);

		// Token: 0x06000087 RID: 135
		IMappingExpression ConstructProjectionUsing(LambdaExpression ctor);

		// Token: 0x06000088 RID: 136
		IMappingExpression ConstructUsing(Func<ResolutionContext, object> ctor);

		// Token: 0x06000089 RID: 137
		IMappingExpression ConstructUsing(Func<object, object> ctor);

		// Token: 0x0600008A RID: 138
		void ProjectUsing(Expression<Func<object, object>> projectionExpression);

		// Token: 0x0600008B RID: 139
		void ForAllMembers(Action<IMemberConfigurationExpression> memberOptions);

		// Token: 0x0600008C RID: 140
		IMappingExpression ForSourceMember(string sourceMemberName, Action<ISourceMemberConfigurationExpression> memberOptions);

		// Token: 0x0600008D RID: 141
		IMappingExpression WithProfile(string profileName);

		// Token: 0x0600008E RID: 142
		void ConvertUsing<TTypeConverter>();

		// Token: 0x0600008F RID: 143
		void ConvertUsing(Type typeConverterType);

		// Token: 0x06000090 RID: 144
		void As(Type typeOverride);

		// Token: 0x06000091 RID: 145
		IMappingExpression ForMember(string name, Action<IMemberConfigurationExpression> memberOptions);

		// Token: 0x06000092 RID: 146
		IMappingExpression Include(Type derivedSourceType, Type derivedDestinationType);

		// Token: 0x06000093 RID: 147
		IMappingExpression IgnoreAllPropertiesWithAnInaccessibleSetter();

		// Token: 0x06000094 RID: 148
		IMappingExpression IgnoreAllSourcePropertiesWithAnInaccessibleSetter();

		// Token: 0x06000095 RID: 149
		IMappingExpression IncludeBase(Type sourceBase, Type destinationBase);

		// Token: 0x06000096 RID: 150
		IMappingExpression BeforeMap(Action<object, object> beforeFunction);

		// Token: 0x06000097 RID: 151
		IMappingExpression BeforeMap<TMappingAction>() where TMappingAction : IMappingAction<object, object>;

		// Token: 0x06000098 RID: 152
		IMappingExpression AfterMap(Action<object, object> afterFunction);

		// Token: 0x06000099 RID: 153
		IMappingExpression AfterMap<TMappingAction>() where TMappingAction : IMappingAction<object, object>;

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600009A RID: 154
		[EditorBrowsable(EditorBrowsableState.Never)]
		TypeMap TypeMap { get; }
	}
}
