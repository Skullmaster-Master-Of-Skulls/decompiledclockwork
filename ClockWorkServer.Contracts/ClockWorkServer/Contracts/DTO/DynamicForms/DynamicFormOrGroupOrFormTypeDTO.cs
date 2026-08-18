using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000699 RID: 1689
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(byte[]))]
	public class DynamicFormOrGroupOrFormTypeDTO
	{
		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x0600225A RID: 8794 RVA: 0x0000FB50 File Offset: 0x0000DD50
		// (set) Token: 0x0600225B RID: 8795 RVA: 0x0000FB58 File Offset: 0x0000DD58
		[DataMember]
		public DynamicFormDTO DynamicForm { get; set; }

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x0600225C RID: 8796 RVA: 0x0000FB61 File Offset: 0x0000DD61
		// (set) Token: 0x0600225D RID: 8797 RVA: 0x0000FB69 File Offset: 0x0000DD69
		[DataMember]
		public eDynamicFormTypeDTO? DynamicFormType { get; set; }

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x0600225E RID: 8798 RVA: 0x0000FB72 File Offset: 0x0000DD72
		// (set) Token: 0x0600225F RID: 8799 RVA: 0x0000FB7A File Offset: 0x0000DD7A
		[DataMember]
		public string GroupName { get; set; }
	}
}
