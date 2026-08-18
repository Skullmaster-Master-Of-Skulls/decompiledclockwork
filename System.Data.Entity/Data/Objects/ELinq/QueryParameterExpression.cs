using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Objects.ELinq
{
	// Token: 0x020001A7 RID: 423
	internal sealed class QueryParameterExpression : Expression
	{
		// Token: 0x06001E88 RID: 7816 RVA: 0x0006A900 File Offset: 0x00068B00
		internal QueryParameterExpression(DbParameterReferenceExpression parameterReference, Expression funcletizedExpression, IEnumerable<ParameterExpression> compiledQueryParameters)
		{
			EntityUtil.CheckArgumentNull<DbParameterReferenceExpression>(parameterReference, "parameterReference");
			EntityUtil.CheckArgumentNull<Expression>(funcletizedExpression, "funcletizedExpression");
			this._compiledQueryParameters = (compiledQueryParameters ?? Enumerable.Empty<ParameterExpression>());
			this._parameterReference = parameterReference;
			this._type = funcletizedExpression.Type;
			this._funcletizedExpression = funcletizedExpression;
			this._cachedDelegate = null;
		}

		// Token: 0x06001E89 RID: 7817 RVA: 0x0006A95C File Offset: 0x00068B5C
		internal object EvaluateParameter(object[] arguments)
		{
			if (this._cachedDelegate == null)
			{
				if (this._funcletizedExpression.NodeType == ExpressionType.Constant)
				{
					return ((ConstantExpression)this._funcletizedExpression).Value;
				}
				ConstantExpression constantExpression;
				if (this.TryEvaluatePath(this._funcletizedExpression, out constantExpression))
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

		// Token: 0x06001E8A RID: 7818 RVA: 0x0006AA2C File Offset: 0x00068C2C
		internal QueryParameterExpression EscapeParameterForLike(Func<string, string> method)
		{
			Expression funcletizedExpression = Expression.Invoke(Expression.Constant(method), new Expression[]
			{
				this._funcletizedExpression
			});
			return new QueryParameterExpression(this._parameterReference, funcletizedExpression, this._compiledQueryParameters);
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001E8B RID: 7819 RVA: 0x0006AA66 File Offset: 0x00068C66
		internal DbParameterReferenceExpression ParameterReference
		{
			get
			{
				return this._parameterReference;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001E8C RID: 7820 RVA: 0x0006AA6E File Offset: 0x00068C6E
		public override Type Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001E8D RID: 7821 RVA: 0x0003BCE8 File Offset: 0x00039EE8
		public override ExpressionType NodeType
		{
			get
			{
				return (ExpressionType)(-1);
			}
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x0006AA78 File Offset: 0x00068C78
		private bool TryEvaluatePath(Expression expression, out ConstantExpression constantExpression)
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
					if (!this.TryGetFieldOrPropertyValue(memberExpression, ((ConstantExpression)memberExpression.Expression).Value, out obj))
					{
						return false;
					}
					if (stack.Count > 0)
					{
						foreach (MemberExpression me in stack)
						{
							if (!this.TryGetFieldOrPropertyValue(me, obj, out obj))
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

		// Token: 0x06001E8F RID: 7823 RVA: 0x0006AB58 File Offset: 0x00068D58
		private bool TryGetFieldOrPropertyValue(MemberExpression me, object instance, out object memberValue)
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

		// Token: 0x04000CD4 RID: 3284
		private readonly DbParameterReferenceExpression _parameterReference;

		// Token: 0x04000CD5 RID: 3285
		private readonly Type _type;

		// Token: 0x04000CD6 RID: 3286
		private readonly Expression _funcletizedExpression;

		// Token: 0x04000CD7 RID: 3287
		private readonly IEnumerable<ParameterExpression> _compiledQueryParameters;

		// Token: 0x04000CD8 RID: 3288
		private Delegate _cachedDelegate;
	}
}
