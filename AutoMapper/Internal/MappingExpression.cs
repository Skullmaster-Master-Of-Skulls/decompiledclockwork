using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AutoMapper.Internal
{
	// Token: 0x020000AB RID: 171
	public class MappingExpression : MappingExpression<object, object>, IMappingExpression, IMemberConfigurationExpression, IMemberConfigurationExpression<object>
	{
		// Token: 0x060004CE RID: 1230 RVA: 0x00012CD9 File Offset: 0x00010ED9
		public MappingExpression(TypeMap typeMap, Func<Type, object> typeConverterCtor, IProfileExpression configurationContainer) : base(typeMap, typeConverterCtor, configurationContainer)
		{
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00012CE4 File Offset: 0x00010EE4
		public new IMappingExpression ReverseMap()
		{
			IMappingExpression mappingExpression = base.Profile.CreateMap(base.TypeMap.DestinationType, base.TypeMap.SourceType, MemberList.Source);
			return (IMappingExpression)base.ConfigureReverseMap((MappingExpression)mappingExpression);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00012D25 File Offset: 0x00010F25
		public new IMappingExpression Substitute(Func<object, object> substituteFunc)
		{
			return (IMappingExpression)base.Substitute(substituteFunc);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00012D33 File Offset: 0x00010F33
		public new IMappingExpression ConstructUsingServiceLocator()
		{
			return (IMappingExpression)base.ConstructUsingServiceLocator();
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00012D40 File Offset: 0x00010F40
		public void ForAllMembers(Action<IMemberConfigurationExpression> memberOptions)
		{
			base.ForAllMembers(delegate(IMemberConfigurationExpression<object> o)
			{
				memberOptions((IMemberConfigurationExpression)o);
			});
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00012D6C File Offset: 0x00010F6C
		void IMappingExpression.ConvertUsing<TTypeConverter>()
		{
			this.ConvertUsing(typeof(TTypeConverter));
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00012D80 File Offset: 0x00010F80
		public void ConvertUsing(Type typeConverterType)
		{
			Type type = typeof(ITypeConverter<, >).MakeGenericType(new Type[]
			{
				base.TypeMap.SourceType,
				base.TypeMap.DestinationType
			});
			DeferredInstantiatedConverter @object = new DeferredInstantiatedConverter(type.IsAssignableFrom(typeConverterType) ? type : typeConverterType, base.BuildCtor<object>(typeConverterType));
			base.TypeMap.UseCustomMapper(new Func<ResolutionContext, object>(@object.Convert));
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00012DF1 File Offset: 0x00010FF1
		public void As(Type typeOverride)
		{
			base.TypeMap.DestinationTypeOverride = typeOverride;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00012E00 File Offset: 0x00011000
		public IMappingExpression ForMember(string name, Action<IMemberConfigurationExpression> memberOptions)
		{
			return (IMappingExpression)base.ForMember(name, delegate(IMemberConfigurationExpression<object> c)
			{
				memberOptions((IMemberConfigurationExpression)c);
			});
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00012E32 File Offset: 0x00011032
		IMappingExpression IMappingExpression.WithProfile(string profileName)
		{
			return (IMappingExpression)base.WithProfile(profileName);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00012E40 File Offset: 0x00011040
		public new IMappingExpression ForSourceMember(string sourceMemberName, Action<ISourceMemberConfigurationExpression> memberOptions)
		{
			return (IMappingExpression)base.ForSourceMember(sourceMemberName, memberOptions);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00012E50 File Offset: 0x00011050
		public void MapFrom(string sourceMember)
		{
			MemberInfo[] member = base.TypeMap.SourceType.GetMember(sourceMember);
			if (!member.Any<MemberInfo>())
			{
				throw new AutoMapperConfigurationException(string.Format("Unable to find source member {0} on type {1}", sourceMember, base.TypeMap.SourceType.FullName));
			}
			if (member.Skip(1).Any<MemberInfo>())
			{
				throw new AutoMapperConfigurationException(string.Format("Source member {0} is ambiguous on type {1}", sourceMember, base.TypeMap.SourceType.FullName));
			}
			MemberInfo memberInfo = member.Single<MemberInfo>();
			base.PropertyMap.SourceMember = memberInfo;
			base.PropertyMap.AssignCustomValueResolver(memberInfo.ToMemberGetter());
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00012EE9 File Offset: 0x000110E9
		public new IMappingExpression Include(Type otherSourceType, Type otherDestinationType)
		{
			return (IMappingExpression)base.Include(otherSourceType, otherDestinationType);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00012EF8 File Offset: 0x000110F8
		public new IMappingExpression IgnoreAllPropertiesWithAnInaccessibleSetter()
		{
			return (IMappingExpression)base.IgnoreAllPropertiesWithAnInaccessibleSetter();
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00012F05 File Offset: 0x00011105
		public new IMappingExpression IgnoreAllSourcePropertiesWithAnInaccessibleSetter()
		{
			return (IMappingExpression)base.IgnoreAllSourcePropertiesWithAnInaccessibleSetter();
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00012F12 File Offset: 0x00011112
		public new IMappingExpression IncludeBase(Type sourceBase, Type destinationBase)
		{
			return (IMappingExpression)base.IncludeBase(sourceBase, destinationBase);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00012F21 File Offset: 0x00011121
		public void ProjectUsing(LambdaExpression projectionExpression)
		{
			base.TypeMap.UseCustomProjection(projectionExpression);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00012F2F File Offset: 0x0001112F
		public new IMappingExpression BeforeMap(Action<object, object> beforeFunction)
		{
			return (IMappingExpression)base.BeforeMap(beforeFunction);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00012F3D File Offset: 0x0001113D
		public new IMappingExpression BeforeMap<TMappingAction>() where TMappingAction : IMappingAction<object, object>
		{
			return (IMappingExpression)base.BeforeMap<TMappingAction>();
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00012F4A File Offset: 0x0001114A
		public new IMappingExpression AfterMap(Action<object, object> afterFunction)
		{
			return (IMappingExpression)base.AfterMap(afterFunction);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00012F58 File Offset: 0x00011158
		public new IMappingExpression AfterMap<TMappingAction>() where TMappingAction : IMappingAction<object, object>
		{
			return (IMappingExpression)base.AfterMap<TMappingAction>();
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00012F65 File Offset: 0x00011165
		public new IMappingExpression ConstructUsing(Func<object, object> ctor)
		{
			return (IMappingExpression)base.ConstructUsing(ctor);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00012F73 File Offset: 0x00011173
		public new IMappingExpression ConstructUsing(Func<ResolutionContext, object> ctor)
		{
			return (IMappingExpression)base.ConstructUsing(ctor);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00012F84 File Offset: 0x00011184
		public IMappingExpression ConstructProjectionUsing(LambdaExpression ctor)
		{
			Delegate func = ctor.Compile();
			base.TypeMap.ConstructExpression = ctor;
			return this.ConstructUsing((ResolutionContext ctxt) => func.DynamicInvoke(new object[]
			{
				ctxt.SourceValue
			}));
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00012FC1 File Offset: 0x000111C1
		public new IMappingExpression MaxDepth(int depth)
		{
			return (IMappingExpression)base.MaxDepth(depth);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00012FCF File Offset: 0x000111CF
		public new IMappingExpression ForCtorParam(string ctorParamName, Action<ICtorParamConfigurationExpression<object>> paramOptions)
		{
			return (IMappingExpression)base.ForCtorParam(ctorParamName, paramOptions);
		}
	}
}
