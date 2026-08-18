using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace System.Web.WebPages.Deployment
{
	// Token: 0x02000003 RID: 3
	internal static class AppDomainHelper
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002240 File Offset: 0x00000440
		public static IDictionary<string, IEnumerable<string>> GetBinAssemblyReferences(string appPath, string configPath)
		{
			string text = Path.Combine(appPath, "bin");
			if (!Directory.Exists(text))
			{
				return null;
			}
			AppDomain appDomain = null;
			IDictionary<string, IEnumerable<string>> result;
			try
			{
				AppDomainSetup info = new AppDomainSetup
				{
					ApplicationBase = appPath,
					ConfigurationFile = configPath,
					PrivateBinPath = text
				};
				appDomain = AppDomain.CreateDomain(typeof(AppDomainHelper).Namespace, AppDomain.CurrentDomain.Evidence, info);
				Type typeFromHandle = typeof(AppDomainHelper.RemoteAssemblyLoader);
				AppDomainHelper.RemoteAssemblyLoader instance = (AppDomainHelper.RemoteAssemblyLoader)appDomain.CreateInstanceAndUnwrap(typeFromHandle.Assembly.FullName, typeFromHandle.FullName);
				result = Directory.EnumerateFiles(text, "*.dll").ToDictionary((string assemblyPath) => assemblyPath, (string assemblyPath) => instance.GetReferences(assemblyPath));
			}
			finally
			{
				if (appDomain != null)
				{
					AppDomain.Unload(appDomain);
				}
			}
			return result;
		}

		// Token: 0x02000004 RID: 4
		private sealed class RemoteAssemblyLoader : MarshalByRefObject
		{
			// Token: 0x06000010 RID: 16 RVA: 0x0000234C File Offset: 0x0000054C
			public IEnumerable<string> GetReferences(string assemblyPath)
			{
				Assembly assembly = Assembly.LoadFrom(assemblyPath);
				return (from asmName in assembly.GetReferencedAssemblies()
				select Assembly.Load(asmName.FullName).FullName).Concat(new string[]
				{
					assembly.FullName
				}).ToArray<string>();
			}
		}
	}
}
