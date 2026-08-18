using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Properties;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000055 RID: 85
	public class DynamicMethodConstructorStrategy : BuilderStrategy
	{
		// Token: 0x0600015B RID: 347 RVA: 0x0000487C File Offset: 0x00002A7C
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation is done by Guard class")]
		public override void PreBuildUp(IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			DynamicBuildPlanGenerationContext dynamicBuildPlanGenerationContext = (DynamicBuildPlanGenerationContext)context.Existing;
			DynamicMethodConstructorStrategy.GuardTypeIsNonPrimitive(context);
			dynamicBuildPlanGenerationContext.AddToBuildPlan(Expression.IfThen(Expression.Equal(dynamicBuildPlanGenerationContext.GetExistingObjectExpression(), Expression.Constant(null)), this.CreateInstanceBuildupExpression(dynamicBuildPlanGenerationContext, context)));
			dynamicBuildPlanGenerationContext.AddToBuildPlan(Expression.Call(null, DynamicMethodConstructorStrategy.SetPerBuildSingletonMethod, new Expression[]
			{
				dynamicBuildPlanGenerationContext.ContextParameter
			}));
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000048EC File Offset: 0x00002AEC
		internal Expression CreateInstanceBuildupExpression(DynamicBuildPlanGenerationContext buildContext, IBuilderContext context)
		{
			TypeInfo typeInfo = context.BuildKey.Type.GetTypeInfo();
			if (typeInfo.IsInterface)
			{
				return DynamicMethodConstructorStrategy.CreateThrowWithContext(buildContext, DynamicMethodConstructorStrategy.ThrowForAttemptingToConstructInterfaceMethod);
			}
			if (typeInfo.IsAbstract)
			{
				return DynamicMethodConstructorStrategy.CreateThrowWithContext(buildContext, DynamicMethodConstructorStrategy.ThrowForAttemptingToConstructAbstractClassMethod);
			}
			if (typeInfo.IsSubclassOf(typeof(Delegate)))
			{
				return DynamicMethodConstructorStrategy.CreateThrowWithContext(buildContext, DynamicMethodConstructorStrategy.ThrowForAttemptingToConstructDelegateMethod);
			}
			IPolicyList resolverPolicyDestination;
			IConstructorSelectorPolicy constructorSelectorPolicy = context.Policies.Get(context.BuildKey, out resolverPolicyDestination);
			SelectedConstructor selectedConstructor = constructorSelectorPolicy.SelectConstructor(context, resolverPolicyDestination);
			if (selectedConstructor == null)
			{
				return DynamicMethodConstructorStrategy.CreateThrowWithContext(buildContext, DynamicMethodConstructorStrategy.ThrowForNullExistingObjectMethod);
			}
			string signature = DynamicMethodConstructorStrategy.CreateSignatureString(selectedConstructor.Constructor);
			if (DynamicMethodConstructorStrategy.IsInvalidConstructor(selectedConstructor))
			{
				return DynamicMethodConstructorStrategy.CreateThrowForNullExistingObjectWithInvalidConstructor(buildContext, signature);
			}
			return Expression.Block(this.CreateNewBuildupSequence(buildContext, selectedConstructor, signature));
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000049B6 File Offset: 0x00002BB6
		private static bool IsInvalidConstructor(SelectedConstructor selectedConstructor)
		{
			return selectedConstructor.Constructor.GetParameters().Any((ParameterInfo pi) => pi.ParameterType.IsByRef);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000049E8 File Offset: 0x00002BE8
		private static Expression CreateThrowWithContext(DynamicBuildPlanGenerationContext buildContext, MethodInfo throwMethod)
		{
			return Expression.Call(null, throwMethod, new Expression[]
			{
				buildContext.ContextParameter
			});
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00004A0D File Offset: 0x00002C0D
		private static Expression CreateThrowForNullExistingObjectWithInvalidConstructor(DynamicBuildPlanGenerationContext buildContext, string signature)
		{
			return Expression.Call(null, DynamicMethodConstructorStrategy.ThrowForNullExistingObjectWithInvalidConstructorMethod, buildContext.ContextParameter, Expression.Constant(signature, typeof(string)));
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00004BEC File Offset: 0x00002DEC
		private IEnumerable<Expression> CreateNewBuildupSequence(DynamicBuildPlanGenerationContext buildContext, SelectedConstructor selectedConstructor, string signature)
		{
			IEnumerable<Expression> parameterExpressions = this.BuildConstructionParameterExpressions(buildContext, selectedConstructor, signature);
			Expression.Variable(selectedConstructor.Constructor.DeclaringType, "newItem");
			yield return Expression.Call(null, DynamicMethodConstructorStrategy.SetCurrentOperationToInvokingConstructorMethod, Expression.Constant(signature), buildContext.ContextParameter);
			yield return Expression.Assign(buildContext.GetExistingObjectExpression(), Expression.Convert(Expression.New(selectedConstructor.Constructor, parameterExpressions), typeof(object)));
			yield return buildContext.GetClearCurrentOperationExpression();
			yield break;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00004E58 File Offset: 0x00003058
		private IEnumerable<Expression> BuildConstructionParameterExpressions(DynamicBuildPlanGenerationContext buildContext, SelectedConstructor selectedConstructor, string constructorSignature)
		{
			int i = 0;
			ParameterInfo[] constructionParameters = selectedConstructor.Constructor.GetParameters();
			foreach (IDependencyResolverPolicy parameterResolver in selectedConstructor.GetParameterResolvers())
			{
				yield return buildContext.CreateParameterExpression(parameterResolver, constructionParameters[i].ParameterType, Expression.Call(null, DynamicMethodConstructorStrategy.SetCurrentOperationToResolvingParameterMethod, Expression.Constant(constructionParameters[i].Name, typeof(string)), Expression.Constant(constructorSignature), buildContext.ContextParameter));
				i++;
			}
			yield break;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00004E8C File Offset: 0x0000308C
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class.")]
		public static void SetPerBuildSingleton(IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			ILifetimePolicy lifetimePolicy = context.Policies.Get(context.BuildKey);
			if (lifetimePolicy is PerResolveLifetimeManager)
			{
				PerResolveLifetimeManager policy = new PerResolveLifetimeManager(context.Existing);
				context.Policies.Set(policy, context.BuildKey);
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00004EDC File Offset: 0x000030DC
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Strategy should only ever expect constructor method")]
		public static string CreateSignatureString(ConstructorInfo constructor)
		{
			Guard.ArgumentNotNull(constructor, "constructor");
			string fullName = constructor.DeclaringType.FullName;
			ParameterInfo[] parameters = constructor.GetParameters();
			string[] array = new string[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				array[i] = string.Format(CultureInfo.CurrentCulture, "{0} {1}", new object[]
				{
					parameters[i].ParameterType.FullName,
					parameters[i].Name
				});
			}
			return string.Format(CultureInfo.CurrentCulture, "{0}({1})", new object[]
			{
				fullName,
				string.Join(", ", array)
			});
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00004F84 File Offset: 0x00003184
		private static void GuardTypeIsNonPrimitive(IBuilderContext context)
		{
			Type type = context.BuildKey.Type;
			if (!type.GetTypeInfo().IsInterface && type == typeof(string))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.TypeIsNotConstructable, new object[]
				{
					type.GetTypeInfo().Name
				}));
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00004FE2 File Offset: 0x000031E2
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class.")]
		public static void SetCurrentOperationToResolvingParameter(string parameterName, string constructorSignature, IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			context.CurrentOperation = new ConstructorArgumentResolveOperation(context.BuildKey.Type, constructorSignature, parameterName);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00005007 File Offset: 0x00003207
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class.")]
		public static void SetCurrentOperationToInvokingConstructor(string constructorSignature, IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			context.CurrentOperation = new InvokingConstructorOperation(context.BuildKey.Type, constructorSignature);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000502C File Offset: 0x0000322C
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class.")]
		public static void ThrowForAttemptingToConstructInterface(IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.CannotConstructInterface, new object[]
			{
				context.BuildKey.Type,
				context.BuildKey
			}));
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00005078 File Offset: 0x00003278
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class.")]
		public static void ThrowForAttemptingToConstructAbstractClass(IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.CannotConstructAbstractClass, new object[]
			{
				context.BuildKey.Type,
				context.BuildKey
			}));
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000050C4 File Offset: 0x000032C4
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class.")]
		public static void ThrowForAttemptingToConstructDelegate(IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.CannotConstructDelegate, new object[]
			{
				context.BuildKey.Type,
				context.BuildKey
			}));
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00005110 File Offset: 0x00003310
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public static void ThrowForNullExistingObject(IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.NoConstructorFound, new object[]
			{
				context.BuildKey.Type.GetTypeInfo().Name
			}));
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000515C File Offset: 0x0000335C
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public static void ThrowForNullExistingObjectWithInvalidConstructor(IBuilderContext context, string signature)
		{
			Guard.ArgumentNotNull(context, "context");
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.SelectedConstructorHasRefParameters, new object[]
			{
				context.BuildKey.Type.GetTypeInfo().Name,
				signature
			}));
		}

		// Token: 0x04000048 RID: 72
		private static readonly MethodInfo ThrowForNullExistingObjectMethod = StaticReflection.GetMethodInfo(() => DynamicMethodConstructorStrategy.ThrowForNullExistingObject(null));

		// Token: 0x04000049 RID: 73
		private static readonly MethodInfo ThrowForNullExistingObjectWithInvalidConstructorMethod = StaticReflection.GetMethodInfo(() => DynamicMethodConstructorStrategy.ThrowForNullExistingObjectWithInvalidConstructor(null, null));

		// Token: 0x0400004A RID: 74
		private static readonly MethodInfo ThrowForAttemptingToConstructInterfaceMethod = StaticReflection.GetMethodInfo(() => DynamicMethodConstructorStrategy.ThrowForAttemptingToConstructInterface(null));

		// Token: 0x0400004B RID: 75
		private static readonly MethodInfo ThrowForAttemptingToConstructAbstractClassMethod = StaticReflection.GetMethodInfo(() => DynamicMethodConstructorStrategy.ThrowForAttemptingToConstructAbstractClass(null));

		// Token: 0x0400004C RID: 76
		private static readonly MethodInfo ThrowForAttemptingToConstructDelegateMethod = StaticReflection.GetMethodInfo(() => DynamicMethodConstructorStrategy.ThrowForAttemptingToConstructDelegate(null));

		// Token: 0x0400004D RID: 77
		private static readonly MethodInfo SetCurrentOperationToResolvingParameterMethod = StaticReflection.GetMethodInfo(() => DynamicMethodConstructorStrategy.SetCurrentOperationToResolvingParameter(null, null, null));

		// Token: 0x0400004E RID: 78
		private static readonly MethodInfo SetCurrentOperationToInvokingConstructorMethod = StaticReflection.GetMethodInfo(() => DynamicMethodConstructorStrategy.SetCurrentOperationToInvokingConstructor(null, null));

		// Token: 0x0400004F RID: 79
		private static readonly MethodInfo SetPerBuildSingletonMethod = StaticReflection.GetMethodInfo(() => DynamicMethodConstructorStrategy.SetPerBuildSingleton(null));
	}
}
