using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using ClockWorkLogger;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace TechnoPro.Common.Unity.IoC
{
	// Token: 0x0200000A RID: 10
	public class ObjectFactory
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000268A File Offset: 0x0000088A
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002691 File Offset: 0x00000891
		public static IUnityContainer Container { get; set; }

		// Token: 0x06000024 RID: 36 RVA: 0x00002699 File Offset: 0x00000899
		private static void CreateContainer()
		{
			ObjectFactory.Container = new UnityContainer();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000026A7 File Offset: 0x000008A7
		public static void Configure(params string[] assemblyNames)
		{
			ObjectFactory.Configure(null, "unity", null, assemblyNames);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000026B8 File Offset: 0x000008B8
		public static void Configure(string configFilePath, params string[] assemblyNames)
		{
			ObjectFactory.Configure(configFilePath, "unity", null, assemblyNames);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000026C9 File Offset: 0x000008C9
		public static void Configure(string configFilePath, string unitySectionName, params string[] assemblyNames)
		{
			ObjectFactory.Configure(configFilePath, unitySectionName, null, assemblyNames);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000026D8 File Offset: 0x000008D8
		public static object Resolve(string name, Type objectType)
		{
			object result;
			try
			{
				MethodInfo method = typeof(ObjectFactory).GetMethod("ResolveType");
				MethodInfo methodInfo = method.MakeGenericMethod(new Type[]
				{
					objectType
				});
				result = methodInfo.Invoke(null, new object[]
				{
					name
				});
			}
			catch (Exception exception)
			{
				CWLogger.Logger.ErrorException(string.Concat(new string[]
				{
					"An unhandled exception occurred when resolving the registered name ",
					name,
					". Verify you have a unity configuration and the name ",
					name,
					" is registered and all the injected dependencies are correctly registered in the unity configuration."
				}), exception);
				throw;
			}
			return result;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002770 File Offset: 0x00000970
		public static bool Contains(string name)
		{
			bool result;
			try
			{
				UnityConfigurationSection unitySection = ObjectFactory.GetUnitySection("unity");
				bool flag = unitySection == null;
				if (flag)
				{
					result = false;
				}
				else
				{
					result = unitySection.Containers.SelectMany((ContainerElement container) => container.Registrations).Any((RegisterElement registration) => registration.Name.Equals(name));
				}
			}
			catch (Exception exception)
			{
				CWLogger.Logger.ErrorException(string.Concat(new string[]
				{
					"An unhandled exception occurred when verifying if the unity configuration contains the ",
					name,
					" registration name. Error searching for a registered name. Verify you have a unity configuration and the name ",
					name,
					" is registered and all the injected dependencies are correctly registered in the unity configuration."
				}), exception);
				result = false;
			}
			return result;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002840 File Offset: 0x00000A40
		public static Type GetType(string name)
		{
			bool flag = string.IsNullOrEmpty(name);
			if (flag)
			{
				throw new ArgumentNullException("name", "ObjectFactory cannot retrieve the type of a registered name that is null or empty");
			}
			UnityConfigurationSection unitySection = ObjectFactory.GetUnitySection("unity");
			bool flag2 = unitySection == null;
			if (flag2)
			{
				throw new InvalidOperationException("Unity section is not correctly configured in the configuration file. Check the error and warning logs for more information.");
			}
			Type result;
			try
			{
				string mapName = unitySection.Containers.SelectMany((ContainerElement container) => container.Registrations).FirstOrDefault((RegisterElement registration) => registration.Name.Equals(name)).MapToName;
				Type type = Type.GetType(mapName);
				result = (type ?? Type.GetType(unitySection.TypeAliases.FirstOrDefault((AliasElement alias) => alias.Alias == mapName).TypeName));
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Concat(new string[]
				{
					"An unhandled exception has occurred when creating the type for the instance name ",
					name,
					". Verify the ",
					name,
					" is correctly registered and all its dependencies."
				}), ex);
				throw new InvalidOperationException("ObjectFactory cannot create the type for the instance name " + name + ", an unhandled exception has occurred", ex);
			}
			return result;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002998 File Offset: 0x00000B98
		private static UnityConfigurationSection GetUnitySection(string unitySectionName)
		{
			UnityConfigurationSection unityConfigurationSection = (UnityConfigurationSection)ConfigurationManager.GetSection(unitySectionName);
			bool flag = unityConfigurationSection == null;
			if (flag)
			{
				string text = ConfigurationManager.AppSettings["UnityPath"];
				try
				{
					bool flag2 = !string.IsNullOrEmpty(text);
					if (flag2)
					{
						string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
						string fullPath = Path.GetFullPath(directoryName.Replace("file:\\", "") + text);
						ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap
						{
							ExeConfigFilename = fullPath
						};
						Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
						unityConfigurationSection = (UnityConfigurationSection)configuration.GetSection("unity");
					}
					else
					{
						CWLogger.Logger.Warn("Unity section is not configured in the configuration file and there is not unityPath setting in the application setting configuration section.");
					}
				}
				catch (Exception exception)
				{
					CWLogger.Logger.ErrorException("An unhandled exception occurred when trying to get the exception from the unity path " + text + ". Verify the unityPath exists and the unity section is correctly defined.", exception);
				}
			}
			return unityConfigurationSection;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002A8C File Offset: 0x00000C8C
		private static void Configure(string configFilePath, string unitySectionName, string unityContainerName, params string[] assemblyNames)
		{
			bool flag = ObjectFactory.Container == null;
			if (flag)
			{
				object sync = ObjectFactory.Sync;
				lock (sync)
				{
					UnityConfigurationSection unityConfigurationSection = null;
					bool flag3 = ObjectFactory.Container == null;
					if (flag3)
					{
						ObjectFactory.Container = new UnityContainer();
						try
						{
							bool flag4 = string.IsNullOrEmpty(configFilePath);
							if (flag4)
							{
								unityConfigurationSection = ObjectFactory.GetUnitySection(unitySectionName);
							}
							else
							{
								ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap
								{
									ExeConfigFilename = configFilePath
								};
								Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
								unityConfigurationSection = (UnityConfigurationSection)configuration.GetSection(unitySectionName);
							}
							bool flag5 = unityConfigurationSection != null;
							if (flag5)
							{
								ObjectFactory.Container = (string.IsNullOrEmpty(unityContainerName) ? unityConfigurationSection.Configure(ObjectFactory.Container) : unityConfigurationSection.Configure(ObjectFactory.Container, unityContainerName));
							}
							DefaultFrameworkInjection.Initialize(assemblyNames);
						}
						catch (Exception exception)
						{
							ObjectFactory.Container = null;
							bool flag6 = unityConfigurationSection != null;
							if (flag6)
							{
								CWLogger.Logger.ErrorException("An unhandled exception occurred when creating the container for the unity section. Unity section was found but the container couldn't be created based on the section. Verify the unity section configuration, the maps and the registrations objects are correctly defined.", exception);
							}
							else
							{
								bool flag7 = string.IsNullOrEmpty(configFilePath);
								if (flag7)
								{
									CWLogger.Logger.ErrorException("An unhandled exception occurred when retrieving the unity section from the configuration using the " + unitySectionName + " as unity section name. Verify you have declared the unity configuration section in the config sections and to have a unity section in the configuration.", exception);
								}
								else
								{
									CWLogger.Logger.ErrorException("An unhandled exception occurred when retrieving the unity section from the external path. Verify you have declared the unity configuration section in the config sections, verify also the external path " + configFilePath + " exists and to have a unity section in the external path file.", exception);
								}
							}
							throw;
						}
					}
				}
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002C14 File Offset: 0x00000E14
		public static void Clear()
		{
			object sync = ObjectFactory.Sync;
			lock (sync)
			{
				ObjectFactory.Container = null;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002C5C File Offset: 0x00000E5C
		public static bool Contains<T>(string name)
		{
			object sync = ObjectFactory.Sync;
			bool result;
			lock (sync)
			{
				result = ObjectFactory.Container.IsRegistered(name);
			}
			return result;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002CA8 File Offset: 0x00000EA8
		public static bool Contains<T>()
		{
			object sync = ObjectFactory.Sync;
			bool result;
			lock (sync)
			{
				result = ObjectFactory.Container.IsRegistered<T>();
			}
			return result;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002CF4 File Offset: 0x00000EF4
		public static T InternalResolve<T>()
		{
			bool flag = ObjectFactory.Container == null;
			if (flag)
			{
				CWLogger.Logger.Error("Unity container is null. Verify if client application has called ObjectFactory.Configure() or if there is any error or warning in the logs after calling ObjectFactory.Configure().");
				throw new InvalidOperationException("ObjectFactory cannot resolve a type if the Unity Container is not created.");
			}
			object sync = ObjectFactory.Sync;
			T result;
			lock (sync)
			{
				result = ObjectFactory.Container.Resolve(Array.Empty<ResolverOverride>());
			}
			return result;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002D6C File Offset: 0x00000F6C
		public static T Resolve<T>(string name)
		{
			object sync = ObjectFactory.Sync;
			T result;
			lock (sync)
			{
				try
				{
					bool flag2 = ObjectFactory.Container != null && ObjectFactory.Container.IsRegistered(name);
					if (flag2)
					{
						result = ObjectFactory.Container.Resolve(name, Array.Empty<ResolverOverride>());
					}
					else
					{
						bool flag3 = !DefaultFrameworkInjection.Contains<T>(name);
						if (flag3)
						{
							CWLogger.Logger.Error(string.Format("Dependency of Injection Framework and Default Framework injection couldn't find the instance of type {0} with id {1}", typeof(T), name));
							result = default(T);
						}
						else
						{
							result = DefaultFrameworkInjection.ResolveByDefault<T>(name);
						}
					}
				}
				catch (Exception exception)
				{
					CWLogger.Logger.ErrorException(string.Concat(new string[]
					{
						"An unhandled exception occurred when resolving the registered name ",
						name,
						". Verify that ",
						name,
						" is correctly configured in the section and all its dependencies."
					}), exception);
					throw;
				}
			}
			return result;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002E68 File Offset: 0x00001068
		public static T ResolveType<T>(string name)
		{
			bool flag = ObjectFactory.Container == null;
			if (flag)
			{
				CWLogger.Logger.Error("Unity container is null. Verify if client application has called ObjectFactory.Configure() or if there is any error or warning in the logs after calling ObjectFactory.Configure().");
				throw new InvalidOperationException("ObjectFactory cannot resolve a type if the Unity Container is not created.");
			}
			object sync = ObjectFactory.Sync;
			T result;
			lock (sync)
			{
				try
				{
					result = ObjectFactory.Container.Resolve(name, Array.Empty<ResolverOverride>());
				}
				catch (Exception exception)
				{
					CWLogger.Logger.ErrorException(string.Concat(new string[]
					{
						"An unhandled exception occurred when resolving the registered name ",
						name,
						". Verify that ",
						name,
						" is correctly configured in the section and all its dependencies."
					}), exception);
					throw;
				}
			}
			return result;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002F2C File Offset: 0x0000112C
		public static T Resolve<T>()
		{
			object sync = ObjectFactory.Sync;
			T result;
			lock (sync)
			{
				try
				{
					bool flag2 = ObjectFactory.Container != null && ObjectFactory.Container.IsRegistered<T>();
					if (flag2)
					{
						result = ObjectFactory.Container.Resolve(Array.Empty<ResolverOverride>());
					}
					else
					{
						bool flag3 = !DefaultFrameworkInjection.Contains<T>();
						if (flag3)
						{
							CWLogger.Logger.Error(string.Format("Dependency of Injection Framework and Default Framework injection couldn't find the instance of type {0} ", typeof(T)));
							result = default(T);
						}
						else
						{
							result = DefaultFrameworkInjection.ResolveByDefault<T>();
						}
					}
				}
				catch (Exception exception)
				{
					CWLogger.Logger.ErrorException(string.Format("An unhandled exception occurred when resolving the registered type {0}. Verify that {1} is correctly configured in the section and all its dependencies.", typeof(T), typeof(T)), exception);
					result = default(T);
				}
			}
			return result;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003020 File Offset: 0x00001220
		public static T TryResolve<T>(string name)
		{
			T result = default(T);
			object sync = ObjectFactory.Sync;
			lock (sync)
			{
				try
				{
					result = ObjectFactory.Container.Resolve(name, Array.Empty<ResolverOverride>());
				}
				catch (Exception exception)
				{
					CWLogger.Logger.ErrorException(string.Format("An unhandled exception occurred when resolving the registered name {0}. Verify that {0} is correctly configured in the section and all its dependencies.", name), exception);
				}
			}
			return result;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000030AC File Offset: 0x000012AC
		public static T TryResolve<T>(string name, params ResolverOverride[] overrides)
		{
			T result = default(T);
			object sync = ObjectFactory.Sync;
			lock (sync)
			{
				try
				{
					result = ObjectFactory.Container.Resolve(name, overrides);
				}
				catch (Exception exception)
				{
					CWLogger.Logger.ErrorException(string.Format("An unhandled exception occurred when resolving the registered name {0}. Verify that {0} is correctly configured in the section and all its dependencies.", name), exception);
				}
			}
			return result;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003134 File Offset: 0x00001334
		public static void RegisterSingleton<TFrom, TTo>(string name, params InjectionMember[] injectionMembers) where TTo : TFrom
		{
			object sync = ObjectFactory.Sync;
			lock (sync)
			{
				bool flag2 = ObjectFactory.Container == null;
				if (flag2)
				{
					ObjectFactory.CreateContainer();
				}
				ObjectFactory.Container.RegisterType(name, new ContainerControlledLifetimeManager(), injectionMembers);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003198 File Offset: 0x00001398
		public static void RegisterSingleton<TFrom, TTo>(params InjectionMember[] injectionMembers) where TTo : TFrom
		{
			object sync = ObjectFactory.Sync;
			lock (sync)
			{
				bool flag2 = ObjectFactory.Container == null;
				if (flag2)
				{
					ObjectFactory.CreateContainer();
				}
				ObjectFactory.Container.RegisterType(new ContainerControlledLifetimeManager(), injectionMembers);
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000031FC File Offset: 0x000013FC
		public static void RegisterTransient<TFrom, TTo>(string name, params InjectionMember[] injectionMembers) where TTo : TFrom
		{
			object sync = ObjectFactory.Sync;
			lock (sync)
			{
				bool flag2 = ObjectFactory.Container == null;
				if (flag2)
				{
					ObjectFactory.CreateContainer();
				}
				ObjectFactory.Container.RegisterType(name, new TransientLifetimeManager(), injectionMembers);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003260 File Offset: 0x00001460
		public static void RegisterTransient<TFrom, TTo>(params InjectionMember[] injectionMembers) where TTo : TFrom
		{
			object sync = ObjectFactory.Sync;
			lock (sync)
			{
				bool flag2 = ObjectFactory.Container == null;
				if (flag2)
				{
					ObjectFactory.CreateContainer();
				}
				ObjectFactory.Container.RegisterType(new TransientLifetimeManager(), injectionMembers);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000032C4 File Offset: 0x000014C4
		public static void RegisterInstance<T>(object instance, LifetimeManager lifetimeManager)
		{
			object sync = ObjectFactory.Sync;
			lock (sync)
			{
				bool flag2 = ObjectFactory.Container == null;
				if (flag2)
				{
					ObjectFactory.CreateContainer();
				}
				ObjectFactory.Container.RegisterInstance(typeof(T), instance, lifetimeManager);
			}
		}

		// Token: 0x04000009 RID: 9
		private const string DefaultUnitySectionName = "unity";

		// Token: 0x0400000A RID: 10
		private static readonly object Sync = new object();
	}
}
