using System;
using System.Collections;
using log4net.Repository;

namespace log4net.Plugin
{
	// Token: 0x020000BF RID: 191
	public sealed class PluginMap
	{
		// Token: 0x0600058C RID: 1420 RVA: 0x0001157F File Offset: 0x0000F77F
		public PluginMap(ILoggerRepository repository)
		{
			this.m_repository = repository;
		}

		// Token: 0x17000133 RID: 307
		public IPlugin this[string name]
		{
			get
			{
				if (name == null)
				{
					throw new ArgumentNullException("name");
				}
				IPlugin result;
				lock (this)
				{
					result = (IPlugin)this.m_mapName2Plugin[name];
				}
				return result;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x000115F4 File Offset: 0x0000F7F4
		public PluginCollection AllPlugins
		{
			get
			{
				PluginCollection result;
				lock (this)
				{
					result = new PluginCollection(this.m_mapName2Plugin.Values);
				}
				return result;
			}
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0001163C File Offset: 0x0000F83C
		public void Add(IPlugin plugin)
		{
			if (plugin == null)
			{
				throw new ArgumentNullException("plugin");
			}
			IPlugin plugin2 = null;
			lock (this)
			{
				plugin2 = (this.m_mapName2Plugin[plugin.Name] as IPlugin);
				this.m_mapName2Plugin[plugin.Name] = plugin;
			}
			if (plugin2 != null)
			{
				plugin2.Shutdown();
			}
			plugin.Attach(this.m_repository);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x000116C0 File Offset: 0x0000F8C0
		public void Remove(IPlugin plugin)
		{
			if (plugin == null)
			{
				throw new ArgumentNullException("plugin");
			}
			lock (this)
			{
				this.m_mapName2Plugin.Remove(plugin.Name);
			}
		}

		// Token: 0x04000243 RID: 579
		private readonly Hashtable m_mapName2Plugin = new Hashtable();

		// Token: 0x04000244 RID: 580
		private readonly ILoggerRepository m_repository;
	}
}
