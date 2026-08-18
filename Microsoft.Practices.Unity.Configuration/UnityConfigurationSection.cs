using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Xml;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;
using Microsoft.Practices.Unity.Configuration.Properties;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x0200003E RID: 62
	public class UnityConfigurationSection : ConfigurationSection
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00006F0A File Offset: 0x0000510A
		public static UnityConfigurationSection CurrentSection
		{
			get
			{
				return UnityConfigurationSection.currentSection;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00006F11 File Offset: 0x00005111
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00006F23 File Offset: 0x00005123
		[ConfigurationProperty("xmlns", IsRequired = false, DefaultValue = "http://schemas.microsoft.com/practices/2010/unity")]
		public string Xmlns
		{
			get
			{
				return (string)base["xmlns"];
			}
			set
			{
				base["xmlns"] = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00006F34 File Offset: 0x00005134
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public ContainerElementCollection Containers
		{
			get
			{
				ContainerElementCollection containerElementCollection = (ContainerElementCollection)base[""];
				containerElementCollection.ContainingSection = this;
				return containerElementCollection;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00006F5A File Offset: 0x0000515A
		[ConfigurationProperty("aliases")]
		public AliasElementCollection TypeAliases
		{
			get
			{
				return (AliasElementCollection)base["aliases"];
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00006F6C File Offset: 0x0000516C
		[ConfigurationProperty("extensions")]
		public SectionExtensionElementCollection SectionExtensions
		{
			get
			{
				return (SectionExtensionElementCollection)base["extensions"];
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00006F7E File Offset: 0x0000517E
		[ConfigurationProperty("namespaces")]
		public NamespaceElementCollection Namespaces
		{
			get
			{
				return (NamespaceElementCollection)base["namespaces"];
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00006F90 File Offset: 0x00005190
		[ConfigurationProperty("assemblies")]
		public AssemblyElementCollection Assemblies
		{
			get
			{
				return (AssemblyElementCollection)base["assemblies"];
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00006FA2 File Offset: 0x000051A2
		public IUnityContainer Configure(IUnityContainer container)
		{
			return this.Configure(container, string.Empty);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00006FB0 File Offset: 0x000051B0
		public IUnityContainer Configure(IUnityContainer container, string configuredContainerName)
		{
			UnityConfigurationSection.currentSection = this;
			TypeResolver.SetAliases(this);
			ContainerElement containerElement = UnityConfigurationSection.GuardContainerExists(configuredContainerName, this.Containers[configuredContainerName]);
			containerElement.ConfigureContainer(container);
			return container;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00006FE4 File Offset: 0x000051E4
		private static ContainerElement GuardContainerExists(string configuredContainerName, ContainerElement containerElement)
		{
			if (containerElement == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.NoSuchContainer, new object[]
				{
					configuredContainerName
				}), "configuredContainerName");
			}
			return containerElement;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000701B File Offset: 0x0000521B
		protected override void DeserializeSection(XmlReader reader)
		{
			ExtensionElementMap.Clear();
			UnityConfigurationSection.currentSection = this;
			base.DeserializeSection(reader);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000702F File Offset: 0x0000522F
		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			return UnityConfigurationSection.UnknownElementHandlerMap.ProcessElement(this, elementName, reader) || base.OnDeserializeUnrecognizedElement(elementName, reader);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000704C File Offset: 0x0000524C
		private void DeserializeSectionExtension(XmlReader reader)
		{
			TypeResolver.SetAliases(this);
			SectionExtensionElement sectionExtensionElement = this.ReadUnwrappedElement(reader, this.SectionExtensions);
			sectionExtensionElement.ExtensionObject.AddExtensions(new UnityConfigurationSection.ExtensionContext(this, sectionExtensionElement.Prefix));
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00007084 File Offset: 0x00005284
		protected override string SerializeSection(ConfigurationElement parentElement, string name, ConfigurationSaveMode saveMode)
		{
			ExtensionElementMap.Clear();
			UnityConfigurationSection.currentSection = this;
			TypeResolver.SetAliases(this);
			this.InitializeSectionExtensions();
			StringBuilder stringBuilder = new StringBuilder();
			using (XmlWriter xmlWriter = UnityConfigurationSection.MakeXmlWriter(stringBuilder))
			{
				xmlWriter.WriteStartElement(name, "http://schemas.microsoft.com/practices/2010/unity");
				xmlWriter.WriteAttributeString("xmlns", "http://schemas.microsoft.com/practices/2010/unity");
				this.TypeAliases.SerializeElementContents(xmlWriter, "alias");
				this.Namespaces.SerializeElementContents(xmlWriter, "namespace");
				this.Assemblies.SerializeElementContents(xmlWriter, "assembly");
				this.SectionExtensions.SerializeElementContents(xmlWriter, "sectionExtension");
				this.Containers.SerializeElementContents(xmlWriter, "container");
				xmlWriter.WriteEndElement();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00007150 File Offset: 0x00005350
		private static XmlWriter MakeXmlWriter(StringBuilder sb)
		{
			XmlWriterSettings settings = new XmlWriterSettings
			{
				Indent = true,
				OmitXmlDeclaration = true,
				ConformanceLevel = ConformanceLevel.Fragment
			};
			return XmlWriter.Create(sb, settings);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00007184 File Offset: 0x00005384
		private void InitializeSectionExtensions()
		{
			foreach (SectionExtensionElement sectionExtensionElement in this.SectionExtensions)
			{
				SectionExtension extensionObject = sectionExtensionElement.ExtensionObject;
				extensionObject.AddExtensions(new UnityConfigurationSection.ExtensionContext(this, sectionExtensionElement.Prefix, false));
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000723C File Offset: 0x0000543C
		// Note: this type is marked as 'beforefieldinit'.
		static UnityConfigurationSection()
		{
			UnknownElementHandlerMap<UnityConfigurationSection> unknownElementHandlerMap = new UnknownElementHandlerMap<UnityConfigurationSection>();
			unknownElementHandlerMap.Add("typeAliases", delegate(UnityConfigurationSection s, XmlReader xr)
			{
				s.TypeAliases.Deserialize(xr);
			});
			unknownElementHandlerMap.Add("containers", delegate(UnityConfigurationSection s, XmlReader xr)
			{
				s.Containers.Deserialize(xr);
			});
			unknownElementHandlerMap.Add("alias", delegate(UnityConfigurationSection s, XmlReader xr)
			{
				s.ReadUnwrappedElement(xr, s.TypeAliases);
			});
			unknownElementHandlerMap.Add("sectionExtension", delegate(UnityConfigurationSection s, XmlReader xr)
			{
				s.DeserializeSectionExtension(xr);
			});
			unknownElementHandlerMap.Add("namespace", delegate(UnityConfigurationSection s, XmlReader xr)
			{
				s.ReadUnwrappedElement(xr, s.Namespaces);
			});
			unknownElementHandlerMap.Add("assembly", delegate(UnityConfigurationSection s, XmlReader xr)
			{
				s.ReadUnwrappedElement(xr, s.Assemblies);
			});
			UnityConfigurationSection.UnknownElementHandlerMap = unknownElementHandlerMap;
		}

		// Token: 0x04000072 RID: 114
		public const string SectionName = "unity";

		// Token: 0x04000073 RID: 115
		public const string XmlNamespace = "http://schemas.microsoft.com/practices/2010/unity";

		// Token: 0x04000074 RID: 116
		private const string ContainersPropertyName = "";

		// Token: 0x04000075 RID: 117
		private const string TypeAliasesPropertyName = "aliases";

		// Token: 0x04000076 RID: 118
		private const string SectionExtensionsPropertyName = "extensions";

		// Token: 0x04000077 RID: 119
		private const string NamespacesPropertyName = "namespaces";

		// Token: 0x04000078 RID: 120
		private const string AssembliesPropertyName = "assemblies";

		// Token: 0x04000079 RID: 121
		private const string XmlnsPropertyName = "xmlns";

		// Token: 0x0400007A RID: 122
		private static readonly UnknownElementHandlerMap<UnityConfigurationSection> UnknownElementHandlerMap;

		// Token: 0x0400007B RID: 123
		[ThreadStatic]
		private static UnityConfigurationSection currentSection;

		// Token: 0x0200003F RID: 63
		private class ExtensionContext : SectionExtensionContext
		{
			// Token: 0x0600020C RID: 524 RVA: 0x0000734D File Offset: 0x0000554D
			public ExtensionContext(UnityConfigurationSection section, string prefix) : this(section, prefix, true)
			{
			}

			// Token: 0x0600020D RID: 525 RVA: 0x00007358 File Offset: 0x00005558
			public ExtensionContext(UnityConfigurationSection section, string prefix, bool saveAliases)
			{
				this.section = section;
				this.prefix = prefix;
				this.saveAliases = saveAliases;
			}

			// Token: 0x0600020E RID: 526 RVA: 0x00007378 File Offset: 0x00005578
			public override void AddAlias(string newAlias, Type aliasedType)
			{
				if (!this.saveAliases)
				{
					return;
				}
				string text = newAlias;
				if (!string.IsNullOrEmpty(this.prefix))
				{
					text = this.prefix + "." + text;
				}
				this.section.TypeAliases.Add(new AliasElement(text, aliasedType));
			}

			// Token: 0x0600020F RID: 527 RVA: 0x000073C8 File Offset: 0x000055C8
			[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
			public override void AddElement(string tag, Type elementType)
			{
				Guard.ArgumentNotNull(elementType, "elementType");
				if (typeof(ContainerConfiguringElement).IsAssignableFrom(elementType))
				{
					ExtensionElementMap.AddContainerConfiguringElement(this.prefix, tag, elementType);
					return;
				}
				if (typeof(InjectionMemberElement).IsAssignableFrom(elementType))
				{
					ExtensionElementMap.AddInjectionMemberElement(this.prefix, tag, elementType);
					return;
				}
				if (typeof(ParameterValueElement).IsAssignableFrom(elementType))
				{
					ExtensionElementMap.AddParameterValueElement(this.prefix, tag, elementType);
					return;
				}
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.InvalidExtensionElementType, new object[]
				{
					elementType.Name
				}));
			}

			// Token: 0x04000082 RID: 130
			private readonly UnityConfigurationSection section;

			// Token: 0x04000083 RID: 131
			private readonly string prefix;

			// Token: 0x04000084 RID: 132
			private readonly bool saveAliases;
		}
	}
}
