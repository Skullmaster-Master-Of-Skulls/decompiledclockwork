using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Telerik.Web.UI.PivotGrid.Core.Engine
{
	// Token: 0x02000D3C RID: 3388
	internal class ParallelState
	{
		// Token: 0x1700282A RID: 10282
		// (get) Token: 0x06007DDF RID: 32223 RVA: 0x001CC2DD File Offset: 0x001CA4DD
		// (set) Token: 0x06007DE0 RID: 32224 RVA: 0x001CC2E5 File Offset: 0x001CA4E5
		public TaskScheduler TaskScheduler { get; set; }

		// Token: 0x1700282B RID: 10283
		// (get) Token: 0x06007DE1 RID: 32225 RVA: 0x001CC2EE File Offset: 0x001CA4EE
		// (set) Token: 0x06007DE2 RID: 32226 RVA: 0x001CC2F6 File Offset: 0x001CA4F6
		public CancellationTokenSource CancellationTokenSource { get; set; }

		// Token: 0x1700282C RID: 10284
		// (get) Token: 0x06007DE3 RID: 32227 RVA: 0x001CC2FF File Offset: 0x001CA4FF
		public CancellationToken CancellationToken
		{
			get
			{
				return this.CancellationTokenSource.Token;
			}
		}

		// Token: 0x1700282D RID: 10285
		// (get) Token: 0x06007DE4 RID: 32228 RVA: 0x001CC30C File Offset: 0x001CA50C
		// (set) Token: 0x06007DE5 RID: 32229 RVA: 0x001CC314 File Offset: 0x001CA514
		public IReadOnlyList<GroupDescription> RowGroupDescriptions { get; set; }

		// Token: 0x1700282E RID: 10286
		// (get) Token: 0x06007DE6 RID: 32230 RVA: 0x001CC31D File Offset: 0x001CA51D
		// (set) Token: 0x06007DE7 RID: 32231 RVA: 0x001CC325 File Offset: 0x001CA525
		public IReadOnlyList<GroupDescription> ColumnGroupDescriptions { get; set; }

		// Token: 0x1700282F RID: 10287
		// (get) Token: 0x06007DE8 RID: 32232 RVA: 0x001CC32E File Offset: 0x001CA52E
		// (set) Token: 0x06007DE9 RID: 32233 RVA: 0x001CC336 File Offset: 0x001CA536
		public IReadOnlyList<IAggregateDescription> AggregateDescriptions { get; set; }

		// Token: 0x17002830 RID: 10288
		// (get) Token: 0x06007DEA RID: 32234 RVA: 0x001CC33F File Offset: 0x001CA53F
		// (set) Token: 0x06007DEB RID: 32235 RVA: 0x001CC347 File Offset: 0x001CA547
		public IReadOnlyList<FilterDescription> FilterDescriptions { get; set; }

		// Token: 0x17002831 RID: 10289
		// (get) Token: 0x06007DEC RID: 32236 RVA: 0x001CC350 File Offset: 0x001CA550
		// (set) Token: 0x06007DED RID: 32237 RVA: 0x001CC358 File Offset: 0x001CA558
		public int AggregateDescriptionCount { get; set; }

		// Token: 0x17002832 RID: 10290
		// (get) Token: 0x06007DEE RID: 32238 RVA: 0x001CC361 File Offset: 0x001CA561
		// (set) Token: 0x06007DEF RID: 32239 RVA: 0x001CC37D File Offset: 0x001CA57D
		public AggregateDescriptionInfo[] AggregateDescriptionInfos
		{
			get
			{
				if (this.aggregateDescriptionInfos == null)
				{
					this.aggregateDescriptionInfos = new AggregateDescriptionInfo[0];
				}
				return this.aggregateDescriptionInfos;
			}
			set
			{
				this.aggregateDescriptionInfos = value;
			}
		}

		// Token: 0x17002833 RID: 10291
		// (get) Token: 0x06007DF0 RID: 32240 RVA: 0x001CC386 File Offset: 0x001CA586
		// (set) Token: 0x06007DF1 RID: 32241 RVA: 0x001CC38E File Offset: 0x001CA58E
		public CultureInfo Culture { get; set; }

		// Token: 0x17002834 RID: 10292
		// (get) Token: 0x06007DF2 RID: 32242 RVA: 0x001CC398 File Offset: 0x001CA598
		public bool IsEmpty
		{
			get
			{
				return (this.RowGroupDescriptions.Count == 0 && this.ColumnGroupDescriptions.Count == 0 && this.AggregateDescriptionCount == 0) || this.ItemsSource == null;
			}
		}

		// Token: 0x17002835 RID: 10293
		// (get) Token: 0x06007DF3 RID: 32243 RVA: 0x001CC3D5 File Offset: 0x001CA5D5
		// (set) Token: 0x06007DF4 RID: 32244 RVA: 0x001CC3DD File Offset: 0x001CA5DD
		public IValueProvider ValueProvider { get; set; }

		// Token: 0x17002836 RID: 10294
		// (get) Token: 0x06007DF5 RID: 32245 RVA: 0x001CC3E6 File Offset: 0x001CA5E6
		// (set) Token: 0x06007DF6 RID: 32246 RVA: 0x001CC3EE File Offset: 0x001CA5EE
		internal IDataSourceView ItemsSource { get; set; }

		// Token: 0x17002837 RID: 10295
		// (get) Token: 0x06007DF7 RID: 32247 RVA: 0x001CC3F7 File Offset: 0x001CA5F7
		// (set) Token: 0x06007DF8 RID: 32248 RVA: 0x001CC3FF File Offset: 0x001CA5FF
		public int MaxDegreeOfParallelism { get; set; }

		// Token: 0x06007DF9 RID: 32249 RVA: 0x001CC408 File Offset: 0x001CA608
		internal object GetItem(int index)
		{
			return this.ItemsSource[index];
		}

		// Token: 0x0400229E RID: 8862
		private AggregateDescriptionInfo[] aggregateDescriptionInfos;
	}
}
