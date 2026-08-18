using System;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Linq.Expressions.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting;

namespace System.Dynamic
{
	// Token: 0x020000C4 RID: 196
	[__DynamicallyInvokable]
	public abstract class DynamicMetaObjectBinder : CallSiteBinder
	{
		// Token: 0x060005B1 RID: 1457 RVA: 0x000116C9 File Offset: 0x0000F8C9
		[__DynamicallyInvokable]
		protected DynamicMetaObjectBinder()
		{
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x000116D1 File Offset: 0x0000F8D1
		[__DynamicallyInvokable]
		public virtual Type ReturnType
		{
			[__DynamicallyInvokable]
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x000116E0 File Offset: 0x0000F8E0
		[__DynamicallyInvokable]
		public sealed override Expression Bind(object[] args, ReadOnlyCollection<ParameterExpression> parameters, LabelTarget returnLabel)
		{
			ContractUtils.RequiresNotNull(args, "args");
			ContractUtils.RequiresNotNull(parameters, "parameters");
			ContractUtils.RequiresNotNull(returnLabel, "returnLabel");
			if (args.Length == 0)
			{
				throw Error.OutOfRange("args.Length", 1);
			}
			if (parameters.Count == 0)
			{
				throw Error.OutOfRange("parameters.Count", 1);
			}
			if (args.Length != parameters.Count)
			{
				throw new ArgumentOutOfRangeException("args");
			}
			Type type;
			if (this.IsStandardBinder)
			{
				type = this.ReturnType;
				if (returnLabel.Type != typeof(void) && !TypeUtils.AreReferenceAssignable(returnLabel.Type, type))
				{
					throw Error.BinderNotCompatibleWithCallSite(type, this, returnLabel.Type);
				}
			}
			else
			{
				type = returnLabel.Type;
			}
			DynamicMetaObject dynamicMetaObject = DynamicMetaObject.Create(args[0], parameters[0]);
			DynamicMetaObject[] args2 = DynamicMetaObjectBinder.CreateArgumentMetaObjects(args, parameters);
			DynamicMetaObject dynamicMetaObject2 = this.Bind(dynamicMetaObject, args2);
			if (dynamicMetaObject2 == null)
			{
				throw Error.BindingCannotBeNull();
			}
			Expression expression = dynamicMetaObject2.Expression;
			BindingRestrictions bindingRestrictions = dynamicMetaObject2.Restrictions;
			if (type != typeof(void) && !TypeUtils.AreReferenceAssignable(type, expression.Type))
			{
				if (dynamicMetaObject.Value is IDynamicMetaObjectProvider)
				{
					throw Error.DynamicObjectResultNotAssignable(expression.Type, dynamicMetaObject.Value.GetType(), this, type);
				}
				throw Error.DynamicBinderResultNotAssignable(expression.Type, this, type);
			}
			else
			{
				if (this.IsStandardBinder && args[0] is IDynamicMetaObjectProvider && bindingRestrictions == BindingRestrictions.Empty)
				{
					throw Error.DynamicBindingNeedsRestrictions(dynamicMetaObject.Value.GetType(), this);
				}
				bindingRestrictions = DynamicMetaObjectBinder.AddRemoteObjectRestrictions(bindingRestrictions, args, parameters);
				if (expression.NodeType != ExpressionType.Goto)
				{
					expression = Expression.Return(returnLabel, expression);
				}
				if (bindingRestrictions != BindingRestrictions.Empty)
				{
					expression = Expression.IfThen(bindingRestrictions.ToExpression(), expression);
				}
				return expression;
			}
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00011894 File Offset: 0x0000FA94
		private static DynamicMetaObject[] CreateArgumentMetaObjects(object[] args, ReadOnlyCollection<ParameterExpression> parameters)
		{
			DynamicMetaObject[] array;
			if (args.Length != 1)
			{
				array = new DynamicMetaObject[args.Length - 1];
				for (int i = 1; i < args.Length; i++)
				{
					array[i - 1] = DynamicMetaObject.Create(args[i], parameters[i]);
				}
			}
			else
			{
				array = DynamicMetaObject.EmptyMetaObjects;
			}
			return array;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x000118E0 File Offset: 0x0000FAE0
		private static BindingRestrictions AddRemoteObjectRestrictions(BindingRestrictions restrictions, object[] args, ReadOnlyCollection<ParameterExpression> parameters)
		{
			for (int i = 0; i < parameters.Count; i++)
			{
				ParameterExpression parameterExpression = parameters[i];
				MarshalByRefObject marshalByRefObject = args[i] as MarshalByRefObject;
				if (marshalByRefObject != null && !DynamicMetaObjectBinder.IsComObject(marshalByRefObject))
				{
					BindingRestrictions expressionRestriction;
					if (RemotingServices.IsObjectOutOfAppDomain(marshalByRefObject))
					{
						expressionRestriction = BindingRestrictions.GetExpressionRestriction(Expression.AndAlso(Expression.NotEqual(parameterExpression, Expression.Constant(null)), Expression.Call(typeof(RemotingServices).GetMethod("IsObjectOutOfAppDomain"), parameterExpression)));
					}
					else
					{
						expressionRestriction = BindingRestrictions.GetExpressionRestriction(Expression.AndAlso(Expression.NotEqual(parameterExpression, Expression.Constant(null)), Expression.Not(Expression.Call(typeof(RemotingServices).GetMethod("IsObjectOutOfAppDomain"), parameterExpression))));
					}
					restrictions = restrictions.Merge(expressionRestriction);
				}
			}
			return restrictions;
		}

		// Token: 0x060005B6 RID: 1462
		[__DynamicallyInvokable]
		public abstract DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args);

		// Token: 0x060005B7 RID: 1463 RVA: 0x0001199E File Offset: 0x0000FB9E
		[__DynamicallyInvokable]
		public Expression GetUpdateExpression(Type type)
		{
			return Expression.Goto(CallSiteBinder.UpdateLabel, type);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000119AC File Offset: 0x0000FBAC
		[__DynamicallyInvokable]
		public DynamicMetaObject Defer(DynamicMetaObject target, params DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			if (args == null)
			{
				return this.MakeDeferred(target.Restrictions, new DynamicMetaObject[]
				{
					target
				});
			}
			return this.MakeDeferred(target.Restrictions.Merge(BindingRestrictions.Combine(args)), args.AddFirst(target));
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x000119FC File Offset: 0x0000FBFC
		[__DynamicallyInvokable]
		public DynamicMetaObject Defer(params DynamicMetaObject[] args)
		{
			return this.MakeDeferred(BindingRestrictions.Combine(args), args);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00011A0C File Offset: 0x0000FC0C
		private DynamicMetaObject MakeDeferred(BindingRestrictions rs, params DynamicMetaObject[] args)
		{
			Expression[] expressions = DynamicMetaObject.GetExpressions(args);
			Type delegateType = DelegateHelpers.MakeDeferredSiteDelegate(args, this.ReturnType);
			return new DynamicMetaObject(DynamicExpression.Make(this.ReturnType, delegateType, this, new TrueReadOnlyCollection<Expression>(expressions)), rs);
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x00011A46 File Offset: 0x0000FC46
		internal virtual bool IsStandardBinder
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00011A49 File Offset: 0x0000FC49
		private static bool IsComObject(object obj)
		{
			return obj != null && DynamicMetaObjectBinder.ComObjectType.IsAssignableFrom(obj.GetType());
		}

		// Token: 0x040005A7 RID: 1447
		private static readonly Type ComObjectType = typeof(object).Assembly.GetType("System.__ComObject");
	}
}
