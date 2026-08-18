using System;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009DF RID: 2527
	internal class DataCapacityIndexer
	{
		// Token: 0x060060BB RID: 24763 RVA: 0x0012C822 File Offset: 0x0012AA22
		public DataCapacityIndexer(int codeVersion, Modes.ErrorCorrectionLevel errorCorrectionLevel)
		{
			this.Version = codeVersion;
			this.ErrorCorrection = errorCorrectionLevel;
		}

		// Token: 0x17001FD1 RID: 8145
		// (get) Token: 0x060060BC RID: 24764 RVA: 0x0012C838 File Offset: 0x0012AA38
		// (set) Token: 0x060060BD RID: 24765 RVA: 0x0012C840 File Offset: 0x0012AA40
		public int Version
		{
			get
			{
				return this.versionL;
			}
			set
			{
				this.versionL = value;
			}
		}

		// Token: 0x17001FD2 RID: 8146
		// (get) Token: 0x060060BE RID: 24766 RVA: 0x0012C849 File Offset: 0x0012AA49
		// (set) Token: 0x060060BF RID: 24767 RVA: 0x0012C851 File Offset: 0x0012AA51
		public Modes.ErrorCorrectionLevel ErrorCorrection
		{
			get
			{
				return this.errorCorrectionL;
			}
			set
			{
				this.errorCorrectionL = value;
			}
		}

		// Token: 0x060060C0 RID: 24768 RVA: 0x0012C85A File Offset: 0x0012AA5A
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataCapacityIndexer);
		}

		// Token: 0x060060C1 RID: 24769 RVA: 0x0012C868 File Offset: 0x0012AA68
		public bool Equals(DataCapacityIndexer obj)
		{
			return obj.Version == this.Version && obj.ErrorCorrection == this.ErrorCorrection;
		}

		// Token: 0x060060C2 RID: 24770 RVA: 0x0012C888 File Offset: 0x0012AA88
		public override int GetHashCode()
		{
			return this.Version * Convert.ToInt32(this.ErrorCorrection);
		}

		// Token: 0x0400178B RID: 6027
		private int versionL;

		// Token: 0x0400178C RID: 6028
		private Modes.ErrorCorrectionLevel errorCorrectionL;
	}
}
