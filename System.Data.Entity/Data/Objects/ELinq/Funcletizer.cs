using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Objects.ELinq
{
	// Token: 0x020001A6 RID: 422
	internal sealed class Funcletizer
	{
		// Token: 0x06001E76 RID: 7798 RVA: 0x0006A528 File Offset: 0x00068728
		private Funcletizer(Funcletizer.Mode mode, ObjectContext rootContext, ParameterExpression rootContextParameter, ReadOnlyCollection<ParameterExpression> compiledQueryParameters)
		{
			this._mode = mode;
			this._rootContext = rootContext;
			this._rootContextParameter = rootContextParameter;
			this._compiledQueryParameters = compiledQueryParameters;
			if (this._rootContextParameter != null && this._rootContext != null)
			{
				this._rootContextExpression = Expression.Constant(this._rootContext);
			}
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x0006A584 File Offset: 0x00068784
		internal static Funcletizer CreateCompiledQueryEvaluationFuncletizer(ObjectContext rootContext, ParameterExpression rootContextParameter, ReadOnlyCollection<ParameterExpression> compiledQueryParameters)
		{
			EntityUtil.CheckArgumentNull<ObjectContext>(rootContext, "rootContext");
			EntityUtil.CheckArgumentNull<ParameterExpression>(rootContextParameter, "rootContextParameter");
			EntityUtil.CheckArgumentNull<ReadOnlyCollection<ParameterExpression>>(compiledQueryParameters, "compiledQueryParameters");
			return new Funcletizer(Funcletizer.Mode.CompiledQueryEvaluation, rootContext, rootContextParameter, compiledQueryParameters);
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x0006A5B3 File Offset: 0x000687B3
		internal static Funcletizer CreateCompiledQueryLockdownFuncletizer()
		{
			return new Funcletizer(Funcletizer.Mode.CompiledQueryLockdown, null, null, null);
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x0006A5BE File Offset: 0x000687BE
		internal static Funcletizer CreateQueryFuncletizer(ObjectContext rootContext)
		{
			EntityUtil.CheckArgumentNull<ObjectContext>(rootContext, "rootContext");
			return new Funcletizer(Funcletizer.Mode.ConventionalQuery, rootContext, null, null);
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001E7A RID: 7802 RVA: 0x0006A5D5 File Offset: 0x000687D5
		internal ObjectContext RootContext
		{
			get
			{
				return this._rootContext;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001E7B RID: 7803 RVA: 0x0006A5DD File Offset: 0x000687DD
		internal ParameterExpression RootContextParameter
		{
			get
			{
				return this._rootContextParameter;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001E7C RID: 7804 RVA: 0x0006A5E5 File Offset: 0x000687E5
		internal ConstantExpression RootContextExpression
		{
			get
			{
				return this._rootContextExpression;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001E7D RID: 7805 RVA: 0x0006A5ED File Offset: 0x000687ED
		internal bool IsCompiledQuery
		{
			get
			{
				return this._mode == Funcletizer.Mode.CompiledQueryEvaluation || this._mode == Funcletizer.Mode.CompiledQueryLockdown;
			}
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x0006A604 File Offset: 0x00068804
		internal Expression Funcletize(Expression expression, out Func<bool> recompileRequired)
		{
			EntityUtil.CheckArgumentNull<Expression>(expression, "expression");
			expression = this.ReplaceRootContextParameter(expression);
			Func<Expression, bool> isClientConstant;
			Func<Expression, bool> isClientVariable;
			if (this._mode == Funcletizer.Mode.CompiledQueryEvaluation)
			{
				isClientConstant = Funcletizer.Nominate(expression, new Func<Expression, bool>(this.IsClosureExpression));
				isClientVariable = Funcletizer.Nominate(expression, new Func<Expression, bool>(this.IsCompiledQueryParameterVariable));
			}
			else if (this._mode == Funcletizer.Mode.CompiledQueryLockdown)
			{
				isClientConstant = Funcletizer.Nominate(expression, new Func<Expression, bool>(this.IsClosureExpression));
				isClientVariable = ((Expression exp) => false);
			}
			else
			{
				isClientConstant = Funcletizer.Nominate(expression, new Func<Expression, bool>(this.IsImmutable));
				isClientVariable = Funcletizer.Nominate(expression, new Func<Expression, bool>(this.IsClosureExpression));
			}
			Funcletizer.FuncletizingVisitor funcletizingVisitor = new Funcletizer.FuncletizingVisitor(this, isClientConstant, isClientVariable);
			Expression result = funcletizingVisitor.Visit(expression);
			recompileRequired = funcletizingVisitor.GetRecompileRequiredFunction();
			return result;
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x0006A6D4 File Offset: 0x000688D4
		private Expression ReplaceRootContextParameter(Expression expression)
		{
			if (this._rootContextExpression != null)
			{
				return EntityExpressionVisitor.Visit(expression, delegate(Expression exp, Func<Expression, Expression> baseVisit)
				{
					if (exp != this._rootContextParameter)
					{
						return baseVisit(exp);
					}
					return this._rootContextExpression;
				});
			}
			return expression;
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x0006A6F4 File Offset: 0x000688F4
		private static Func<Expression, bool> Nominate(Expression expression, Func<Expression, bool> localCriterion)
		{
			EntityUtil.CheckArgumentNull<Func<Expression, bool>>(localCriterion, "localCriterion");
			HashSet<Expression> candidates = new HashSet<Expression>();
			bool cannotBeNominated = false;
			Func<Expression, Func<Expression, Expression>, Expression> visit = delegate(Expression exp, Func<Expression, Expression> baseVisit)
			{
				if (exp != null)
				{
					bool cannotBeNominated = cannotBeNominated;
					cannotBeNominated = false;
					baseVisit(exp);
					if (!cannotBeNominated)
					{
						if (localCriterion(exp))
						{
							candidates.Add(exp);
						}
						else
						{
							cannotBeNominated = true;
						}
					}
					cannotBeNominated = (cannotBeNominated || cannotBeNominated);
				}
				return exp;
			};
			EntityExpressionVisitor.Visit(expression, visit);
			return new Func<Expression, bool>(candidates.Contains);
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x0006A758 File Offset: 0x00068958
		private bool IsImmutable(Expression expression)
		{
			if (expression == null)
			{
				return false;
			}
			ExpressionType nodeType = expression.NodeType;
			if (nodeType <= ExpressionType.Convert)
			{
				if (nodeType == ExpressionType.Constant)
				{
					return true;
				}
				if (nodeType == ExpressionType.Convert)
				{
					return true;
				}
			}
			else
			{
				if (nodeType == ExpressionType.New)
				{
					PrimitiveType primitiveType;
					return ClrProviderManifest.Instance.TryGetPrimitiveType(TypeSystem.GetNonNullableType(expression.Type), out primitiveType);
				}
				if (nodeType == ExpressionType.NewArrayInit)
				{
					return typeof(byte[]) == expression.Type;
				}
			}
			return false;
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x0006A7C8 File Offset: 0x000689C8
		private bool IsClosureExpression(Expression expression)
		{
			if (expression == null)
			{
				return false;
			}
			if (this.IsImmutable(expression))
			{
				return true;
			}
			if (ExpressionType.MemberAccess == expression.NodeType)
			{
				MemberExpression memberExpression = (MemberExpression)expression;
				return memberExpression.Member.MemberType != MemberTypes.Property || ExpressionConverter.CanFuncletizePropertyInfo((PropertyInfo)memberExpression.Member);
			}
			return false;
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x0006A81C File Offset: 0x00068A1C
		private bool IsCompiledQueryParameterVariable(Expression expression)
		{
			if (expression == null)
			{
				return false;
			}
			if (this.IsClosureExpression(expression))
			{
				return true;
			}
			if (ExpressionType.Parameter == expression.NodeType)
			{
				ParameterExpression value = (ParameterExpression)expression;
				return this._compiledQueryParameters.Contains(value);
			}
			return false;
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x0006A858 File Offset: 0x00068A58
		private bool TryGetTypeUsageForTerminal(Type type, out TypeUsage typeUsage)
		{
			EntityUtil.CheckArgumentNull<Type>(type, "type");
			if (this._rootContext.Perspective.TryGetTypeByName(TypeSystem.GetNonNullableType(type).FullName, false, out typeUsage) && TypeSemantics.IsScalarType(typeUsage))
			{
				return true;
			}
			typeUsage = null;
			return false;
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x0006A894 File Offset: 0x00068A94
		internal string GenerateParameterName()
		{
			IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
			string format = "{0}{1}";
			object[] array = new object[2];
			array[0] = Funcletizer.s_parameterPrefix;
			int num = 1;
			long parameterNumber = this._parameterNumber;
			this._parameterNumber = parameterNumber + 1L;
			array[num] = parameterNumber;
			return string.Format(invariantCulture, format, array);
		}

		// Token: 0x04000CCC RID: 3276
		private readonly ParameterExpression _rootContextParameter;

		// Token: 0x04000CCD RID: 3277
		private readonly ObjectContext _rootContext;

		// Token: 0x04000CCE RID: 3278
		private readonly ConstantExpression _rootContextExpression;

		// Token: 0x04000CCF RID: 3279
		private readonly ReadOnlyCollection<ParameterExpression> _compiledQueryParameters;

		// Token: 0x04000CD0 RID: 3280
		private readonly Funcletizer.Mode _mode;

		// Token: 0x04000CD1 RID: 3281
		private readonly HashSet<Expression> _linqExpressionStack = new HashSet<Expression>();

		// Token: 0x04000CD2 RID: 3282
		private static readonly string s_parameterPrefix = "p__linq__";

		// Token: 0x04000CD3 RID: 3283
		private long _parameterNumber;

		// Token: 0x0200050B RID: 1291
		private enum Mode
		{
			// Token: 0x04001B02 RID: 6914
			CompiledQueryLockdown,
			// Token: 0x04001B03 RID: 6915
			CompiledQueryEvaluation,
			// Token: 0x04001B04 RID: 6916
			ConventionalQuery
		}

		// Token: 0x0200050C RID: 1292
		private sealed class FuncletizingVisitor : EntityExpressionVisitor
		{
			// Token: 0x06003DB9 RID: 15801 RVA: 0x000E6DDC File Offset: 0x000E4FDC
			internal FuncletizingVisitor(Funcletizer funcletizer, Func<Expression, bool> isClientConstant, Func<Expression, bool> isClientVariable)
			{
				EntityUtil.CheckArgumentNull<Funcletizer>(funcletizer, "funcletizer");
				EntityUtil.CheckArgumentNull<Func<Expression, bool>>(isClientConstant, "isClientConstant");
				EntityUtil.CheckArgumentNull<Func<Expression, bool>>(isClientVariable, "isClientVariable");
				this._funcletizer = funcletizer;
				this._isClientConstant = isClientConstant;
				this._isClientVariable = isClientVariable;
			}

			// Token: 0x06003DBA RID: 15802 RVA: 0x000E6E34 File Offset: 0x000E5034
			internal Func<bool> GetRecompileRequiredFunction()
			{
				ReadOnlyCollection<Func<bool>> recompileRequiredDelegates = this._recompileRequiredDelegates.AsReadOnly();
				return () => recompileRequiredDelegates.Any((Func<bool> d) => d());
			}

			// Token: 0x06003DBB RID: 15803 RVA: 0x000E6E64 File Offset: 0x000E5064
			internal override Expression Visit(Expression exp)
			{
				if (exp != null)
				{
					if (!this._funcletizer._linqExpressionStack.Add(exp))
					{
						throw EntityUtil.InvalidOperation(Strings.ELinq_CycleDetected);
					}
					try
					{
						if (this._isClientConstant(exp))
						{
							return this.InlineValue(exp, false);
						}
						if (!this._isClientVariable(exp))
						{
							return base.Visit(exp);
						}
						TypeUsage type;
						if (this._funcletizer.TryGetTypeUsageForTerminal(exp.Type, out type))
						{
							DbParameterReferenceExpression parameterReference = type.Parameter(this._funcletizer.GenerateParameterName());
							return new QueryParameterExpression(parameterReference, exp, this._funcletizer._compiledQueryParameters);
						}
						if (this._funcletizer.IsCompiledQuery)
						{
							throw Funcletizer.FuncletizingVisitor.InvalidCompiledQueryParameterException(exp);
						}
						return this.InlineValue(exp, true);
					}
					finally
					{
						this._funcletizer._linqExpressionStack.Remove(exp);
					}
				}
				return base.Visit(exp);
			}

			// Token: 0x06003DBC RID: 15804 RVA: 0x000E6F54 File Offset: 0x000E5154
			private static NotSupportedException InvalidCompiledQueryParameterException(Expression expression)
			{
				ParameterExpression parameterExpression;
				if (expression.NodeType == ExpressionType.Parameter)
				{
					parameterExpression = (ParameterExpression)expression;
				}
				else
				{
					HashSet<ParameterExpression> parameters = new HashSet<ParameterExpression>();
					EntityExpressionVisitor.Visit(expression, delegate(Expression exp, Func<Expression, Expression> baseVisit)
					{
						if (exp != null && exp.NodeType == ExpressionType.Parameter)
						{
							parameters.Add((ParameterExpression)exp);
						}
						return baseVisit(exp);
					});
					if (parameters.Count != 1)
					{
						return EntityUtil.NotSupported(Strings.CompiledELinq_UnsupportedParameterTypes(expression.Type.FullName));
					}
					parameterExpression = parameters.Single<ParameterExpression>();
				}
				if (parameterExpression.Type.Equals(expression.Type))
				{
					return EntityUtil.NotSupported(Strings.CompiledELinq_UnsupportedNamedParameterType(parameterExpression.Name, parameterExpression.Type.FullName));
				}
				return EntityUtil.NotSupported(Strings.CompiledELinq_UnsupportedNamedParameterUseAsType(parameterExpression.Name, expression.Type.FullName));
			}

			// Token: 0x06003DBD RID: 15805 RVA: 0x000E7014 File Offset: 0x000E5214
			private Func<object> CompileExpression(Expression expression)
			{
				return Expression.Lambda<Func<object>>(TypeSystem.EnsureType(expression, typeof(object)), new ParameterExpression[0]).Compile();
			}

			// Token: 0x06003DBE RID: 15806 RVA: 0x000E7044 File Offset: 0x000E5244
			private Expression InlineValue(Expression expression, bool recompileOnChange)
			{
				Func<object> func = null;
				object obj = null;
				if (expression.NodeType == ExpressionType.Constant)
				{
					obj = ((ConstantExpression)expression).Value;
				}
				else
				{
					bool flag = false;
					if (expression.NodeType == ExpressionType.Convert)
					{
						UnaryExpression unaryExpression = (UnaryExpression)expression;
						if (!recompileOnChange && unaryExpression.Operand.NodeType == ExpressionType.Constant && typeof(IQueryable).IsAssignableFrom(unaryExpression.Operand.Type))
						{
							obj = ((ConstantExpression)unaryExpression.Operand).Value;
							flag = true;
						}
					}
					if (!flag)
					{
						func = this.CompileExpression(expression);
						obj = func();
					}
				}
				ObjectQuery objectQuery = obj as ObjectQuery;
				Expression result;
				if (objectQuery != null)
				{
					result = this.InlineObjectQuery(objectQuery, expression.Type);
				}
				else
				{
					LambdaExpression lambdaExpression = obj as LambdaExpression;
					if (lambdaExpression != null)
					{
						result = this.InlineExpression(Expression.Quote(lambdaExpression));
					}
					else
					{
						result = ((expression.NodeType == ExpressionType.Constant) ? expression : Expression.Constant(obj, expression.Type));
					}
				}
				if (recompileOnChange)
				{
					this.AddRecompileRequiredDelegates(func, obj);
				}
				return result;
			}

			// Token: 0x06003DBF RID: 15807 RVA: 0x000E7138 File Offset: 0x000E5338
			private void AddRecompileRequiredDelegates(Func<object> getValue, object value)
			{
				Funcletizer.FuncletizingVisitor.<>c__DisplayClass10_0 CS$<>8__locals1 = new Funcletizer.FuncletizingVisitor.<>c__DisplayClass10_0();
				CS$<>8__locals1.getValue = getValue;
				CS$<>8__locals1.value = value;
				CS$<>8__locals1.originalQuery = (CS$<>8__locals1.value as ObjectQuery);
				if (CS$<>8__locals1.originalQuery == null)
				{
					if (CS$<>8__locals1.getValue != null)
					{
						this._recompileRequiredDelegates.Add(() => CS$<>8__locals1.value != CS$<>8__locals1.getValue());
					}
					return;
				}
				MergeOption? originalMergeOption = CS$<>8__locals1.originalQuery.QueryState.UserSpecifiedMergeOption;
				if (CS$<>8__locals1.getValue == null)
				{
					this._recompileRequiredDelegates.Add(delegate
					{
						MergeOption? userSpecifiedMergeOption = CS$<>8__locals1.originalQuery.QueryState.UserSpecifiedMergeOption;
						MergeOption? originalMergeOption = originalMergeOption;
						return !(userSpecifiedMergeOption.GetValueOrDefault() == originalMergeOption.GetValueOrDefault() & userSpecifiedMergeOption != null == (originalMergeOption != null));
					});
					return;
				}
				this._recompileRequiredDelegates.Add(delegate
				{
					ObjectQuery objectQuery = CS$<>8__locals1.getValue() as ObjectQuery;
					if (CS$<>8__locals1.originalQuery == objectQuery)
					{
						MergeOption? userSpecifiedMergeOption = objectQuery.QueryState.UserSpecifiedMergeOption;
						MergeOption? originalMergeOption = originalMergeOption;
						return !(userSpecifiedMergeOption.GetValueOrDefault() == originalMergeOption.GetValueOrDefault() & userSpecifiedMergeOption != null == (originalMergeOption != null));
					}
					return true;
				});
			}

			// Token: 0x06003DC0 RID: 15808 RVA: 0x000E71F8 File Offset: 0x000E53F8
			private Expression InlineObjectQuery(ObjectQuery inlineQuery, Type expressionType)
			{
				EntityUtil.CheckArgumentNull<ObjectQuery>(inlineQuery, "inlineQuery");
				Expression expression;
				if (this._funcletizer._mode == Funcletizer.Mode.CompiledQueryLockdown)
				{
					expression = Expression.Constant(inlineQuery, expressionType);
				}
				else
				{
					if (this._funcletizer._rootContext != inlineQuery.QueryState.ObjectContext)
					{
						throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedDifferentContexts);
					}
					expression = inlineQuery.GetExpression();
					if (!(inlineQuery.QueryState is EntitySqlQueryState))
					{
						expression = this.InlineExpression(expression);
					}
					expression = TypeSystem.EnsureType(expression, expressionType);
				}
				return expression;
			}

			// Token: 0x06003DC1 RID: 15809 RVA: 0x000E7270 File Offset: 0x000E5470
			private Expression InlineExpression(Expression exp)
			{
				Func<bool> item;
				exp = this._funcletizer.Funcletize(exp, out item);
				if (!this._funcletizer.IsCompiledQuery)
				{
					this._recompileRequiredDelegates.Add(item);
				}
				return exp;
			}

			// Token: 0x04001B05 RID: 6917
			private readonly Funcletizer _funcletizer;

			// Token: 0x04001B06 RID: 6918
			private readonly Func<Expression, bool> _isClientConstant;

			// Token: 0x04001B07 RID: 6919
			private readonly Func<Expression, bool> _isClientVariable;

			// Token: 0x04001B08 RID: 6920
			private readonly List<Func<bool>> _recompileRequiredDelegates = new List<Func<bool>>();
		}
	}
}
