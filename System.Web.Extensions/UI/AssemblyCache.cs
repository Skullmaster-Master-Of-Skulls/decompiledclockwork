using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Reflection;
using System.Web.Configuration;
using System.Web.Script;

namespace System.Web.UI
{
	// Token: 0x02000042 RID: 66
	internal static class AssemblyCache
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00010F02 File Offset: 0x0000F102
		private static CompilationSection CompilationSection
		{
			get
			{
				if (AssemblyCache._compilationSection == null)
				{
					AssemblyCache._compilationSection = RuntimeConfig.GetAppConfig().Compilation;
				}
				return AssemblyCache._compilationSection;
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00010F20 File Offset: 0x0000F120
		public static Version GetVersion(Assembly assembly)
		{
			Version version = (Version)AssemblyCache._versionCache[assembly];
			if (version == null)
			{
				version = new AssemblyName(assembly.FullName).Version;
				AssemblyCache._versionCache[assembly] = version;
			}
			return version;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00010F68 File Offset: 0x0000F168
		public static Assembly Load(string assemblyName)
		{
			Assembly assembly = (Assembly)AssemblyCache._assemblyCache[assemblyName];
			if (assembly == null)
			{
				if (AssemblyCache._useCompilationSection)
				{
					assembly = AssemblyCache.CompilationSection.LoadAssembly(assemblyName, true);
				}
				else
				{
					assembly = Assembly.Load(assemblyName);
				}
				AssemblyCache._assemblyCache[assemblyName] = assembly;
			}
			return assembly;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00010FB9 File Offset: 0x0000F1B9
		public static bool IsAjaxFrameworkAssembly(Assembly assembly)
		{
			return AssemblyCache.GetAjaxFrameworkAssemblyAttribute(assembly) != null;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00010FC4 File Offset: 0x0000F1C4
		public static AjaxFrameworkAssemblyAttribute GetAjaxFrameworkAssemblyAttribute(Assembly assembly)
		{
			AjaxFrameworkAssemblyAttribute ajaxFrameworkAssemblyAttribute;
			if (!AssemblyCache._ajaxAssemblyAttributeCache.TryGetValue(assembly, out ajaxFrameworkAssemblyAttribute))
			{
				ajaxFrameworkAssemblyAttribute = AssemblyCache.SafeGetAjaxFrameworkAssemblyAttribute(assembly);
				AssemblyCache._ajaxAssemblyAttributeCache.TryAdd(assembly, ajaxFrameworkAssemblyAttribute);
			}
			return ajaxFrameworkAssemblyAttribute;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00010FF8 File Offset: 0x0000F1F8
		internal static AjaxFrameworkAssemblyAttribute SafeGetAjaxFrameworkAssemblyAttribute(ICustomAttributeProvider attributeProvider)
		{
			try
			{
				foreach (Attribute attribute in attributeProvider.GetCustomAttributes(false))
				{
					AjaxFrameworkAssemblyAttribute ajaxFrameworkAssemblyAttribute = attribute as AjaxFrameworkAssemblyAttribute;
					if (ajaxFrameworkAssemblyAttribute != null)
					{
						return ajaxFrameworkAssemblyAttribute;
					}
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x040000FB RID: 251
		public static readonly Assembly SystemWebExtensions = typeof(ScriptManager).Assembly;

		// Token: 0x040000FC RID: 252
		public static readonly Assembly SystemWeb = typeof(Page).Assembly;

		// Token: 0x040000FD RID: 253
		private static CompilationSection _compilationSection;

		// Token: 0x040000FE RID: 254
		internal static bool _useCompilationSection = true;

		// Token: 0x040000FF RID: 255
		private static readonly Hashtable _assemblyCache = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04000100 RID: 256
		internal static readonly Hashtable _versionCache = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04000101 RID: 257
		private static readonly ConcurrentDictionary<Assembly, AjaxFrameworkAssemblyAttribute> _ajaxAssemblyAttributeCache = new ConcurrentDictionary<Assembly, AjaxFrameworkAssemblyAttribute>();
	}
}
