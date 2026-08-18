using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000695 RID: 1685
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(DynamicFormWithExtendedInfoDTO))]
	public class DynamicFormDTO : DynamicFormBaseDTO
	{
		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06002242 RID: 8770 RVA: 0x0000FA8C File Offset: 0x0000DC8C
		// (set) Token: 0x06002243 RID: 8771 RVA: 0x0000FA94 File Offset: 0x0000DC94
		[DataMember]
		public double ColumnWidthPercent { get; set; }

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06002244 RID: 8772 RVA: 0x0000FA9D File Offset: 0x0000DC9D
		// (set) Token: 0x06002245 RID: 8773 RVA: 0x0000FAA5 File Offset: 0x0000DCA5
		[DataMember]
		public bool BottomLess { get; set; }

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06002246 RID: 8774 RVA: 0x0000FAAE File Offset: 0x0000DCAE
		// (set) Token: 0x06002247 RID: 8775 RVA: 0x0000FAB6 File Offset: 0x0000DCB6
		[DataMember]
		public string CSharp_FormLoad { get; set; }

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x06002248 RID: 8776 RVA: 0x0000FABF File Offset: 0x0000DCBF
		// (set) Token: 0x06002249 RID: 8777 RVA: 0x0000FAC7 File Offset: 0x0000DCC7
		[DataMember]
		public string CSharp_FormSave { get; set; }

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x0600224A RID: 8778 RVA: 0x0000FAD0 File Offset: 0x0000DCD0
		// (set) Token: 0x0600224B RID: 8779 RVA: 0x0000FAD8 File Offset: 0x0000DCD8
		[DataMember]
		public string CSharp_Misc { get; set; }

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x0600224C RID: 8780 RVA: 0x0000FAE1 File Offset: 0x0000DCE1
		// (set) Token: 0x0600224D RID: 8781 RVA: 0x0000FAE9 File Offset: 0x0000DCE9
		[DataMember]
		public string GroupName { get; set; }

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x0600224E RID: 8782 RVA: 0x0000FAF2 File Offset: 0x0000DCF2
		// (set) Token: 0x0600224F RID: 8783 RVA: 0x0000FAFA File Offset: 0x0000DCFA
		[DataMember]
		public int LargeImageIndex { get; set; }

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06002250 RID: 8784 RVA: 0x0000FB03 File Offset: 0x0000DD03
		// (set) Token: 0x06002251 RID: 8785 RVA: 0x0000FB0B File Offset: 0x0000DD0B
		[DataMember]
		public int SmallImageIndex { get; set; }
	}
}
