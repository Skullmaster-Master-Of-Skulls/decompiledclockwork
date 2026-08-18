using System;
using System.Data.Entity.Core.Common.Utils;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration
{
	// Token: 0x02000421 RID: 1057
	internal sealed class ConfigViewGenerator : InternalBase
	{
		// Token: 0x060026EE RID: 9966 RVA: 0x000BD8E4 File Offset: 0x000BBAE4
		internal ConfigViewGenerator()
		{
			this.m_watch = new Stopwatch();
			this.m_singleWatch = new Stopwatch();
			int num = Enum.GetNames(typeof(PerfType)).Length;
			this.m_breakdownTimes = new TimeSpan[num];
			this.m_traceLevel = ViewGenTraceLevel.None;
			this.m_generateUpdateViews = false;
			this.StartWatch();
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x060026EF RID: 9967 RVA: 0x000BD94D File Offset: 0x000BBB4D
		// (set) Token: 0x060026F0 RID: 9968 RVA: 0x000BD955 File Offset: 0x000BBB55
		internal bool GenerateEsql { get; set; }

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x060026F1 RID: 9969 RVA: 0x000BD95E File Offset: 0x000BBB5E
		internal TimeSpan[] BreakdownTimes
		{
			get
			{
				return this.m_breakdownTimes;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x060026F2 RID: 9970 RVA: 0x000BD966 File Offset: 0x000BBB66
		// (set) Token: 0x060026F3 RID: 9971 RVA: 0x000BD96E File Offset: 0x000BBB6E
		internal ViewGenTraceLevel TraceLevel
		{
			get
			{
				return this.m_traceLevel;
			}
			set
			{
				this.m_traceLevel = value;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x060026F4 RID: 9972 RVA: 0x000BD977 File Offset: 0x000BBB77
		// (set) Token: 0x060026F5 RID: 9973 RVA: 0x000BD97F File Offset: 0x000BBB7F
		internal bool IsValidationEnabled
		{
			get
			{
				return this.m_enableValidation;
			}
			set
			{
				this.m_enableValidation = value;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x060026F6 RID: 9974 RVA: 0x000BD988 File Offset: 0x000BBB88
		// (set) Token: 0x060026F7 RID: 9975 RVA: 0x000BD990 File Offset: 0x000BBB90
		internal bool GenerateUpdateViews
		{
			get
			{
				return this.m_generateUpdateViews;
			}
			set
			{
				this.m_generateUpdateViews = value;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x060026F8 RID: 9976 RVA: 0x000BD999 File Offset: 0x000BBB99
		// (set) Token: 0x060026F9 RID: 9977 RVA: 0x000BD9A1 File Offset: 0x000BBBA1
		internal bool GenerateViewsForEachType { get; set; }

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x060026FA RID: 9978 RVA: 0x000BD9AA File Offset: 0x000BBBAA
		internal bool IsViewTracing
		{
			get
			{
				return this.IsTraceAllowed(ViewGenTraceLevel.ViewsOnly);
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x060026FB RID: 9979 RVA: 0x000BD9B3 File Offset: 0x000BBBB3
		internal bool IsNormalTracing
		{
			get
			{
				return this.IsTraceAllowed(ViewGenTraceLevel.Normal);
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x060026FC RID: 9980 RVA: 0x000BD9BC File Offset: 0x000BBBBC
		internal bool IsVerboseTracing
		{
			get
			{
				return this.IsTraceAllowed(ViewGenTraceLevel.Verbose);
			}
		}

		// Token: 0x060026FD RID: 9981 RVA: 0x000BD9C5 File Offset: 0x000BBBC5
		private void StartWatch()
		{
			this.m_watch.Start();
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x000BD9D2 File Offset: 0x000BBBD2
		internal void StartSingleWatch(PerfType perfType)
		{
			this.m_singleWatch.Start();
			this.m_singlePerfOp = perfType;
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x000BD9E8 File Offset: 0x000BBBE8
		internal void StopSingleWatch(PerfType perfType)
		{
			TimeSpan elapsed = this.m_singleWatch.Elapsed;
			this.m_singleWatch.Stop();
			this.m_singleWatch.Reset();
			this.BreakdownTimes[(int)perfType] = this.BreakdownTimes[(int)perfType].Add(elapsed);
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x000BDA3C File Offset: 0x000BBC3C
		internal void SetTimeForFinishedActivity(PerfType perfType)
		{
			TimeSpan elapsed = this.m_watch.Elapsed;
			this.BreakdownTimes[(int)perfType] = this.BreakdownTimes[(int)perfType].Add(elapsed);
			this.m_watch.Reset();
			this.m_watch.Start();
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x000BDA90 File Offset: 0x000BBC90
		internal bool IsTraceAllowed(ViewGenTraceLevel traceLevel)
		{
			return this.TraceLevel >= traceLevel;
		}

		// Token: 0x06002702 RID: 9986 RVA: 0x000BDAA0 File Offset: 0x000BBCA0
		internal override void ToCompactString(StringBuilder builder)
		{
			StringUtil.FormatStringBuilder(builder, "Trace Switch: {0}", new object[]
			{
				this.m_traceLevel
			});
		}

		// Token: 0x04000EA2 RID: 3746
		private ViewGenTraceLevel m_traceLevel;

		// Token: 0x04000EA3 RID: 3747
		private readonly TimeSpan[] m_breakdownTimes;

		// Token: 0x04000EA4 RID: 3748
		private readonly Stopwatch m_watch;

		// Token: 0x04000EA5 RID: 3749
		private readonly Stopwatch m_singleWatch;

		// Token: 0x04000EA6 RID: 3750
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
		private PerfType m_singlePerfOp;

		// Token: 0x04000EA7 RID: 3751
		private bool m_enableValidation = true;

		// Token: 0x04000EA8 RID: 3752
		private bool m_generateUpdateViews = true;
	}
}
