using System;
using System.Configuration;

namespace System.Web.Profile
{
	// Token: 0x02000169 RID: 361
	public abstract class ProfileProvider : SettingsProvider
	{
		// Token: 0x06001439 RID: 5177
		public abstract int DeleteProfiles(ProfileInfoCollection profiles);

		// Token: 0x0600143A RID: 5178
		public abstract int DeleteProfiles(string[] usernames);

		// Token: 0x0600143B RID: 5179
		public abstract int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate);

		// Token: 0x0600143C RID: 5180
		public abstract int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate);

		// Token: 0x0600143D RID: 5181
		public abstract ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords);

		// Token: 0x0600143E RID: 5182
		public abstract ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords);

		// Token: 0x0600143F RID: 5183
		public abstract ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords);

		// Token: 0x06001440 RID: 5184
		public abstract ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords);
	}
}
