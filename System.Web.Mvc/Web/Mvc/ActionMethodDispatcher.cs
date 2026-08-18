using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Web.Mvc
{
	// Token: 0x02000170 RID: 368
	internal sealed class ActionMethodDispatcher
	{
		// Token: 0x0600099E RID: 2462 RVA: 0x0001AB9E File Offset: 0x00018D9E
		public ActionMethodDispatcher(MethodInfo methodInfo)
		{
			this._executor = ActionMethodDispatcher.GetExecutor(methodInfo);
			this.MethodInfo = methodInfo;
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x0001ABB9 File Offset: 0x00018DB9
		// (set) Token: 0x060009A0 RID: 2464 RVA: 0x0001ABC1 File Offset: 0x00018DC1
		public MethodInfo MethodInfo { get; private set; }

		// Token: 0x060009A1 RID: 2465 RVA: 0x0001ABCA File Offset: 0x00018DCA
		public object Execute(ControllerBase controller, object[] parameters)
		{
			return this._executor(controller, parameters);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0001ABDC File Offset: 0x00018DDC
		private static ActionMethodDispatcher.ActionExecutor GetExecutor(MethodInfo methodInfo)
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(ControllerBase), "controller");
			ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object[]), "parameters");
			List<Expression> list = new List<Expression>();
			ParameterInfo[] parameters = methodInfo.GetParameters();
			for (int i = 0; i < parameters.Length; i++)
			{
				ParameterInfo parameterInfo = parameters[i];
				BinaryExpression expression = Expression.ArrayIndex(parameterExpression2, Expression.Constant(i));
				UnaryExpression item = Expression.Convert(expression, parameterInfo.ParameterType);
				list.Add(item);
			}
			UnaryExpression instance = (!methodInfo.IsStatic) ? Expression.Convert(parameterExpression, methodInfo.ReflectedType) : null;
			MethodCallExpression methodCallExpression = Expression.Call(instance, methodInfo, list);
			if (methodCallExpression.Type == typeof(void))
			{
				Expression<ActionMethodDispatcher.VoidActionExecutor> expression2 = Expression.Lambda<ActionMethodDispatcher.VoidActionExecutor>(methodCallExpression, new ParameterExpression[]
				{
					parameterExpression,
					parameterExpression2
				});
				ActionMethodDispatcher.VoidActionExecutor executor = expression2.Compile();
				return ActionMethodDispatcher.WrapVoidAction(executor);
			}
			UnaryExpression body = Expression.Convert(methodCallExpression, typeof(object));
			Expression<ActionMethodDispatcher.ActionExecutor> expression3 = Expression.Lambda<ActionMethodDispatcher.ActionExecutor>(body, new ParameterExpression[]
			{
				parameterExpression,
				parameterExpression2
			});
			return expression3.Compile();
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0001AD20 File Offset: 0x00018F20
		private static ActionMethodDispatcher.ActionExecutor WrapVoidAction(ActionMethodDispatcher.VoidActionExecutor executor)
		{
			return delegate(ControllerBase controller, object[] parameters)
			{
				executor(controller, parameters);
				return null;
			};
		}

		// Token: 0x04000297 RID: 663
		private ActionMethodDispatcher.ActionExecutor _executor;

		// Token: 0x02000171 RID: 369
		// (Invoke) Token: 0x060009A5 RID: 2469
		private delegate object ActionExecutor(ControllerBase controller, object[] parameters);

		// Token: 0x02000172 RID: 370
		// (Invoke) Token: 0x060009A9 RID: 2473
		private delegate void VoidActionExecutor(ControllerBase controller, object[] parameters);
	}
}
