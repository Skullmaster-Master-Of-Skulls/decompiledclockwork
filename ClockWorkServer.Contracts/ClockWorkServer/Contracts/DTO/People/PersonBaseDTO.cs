using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000370 RID: 880
	[DataContract(Namespace = "http://tpro.ca")]
	public class PersonBaseDTO : ICloneable<PersonBaseDTO>, ICloneable
	{
		// Token: 0x0600142C RID: 5164 RVA: 0x000036BD File Offset: 0x000018BD
		public PersonBaseDTO()
		{
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x00009780 File Offset: 0x00007980
		public PersonBaseDTO(PersonBaseDTO item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.PersonId = item.PersonId;
				this.FirstName = item.FirstName;
				this.MiddleName = item.MiddleName;
				this.LastName = item.LastName;
				this.Student_no = item.Student_no;
				bool flag2 = item.Groups == null;
				if (flag2)
				{
					this.Groups = new List<GroupDTO>();
				}
				else
				{
					this.Groups = item.Groups.ToList<GroupDTO>().ConvertAll<GroupDTO>((GroupDTO g) => g.Clone());
				}
				this.CoreGroup = item.CoreGroup;
				this.IsActivated = item.IsActivated;
				this.Tag = item.Tag;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x0000985B File Offset: 0x00007A5B
		// (set) Token: 0x0600142F RID: 5167 RVA: 0x00009863 File Offset: 0x00007A63
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x0000986C File Offset: 0x00007A6C
		// (set) Token: 0x06001431 RID: 5169 RVA: 0x00009874 File Offset: 0x00007A74
		[DataMember]
		public string FirstName { get; set; }

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x0000987D File Offset: 0x00007A7D
		// (set) Token: 0x06001433 RID: 5171 RVA: 0x00009885 File Offset: 0x00007A85
		[DataMember]
		public string MiddleName { get; set; }

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x0000988E File Offset: 0x00007A8E
		// (set) Token: 0x06001435 RID: 5173 RVA: 0x00009896 File Offset: 0x00007A96
		[DataMember]
		public string LastName { get; set; }

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x0000989F File Offset: 0x00007A9F
		// (set) Token: 0x06001437 RID: 5175 RVA: 0x000098A7 File Offset: 0x00007AA7
		[DataMember]
		public string Student_no { get; set; }

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x000098B0 File Offset: 0x00007AB0
		// (set) Token: 0x06001439 RID: 5177 RVA: 0x000098B8 File Offset: 0x00007AB8
		[DataMember]
		public List<GroupDTO> Groups { get; set; }

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x000098C1 File Offset: 0x00007AC1
		// (set) Token: 0x0600143B RID: 5179 RVA: 0x000098C9 File Offset: 0x00007AC9
		[DataMember]
		public eCoreGroupDTO CoreGroup { get; set; }

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x000098D2 File Offset: 0x00007AD2
		// (set) Token: 0x0600143D RID: 5181 RVA: 0x000098DA File Offset: 0x00007ADA
		[DataMember]
		public bool? IsActivated { get; set; }

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x000098E3 File Offset: 0x00007AE3
		// (set) Token: 0x0600143F RID: 5183 RVA: 0x000098EB File Offset: 0x00007AEB
		public object Tag { get; set; }

		// Token: 0x06001440 RID: 5184 RVA: 0x000098F4 File Offset: 0x00007AF4
		public PersonBaseDTO Clone()
		{
			return new PersonBaseDTO(this);
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0000990C File Offset: 0x00007B0C
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
