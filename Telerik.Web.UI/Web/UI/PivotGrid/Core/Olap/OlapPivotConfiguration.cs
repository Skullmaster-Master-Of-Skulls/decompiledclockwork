using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D0E RID: 3342
	internal class OlapPivotConfiguration : IOlapPivotConfiguration
	{
		// Token: 0x06007C88 RID: 31880 RVA: 0x001C9BD8 File Offset: 0x001C7DD8
		internal OlapPivotConfiguration()
		{
			this.PivotFilterDescriptions = new List<OlapFilterDescription>();
			this.PivotAggregateDescriptions = new List<OlapAggregateDescription>();
			this.PivotColumnGroupDescriptions = new List<OlapGroupDescription>();
			this.PivotRowGroupDescriptions = new List<OlapGroupDescription>();
		}

		// Token: 0x170027B6 RID: 10166
		// (get) Token: 0x06007C89 RID: 31881 RVA: 0x001C9C0C File Offset: 0x001C7E0C
		// (set) Token: 0x06007C8A RID: 31882 RVA: 0x001C9C14 File Offset: 0x001C7E14
		public IList<OlapAggregateDescription> PivotAggregateDescriptions { get; private set; }

		// Token: 0x170027B7 RID: 10167
		// (get) Token: 0x06007C8B RID: 31883 RVA: 0x001C9C1D File Offset: 0x001C7E1D
		// (set) Token: 0x06007C8C RID: 31884 RVA: 0x001C9C25 File Offset: 0x001C7E25
		public IList<OlapGroupDescription> PivotRowGroupDescriptions { get; private set; }

		// Token: 0x170027B8 RID: 10168
		// (get) Token: 0x06007C8D RID: 31885 RVA: 0x001C9C2E File Offset: 0x001C7E2E
		// (set) Token: 0x06007C8E RID: 31886 RVA: 0x001C9C36 File Offset: 0x001C7E36
		public IList<OlapGroupDescription> PivotColumnGroupDescriptions { get; private set; }

		// Token: 0x170027B9 RID: 10169
		// (get) Token: 0x06007C8F RID: 31887 RVA: 0x001C9C3F File Offset: 0x001C7E3F
		// (set) Token: 0x06007C90 RID: 31888 RVA: 0x001C9C47 File Offset: 0x001C7E47
		public IList<OlapFilterDescription> PivotFilterDescriptions { get; private set; }

		// Token: 0x06007C91 RID: 31889 RVA: 0x001C9C50 File Offset: 0x001C7E50
		public static OlapPivotConfiguration FromDataProviderCloned(IDataProvider dataProvider)
		{
			if (dataProvider == null)
			{
				throw new NotImplementedException("Data provider cannot be null");
			}
			OlapPivotConfiguration olapPivotConfiguration = new OlapPivotConfiguration();
			IPivotSettings settings = dataProvider.Settings;
			OlapPivotConfiguration.CloneAndAddPivotAggregateDescriptions(settings, olapPivotConfiguration);
			OlapPivotConfiguration.CloneAndAddPivotRowDescriptions(settings, olapPivotConfiguration);
			OlapPivotConfiguration.CloneAndAddPivotColumnDescriptions(settings, olapPivotConfiguration);
			OlapPivotConfiguration.CloneAndAddPivotFilterDescriptions(settings, olapPivotConfiguration);
			return olapPivotConfiguration;
		}

		// Token: 0x06007C92 RID: 31890 RVA: 0x001C9C98 File Offset: 0x001C7E98
		private static void CloneAndAddPivotAggregateDescriptions(IPivotSettings settings, OlapPivotConfiguration configuration)
		{
			List<OlapAggregateDescription> list = new List<OlapAggregateDescription>();
			foreach (OlapAggregateDescription item in settings.AggregateDescriptions.OfType<OlapAggregateDescription>())
			{
				list.Add(item);
			}
			configuration.PivotAggregateDescriptions = new List<OlapAggregateDescription>(list);
		}

		// Token: 0x06007C93 RID: 31891 RVA: 0x001C9CFC File Offset: 0x001C7EFC
		private static void CloneAndAddPivotRowDescriptions(IPivotSettings settings, OlapPivotConfiguration configuration)
		{
			List<OlapGroupDescription> list = new List<OlapGroupDescription>();
			foreach (OlapGroupDescription item in settings.RowGroupDescriptions.OfType<OlapGroupDescription>())
			{
				list.Add(item);
			}
			configuration.PivotRowGroupDescriptions = new List<OlapGroupDescription>(list);
		}

		// Token: 0x06007C94 RID: 31892 RVA: 0x001C9D60 File Offset: 0x001C7F60
		private static void CloneAndAddPivotColumnDescriptions(IPivotSettings settings, OlapPivotConfiguration configuration)
		{
			List<OlapGroupDescription> list = new List<OlapGroupDescription>();
			foreach (OlapGroupDescription item in settings.ColumnGroupDescriptions.OfType<OlapGroupDescription>())
			{
				list.Add(item);
			}
			configuration.PivotColumnGroupDescriptions = new List<OlapGroupDescription>(list);
		}

		// Token: 0x06007C95 RID: 31893 RVA: 0x001C9DC4 File Offset: 0x001C7FC4
		private static void CloneAndAddPivotFilterDescriptions(IPivotSettings settings, OlapPivotConfiguration configuration)
		{
			List<OlapFilterDescription> list = new List<OlapFilterDescription>();
			foreach (OlapFilterDescription item in settings.FilterDescriptions.OfType<OlapFilterDescription>())
			{
				list.Add(item);
			}
			configuration.PivotFilterDescriptions = new List<OlapFilterDescription>(list);
		}
	}
}
