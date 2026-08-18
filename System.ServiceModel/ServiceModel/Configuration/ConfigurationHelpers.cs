using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Diagnostics;
using System.Security;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200060A RID: 1546
	internal static class ConfigurationHelpers
	{
		// Token: 0x06003B7C RID: 15228 RVA: 0x000E3B68 File Offset: 0x000E1D68
		internal static BindingCollectionElement GetAssociatedBindingCollectionElement(ContextInformation evaluationContext, string bindingCollectionName)
		{
			BindingCollectionElement result = null;
			BindingsSection bindingsSection = (BindingsSection)ConfigurationHelpers.GetAssociatedSection(evaluationContext, ConfigurationStrings.BindingsSectionGroupPath);
			if (bindingsSection != null)
			{
				bindingsSection.UpdateBindingSections(evaluationContext);
				try
				{
					result = bindingsSection[bindingCollectionName];
				}
				catch (KeyNotFoundException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigBindingExtensionNotFound", new object[]
					{
						ConfigurationHelpers.GetBindingsSectionPath(bindingCollectionName)
					})));
				}
				catch (NullReferenceException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigBindingExtensionNotFound", new object[]
					{
						ConfigurationHelpers.GetBindingsSectionPath(bindingCollectionName)
					})));
				}
			}
			return result;
		}

		// Token: 0x06003B7D RID: 15229 RVA: 0x000E3C10 File Offset: 0x000E1E10
		[SecurityCritical]
		internal static BindingCollectionElement UnsafeGetAssociatedBindingCollectionElement(ContextInformation evaluationContext, string bindingCollectionName)
		{
			BindingCollectionElement result = null;
			BindingsSection bindingsSection = (BindingsSection)ConfigurationHelpers.UnsafeGetAssociatedSection(evaluationContext, ConfigurationStrings.BindingsSectionGroupPath);
			if (bindingsSection != null)
			{
				bindingsSection.UpdateBindingSections(evaluationContext);
				try
				{
					result = bindingsSection[bindingCollectionName];
				}
				catch (KeyNotFoundException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigBindingExtensionNotFound", new object[]
					{
						ConfigurationHelpers.GetBindingsSectionPath(bindingCollectionName)
					})));
				}
				catch (NullReferenceException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigBindingExtensionNotFound", new object[]
					{
						ConfigurationHelpers.GetBindingsSectionPath(bindingCollectionName)
					})));
				}
			}
			return result;
		}

		// Token: 0x06003B7E RID: 15230 RVA: 0x000E3CB8 File Offset: 0x000E1EB8
		internal static EndpointCollectionElement GetAssociatedEndpointCollectionElement(ContextInformation evaluationContext, string endpointCollectionName)
		{
			EndpointCollectionElement result = null;
			StandardEndpointsSection standardEndpointsSection = (StandardEndpointsSection)ConfigurationHelpers.GetAssociatedSection(evaluationContext, ConfigurationStrings.StandardEndpointsSectionPath);
			if (standardEndpointsSection != null)
			{
				standardEndpointsSection.UpdateEndpointSections(evaluationContext);
				try
				{
					result = standardEndpointsSection[endpointCollectionName];
				}
				catch (KeyNotFoundException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigEndpointExtensionNotFound", new object[]
					{
						ConfigurationHelpers.GetEndpointsSectionPath(endpointCollectionName)
					})));
				}
				catch (NullReferenceException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigEndpointExtensionNotFound", new object[]
					{
						ConfigurationHelpers.GetEndpointsSectionPath(endpointCollectionName)
					})));
				}
			}
			return result;
		}

		// Token: 0x06003B7F RID: 15231 RVA: 0x000E3D60 File Offset: 0x000E1F60
		[SecurityCritical]
		internal static EndpointCollectionElement UnsafeGetAssociatedEndpointCollectionElement(ContextInformation evaluationContext, string endpointCollectionName)
		{
			EndpointCollectionElement result = null;
			StandardEndpointsSection standardEndpointsSection = (StandardEndpointsSection)ConfigurationHelpers.UnsafeGetAssociatedSection(evaluationContext, ConfigurationStrings.StandardEndpointsSectionPath);
			if (standardEndpointsSection != null)
			{
				standardEndpointsSection.UpdateEndpointSections(evaluationContext);
				try
				{
					result = standardEndpointsSection[endpointCollectionName];
				}
				catch (KeyNotFoundException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigEndpointExtensionNotFound", new object[]
					{
						ConfigurationHelpers.GetEndpointsSectionPath(endpointCollectionName)
					})));
				}
				catch (NullReferenceException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigEndpointExtensionNotFound", new object[]
					{
						ConfigurationHelpers.GetEndpointsSectionPath(endpointCollectionName)
					})));
				}
			}
			return result;
		}

		// Token: 0x06003B80 RID: 15232 RVA: 0x000E3E08 File Offset: 0x000E2008
		internal static object GetAssociatedSection(ContextInformation evalContext, string sectionPath)
		{
			object obj;
			if (evalContext != null)
			{
				obj = evalContext.GetSection(sectionPath);
			}
			else
			{
				obj = AspNetEnvironment.Current.GetConfigurationSection(sectionPath);
				if (DiagnosticUtility.ShouldTraceVerbose)
				{
					TraceUtility.TraceEvent(TraceEventType.Verbose, 524324, SR.GetString("TraceCodeGetConfigurationSection"), new StringTraceRecord("ConfigurationSection", sectionPath), null, null);
				}
			}
			if (obj == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigSectionNotFound", new object[]
				{
					sectionPath
				})));
			}
			return obj;
		}

		// Token: 0x06003B81 RID: 15233 RVA: 0x000E3E84 File Offset: 0x000E2084
		[SecurityCritical]
		internal static object UnsafeGetAssociatedSection(ContextInformation evalContext, string sectionPath)
		{
			object obj;
			if (evalContext != null)
			{
				obj = ConfigurationHelpers.UnsafeGetSectionFromContext(evalContext, sectionPath);
			}
			else
			{
				obj = AspNetEnvironment.Current.UnsafeGetConfigurationSection(sectionPath);
				if (DiagnosticUtility.ShouldTraceVerbose)
				{
					TraceUtility.TraceEvent(TraceEventType.Verbose, 524324, SR.GetString("TraceCodeGetConfigurationSection"), new StringTraceRecord("ConfigurationSection", sectionPath), null, null);
				}
			}
			if (obj == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigSectionNotFound", new object[]
				{
					sectionPath
				})));
			}
			return obj;
		}

		// Token: 0x06003B82 RID: 15234 RVA: 0x000E3EFE File Offset: 0x000E20FE
		internal static BindingCollectionElement GetBindingCollectionElement(string bindingCollectionName)
		{
			return ConfigurationHelpers.GetAssociatedBindingCollectionElement(null, bindingCollectionName);
		}

		// Token: 0x06003B83 RID: 15235 RVA: 0x000E3F07 File Offset: 0x000E2107
		[SecurityCritical]
		internal static BindingCollectionElement UnsafeGetBindingCollectionElement(string bindingCollectionName)
		{
			return ConfigurationHelpers.UnsafeGetAssociatedBindingCollectionElement(null, bindingCollectionName);
		}

		// Token: 0x06003B84 RID: 15236 RVA: 0x000E3F10 File Offset: 0x000E2110
		internal static string GetBindingsSectionPath(string sectionName)
		{
			return ConfigurationStrings.BindingsSectionGroupPath + "/" + sectionName;
		}

		// Token: 0x06003B85 RID: 15237 RVA: 0x000E3F22 File Offset: 0x000E2122
		internal static string GetEndpointsSectionPath(string sectionName)
		{
			return "standardEndpoints" + "/" + sectionName;
		}

		// Token: 0x06003B86 RID: 15238 RVA: 0x000E3F34 File Offset: 0x000E2134
		internal static EndpointCollectionElement GetEndpointCollectionElement(string endpointCollectionName)
		{
			return ConfigurationHelpers.GetAssociatedEndpointCollectionElement(null, endpointCollectionName);
		}

		// Token: 0x06003B87 RID: 15239 RVA: 0x000E3F3D File Offset: 0x000E213D
		[SecurityCritical]
		internal static EndpointCollectionElement UnsafeGetEndpointCollectionElement(string endpointCollectionName)
		{
			return ConfigurationHelpers.UnsafeGetAssociatedEndpointCollectionElement(null, endpointCollectionName);
		}

		// Token: 0x06003B88 RID: 15240 RVA: 0x000E3F46 File Offset: 0x000E2146
		internal static object GetSection(string sectionPath)
		{
			return ConfigurationHelpers.GetAssociatedSection(null, sectionPath);
		}

		// Token: 0x06003B89 RID: 15241 RVA: 0x000E3F4F File Offset: 0x000E214F
		[SecurityCritical]
		internal static object UnsafeGetSection(string sectionPath)
		{
			return ConfigurationHelpers.UnsafeGetAssociatedSection(null, sectionPath);
		}

		// Token: 0x06003B8A RID: 15242 RVA: 0x000E3F58 File Offset: 0x000E2158
		[SecurityCritical]
		internal static object UnsafeGetSectionNoTrace(string sectionPath)
		{
			object obj = AspNetEnvironment.Current.UnsafeGetConfigurationSection(sectionPath);
			if (obj == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigSectionNotFound", new object[]
				{
					sectionPath
				})));
			}
			return obj;
		}

		// Token: 0x06003B8B RID: 15243 RVA: 0x000E3F99 File Offset: 0x000E2199
		[SecurityCritical]
		[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static object UnsafeGetSectionFromContext(ContextInformation evalContext, string sectionPath)
		{
			return evalContext.GetSection(sectionPath);
		}

		// Token: 0x06003B8C RID: 15244 RVA: 0x000E3FA2 File Offset: 0x000E21A2
		internal static string GetSectionPath(string sectionName)
		{
			return "system.serviceModel" + "/" + sectionName;
		}

		// Token: 0x06003B8D RID: 15245 RVA: 0x000E3FB4 File Offset: 0x000E21B4
		[SecurityCritical]
		internal static void SetIsPresent(ConfigurationElement element)
		{
			PropertyInfo property = element.GetType().GetProperty("ElementPresent", BindingFlags.Instance | BindingFlags.NonPublic);
			ConfigurationHelpers.SetIsPresentWithAssert(property, element, true);
		}

		// Token: 0x06003B8E RID: 15246 RVA: 0x000E3FDC File Offset: 0x000E21DC
		[SecurityCritical]
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private static void SetIsPresentWithAssert(PropertyInfo elementPresent, ConfigurationElement element, bool value)
		{
			elementPresent.SetValue(element, value, null);
		}

		// Token: 0x06003B8F RID: 15247 RVA: 0x000E3FEC File Offset: 0x000E21EC
		internal static ContextInformation GetEvaluationContext(IConfigurationContextProviderInternal provider)
		{
			if (provider != null)
			{
				try
				{
					return provider.GetEvaluationContext();
				}
				catch (ConfigurationErrorsException)
				{
				}
			}
			return null;
		}

		// Token: 0x06003B90 RID: 15248 RVA: 0x000E401C File Offset: 0x000E221C
		internal static ContextInformation GetOriginalEvaluationContext(IConfigurationContextProviderInternal provider)
		{
			if (provider != null)
			{
				try
				{
					return provider.GetOriginalEvaluationContext();
				}
				catch (ConfigurationErrorsException)
				{
				}
			}
			return null;
		}

		// Token: 0x06003B91 RID: 15249 RVA: 0x000E404C File Offset: 0x000E224C
		internal static void TraceExtensionTypeNotFound(ExtensionElement extensionElement)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				DictionaryTraceRecord extendedData = new DictionaryTraceRecord(new Dictionary<string, string>(2)
				{
					{
						"ExtensionName",
						extensionElement.Name
					},
					{
						"ExtensionType",
						extensionElement.Type
					}
				});
				TraceUtility.TraceEvent(TraceEventType.Warning, 524357, SR.GetString("TraceCodeExtensionTypeNotFound"), extendedData, null, null);
			}
		}
	}
}
