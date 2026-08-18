using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Xml;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;
using Microsoft.Practices.Unity.Configuration.Properties;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x02000030 RID: 48
	public class ParameterElement : DeserializableConfigurationElement, IValueProvidingElement
	{
		// Token: 0x0600016B RID: 363 RVA: 0x00005B64 File Offset: 0x00003D64
		public ParameterElement()
		{
			this.valueElementHelper = new ValueElementHelper(this);
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00005B78 File Offset: 0x00003D78
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00005B8A File Offset: 0x00003D8A
		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
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

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00005B98 File Offset: 0x00003D98
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00005BAA File Offset: 0x00003DAA
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

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00005BB8 File Offset: 0x00003DB8
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00005BC5 File Offset: 0x00003DC5
		public ParameterValueElement Value
		{
			get
			{
				return ValueElementHelper.GetValue(this.valueElement);
			}
			set
			{
				this.valueElement = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00005BCE File Offset: 0x00003DCE
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00005BD6 File Offset: 0x00003DD6
		ParameterValueElement IValueProvidingElement.Value
		{
			get
			{
				return this.valueElement;
			}
			set
			{
				this.Value = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00005BE0 File Offset: 0x00003DE0
		public string DestinationName
		{
			get
			{
				return string.Format(CultureInfo.CurrentCulture, Resources.DestinationNameFormat, new object[]
				{
					Resources.Parameter,
					this.Name
				});
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00005C18 File Offset: 0x00003E18
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public InjectionParameterValue GetParameterValue(IUnityContainer container, Type parameterType)
		{
			Guard.ArgumentNotNull(parameterType, "parameterType");
			Type parameterType2 = parameterType;
			if (!string.IsNullOrEmpty(this.TypeName))
			{
				parameterType2 = TypeResolver.ResolveType(this.TypeName);
			}
			return this.Value.GetInjectionParameterValue(container, parameterType2);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005C58 File Offset: 0x00003E58
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public bool Matches(ParameterInfo parameterInfo)
		{
			Guard.ArgumentNotNull(parameterInfo, "parameterInfo");
			if (this.Name != parameterInfo.Name)
			{
				return false;
			}
			if (!string.IsNullOrEmpty(this.TypeName))
			{
				Type left = TypeResolver.ResolveType(this.TypeName);
				return left == parameterInfo.ParameterType;
			}
			return true;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005CAC File Offset: 0x00003EAC
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			base.DeserializeElement(reader, serializeCollectionKey);
			this.valueElementHelper.CompleteValueElement(reader);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005CC2 File Offset: 0x00003EC2
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			return name == "genericParameterName" || this.valueElementHelper.DeserializeUnrecognizedAttribute(name, value);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00005CE0 File Offset: 0x00003EE0
		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			return this.valueElementHelper.DeserializeUnknownElement(elementName, reader) || base.OnDeserializeUnrecognizedElement(elementName, reader);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00005CFB File Offset: 0x00003EFB
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public override void SerializeContent(XmlWriter writer)
		{
			Guard.ArgumentNotNull(writer, "writer");
			writer.WriteAttributeString("name", this.Name);
			writer.WriteAttributeIfNotEmpty("type", this.TypeName);
			ValueElementHelper.SerializeParameterValueElement(writer, this.Value, false);
		}

		// Token: 0x04000056 RID: 86
		private const string NamePropertyName = "name";

		// Token: 0x04000057 RID: 87
		private const string TypeNamePropertyName = "type";

		// Token: 0x04000058 RID: 88
		private readonly ValueElementHelper valueElementHelper;

		// Token: 0x04000059 RID: 89
		private ParameterValueElement valueElement;
	}
}
