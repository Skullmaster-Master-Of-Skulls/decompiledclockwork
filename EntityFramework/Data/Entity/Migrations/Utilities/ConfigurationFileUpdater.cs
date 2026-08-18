using System;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace System.Data.Entity.Migrations.Utilities
{
	// Token: 0x02000716 RID: 1814
	internal class ConfigurationFileUpdater
	{
		// Token: 0x06004970 RID: 18800 RVA: 0x0015F5A4 File Offset: 0x0015D7A4
		[SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
		static ConfigurationFileUpdater()
		{
			AssemblyName name = typeof(ConfigurationFileUpdater).Assembly().GetName();
			ConfigurationFileUpdater._dependentAssemblyElement = new XElement(ConfigurationFileUpdater._asm + "dependentAssembly", new object[]
			{
				new XElement(ConfigurationFileUpdater._asm + "assemblyIdentity", new object[]
				{
					new XAttribute("name", "EntityFramework"),
					new XAttribute("culture", "neutral"),
					new XAttribute("publicKeyToken", "b77a5c561934e089")
				}),
				new XElement(ConfigurationFileUpdater._asm + "codeBase", new object[]
				{
					new XAttribute("version", name.Version.ToString()),
					new XAttribute("href", name.CodeBase)
				})
			});
		}

		// Token: 0x06004971 RID: 18801 RVA: 0x0015F6B0 File Offset: 0x0015D8B0
		public virtual string Update(string configurationFile)
		{
			bool flag = !string.IsNullOrWhiteSpace(configurationFile) && File.Exists(configurationFile);
			XDocument xdocument = flag ? XDocument.Load(configurationFile) : new XDocument();
			xdocument.GetOrAddElement("configuration").GetOrAddElement("runtime").GetOrAddElement(ConfigurationFileUpdater._asm + "assemblyBinding").Add(ConfigurationFileUpdater._dependentAssemblyElement);
			string text = Path.GetTempFileName();
			if (flag)
			{
				File.Delete(text);
				text = Path.Combine(Path.GetDirectoryName(configurationFile), Path.GetFileName(text));
			}
			xdocument.Save(text);
			return text;
		}

		// Token: 0x04001B4B RID: 6987
		private static readonly XNamespace _asm = "urn:schemas-microsoft-com:asm.v1";

		// Token: 0x04001B4C RID: 6988
		private static readonly XElement _dependentAssemblyElement;
	}
}
