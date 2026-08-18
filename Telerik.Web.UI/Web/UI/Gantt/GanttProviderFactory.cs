using System;
using System.Configuration;
using System.Web.Configuration;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x020004A5 RID: 1189
	internal static class GanttProviderFactory
	{
		// Token: 0x060029FF RID: 10751 RVA: 0x0008759E File Offset: 0x0008579E
		public static GanttProviderBase GetProvider(RadGantt owner, string name)
		{
			if (name == "Integrated")
			{
				return new DataSourceViewGanttProvider(owner);
			}
			return GanttProviderFactory.GetProvider(name);
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x000875BC File Offset: 0x000857BC
		public static GanttProviderBase GetProvider(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("Provider name cannot be empty string");
			}
			GanttProviderFactory.LoadProviders();
			GanttProviderBase result;
			lock (GanttProviderFactory.locker)
			{
				GanttProviderBase ganttProviderBase = GanttProviderFactory.ganttProviders[name];
				if (ganttProviderBase == null)
				{
					throw new ArgumentException("Provider '" + name + "' has not been declared in web.config.");
				}
				result = ganttProviderBase;
			}
			return result;
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x00087638 File Offset: 0x00085838
		public static void LoadProviders()
		{
			if (GanttProviderFactory.ganttProviders == null)
			{
				lock (GanttProviderFactory.locker)
				{
					if (GanttProviderFactory.ganttProviders == null)
					{
						GanttProviderFactory.ganttProviders = new GanttProviderCollection();
						RadGanttConfigurationSection radGanttConfigurationSection = (RadGanttConfigurationSection)ConfigurationManager.GetSection("telerik.web.ui/radGantt");
						if (radGanttConfigurationSection != null)
						{
							ProvidersHelper.InstantiateProviders(radGanttConfigurationSection.TaskProviders, GanttProviderFactory.ganttProviders, typeof(GanttProviderBase));
						}
					}
				}
			}
		}

		// Token: 0x04000ADD RID: 2781
		private static GanttProviderCollection ganttProviders;

		// Token: 0x04000ADE RID: 2782
		private static readonly object locker = new object();
	}
}
