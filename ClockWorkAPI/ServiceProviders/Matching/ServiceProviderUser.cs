using System;
using System.Data;

namespace ClockWorkAPI.ServiceProviders.Matching
{
	// Token: 0x0200001D RID: 29
	public class ServiceProviderUser
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00007564 File Offset: 0x00006564
		// (set) Token: 0x06000109 RID: 265 RVA: 0x0000757C File Offset: 0x0000657C
		public int ServiceProviderid
		{
			get
			{
				return this.serviceProviderId;
			}
			set
			{
				this.serviceProviderId = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00007588 File Offset: 0x00006588
		// (set) Token: 0x0600010B RID: 267 RVA: 0x000075A0 File Offset: 0x000065A0
		public string FirstName
		{
			get
			{
				return this.firstName;
			}
			set
			{
				this.firstName = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600010C RID: 268 RVA: 0x000075AC File Offset: 0x000065AC
		// (set) Token: 0x0600010D RID: 269 RVA: 0x000075C4 File Offset: 0x000065C4
		public string LastName
		{
			get
			{
				return this.lastName;
			}
			set
			{
				this.lastName = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600010E RID: 270 RVA: 0x000075D0 File Offset: 0x000065D0
		// (set) Token: 0x0600010F RID: 271 RVA: 0x000075E8 File Offset: 0x000065E8
		public string Student_no
		{
			get
			{
				return this.student_no;
			}
			set
			{
				this.student_no = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000110 RID: 272 RVA: 0x000075F4 File Offset: 0x000065F4
		// (set) Token: 0x06000111 RID: 273 RVA: 0x0000760C File Offset: 0x0000660C
		public string Email
		{
			get
			{
				return this.email;
			}
			set
			{
				this.email = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00007618 File Offset: 0x00006618
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00007630 File Offset: 0x00006630
		public string Phone1
		{
			get
			{
				return this.phone1;
			}
			set
			{
				this.phone1 = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000114 RID: 276 RVA: 0x0000763C File Offset: 0x0000663C
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00007654 File Offset: 0x00006654
		public string Phone2
		{
			get
			{
				return this.phone1;
			}
			set
			{
				this.phone2 = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00007660 File Offset: 0x00006660
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00007678 File Offset: 0x00006678
		public string Notes1
		{
			get
			{
				return this.notes1;
			}
			set
			{
				this.notes1 = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00007684 File Offset: 0x00006684
		// (set) Token: 0x06000119 RID: 281 RVA: 0x0000769C File Offset: 0x0000669C
		public string Notes2
		{
			get
			{
				return this.notes2;
			}
			set
			{
				this.notes2 = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000076A8 File Offset: 0x000066A8
		// (set) Token: 0x0600011B RID: 283 RVA: 0x000076C0 File Offset: 0x000066C0
		public string Specialization
		{
			get
			{
				return this.specialization;
			}
			set
			{
				this.specialization = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000076CC File Offset: 0x000066CC
		// (set) Token: 0x0600011D RID: 285 RVA: 0x000076E4 File Offset: 0x000066E4
		public string AdditionalServices
		{
			get
			{
				return this.additionalServices;
			}
			set
			{
				this.additionalServices = value;
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000076EE File Offset: 0x000066EE
		public ServiceProviderUser()
		{
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000076FC File Offset: 0x000066FC
		public ServiceProviderUser(DataRow dr)
		{
			this.serviceProviderId = (int)dr["serviceproviderid"];
			this.firstName = dr["spfirstname"].ToString();
			this.lastName = dr["splastname"].ToString();
			this.student_no = dr["spstudent_no"].ToString();
			this.email = dr["spemail"].ToString();
			this.phone1 = dr["phone1"].ToString();
			this.phone2 = dr["phone2"].ToString();
			this.notes1 = dr["notes1"].ToString();
			this.notes2 = dr["notes2"].ToString();
			this.specialization = dr["specialization"].ToString();
			this.additionalServices = dr["additionalServices"].ToString();
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00007804 File Offset: 0x00006804
		public ServiceProviderUser(int serviceProviderId, string firstname, string lastname, string student_no, string email, string phone1, string phone2, string notes1, string notes2, string specialization, string additionalServices)
		{
			this.serviceProviderId = serviceProviderId;
			this.firstName = firstname;
			this.lastName = lastname;
			this.student_no = student_no;
			this.email = email;
			this.phone1 = phone1;
			this.phone2 = phone2;
			this.notes1 = notes1;
			this.notes2 = notes2;
			this.specialization = specialization;
			this.additionalServices = additionalServices;
		}

		// Token: 0x04000095 RID: 149
		private int serviceProviderId;

		// Token: 0x04000096 RID: 150
		private string firstName;

		// Token: 0x04000097 RID: 151
		private string lastName;

		// Token: 0x04000098 RID: 152
		private string student_no;

		// Token: 0x04000099 RID: 153
		private string email;

		// Token: 0x0400009A RID: 154
		private string phone1;

		// Token: 0x0400009B RID: 155
		private string phone2;

		// Token: 0x0400009C RID: 156
		private string notes1;

		// Token: 0x0400009D RID: 157
		private string notes2;

		// Token: 0x0400009E RID: 158
		private string specialization;

		// Token: 0x0400009F RID: 159
		private string additionalServices;
	}
}
