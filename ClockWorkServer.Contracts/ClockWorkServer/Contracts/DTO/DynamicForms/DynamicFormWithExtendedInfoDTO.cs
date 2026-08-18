using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x020006B3 RID: 1715
	[DataContract(Namespace = "http://tpro.ca")]
	public class DynamicFormWithExtendedInfoDTO : DynamicFormDTO
	{
		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x060022AE RID: 8878 RVA: 0x0000FD57 File Offset: 0x0000DF57
		// (set) Token: 0x060022AF RID: 8879 RVA: 0x0000FD5F File Offset: 0x0000DF5F
		[DataMember]
		public int VerticalControlPadding { get; set; }

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x060022B0 RID: 8880 RVA: 0x0000FD68 File Offset: 0x0000DF68
		// (set) Token: 0x060022B1 RID: 8881 RVA: 0x0000FD70 File Offset: 0x0000DF70
		[DataMember]
		public int ColumnPadding { get; set; }

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x060022B2 RID: 8882 RVA: 0x0000FD79 File Offset: 0x0000DF79
		// (set) Token: 0x060022B3 RID: 8883 RVA: 0x0000FD81 File Offset: 0x0000DF81
		[DataMember]
		public DateTime DateAdded { get; set; }

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x060022B4 RID: 8884 RVA: 0x0000FD8A File Offset: 0x0000DF8A
		// (set) Token: 0x060022B5 RID: 8885 RVA: 0x0000FD92 File Offset: 0x0000DF92
		[DataMember]
		public DateTime? DateModified { get; set; }

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x060022B6 RID: 8886 RVA: 0x0000FD9B File Offset: 0x0000DF9B
		// (set) Token: 0x060022B7 RID: 8887 RVA: 0x0000FDA3 File Offset: 0x0000DFA3
		[DataMember]
		public bool StudentNameNumEditable { get; set; }

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x060022B8 RID: 8888 RVA: 0x0000FDAC File Offset: 0x0000DFAC
		// (set) Token: 0x060022B9 RID: 8889 RVA: 0x0000FDB4 File Offset: 0x0000DFB4
		[DataMember]
		public int ScreenId { get; set; }

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x060022BA RID: 8890 RVA: 0x0000FDBD File Offset: 0x0000DFBD
		// (set) Token: 0x060022BB RID: 8891 RVA: 0x0000FDC5 File Offset: 0x0000DFC5
		[DataMember]
		public string FontName { get; set; }

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x060022BC RID: 8892 RVA: 0x0000FDCE File Offset: 0x0000DFCE
		// (set) Token: 0x060022BD RID: 8893 RVA: 0x0000FDD6 File Offset: 0x0000DFD6
		[DataMember]
		public int FontSize { get; set; }

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x060022BE RID: 8894 RVA: 0x0000FDDF File Offset: 0x0000DFDF
		// (set) Token: 0x060022BF RID: 8895 RVA: 0x0000FDE7 File Offset: 0x0000DFE7
		[DataMember]
		public IList<int> GroupIds { get; set; }

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x060022C0 RID: 8896 RVA: 0x0000FDF0 File Offset: 0x0000DFF0
		// (set) Token: 0x060022C1 RID: 8897 RVA: 0x0000FDF8 File Offset: 0x0000DFF8
		[DataMember]
		public bool IsWebScreen { get; set; }

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x060022C2 RID: 8898 RVA: 0x0000FE01 File Offset: 0x0000E001
		// (set) Token: 0x060022C3 RID: 8899 RVA: 0x0000FE09 File Offset: 0x0000E009
		[DataMember]
		public int ControlIdToActivate { get; set; }

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x060022C4 RID: 8900 RVA: 0x0000FE12 File Offset: 0x0000E012
		// (set) Token: 0x060022C5 RID: 8901 RVA: 0x0000FE1A File Offset: 0x0000E01A
		[DataMember]
		public string StudentNumberCaption { get; set; }

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x060022C6 RID: 8902 RVA: 0x0000FE23 File Offset: 0x0000E023
		// (set) Token: 0x060022C7 RID: 8903 RVA: 0x0000FE2B File Offset: 0x0000E02B
		[DataMember]
		public string StudentNumberAutoGenerateRule { get; set; }

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x060022C8 RID: 8904 RVA: 0x0000FE34 File Offset: 0x0000E034
		// (set) Token: 0x060022C9 RID: 8905 RVA: 0x0000FE3C File Offset: 0x0000E03C
		[DataMember]
		public bool StudentNameHidden { get; set; }
	}
}
