using System;
using System.Runtime.Serialization;
using TechnoPro.Common.DataStructure;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000694 RID: 1684
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(DynamicFormDTO))]
	[KnownType(typeof(DynamicFormWithExtendedInfoDTO))]
	public class DynamicFormBaseDTO : ICloneable<DynamicFormBaseDTO>, ICloneable
	{
		// Token: 0x0600222E RID: 8750 RVA: 0x000036BD File Offset: 0x000018BD
		public DynamicFormBaseDTO()
		{
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x0000F94C File Offset: 0x0000DB4C
		public DynamicFormBaseDTO(DynamicFormBaseDTO item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.ScreenNum = item.ScreenNum;
				this.FormType = item.FormType;
				this.Title = item.Title;
				this.SecondaryTitle = item.SecondaryTitle;
				this.IsEnabled = item.IsEnabled;
				this.ShowAsButton = item.ShowAsButton;
				this.UniqueId = item.UniqueId;
				this.SubForm = item.SubForm;
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06002230 RID: 8752 RVA: 0x0000F9D3 File Offset: 0x0000DBD3
		// (set) Token: 0x06002231 RID: 8753 RVA: 0x0000F9DB File Offset: 0x0000DBDB
		[DataMember]
		public int ScreenNum { get; set; }

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x06002232 RID: 8754 RVA: 0x0000F9E4 File Offset: 0x0000DBE4
		// (set) Token: 0x06002233 RID: 8755 RVA: 0x0000F9EC File Offset: 0x0000DBEC
		[DataMember]
		public eDynamicFormTypeDTO FormType { get; set; }

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x06002234 RID: 8756 RVA: 0x0000F9F5 File Offset: 0x0000DBF5
		// (set) Token: 0x06002235 RID: 8757 RVA: 0x0000F9FD File Offset: 0x0000DBFD
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x06002236 RID: 8758 RVA: 0x0000FA06 File Offset: 0x0000DC06
		// (set) Token: 0x06002237 RID: 8759 RVA: 0x0000FA0E File Offset: 0x0000DC0E
		[DataMember]
		public string SecondaryTitle { get; set; }

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x06002238 RID: 8760 RVA: 0x0000FA17 File Offset: 0x0000DC17
		// (set) Token: 0x06002239 RID: 8761 RVA: 0x0000FA1F File Offset: 0x0000DC1F
		[DataMember]
		public bool IsEnabled { get; set; }

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x0600223A RID: 8762 RVA: 0x0000FA28 File Offset: 0x0000DC28
		// (set) Token: 0x0600223B RID: 8763 RVA: 0x0000FA30 File Offset: 0x0000DC30
		[DataMember]
		public bool ShowAsButton { get; set; }

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x0600223C RID: 8764 RVA: 0x0000FA39 File Offset: 0x0000DC39
		// (set) Token: 0x0600223D RID: 8765 RVA: 0x0000FA41 File Offset: 0x0000DC41
		[DataMember]
		public string UniqueId { get; set; }

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x0600223E RID: 8766 RVA: 0x0000FA4A File Offset: 0x0000DC4A
		// (set) Token: 0x0600223F RID: 8767 RVA: 0x0000FA52 File Offset: 0x0000DC52
		[DataMember]
		public DynamicFormDTO SubForm { get; set; }

		// Token: 0x06002240 RID: 8768 RVA: 0x0000FA5C File Offset: 0x0000DC5C
		public DynamicFormBaseDTO Clone()
		{
			return new DynamicFormBaseDTO(this);
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x0000FA74 File Offset: 0x0000DC74
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
