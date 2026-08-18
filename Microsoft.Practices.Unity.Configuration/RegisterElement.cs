using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x02000036 RID: 54
	public class RegisterElement : ContainerConfiguringElement
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00006214 File Offset: 0x00004414
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00006226 File Offset: 0x00004426
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
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00006234 File Offset: 0x00004434
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x00006246 File Offset: 0x00004446
		[ConfigurationProperty("name", DefaultValue = "", IsRequired = false, IsKey = true)]
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

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00006254 File Offset: 0x00004454
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00006266 File Offset: 0x00004466
		[ConfigurationProperty("mapTo", DefaultValue = "", IsRequired = false)]
		public string MapToName
		{
			get
			{
				return (string)base["mapTo"];
			}
			set
			{
				base["mapTo"] = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00006274 File Offset: 0x00004474
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00006286 File Offset: 0x00004486
		[ConfigurationProperty("lifetime", IsRequired = false)]
		public LifetimeElement Lifetime
		{
			get
			{
				return (LifetimeElement)base["lifetime"];
			}
			set
			{
				base["lifetime"] = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00006294 File Offset: 0x00004494
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public InjectionMemberElementCollection InjectionMembers
		{
			get
			{
				return (InjectionMemberElementCollection)base[""];
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000062D4 File Offset: 0x000044D4
		protected override void ConfigureContainer(IUnityContainer container)
		{
			Type registeringType = this.GetRegisteringType();
			Type mappedType = this.GetMappedType();
			LifetimeManager lifetimeManager = this.Lifetime.CreateLifetimeManager();
			IEnumerable<InjectionMember> source = this.InjectionMembers.SelectMany((InjectionMemberElement m) => m.GetInjectionMembers(container, registeringType, mappedType, this.Name));
			container.RegisterType(registeringType, mappedType, this.Name, lifetimeManager, source.ToArray<InjectionMember>());
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00006358 File Offset: 0x00004558
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public override void SerializeContent(XmlWriter writer)
		{
			Guard.ArgumentNotNull(writer, "writer");
			writer.WriteAttributeString("type", this.TypeName);
			writer.WriteAttributeIfNotEmpty("mapTo", this.MapToName).WriteAttributeIfNotEmpty("name", this.Name);
			if (!string.IsNullOrEmpty(this.Lifetime.TypeName))
			{
				writer.WriteElement("lifetime", new Action<XmlWriter>(this.Lifetime.SerializeContent));
			}
			this.SerializeInjectionMembers(writer);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000063DA File Offset: 0x000045DA
		private Type GetRegisteringType()
		{
			if (!string.IsNullOrEmpty(this.MapToName))
			{
				return TypeResolver.ResolveType(this.TypeName);
			}
			return null;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000063F6 File Offset: 0x000045F6
		private Type GetMappedType()
		{
			if (string.IsNullOrEmpty(this.MapToName))
			{
				return TypeResolver.ResolveType(this.TypeName);
			}
			return TypeResolver.ResolveType(this.MapToName);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000641C File Offset: 0x0000461C
		private void SerializeInjectionMembers(XmlWriter writer)
		{
			foreach (InjectionMemberElement injectionMemberElement in this.InjectionMembers)
			{
				writer.WriteElement(injectionMemberElement.ElementName, new Action<XmlWriter>(injectionMemberElement.SerializeContent));
			}
		}

		// Token: 0x04000060 RID: 96
		private const string InjectionMembersPropertyName = "";

		// Token: 0x04000061 RID: 97
		private const string LifetimePropertyName = "lifetime";

		// Token: 0x04000062 RID: 98
		private const string MapToPropertyName = "mapTo";

		// Token: 0x04000063 RID: 99
		private const string NamePropertyName = "name";

		// Token: 0x04000064 RID: 100
		private const string TypePropertyName = "type";
	}
}
