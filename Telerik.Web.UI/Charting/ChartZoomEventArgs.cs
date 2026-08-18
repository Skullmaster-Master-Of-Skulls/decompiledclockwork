using System;

namespace Telerik.Charting
{
	// Token: 0x020016DB RID: 5851
	public class ChartZoomEventArgs : EventArgs
	{
		// Token: 0x17004554 RID: 17748
		// (get) Token: 0x0600E2DF RID: 58079 RVA: 0x00324939 File Offset: 0x00322B39
		// (set) Token: 0x0600E2E0 RID: 58080 RVA: 0x00324941 File Offset: 0x00322B41
		public double XScaleOld
		{
			get
			{
				return this.xScaleOld;
			}
			set
			{
				this.xScaleOld = value;
			}
		}

		// Token: 0x17004555 RID: 17749
		// (get) Token: 0x0600E2E1 RID: 58081 RVA: 0x0032494A File Offset: 0x00322B4A
		// (set) Token: 0x0600E2E2 RID: 58082 RVA: 0x00324952 File Offset: 0x00322B52
		public double XScaleNew
		{
			get
			{
				return this.xScaleNew;
			}
			set
			{
				this.xScaleNew = value;
			}
		}

		// Token: 0x17004556 RID: 17750
		// (get) Token: 0x0600E2E3 RID: 58083 RVA: 0x0032495B File Offset: 0x00322B5B
		// (set) Token: 0x0600E2E4 RID: 58084 RVA: 0x00324963 File Offset: 0x00322B63
		public double YScaleOld
		{
			get
			{
				return this.yScaleOld;
			}
			set
			{
				this.xScaleOld = value;
			}
		}

		// Token: 0x17004557 RID: 17751
		// (get) Token: 0x0600E2E5 RID: 58085 RVA: 0x0032496C File Offset: 0x00322B6C
		// (set) Token: 0x0600E2E6 RID: 58086 RVA: 0x00324974 File Offset: 0x00322B74
		public double YScaleNew
		{
			get
			{
				return this.yScaleNew;
			}
			set
			{
				this.yScaleNew = value;
			}
		}

		// Token: 0x0600E2E7 RID: 58087 RVA: 0x0032497D File Offset: 0x00322B7D
		public ChartZoomEventArgs(double xScaleOld, double xScaleNew, double yScaleOld, double yScaleNew)
		{
			this.xScaleOld = xScaleOld;
			this.xScaleNew = xScaleNew;
			this.yScaleOld = yScaleOld;
			this.yScaleNew = yScaleNew;
		}

		// Token: 0x04004177 RID: 16759
		private double xScaleOld;

		// Token: 0x04004178 RID: 16760
		private double xScaleNew;

		// Token: 0x04004179 RID: 16761
		private double yScaleOld;

		// Token: 0x0400417A RID: 16762
		private double yScaleNew;
	}
}
