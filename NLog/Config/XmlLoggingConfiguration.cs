using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using NLog.Common;
using NLog.Filters;
using NLog.Internal;
using NLog.Layouts;
using NLog.Targets;
using NLog.Targets.Wrappers;
using NLog.Time;

namespace NLog.Config
{
	// Token: 0x0200005B RID: 91
	public class XmlLoggingConfiguration : LoggingConfiguration
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00006B36 File Offset: 0x00004D36
		private ConfigurationItemFactory ConfigurationItemFactory
		{
			get
			{
				return ConfigurationItemFactory.Default;
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00006B3D File Offset: 0x00004D3D
		public XmlLoggingConfiguration(string fileName) : this(fileName, LogManager.LogFactory)
		{
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00006B4B File Offset: 0x00004D4B
		public XmlLoggingConfiguration(string fileName, LogFactory logFactory) : this(fileName, false, logFactory)
		{
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00006B56 File Offset: 0x00004D56
		public XmlLoggingConfiguration(string fileName, bool ignoreErrors) : this(fileName, ignoreErrors, LogManager.LogFactory)
		{
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00006B68 File Offset: 0x00004D68
		public XmlLoggingConfiguration(string fileName, bool ignoreErrors, LogFactory logFactory)
		{
			this.fileMustAutoReloadLookup = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
			base..ctor();
			this.logFactory = logFactory;
			using (XmlReader xmlReader = XmlLoggingConfiguration.CreateFileReader(fileName))
			{
				this.Initialize(xmlReader, fileName, ignoreErrors);
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00006BC0 File Offset: 0x00004DC0
		private static XmlReader CreateFileReader(string fileName)
		{
			if (!string.IsNullOrEmpty(fileName))
			{
				fileName = fileName.Trim();
				return XmlReader.Create(fileName);
			}
			return null;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00006BDA File Offset: 0x00004DDA
		public XmlLoggingConfiguration(XmlReader reader, string fileName) : this(reader, fileName, LogManager.LogFactory)
		{
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00006BE9 File Offset: 0x00004DE9
		public XmlLoggingConfiguration(XmlReader reader, string fileName, LogFactory logFactory) : this(reader, fileName, false, logFactory)
		{
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00006BF5 File Offset: 0x00004DF5
		public XmlLoggingConfiguration(XmlReader reader, string fileName, bool ignoreErrors) : this(reader, fileName, ignoreErrors, LogManager.LogFactory)
		{
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00006C05 File Offset: 0x00004E05
		public XmlLoggingConfiguration(XmlReader reader, string fileName, bool ignoreErrors, LogFactory logFactory)
		{
			this.fileMustAutoReloadLookup = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
			base..ctor();
			this.logFactory = logFactory;
			this.Initialize(reader, fileName, ignoreErrors);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00006C30 File Offset: 0x00004E30
		internal XmlLoggingConfiguration(XmlElement element, string fileName)
		{
			this.fileMustAutoReloadLookup = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
			base..ctor();
			this.logFactory = LogManager.LogFactory;
			using (StringReader stringReader = new StringReader(element.OuterXml))
			{
				XmlReader reader = XmlReader.Create(stringReader);
				this.Initialize(reader, fileName, false);
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00006C98 File Offset: 0x00004E98
		internal XmlLoggingConfiguration(XmlElement element, string fileName, bool ignoreErrors)
		{
			this.fileMustAutoReloadLookup = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
			base..ctor();
			this.logFactory = LogManager.LogFactory;
			using (StringReader stringReader = new StringReader(element.OuterXml))
			{
				XmlReader reader = XmlReader.Create(stringReader);
				this.Initialize(reader, fileName, ignoreErrors);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00006D00 File Offset: 0x00004F00
		public static LoggingConfiguration AppConfig
		{
			get
			{
				object section = System.Configuration.ConfigurationManager.GetSection("nlog");
				return section as LoggingConfiguration;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00006D1E File Offset: 0x00004F1E
		// (set) Token: 0x060001EE RID: 494 RVA: 0x00006D26 File Offset: 0x00004F26
		public bool? InitializeSucceeded { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00006D32 File Offset: 0x00004F32
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x00006D64 File Offset: 0x00004F64
		public bool AutoReload
		{
			get
			{
				return this.fileMustAutoReloadLookup.Values.All((bool mustAutoReload) => mustAutoReload);
			}
			set
			{
				List<string> list = this.fileMustAutoReloadLookup.Keys.ToList<string>();
				foreach (string key in list)
				{
					this.fileMustAutoReloadLookup[key] = value;
				}
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00006DE0 File Offset: 0x00004FE0
		public override IEnumerable<string> FileNamesToWatch
		{
			get
			{
				return from entry in this.fileMustAutoReloadLookup
				where entry.Value
				select entry.Key;
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00006E37 File Offset: 0x00005037
		public override LoggingConfiguration Reload()
		{
			return new XmlLoggingConfiguration(this.originalFileName);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00006E44 File Offset: 0x00005044
		public static IEnumerable<string> GetCandidateConfigFilePaths()
		{
			return LogManager.LogFactory.GetCandidateConfigFilePaths();
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00006E50 File Offset: 0x00005050
		public static void SetCandidateConfigFilePaths(IEnumerable<string> filePaths)
		{
			LogManager.LogFactory.SetCandidateConfigFilePaths(filePaths);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00006E5D File Offset: 0x0000505D
		public static void ResetCandidateConfigFilePath()
		{
			LogManager.LogFactory.ResetCandidateConfigFilePath();
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00006E69 File Offset: 0x00005069
		private static bool IsTargetElement(string name)
		{
			return name.Equals("target", StringComparison.OrdinalIgnoreCase) || name.Equals("wrapper", StringComparison.OrdinalIgnoreCase) || name.Equals("wrapper-target", StringComparison.OrdinalIgnoreCase) || name.Equals("compound-target", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00006EA3 File Offset: 0x000050A3
		private static bool IsTargetRefElement(string name)
		{
			return name.Equals("target-ref", StringComparison.OrdinalIgnoreCase) || name.Equals("wrapper-target-ref", StringComparison.OrdinalIgnoreCase) || name.Equals("compound-target-ref", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00006ECF File Offset: 0x000050CF
		private static string CleanSpaces(string s)
		{
			s = s.Replace(" ", string.Empty);
			return s;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00006EE4 File Offset: 0x000050E4
		private static string StripOptionalNamespacePrefix(string attributeValue)
		{
			if (attributeValue == null)
			{
				return null;
			}
			int num = attributeValue.IndexOf(':');
			if (num < 0)
			{
				return attributeValue;
			}
			return attributeValue.Substring(num + 1);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00006F10 File Offset: 0x00005110
		private static Target WrapWithAsyncTargetWrapper(Target target)
		{
			AsyncTargetWrapper asyncTargetWrapper = new AsyncTargetWrapper();
			asyncTargetWrapper.WrappedTarget = target;
			asyncTargetWrapper.Name = target.Name;
			target.Name += "_wrapped";
			InternalLogger.Debug("Wrapping target '{0}' with AsyncTargetWrapper and renaming to '{1}", new object[]
			{
				asyncTargetWrapper.Name,
				target.Name
			});
			target = asyncTargetWrapper;
			return target;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00006F74 File Offset: 0x00005174
		private void Initialize(XmlReader reader, string fileName, bool ignoreErrors)
		{
			try
			{
				this.InitializeSucceeded = null;
				reader.MoveToContent();
				NLogXmlElement content = new NLogXmlElement(reader);
				if (fileName != null)
				{
					this.originalFileName = fileName;
					this.ParseTopLevel(content, fileName, false);
					InternalLogger.Info("Configured from an XML element in {0}...", new object[]
					{
						fileName
					});
				}
				else
				{
					this.ParseTopLevel(content, null, false);
				}
				this.InitializeSucceeded = new bool?(true);
				this.CheckUnusedTargets();
			}
			catch (Exception ex)
			{
				this.InitializeSucceeded = new bool?(false);
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
				NLogConfigurationException ex2 = new NLogConfigurationException("Exception occurred when loading configuration from " + fileName, ex);
				InternalLogger.Error(ex2, "Error in Parsing Configuration File.");
				if (!ignoreErrors && ex2.MustBeRethrown())
				{
					throw ex2;
				}
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000070A0 File Offset: 0x000052A0
		private void CheckUnusedTargets()
		{
			if (this.InitializeSucceeded == null)
			{
				InternalLogger.Warn("Unused target checking is canceled -> initialize not started yet.");
				return;
			}
			if (!this.InitializeSucceeded.Value)
			{
				InternalLogger.Warn("Unused target checking is canceled -> initialize not succeeded.");
				return;
			}
			ReadOnlyCollection<Target> configuredNamedTargets = base.ConfiguredNamedTargets;
			InternalLogger.Debug("Unused target checking is started... Rule Count: {0}, Target Count: {1}", new object[]
			{
				base.LoggingRules.Count,
				configuredNamedTargets.Count
			});
			HashSet<string> targetNamesAtRules = new HashSet<string>(from t in base.LoggingRules.SelectMany((LoggingRule r) => r.Targets)
			select t.Name);
			int unusedCount = 0;
			configuredNamedTargets.ToList<Target>().ForEach(delegate(Target target)
			{
				if (!targetNamesAtRules.Contains(target.Name))
				{
					InternalLogger.Warn("Unused target detected. Add a rule for this target to the configuration. TargetName: {0}", new object[]
					{
						target.Name
					});
					unusedCount++;
				}
			});
			InternalLogger.Debug("Unused target checking is completed. Total Rule Count: {0}, Total Target Count: {1}, Unused Target Count: {2}", new object[]
			{
				base.LoggingRules.Count,
				configuredNamedTargets.Count,
				unusedCount
			});
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000071E0 File Offset: 0x000053E0
		private void ConfigureFromFile(string fileName, bool autoReloadDefault)
		{
			if (!this.fileMustAutoReloadLookup.ContainsKey(XmlLoggingConfiguration.GetFileLookupKey(fileName)))
			{
				this.ParseTopLevel(new NLogXmlElement(fileName), fileName, autoReloadDefault);
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00007204 File Offset: 0x00005404
		private void ParseTopLevel(NLogXmlElement content, string filePath, bool autoReloadDefault)
		{
			content.AssertName(new string[]
			{
				"nlog",
				"configuration"
			});
			string a;
			if ((a = content.LocalName.ToUpper(CultureInfo.InvariantCulture)) != null)
			{
				if (a == "CONFIGURATION")
				{
					this.ParseConfigurationElement(content, filePath, autoReloadDefault);
					return;
				}
				if (!(a == "NLOG"))
				{
					return;
				}
				this.ParseNLogElement(content, filePath, autoReloadDefault);
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00007274 File Offset: 0x00005474
		private void ParseConfigurationElement(NLogXmlElement configurationElement, string filePath, bool autoReloadDefault)
		{
			InternalLogger.Trace("ParseConfigurationElement");
			configurationElement.AssertName(new string[]
			{
				"configuration"
			});
			List<NLogXmlElement> list = configurationElement.Elements("nlog").ToList<NLogXmlElement>();
			foreach (NLogXmlElement nlogElement in list)
			{
				this.ParseNLogElement(nlogElement, filePath, autoReloadDefault);
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000730C File Offset: 0x0000550C
		private void ParseNLogElement(NLogXmlElement nlogElement, string filePath, bool autoReloadDefault)
		{
			InternalLogger.Trace("ParseNLogElement");
			nlogElement.AssertName(new string[]
			{
				"nlog"
			});
			if (nlogElement.GetOptionalBooleanAttribute("useInvariantCulture", false))
			{
				base.DefaultCultureInfo = CultureInfo.InvariantCulture;
			}
			InternalLogger.LogLevel = LogLevel.FromString(nlogElement.GetOptionalAttribute("internalLogLevel", InternalLogger.LogLevel.Name));
			base.ExceptionLoggingOldStyle = nlogElement.GetOptionalBooleanAttribute("exceptionLoggingOldStyle", false);
			bool optionalBooleanAttribute = nlogElement.GetOptionalBooleanAttribute("autoReload", autoReloadDefault);
			if (filePath != null)
			{
				this.fileMustAutoReloadLookup[XmlLoggingConfiguration.GetFileLookupKey(filePath)] = optionalBooleanAttribute;
			}
			this.logFactory.ThrowExceptions = nlogElement.GetOptionalBooleanAttribute("throwExceptions", this.logFactory.ThrowExceptions);
			this.logFactory.ThrowConfigExceptions = nlogElement.GetOptionalBooleanAttribute("throwConfigExceptions", this.logFactory.ThrowConfigExceptions);
			InternalLogger.LogToConsole = nlogElement.GetOptionalBooleanAttribute("internalLogToConsole", InternalLogger.LogToConsole);
			InternalLogger.LogToConsoleError = nlogElement.GetOptionalBooleanAttribute("internalLogToConsoleError", InternalLogger.LogToConsoleError);
			InternalLogger.LogFile = nlogElement.GetOptionalAttribute("internalLogFile", InternalLogger.LogFile);
			InternalLogger.LogToTrace = nlogElement.GetOptionalBooleanAttribute("internalLogToTrace", InternalLogger.LogToTrace);
			InternalLogger.IncludeTimestamp = nlogElement.GetOptionalBooleanAttribute("internalLogIncludeTimestamp", InternalLogger.IncludeTimestamp);
			this.logFactory.GlobalThreshold = LogLevel.FromString(nlogElement.GetOptionalAttribute("globalThreshold", this.logFactory.GlobalThreshold.Name));
			List<NLogXmlElement> list = nlogElement.Children.ToList<NLogXmlElement>();
			List<NLogXmlElement> list2 = (from child in list
			where child.LocalName.Equals("EXTENSIONS", StringComparison.InvariantCultureIgnoreCase)
			select child).ToList<NLogXmlElement>();
			foreach (NLogXmlElement extensionsElement in list2)
			{
				this.ParseExtensionsElement(extensionsElement, Path.GetDirectoryName(filePath));
			}
			foreach (NLogXmlElement nlogXmlElement in list)
			{
				string key;
				switch (key = nlogXmlElement.LocalName.ToUpper(CultureInfo.InvariantCulture))
				{
				case "EXTENSIONS":
					continue;
				case "INCLUDE":
					this.ParseIncludeElement(nlogXmlElement, Path.GetDirectoryName(filePath), optionalBooleanAttribute);
					continue;
				case "APPENDERS":
				case "TARGETS":
					this.ParseTargetsElement(nlogXmlElement);
					continue;
				case "VARIABLE":
					this.ParseVariableElement(nlogXmlElement);
					continue;
				case "RULES":
					this.ParseRulesElement(nlogXmlElement, base.LoggingRules);
					continue;
				case "TIME":
					this.ParseTimeElement(nlogXmlElement);
					continue;
				}
				InternalLogger.Warn("Skipping unknown node: {0}", new object[]
				{
					nlogXmlElement.LocalName
				});
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000766C File Offset: 0x0000586C
		private void ParseRulesElement(NLogXmlElement rulesElement, IList<LoggingRule> rulesCollection)
		{
			InternalLogger.Trace("ParseRulesElement");
			rulesElement.AssertName(new string[]
			{
				"rules"
			});
			List<NLogXmlElement> list = rulesElement.Elements("logger").ToList<NLogXmlElement>();
			foreach (NLogXmlElement loggerElement in list)
			{
				this.ParseLoggerElement(loggerElement, rulesCollection);
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000076EC File Offset: 0x000058EC
		private void ParseLoggerElement(NLogXmlElement loggerElement, IList<LoggingRule> rulesCollection)
		{
			loggerElement.AssertName(new string[]
			{
				"logger"
			});
			string optionalAttribute = loggerElement.GetOptionalAttribute("name", "*");
			if (!loggerElement.GetOptionalBooleanAttribute("enabled", true))
			{
				InternalLogger.Debug("The logger named '{0}' are disabled");
				return;
			}
			LoggingRule loggingRule = new LoggingRule();
			string optionalAttribute2 = loggerElement.GetOptionalAttribute("appendTo", null);
			if (optionalAttribute2 == null)
			{
				optionalAttribute2 = loggerElement.GetOptionalAttribute("writeTo", null);
			}
			loggingRule.LoggerNamePattern = optionalAttribute;
			if (optionalAttribute2 != null)
			{
				foreach (string text in optionalAttribute2.Split(new char[]
				{
					','
				}))
				{
					string text2 = text.Trim();
					Target target = base.FindTargetByName(text2);
					if (target == null)
					{
						throw new NLogConfigurationException("Target " + text2 + " not found.");
					}
					loggingRule.Targets.Add(target);
				}
			}
			loggingRule.Final = loggerElement.GetOptionalBooleanAttribute("final", false);
			string text3;
			if (loggerElement.AttributeValues.TryGetValue("level", out text3))
			{
				LogLevel level = LogLevel.FromString(text3);
				loggingRule.EnableLoggingForLevel(level);
			}
			else if (loggerElement.AttributeValues.TryGetValue("levels", out text3))
			{
				text3 = XmlLoggingConfiguration.CleanSpaces(text3);
				string[] array2 = text3.Split(new char[]
				{
					','
				});
				foreach (string text4 in array2)
				{
					if (!string.IsNullOrEmpty(text4))
					{
						LogLevel level2 = LogLevel.FromString(text4);
						loggingRule.EnableLoggingForLevel(level2);
					}
				}
			}
			else
			{
				int num = 0;
				int ordinal = LogLevel.MaxLevel.Ordinal;
				string levelName;
				if (loggerElement.AttributeValues.TryGetValue("minLevel", out levelName))
				{
					num = LogLevel.FromString(levelName).Ordinal;
				}
				string levelName2;
				if (loggerElement.AttributeValues.TryGetValue("maxLevel", out levelName2))
				{
					ordinal = LogLevel.FromString(levelName2).Ordinal;
				}
				for (int k = num; k <= ordinal; k++)
				{
					loggingRule.EnableLoggingForLevel(LogLevel.FromOrdinal(k));
				}
			}
			List<NLogXmlElement> list = loggerElement.Children.ToList<NLogXmlElement>();
			foreach (NLogXmlElement nlogXmlElement in list)
			{
				string a;
				if ((a = nlogXmlElement.LocalName.ToUpper(CultureInfo.InvariantCulture)) != null)
				{
					if (!(a == "FILTERS"))
					{
						if (a == "LOGGER")
						{
							this.ParseLoggerElement(nlogXmlElement, loggingRule.ChildRules);
						}
					}
					else
					{
						this.ParseFilters(loggingRule, nlogXmlElement);
					}
				}
			}
			rulesCollection.Add(loggingRule);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00007998 File Offset: 0x00005B98
		private void ParseFilters(LoggingRule rule, NLogXmlElement filtersElement)
		{
			filtersElement.AssertName(new string[]
			{
				"filters"
			});
			List<NLogXmlElement> list = filtersElement.Children.ToList<NLogXmlElement>();
			foreach (NLogXmlElement nlogXmlElement in list)
			{
				string localName = nlogXmlElement.LocalName;
				Filter filter = this.ConfigurationItemFactory.Filters.CreateInstance(localName);
				this.ConfigureObjectFromAttributes(filter, nlogXmlElement, false);
				rule.Filters.Add(filter);
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00007A34 File Offset: 0x00005C34
		private void ParseVariableElement(NLogXmlElement variableElement)
		{
			variableElement.AssertName(new string[]
			{
				"variable"
			});
			string requiredAttribute = variableElement.GetRequiredAttribute("name");
			string text = this.ExpandSimpleVariables(variableElement.GetRequiredAttribute("value"));
			base.Variables[requiredAttribute] = text;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00007A88 File Offset: 0x00005C88
		private void ParseTargetsElement(NLogXmlElement targetsElement)
		{
			targetsElement.AssertName(new string[]
			{
				"targets",
				"appenders"
			});
			bool optionalBooleanAttribute = targetsElement.GetOptionalBooleanAttribute("async", false);
			NLogXmlElement nlogXmlElement = null;
			Dictionary<string, NLogXmlElement> dictionary = new Dictionary<string, NLogXmlElement>();
			List<NLogXmlElement> list = targetsElement.Children.ToList<NLogXmlElement>();
			foreach (NLogXmlElement nlogXmlElement2 in list)
			{
				string localName = nlogXmlElement2.LocalName;
				string text = XmlLoggingConfiguration.StripOptionalNamespacePrefix(nlogXmlElement2.GetOptionalAttribute("type", null));
				string key;
				switch (key = localName.ToUpper(CultureInfo.InvariantCulture))
				{
				case "DEFAULT-WRAPPER":
					nlogXmlElement = nlogXmlElement2;
					break;
				case "DEFAULT-TARGET-PARAMETERS":
					if (text == null)
					{
						throw new NLogConfigurationException("Missing 'type' attribute on <" + localName + "/>.");
					}
					dictionary[text] = nlogXmlElement2;
					break;
				case "TARGET":
				case "APPENDER":
				case "WRAPPER":
				case "WRAPPER-TARGET":
				case "COMPOUND-TARGET":
				{
					if (text == null)
					{
						throw new NLogConfigurationException("Missing 'type' attribute on <" + localName + "/>.");
					}
					Target target = this.ConfigurationItemFactory.Targets.CreateInstance(text);
					NLogXmlElement targetElement;
					if (dictionary.TryGetValue(text, out targetElement))
					{
						this.ParseTargetElement(target, targetElement);
					}
					this.ParseTargetElement(target, nlogXmlElement2);
					if (optionalBooleanAttribute)
					{
						target = XmlLoggingConfiguration.WrapWithAsyncTargetWrapper(target);
					}
					if (nlogXmlElement != null)
					{
						target = this.WrapWithDefaultWrapper(target, nlogXmlElement);
					}
					InternalLogger.Info("Adding target {0}", new object[]
					{
						target
					});
					base.AddTarget(target.Name, target);
					break;
				}
				}
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00007CC4 File Offset: 0x00005EC4
		private void ParseTargetElement(Target target, NLogXmlElement targetElement)
		{
			CompoundTargetBase compoundTargetBase = target as CompoundTargetBase;
			WrapperTargetBase wrapperTargetBase = target as WrapperTargetBase;
			this.ConfigureObjectFromAttributes(target, targetElement, true);
			List<NLogXmlElement> list = targetElement.Children.ToList<NLogXmlElement>();
			foreach (NLogXmlElement nlogXmlElement in list)
			{
				string localName = nlogXmlElement.LocalName;
				if (compoundTargetBase != null)
				{
					if (XmlLoggingConfiguration.IsTargetRefElement(localName))
					{
						string requiredAttribute = nlogXmlElement.GetRequiredAttribute("name");
						Target target2 = base.FindTargetByName(requiredAttribute);
						if (target2 == null)
						{
							throw new NLogConfigurationException("Referenced target '" + requiredAttribute + "' not found.");
						}
						compoundTargetBase.Targets.Add(target2);
						continue;
					}
					else if (XmlLoggingConfiguration.IsTargetElement(localName))
					{
						string itemName = XmlLoggingConfiguration.StripOptionalNamespacePrefix(nlogXmlElement.GetRequiredAttribute("type"));
						Target target3 = this.ConfigurationItemFactory.Targets.CreateInstance(itemName);
						if (target3 != null)
						{
							this.ParseTargetElement(target3, nlogXmlElement);
							if (target3.Name != null)
							{
								base.AddTarget(target3.Name, target3);
							}
							compoundTargetBase.Targets.Add(target3);
							continue;
						}
						continue;
					}
				}
				if (wrapperTargetBase != null)
				{
					if (XmlLoggingConfiguration.IsTargetRefElement(localName))
					{
						string requiredAttribute2 = nlogXmlElement.GetRequiredAttribute("name");
						Target target4 = base.FindTargetByName(requiredAttribute2);
						if (target4 == null)
						{
							throw new NLogConfigurationException("Referenced target '" + requiredAttribute2 + "' not found.");
						}
						wrapperTargetBase.WrappedTarget = target4;
						continue;
					}
					else if (XmlLoggingConfiguration.IsTargetElement(localName))
					{
						string itemName2 = XmlLoggingConfiguration.StripOptionalNamespacePrefix(nlogXmlElement.GetRequiredAttribute("type"));
						Target target5 = this.ConfigurationItemFactory.Targets.CreateInstance(itemName2);
						if (target5 == null)
						{
							continue;
						}
						this.ParseTargetElement(target5, nlogXmlElement);
						if (target5.Name != null)
						{
							base.AddTarget(target5.Name, target5);
						}
						if (wrapperTargetBase.WrappedTarget != null)
						{
							throw new NLogConfigurationException("Wrapped target already defined.");
						}
						wrapperTargetBase.WrappedTarget = target5;
						continue;
					}
				}
				this.SetPropertyFromElement(target, nlogXmlElement);
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00007ECC File Offset: 0x000060CC
		private void ParseExtensionsElement(NLogXmlElement extensionsElement, string baseDirectory)
		{
			extensionsElement.AssertName(new string[]
			{
				"extensions"
			});
			List<NLogXmlElement> list = extensionsElement.Elements("add").ToList<NLogXmlElement>();
			foreach (NLogXmlElement nlogXmlElement in list)
			{
				string text = nlogXmlElement.GetOptionalAttribute("prefix", null);
				if (text != null)
				{
					text += ".";
				}
				string text2 = XmlLoggingConfiguration.StripOptionalNamespacePrefix(nlogXmlElement.GetOptionalAttribute("type", null));
				if (text2 != null)
				{
					try
					{
						this.ConfigurationItemFactory.RegisterType(Type.GetType(text2, true), text);
					}
					catch (Exception ex)
					{
						if (ex.MustBeRethrownImmediately())
						{
							throw;
						}
						InternalLogger.Error(ex, "Error loading extensions.");
						NLogConfigurationException ex2 = new NLogConfigurationException("Error loading extensions: " + text2, ex);
						if (ex2.MustBeRethrown())
						{
							throw ex2;
						}
					}
				}
				string optionalAttribute = nlogXmlElement.GetOptionalAttribute("assemblyFile", null);
				if (optionalAttribute != null)
				{
					try
					{
						string text3 = Path.Combine(baseDirectory, optionalAttribute);
						InternalLogger.Info("Loading assembly file: {0}", new object[]
						{
							text3
						});
						Assembly assembly = Assembly.LoadFrom(text3);
						this.ConfigurationItemFactory.RegisterItemsFromAssembly(assembly, text);
						continue;
					}
					catch (Exception ex3)
					{
						if (ex3.MustBeRethrownImmediately())
						{
							throw;
						}
						InternalLogger.Error(ex3, "Error loading extensions.");
						NLogConfigurationException ex4 = new NLogConfigurationException("Error loading extensions: " + optionalAttribute, ex3);
						if (ex4.MustBeRethrown())
						{
							throw ex4;
						}
						continue;
					}
				}
				string optionalAttribute2 = nlogXmlElement.GetOptionalAttribute("assembly", null);
				if (optionalAttribute2 != null)
				{
					try
					{
						InternalLogger.Info("Loading assembly name: {0}", new object[]
						{
							optionalAttribute2
						});
						Assembly assembly2 = Assembly.Load(optionalAttribute2);
						this.ConfigurationItemFactory.RegisterItemsFromAssembly(assembly2, text);
					}
					catch (Exception ex5)
					{
						if (ex5.MustBeRethrownImmediately())
						{
							throw;
						}
						InternalLogger.Error(ex5, "Error loading extensions.");
						NLogConfigurationException ex6 = new NLogConfigurationException("Error loading extensions: " + optionalAttribute2, ex5);
						if (ex6.MustBeRethrown())
						{
							throw ex6;
						}
					}
				}
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000812C File Offset: 0x0000632C
		private void ParseIncludeElement(NLogXmlElement includeElement, string baseDirectory, bool autoReloadDefault)
		{
			includeElement.AssertName(new string[]
			{
				"include"
			});
			string text = includeElement.GetRequiredAttribute("file");
			try
			{
				text = this.ExpandSimpleVariables(text);
				text = SimpleLayout.Evaluate(text);
				if (baseDirectory != null)
				{
					text = Path.Combine(baseDirectory, text);
				}
				if (!File.Exists(text))
				{
					throw new FileNotFoundException("Included file not found: " + text);
				}
				InternalLogger.Debug("Including file '{0}'", new object[]
				{
					text
				});
				this.ConfigureFromFile(text, autoReloadDefault);
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "Error when including '{0}'.", new object[]
				{
					text
				});
				if (ex.MustBeRethrown())
				{
					throw;
				}
				if (!includeElement.GetOptionalBooleanAttribute("ignoreErrors", false))
				{
					throw new NLogConfigurationException("Error when including: " + text, ex);
				}
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00008208 File Offset: 0x00006408
		private void ParseTimeElement(NLogXmlElement timeElement)
		{
			timeElement.AssertName(new string[]
			{
				"time"
			});
			string requiredAttribute = timeElement.GetRequiredAttribute("type");
			TimeSource timeSource = this.ConfigurationItemFactory.TimeSources.CreateInstance(requiredAttribute);
			this.ConfigureObjectFromAttributes(timeSource, timeElement, true);
			InternalLogger.Info("Selecting time source {0}", new object[]
			{
				timeSource
			});
			TimeSource.Current = timeSource;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000826E File Offset: 0x0000646E
		private static string GetFileLookupKey(string fileName)
		{
			return Path.GetFullPath(fileName);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00008278 File Offset: 0x00006478
		private void SetPropertyFromElement(object o, NLogXmlElement element)
		{
			if (this.AddArrayItemFromElement(o, element))
			{
				return;
			}
			if (this.SetLayoutFromElement(o, element))
			{
				return;
			}
			if (this.SetItemFromElement(o, element))
			{
				return;
			}
			PropertyHelper.SetPropertyFromString(o, element.LocalName, this.ExpandSimpleVariables(element.Value), this.ConfigurationItemFactory);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000082C4 File Offset: 0x000064C4
		private bool AddArrayItemFromElement(object o, NLogXmlElement element)
		{
			string localName = element.LocalName;
			PropertyInfo propertyInfo;
			if (!PropertyHelper.TryGetPropertyInfo(o, localName, out propertyInfo))
			{
				return false;
			}
			Type arrayItemType = PropertyHelper.GetArrayItemType(propertyInfo);
			if (arrayItemType != null)
			{
				IList list = (IList)propertyInfo.GetValue(o, null);
				object obj = this.TryCreateLayoutInstance(element, arrayItemType);
				if (obj == null)
				{
					obj = FactoryHelper.CreateInstance(arrayItemType);
				}
				this.ConfigureObjectFromAttributes(obj, element, true);
				this.ConfigureObjectFromElement(obj, element);
				list.Add(obj);
				return true;
			}
			return false;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00008338 File Offset: 0x00006538
		private void ConfigureObjectFromAttributes(object targetObject, NLogXmlElement element, bool ignoreType)
		{
			List<KeyValuePair<string, string>> list = element.AttributeValues.ToList<KeyValuePair<string, string>>();
			foreach (KeyValuePair<string, string> keyValuePair in list)
			{
				string key = keyValuePair.Key;
				string value = keyValuePair.Value;
				if (!ignoreType || !key.Equals("type", StringComparison.OrdinalIgnoreCase))
				{
					PropertyHelper.SetPropertyFromString(targetObject, key, this.ExpandSimpleVariables(value), this.ConfigurationItemFactory);
				}
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000083C4 File Offset: 0x000065C4
		private bool SetLayoutFromElement(object o, NLogXmlElement layoutElement)
		{
			string localName = layoutElement.LocalName;
			PropertyInfo propertyInfo;
			if (PropertyHelper.TryGetPropertyInfo(o, localName, out propertyInfo))
			{
				Layout layout = this.TryCreateLayoutInstance(layoutElement, propertyInfo.PropertyType);
				if (layout != null)
				{
					this.ConfigureObjectFromAttributes(layout, layoutElement, true);
					this.ConfigureObjectFromElement(layout, layoutElement);
					propertyInfo.SetValue(o, layout, null);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00008414 File Offset: 0x00006614
		private bool SetItemFromElement(object o, NLogXmlElement element)
		{
			if (element.Value != null)
			{
				return false;
			}
			string localName = element.LocalName;
			PropertyInfo propertyInfo;
			if (!PropertyHelper.TryGetPropertyInfo(o, localName, out propertyInfo))
			{
				return false;
			}
			object value = propertyInfo.GetValue(o, null);
			this.ConfigureObjectFromAttributes(value, element, true);
			this.ConfigureObjectFromElement(value, element);
			return true;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000845C File Offset: 0x0000665C
		private void ConfigureObjectFromElement(object targetObject, NLogXmlElement element)
		{
			List<NLogXmlElement> list = element.Children.ToList<NLogXmlElement>();
			foreach (NLogXmlElement element2 in list)
			{
				this.SetPropertyFromElement(targetObject, element2);
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000084B8 File Offset: 0x000066B8
		private Target WrapWithDefaultWrapper(Target t, NLogXmlElement defaultParameters)
		{
			string itemName = XmlLoggingConfiguration.StripOptionalNamespacePrefix(defaultParameters.GetRequiredAttribute("type"));
			Target target = this.ConfigurationItemFactory.Targets.CreateInstance(itemName);
			WrapperTargetBase wrapperTargetBase = target as WrapperTargetBase;
			if (wrapperTargetBase == null)
			{
				throw new NLogConfigurationException("Target type specified on <default-wrapper /> is not a wrapper.");
			}
			this.ParseTargetElement(target, defaultParameters);
			while (wrapperTargetBase.WrappedTarget != null)
			{
				wrapperTargetBase = (wrapperTargetBase.WrappedTarget as WrapperTargetBase);
				if (wrapperTargetBase == null)
				{
					throw new NLogConfigurationException("Child target type specified on <default-wrapper /> is not a wrapper.");
				}
			}
			wrapperTargetBase.WrappedTarget = t;
			target.Name = t.Name;
			t.Name += "_wrapped";
			InternalLogger.Debug("Wrapping target '{0}' with '{1}' and renaming to '{2}", new object[]
			{
				target.Name,
				target.GetType().Name,
				t.Name
			});
			return target;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00008588 File Offset: 0x00006788
		private Layout TryCreateLayoutInstance(NLogXmlElement element, Type type)
		{
			if (!typeof(Layout).IsAssignableFrom(type))
			{
				return null;
			}
			string text = XmlLoggingConfiguration.StripOptionalNamespacePrefix(element.GetOptionalAttribute("type", null));
			if (text == null)
			{
				return null;
			}
			return this.ConfigurationItemFactory.Layouts.CreateInstance(this.ExpandSimpleVariables(text));
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000085D8 File Offset: 0x000067D8
		private string ExpandSimpleVariables(string input)
		{
			string text = input;
			List<KeyValuePair<string, SimpleLayout>> list = base.Variables.ToList<KeyValuePair<string, SimpleLayout>>();
			foreach (KeyValuePair<string, SimpleLayout> keyValuePair in list)
			{
				SimpleLayout value = keyValuePair.Value;
				if (value != null)
				{
					text = text.Replace("${" + keyValuePair.Key + "}", value.OriginalText);
				}
			}
			return text;
		}

		// Token: 0x040000B1 RID: 177
		private readonly Dictionary<string, bool> fileMustAutoReloadLookup;

		// Token: 0x040000B2 RID: 178
		private string originalFileName;

		// Token: 0x040000B3 RID: 179
		private LogFactory logFactory;
	}
}
