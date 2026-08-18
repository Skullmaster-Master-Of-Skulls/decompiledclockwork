using System;
using System.Data.Common.Utils;
using System.Diagnostics;
using System.Text;

namespace System.Data.Mapping.ViewGeneration
{
	// Token: 0x02000267 RID: 615
	internal sealed class ConfigViewGenerator : InternalBase
	{
		// Token: 0x060025C7 RID: 9671 RVA: 0x0008E940 File Offset: 0x0008CB40
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

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x060025C8 RID: 9672 RVA: 0x0008E9A9 File Offset: 0x0008CBA9
		// (set) Token: 0x060025C9 RID: 9673 RVA: 0x0008E9B1 File Offset: 0x0008CBB1
		internal bool GenerateEsql
		{
			get
			{
				return this.m_generateEsql;
			}
			set
			{
				this.m_generateEsql = value;
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x060025CA RID: 9674 RVA: 0x0008E9BA File Offset: 0x0008CBBA
		internal TimeSpan[] BreakdownTimes
		{
			get
			{
				return this.m_breakdownTimes;
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x060025CB RID: 9675 RVA: 0x0008E9C2 File Offset: 0x0008CBC2
		// (set) Token: 0x060025CC RID: 9676 RVA: 0x0008E9CA File Offset: 0x0008CBCA
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

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x060025CD RID: 9677 RVA: 0x0008E9D3 File Offset: 0x0008CBD3
		// (set) Token: 0x060025CE RID: 9678 RVA: 0x0008E9DB File Offset: 0x0008CBDB
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

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x060025CF RID: 9679 RVA: 0x0008E9E4 File Offset: 0x0008CBE4
		// (set) Token: 0x060025D0 RID: 9680 RVA: 0x0008E9EC File Offset: 0x0008CBEC
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

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x060025D1 RID: 9681 RVA: 0x0008E9F5 File Offset: 0x0008CBF5
		// (set) Token: 0x060025D2 RID: 9682 RVA: 0x0008E9FD File Offset: 0x0008CBFD
		internal bool GenerateViewsForEachType
		{
			get
			{
				return this.m_generateViewsForEachType;
			}
			set
			{
				this.m_generateViewsForEachType = value;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x060025D3 RID: 9683 RVA: 0x0008EA06 File Offset: 0x0008CC06
		internal bool IsViewTracing
		{
			get
			{
				return this.IsTraceAllowed(ViewGenTraceLevel.ViewsOnly);
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x060025D4 RID: 9684 RVA: 0x0008EA0F File Offset: 0x0008CC0F
		internal bool IsNormalTracing
		{
			get
			{
				return this.IsTraceAllowed(ViewGenTraceLevel.Normal);
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x060025D5 RID: 9685 RVA: 0x0008EA18 File Offset: 0x0008CC18
		internal bool IsVerboseTracing
		{
			get
			{
				return this.IsTraceAllowed(ViewGenTraceLevel.Verbose);
			}
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x0008EA21 File Offset: 0x0008CC21
		private void StartWatch()
		{
			this.m_watch.Start();
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x0008EA2E File Offset: 0x0008CC2E
		internal void StartSingleWatch(PerfType perfType)
		{
			this.m_singleWatch.Start();
			this.m_singlePerfOp = perfType;
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x0008EA44 File Offset: 0x0008CC44
		internal void StopSingleWatch(PerfType perfType)
		{
			TimeSpan elapsed = this.m_singleWatch.Elapsed;
			this.m_singleWatch.Stop();
			this.m_singleWatch.Reset();
			this.BreakdownTimes[(int)perfType] = this.BreakdownTimes[(int)perfType].Add(elapsed);
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x0008EA94 File Offset: 0x0008CC94
		internal void SetTimeForFinishedActivity(PerfType perfType)
		{
			TimeSpan elapsed = this.m_watch.Elapsed;
			this.BreakdownTimes[(int)perfType] = this.BreakdownTimes[(int)perfType].Add(elapsed);
			this.m_watch.Reset();
			this.m_watch.Start();
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x0008EAE3 File Offset: 0x0008CCE3
		internal bool IsTraceAllowed(ViewGenTraceLevel traceLevel)
		{
			return this.TraceLevel >= traceLevel;
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x0008EAF1 File Offset: 0x0008CCF1
		internal override void ToCompactString(StringBuilder builder)
		{
			StringUtil.FormatStringBuilder(builder, "Trace Switch: {0}", new object[]
			{
				this.m_traceLevel
			});
		}

		// Token: 0x0400116C RID: 4460
		private bool m_generateViewsForEachType;

		// Token: 0x0400116D RID: 4461
		private ViewGenTraceLevel m_traceLevel;

		// Token: 0x0400116E RID: 4462
		private readonly TimeSpan[] m_breakdownTimes;

		// Token: 0x0400116F RID: 4463
		private Stopwatch m_watch;

		// Token: 0x04001170 RID: 4464
		private Stopwatch m_singleWatch;

		// Token: 0x04001171 RID: 4465
		private PerfType m_singlePerfOp;

		// Token: 0x04001172 RID: 4466
		private bool m_enableValidation = true;

		// Token: 0x04001173 RID: 4467
		private bool m_generateUpdateViews = true;

		// Token: 0x04001174 RID: 4468
		private bool m_generateEsql;
	}
}
