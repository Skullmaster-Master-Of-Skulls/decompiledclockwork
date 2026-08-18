using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.DataStructure;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops
{
	// Token: 0x020008F3 RID: 2291
	[DataContract(Namespace = "http://tpro.ca")]
	public class WorkshopDefinitionDTO : ICloneable<WorkshopDefinitionDTO>, ICloneable
	{
		// Token: 0x06002EAA RID: 11946 RVA: 0x000036BD File Offset: 0x000018BD
		public WorkshopDefinitionDTO()
		{
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x0001625C File Offset: 0x0001445C
		public WorkshopDefinitionDTO(WorkshopDefinitionDTO item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.WorkshopId = item.WorkshopId;
				this.AppTypeParent = item.AppTypeParent.Clone();
				this.WorkshopTitle = item.WorkshopTitle;
				this.WorkshopDescription = item.WorkshopDescription;
				this.MaxAttendeeDefaultCount = item.MaxAttendeeDefaultCount;
				this.Fee = item.Fee;
				List<PersonBaseDTO> facilitators;
				if (item.Facilitators != null)
				{
					facilitators = (from g in item.Facilitators
					select g.Clone()).ToList<PersonBaseDTO>();
				}
				else
				{
					facilitators = null;
				}
				this.Facilitators = facilitators;
				this.WorkshopLocation = item.WorkshopLocation;
				this.WorkshopNotes = item.WorkshopNotes;
			}
		}

		// Token: 0x17001086 RID: 4230
		// (get) Token: 0x06002EAC RID: 11948 RVA: 0x0001632C File Offset: 0x0001452C
		// (set) Token: 0x06002EAD RID: 11949 RVA: 0x00016334 File Offset: 0x00014534
		[DataMember]
		public int WorkshopId { get; set; }

		// Token: 0x17001087 RID: 4231
		// (get) Token: 0x06002EAE RID: 11950 RVA: 0x0001633D File Offset: 0x0001453D
		// (set) Token: 0x06002EAF RID: 11951 RVA: 0x00016345 File Offset: 0x00014545
		[DataMember]
		public AppTypeDTO AppTypeParent { get; set; }

		// Token: 0x17001088 RID: 4232
		// (get) Token: 0x06002EB0 RID: 11952 RVA: 0x0001634E File Offset: 0x0001454E
		// (set) Token: 0x06002EB1 RID: 11953 RVA: 0x00016356 File Offset: 0x00014556
		[DataMember]
		public string WorkshopTitle { get; set; }

		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x06002EB2 RID: 11954 RVA: 0x0001635F File Offset: 0x0001455F
		// (set) Token: 0x06002EB3 RID: 11955 RVA: 0x00016367 File Offset: 0x00014567
		[DataMember]
		public string WorkshopDescription { get; set; }

		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x06002EB4 RID: 11956 RVA: 0x00016370 File Offset: 0x00014570
		// (set) Token: 0x06002EB5 RID: 11957 RVA: 0x00016378 File Offset: 0x00014578
		[DataMember]
		public int MaxAttendeeDefaultCount { get; set; }

		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x06002EB6 RID: 11958 RVA: 0x00016381 File Offset: 0x00014581
		// (set) Token: 0x06002EB7 RID: 11959 RVA: 0x00016389 File Offset: 0x00014589
		[DataMember]
		public double Fee { get; set; }

		// Token: 0x1700108C RID: 4236
		// (get) Token: 0x06002EB8 RID: 11960 RVA: 0x00016392 File Offset: 0x00014592
		// (set) Token: 0x06002EB9 RID: 11961 RVA: 0x0001639A File Offset: 0x0001459A
		[DataMember]
		public List<PersonBaseDTO> Facilitators { get; set; }

		// Token: 0x1700108D RID: 4237
		// (get) Token: 0x06002EBA RID: 11962 RVA: 0x000163A3 File Offset: 0x000145A3
		// (set) Token: 0x06002EBB RID: 11963 RVA: 0x000163AB File Offset: 0x000145AB
		[DataMember]
		public string WorkshopLocation { get; set; }

		// Token: 0x1700108E RID: 4238
		// (get) Token: 0x06002EBC RID: 11964 RVA: 0x000163B4 File Offset: 0x000145B4
		// (set) Token: 0x06002EBD RID: 11965 RVA: 0x000163BC File Offset: 0x000145BC
		[DataMember]
		public string WorkshopNotes { get; set; }

		// Token: 0x06002EBE RID: 11966 RVA: 0x000163C8 File Offset: 0x000145C8
		public WorkshopDefinitionDTO Clone()
		{
			return new WorkshopDefinitionDTO(this);
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x000163E0 File Offset: 0x000145E0
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
