using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.Context
{
	// Token: 0x02000773 RID: 1907
	[DataContract(Namespace = "http://tpro.ca")]
	public class CustomDataPerDateContextDTO : CustomDataContextDTO
	{
		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x0600272D RID: 10029 RVA: 0x0001241B File Offset: 0x0001061B
		// (set) Token: 0x0600272E RID: 10030 RVA: 0x00012423 File Offset: 0x00010623
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x0600272F RID: 10031 RVA: 0x0001242C File Offset: 0x0001062C
		// (set) Token: 0x06002730 RID: 10032 RVA: 0x00012434 File Offset: 0x00010634
		[DataMember]
		public int CustomDataPerDateId { get; set; }

		// Token: 0x06002731 RID: 10033 RVA: 0x0001243D File Offset: 0x0001063D
		public CustomDataPerDateContextDTO()
		{
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x00012448 File Offset: 0x00010648
		public CustomDataPerDateContextDTO(string parameters)
		{
			IDictionary<string, string> dictionary = base.Parse(parameters);
			this.PersonId = int.Parse(dictionary["PersonId"]);
			this.CustomDataPerDateId = int.Parse(dictionary["CustomDataPerDateId"]);
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x00012494 File Offset: 0x00010694
		public override string ToString()
		{
			return string.Format("{0}|PersonId={1},CustomDataPerDateId={2}", base.GetType().FullName, this.PersonId, this.CustomDataPerDateId);
		}
	}
}
