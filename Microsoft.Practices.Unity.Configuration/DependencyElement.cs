using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Xml;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;
using Microsoft.Practices.Unity.Configuration.Properties;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x02000022 RID: 34
	public class DependencyElement : ParameterValueElement, IAttributeOnlyElement
	{
		// Token: 0x06000104 RID: 260 RVA: 0x00004EF3 File Offset: 0x000030F3
		public DependencyElement()
		{
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004F10 File Offset: 0x00003110
		public DependencyElement(IDictionary<string, string> attributeValues)
		{
			DependencyElement.SetIfPresent(attributeValues, "dependencyName", delegate(string value)
			{
				this.Name = value;
			});
			DependencyElement.SetIfPresent(attributeValues, "dependencyType", delegate(string value)
			{
				this.TypeName = value;
			});
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00004F5F File Offset: 0x0000315F
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00004F71 File Offset: 0x00003171
		[ConfigurationProperty("name", IsRequired = false)]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				base["name"] = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00004F7F File Offset: 0x0000317F
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00004F91 File Offset: 0x00003191
		[ConfigurationProperty("type", IsRequired = false)]
		public string TypeName
		{
			get
			{
				return (string)base["type"];
			}
			set
			{
				base["type"] = value;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004F9F File Offset: 0x0000319F
		void IAttributeOnlyElement.SerializeContent(XmlWriter writer)
		{
			writer.WriteAttributeIfNotEmpty("dependencyName", this.Name).WriteAttributeIfNotEmpty("dependencyType", this.TypeName);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004FC3 File Offset: 0x000031C3
		public override void SerializeContent(XmlWriter writer)
		{
			writer.WriteAttributeIfNotEmpty("name", this.Name).WriteAttributeIfNotEmpty("type", this.TypeName);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004FE8 File Offset: 0x000031E8
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public override InjectionParameterValue GetInjectionParameterValue(IUnityContainer container, Type parameterType)
		{
			Guard.ArgumentNotNull(parameterType, "parameterType");
			string text = this.Name;
			if (string.IsNullOrEmpty(text))
			{
				text = null;
			}
			if (!parameterType.IsGenericParameter)
			{
				return new ResolvedParameter(TypeResolver.ResolveTypeWithDefault(this.TypeName, parameterType), text);
			}
			if (!string.IsNullOrEmpty(this.TypeName))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.DependencyForGenericParameterWithTypeSet, new object[]
				{
					parameterType.Name,
					this.TypeName
				}));
			}
			return new GenericParameter(parameterType.Name, text);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005074 File Offset: 0x00003274
		private static void SetIfPresent(IDictionary<string, string> attributeValues, string key, Action<string> setter)
		{
			if (attributeValues.ContainsKey(key))
			{
				setter(attributeValues[key]);
			}
		}

		// Token: 0x0400003E RID: 62
		private const string NamePropertyName = "name";

		// Token: 0x0400003F RID: 63
		private const string TypeNamePropertyName = "type";
	}
}
