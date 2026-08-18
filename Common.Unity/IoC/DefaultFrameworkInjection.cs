using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ClockWorkLogger;
using TechnoPro.Common.Unity.Adapters;

namespace TechnoPro.Common.Unity.IoC
{
	// Token: 0x02000003 RID: 3
	public static class DefaultFrameworkInjection
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000021D8 File Offset: 0x000003D8
		public static void Initialize(params string[] assemblyNames)
		{
			CWLogger.Logger.Trace("DefaultFrameworkInjection::Initialize: Initializing assemblies " + string.Join(",", assemblyNames));
			DefaultFrameworkInjection.LoadCollection(assemblyNames);
			foreach (IConventionInjection conventionInjection in DefaultFrameworkInjection.ConventionInjectionCollection)
			{
				try
				{
					CWLogger.Logger.Trace("DefaultFrameworkInjection::Initialize: Initializing " + conventionInjection.GetType().Name + " ...");
					conventionInjection.Initialize();
				}
				catch (Exception ex)
				{
					CWLogger.Logger.ErrorException(string.Format("DefaultFrameworkInjection::Initialize: {0}", ex), ex);
					throw;
				}
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022A0 File Offset: 0x000004A0
		public static T ResolveByDefault<T>()
		{
			using (IEnumerator<IConventionInjection> enumerator = (from conventionInjection in DefaultFrameworkInjection.ConventionInjectionCollection
			where conventionInjection.Contains<T>()
			select conventionInjection).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					IConventionInjection conventionInjection2 = enumerator.Current;
					return conventionInjection2.ResolveByDefault<T>();
				}
			}
			return default(T);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002324 File Offset: 0x00000524
		public static bool Contains<T>()
		{
			return DefaultFrameworkInjection.ConventionInjectionCollection.Any((IConventionInjection conventionInjection) => conventionInjection.Contains<T>());
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002360 File Offset: 0x00000560
		public static T ResolveByDefault<T>(string name)
		{
			IEnumerable<IConventionInjection> conventionInjectionCollection = DefaultFrameworkInjection.ConventionInjectionCollection;
			Func<IConventionInjection, bool> <>9__0;
			Func<IConventionInjection, bool> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = ((IConventionInjection conventionInjection) => conventionInjection.Contains<T>(name)));
			}
			using (IEnumerator<IConventionInjection> enumerator = conventionInjectionCollection.Where(predicate).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					IConventionInjection conventionInjection2 = enumerator.Current;
					return conventionInjection2.ResolveByDefault<T>(name);
				}
			}
			return default(T);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023FC File Offset: 0x000005FC
		public static bool Contains<T>(string name)
		{
			return DefaultFrameworkInjection.ConventionInjectionCollection.Any((IConventionInjection conventionInjection) => conventionInjection.Contains<T>(name));
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002434 File Offset: 0x00000634
		private static void LoadCollection(params string[] assemblyNames)
		{
			DefaultFrameworkInjection.ConventionInjectionCollection = new List<IConventionInjection>();
			string assembliesFolder = DefaultFrameworkInjection.AssemblyDirectory;
			string[] assemblyPath = (assemblyNames != null && assemblyNames.Length != 0) ? (from a in assemblyNames
			select Path.Combine(assembliesFolder, a)).ToArray<string>() : Directory.GetFiles(assembliesFolder, "*.dll");
			IList<Assembly> source = assemblyPath.LoadAssemblySafely();
			Type conventionInjectionType = typeof(IConventionInjection);
			IEnumerable<Type> enumerable = from type in source.SelectMany((Assembly s) => s.GetTypesSafely())
			where conventionInjectionType.IsAssignableFrom(type) && !type.IsAbstract
			select type;
			foreach (Type type2 in enumerable)
			{
				DefaultFrameworkInjection.ConventionInjectionCollection.Add((IConventionInjection)Activator.CreateInstance(type2));
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002530 File Offset: 0x00000730
		public static string AssemblyDirectory
		{
			get
			{
				string codeBase = Assembly.GetExecutingAssembly().CodeBase;
				UriBuilder uriBuilder = new UriBuilder(codeBase);
				string path = Uri.UnescapeDataString(uriBuilder.Path);
				return Path.GetDirectoryName(path);
			}
		}

		// Token: 0x04000003 RID: 3
		private static IList<IConventionInjection> ConventionInjectionCollection = new List<IConventionInjection>();
	}
}
