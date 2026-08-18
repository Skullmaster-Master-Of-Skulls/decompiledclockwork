using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x020006B2 RID: 1714
	[DataContract(Namespace = "http://tpro.ca")]
	public class DynamicFormTypeAttributeDTO : Attribute
	{
		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x060022A9 RID: 8873 RVA: 0x0000FD1B File Offset: 0x0000DF1B
		// (set) Token: 0x060022AA RID: 8874 RVA: 0x0000FD23 File Offset: 0x0000DF23
		[DataMember]
		public string TablePostFix { get; set; }

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x060022AB RID: 8875 RVA: 0x0000FD2C File Offset: 0x0000DF2C
		// (set) Token: 0x060022AC RID: 8876 RVA: 0x0000FD34 File Offset: 0x0000DF34
		[DataMember]
		public bool UseSecondaryContextId { get; set; }

		// Token: 0x060022AD RID: 8877 RVA: 0x0000FD3D File Offset: 0x0000DF3D
		public DynamicFormTypeAttributeDTO(string TablePostFix, bool UseSecondaryContextId)
		{
			this.TablePostFix = TablePostFix;
			this.UseSecondaryContextId = UseSecondaryContextId;
		}
	}
}
