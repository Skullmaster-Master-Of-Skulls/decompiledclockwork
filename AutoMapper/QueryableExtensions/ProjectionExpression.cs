using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper.Internal;

namespace AutoMapper.QueryableExtensions
{
	// Token: 0x0200005C RID: 92
	public class ProjectionExpression : IProjectionExpression
	{
		// Token: 0x06000362 RID: 866 RVA: 0x000086BD File Offset: 0x000068BD
		public ProjectionExpression(IQueryable source, IExpressionBuilder builder)
		{
			this._source = source;
			this._builder = builder;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x000086D4 File Offset: 0x000068D4
		private static MethodInfo FindQueryableSelectMethod()
		{
			return ((MethodCallExpression)(() => Queryable.Select((IQueryable<TSource>)null, null)).Body).Method.GetGenericMethodDefinition();
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00008741 File Offset: 0x00006941
		public IQueryable<TResult> To<TResult>(object parameters = null)
		{
			return this.To<TResult>(parameters, new string[0]);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00008750 File Offset: 0x00006950
		public IQueryable<TResult> To<TResult>(object parameters = null, params string[] membersToExpand)
		{
			IDictionary<string, object> parameters2 = ProjectionExpression.GetParameters(parameters);
			return this.To<TResult>(parameters2, membersToExpand);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000876C File Offset: 0x0000696C
		private static IDictionary<string, object> GetParameters(object parameters)
		{
			return (parameters ?? new object()).GetType().GetDeclaredProperties().ToDictionary((PropertyInfo pi) => pi.Name, (PropertyInfo pi) => pi.GetValue(parameters, null));
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000087CF File Offset: 0x000069CF
		public IQueryable<TResult> To<TResult>(IDictionary<string, object> parameters)
		{
			return this.To<TResult>(parameters, new string[0]);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x000087E0 File Offset: 0x000069E0
		public IQueryable<TResult> To<TResult>(IDictionary<string, object> parameters, params string[] membersToExpand)
		{
			IEnumerable<IEnumerable<MemberInfo>> memberPaths = this.GetMemberPaths(typeof(TResult), membersToExpand);
			return this.To<TResult>(parameters, memberPaths);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00008807 File Offset: 0x00006A07
		public IQueryable<TResult> To<TResult>(object parameters = null, params Expression<Func<TResult, object>>[] membersToExpand)
		{
			return this.To<TResult>(ProjectionExpression.GetParameters(parameters), this.GetMemberPaths<TResult>(membersToExpand));
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000881C File Offset: 0x00006A1C
		private IEnumerable<IEnumerable<MemberInfo>> GetMemberPaths(Type type, string[] membersToExpand)
		{
			return from m in membersToExpand
			select ReflectionHelper.GetMemberPath(type, m);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00008848 File Offset: 0x00006A48
		private IEnumerable<IEnumerable<MemberInfo>> GetMemberPaths<TResult>(Expression<Func<TResult, object>>[] membersToExpand)
		{
			return membersToExpand.Select(delegate(Expression<Func<TResult, object>> expr)
			{
				ProjectionExpression.MemberVisitor memberVisitor = new ProjectionExpression.MemberVisitor();
				memberVisitor.Visit(expr);
				return memberVisitor.MemberPath;
			});
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00008870 File Offset: 0x00006A70
		public IQueryable<TResult> To<TResult>(IDictionary<string, object> parameters, params Expression<Func<TResult, object>>[] membersToExpand)
		{
			IEnumerable<IEnumerable<MemberInfo>> memberPaths = this.GetMemberPaths<TResult>(membersToExpand);
			return this.To<TResult>(parameters, memberPaths);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00008890 File Offset: 0x00006A90
		private IQueryable<TResult> To<TResult>(IDictionary<string, object> parameters, IEnumerable<IEnumerable<MemberInfo>> memberPathsToExpand)
		{
			MemberInfo[] membersToExpand = memberPathsToExpand.SelectMany((IEnumerable<MemberInfo> m) => m).Distinct<MemberInfo>().ToArray<MemberInfo>();
			Expression expression = this._builder.CreateMapExpression(this._source.ElementType, typeof(TResult), parameters, membersToExpand);
			return this._source.Provider.CreateQuery<TResult>(Expression.Call(null, ProjectionExpression.QueryableSelectMethod.MakeGenericMethod(new Type[]
			{
				this._source.ElementType,
				typeof(TResult)
			}), new Expression[]
			{
				this._source.Expression,
				Expression.Quote(expression)
			}));
		}

		// Token: 0x040000AC RID: 172
		private static readonly MethodInfo QueryableSelectMethod = ProjectionExpression.FindQueryableSelectMethod();

		// Token: 0x040000AD RID: 173
		private readonly IQueryable _source;

		// Token: 0x040000AE RID: 174
		private readonly IExpressionBuilder _builder;

		// Token: 0x02000128 RID: 296
		private class MemberVisitor : ExpressionVisitor
		{
			// Token: 0x06000710 RID: 1808 RVA: 0x000171A4 File Offset: 0x000153A4
			protected override Expression VisitLambda<T>(Expression<T> node)
			{
				MemberExpression memberExpression = node.Body as MemberExpression;
				if (memberExpression != null)
				{
					if (this.MemberPath != null)
					{
						throw new InvalidOperationException("There are more than one lambda member expressions.");
					}
					this.MemberPath = this.GetMemberPath(memberExpression);
				}
				return base.VisitLambda<T>(node);
			}

			// Token: 0x06000711 RID: 1809 RVA: 0x000171E7 File Offset: 0x000153E7
			private IEnumerable<MemberInfo> GetMemberPath(MemberExpression memberExpression)
			{
				for (MemberExpression expression = memberExpression; expression != null; expression = (expression.Expression as MemberExpression))
				{
					yield return expression.Member;
				}
				yield break;
			}

			// Token: 0x17000104 RID: 260
			// (get) Token: 0x06000712 RID: 1810 RVA: 0x000171F7 File Offset: 0x000153F7
			// (set) Token: 0x06000713 RID: 1811 RVA: 0x000171FF File Offset: 0x000153FF
			public IEnumerable<MemberInfo> MemberPath { get; private set; }
		}
	}
}
