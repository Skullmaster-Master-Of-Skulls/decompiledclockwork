using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x0200071C RID: 1820
	public sealed class NamespaceInfo : ConfigurationElement
	{
		// Token: 0x0600579D RID: 22429 RVA: 0x00133168 File Offset: 0x00131368
		static NamespaceInfo()
		{
			NamespaceInfo._properties = new ConfigurationPropertyCollection();
			NamespaceInfo._properties.Add(NamespaceInfo._propNamespace);
		}

		// Token: 0x0600579E RID: 22430 RVA: 0x00117E9E File Offset: 0x0011609E
		internal NamespaceInfo()
		{
		}

		// Token: 0x0600579F RID: 22431 RVA: 0x001331A4 File Offset: 0x001313A4
		public NamespaceInfo(string name) : this()
		{
			this.Namespace = name;
		}

		// Token: 0x060057A0 RID: 22432 RVA: 0x001331B4 File Offset: 0x001313B4
		public override bool Equals(object namespaceInformation)
		{
			NamespaceInfo namespaceInfo = namespaceInformation as NamespaceInfo;
			return namespaceInfo != null && this.Namespace == namespaceInfo.Namespace;
		}

		// Token: 0x060057A1 RID: 22433 RVA: 0x001331DE File Offset: 0x001313DE
		public override int GetHashCode()
		{
			return this.Namespace.GetHashCode();
		}

		// Token: 0x17001945 RID: 6469
		// (get) Token: 0x060057A2 RID: 22434 RVA: 0x001331EB File Offset: 0x001313EB
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return NamespaceInfo._properties;
			}
		}

		// Token: 0x17001946 RID: 6470
		// (get) Token: 0x060057A3 RID: 22435 RVA: 0x001331F2 File Offset: 0x001313F2
		// (set) Token: 0x060057A4 RID: 22436 RVA: 0x00133204 File Offset: 0x00131404
		[ConfigurationProperty("namespace", IsRequired = true, IsKey = true, DefaultValue = "")]
		[StringValidator(MinLength = 1)]
		public string Namespace
		{
			get
			{
				return (string)base[NamespaceInfo._propNamespace];
			}
			set
			{
				base[NamespaceInfo._propNamespace] = value;
			}
		}

		// Token: 0x04002E96 RID: 11926
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002E97 RID: 11927
		private static readonly ConfigurationProperty _propNamespace = new ConfigurationProperty("namespace", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
	}
}
