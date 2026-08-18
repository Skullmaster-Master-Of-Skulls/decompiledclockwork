using System;
using System.Diagnostics;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000E4 RID: 228
	[LayoutRenderer("performancecounter")]
	public class PerformanceCounterLayoutRenderer : LayoutRenderer
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x0000EC5B File Offset: 0x0000CE5B
		// (set) Token: 0x06000695 RID: 1685 RVA: 0x0000EC63 File Offset: 0x0000CE63
		[RequiredParameter]
		public string Category { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x0000EC6C File Offset: 0x0000CE6C
		// (set) Token: 0x06000697 RID: 1687 RVA: 0x0000EC74 File Offset: 0x0000CE74
		[RequiredParameter]
		public string Counter { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x0000EC7D File Offset: 0x0000CE7D
		// (set) Token: 0x06000699 RID: 1689 RVA: 0x0000EC85 File Offset: 0x0000CE85
		public string Instance { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0000EC8E File Offset: 0x0000CE8E
		// (set) Token: 0x0600069B RID: 1691 RVA: 0x0000EC96 File Offset: 0x0000CE96
		public string MachineName { get; set; }

		// Token: 0x0600069C RID: 1692 RVA: 0x0000ECA0 File Offset: 0x0000CEA0
		protected override void InitializeLayoutRenderer()
		{
			base.InitializeLayoutRenderer();
			if (this.MachineName != null)
			{
				this.perfCounter = new PerformanceCounter(this.Category, this.Counter, this.Instance, this.MachineName);
				return;
			}
			this.perfCounter = new PerformanceCounter(this.Category, this.Counter, this.Instance, true);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0000ECFD File Offset: 0x0000CEFD
		protected override void CloseLayoutRenderer()
		{
			base.CloseLayoutRenderer();
			if (this.perfCounter != null)
			{
				this.perfCounter.Close();
				this.perfCounter = null;
			}
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0000ED20 File Offset: 0x0000CF20
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			IFormatProvider formatProvider = base.GetFormatProvider(logEvent, null);
			builder.Append(this.perfCounter.NextValue().ToString(formatProvider));
		}

		// Token: 0x040001AC RID: 428
		private PerformanceCounter perfCounter;
	}
}
