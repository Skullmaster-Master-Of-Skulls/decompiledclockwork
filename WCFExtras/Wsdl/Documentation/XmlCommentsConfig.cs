using System;
using System.Configuration;
using System.IO;

namespace WCFExtras.Wsdl.Documentation
{
	// Token: 0x02000019 RID: 25
	internal class XmlCommentsConfig : ConfigurationSection
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00004ABC File Offset: 0x00002CBC
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00004ADE File Offset: 0x00002CDE
		[ConfigurationProperty("format", DefaultValue = XmlCommentFormat.Default)]
		public XmlCommentFormat Format
		{
			get
			{
				return (XmlCommentFormat)base["format"];
			}
			set
			{
				base["format"] = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00004AF4 File Offset: 0x00002CF4
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00004B16 File Offset: 0x00002D16
		[ConfigurationProperty("wrapLongLines", DefaultValue = false)]
		public bool WrapLongLines
		{
			get
			{
				return (bool)base["wrapLongLines"];
			}
			set
			{
				base["wrapLongLines"] = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004B2C File Offset: 0x00002D2C
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00004B4E File Offset: 0x00002D4E
		[ConfigurationProperty("documentable", DefaultValue = false)]
		public bool Documentable
		{
			get
			{
				return (bool)base["documentable"];
			}
			set
			{
				base["documentable"] = value;
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004B64 File Offset: 0x00002D64
		public static XmlCommentsConfig GetConfiguration()
		{
			return XmlCommentsConfig.GetConfiguration(null);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004B7C File Offset: 0x00002D7C
		public static XmlCommentsConfig GetConfiguration(Configuration configuration)
		{
			XmlCommentsConfig xmlCommentsConfig = null;
			if (configuration != null)
			{
				xmlCommentsConfig = (configuration.GetSection("xmlComments") as XmlCommentsConfig);
			}
			if (xmlCommentsConfig == null)
			{
				xmlCommentsConfig = (ConfigurationManager.GetSection("xmlComments") as XmlCommentsConfig);
				if (xmlCommentsConfig == null)
				{
					string configFileFromCommandLine = XmlCommentsConfig.GetConfigFileFromCommandLine();
					if (configFileFromCommandLine != null && File.Exists(configFileFromCommandLine))
					{
						Configuration configuration2 = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap
						{
							ExeConfigFilename = configFileFromCommandLine
						}, ConfigurationUserLevel.None);
						if (configuration2 != null)
						{
							xmlCommentsConfig = (configuration2.GetSection("xmlComments") as XmlCommentsConfig);
						}
					}
				}
			}
			return xmlCommentsConfig;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004C2C File Offset: 0x00002E2C
		private static string GetConfigFileFromCommandLine()
		{
			foreach (string text in Environment.GetCommandLineArgs())
			{
				string[] array = text.Split(new char[]
				{
					':',
					'='
				}, 2);
				if (array.Length == 2 && array[0].IndexOf("svcutilConfig", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					return array[1];
				}
			}
			return null;
		}
	}
}
