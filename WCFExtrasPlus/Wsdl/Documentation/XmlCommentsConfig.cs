using System;
using System.Configuration;
using System.IO;

namespace WCFExtrasPlus.Wsdl.Documentation
{
	// Token: 0x0200001C RID: 28
	internal class XmlCommentsConfig : ConfigurationSection
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00004A51 File Offset: 0x00002C51
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00004A63 File Offset: 0x00002C63
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

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00004A76 File Offset: 0x00002C76
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00004A88 File Offset: 0x00002C88
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

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00004A9B File Offset: 0x00002C9B
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00004AAD File Offset: 0x00002CAD
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

		// Token: 0x060000A5 RID: 165 RVA: 0x00004AC0 File Offset: 0x00002CC0
		public static XmlCommentsConfig GetConfiguration()
		{
			return XmlCommentsConfig.GetConfiguration(null);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004AC8 File Offset: 0x00002CC8
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

		// Token: 0x060000A7 RID: 167 RVA: 0x00004B3C File Offset: 0x00002D3C
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
