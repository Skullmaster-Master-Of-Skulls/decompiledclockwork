using System;

namespace Ionic.Zip
{
	// Token: 0x0200003C RID: 60
	public class SelfExtractorSaveOptions
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00010620 File Offset: 0x0000E820
		// (set) Token: 0x0600029C RID: 668 RVA: 0x00010628 File Offset: 0x0000E828
		public SelfExtractorFlavor Flavor { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00010631 File Offset: 0x0000E831
		// (set) Token: 0x0600029E RID: 670 RVA: 0x00010639 File Offset: 0x0000E839
		public string PostExtractCommandLine { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00010642 File Offset: 0x0000E842
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0001064A File Offset: 0x0000E84A
		public string DefaultExtractDirectory { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00010653 File Offset: 0x0000E853
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x0001065B File Offset: 0x0000E85B
		public string IconFile { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00010664 File Offset: 0x0000E864
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x0001066C File Offset: 0x0000E86C
		public bool Quiet { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00010675 File Offset: 0x0000E875
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x0001067D File Offset: 0x0000E87D
		public ExtractExistingFileAction ExtractExistingFile { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00010686 File Offset: 0x0000E886
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0001068E File Offset: 0x0000E88E
		public bool RemoveUnpackedFilesAfterExecute { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00010697 File Offset: 0x0000E897
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0001069F File Offset: 0x0000E89F
		public Version FileVersion { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002AB RID: 683 RVA: 0x000106A8 File Offset: 0x0000E8A8
		// (set) Token: 0x060002AC RID: 684 RVA: 0x000106B0 File Offset: 0x0000E8B0
		public string ProductVersion { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002AD RID: 685 RVA: 0x000106B9 File Offset: 0x0000E8B9
		// (set) Token: 0x060002AE RID: 686 RVA: 0x000106C1 File Offset: 0x0000E8C1
		public string Copyright { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002AF RID: 687 RVA: 0x000106CA File Offset: 0x0000E8CA
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x000106D2 File Offset: 0x0000E8D2
		public string Description { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x000106DB File Offset: 0x0000E8DB
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x000106E3 File Offset: 0x0000E8E3
		public string ProductName { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x000106EC File Offset: 0x0000E8EC
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x000106F4 File Offset: 0x0000E8F4
		public string SfxExeWindowTitle { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x000106FD File Offset: 0x0000E8FD
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x00010705 File Offset: 0x0000E905
		public string AdditionalCompilerSwitches { get; set; }
	}
}
