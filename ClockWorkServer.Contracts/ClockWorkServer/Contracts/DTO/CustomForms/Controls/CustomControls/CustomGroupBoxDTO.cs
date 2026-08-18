using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls.CustomControls
{
	// Token: 0x0200077E RID: 1918
	[DataContract(Namespace = "http://tpro.ca")]
	[CustomControlBase(eCustomControlType.GroupBox)]
	public class CustomGroupBoxDTO : CustomControlContainerDTO
	{
		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x0600275D RID: 10077 RVA: 0x000126DD File Offset: 0x000108DD
		// (set) Token: 0x0600275E RID: 10078 RVA: 0x000126E5 File Offset: 0x000108E5
		[DataMember]
		public bool ShowCaption { get; set; }

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x0600275F RID: 10079 RVA: 0x000126EE File Offset: 0x000108EE
		// (set) Token: 0x06002760 RID: 10080 RVA: 0x000126F6 File Offset: 0x000108F6
		[DataMember]
		public int? BackgroundColorArgb { get; set; }
	}
}
