using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls.CustomControls
{
	// Token: 0x02000788 RID: 1928
	[DataContract(Namespace = "http://tpro.ca")]
	[CustomControlBase(eCustomControlType.CustomYesNoChooser)]
	public class CustomYesNoChooserDTO : CustomControlDataHolderDTO
	{
		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x0600278B RID: 10123 RVA: 0x00012821 File Offset: 0x00010A21
		// (set) Token: 0x0600278C RID: 10124 RVA: 0x00012829 File Offset: 0x00010A29
		[DataMember]
		public string[] PopupYesControlIds { get; set; }

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x0600278D RID: 10125 RVA: 0x00012832 File Offset: 0x00010A32
		// (set) Token: 0x0600278E RID: 10126 RVA: 0x0001283A File Offset: 0x00010A3A
		[DataMember]
		public string[] PopupNoControlIds { get; set; }

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x0600278F RID: 10127 RVA: 0x00012843 File Offset: 0x00010A43
		public bool IsPopupYesEnabled
		{
			get
			{
				string[] popupYesControlIds = this.PopupYesControlIds;
				return ((popupYesControlIds != null) ? popupYesControlIds.Length : 0) > 0;
			}
		}

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x06002790 RID: 10128 RVA: 0x00012857 File Offset: 0x00010A57
		public bool IsPopupNoEnabled
		{
			get
			{
				string[] popupNoControlIds = this.PopupNoControlIds;
				return ((popupNoControlIds != null) ? popupNoControlIds.Length : 0) > 0;
			}
		}

		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x06002791 RID: 10129 RVA: 0x0001286B File Offset: 0x00010A6B
		public bool IsPopupEnabled
		{
			get
			{
				return this.IsPopupYesEnabled || this.IsPopupNoEnabled;
			}
		}
	}
}
