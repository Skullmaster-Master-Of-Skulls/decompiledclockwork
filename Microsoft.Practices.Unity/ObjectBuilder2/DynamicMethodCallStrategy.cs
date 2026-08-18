using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Practices.Unity.Properties;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x0200005D RID: 93
	public class DynamicMethodCallStrategy : BuilderStrategy
	{
		// Token: 0x06000190 RID: 400 RVA: 0x000059EC File Offset: 0x00003BEC
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public override void PreBuildUp(IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			DynamicBuildPlanGenerationContext dynamicBuildPlanGenerationContext = (DynamicBuildPlanGenerationContext)context.Existing;
			IPolicyList resolverPolicyDestination;
			IMethodSelectorPolicy methodSelectorPolicy = context.Policies.Get(context.BuildKey, out resolverPolicyDestination);
			bool flag = false;
			foreach (SelectedMethod selectedMethod in methodSelectorPolicy.SelectMethods(context, resolverPolicyDestination))
			{
				flag = true;
				string methodSignature = DynamicMethodCallStrategy.GetMethodSignature(selectedMethod.Method);
				DynamicMethodCallStrategy.GuardMethodIsNotOpenGeneric(selectedMethod.Method);
				DynamicMethodCallStrategy.GuardMethodHasNoOutParams(selectedMethod.Method);
				DynamicMethodCallStrategy.GuardMethodHasNoRefParams(selectedMethod.Method);
				dynamicBuildPlanGenerationContext.AddToBuildPlan(Expression.Block(Expression.Call(null, DynamicMethodCallStrategy.SetCurrentOperationToInvokingMethodInfo, Expression.Constant(methodSignature), dynamicBuildPlanGenerationContext.ContextParameter), Expression.Call(Expression.Convert(dynamicBuildPlanGenerationContext.GetExistingObjectExpression(), dynamicBuildPlanGenerationContext.TypeToBuild), selectedMethod.Method, this.BuildMethodParameterExpressions(dynamicBuildPlanGenerationContext, selectedMethod, methodSignature))));
			}
			if (flag)
			{
				dynamicBuildPlanGenerationContext.AddToBuildPlan(dynamicBuildPlanGenerationContext.GetClearCurrentOperationExpression());
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005D34 File Offset: 0x00003F34
		private IEnumerable<Expression> BuildMethodParameterExpressions(DynamicBuildPlanGenerationContext context, SelectedMethod method, string methodSignature)
		{
			int i = 0;
			ParameterInfo[] methodParameters = method.Method.GetParameters();
			foreach (IDependencyResolverPolicy parameterResolver in method.GetParameterResolvers())
			{
				yield return context.CreateParameterExpression(parameterResolver, methodParameters[i].ParameterType, Expression.Call(null, DynamicMethodCallStrategy.SetCurrentOperationToResolvingParameterMethod, Expression.Constant(methodParameters[i].Name, typeof(string)), Expression.Constant(methodSignature), context.ContextParameter));
				i++;
			}
			yield break;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005D66 File Offset: 0x00003F66
		private static void GuardMethodIsNotOpenGeneric(MethodInfo method)
		{
			if (method.IsGenericMethodDefinition)
			{
				DynamicMethodCallStrategy.ThrowIllegalInjectionMethod(Resources.CannotInjectOpenGenericMethod, method);
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005D83 File Offset: 0x00003F83
		private static void GuardMethodHasNoOutParams(MethodInfo method)
		{
			if (method.GetParameters().Any((ParameterInfo param) => param.IsOut))
			{
				DynamicMethodCallStrategy.ThrowIllegalInjectionMethod(Resources.CannotInjectMethodWithOutParam, method);
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00005DC7 File Offset: 0x00003FC7
		private static void GuardMethodHasNoRefParams(MethodInfo method)
		{
			if (method.GetParameters().Any((ParameterInfo param) => param.ParameterType.IsByRef))
			{
				DynamicMethodCallStrategy.ThrowIllegalInjectionMethod(Resources.CannotInjectMethodWithOutParam, method);
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00005E00 File Offset: 0x00004000
		private static void ThrowIllegalInjectionMethod(string format, MethodInfo method)
		{
			throw new IllegalInjectionMethodException(string.Format(CultureInfo.CurrentCulture, format, new object[]
			{
				method.DeclaringType.GetTypeInfo().Name,
				method.Name
			}));
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00005E41 File Offset: 0x00004041
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class.")]
		public static void SetCurrentOperationToResolvingParameter(string parameterName, string methodSignature, IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			context.CurrentOperation = new MethodArgumentResolveOperation(context.BuildKey.Type, methodSignature, parameterName);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00005E66 File Offset: 0x00004066
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class.")]
		public static void SetCurrentOperationToInvokingMethod(string methodSignature, IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			context.CurrentOperation = new InvokingMethodOperation(context.BuildKey.Type, methodSignature);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00005E8C File Offset: 0x0000408C
		private static string GetMethodSignature(MethodBase method)
		{
			string name = method.Name;
			ParameterInfo[] parameters = method.GetParameters();
			string[] array = new string[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				array[i] = parameters[i].ParameterType.FullName + " " + parameters[i].Name;
			}
			return string.Format(CultureInfo.CurrentCulture, "{0}({1})", new object[]
			{
				name,
				string.Join(", ", array)
			});
		}

		// Token: 0x0400005D RID: 93
		private static readonly MethodInfo SetCurrentOperationToResolvingParameterMethod = StaticReflection.GetMethodInfo(() => DynamicMethodCallStrategy.SetCurrentOperationToResolvingParameter(null, null, null));

		// Token: 0x0400005E RID: 94
		private static readonly MethodInfo SetCurrentOperationToInvokingMethodInfo = StaticReflection.GetMethodInfo(() => DynamicMethodCallStrategy.SetCurrentOperationToInvokingMethod(null, null));
	}
}
