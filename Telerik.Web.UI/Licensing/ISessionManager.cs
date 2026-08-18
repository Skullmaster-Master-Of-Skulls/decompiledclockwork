using System;

namespace Telerik.Licensing
{
	// Token: 0x02000418 RID: 1048
	internal interface ISessionManager
	{
		// Token: 0x060025E5 RID: 9701
		Session GetSessionByName(SessionName name);

		// Token: 0x060025E6 RID: 9702
		Session GetCurrentSession();

		// Token: 0x060025E7 RID: 9703
		Session Create(SessionName name);

		// Token: 0x060025E8 RID: 9704
		bool Exists(SessionName name);

		// Token: 0x060025E9 RID: 9705
		void Save(Session session);

		// Token: 0x060025EA RID: 9706
		Session Load(SessionName name);
	}
}
