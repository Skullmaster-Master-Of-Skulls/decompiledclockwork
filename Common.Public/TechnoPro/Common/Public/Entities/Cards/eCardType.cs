using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Cards
{
	// Token: 0x0200046B RID: 1131
	[Serializable]
	public enum eCardType
	{
		// Token: 0x040019D3 RID: 6611
		[CardType(IsDisabled = true)]
		Unknown,
		// Token: 0x040019D4 RID: 6612
		[CardType(new eCoreGroup[]
		{
			eCoreGroup.Unknown
		})]
		Announcements,
		// Token: 0x040019D5 RID: 6613
		[CardType(new eCoreGroup[]
		{
			eCoreGroup.Unknown
		})]
		Notifications,
		// Token: 0x040019D6 RID: 6614
		[CardType(new eCoreGroup[]
		{
			eCoreGroup.Unknown
		})]
		MyAccount,
		// Token: 0x040019D7 RID: 6615
		[CardType(new eCoreGroup[]
		{
			eCoreGroup.Students
		})]
		VetsApplicationsStudent,
		// Token: 0x040019D8 RID: 6616
		[CardType(new eCoreGroup[]
		{
			eCoreGroup.Staff
		})]
		VetsApplicationStaffMyApplications,
		// Token: 0x040019D9 RID: 6617
		[CardType(new eCoreGroup[]
		{
			eCoreGroup.Staff
		})]
		VetsApplicationStaffUnassignedApplications,
		// Token: 0x040019DA RID: 6618
		[CardType(new eCoreGroup[]
		{
			eCoreGroup.Staff
		})]
		ReportsStaff
	}
}
