using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnoPro.Common.Public.Entities.People
{
	// Token: 0x0200026B RID: 619
	public class PersonBase : BusinessBase<int>, ICloneable<PersonBase>, ICloneable
	{
		// Token: 0x0600129E RID: 4766 RVA: 0x00018D1C File Offset: 0x00016F1C
		public PersonBase()
		{
			this.CoreGroup = eCoreGroup.Unknown;
			this.FirstName = "";
			this.LastName = "";
			this.Student_no = "";
			this.MiddleName = "";
			this.PersonId = 0;
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00018D74 File Offset: 0x00016F74
		public PersonBase(PersonBase pb)
		{
			bool flag = pb == null;
			if (!flag)
			{
				this.PersonId = pb.PersonId;
				this.FirstName = pb.FirstName;
				this.MiddleName = pb.MiddleName;
				this.LastName = pb.LastName;
				this.Student_no = pb.Student_no;
				this.CoreGroup = pb.CoreGroup;
				this.IsActivated = pb.IsActivated;
				List<Group> groups = pb.Groups;
				List<Group> groups2;
				if (groups == null)
				{
					groups2 = null;
				}
				else
				{
					groups2 = (from g in groups
					select g.Clone()).ToList<Group>();
				}
				this.Groups = groups2;
			}
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00018E30 File Offset: 0x00017030
		public PersonBase(int PersonId, string FirstName, string MiddleName, string LastName, string Student_no, params eCoreGroup[] CoreGroups)
		{
			this.PersonId = PersonId;
			this.FirstName = FirstName;
			this.MiddleName = MiddleName;
			this.LastName = LastName;
			this.Student_no = Student_no;
			this.CoreGroup = eCoreGroup.Unknown;
			foreach (eCoreGroup eCoreGroup in CoreGroups)
			{
				this.CoreGroup |= eCoreGroup;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x060012A1 RID: 4769 RVA: 0x00018EA0 File Offset: 0x000170A0
		// (set) Token: 0x060012A2 RID: 4770 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int PersonId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x060012A3 RID: 4771 RVA: 0x00018EB8 File Offset: 0x000170B8
		// (set) Token: 0x060012A4 RID: 4772 RVA: 0x00018EC0 File Offset: 0x000170C0
		public string FirstName { get; set; }

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x060012A5 RID: 4773 RVA: 0x00018EC9 File Offset: 0x000170C9
		// (set) Token: 0x060012A6 RID: 4774 RVA: 0x00018ED1 File Offset: 0x000170D1
		public string MiddleName { get; set; }

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x060012A7 RID: 4775 RVA: 0x00018EDA File Offset: 0x000170DA
		// (set) Token: 0x060012A8 RID: 4776 RVA: 0x00018EE2 File Offset: 0x000170E2
		public string LastName { get; set; }

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x060012A9 RID: 4777 RVA: 0x00018EEB File Offset: 0x000170EB
		// (set) Token: 0x060012AA RID: 4778 RVA: 0x00018EF3 File Offset: 0x000170F3
		public string Student_no { get; set; }

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x060012AB RID: 4779 RVA: 0x00018EFC File Offset: 0x000170FC
		// (set) Token: 0x060012AC RID: 4780 RVA: 0x00018F04 File Offset: 0x00017104
		public List<Group> Groups { get; set; }

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x060012AD RID: 4781 RVA: 0x00018F0D File Offset: 0x0001710D
		// (set) Token: 0x060012AE RID: 4782 RVA: 0x00018F15 File Offset: 0x00017115
		public eCoreGroup CoreGroup { get; set; }

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x00018F1E File Offset: 0x0001711E
		// (set) Token: 0x060012B0 RID: 4784 RVA: 0x00018F26 File Offset: 0x00017126
		public bool? IsActivated { get; set; }

		// Token: 0x060012B1 RID: 4785 RVA: 0x00018F30 File Offset: 0x00017130
		public PersonBase Clone()
		{
			return new PersonBase(this);
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x00018F48 File Offset: 0x00017148
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
