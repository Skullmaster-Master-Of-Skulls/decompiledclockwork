using System;
using System.Collections.Generic;
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
	// Token: 0x02000035 RID: 53
	public class PropertyElement : InjectionMemberElement, IValueProvidingElement
	{
		// Token: 0x060001A5 RID: 421 RVA: 0x0000606E File Offset: 0x0000426E
		public PropertyElement()
		{
			this.valueElementHelper = new ValueElementHelper(this);
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00006082 File Offset: 0x00004282
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00006094 File Offset: 0x00004294
		[ConfigurationProperty("name", IsRequired = true)]
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

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x000060A2 File Offset: 0x000042A2
		public override string Key
		{
			get
			{
				return "property:" + this.Name;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x000060B4 File Offset: 0x000042B4
		// (set) Token: 0x060001AA RID: 426 RVA: 0x000060C1 File Offset: 0x000042C1
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

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001AB RID: 427 RVA: 0x000060CA File Offset: 0x000042CA
		// (set) Token: 0x060001AC RID: 428 RVA: 0x000060D2 File Offset: 0x000042D2
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

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001AD RID: 429 RVA: 0x000060DC File Offset: 0x000042DC
		public string DestinationName
		{
			get
			{
				return string.Format(CultureInfo.CurrentCulture, Resources.DestinationNameFormat, new object[]
				{
					Resources.Property,
					this.Name
				});
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00006111 File Offset: 0x00004311
		public override string ElementName
		{
			get
			{
				return "property";
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00006118 File Offset: 0x00004318
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			base.DeserializeElement(reader, serializeCollectionKey);
			this.valueElementHelper.CompleteValueElement(reader);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000612E File Offset: 0x0000432E
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			return this.valueElementHelper.DeserializeUnrecognizedAttribute(name, value);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000613D File Offset: 0x0000433D
		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			return this.valueElementHelper.DeserializeUnknownElement(elementName, reader) || base.OnDeserializeUnrecognizedElement(elementName, reader);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00006158 File Offset: 0x00004358
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public override void SerializeContent(XmlWriter writer)
		{
			Guard.ArgumentNotNull(writer, "writer");
			writer.WriteAttributeString("name", this.Name);
			ValueElementHelper.SerializeParameterValueElement(writer, this.Value, false);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00006184 File Offset: 0x00004384
		public override IEnumerable<InjectionMember> GetInjectionMembers(IUnityContainer container, Type fromType, Type toType, string name)
		{
			return new InjectionProperty[]
			{
				new InjectionProperty(this.Name, this.Value.GetInjectionParameterValue(container, this.GetPropertyType(toType)))
			};
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000061BC File Offset: 0x000043BC
		private Type GetPropertyType(Type typeContainingProperty)
		{
			PropertyInfo property = typeContainingProperty.GetProperty(this.Name);
			if (property == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.NoSuchProperty, new object[]
				{
					typeContainingProperty.Name,
					this.Name
				}));
			}
			return property.PropertyType;
		}

		// Token: 0x0400005D RID: 93
		private const string NamePropertyName = "name";

		// Token: 0x0400005E RID: 94
		private readonly ValueElementHelper valueElementHelper;

		// Token: 0x0400005F RID: 95
		private ParameterValueElement valueElement;
	}
}
