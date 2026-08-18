using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x02000003 RID: 3
	public class AliasElement : DeserializableConfigurationElement
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000020E4 File Offset: 0x000002E4
		public AliasElement()
		{
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020EC File Offset: 0x000002EC
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public AliasElement(string alias, Type targetType)
		{
			Guard.ArgumentNotNull(targetType, "targetType");
			this.Alias = alias;
			this.TypeName = targetType.AssemblyQualifiedName;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002112 File Offset: 0x00000312
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002124 File Offset: 0x00000324
		[ConfigurationProperty("alias", IsRequired = true, IsKey = true)]
		public string Alias
		{
			get
			{
				return (string)base["alias"];
			}
			set
			{
				base["alias"] = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002132 File Offset: 0x00000332
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002144 File Offset: 0x00000344
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

		// Token: 0x0600000A RID: 10 RVA: 0x00002152 File Offset: 0x00000352
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public override void SerializeContent(XmlWriter writer)
		{
			Guard.ArgumentNotNull(writer, "writer");
			writer.WriteAttributeString("alias", this.Alias);
			writer.WriteAttributeString("type", this.TypeName);
		}

		// Token: 0x04000001 RID: 1
		private const string AliasPropertyName = "alias";

		// Token: 0x04000002 RID: 2
		private const string TypeNamePropertyName = "type";
	}
}
