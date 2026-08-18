using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.Context
{
	// Token: 0x02000775 RID: 1909
	[DataContract(Namespace = "http://tpro.ca")]
	public class CustomDataPerStudentContextDTO : CustomDataContextDTO
	{
		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x0600273B RID: 10043 RVA: 0x0001257D File Offset: 0x0001077D
		// (set) Token: 0x0600273C RID: 10044 RVA: 0x00012585 File Offset: 0x00010785
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x0600273D RID: 10045 RVA: 0x0001243D File Offset: 0x0001063D
		public CustomDataPerStudentContextDTO()
		{
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x00012590 File Offset: 0x00010790
		public CustomDataPerStudentContextDTO(string parameters)
		{
			IDictionary<string, string> dictionary = base.Parse(parameters);
			this.PersonId = int.Parse(dictionary["PersonId"]);
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x000125C4 File Offset: 0x000107C4
		public override string ToString()
		{
			return string.Format("{0}|PersonId={1}", base.GetType().FullName, this.PersonId);
		}
	}
}
