using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.DataHolders;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data
{
	// Token: 0x02000769 RID: 1897
	[DataContract(Namespace = "http://tpro.ca")]
	public class CustomDataHolderCollectionDTO
	{
		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x060026FC RID: 9980 RVA: 0x0001213E File Offset: 0x0001033E
		// (set) Token: 0x060026FD RID: 9981 RVA: 0x00012146 File Offset: 0x00010346
		[DataMember]
		public IList<CustomDataHolderDTO> Datas { get; set; }

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x060026FE RID: 9982 RVA: 0x00012150 File Offset: 0x00010350
		// (set) Token: 0x060026FF RID: 9983 RVA: 0x00012174 File Offset: 0x00010374
		public CustomDataHolderDTO Data
		{
			get
			{
				IList<CustomDataHolderDTO> datas = this.Datas;
				return (datas != null) ? datas.FirstOrDefault<CustomDataHolderDTO>() : null;
			}
			set
			{
				this.Datas = new CustomDataHolderDTO[]
				{
					value
				}.ToList<CustomDataHolderDTO>();
			}
		}
	}
}
