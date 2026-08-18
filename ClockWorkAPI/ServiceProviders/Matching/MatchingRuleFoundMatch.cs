using System;
using System.Collections.Generic;
using System.Text;

namespace ClockWorkAPI.ServiceProviders.Matching
{
	// Token: 0x0200002B RID: 43
	public class MatchingRuleFoundMatch
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000CE78 File Offset: 0x0000BE78
		private int totalTryToMatchCount
		{
			get
			{
				return (this.tryToMatch != null) ? this.tryToMatch.Count : 0;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000CEA0 File Offset: 0x0000BEA0
		public string FirstName
		{
			get
			{
				return (this.serviceProvider == null) ? "" : this.serviceProvider.FirstName;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000CECC File Offset: 0x0000BECC
		public string LastName
		{
			get
			{
				return (this.serviceProvider == null) ? "" : this.serviceProvider.LastName;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000CEF8 File Offset: 0x0000BEF8
		public string Student_no
		{
			get
			{
				return (this.serviceProvider == null) ? "" : this.serviceProvider.Student_no;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000CF24 File Offset: 0x0000BF24
		public string Email
		{
			get
			{
				return (this.serviceProvider == null) ? "" : this.serviceProvider.Email;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000CF50 File Offset: 0x0000BF50
		public string Phone1
		{
			get
			{
				return (this.serviceProvider == null) ? "" : this.serviceProvider.Phone1;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000CF7C File Offset: 0x0000BF7C
		public string Phone2
		{
			get
			{
				return (this.serviceProvider == null) ? "" : this.serviceProvider.Phone2;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000CFA8 File Offset: 0x0000BFA8
		public string Specialization
		{
			get
			{
				return (this.serviceProvider == null) ? "" : this.serviceProvider.Specialization;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000CFD4 File Offset: 0x0000BFD4
		public string Notes1
		{
			get
			{
				return (this.serviceProvider == null) ? "" : this.serviceProvider.Notes1;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000D000 File Offset: 0x0000C000
		public string Notes2
		{
			get
			{
				return (this.serviceProvider == null) ? "" : this.serviceProvider.Notes2;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000D02C File Offset: 0x0000C02C
		public string AdditionalServices
		{
			get
			{
				return (this.serviceProvider == null) ? "" : this.serviceProvider.AdditionalServices;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000D058 File Offset: 0x0000C058
		public int AssignedToCount
		{
			get
			{
				return this.studentPidsAssignedTo.Count;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000D078 File Offset: 0x0000C078
		// (set) Token: 0x06000238 RID: 568 RVA: 0x0000D090 File Offset: 0x0000C090
		public ServiceProviderUser ServiceProvider
		{
			get
			{
				return this.serviceProvider;
			}
			set
			{
				this.serviceProvider = value;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000D09C File Offset: 0x0000C09C
		// (set) Token: 0x0600023A RID: 570 RVA: 0x0000D0B4 File Offset: 0x0000C0B4
		public Course ServiceProviderCourse
		{
			get
			{
				return this.serviceProviderCourse;
			}
			set
			{
				this.serviceProviderCourse = value;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000D0C0 File Offset: 0x0000C0C0
		public string DaysAvailable
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (TimeTableItem timeTableItem in this.matches.Keys)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(timeTableItem.DayOfWeek.ToString());
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000D160 File Offset: 0x0000C160
		public string NotAvailable
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (TimeTableItem timeTableItem in this.tryToMatch)
				{
					bool flag = false;
					foreach (TimeTableItem timeTableItem2 in this.matches.Keys)
					{
						if (timeTableItem2 == timeTableItem)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						if (stringBuilder.Length > 0)
						{
							stringBuilder.Append(", ");
						}
						stringBuilder.Append(timeTableItem.DayOfWeek.ToString());
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000D26C File Offset: 0x0000C26C
		public int MatchesTimeTableItemCount
		{
			get
			{
				return this.matches.Count;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000D28C File Offset: 0x0000C28C
		public int MatchedPercentage
		{
			get
			{
				int result;
				if (this.totalTryToMatchCount <= 0)
				{
					result = 0;
				}
				else
				{
					double num = Convert.ToDouble(this.matches.Count) / Convert.ToDouble(this.totalTryToMatchCount);
					result = (int)(num * 100.0);
				}
				return result;
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000D2DC File Offset: 0x0000C2DC
		public bool AddStudentPidAssignedTo(int pid)
		{
			bool result;
			if (!this.studentPidsAssignedTo.Contains(pid))
			{
				this.studentPidsAssignedTo.Add(pid);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000D314 File Offset: 0x0000C314
		public int MatchedCount(TimeTableItem tti)
		{
			int result;
			if (this.matches.ContainsKey(tti))
			{
				result = this.matches[tti];
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000D34A File Offset: 0x0000C34A
		public void AddMatched(TimeTableItem tti)
		{
			this.matches.Add(tti, 1);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000D35B File Offset: 0x0000C35B
		public MatchingRuleFoundMatch(List<TimeTableItem> tryToMatch)
		{
			this.tryToMatch = tryToMatch;
			this.matches = new Dictionary<TimeTableItem, int>();
			this.studentPidsAssignedTo = new List<int>();
		}

		// Token: 0x0400012A RID: 298
		private ServiceProviderUser serviceProvider;

		// Token: 0x0400012B RID: 299
		private Course serviceProviderCourse;

		// Token: 0x0400012C RID: 300
		private List<TimeTableItem> tryToMatch;

		// Token: 0x0400012D RID: 301
		private Dictionary<TimeTableItem, int> matches;

		// Token: 0x0400012E RID: 302
		private List<int> studentPidsAssignedTo;
	}
}
