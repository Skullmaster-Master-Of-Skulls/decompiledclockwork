using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x02000009 RID: 9
	public abstract class NamedElement : DeserializableConfigurationElement
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002644 File Offset: 0x00000844
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002656 File Offset: 0x00000856
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

		// Token: 0x0600002D RID: 45 RVA: 0x00002664 File Offset: 0x00000864
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public override void SerializeContent(XmlWriter writer)
		{
			Guard.ArgumentNotNull(writer, "writer");
			writer.WriteAttributeString("name", this.Name);
		}

		// Token: 0x04000009 RID: 9
		private const string NamePropertyName = "name";
	}
}
