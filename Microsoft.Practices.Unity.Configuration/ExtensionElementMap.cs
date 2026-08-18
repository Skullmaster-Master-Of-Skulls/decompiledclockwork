using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;
using Microsoft.Practices.Unity.Configuration.Properties;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x02000025 RID: 37
	public static class ExtensionElementMap
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00005100 File Offset: 0x00003300
		private static ExtensionElementMap.ExtensionElementMapImpl Instance
		{
			get
			{
				if (ExtensionElementMap.instance == null)
				{
					ExtensionElementMap.instance = new ExtensionElementMap.ExtensionElementMapImpl();
				}
				return ExtensionElementMap.instance;
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005118 File Offset: 0x00003318
		public static void Clear()
		{
			ExtensionElementMap.Instance.Clear();
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005124 File Offset: 0x00003324
		public static void AddContainerConfiguringElement(string prefix, string tag, Type elementType)
		{
			ExtensionElementMap.Instance.AddContainerConfiguringElement(prefix, tag, elementType);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005133 File Offset: 0x00003333
		public static void AddInjectionMemberElement(string prefix, string tag, Type elementType)
		{
			ExtensionElementMap.Instance.AddInjectionMemberElement(prefix, tag, elementType);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005142 File Offset: 0x00003342
		public static void AddParameterValueElement(string prefix, string tag, Type elementType)
		{
			ExtensionElementMap.Instance.AddParameterValueElement(prefix, tag, elementType);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005151 File Offset: 0x00003351
		public static Type GetContainerConfiguringElementType(string tag)
		{
			return ExtensionElementMap.Instance.GetContainerConfiguringElementType(tag);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000515E File Offset: 0x0000335E
		public static Type GetInjectionMemberElementType(string tag)
		{
			return ExtensionElementMap.Instance.GetInjectionMemberElementType(tag);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000516B File Offset: 0x0000336B
		public static Type GetParameterValueElementType(string tag)
		{
			return ExtensionElementMap.Instance.GetParameterValueElementType(tag);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005178 File Offset: 0x00003378
		public static string GetTagForExtensionElement(ConfigurationElement element)
		{
			Guard.ArgumentNotNull(element, "element");
			return ExtensionElementMap.Instance.GetTagForExtensionElement(element);
		}

		// Token: 0x04000042 RID: 66
		[ThreadStatic]
		private static ExtensionElementMap.ExtensionElementMapImpl instance;

		// Token: 0x02000026 RID: 38
		private class ExtensionElementMapImpl
		{
			// Token: 0x06000120 RID: 288 RVA: 0x00005190 File Offset: 0x00003390
			public void Clear()
			{
				this.containerConfiguringElements.Clear();
				this.injectionMemberElements.Clear();
				this.parameterValueElements.Clear();
			}

			// Token: 0x06000121 RID: 289 RVA: 0x000051B3 File Offset: 0x000033B3
			public void AddContainerConfiguringElement(string prefix, string tag, Type elementType)
			{
				this.containerConfiguringElements[ExtensionElementMap.ExtensionElementMapImpl.CreateTag(prefix, tag)] = elementType;
			}

			// Token: 0x06000122 RID: 290 RVA: 0x000051C8 File Offset: 0x000033C8
			public void AddInjectionMemberElement(string prefix, string tag, Type elementType)
			{
				this.injectionMemberElements[ExtensionElementMap.ExtensionElementMapImpl.CreateTag(prefix, tag)] = elementType;
			}

			// Token: 0x06000123 RID: 291 RVA: 0x000051DD File Offset: 0x000033DD
			public void AddParameterValueElement(string prefix, string tag, Type elementType)
			{
				this.parameterValueElements[ExtensionElementMap.ExtensionElementMapImpl.CreateTag(prefix, tag)] = elementType;
			}

			// Token: 0x06000124 RID: 292 RVA: 0x000051F2 File Offset: 0x000033F2
			public Type GetContainerConfiguringElementType(string tag)
			{
				return this.containerConfiguringElements.GetOrNull(tag);
			}

			// Token: 0x06000125 RID: 293 RVA: 0x00005200 File Offset: 0x00003400
			public Type GetInjectionMemberElementType(string tag)
			{
				return this.injectionMemberElements.GetOrNull(tag);
			}

			// Token: 0x06000126 RID: 294 RVA: 0x0000520E File Offset: 0x0000340E
			public Type GetParameterValueElementType(string tag)
			{
				return this.parameterValueElements.GetOrNull(tag);
			}

			// Token: 0x06000127 RID: 295 RVA: 0x0000521C File Offset: 0x0000341C
			public string GetTagForExtensionElement(ConfigurationElement element)
			{
				Type type = element.GetType();
				Dictionary<string, Type> dictToSearch = this.GetDictToSearch(type);
				foreach (KeyValuePair<string, Type> keyValuePair in dictToSearch)
				{
					if (keyValuePair.Value == type)
					{
						return keyValuePair.Key;
					}
				}
				throw ExtensionElementMap.ExtensionElementMapImpl.ElementTypeNotFound(type);
			}

			// Token: 0x06000128 RID: 296 RVA: 0x00005294 File Offset: 0x00003494
			private Dictionary<string, Type> GetDictToSearch(Type elementType)
			{
				if (typeof(ContainerConfiguringElement).IsAssignableFrom(elementType))
				{
					return this.containerConfiguringElements;
				}
				if (typeof(InjectionMemberElement).IsAssignableFrom(elementType))
				{
					return this.injectionMemberElements;
				}
				if (typeof(ParameterValueElement).IsAssignableFrom(elementType))
				{
					return this.parameterValueElements;
				}
				throw ExtensionElementMap.ExtensionElementMapImpl.ElementTypeNotFound(elementType);
			}

			// Token: 0x06000129 RID: 297 RVA: 0x000052F4 File Offset: 0x000034F4
			[SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "Factory method to create exception for callers.")]
			private static Exception ElementTypeNotFound(Type elementType)
			{
				return new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.ElementTypeNotRegistered, new object[]
				{
					elementType
				}), "memberElement");
			}

			// Token: 0x0600012A RID: 298 RVA: 0x00005326 File Offset: 0x00003526
			private static string CreateTag(string prefix, string tag)
			{
				if (string.IsNullOrEmpty(prefix))
				{
					return tag;
				}
				return prefix + "." + tag;
			}

			// Token: 0x04000043 RID: 67
			private readonly Dictionary<string, Type> containerConfiguringElements = new Dictionary<string, Type>();

			// Token: 0x04000044 RID: 68
			private readonly Dictionary<string, Type> injectionMemberElements = new Dictionary<string, Type>();

			// Token: 0x04000045 RID: 69
			private readonly Dictionary<string, Type> parameterValueElements = new Dictionary<string, Type>();
		}
	}
}
