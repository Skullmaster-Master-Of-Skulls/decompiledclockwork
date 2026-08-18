using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Reflection;
using System.Web.Resources;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x0200007D RID: 125
	public class ScriptResourceMapping : IScriptResourceMapping
	{
		// Token: 0x06000568 RID: 1384 RVA: 0x000198D4 File Offset: 0x00017AD4
		public void AddDefinition(string name, ScriptResourceDefinition definition)
		{
			this.AddDefinition(name, AssemblyCache.SystemWebExtensions, definition);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x000198E4 File Offset: 0x00017AE4
		public void AddDefinition(string name, Assembly assembly, ScriptResourceDefinition definition)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(AtlasWeb.Common_NullOrEmpty, "name");
			}
			if (definition == null)
			{
				throw new ArgumentNullException("definition");
			}
			if (string.IsNullOrEmpty(definition.ResourceName) && string.IsNullOrEmpty(definition.Path))
			{
				throw new ArgumentException(AtlasWeb.ScriptResourceDefinition_NameAndPathCannotBeEmpty, "definition");
			}
			this.EnsureAbsoluteOrAppRelative(definition.Path);
			this.EnsureAbsoluteOrAppRelative(definition.DebugPath);
			this.EnsureAbsoluteOrAppRelative(definition.CdnPath);
			this.EnsureAbsoluteOrAppRelative(definition.CdnDebugPath);
			assembly = ScriptResourceMapping.NormalizeAssembly(assembly);
			this._definitions[new Tuple<string, Assembly>(name, assembly)] = definition;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0001998C File Offset: 0x00017B8C
		public void Clear()
		{
			this._definitions.Clear();
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001999C File Offset: 0x00017B9C
		private void EnsureAbsoluteOrAppRelative(string path)
		{
			if (!string.IsNullOrEmpty(path) && !UrlPath.IsAppRelativePath(path) && !UrlPath.IsRooted(path) && !Uri.IsWellFormedUriString(path, UriKind.Absolute))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.ScriptResourceDefinition_InvalidPath, new object[]
				{
					path
				}));
			}
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x000199E9 File Offset: 0x00017BE9
		public ScriptResourceDefinition GetDefinition(string name)
		{
			return this.GetDefinition(name, AssemblyCache.SystemWebExtensions);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x000199F8 File Offset: 0x00017BF8
		public ScriptResourceDefinition GetDefinition(string name, Assembly assembly)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(AtlasWeb.Common_NullOrEmpty, "name");
			}
			assembly = ScriptResourceMapping.NormalizeAssembly(assembly);
			ScriptResourceDefinition result;
			this._definitions.TryGetValue(new Tuple<string, Assembly>(name, assembly), out result);
			return result;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00019A3C File Offset: 0x00017C3C
		public ScriptResourceDefinition GetDefinition(ScriptReference scriptReference)
		{
			if (scriptReference == null)
			{
				throw new ArgumentNullException("scriptReference");
			}
			string name = scriptReference.Name;
			ScriptResourceDefinition result = null;
			if (!string.IsNullOrEmpty(name))
			{
				Assembly assembly = scriptReference.GetAssembly();
				result = ScriptManager.ScriptResourceMapping.GetDefinition(name, assembly);
			}
			return result;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00019A7F File Offset: 0x00017C7F
		public ScriptResourceDefinition RemoveDefinition(string name)
		{
			return this.RemoveDefinition(name, AssemblyCache.SystemWebExtensions);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00019A90 File Offset: 0x00017C90
		public ScriptResourceDefinition RemoveDefinition(string name, Assembly assembly)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(AtlasWeb.Common_NullOrEmpty, "name");
			}
			assembly = ScriptResourceMapping.NormalizeAssembly(assembly);
			ScriptResourceDefinition result;
			this._definitions.TryRemove(new Tuple<string, Assembly>(name, assembly), out result);
			return result;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00019AD3 File Offset: 0x00017CD3
		IScriptResourceDefinition IScriptResourceMapping.GetDefinition(string name)
		{
			return this.GetDefinition(name);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00019ADC File Offset: 0x00017CDC
		IScriptResourceDefinition IScriptResourceMapping.GetDefinition(string name, Assembly assembly)
		{
			return this.GetDefinition(name, assembly);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00019AE6 File Offset: 0x00017CE6
		private static Assembly NormalizeAssembly(Assembly assembly)
		{
			if (assembly != null && AssemblyCache.IsAjaxFrameworkAssembly(assembly))
			{
				assembly = null;
			}
			return assembly;
		}

		// Token: 0x040001F2 RID: 498
		private readonly ConcurrentDictionary<Tuple<string, Assembly>, ScriptResourceDefinition> _definitions = new ConcurrentDictionary<Tuple<string, Assembly>, ScriptResourceDefinition>();
	}
}
