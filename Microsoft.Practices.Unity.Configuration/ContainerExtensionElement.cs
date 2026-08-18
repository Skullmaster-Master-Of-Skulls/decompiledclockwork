using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x0200001F RID: 31
	public class ContainerExtensionElement : ContainerConfiguringElement
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00004E50 File Offset: 0x00003050
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00004E62 File Offset: 0x00003062
		[ConfigurationProperty("type", IsRequired = true)]
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

		// Token: 0x060000FD RID: 253 RVA: 0x00004E70 File Offset: 0x00003070
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		protected override void ConfigureContainer(IUnityContainer container)
		{
			Guard.ArgumentNotNull(container, "container");
			Type extensionType = this.GetExtensionType();
			UnityContainerExtension extension = (UnityContainerExtension)container.Resolve(extensionType, new ResolverOverride[0]);
			container.AddExtension(extension);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004EAA File Offset: 0x000030AA
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public override void SerializeContent(XmlWriter writer)
		{
			Guard.ArgumentNotNull(writer, "writer");
			writer.WriteAttributeString("type", this.TypeName);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004EC8 File Offset: 0x000030C8
		private Type GetExtensionType()
		{
			return TypeResolver.ResolveType(this.TypeName, true);
		}

		// Token: 0x0400003D RID: 61
		private const string TypeNamePropertyName = "type";
	}
}
