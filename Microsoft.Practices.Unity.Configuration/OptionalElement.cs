using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Xml;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;
using Microsoft.Practices.Unity.Configuration.Properties;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x0200002F RID: 47
	public class OptionalElement : ParameterValueElement
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00005A69 File Offset: 0x00003C69
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00005A7B File Offset: 0x00003C7B
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

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00005A89 File Offset: 0x00003C89
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00005A9B File Offset: 0x00003C9B
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

		// Token: 0x06000168 RID: 360 RVA: 0x00005AA9 File Offset: 0x00003CA9
		public override void SerializeContent(XmlWriter writer)
		{
			writer.WriteAttributeIfNotEmpty("name", this.Name).WriteAttributeIfNotEmpty("type", this.TypeName);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005AD0 File Offset: 0x00003CD0
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
				return new OptionalParameter(TypeResolver.ResolveTypeWithDefault(this.TypeName, parameterType), text);
			}
			if (!string.IsNullOrEmpty(this.TypeName))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.DependencyForOptionalGenericParameterWithTypeSet, new object[]
				{
					parameterType.Name,
					this.TypeName
				}));
			}
			return new OptionalGenericParameter(parameterType.Name, text);
		}

		// Token: 0x04000054 RID: 84
		private const string NamePropertyName = "name";

		// Token: 0x04000055 RID: 85
		private const string TypeNamePropertyName = "type";
	}
}
