using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.ObjectBuilder;
using Microsoft.Practices.Unity.Properties;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000087 RID: 135
	public class InjectionProperty : InjectionMember
	{
		// Token: 0x06000271 RID: 625 RVA: 0x00008110 File Offset: 0x00006310
		public InjectionProperty(string propertyName)
		{
			this.propertyName = propertyName;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000811F File Offset: 0x0000631F
		public InjectionProperty(string propertyName, object propertyValue)
		{
			this.propertyName = propertyName;
			this.parameterValue = InjectionParameterValue.ToParameter(propertyValue);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00008160 File Offset: 0x00006360
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation is done via Guard class")]
		public override void AddPolicies(Type serviceType, Type implementationType, string name, IPolicyList policies)
		{
			Guard.ArgumentNotNull(implementationType, "implementationType");
			PropertyInfo propertyInfo = implementationType.GetPropertiesHierarchical().FirstOrDefault((PropertyInfo p) => p.Name == this.propertyName && !p.SetMethod.IsStatic);
			InjectionProperty.GuardPropertyExists(propertyInfo, implementationType, this.propertyName);
			InjectionProperty.GuardPropertyIsSettable(propertyInfo);
			InjectionProperty.GuardPropertyIsNotIndexer(propertyInfo);
			this.InitializeParameterValue(propertyInfo);
			InjectionProperty.GuardPropertyValueIsCompatible(propertyInfo, this.parameterValue);
			SpecifiedPropertiesSelectorPolicy selectorPolicy = InjectionProperty.GetSelectorPolicy(policies, implementationType, name);
			selectorPolicy.AddPropertyAndValue(propertyInfo, this.parameterValue);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x000081D4 File Offset: 0x000063D4
		private InjectionParameterValue InitializeParameterValue(PropertyInfo propInfo)
		{
			if (this.parameterValue == null)
			{
				this.parameterValue = new ResolvedParameter(propInfo.PropertyType);
			}
			return this.parameterValue;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x000081F8 File Offset: 0x000063F8
		private static SpecifiedPropertiesSelectorPolicy GetSelectorPolicy(IPolicyList policies, Type typeToInject, string name)
		{
			NamedTypeBuildKey buildKey = new NamedTypeBuildKey(typeToInject, name);
			IPropertySelectorPolicy propertySelectorPolicy = policies.GetNoDefault(buildKey, false);
			if (propertySelectorPolicy == null || !(propertySelectorPolicy is SpecifiedPropertiesSelectorPolicy))
			{
				propertySelectorPolicy = new SpecifiedPropertiesSelectorPolicy();
				policies.Set(propertySelectorPolicy, buildKey);
			}
			return (SpecifiedPropertiesSelectorPolicy)propertySelectorPolicy;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00008238 File Offset: 0x00006438
		private static void GuardPropertyExists(PropertyInfo propInfo, Type typeToCreate, string propertyName)
		{
			if (propInfo == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.NoSuchProperty, new object[]
				{
					typeToCreate.GetTypeInfo().Name,
					propertyName
				}));
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00008278 File Offset: 0x00006478
		private static void GuardPropertyIsSettable(PropertyInfo propInfo)
		{
			if (!propInfo.CanWrite)
			{
				throw new InvalidOperationException(InjectionProperty.ExceptionMessage(Resources.PropertyNotSettable, new object[]
				{
					propInfo.Name,
					propInfo.DeclaringType
				}));
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000082B8 File Offset: 0x000064B8
		private static void GuardPropertyIsNotIndexer(PropertyInfo property)
		{
			if (property.GetIndexParameters().Length > 0)
			{
				throw new InvalidOperationException(InjectionProperty.ExceptionMessage(Resources.CannotInjectIndexer, new object[]
				{
					property.Name,
					property.DeclaringType
				}));
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000082FC File Offset: 0x000064FC
		private static void GuardPropertyValueIsCompatible(PropertyInfo property, InjectionParameterValue value)
		{
			if (!value.MatchesType(property.PropertyType))
			{
				throw new InvalidOperationException(InjectionProperty.ExceptionMessage(Resources.PropertyTypeMismatch, new object[]
				{
					property.Name,
					property.DeclaringType,
					property.PropertyType,
					value.ParameterTypeName
				}));
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00008354 File Offset: 0x00006554
		private static string ExceptionMessage(string format, params object[] args)
		{
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i] is Type)
				{
					args[i] = ((Type)args[i]).GetTypeInfo().Name;
				}
			}
			return string.Format(CultureInfo.CurrentCulture, format, args);
		}

		// Token: 0x04000088 RID: 136
		private readonly string propertyName;

		// Token: 0x04000089 RID: 137
		private InjectionParameterValue parameterValue;
	}
}
