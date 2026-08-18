using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008B4 RID: 2228
	internal class CorrelationDataSourceHelper : ICorrelationDataSource
	{
		// Token: 0x060054ED RID: 21741 RVA: 0x001384D1 File Offset: 0x001366D1
		public CorrelationDataSourceHelper(ICollection<CorrelationDataDescription> dataSources)
		{
			if (dataSources.IsReadOnly)
			{
				this.dataSources = dataSources;
				return;
			}
			this.dataSources = new ReadOnlyCollection<CorrelationDataDescription>(new List<CorrelationDataDescription>(dataSources));
		}

		// Token: 0x060054EE RID: 21742 RVA: 0x001384FC File Offset: 0x001366FC
		private CorrelationDataSourceHelper(ICollection<CorrelationDataDescription> dataSource1, ICollection<CorrelationDataDescription> dataSource2)
		{
			List<CorrelationDataDescription> list = new List<CorrelationDataDescription>(dataSource1);
			foreach (CorrelationDataDescription item in dataSource2)
			{
				list.Add(item);
			}
			this.dataSources = new ReadOnlyCollection<CorrelationDataDescription>(list);
		}

		// Token: 0x060054EF RID: 21743 RVA: 0x00138560 File Offset: 0x00136760
		public static ICorrelationDataSource Combine(ICorrelationDataSource dataSource1, ICorrelationDataSource dataSource2)
		{
			if (dataSource1 == null)
			{
				return dataSource2;
			}
			if (dataSource2 == null)
			{
				return dataSource1;
			}
			return new CorrelationDataSourceHelper(dataSource1.DataSources, dataSource2.DataSources);
		}

		// Token: 0x170014DF RID: 5343
		// (get) Token: 0x060054F0 RID: 21744 RVA: 0x0013857D File Offset: 0x0013677D
		ICollection<CorrelationDataDescription> ICorrelationDataSource.DataSources
		{
			get
			{
				return this.dataSources;
			}
		}

		// Token: 0x0400334B RID: 13131
		private ICollection<CorrelationDataDescription> dataSources;
	}
}
