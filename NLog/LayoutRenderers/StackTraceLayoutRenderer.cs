using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000F1 RID: 241
	[ThreadAgnostic]
	[LayoutRenderer("stacktrace")]
	public class StackTraceLayoutRenderer : LayoutRenderer, IUsesStackTrace
	{
		// Token: 0x060006E0 RID: 1760 RVA: 0x0000F677 File Offset: 0x0000D877
		public StackTraceLayoutRenderer()
		{
			this.Separator = " => ";
			this.TopFrames = 3;
			this.Format = StackTraceFormat.Flat;
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0000F698 File Offset: 0x0000D898
		// (set) Token: 0x060006E2 RID: 1762 RVA: 0x0000F6A0 File Offset: 0x0000D8A0
		[DefaultValue("Flat")]
		public StackTraceFormat Format { get; set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0000F6A9 File Offset: 0x0000D8A9
		// (set) Token: 0x060006E4 RID: 1764 RVA: 0x0000F6B1 File Offset: 0x0000D8B1
		[DefaultValue(3)]
		public int TopFrames { get; set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x0000F6BA File Offset: 0x0000D8BA
		// (set) Token: 0x060006E6 RID: 1766 RVA: 0x0000F6C2 File Offset: 0x0000D8C2
		[DefaultValue(0)]
		public int SkipFrames { get; set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x0000F6CB File Offset: 0x0000D8CB
		// (set) Token: 0x060006E8 RID: 1768 RVA: 0x0000F6D3 File Offset: 0x0000D8D3
		[DefaultValue(" => ")]
		public string Separator { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x0000F6DC File Offset: 0x0000D8DC
		StackTraceUsage IUsesStackTrace.StackTraceUsage
		{
			get
			{
				return StackTraceUsage.WithoutSource;
			}
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0000F6E0 File Offset: 0x0000D8E0
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			bool flag = true;
			int num = logEvent.UserStackFrameNumber + this.TopFrames - 1;
			if (num >= logEvent.StackTrace.FrameCount)
			{
				num = logEvent.StackTrace.FrameCount - 1;
			}
			int num2 = logEvent.UserStackFrameNumber + this.SkipFrames;
			switch (this.Format)
			{
			case StackTraceFormat.Raw:
				for (int i = num; i >= num2; i--)
				{
					StackFrame frame = logEvent.StackTrace.GetFrame(i);
					builder.Append(frame.ToString());
				}
				return;
			case StackTraceFormat.Flat:
				for (int j = num; j >= num2; j--)
				{
					StackFrame frame2 = logEvent.StackTrace.GetFrame(j);
					if (!flag)
					{
						builder.Append(this.Separator);
					}
					Type declaringType = frame2.GetMethod().DeclaringType;
					if (declaringType != null)
					{
						builder.Append(declaringType.Name);
					}
					else
					{
						builder.Append("<no type>");
					}
					builder.Append(".");
					builder.Append(frame2.GetMethod().Name);
					flag = false;
				}
				return;
			case StackTraceFormat.DetailedFlat:
				for (int k = num; k >= num2; k--)
				{
					StackFrame frame3 = logEvent.StackTrace.GetFrame(k);
					if (!flag)
					{
						builder.Append(this.Separator);
					}
					builder.Append("[");
					builder.Append(frame3.GetMethod());
					builder.Append("]");
					flag = false;
				}
				return;
			default:
				return;
			}
		}
	}
}
