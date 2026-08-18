using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NLog.Common;
using NLog.Conditions;
using NLog.Filters;
using NLog.Internal;
using NLog.LayoutRenderers;
using NLog.Layouts;
using NLog.Targets;
using NLog.Time;

namespace NLog.Config
{
	// Token: 0x02000044 RID: 68
	public class ConfigurationItemFactory
	{
		// Token: 0x06000135 RID: 309 RVA: 0x00004C6C File Offset: 0x00002E6C
		public ConfigurationItemFactory(params Assembly[] assemblies)
		{
			this.CreateInstance = new ConfigurationItemCreator(FactoryHelper.CreateInstance);
			this.targets = new Factory<Target, TargetAttribute>(this);
			this.filters = new Factory<Filter, FilterAttribute>(this);
			this.layoutRenderers = new Factory<LayoutRenderer, LayoutRendererAttribute>(this);
			this.layouts = new Factory<Layout, LayoutAttribute>(this);
			this.conditionMethods = new MethodFactory<ConditionMethodsAttribute, ConditionMethodAttribute>();
			this.ambientProperties = new Factory<LayoutRenderer, AmbientPropertyAttribute>(this);
			this.timeSources = new Factory<TimeSource, TimeSourceAttribute>(this);
			this.allFactories = new List<object>
			{
				this.targets,
				this.filters,
				this.layoutRenderers,
				this.layouts,
				this.conditionMethods,
				this.ambientProperties,
				this.timeSources
			};
			foreach (Assembly assembly in assemblies)
			{
				this.RegisterItemsFromAssembly(assembly);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00004D60 File Offset: 0x00002F60
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00004D78 File Offset: 0x00002F78
		public static ConfigurationItemFactory Default
		{
			get
			{
				if (ConfigurationItemFactory.defaultInstance == null)
				{
					ConfigurationItemFactory.defaultInstance = ConfigurationItemFactory.BuildDefaultFactory();
				}
				return ConfigurationItemFactory.defaultInstance;
			}
			set
			{
				ConfigurationItemFactory.defaultInstance = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00004D80 File Offset: 0x00002F80
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00004D88 File Offset: 0x00002F88
		public ConfigurationItemCreator CreateInstance { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00004D91 File Offset: 0x00002F91
		public INamedItemFactory<Target, Type> Targets
		{
			get
			{
				return this.targets;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00004D99 File Offset: 0x00002F99
		public INamedItemFactory<Filter, Type> Filters
		{
			get
			{
				return this.filters;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00004DA1 File Offset: 0x00002FA1
		public INamedItemFactory<LayoutRenderer, Type> LayoutRenderers
		{
			get
			{
				return this.layoutRenderers;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00004DA9 File Offset: 0x00002FA9
		public INamedItemFactory<Layout, Type> Layouts
		{
			get
			{
				return this.layouts;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00004DB1 File Offset: 0x00002FB1
		public INamedItemFactory<LayoutRenderer, Type> AmbientProperties
		{
			get
			{
				return this.ambientProperties;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00004DB9 File Offset: 0x00002FB9
		public INamedItemFactory<TimeSource, Type> TimeSources
		{
			get
			{
				return this.timeSources;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00004DC1 File Offset: 0x00002FC1
		public INamedItemFactory<MethodInfo, MethodInfo> ConditionMethods
		{
			get
			{
				return this.conditionMethods;
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004DC9 File Offset: 0x00002FC9
		public void RegisterItemsFromAssembly(Assembly assembly)
		{
			this.RegisterItemsFromAssembly(assembly, string.Empty);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004DD8 File Offset: 0x00002FD8
		public void RegisterItemsFromAssembly(Assembly assembly, string itemNamePrefix)
		{
			InternalLogger.Debug("ScanAssembly('{0}')", new object[]
			{
				assembly.FullName
			});
			Type[] type = assembly.SafeGetTypes();
			foreach (object obj in this.allFactories)
			{
				IFactory factory = (IFactory)obj;
				factory.ScanTypes(type, itemNamePrefix);
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004E50 File Offset: 0x00003050
		public void Clear()
		{
			foreach (object obj in this.allFactories)
			{
				IFactory factory = (IFactory)obj;
				factory.Clear();
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004EA4 File Offset: 0x000030A4
		public void RegisterType(Type type, string itemNamePrefix)
		{
			foreach (object obj in this.allFactories)
			{
				IFactory factory = (IFactory)obj;
				factory.RegisterType(type, itemNamePrefix);
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004F44 File Offset: 0x00003144
		private static ConfigurationItemFactory BuildDefaultFactory()
		{
			Assembly assembly = typeof(ILogger).Assembly;
			ConfigurationItemFactory configurationItemFactory = new ConfigurationItemFactory(new Assembly[]
			{
				assembly
			});
			configurationItemFactory.RegisterExtendedItems();
			string assemblyLocation = Path.GetDirectoryName(new Uri(assembly.CodeBase).LocalPath);
			if (assemblyLocation == null)
			{
				InternalLogger.Warn("No auto loading because Nlog.dll location is unknown");
				return configurationItemFactory;
			}
			if (!Directory.Exists(assemblyLocation))
			{
				InternalLogger.Warn("No auto loading because '{0}' doesn't exists", new object[]
				{
					assemblyLocation
				});
				return configurationItemFactory;
			}
			IEnumerable<string> enumerable = from x in Directory.GetFiles(assemblyLocation, "NLog*.dll").Select(new Func<string, string>(Path.GetFileName))
			where !x.Equals("NLog.dll", StringComparison.OrdinalIgnoreCase)
			where !x.Equals("NLog.UnitTests.dll", StringComparison.OrdinalIgnoreCase)
			where !x.Equals("NLog.Extended.dll", StringComparison.OrdinalIgnoreCase)
			select Path.Combine(assemblyLocation, x);
			InternalLogger.Debug("Start auto loading, location: {0}", new object[]
			{
				assemblyLocation
			});
			foreach (string text in enumerable)
			{
				InternalLogger.Info("Auto loading assembly file: {0}", new object[]
				{
					text
				});
				bool flag = false;
				try
				{
					Assembly assembly2 = Assembly.LoadFrom(text);
					InternalLogger.LogAssemblyVersion(assembly2);
					configurationItemFactory.RegisterItemsFromAssembly(assembly2);
					flag = true;
				}
				catch (Exception ex)
				{
					if (ex.MustBeRethrownImmediately())
					{
						throw;
					}
					InternalLogger.Warn(ex, "Auto loading assembly file: {0} failed! Skipping this file.", new object[]
					{
						text
					});
				}
				if (flag)
				{
					InternalLogger.Info("Auto loading assembly file: {0} succeeded!", new object[]
					{
						text
					});
				}
			}
			InternalLogger.Debug("Auto loading done");
			return configurationItemFactory;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00005174 File Offset: 0x00003374
		private void RegisterExtendedItems()
		{
			string text = typeof(ILogger).AssemblyQualifiedName;
			string text2 = "NLog,";
			string str = "NLog.Extended,";
			int num = text.IndexOf(text2, StringComparison.OrdinalIgnoreCase);
			if (num >= 0)
			{
				text = ", " + str + text.Substring(num + text2.Length);
				string @namespace = typeof(DebugTarget).Namespace;
				this.targets.RegisterNamedType("AspNetTrace", @namespace + ".AspNetTraceTarget" + text);
				this.targets.RegisterNamedType("MSMQ", @namespace + ".MessageQueueTarget" + text);
				this.targets.RegisterNamedType("AspNetBufferingWrapper", @namespace + ".Wrappers.AspNetBufferingTargetWrapper" + text);
				string namespace2 = typeof(MessageLayoutRenderer).Namespace;
				this.layoutRenderers.RegisterNamedType("appsetting", namespace2 + ".AppSettingLayoutRenderer" + text);
				this.layoutRenderers.RegisterNamedType("aspnet-application", namespace2 + ".AspNetApplicationValueLayoutRenderer" + text);
				this.layoutRenderers.RegisterNamedType("aspnet-request", namespace2 + ".AspNetRequestValueLayoutRenderer" + text);
				this.layoutRenderers.RegisterNamedType("aspnet-sessionid", namespace2 + ".AspNetSessionIDLayoutRenderer" + text);
				this.layoutRenderers.RegisterNamedType("aspnet-session", namespace2 + ".AspNetSessionValueLayoutRenderer" + text);
				this.layoutRenderers.RegisterNamedType("aspnet-user-authtype", namespace2 + ".AspNetUserAuthTypeLayoutRenderer" + text);
				this.layoutRenderers.RegisterNamedType("aspnet-user-identity", namespace2 + ".AspNetUserIdentityLayoutRenderer" + text);
			}
		}

		// Token: 0x04000070 RID: 112
		private static ConfigurationItemFactory defaultInstance;

		// Token: 0x04000071 RID: 113
		private readonly IList<object> allFactories;

		// Token: 0x04000072 RID: 114
		private readonly Factory<Target, TargetAttribute> targets;

		// Token: 0x04000073 RID: 115
		private readonly Factory<Filter, FilterAttribute> filters;

		// Token: 0x04000074 RID: 116
		private readonly Factory<LayoutRenderer, LayoutRendererAttribute> layoutRenderers;

		// Token: 0x04000075 RID: 117
		private readonly Factory<Layout, LayoutAttribute> layouts;

		// Token: 0x04000076 RID: 118
		private readonly MethodFactory<ConditionMethodsAttribute, ConditionMethodAttribute> conditionMethods;

		// Token: 0x04000077 RID: 119
		private readonly Factory<LayoutRenderer, AmbientPropertyAttribute> ambientProperties;

		// Token: 0x04000078 RID: 120
		private readonly Factory<TimeSource, TimeSourceAttribute> timeSources;
	}
}
