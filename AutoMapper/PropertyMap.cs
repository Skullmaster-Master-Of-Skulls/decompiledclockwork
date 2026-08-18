using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper.Internal;

namespace AutoMapper
{
	// Token: 0x02000036 RID: 54
	[DebuggerDisplay("{DestinationProperty.Name}")]
	public class PropertyMap
	{
		// Token: 0x06000204 RID: 516 RVA: 0x0000527E File Offset: 0x0000347E
		public PropertyMap(IMemberAccessor destinationProperty)
		{
			this.UseDestinationValue = true;
			this.DestinationProperty = destinationProperty;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000052A0 File Offset: 0x000034A0
		public PropertyMap(PropertyMap inheritedMappedProperty) : this(inheritedMappedProperty.DestinationProperty)
		{
			if (inheritedMappedProperty.IsIgnored())
			{
				this.Ignore();
			}
			else
			{
				foreach (IValueResolver valueResolver in inheritedMappedProperty.GetSourceValueResolvers())
				{
					this.ChainResolver(valueResolver);
				}
			}
			this.ApplyCondition(inheritedMappedProperty._condition);
			this.SetNullSubstitute(inheritedMappedProperty.NullSubstitute);
			this.SetMappingOrder(inheritedMappedProperty._mappingOrder);
			this.CustomExpression = inheritedMappedProperty.CustomExpression;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000533C File Offset: 0x0000353C
		public IMemberAccessor DestinationProperty { get; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00005344 File Offset: 0x00003544
		public Type DestinationPropertyType
		{
			get
			{
				return this.DestinationProperty.MemberType;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00005351 File Offset: 0x00003551
		// (set) Token: 0x06000209 RID: 521 RVA: 0x00005359 File Offset: 0x00003559
		public LambdaExpression CustomExpression { get; private set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00005362 File Offset: 0x00003562
		// (set) Token: 0x0600020B RID: 523 RVA: 0x00005389 File Offset: 0x00003589
		public MemberInfo SourceMember
		{
			get
			{
				MemberInfo result;
				if ((result = this._sourceMember) == null)
				{
					IMemberGetter memberGetter = this.GetSourceValueResolvers().OfType<IMemberGetter>().LastOrDefault<IMemberGetter>();
					if (memberGetter == null)
					{
						return null;
					}
					result = memberGetter.MemberInfo;
				}
				return result;
			}
			internal set
			{
				this._sourceMember = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00005392 File Offset: 0x00003592
		public bool CanBeSet
		{
			get
			{
				return !(this.DestinationProperty is PropertyAccessor) || ((PropertyAccessor)this.DestinationProperty).HasSetter;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600020D RID: 525 RVA: 0x000053B3 File Offset: 0x000035B3
		// (set) Token: 0x0600020E RID: 526 RVA: 0x000053BB File Offset: 0x000035BB
		public bool UseDestinationValue { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600020F RID: 527 RVA: 0x000053C4 File Offset: 0x000035C4
		// (set) Token: 0x06000210 RID: 528 RVA: 0x000053CC File Offset: 0x000035CC
		internal bool HasCustomValueResolver { get; private set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000211 RID: 529 RVA: 0x000053D5 File Offset: 0x000035D5
		// (set) Token: 0x06000212 RID: 530 RVA: 0x000053DD File Offset: 0x000035DD
		public bool ExplicitExpansion { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000213 RID: 531 RVA: 0x000053E6 File Offset: 0x000035E6
		// (set) Token: 0x06000214 RID: 532 RVA: 0x000053EE File Offset: 0x000035EE
		public object NullSubstitute { get; private set; }

		// Token: 0x06000215 RID: 533 RVA: 0x000053F7 File Offset: 0x000035F7
		public IEnumerable<IValueResolver> GetSourceValueResolvers()
		{
			if (this._customMemberResolver != null)
			{
				yield return this._customMemberResolver;
			}
			if (this._customResolver != null)
			{
				yield return this._customResolver;
			}
			foreach (IValueResolver valueResolver in this._sourceValueResolvers)
			{
				yield return valueResolver;
			}
			LinkedList<IValueResolver>.Enumerator enumerator = default(LinkedList<IValueResolver>.Enumerator);
			if (this.NullSubstitute != null)
			{
				yield return new NullReplacementMethod(this.NullSubstitute);
			}
			yield break;
			yield break;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00005407 File Offset: 0x00003607
		public void RemoveLastResolver()
		{
			this._sourceValueResolvers.RemoveLast();
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00005414 File Offset: 0x00003614
		public ResolutionResult ResolveValue(ResolutionContext context)
		{
			this.Seal();
			ResolutionResult seed = new ResolutionResult(context);
			return this._cachedResolvers.Aggregate(seed, (ResolutionResult current, IValueResolver resolver) => resolver.Resolve(current));
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00005459 File Offset: 0x00003659
		internal void Seal()
		{
			if (this._sealed)
			{
				return;
			}
			this._cachedResolvers = this.GetSourceValueResolvers().ToArray<IValueResolver>();
			this._sealed = true;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000547C File Offset: 0x0000367C
		public void ChainResolver(IValueResolver valueResolver)
		{
			this._sourceValueResolvers.AddLast(valueResolver);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000548B File Offset: 0x0000368B
		public void AssignCustomExpression(LambdaExpression customExpression)
		{
			this.CustomExpression = customExpression;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00005494 File Offset: 0x00003694
		public void AssignCustomValueResolver(IValueResolver valueResolver)
		{
			this._ignored = false;
			this._customResolver = valueResolver;
			this.ResetSourceMemberChain();
			this.HasCustomValueResolver = true;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000054B1 File Offset: 0x000036B1
		public void ChainTypeMemberForResolver(IValueResolver valueResolver)
		{
			this.ResetSourceMemberChain();
			this._customMemberResolver = valueResolver;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000054C0 File Offset: 0x000036C0
		public void ChainConstructorForResolver(IValueResolver valueResolver)
		{
			this._customResolver = valueResolver;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000054C9 File Offset: 0x000036C9
		public void Ignore()
		{
			this._ignored = true;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000054D2 File Offset: 0x000036D2
		public bool IsIgnored()
		{
			return this._ignored;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000054DA File Offset: 0x000036DA
		public void SetMappingOrder(int mappingOrder)
		{
			this._mappingOrder = mappingOrder;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000054E3 File Offset: 0x000036E3
		public int GetMappingOrder()
		{
			return this._mappingOrder;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000054EB File Offset: 0x000036EB
		public bool IsMapped()
		{
			return this._sourceValueResolvers.Count > 0 || this.HasCustomValueResolver || this._ignored;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000550B File Offset: 0x0000370B
		public bool CanResolveValue()
		{
			return (this._sourceValueResolvers.Count > 0 || this.HasCustomValueResolver) && !this._ignored;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000552E File Offset: 0x0000372E
		public void SetNullSubstitute(object nullSubstitute)
		{
			this.NullSubstitute = nullSubstitute;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00005537 File Offset: 0x00003737
		private void ResetSourceMemberChain()
		{
			this._sourceValueResolvers.Clear();
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00005544 File Offset: 0x00003744
		public bool Equals(PropertyMap other)
		{
			return other != null && (this == other || object.Equals(other.DestinationProperty, this.DestinationProperty));
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00005562 File Offset: 0x00003762
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != typeof(PropertyMap)) && this.Equals((PropertyMap)obj)));
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00005594 File Offset: 0x00003794
		public override int GetHashCode()
		{
			return this.DestinationProperty.GetHashCode();
		}

		// Token: 0x06000229 RID: 553 RVA: 0x000055A1 File Offset: 0x000037A1
		public void ApplyCondition(Func<ResolutionContext, bool> condition)
		{
			this._condition = condition;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000055AA File Offset: 0x000037AA
		public void ApplyPreCondition(Func<ResolutionContext, bool> condition)
		{
			this._preCondition = condition;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000055B3 File Offset: 0x000037B3
		public bool ShouldAssignValue(ResolutionContext context)
		{
			return this._condition == null || this._condition(context);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000055CB File Offset: 0x000037CB
		public bool ShouldAssignValuePreResolving(ResolutionContext context)
		{
			return this._preCondition == null || this._preCondition(context);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000055E4 File Offset: 0x000037E4
		public void SetCustomValueResolverExpression<TSource, TMember>(Expression<Func<TSource, TMember>> sourceMember)
		{
			MemberExpression memberExpression = sourceMember.Body as MemberExpression;
			if (memberExpression != null)
			{
				this.SourceMember = memberExpression.Member;
			}
			this.CustomExpression = sourceMember;
			this.AssignCustomValueResolver(new NullReferenceExceptionSwallowingResolver(new DelegateBasedResolver<TSource, TMember>(sourceMember.Compile())));
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00005629 File Offset: 0x00003829
		public object GetDestinationValue(object mappedObject)
		{
			if (!this.UseDestinationValue)
			{
				return null;
			}
			return this.DestinationProperty.GetValue(mappedObject);
		}

		// Token: 0x04000046 RID: 70
		private readonly LinkedList<IValueResolver> _sourceValueResolvers = new LinkedList<IValueResolver>();

		// Token: 0x04000047 RID: 71
		private bool _ignored;

		// Token: 0x04000048 RID: 72
		private int _mappingOrder;

		// Token: 0x04000049 RID: 73
		private IValueResolver _customResolver;

		// Token: 0x0400004A RID: 74
		private IValueResolver _customMemberResolver;

		// Token: 0x0400004B RID: 75
		private bool _sealed;

		// Token: 0x0400004C RID: 76
		private IValueResolver[] _cachedResolvers;

		// Token: 0x0400004D RID: 77
		private Func<ResolutionContext, bool> _condition;

		// Token: 0x0400004E RID: 78
		private Func<ResolutionContext, bool> _preCondition;

		// Token: 0x0400004F RID: 79
		private MemberInfo _sourceMember;
	}
}
