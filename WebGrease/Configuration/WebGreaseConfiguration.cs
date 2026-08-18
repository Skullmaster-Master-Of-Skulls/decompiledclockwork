using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using WebGrease.Extensions;

namespace WebGrease.Configuration
{
	// Token: 0x020000F9 RID: 249
	public class WebGreaseConfiguration
	{
		// Token: 0x06000FDC RID: 4060 RVA: 0x000481F4 File Offset: 0x000463F4
		internal WebGreaseConfiguration()
		{
			this.global = new Dictionary<string, GlobalConfig>();
			this.Global = new GlobalConfig();
			this.ImageExtensions = new List<string>();
			this.ImageDirectories = new List<string>();
			this.ImageDirectoriesToHash = new List<string>();
			this.CssFileSets = new List<CssFileSet>();
			this.JSFileSets = new List<JSFileSet>();
			this.DefaultDpi = new Dictionary<string, HashSet<float>>(StringComparer.OrdinalIgnoreCase);
			this.DefaultPreprocessing = new Dictionary<string, PreprocessingConfig>();
			this.DefaultJSMinification = new Dictionary<string, JsMinificationConfig>();
			this.DefaultSpriting = new Dictionary<string, CssSpritingConfig>();
			this.DefaultCssMinification = new Dictionary<string, CssMinificationConfig>();
			this.DefaultBundling = new Dictionary<string, BundlingConfig>();
			this.DefaultCssResourcePivots = new ResourcePivotGroupCollection();
			this.DefaultJsResourcePivots = new ResourcePivotGroupCollection();
			this.LoadedConfigurationFiles = new List<string>();
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x000482C7 File Offset: 0x000464C7
		internal WebGreaseConfiguration(string configType, string preprocessingPluginPath = null) : this()
		{
			this.ConfigType = configType;
			this.PreprocessingPluginPath = preprocessingPluginPath;
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x000482E0 File Offset: 0x000464E0
		internal WebGreaseConfiguration(WebGreaseConfiguration configuration, FileInfo configurationFile) : this(configurationFile, configuration.ConfigType, configuration.SourceDirectory, configuration.DestinationDirectory, configuration.LogsDirectory, configuration.ToolsTempDirectory, configuration.ApplicationRootDirectory, configuration.PreprocessingPluginPath)
		{
			this.CacheEnabled = configuration.CacheEnabled;
			this.CacheRootPath = configuration.CacheRootPath;
			this.CacheTimeout = configuration.CacheTimeout;
			this.CacheUniqueKey = configuration.CacheUniqueKey;
			this.Measure = configuration.Measure;
			this.Overrides = configuration.Overrides;
			this.ReportPath = configuration.ReportPath;
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x00048374 File Offset: 0x00046574
		internal WebGreaseConfiguration(WebGreaseConfiguration configuration) : this(configuration.ConfigType, configuration.SourceDirectory, configuration.DestinationDirectory, configuration.LogsDirectory, configuration.ToolsTempDirectory, configuration.ApplicationRootDirectory, configuration.PreprocessingPluginPath)
		{
			this.CacheEnabled = configuration.CacheEnabled;
			this.CacheRootPath = configuration.CacheRootPath;
			this.CacheTimeout = configuration.CacheTimeout;
			this.CacheUniqueKey = configuration.CacheUniqueKey;
			this.Measure = configuration.Measure;
			this.Overrides = configuration.Overrides;
			this.ReportPath = configuration.ReportPath;
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x00048405 File Offset: 0x00046605
		internal WebGreaseConfiguration(FileInfo configurationFile, string configType, string sourceDirectory, string destinationDirectory, string logsDirectory, string toolsTempDirectory = null, string appRootDirectory = null, string preprocessingPluginPath = null) : this(configType, sourceDirectory, destinationDirectory, logsDirectory, toolsTempDirectory, appRootDirectory, preprocessingPluginPath)
		{
			if (configurationFile == null)
			{
				throw new ArgumentNullException("configType");
			}
			this.Parse(configurationFile.FullName);
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x00048434 File Offset: 0x00046634
		internal WebGreaseConfiguration(string configType, string sourceDirectory, string destinationDirectory, string logsDirectory, string toolsTempDirectory, string appRootDirectory = null, string preprocessingPluginPath = null) : this(configType, preprocessingPluginPath)
		{
			this.SourceDirectory = sourceDirectory;
			this.DestinationDirectory = destinationDirectory;
			this.LogsDirectory = logsDirectory;
			this.ToolsTempDirectory = toolsTempDirectory;
			this.ApplicationRootDirectory = (appRootDirectory ?? Environment.CurrentDirectory);
			this.IntermediateErrorDirectory = Path.Combine(this.ApplicationRootDirectory, "IntermediateErrorFiles");
			if (!string.IsNullOrWhiteSpace(destinationDirectory))
			{
				Directory.CreateDirectory(destinationDirectory);
			}
			if (!string.IsNullOrWhiteSpace(logsDirectory))
			{
				Directory.CreateDirectory(logsDirectory);
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x000484AF File Offset: 0x000466AF
		// (set) Token: 0x06000FE3 RID: 4067 RVA: 0x000484B7 File Offset: 0x000466B7
		public string SourceDirectory { get; set; }

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x000484D0 File Offset: 0x000466D0
		internal IEnumerable<string> AllLoadedConfigurationFiles
		{
			get
			{
				return this.LoadedConfigurationFiles.Concat(this.CssFileSets.SelectMany((CssFileSet cfs) => cfs.LoadedConfigurationFiles).Concat(this.JSFileSets.SelectMany((JSFileSet cfs) => cfs.LoadedConfigurationFiles))).Distinct<string>();
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x00048542 File Offset: 0x00046742
		// (set) Token: 0x06000FE6 RID: 4070 RVA: 0x0004854A File Offset: 0x0004674A
		internal GlobalConfig Global { get; private set; }

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x00048553 File Offset: 0x00046753
		// (set) Token: 0x06000FE8 RID: 4072 RVA: 0x0004855B File Offset: 0x0004675B
		internal string ConfigType { get; private set; }

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x00048564 File Offset: 0x00046764
		// (set) Token: 0x06000FEA RID: 4074 RVA: 0x0004856C File Offset: 0x0004676C
		internal string DestinationDirectory { get; set; }

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000FEB RID: 4075 RVA: 0x00048575 File Offset: 0x00046775
		// (set) Token: 0x06000FEC RID: 4076 RVA: 0x0004857D File Offset: 0x0004677D
		internal string TokensDirectory { get; set; }

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x00048586 File Offset: 0x00046786
		// (set) Token: 0x06000FEE RID: 4078 RVA: 0x0004858E File Offset: 0x0004678E
		internal string OverrideTokensDirectory { get; private set; }

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000FEF RID: 4079 RVA: 0x00048597 File Offset: 0x00046797
		// (set) Token: 0x06000FF0 RID: 4080 RVA: 0x0004859F File Offset: 0x0004679F
		internal string ApplicationRootDirectory { get; private set; }

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x000485A8 File Offset: 0x000467A8
		// (set) Token: 0x06000FF2 RID: 4082 RVA: 0x000485B0 File Offset: 0x000467B0
		internal string LogsDirectory { get; set; }

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x000485B9 File Offset: 0x000467B9
		// (set) Token: 0x06000FF4 RID: 4084 RVA: 0x000485C1 File Offset: 0x000467C1
		internal string ReportPath { get; set; }

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x000485CA File Offset: 0x000467CA
		// (set) Token: 0x06000FF6 RID: 4086 RVA: 0x000485D2 File Offset: 0x000467D2
		internal string ToolsTempDirectory { get; private set; }

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x000485DB File Offset: 0x000467DB
		// (set) Token: 0x06000FF8 RID: 4088 RVA: 0x000485E3 File Offset: 0x000467E3
		internal string PreprocessingPluginPath { get; private set; }

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x000485EC File Offset: 0x000467EC
		// (set) Token: 0x06000FFA RID: 4090 RVA: 0x000485F4 File Offset: 0x000467F4
		internal IList<string> ImageDirectories { get; private set; }

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000FFB RID: 4091 RVA: 0x000485FD File Offset: 0x000467FD
		// (set) Token: 0x06000FFC RID: 4092 RVA: 0x00048605 File Offset: 0x00046805
		internal IList<string> ImageDirectoriesToHash { get; private set; }

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x0004860E File Offset: 0x0004680E
		// (set) Token: 0x06000FFE RID: 4094 RVA: 0x00048616 File Offset: 0x00046816
		internal IList<string> ImageExtensions { get; set; }

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000FFF RID: 4095 RVA: 0x0004861F File Offset: 0x0004681F
		// (set) Token: 0x06001000 RID: 4096 RVA: 0x00048627 File Offset: 0x00046827
		internal IList<CssFileSet> CssFileSets { get; private set; }

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x00048630 File Offset: 0x00046830
		// (set) Token: 0x06001002 RID: 4098 RVA: 0x00048638 File Offset: 0x00046838
		internal IList<JSFileSet> JSFileSets { get; private set; }

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x00048641 File Offset: 0x00046841
		// (set) Token: 0x06001004 RID: 4100 RVA: 0x00048649 File Offset: 0x00046849
		internal IList<string> LoadedConfigurationFiles { get; private set; }

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001005 RID: 4101 RVA: 0x00048652 File Offset: 0x00046852
		// (set) Token: 0x06001006 RID: 4102 RVA: 0x0004865A File Offset: 0x0004685A
		internal bool Measure { get; set; }

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001007 RID: 4103 RVA: 0x00048663 File Offset: 0x00046863
		// (set) Token: 0x06001008 RID: 4104 RVA: 0x0004866B File Offset: 0x0004686B
		internal string DefaultOutputPathFormat { get; set; }

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x00048674 File Offset: 0x00046874
		// (set) Token: 0x0600100A RID: 4106 RVA: 0x0004867C File Offset: 0x0004687C
		internal bool CacheEnabled { get; set; }

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x00048685 File Offset: 0x00046885
		// (set) Token: 0x0600100C RID: 4108 RVA: 0x0004868D File Offset: 0x0004688D
		internal string CacheRootPath { get; set; }

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x00048696 File Offset: 0x00046896
		// (set) Token: 0x0600100E RID: 4110 RVA: 0x0004869E File Offset: 0x0004689E
		internal string CacheUniqueKey { get; set; }

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x000486A7 File Offset: 0x000468A7
		// (set) Token: 0x06001010 RID: 4112 RVA: 0x000486AF File Offset: 0x000468AF
		internal TimeSpan CacheTimeout { get; set; }

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001011 RID: 4113 RVA: 0x000486B8 File Offset: 0x000468B8
		// (set) Token: 0x06001012 RID: 4114 RVA: 0x000486C0 File Offset: 0x000468C0
		internal string IntermediateErrorDirectory { get; set; }

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001013 RID: 4115 RVA: 0x000486C9 File Offset: 0x000468C9
		// (set) Token: 0x06001014 RID: 4116 RVA: 0x000486D1 File Offset: 0x000468D1
		internal IDictionary<string, HashSet<float>> DefaultDpi { get; set; }

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x000486DA File Offset: 0x000468DA
		// (set) Token: 0x06001016 RID: 4118 RVA: 0x000486E2 File Offset: 0x000468E2
		internal TemporaryOverrides Overrides { get; set; }

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001017 RID: 4119 RVA: 0x000486EB File Offset: 0x000468EB
		// (set) Token: 0x06001018 RID: 4120 RVA: 0x000486F3 File Offset: 0x000468F3
		internal ResourcePivotGroupCollection DefaultCssResourcePivots { get; set; }

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x000486FC File Offset: 0x000468FC
		// (set) Token: 0x0600101A RID: 4122 RVA: 0x00048704 File Offset: 0x00046904
		internal ResourcePivotGroupCollection DefaultJsResourcePivots { get; set; }

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x0004870D File Offset: 0x0004690D
		// (set) Token: 0x0600101C RID: 4124 RVA: 0x00048715 File Offset: 0x00046915
		private IDictionary<string, JsMinificationConfig> DefaultJSMinification { get; set; }

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x0600101D RID: 4125 RVA: 0x0004871E File Offset: 0x0004691E
		// (set) Token: 0x0600101E RID: 4126 RVA: 0x00048726 File Offset: 0x00046926
		private IDictionary<string, CssMinificationConfig> DefaultCssMinification { get; set; }

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x0600101F RID: 4127 RVA: 0x0004872F File Offset: 0x0004692F
		// (set) Token: 0x06001020 RID: 4128 RVA: 0x00048737 File Offset: 0x00046937
		private IDictionary<string, BundlingConfig> DefaultBundling { get; set; }

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x00048740 File Offset: 0x00046940
		// (set) Token: 0x06001022 RID: 4130 RVA: 0x00048748 File Offset: 0x00046948
		private IDictionary<string, CssSpritingConfig> DefaultSpriting { get; set; }

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001023 RID: 4131 RVA: 0x00048751 File Offset: 0x00046951
		// (set) Token: 0x06001024 RID: 4132 RVA: 0x00048759 File Offset: 0x00046959
		private IDictionary<string, PreprocessingConfig> DefaultPreprocessing { get; set; }

		// Token: 0x06001025 RID: 4133 RVA: 0x00048764 File Offset: 0x00046964
		internal static void AddSeperatedValues(IList<string> list, string seperatedValues, Func<string, string> action = null)
		{
			if (!string.IsNullOrWhiteSpace(seperatedValues))
			{
				foreach (string text in seperatedValues.SafeSplitSemiColonSeperatedValue())
				{
					string text2 = text.Trim();
					list.Add((action != null) ? action(text2) : text2);
				}
			}
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x000487E0 File Offset: 0x000469E0
		internal static void ForEachConfigSourceElement(XElement parentElement, string parentFilePath, Action<XElement, string> configSourceAction)
		{
			List<string> list = (from e in parentElement.Elements("ConfigSource")
			select (string)e).ToList<string>();
			list.Add((string)parentElement.Attribute("configSource"));
			foreach (string text in from cs in list
			where !cs.IsNullOrWhitespace()
			select cs)
			{
				string fullPath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(parentFilePath), text));
				if (!File.Exists(fullPath))
				{
					throw new ConfigurationErrorsException("Configuration file not found: {0}, referenced in : {1}".InvariantFormat(new object[]
					{
						fullPath,
						parentFilePath
					}));
				}
				try
				{
					configSourceAction(XDocument.Load(fullPath).Root, fullPath);
				}
				catch (Exception inner)
				{
					throw new ConfigurationErrorsException("Could not load configuration file: {0}, references in {1}".InvariantFormat(new object[]
					{
						text,
						parentFilePath
					}), inner);
				}
			}
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x00048928 File Offset: 0x00046B28
		internal void Validate()
		{
			this.ApplicationRootDirectory = WebGreaseConfiguration.EnsureAndExpandDirectory(this.ApplicationRootDirectory, false);
			this.DestinationDirectory = WebGreaseConfiguration.EnsureAndExpandDirectory(this.DestinationDirectory, false);
			this.SourceDirectory = WebGreaseConfiguration.EnsureAndExpandDirectory(this.SourceDirectory, false);
			this.PreprocessingPluginPath = WebGreaseConfiguration.EnsureAndExpandDirectory(this.PreprocessingPluginPath, false);
			this.LogsDirectory = WebGreaseConfiguration.EnsureAndExpandDirectory(this.LogsDirectory, true);
			this.CacheRootPath = WebGreaseConfiguration.EnsureAndExpandDirectory(this.CacheRootPath, true);
			this.ToolsTempDirectory = WebGreaseConfiguration.EnsureAndExpandDirectory(this.ToolsTempDirectory, true);
			this.ReportPath = WebGreaseConfiguration.EnsureAndExpandDirectory(this.ReportPath ?? this.LogsDirectory, true);
			if (this.CacheTimeout > TimeSpan.Zero && this.CacheTimeout < WebGreaseConfiguration.MinimumCacheTimeout)
			{
				this.CacheTimeout = WebGreaseConfiguration.MinimumCacheTimeout;
			}
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x00048A1C File Offset: 0x00046C1C
		private static string EnsureAndExpandDirectory(string directory, bool allowCreate)
		{
			if (!string.IsNullOrWhiteSpace(directory))
			{
				directory = WebGreaseConfiguration.EnvironmentVariablesMatchPattern.Replace(directory, (Match match) => Environment.GetEnvironmentVariable(match.Groups["name"].Value));
				DirectoryInfo directoryInfo = new DirectoryInfo(directory);
				if (!directoryInfo.Exists)
				{
					if (!allowCreate)
					{
						throw new DirectoryNotFoundException(directory);
					}
					directoryInfo.Create();
				}
				return directoryInfo.FullName;
			}
			return null;
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x00048A84 File Offset: 0x00046C84
		private void Parse(string configurationFile)
		{
			XElement element = XElement.Load(configurationFile);
			this.Parse(element, configurationFile);
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x00048AA0 File Offset: 0x00046CA0
		private void Parse(XElement element, string configurationFile)
		{
			this.ParseSettings(element.Descendants("Settings"), configurationFile);
			this.Global = this.global.GetNamedConfig(this.ConfigType);
			foreach (XElement cssFileSetElement in element.Descendants("CssFileSet"))
			{
				this.CssFileSets.Add(new CssFileSet(cssFileSetElement, this.SourceDirectory, this.DefaultCssMinification, this.DefaultSpriting, this.DefaultPreprocessing, this.DefaultBundling, this.DefaultCssResourcePivots, this.Global, this.DefaultOutputPathFormat, this.DefaultDpi, configurationFile));
			}
			foreach (XElement jsFileSetElement in element.Descendants("JsFileSet"))
			{
				this.JSFileSets.Add(new JSFileSet(jsFileSetElement, this.SourceDirectory, this.DefaultJSMinification, this.DefaultPreprocessing, this.DefaultBundling, this.DefaultJsResourcePivots, this.Global, this.DefaultOutputPathFormat, configurationFile));
			}
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x00048BF0 File Offset: 0x00046DF0
		private void ParseSettings(IEnumerable<XElement> settingsElements, string configurationFile)
		{
			foreach (XElement settingsElement in from e in settingsElements
			where e != null
			select e)
			{
				this.ParseSettings(settingsElement, configurationFile);
			}
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x00048CDC File Offset: 0x00046EDC
		private void ParseSettings(XElement settingsElement, string configurationFile)
		{
			if (settingsElement == null)
			{
				throw new ArgumentNullException("settingsElement");
			}
			WebGreaseConfiguration.ForEachConfigSourceElement(settingsElement, configurationFile, delegate(XElement element, string s)
			{
				this.ParseSettings(element, s);
				this.LoadedConfigurationFiles.Add(s);
			});
			foreach (XElement xelement in settingsElement.Descendants())
			{
				string text = xelement.Name.ToString();
				string value3 = xelement.Value;
				string key;
				switch (key = text)
				{
				case "ImageDirectories":
					WebGreaseConfiguration.AddSeperatedValues(this.ImageDirectories, value3, (string value) => Path.GetFullPath(Path.Combine(this.SourceDirectory, value)));
					break;
				case "ImageDirectoriesToHash":
					WebGreaseConfiguration.AddSeperatedValues(this.ImageDirectoriesToHash, value3, (string value) => Path.GetFullPath(Path.Combine(this.SourceDirectory, value)));
					break;
				case "ImageExtensions":
					WebGreaseConfiguration.AddSeperatedValues(this.ImageExtensions, value3, null);
					break;
				case "Dpi":
				{
					IEnumerable<float> collection = from d in value3.NullSafeAction(new Func<string, IEnumerable<string>>(StringExtensions.SafeSplitSemiColonSeperatedValue))
					select d.TryParseFloat() into d
					where d != null
					select d.Value;
					string value2 = (string)xelement.Attribute("output");
					this.DefaultDpi[value2.AsNullIfWhiteSpace() ?? string.Empty] = new HashSet<float>(collection);
					break;
				}
				case "TokensDirectory":
					this.TokensDirectory = value3;
					break;
				case "OutputPathFormat":
					this.DefaultOutputPathFormat = value3;
					break;
				case "OverrideTokensDirectory":
					this.OverrideTokensDirectory = value3;
					break;
				case "Locales":
					this.DefaultCssResourcePivots.Set("locales", new ResourcePivotApplyMode?(ResourcePivotApplyMode.ApplyAsStringReplace), value3.NullSafeAction((string sv) => sv.SafeSplitSemiColonSeperatedValue()));
					this.DefaultJsResourcePivots.Set("locales", new ResourcePivotApplyMode?(ResourcePivotApplyMode.ApplyAsStringReplace), value3.NullSafeAction((string sv) => sv.SafeSplitSemiColonSeperatedValue()));
					break;
				case "Themes":
					this.DefaultCssResourcePivots.Set("themes", new ResourcePivotApplyMode?(ResourcePivotApplyMode.ApplyAsStringReplace), value3.NullSafeAction((string sv) => sv.SafeSplitSemiColonSeperatedValue()));
					break;
				case "ResourcePivot":
					this.DefaultJsResourcePivots.Set((string)xelement.Attribute("key"), new ResourcePivotApplyMode?(((string)xelement.Attribute("applyMode")).TryParseToEnum(null) ?? ResourcePivotApplyMode.ApplyAsStringReplace), ((string)xelement).NullSafeAction((string sv) => sv.SafeSplitSemiColonSeperatedValue()));
					this.DefaultCssResourcePivots.Set((string)xelement.Attribute("key"), new ResourcePivotApplyMode?(((string)xelement.Attribute("applyMode")).TryParseToEnum(null) ?? ResourcePivotApplyMode.ApplyAsStringReplace), ((string)xelement).NullSafeAction((string sv) => sv.SafeSplitSemiColonSeperatedValue()));
					break;
				case "Bundling":
					this.DefaultBundling.AddNamedConfig(new BundlingConfig(xelement));
					break;
				case "Global":
					this.global.AddNamedConfig(new GlobalConfig(xelement));
					break;
				case "CssMinification":
					this.DefaultCssMinification.AddNamedConfig(new CssMinificationConfig(xelement));
					break;
				case "Spriting":
					this.DefaultSpriting.AddNamedConfig(new CssSpritingConfig(xelement));
					break;
				case "JsMinification":
					this.DefaultJSMinification.AddNamedConfig(new JsMinificationConfig(xelement));
					break;
				case "Preprocessing":
					this.DefaultPreprocessing.AddNamedConfig(new PreprocessingConfig(xelement));
					break;
				}
			}
		}

		// Token: 0x04000627 RID: 1575
		private static readonly Regex EnvironmentVariablesMatchPattern = new Regex("%(?<name>[a-zA-Z]*?)%", RegexOptions.Compiled);

		// Token: 0x04000628 RID: 1576
		private static readonly TimeSpan MinimumCacheTimeout = TimeSpan.FromHours(1.0);

		// Token: 0x04000629 RID: 1577
		private readonly Dictionary<string, GlobalConfig> global = new Dictionary<string, GlobalConfig>();
	}
}
