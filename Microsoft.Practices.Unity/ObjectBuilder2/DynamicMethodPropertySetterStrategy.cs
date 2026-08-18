using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Practices.Unity.Properties;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000060 RID: 96
	public class DynamicMethodPropertySetterStrategy : BuilderStrategy
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x00006060 File Offset: 0x00004260
		public override void PreBuildUp(IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			DynamicBuildPlanGenerationContext dynamicBuildPlanGenerationContext = (DynamicBuildPlanGenerationContext)context.Existing;
			IPolicyList resolverPolicyDestination;
			IPropertySelectorPolicy propertySelectorPolicy = context.Policies.Get(context.BuildKey, out resolverPolicyDestination);
			bool flag = false;
			foreach (SelectedProperty selectedProperty in propertySelectorPolicy.SelectProperties(context, resolverPolicyDestination))
			{
				flag = true;
				ParameterExpression parameterExpression = Expression.Parameter(selectedProperty.Property.PropertyType);
				dynamicBuildPlanGenerationContext.AddToBuildPlan(Expression.Block(new ParameterExpression[]
				{
					parameterExpression
				}, new Expression[]
				{
					Expression.Call(null, DynamicMethodPropertySetterStrategy.SetCurrentOperationToResolvingPropertyValueMethod, Expression.Constant(selectedProperty.Property.Name), dynamicBuildPlanGenerationContext.ContextParameter),
					Expression.Assign(parameterExpression, dynamicBuildPlanGenerationContext.GetResolveDependencyExpression(selectedProperty.Property.PropertyType, selectedProperty.Resolver)),
					Expression.Call(null, DynamicMethodPropertySetterStrategy.SetCurrentOperationToSettingPropertyMethod, Expression.Constant(selectedProperty.Property.Name), dynamicBuildPlanGenerationContext.ContextParameter),
					Expression.Call(Expression.Convert(dynamicBuildPlanGenerationContext.GetExistingObjectExpression(), dynamicBuildPlanGenerationContext.TypeToBuild), DynamicMethodPropertySetterStrategy.GetValidatedPropertySetter(selectedProperty.Property), new Expression[]
					{
						parameterExpression
					})
				}));
			}
			if (flag)
			{
				dynamicBuildPlanGenerationContext.AddToBuildPlan(dynamicBuildPlanGenerationContext.GetClearCurrentOperationExpression());
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000061D0 File Offset: 0x000043D0
		private static MethodInfo GetValidatedPropertySetter(PropertyInfo property)
		{
			MethodInfo setMethod = property.SetMethod;
			if (setMethod == null || setMethod.IsPrivate)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.PropertyNotSettable, new object[]
				{
					property.Name,
					property.DeclaringType.FullName
				}));
			}
			return setMethod;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00006224 File Offset: 0x00004424
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class.")]
		public static void SetCurrentOperationToResolvingPropertyValue(string propertyName, IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			context.CurrentOperation = new ResolvingPropertyValueOperation(context.BuildKey.Type, propertyName);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00006248 File Offset: 0x00004448
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class.")]
		public static void SetCurrentOperationToSettingProperty(string propertyName, IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			context.CurrentOperation = new SettingPropertyOperation(context.BuildKey.Type, propertyName);
		}

		// Token: 0x04000062 RID: 98
		private static readonly MethodInfo SetCurrentOperationToResolvingPropertyValueMethod = StaticReflection.GetMethodInfo(() => DynamicMethodPropertySetterStrategy.SetCurrentOperationToResolvingPropertyValue(null, null));

		// Token: 0x04000063 RID: 99
		private static readonly MethodInfo SetCurrentOperationToSettingPropertyMethod = StaticReflection.GetMethodInfo(() => DynamicMethodPropertySetterStrategy.SetCurrentOperationToSettingProperty(null, null));
	}
}
