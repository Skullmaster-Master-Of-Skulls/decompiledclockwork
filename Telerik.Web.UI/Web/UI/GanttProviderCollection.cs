using System;
using System.Configuration.Provider;

namespace Telerik.Web.UI
{
	// Token: 0x020002F1 RID: 753
	public class GanttProviderCollection : ProviderCollection
	{
		// Token: 0x170008B1 RID: 2225
		public GanttProviderBase this[string name]
		{
			get
			{
				return (GanttProviderBase)base[name];
			}
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x00054DDC File Offset: 0x00052FDC
		public override void Add(ProviderBase provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (!(provider is GanttProviderBase))
			{
				throw new ArgumentException("Invalid provider type", "provider");
			}
			base.Add(provider);
		}
	}
}
