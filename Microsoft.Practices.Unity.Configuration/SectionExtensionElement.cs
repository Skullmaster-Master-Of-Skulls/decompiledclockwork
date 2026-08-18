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
	// Token: 0x0200003A RID: 58
	public class SectionExtensionElement : DeserializableConfigurationElement
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00006557 File Offset: 0x00004757
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x00006569 File Offset: 0x00004769
		[ConfigurationProperty("type", IsRequired = true, IsKey = true)]
		public string TypeName
		{
			get
			{
				return (string)base["type"];
			}
			set
			{
				base["type"] = value;
				this.extensionObject = null;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000657E File Offset: 0x0000477E
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x00006590 File Offset: 0x00004790
		[ConfigurationProperty("prefix", IsRequired = false)]
		public string Prefix
		{
			get
			{
				return (string)base["prefix"];
			}
			set
			{
				base["prefix"] = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000659E File Offset: 0x0000479E
		public SectionExtension ExtensionObject
		{
			get
			{
				if (this.extensionObject == null)
				{
					this.extensionObject = (SectionExtension)Activator.CreateInstance(this.GetExtensionObjectType());
				}
				return this.extensionObject;
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000065C4 File Offset: 0x000047C4
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			base.DeserializeElement(reader, serializeCollectionKey);
			this.GetExtensionObjectType();
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000065D5 File Offset: 0x000047D5
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public override void SerializeContent(XmlWriter writer)
		{
			Guard.ArgumentNotNull(writer, "writer");
			writer.WriteAttributeString("type", this.TypeName);
			if (!string.IsNullOrEmpty(this.Prefix))
			{
				writer.WriteAttributeString("prefix", this.Prefix);
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00006614 File Offset: 0x00004814
		private void GuardIsValidExtensionType(Type extensionType)
		{
			if (extensionType == null)
			{
				throw new ConfigurationErrorsException(string.Format(CultureInfo.CurrentCulture, Resources.ExtensionTypeNotFound, new object[]
				{
					this.TypeName
				}));
			}
			if (!typeof(SectionExtension).IsAssignableFrom(extensionType))
			{
				throw new ConfigurationErrorsException(string.Format(CultureInfo.CurrentCulture, Resources.ExtensionTypeNotValid, new object[]
				{
					this.TypeName
				}));
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00006688 File Offset: 0x00004888
		private Type GetExtensionObjectType()
		{
			Type type = TypeResolver.ResolveType(this.TypeName);
			this.GuardIsValidExtensionType(type);
			return type;
		}

		// Token: 0x04000067 RID: 103
		private const string TypeNamePropertyName = "type";

		// Token: 0x04000068 RID: 104
		private const string PrefixPropertyName = "prefix";

		// Token: 0x04000069 RID: 105
		private SectionExtension extensionObject;
	}
}
