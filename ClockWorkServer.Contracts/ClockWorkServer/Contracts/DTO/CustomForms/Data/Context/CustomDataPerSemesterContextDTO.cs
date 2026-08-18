using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.Context
{
	// Token: 0x02000774 RID: 1908
	[DataContract(Namespace = "http://tpro.ca")]
	public class CustomDataPerSemesterContextDTO : CustomDataContextDTO
	{
		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x06002734 RID: 10036 RVA: 0x000124D1 File Offset: 0x000106D1
		// (set) Token: 0x06002735 RID: 10037 RVA: 0x000124D9 File Offset: 0x000106D9
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x06002736 RID: 10038 RVA: 0x000124E2 File Offset: 0x000106E2
		// (set) Token: 0x06002737 RID: 10039 RVA: 0x000124EA File Offset: 0x000106EA
		[DataMember]
		public int SemesterId { get; set; }

		// Token: 0x06002738 RID: 10040 RVA: 0x0001243D File Offset: 0x0001063D
		public CustomDataPerSemesterContextDTO()
		{
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x000124F4 File Offset: 0x000106F4
		public CustomDataPerSemesterContextDTO(string parameters) : base(parameters)
		{
			IDictionary<string, string> dictionary = base.Parse(parameters);
			this.PersonId = int.Parse(dictionary["PersonId"]);
			this.SemesterId = int.Parse(dictionary["SemesterId"]);
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x00012540 File Offset: 0x00010740
		public override string ToString()
		{
			return string.Format("{0}|PersonId={1},SemesterId={2}", base.GetType().FullName, this.PersonId, this.SemesterId);
		}
	}
}
