using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.CustomForms.Controls;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls.CustomControls
{
	// Token: 0x02000780 RID: 1920
	[DataContract(Namespace = "http://tpro.ca")]
	[CustomControlBase(eCustomControlType.Label)]
	public class CustomLabelDTO : CustomControlStaticDTO
	{
		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x06002767 RID: 10087 RVA: 0x0001272A File Offset: 0x0001092A
		// (set) Token: 0x06002768 RID: 10088 RVA: 0x00012732 File Offset: 0x00010932
		[DataMember]
		public bool ShowAsHtmlLink { get; set; }

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x06002769 RID: 10089 RVA: 0x0001273B File Offset: 0x0001093B
		// (set) Token: 0x0600276A RID: 10090 RVA: 0x00012743 File Offset: 0x00010943
		[DataMember]
		public eCustomControlCharacterCasing CharacterCasing { get; set; }
	}
}
