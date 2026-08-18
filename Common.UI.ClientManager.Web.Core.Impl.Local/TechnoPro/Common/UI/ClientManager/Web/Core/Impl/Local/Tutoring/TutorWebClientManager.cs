using System;
using ClockWorkLogger;
using TechnoPro.Common.ClientManager.Core.Tutoring;
using TechnoPro.Common.Public.Entities.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Cache;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Tutoring
{
	// Token: 0x02000015 RID: 21
	public class TutorWebClientManager : TutorClientManager
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00004634 File Offset: 0x00002834
		private void ClearStatusCache()
		{
			SessionCaching currentInstance = SessionCaching.CurrentInstance;
			string key = "TutorStatus";
			currentInstance.Remove(key);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004656 File Offset: 0x00002856
		public override void RecordConfidentialityAgreementSignedByTutor(int TutorPersonId)
		{
			base.RecordConfidentialityAgreementSignedByTutor(TutorPersonId);
			this.ClearStatusCache();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004668 File Offset: 0x00002868
		public override eTutorStatus GetTutorStatus(int TutorPersonId)
		{
			SessionCaching currentInstance = SessionCaching.CurrentInstance;
			string key = "TutorStatus";
			object obj = (TutorPersonId > 0) ? currentInstance[key] : null;
			bool flag = obj != null && obj is eTutorStatus;
			eTutorStatus result;
			if (flag)
			{
				eTutorStatus eTutorStatus = (eTutorStatus)obj;
				CWLogger.Logger.Trace("TutorWebClientManager:GetTutorStatus:GotTutorStatusFromCache:pid={0}:status={1}", TutorPersonId.ToString(), eTutorStatus.ToString());
				result = eTutorStatus;
			}
			else
			{
				bool flag2 = TutorPersonId < 1;
				if (flag2)
				{
					CWLogger.Logger.Trace("TutorWebClientManager:GetTutorStatus:GotTutorStatusFromTutorPersonId<1:pid={0}:status={1}", TutorPersonId.ToString(), "NotATutor");
					result = eTutorStatus.NotATutor;
				}
				else
				{
					eTutorStatus tutorStatus = base.GetTutorStatus(TutorPersonId);
					CWLogger.Logger.Trace("TutorWebClientManager:GetTutorStatus:GotTutorStatusFromManager:pid={0}:status={1}", TutorPersonId.ToString(), tutorStatus.ToString());
					bool flag3 = tutorStatus == eTutorStatus.NotATutor || tutorStatus == eTutorStatus.TutorNotActive;
					if (flag3)
					{
						currentInstance.Insert(key, tutorStatus, TimeSpan.FromMinutes(10.0));
					}
					else
					{
						currentInstance.Insert(key, tutorStatus, TimeSpan.FromMinutes(60.0));
					}
					result = tutorStatus;
				}
			}
			return result;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004788 File Offset: 0x00002988
		public override int CreateTutor(string FirstName, string MiddleName, string LastName, string StudentNumber)
		{
			int result = base.CreateTutor(FirstName, MiddleName, LastName, StudentNumber);
			this.ClearStatusCache();
			return result;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000047AE File Offset: 0x000029AE
		public override void RegisterTutorByExistingPersonId(int PersonId)
		{
			base.RegisterTutorByExistingPersonId(PersonId);
			this.ClearStatusCache();
		}

		// Token: 0x04000016 RID: 22
		private const string key_status = "TutorStatus";
	}
}
