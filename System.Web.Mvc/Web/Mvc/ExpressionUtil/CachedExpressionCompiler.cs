using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Web.Mvc.ExpressionUtil
{
	// Token: 0x020000A8 RID: 168
	internal static class CachedExpressionCompiler
	{
		// Token: 0x06000491 RID: 1169 RVA: 0x0000D230 File Offset: 0x0000B430
		public static Func<TModel, TValue> Process<TModel, TValue>(Expression<Func<TModel, TValue>> lambdaExpression)
		{
			return CachedExpressionCompiler.Compiler<TModel, TValue>.Compile(lambdaExpression);
		}

		// Token: 0x020000A9 RID: 169
		private static class Compiler<TIn, TOut>
		{
			// Token: 0x06000492 RID: 1170 RVA: 0x0000D238 File Offset: 0x0000B438
			public static Func<TIn, TOut> Compile(Expression<Func<TIn, TOut>> expr)
			{
				Func<TIn, TOut> result;
				if ((result = CachedExpressionCompiler.Compiler<TIn, TOut>.CompileFromIdentityFunc(expr)) == null && (result = CachedExpressionCompiler.Compiler<TIn, TOut>.CompileFromConstLookup(expr)) == null && (result = CachedExpressionCompiler.Compiler<TIn, TOut>.CompileFromMemberAccess(expr)) == null)
				{
					result = (CachedExpressionCompiler.Compiler<TIn, TOut>.CompileFromFingerprint(expr) ?? CachedExpressionCompiler.Compiler<TIn, TOut>.CompileSlow(expr));
				}
				return result;
			}

			// Token: 0x06000493 RID: 1171 RVA: 0x0000D278 File Offset: 0x0000B478
			private static Func<TIn, TOut> CompileFromConstLookup(Expression<Func<TIn, TOut>> expr)
			{
				ConstantExpression constantExpression = expr.Body as ConstantExpression;
				if (constantExpression != null)
				{
					TOut constantValue = (TOut)((object)constantExpression.Value);
					return (TIn _) => constantValue;
				}
				return null;
			}

			// Token: 0x06000494 RID: 1172 RVA: 0x0000D2B9 File Offset: 0x0000B4B9
			private static Func<TIn, TOut> CompileFromIdentityFunc(Expression<Func<TIn, TOut>> expr)
			{
				if (expr.Body == expr.Parameters[0])
				{
					if (CachedExpressionCompiler.Compiler<TIn, TOut>._identityFunc == null)
					{
						CachedExpressionCompiler.Compiler<TIn, TOut>._identityFunc = expr.Compile();
					}
					return CachedExpressionCompiler.Compiler<TIn, TOut>._identityFunc;
				}
				return null;
			}

			// Token: 0x06000495 RID: 1173 RVA: 0x0000D330 File Offset: 0x0000B530
			private static Func<TIn, TOut> CompileFromFingerprint(Expression<Func<TIn, TOut>> expr)
			{
				List<object> capturedConstants;
				ExpressionFingerprintChain fingerprintChain = FingerprintingExpressionVisitor.GetFingerprintChain(expr, out capturedConstants);
				if (fingerprintChain != null)
				{
					Hoisted<TIn, TOut> del = CachedExpressionCompiler.Compiler<TIn, TOut>._fingerprintedCache.GetOrAdd(fingerprintChain, delegate(ExpressionFingerprintChain _)
					{
						Expression<Hoisted<TIn, TOut>> expression = HoistingExpressionVisitor<TIn, TOut>.Hoist(expr);
						return expression.Compile();
					});
					return (TIn model) => del(model, capturedConstants);
				}
				return null;
			}

			// Token: 0x06000496 RID: 1174 RVA: 0x0000D434 File Offset: 0x0000B634
			private static Func<TIn, TOut> CompileFromMemberAccess(Expression<Func<TIn, TOut>> expr)
			{
				MemberExpression memberExpr = expr.Body as MemberExpression;
				if (memberExpr != null)
				{
					if (memberExpr.Expression == expr.Parameters[0] || memberExpr.Expression == null)
					{
						return CachedExpressionCompiler.Compiler<TIn, TOut>._simpleMemberAccessDict.GetOrAdd(memberExpr.Member, (MemberInfo _) => expr.Compile());
					}
					ConstantExpression constantExpression = memberExpr.Expression as ConstantExpression;
					if (constantExpression != null)
					{
						Func<object, TOut> del = CachedExpressionCompiler.Compiler<TIn, TOut>._constMemberAccessDict.GetOrAdd(memberExpr.Member, delegate(MemberInfo _)
						{
							ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "capturedLocal");
							UnaryExpression expression = Expression.Convert(parameterExpression, memberExpr.Member.DeclaringType);
							MemberExpression body = memberExpr.Update(expression);
							Expression<Func<object, TOut>> expression2 = Expression.Lambda<Func<object, TOut>>(body, new ParameterExpression[]
							{
								parameterExpression
							});
							return expression2.Compile();
						});
						object capturedLocal = constantExpression.Value;
						return (TIn _) => del(capturedLocal);
					}
				}
				return null;
			}

			// Token: 0x06000497 RID: 1175 RVA: 0x0000D53A File Offset: 0x0000B73A
			private static Func<TIn, TOut> CompileSlow(Expression<Func<TIn, TOut>> expr)
			{
				return expr.Compile();
			}

			// Token: 0x04000146 RID: 326
			private static Func<TIn, TOut> _identityFunc;

			// Token: 0x04000147 RID: 327
			private static readonly ConcurrentDictionary<MemberInfo, Func<TIn, TOut>> _simpleMemberAccessDict = new ConcurrentDictionary<MemberInfo, Func<TIn, TOut>>();

			// Token: 0x04000148 RID: 328
			private static readonly ConcurrentDictionary<MemberInfo, Func<object, TOut>> _constMemberAccessDict = new ConcurrentDictionary<MemberInfo, Func<object, TOut>>();

			// Token: 0x04000149 RID: 329
			private static readonly ConcurrentDictionary<ExpressionFingerprintChain, Hoisted<TIn, TOut>> _fingerprintedCache = new ConcurrentDictionary<ExpressionFingerprintChain, Hoisted<TIn, TOut>>();
		}
	}
}
