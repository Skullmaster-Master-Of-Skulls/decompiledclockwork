using System;
using System.Configuration;
using System.Linq;
using System.Xml;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x0200001E RID: 30
	public class ContainerElement : DeserializableConfigurationElement
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00004B69 File Offset: 0x00002D69
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00004B71 File Offset: 0x00002D71
		internal UnityConfigurationSection ContainingSection { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00004B7A File Offset: 0x00002D7A
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00004B8C File Offset: 0x00002D8C
		[ConfigurationProperty("name", IsKey = true, DefaultValue = "")]
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

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00004B9A File Offset: 0x00002D9A
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public RegisterElementCollection Registrations
		{
			get
			{
				return (RegisterElementCollection)base[""];
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004BAC File Offset: 0x00002DAC
		[ConfigurationProperty("instances")]
		public InstanceElementCollection Instances
		{
			get
			{
				return (InstanceElementCollection)base["instances"];
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00004BBE File Offset: 0x00002DBE
		[ConfigurationProperty("extensions")]
		public ContainerExtensionElementCollection Extensions
		{
			get
			{
				return (ContainerExtensionElementCollection)base["extensions"];
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00004BD0 File Offset: 0x00002DD0
		public ContainerConfiguringElementCollection ConfiguringElements
		{
			get
			{
				return this.configuringElements;
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004BD8 File Offset: 0x00002DD8
		[Obsolete("Use the UnityConfigurationSection.Configure(container, name) method instead")]
		public void Configure(IUnityContainer container)
		{
			this.ContainingSection.Configure(container, this.Name);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004C04 File Offset: 0x00002E04
		internal void ConfigureContainer(IUnityContainer container)
		{
			this.Extensions.Cast<ContainerConfiguringElement>().Concat(this.Registrations.Cast<ContainerConfiguringElement>()).Concat(this.Instances.Cast<ContainerConfiguringElement>()).Concat(this.ConfiguringElements).ForEach(delegate(ContainerConfiguringElement element)
			{
				element.ConfigureContainerInternal(container);
			});
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004C65 File Offset: 0x00002E65
		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			return ContainerElement.UnknownElementHandlerMap.ProcessElement(this, elementName, reader) || this.DeserializeContainerConfiguringElement(elementName, reader) || base.OnDeserializeUnrecognizedElement(elementName, reader);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004C8C File Offset: 0x00002E8C
		public override void SerializeContent(XmlWriter writer)
		{
			writer.WriteAttributeIfNotEmpty("name", this.Name);
			this.Extensions.SerializeElementContents(writer, "extension");
			this.Registrations.SerializeElementContents(writer, "register");
			this.Instances.SerializeElementContents(writer, "instance");
			this.SerializeContainerConfiguringElements(writer);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004CE8 File Offset: 0x00002EE8
		private bool DeserializeContainerConfiguringElement(string elementName, XmlReader reader)
		{
			Type containerConfiguringElementType = ExtensionElementMap.GetContainerConfiguringElementType(elementName);
			if (containerConfiguringElementType != null)
			{
				this.ReadElementByType(reader, containerConfiguringElementType, this.ConfiguringElements);
				return true;
			}
			return false;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004D18 File Offset: 0x00002F18
		private void SerializeContainerConfiguringElements(XmlWriter writer)
		{
			foreach (ContainerConfiguringElement containerConfiguringElement in this.ConfiguringElements)
			{
				string tagForExtensionElement = ExtensionElementMap.GetTagForExtensionElement(containerConfiguringElement);
				writer.WriteElement(tagForExtensionElement, new Action<XmlWriter>(containerConfiguringElement.SerializeContent));
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004DAC File Offset: 0x00002FAC
		// Note: this type is marked as 'beforefieldinit'.
		static ContainerElement()
		{
			UnknownElementHandlerMap<ContainerElement> unknownElementHandlerMap = new UnknownElementHandlerMap<ContainerElement>();
			unknownElementHandlerMap.Add("types", delegate(ContainerElement ce, XmlReader xr)
			{
				ce.Registrations.Deserialize(xr);
			});
			unknownElementHandlerMap.Add("extension", delegate(ContainerElement ce, XmlReader xr)
			{
				ce.ReadUnwrappedElement(xr, ce.Extensions);
			});
			unknownElementHandlerMap.Add("instance", delegate(ContainerElement ce, XmlReader xr)
			{
				ce.ReadUnwrappedElement(xr, ce.Instances);
			});
			ContainerElement.UnknownElementHandlerMap = unknownElementHandlerMap;
		}

		// Token: 0x04000033 RID: 51
		private const string RegistrationsPropertyName = "";

		// Token: 0x04000034 RID: 52
		private const string NamePropertyName = "name";

		// Token: 0x04000035 RID: 53
		private const string InstancesPropertyName = "instances";

		// Token: 0x04000036 RID: 54
		private const string ExtensionsPropertyName = "extensions";

		// Token: 0x04000037 RID: 55
		private static readonly UnknownElementHandlerMap<ContainerElement> UnknownElementHandlerMap;

		// Token: 0x04000038 RID: 56
		private readonly ContainerConfiguringElementCollection configuringElements = new ContainerConfiguringElementCollection();
	}
}
