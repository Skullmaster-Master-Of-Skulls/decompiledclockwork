using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Core.Objects.ELinq
{
	// Token: 0x0200055B RID: 1371
	internal sealed class QueryParameterExpression : Expression
	{
		// Token: 0x0600351A RID: 13594 RVA: 0x000FAC43 File Offset: 0x000F8E43
		internal QueryParameterExpression(DbParameterReferenceExpression parameterReference, Expression funcletizedExpression, IEnumerable<ParameterExpression> compiledQueryParameters)
		{
			this._compiledQueryParameters = (compiledQueryParameters ?? Enumerable.Empty<ParameterExpression>());
			this._parameterReference = parameterReference;
			this._type = funcletizedExpression.Type;
			this._funcletizedExpression = funcletizedExpression;
			this._cachedDelegate = null;
		}

		// Token: 0x0600351B RID: 13595 RVA: 0x000FAC84 File Offset: 0x000F8E84
		internal object EvaluateParameter(object[] arguments)
		{
			if (this._cachedDelegate == null)
			{
				if (this._funcletizedExpression.NodeType == ExpressionType.Constant)
				{
					return ((ConstantExpression)this._funcletizedExpression).Value;
				}
				ConstantExpression constantExpression;
				if (QueryParameterExpression.TryEvaluatePath(this._funcletizedExpression, out constantExpression))
				{
					return constantExpression.Value;
				}
			}
			object result;
			try
			{
				if (this._cachedDelegate == null)
				{
					Type delegateType = TypeSystem.GetDelegateType(from p in this._compiledQueryParameters
					select p.Type, this._type);
					this._cachedDelegate = Expression.Lambda(delegateType, this._funcletizedExpression, this._compiledQueryParameters).Compile();
				}
				result = this._cachedDelegate.DynamicInvoke(arguments);
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
			return result;
		}

		// Token: 0x0600351C RID: 13596 RVA: 0x000FAD50 File Offset: 0x000F8F50
		internal QueryParameterExpression EscapeParameterForLike(Func<string, string> method)
		{
			Expression funcletizedExpression = Expression.Invoke(Expression.Constant(method), new Expression[]
			{
				this._funcletizedExpression
			});
			return new QueryParameterExpression(this._parameterReference, funcletizedExpression, this._compiledQueryParameters);
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x0600351D RID: 13597 RVA: 0x000FAD8C File Offset: 0x000F8F8C
		internal DbParameterReferenceExpression ParameterReference
		{
			get
			{
				return this._parameterReference;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x0600351E RID: 13598 RVA: 0x000FAD94 File Offset: 0x000F8F94
		public override Type Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x0600351F RID: 13599 RVA: 0x000FAD9C File Offset: 0x000F8F9C
		public override ExpressionType NodeType
		{
			get
			{
				return (ExpressionType)(-1);
			}
		}

		// Token: 0x06003520 RID: 13600 RVA: 0x000FADA0 File Offset: 0x000F8FA0
		private static bool TryEvaluatePath(Expression expression, out ConstantExpression constantExpression)
		{
			MemberExpression memberExpression = expression as MemberExpression;
			constantExpression = null;
			if (memberExpression != null)
			{
				Stack<MemberExpression> stack = new Stack<MemberExpression>();
				stack.Push(memberExpression);
				while ((memberExpression = (memberExpression.Expression as MemberExpression)) != null)
				{
					stack.Push(memberExpression);
				}
				memberExpression = stack.Pop();
				ConstantExpression constantExpression2 = memberExpression.Expression as ConstantExpression;
				if (constantExpression2 != null)
				{
					object obj;
					if (!QueryParameterExpression.TryGetFieldOrPropertyValue(memberExpression, ((ConstantExpression)memberExpression.Expression).Value, out obj))
					{
						return false;
					}
					if (stack.Count > 0)
					{
						foreach (MemberExpression me in stack)
						{
							if (!QueryParameterExpression.TryGetFieldOrPropertyValue(me, obj, out obj))
							{
								return false;
							}
						}
					}
					constantExpression = Expression.Constant(obj, expression.Type);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x000FAE80 File Offset: 0x000F9080
		private static bool TryGetFieldOrPropertyValue(MemberExpression me, object instance, out object memberValue)
		{
			bool flag = false;
			memberValue = null;
			bool result;
			try
			{
				if (me.Member.MemberType == MemberTypes.Field)
				{
					memberValue = ((FieldInfo)me.Member).GetValue(instance);
					flag = true;
				}
				else if (me.Member.MemberType == MemberTypes.Property)
				{
					memberValue = ((PropertyInfo)me.Member).GetValue(instance, null);
					flag = true;
				}
				result = flag;
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
			return result;
		}

		// Token: 0x040013DB RID: 5083
		private readonly DbParameterReferenceExpression _parameterReference;

		// Token: 0x040013DC RID: 5084
		private readonly Type _type;

		// Token: 0x040013DD RID: 5085
		private readonly Expression _funcletizedExpression;

		// Token: 0x040013DE RID: 5086
		private readonly IEnumerable<ParameterExpression> _compiledQueryParameters;

		// Token: 0x040013DF RID: 5087
		private Delegate _cachedDelegate;
	}
}
