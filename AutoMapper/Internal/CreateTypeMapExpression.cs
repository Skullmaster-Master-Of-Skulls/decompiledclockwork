using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AutoMapper.Internal
{
	// Token: 0x02000094 RID: 148
	public class CreateTypeMapExpression : IMappingExpression
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00012025 File Offset: 0x00010225
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x0001202D File Offset: 0x0001022D
		public TypePair TypePair { get; private set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x00012036 File Offset: 0x00010236
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x0001203E File Offset: 0x0001023E
		public MemberList MemberList { get; private set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x00012047 File Offset: 0x00010247
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x0001204F File Offset: 0x0001024F
		public string ProfileName { get; private set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x00008F3F File Offset: 0x0000713F
		public TypeMap TypeMap
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00012058 File Offset: 0x00010258
		public CreateTypeMapExpression(TypePair typePair, MemberList memberList, string profileName)
		{
			this.TypePair = typePair;
			this.MemberList = memberList;
			this.ProfileName = profileName;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00012080 File Offset: 0x00010280
		public void ConvertUsing<TTypeConverter>()
		{
			this._actions.Add(delegate(IMappingExpression me)
			{
				me.ConvertUsing<TTypeConverter>();
			});
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x000120AC File Offset: 0x000102AC
		public void ConvertUsing(Type typeConverterType)
		{
			this._actions.Add(delegate(IMappingExpression me)
			{
				me.ConvertUsing(typeConverterType);
			});
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x000120E0 File Offset: 0x000102E0
		public void As(Type typeOverride)
		{
			this._actions.Add(delegate(IMappingExpression me)
			{
				me.As(typeOverride);
			});
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00012114 File Offset: 0x00010314
		public IMappingExpression WithProfile(string profileName)
		{
			this._actions.Add(delegate(IMappingExpression me)
			{
				me.WithProfile(profileName);
			});
			return this;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00012148 File Offset: 0x00010348
		public IMappingExpression ForMember(string name, Action<IMemberConfigurationExpression> memberOptions)
		{
			this._actions.Add(delegate(IMappingExpression me)
			{
				me.ForMember(name, memberOptions);
			});
			return this;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00012184 File Offset: 0x00010384
		public IMappingExpression ForSourceMember(string sourceMemberName, Action<ISourceMemberConfigurationExpression> memberOptions)
		{
			this._actions.Add(delegate(IMappingExpression me)
			{
				me.ForSourceMember(sourceMemberName, memberOptions);
			});
			return this;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x000121C0 File Offset: 0x000103C0
		public void Accept(IMappingExpression mappingExpression)
		{
			foreach (Action<IMappingExpression> action in this._actions)
			{
				action(mappingExpression);
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00012214 File Offset: 0x00010414
		public IMappingExpression Include(Type derivedSourceType, Type derivedDestinationType)
		{
			this._actions.Add(delegate(IMappingExpression me)
			{
				me.Include(derivedSourceType, derivedDestinationType);
			});
			return this;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00012250 File Offset: 0x00010450
		public void ForAllMembers(Action<IMemberConfigurationExpression> memberOptions)
		{
			this._actions.Add(delegate(IMappingExpression me)
			{
				me.ForAllMembers(memberOptions);
			});
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00012281 File Offset: 0x00010481
		public IMappingExpression IgnoreAllPropertiesWithAnInaccessibleSetter()
		{
			this._actions.Add(delegate(IMappingExpression me)
			{
				me.IgnoreAllPropertiesWithAnInaccessibleSetter();
			});
			return this;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x000122AE File Offset: 0x000104AE
		public IMappingExpression IgnoreAllSourcePropertiesWithAnInaccessibleSetter()
		{
			this._actions.Add(delegate(IMappingExpression me)
			{
				me.IgnoreAllSourcePropertiesWithAnInaccessibleSetter();
			});
			return this;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x000122DC File Offset: 0x000104DC
		public IMappingExpression IncludeBase(Type sourceBase, Type destinationBase)
		{
			this._actions.Add(delegate(IMappingExpression me)
			{
				me.IncludeBase(sourceBase, destinationBase);
			});
			return this;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00012318 File Offset: 0x00010518
		public void ProjectUsing(Expression<Func<object, object>> projectionExpression)
		{
			this._actions.Add(delegate(IMappingExpression me)
			{
				me.ProjectUsing(projectionExpression);
			});
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0001234C File Offset: 0x0001054C
		public IMappingExpression BeforeMap(Action<object, object> beforeFunction)
		{
			this._actions.Add(delegate(IMappingExpression me)
			{
				me.BeforeMap(beforeFunction);
			});
			return this;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0001237E File Offset: 0x0001057E
		public IMappingExpression BeforeMap<TMappingAction>() where TMappingAction : IMappingAction<object, object>
		{
			this._actions.Add(delegate(IMappingExpression me)
			{
				me.BeforeMap<TMappingAction>();
			});
			return this;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x000123AC File Offset: 0x000105AC
		public IMappingExpression AfterMap(Action<object, object> afterFunction)
		{
			this._actions.Add(delegate(IMappingExpression me)
			{
				me.AfterMap(afterFunction);
			});
			return this;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x000123DE File Offset: 0x000105DE
		public IMappingExpression AfterMap<TMappingAction>() where TMappingAction : IMappingAction<object, object>
		{
			this._actions.Add(delegate(IMappingExpression me)
			{
				me.AfterMap<TMappingAction>();
			});
			return this;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0001240C File Offset: 0x0001060C
		public IMappingExpression ConstructUsing(Func<object, object> ctor)
		{
			this._actions.Add(delegate(IMappingExpression me)
			{
				me.ConstructUsing(ctor);
			});
			return this;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00012440 File Offset: 0x00010640
		public IMappingExpression ConstructUsing(Func<ResolutionContext, object> ctor)
		{
			this._actions.Add(delegate(IMappingExpression me)
			{
				me.ConstructUsing(ctor);
			});
			return this;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00012474 File Offset: 0x00010674
		public IMappingExpression ConstructProjectionUsing(LambdaExpression ctor)
		{
			this._actions.Add(delegate(IMappingExpression me)
			{
				me.ConstructProjectionUsing(ctor);
			});
			return this;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x000124A8 File Offset: 0x000106A8
		public IMappingExpression MaxDepth(int depth)
		{
			this._actions.Add(delegate(IMappingExpression me)
			{
				me.MaxDepth(depth);
			});
			return this;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x000124DA File Offset: 0x000106DA
		public IMappingExpression ConstructUsingServiceLocator()
		{
			this._actions.Add(delegate(IMappingExpression me)
			{
				me.ConstructUsingServiceLocator();
			});
			return this;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00012508 File Offset: 0x00010708
		public IMappingExpression Substitute(Func<object, object> substituteFunc)
		{
			this._actions.Add(delegate(IMappingExpression me)
			{
				me.Substitute(substituteFunc);
			});
			return this;
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0001253A File Offset: 0x0001073A
		public IMappingExpression ReverseMap()
		{
			this._actions.Add(delegate(IMappingExpression me)
			{
				me.ReverseMap();
			});
			return null;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00012568 File Offset: 0x00010768
		public IMappingExpression ForCtorParam(string ctorParamName, Action<ICtorParamConfigurationExpression<object>> paramOptions)
		{
			this._actions.Add(delegate(IMappingExpression me)
			{
				me.ForCtorParam(ctorParamName, paramOptions);
			});
			return this;
		}

		// Token: 0x040000D1 RID: 209
		private readonly List<Action<IMappingExpression>> _actions = new List<Action<IMappingExpression>>();
	}
}
