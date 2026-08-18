using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper.Internal;

namespace AutoMapper
{
	// Token: 0x0200003D RID: 61
	[DebuggerDisplay("{_sourceType.Type.Name} -> {_destinationType.Type.Name}")]
	public class TypeMap
	{
		// Token: 0x0600028E RID: 654 RVA: 0x000064C0 File Offset: 0x000046C0
		public TypeMap(TypeDetails sourceType, TypeDetails destinationType, MemberList memberList, string profileName)
		{
			this._sourceType = sourceType;
			this._destinationType = destinationType;
			this.Types = new TypePair(sourceType.Type, destinationType.Type);
			this.Profile = profileName;
			this.ConfiguredMemberList = memberList;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000655F File Offset: 0x0000475F
		public TypePair Types { get; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00006567 File Offset: 0x00004767
		// (set) Token: 0x06000291 RID: 657 RVA: 0x0000656F File Offset: 0x0000476F
		public ConstructorMap ConstructorMap { get; private set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00006578 File Offset: 0x00004778
		public Type SourceType
		{
			get
			{
				return this._sourceType.Type;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00006585 File Offset: 0x00004785
		public Type DestinationType
		{
			get
			{
				return this._destinationType.Type;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00006592 File Offset: 0x00004792
		// (set) Token: 0x06000295 RID: 661 RVA: 0x0000659A File Offset: 0x0000479A
		public string Profile { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000296 RID: 662 RVA: 0x000065A3 File Offset: 0x000047A3
		// (set) Token: 0x06000297 RID: 663 RVA: 0x000065AB File Offset: 0x000047AB
		public Func<ResolutionContext, object> CustomMapper { get; private set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000298 RID: 664 RVA: 0x000065B4 File Offset: 0x000047B4
		// (set) Token: 0x06000299 RID: 665 RVA: 0x000065BC File Offset: 0x000047BC
		public LambdaExpression CustomProjection { get; private set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600029A RID: 666 RVA: 0x000065C5 File Offset: 0x000047C5
		public Action<object, object> BeforeMap
		{
			get
			{
				return delegate(object src, object dest)
				{
					foreach (Action<object, object> action in this._beforeMapActions)
					{
						action(src, dest);
					}
				};
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600029B RID: 667 RVA: 0x000065D3 File Offset: 0x000047D3
		public Action<object, object> AfterMap
		{
			get
			{
				return delegate(object src, object dest)
				{
					foreach (Action<object, object> action in this._afterMapActions)
					{
						action(src, dest);
					}
				};
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600029C RID: 668 RVA: 0x000065E1 File Offset: 0x000047E1
		// (set) Token: 0x0600029D RID: 669 RVA: 0x000065E9 File Offset: 0x000047E9
		public Func<ResolutionContext, object> DestinationCtor { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600029E RID: 670 RVA: 0x000065F2 File Offset: 0x000047F2
		// (set) Token: 0x0600029F RID: 671 RVA: 0x000065FA File Offset: 0x000047FA
		public IEnumerable<string> IgnorePropertiesStartingWith { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00006603 File Offset: 0x00004803
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x0000660B File Offset: 0x0000480B
		public Type DestinationTypeOverride { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00006614 File Offset: 0x00004814
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x0000661C File Offset: 0x0000481C
		public bool ConstructDestinationUsingServiceLocator { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00006625 File Offset: 0x00004825
		public MemberList ConfiguredMemberList { get; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000662D File Offset: 0x0000482D
		public IEnumerable<TypePair> IncludedDerivedTypes
		{
			get
			{
				return this._includedDerivedTypes;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00006635 File Offset: 0x00004835
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x00006640 File Offset: 0x00004840
		public int MaxDepth
		{
			get
			{
				return this._maxDepth;
			}
			set
			{
				this._maxDepth = value;
				this.SetCondition((ResolutionContext o) => TypeMap.PassesDepthCheck(o, value));
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00006678 File Offset: 0x00004878
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x00006680 File Offset: 0x00004880
		public Func<object, object> Substitution { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002AA RID: 682 RVA: 0x00006689 File Offset: 0x00004889
		// (set) Token: 0x060002AB RID: 683 RVA: 0x00006691 File Offset: 0x00004891
		public LambdaExpression ConstructExpression { get; set; }

		// Token: 0x060002AC RID: 684 RVA: 0x0000669C File Offset: 0x0000489C
		public IEnumerable<PropertyMap> GetPropertyMaps()
		{
			if (!this._sealed)
			{
				return this._propertyMaps.Concat(this._inheritedMaps);
			}
			return this._orderedPropertyMaps;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x000066CB File Offset: 0x000048CB
		public void AddPropertyMap(PropertyMap propertyMap)
		{
			this._propertyMaps.Add(propertyMap);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x000066D9 File Offset: 0x000048D9
		protected void AddInheritedMap(PropertyMap propertyMap)
		{
			this._inheritedMaps.Add(propertyMap);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000066E8 File Offset: 0x000048E8
		public void AddPropertyMap(IMemberAccessor destProperty, IEnumerable<IValueResolver> resolvers)
		{
			PropertyMap propertyMap = new PropertyMap(destProperty);
			resolvers.Each(new Action<IValueResolver>(propertyMap.ChainResolver));
			this.AddPropertyMap(propertyMap);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00006718 File Offset: 0x00004918
		public string[] GetUnmappedPropertyNames()
		{
			Func<PropertyMap, string> selector = delegate(PropertyMap pm)
			{
				if (this.ConfiguredMemberList == MemberList.Destination)
				{
					return pm.DestinationProperty.Name;
				}
				if (pm.CustomExpression != null || !(pm.SourceMember != null))
				{
					return pm.DestinationProperty.Name;
				}
				return pm.SourceMember.Name;
			};
			List<string> second = (from pm in this._propertyMaps
			where pm.IsMapped()
			select pm).Select(selector).ToList<string>();
			List<string> second2 = (from pm in this._inheritedMaps
			where pm.IsMapped()
			select pm).Select(selector).ToList<string>();
			IEnumerable<string> source;
			if (this.ConfiguredMemberList == MemberList.Destination)
			{
				source = (from p in this._destinationType.PublicWriteAccessors
				select p.Name).Except(second).Except(second2);
			}
			else
			{
				IEnumerable<string> second3 = from pm in this._propertyMaps
				where pm.IsMapped() && pm.SourceMember != null && pm.SourceMember.Name != pm.DestinationProperty.Name
				select pm.SourceMember.Name;
				List<string> second4 = (from smc in this._sourceMemberConfigs
				where smc.IsIgnored()
				select smc into pm
				select pm.SourceMember.Name).ToList<string>();
				source = (from p in this._sourceType.PublicReadAccessors
				select p.Name).Except(second).Except(second2).Except(second3).Except(second4);
			}
			return (from memberName in source
			where !this.IgnorePropertiesStartingWith.Any(new Func<string, bool>(memberName.StartsWith))
			select memberName).ToArray<string>();
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x000068F0 File Offset: 0x00004AF0
		public PropertyMap FindOrCreatePropertyMapFor(IMemberAccessor destinationProperty)
		{
			PropertyMap propertyMap = this.GetExistingPropertyMapFor(destinationProperty);
			if (propertyMap != null)
			{
				return propertyMap;
			}
			propertyMap = new PropertyMap(destinationProperty);
			this.AddPropertyMap(propertyMap);
			return propertyMap;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000691C File Offset: 0x00004B1C
		public void IncludeDerivedTypes(Type derivedSourceType, Type derivedDestinationType)
		{
			TypePair typePair = new TypePair(derivedSourceType, derivedDestinationType);
			if (typePair.Equals(this.Types))
			{
				throw new InvalidOperationException("You cannot include a type map into itself.");
			}
			this._includedDerivedTypes.Add(typePair);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00006958 File Offset: 0x00004B58
		public Type GetDerivedTypeFor(Type derivedSourceType)
		{
			TypePair typePair = this._includedDerivedTypes.FirstOrDefault((TypePair tp) => tp.SourceType == derivedSourceType);
			Type result;
			if ((result = this.DestinationTypeOverride) == null)
			{
				result = (((typePair != null) ? typePair.DestinationType : null) ?? this.DestinationType);
			}
			return result;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x000069AA File Offset: 0x00004BAA
		public bool TypeHasBeenIncluded(TypePair derivedTypes)
		{
			return this._includedDerivedTypes.Contains(derivedTypes);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x000069B8 File Offset: 0x00004BB8
		public bool HasDerivedTypesToInclude()
		{
			return this._includedDerivedTypes.Any<TypePair>() || this.DestinationTypeOverride != null;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x000069D5 File Offset: 0x00004BD5
		public void UseCustomMapper(Func<ResolutionContext, object> customMapper)
		{
			this.CustomMapper = customMapper;
			this._propertyMaps.Clear();
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x000069E9 File Offset: 0x00004BE9
		public void AddBeforeMapAction(Action<object, object> beforeMap)
		{
			this._beforeMapActions.Add(beforeMap);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x000069F7 File Offset: 0x00004BF7
		public void AddAfterMapAction(Action<object, object> afterMap)
		{
			this._afterMapActions.Add(afterMap);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00006A08 File Offset: 0x00004C08
		public void Seal()
		{
			if (this._sealed)
			{
				return;
			}
			foreach (TypeMap typeMap in this._inheritedTypeMaps)
			{
				typeMap.Seal();
				this.ApplyInheritedTypeMap(typeMap);
			}
			this._orderedPropertyMaps = (from map in this._propertyMaps.Union(this._inheritedMaps)
			orderby map.GetMappingOrder()
			select map).ToArray<PropertyMap>();
			this._orderedPropertyMaps.Each(delegate(PropertyMap pm)
			{
				pm.Seal();
			});
			foreach (PropertyMap propertyMap in this._inheritedMaps)
			{
				propertyMap.Seal();
			}
			this._sealed = true;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00006B10 File Offset: 0x00004D10
		public bool Equals(TypeMap other)
		{
			return other != null && (this == other || (object.Equals(other._sourceType, this._sourceType) && object.Equals(other._destinationType, this._destinationType)));
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00006B43 File Offset: 0x00004D43
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != typeof(TypeMap)) && this.Equals((TypeMap)obj)));
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00006B75 File Offset: 0x00004D75
		public override int GetHashCode()
		{
			return this._sourceType.GetHashCode() * 397 ^ this._destinationType.GetHashCode();
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00006B94 File Offset: 0x00004D94
		public PropertyMap GetExistingPropertyMapFor(IMemberAccessor destinationProperty)
		{
			PropertyMap propertyMap = this._propertyMaps.FirstOrDefault((PropertyMap pm) => pm.DestinationProperty.Name.Equals(destinationProperty.Name));
			if (propertyMap != null)
			{
				return propertyMap;
			}
			propertyMap = this._inheritedMaps.FirstOrDefault((PropertyMap pm) => pm.DestinationProperty.Name.Equals(destinationProperty.Name));
			if (propertyMap == null)
			{
				return null;
			}
			PropertyInfo propertyInfo = propertyMap.DestinationProperty.MemberInfo as PropertyInfo;
			if (propertyInfo == null)
			{
				return propertyMap;
			}
			MethodInfo getMethod = propertyInfo.GetMethod;
			if (getMethod.IsAbstract || getMethod.IsVirtual)
			{
				return propertyMap;
			}
			MethodInfo getMethod2 = ((PropertyInfo)destinationProperty.MemberInfo).GetMethod;
			if (getMethod.DeclaringType == getMethod2.DeclaringType)
			{
				return propertyMap;
			}
			return null;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x000066D9 File Offset: 0x000048D9
		public void AddInheritedPropertyMap(PropertyMap mappedProperty)
		{
			this._inheritedMaps.Add(mappedProperty);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00006C4C File Offset: 0x00004E4C
		public void InheritTypes(TypeMap inheritedTypeMap)
		{
			foreach (TypePair item in from includedDerivedType in inheritedTypeMap._includedDerivedTypes
			where !this._includedDerivedTypes.Contains(includedDerivedType)
			select includedDerivedType)
			{
				this._includedDerivedTypes.Add(item);
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00006CB0 File Offset: 0x00004EB0
		public void SetCondition(Func<ResolutionContext, bool> condition)
		{
			this._condition = condition;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00006CB9 File Offset: 0x00004EB9
		public bool ShouldAssignValue(ResolutionContext resolutionContext)
		{
			return this._condition == null || this._condition(resolutionContext);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00006CD4 File Offset: 0x00004ED4
		public void AddConstructorMap(ConstructorInfo constructorInfo, IEnumerable<ConstructorParameterMap> parameters)
		{
			ConstructorMap constructorMap = new ConstructorMap(constructorInfo, parameters);
			this.ConstructorMap = constructorMap;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00006CF0 File Offset: 0x00004EF0
		public SourceMemberConfig FindOrCreateSourceMemberConfigFor(MemberInfo sourceMember)
		{
			SourceMemberConfig sourceMemberConfig = this._sourceMemberConfigs.FirstOrDefault((SourceMemberConfig smc) => smc.SourceMember == sourceMember);
			if (sourceMemberConfig == null)
			{
				sourceMemberConfig = new SourceMemberConfig(sourceMember);
				this._sourceMemberConfigs.Add(sourceMemberConfig);
			}
			return sourceMemberConfig;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00006D40 File Offset: 0x00004F40
		private static bool PassesDepthCheck(ResolutionContext context, int maxDepth)
		{
			if (context.InstanceCache.ContainsKey(context))
			{
				return true;
			}
			ResolutionContext resolutionContext = context;
			int num = 1;
			while (resolutionContext.Parent != null)
			{
				if (resolutionContext.SourceType == context.TypeMap.SourceType && resolutionContext.DestinationType == context.TypeMap.DestinationType)
				{
					num++;
				}
				resolutionContext = resolutionContext.Parent;
			}
			return num <= maxDepth;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00006DAD File Offset: 0x00004FAD
		public void UseCustomProjection(LambdaExpression projectionExpression)
		{
			this.CustomProjection = projectionExpression;
			this._propertyMaps.Clear();
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00006DC1 File Offset: 0x00004FC1
		public void ApplyInheritedMap(TypeMap inheritedTypeMap)
		{
			this._inheritedTypeMaps.Add(inheritedTypeMap);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00006DCF File Offset: 0x00004FCF
		public bool ShouldCheckForValid()
		{
			return this.CustomMapper == null && this.CustomProjection == null && this.DestinationTypeOverride == null;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00006DF0 File Offset: 0x00004FF0
		private void ApplyInheritedTypeMap(TypeMap inheritedTypeMap)
		{
			using (IEnumerator<PropertyMap> enumerator = (from m in inheritedTypeMap.GetPropertyMaps()
			where m.IsMapped()
			select m).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					PropertyMap inheritedMappedProperty = enumerator.Current;
					PropertyMap propertyMap = this.GetPropertyMaps().SingleOrDefault((PropertyMap m) => m.DestinationProperty.Name == inheritedMappedProperty.DestinationProperty.Name);
					if (propertyMap != null && inheritedMappedProperty.HasCustomValueResolver && !propertyMap.HasCustomValueResolver)
					{
						propertyMap.AssignCustomValueResolver(inheritedMappedProperty.GetSourceValueResolvers().First<IValueResolver>());
						propertyMap.AssignCustomExpression(inheritedMappedProperty.CustomExpression);
					}
					else if (propertyMap == null)
					{
						PropertyMap mappedProperty = new PropertyMap(inheritedMappedProperty);
						this.AddInheritedPropertyMap(mappedProperty);
					}
				}
			}
			if (inheritedTypeMap.BeforeMap != null)
			{
				this.AddBeforeMapAction(inheritedTypeMap.BeforeMap);
			}
			if (inheritedTypeMap.AfterMap != null)
			{
				this.AddAfterMapAction(inheritedTypeMap.AfterMap);
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00006F04 File Offset: 0x00005104
		internal LambdaExpression DestinationConstructorExpression(Expression instanceParameter)
		{
			LambdaExpression constructExpression = this.ConstructExpression;
			if (constructExpression != null)
			{
				return constructExpression;
			}
			Expression body;
			if (this.ConstructorMap != null)
			{
				if (this.ConstructorMap.CtorParams.All((ConstructorParameterMap p) => p.CanResolve))
				{
					body = this.ConstructorMap.NewExpression(instanceParameter);
					goto IL_6A;
				}
			}
			body = Expression.New(this.DestinationTypeOverride ?? this.DestinationType);
			IL_6A:
			return Expression.Lambda(body, new ParameterExpression[0]);
		}

		// Token: 0x04000075 RID: 117
		private readonly IList<Action<object, object>> _afterMapActions = new List<Action<object, object>>();

		// Token: 0x04000076 RID: 118
		private readonly IList<Action<object, object>> _beforeMapActions = new List<Action<object, object>>();

		// Token: 0x04000077 RID: 119
		private readonly TypeDetails _destinationType;

		// Token: 0x04000078 RID: 120
		private readonly ISet<TypePair> _includedDerivedTypes = new HashSet<TypePair>();

		// Token: 0x04000079 RID: 121
		private readonly ThreadSafeList<PropertyMap> _propertyMaps = new ThreadSafeList<PropertyMap>();

		// Token: 0x0400007A RID: 122
		private readonly ThreadSafeList<SourceMemberConfig> _sourceMemberConfigs = new ThreadSafeList<SourceMemberConfig>();

		// Token: 0x0400007B RID: 123
		private readonly IList<PropertyMap> _inheritedMaps = new List<PropertyMap>();

		// Token: 0x0400007C RID: 124
		private PropertyMap[] _orderedPropertyMaps;

		// Token: 0x0400007D RID: 125
		private readonly TypeDetails _sourceType;

		// Token: 0x0400007E RID: 126
		private bool _sealed;

		// Token: 0x0400007F RID: 127
		private Func<ResolutionContext, bool> _condition;

		// Token: 0x04000080 RID: 128
		private int _maxDepth = int.MaxValue;

		// Token: 0x04000081 RID: 129
		private readonly IList<TypeMap> _inheritedTypeMaps = new List<TypeMap>();
	}
}
