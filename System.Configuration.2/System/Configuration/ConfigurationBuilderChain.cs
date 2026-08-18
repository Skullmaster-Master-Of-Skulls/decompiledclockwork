using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x0200001E RID: 30
	internal class ConfigurationBuilderChain : ConfigurationBuilder
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00009369 File Offset: 0x00007569
		public List<ConfigurationBuilder> Builders
		{
			get
			{
				return this._builders;
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00009371 File Offset: 0x00007571
		public override void Initialize(string name, NameValueCollection config)
		{
			this._builders = new List<ConfigurationBuilder>();
			base.Initialize(name, config);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00009388 File Offset: 0x00007588
		public override XmlNode ProcessRawXml(XmlNode rawXml)
		{
			XmlNode xmlNode = rawXml;
			string text = null;
			XmlNode result;
			try
			{
				foreach (ConfigurationBuilder configurationBuilder in this._builders)
				{
					text = configurationBuilder.Name;
					xmlNode = configurationBuilder.ProcessRawXml(xmlNode);
				}
				result = xmlNode;
			}
			catch (Exception e)
			{
				throw ExceptionUtil.WrapAsConfigException(SR.GetString("ConfigBuilder_processXml_error_short", new object[]
				{
					text
				}), e, null);
			}
			return result;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000941C File Offset: 0x0000761C
		public override ConfigurationSection ProcessConfigurationSection(ConfigurationSection configSection)
		{
			ConfigurationSection configurationSection = configSection;
			string text = null;
			ConfigurationSection result;
			try
			{
				foreach (ConfigurationBuilder configurationBuilder in this._builders)
				{
					text = configurationBuilder.Name;
					configurationSection = configurationBuilder.ProcessConfigurationSection(configurationSection);
				}
				result = configurationSection;
			}
			catch (Exception e)
			{
				throw ExceptionUtil.WrapAsConfigException(SR.GetString("ConfigBuilder_processSection_error", new object[]
				{
					text,
					configSection.SectionInformation.Name
				}), e, null);
			}
			return result;
		}

		// Token: 0x04000186 RID: 390
		private List<ConfigurationBuilder> _builders;
	}
}
