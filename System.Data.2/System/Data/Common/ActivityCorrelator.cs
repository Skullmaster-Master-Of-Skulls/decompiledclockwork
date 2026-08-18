using System;
using System.Globalization;

namespace System.Data.Common
{
	// Token: 0x02000333 RID: 819
	internal static class ActivityCorrelator
	{
		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06003375 RID: 13173 RVA: 0x0013D7F8 File Offset: 0x0013CBF8
		internal static ActivityCorrelator.ActivityId Current
		{
			get
			{
				if (ActivityCorrelator.tlsActivity == null)
				{
					ActivityCorrelator.tlsActivity = new ActivityCorrelator.ActivityId();
				}
				return new ActivityCorrelator.ActivityId(ActivityCorrelator.tlsActivity);
			}
		}

		// Token: 0x06003376 RID: 13174 RVA: 0x0013D820 File Offset: 0x0013CC20
		internal static ActivityCorrelator.ActivityId Next()
		{
			if (ActivityCorrelator.tlsActivity == null)
			{
				ActivityCorrelator.tlsActivity = new ActivityCorrelator.ActivityId();
			}
			ActivityCorrelator.tlsActivity.Increment();
			return new ActivityCorrelator.ActivityId(ActivityCorrelator.tlsActivity);
		}

		// Token: 0x04001E12 RID: 7698
		internal const Bid.ApiGroup CorrelationTracePoints = Bid.ApiGroup.Correlation;

		// Token: 0x04001E13 RID: 7699
		[ThreadStatic]
		private static ActivityCorrelator.ActivityId tlsActivity;

		// Token: 0x02000462 RID: 1122
		internal class ActivityId
		{
			// Token: 0x17000880 RID: 2176
			// (get) Token: 0x060036EE RID: 14062 RVA: 0x00149C50 File Offset: 0x00149050
			// (set) Token: 0x060036EF RID: 14063 RVA: 0x00149C64 File Offset: 0x00149064
			internal Guid Id { get; private set; }

			// Token: 0x17000881 RID: 2177
			// (get) Token: 0x060036F0 RID: 14064 RVA: 0x00149C78 File Offset: 0x00149078
			// (set) Token: 0x060036F1 RID: 14065 RVA: 0x00149C8C File Offset: 0x0014908C
			internal uint Sequence { get; private set; }

			// Token: 0x060036F2 RID: 14066 RVA: 0x00149CA0 File Offset: 0x001490A0
			internal ActivityId()
			{
				this.Id = Guid.NewGuid();
				this.Sequence = 0U;
			}

			// Token: 0x060036F3 RID: 14067 RVA: 0x00149CC8 File Offset: 0x001490C8
			internal ActivityId(ActivityCorrelator.ActivityId activity)
			{
				this.Id = activity.Id;
				this.Sequence = activity.Sequence;
			}

			// Token: 0x060036F4 RID: 14068 RVA: 0x00149CF4 File Offset: 0x001490F4
			internal void Increment()
			{
				uint sequence = this.Sequence + 1U;
				this.Sequence = sequence;
			}

			// Token: 0x060036F5 RID: 14069 RVA: 0x00149D14 File Offset: 0x00149114
			public override string ToString()
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
				{
					this.Id,
					this.Sequence
				});
			}
		}
	}
}
