using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper.Internal;

namespace AutoMapper
{
	// Token: 0x02000010 RID: 16
	public class CtorParamConfigurationExpression<TSource> : ICtorParamConfigurationExpression<TSource>
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00002FA1 File Offset: 0x000011A1
		public CtorParamConfigurationExpression(ConstructorParameterMap ctorParamMap)
		{
			this._ctorParamMap = ctorParamMap;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002FB0 File Offset: 0x000011B0
		public void MapFrom<TMember>(Expression<Func<TSource, TMember>> sourceMember)
		{
			CtorParamConfigurationExpression<TSource>.MemberInfoFinderVisitor memberInfoFinderVisitor = new CtorParamConfigurationExpression<TSource>.MemberInfoFinderVisitor();
			memberInfoFinderVisitor.Visit(sourceMember);
			this._ctorParamMap.ResolveUsing(memberInfoFinderVisitor.Members);
		}

		// Token: 0x0400001A RID: 26
		private readonly ConstructorParameterMap _ctorParamMap;

		// Token: 0x020000C9 RID: 201
		private class MemberInfoFinderVisitor : ExpressionVisitor
		{
			// Token: 0x060005BD RID: 1469 RVA: 0x000153FA File Offset: 0x000135FA
			protected override Expression VisitMember(MemberExpression node)
			{
				this._members.Add(node.Member.ToMemberGetter());
				return base.VisitMember(node);
			}

			// Token: 0x170000F2 RID: 242
			// (get) Token: 0x060005BE RID: 1470 RVA: 0x00015419 File Offset: 0x00013619
			public IEnumerable<IMemberGetter> Members
			{
				get
				{
					return this._members;
				}
			}

			// Token: 0x0400011B RID: 283
			private readonly List<IMemberGetter> _members = new List<IMemberGetter>();
		}
	}
}
