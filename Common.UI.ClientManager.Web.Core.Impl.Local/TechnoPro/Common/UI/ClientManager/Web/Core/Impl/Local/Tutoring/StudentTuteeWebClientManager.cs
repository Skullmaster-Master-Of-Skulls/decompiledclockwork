using System;
using TechnoPro.Common.ClientManager.Core.Tutoring;
using TechnoPro.Common.Public.Entities.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Cache;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Tutoring
{
	// Token: 0x02000013 RID: 19
	public class StudentTuteeWebClientManager : StudentTuteeClientManager
	{
		// Token: 0x06000070 RID: 112 RVA: 0x000042EC File Offset: 0x000024EC
		public override eTuteeStatus GetTuteeStatus(int StudentPersonId)
		{
			SessionCaching currentInstance = SessionCaching.CurrentInstance;
			string key = "StudentTuteeStatus";
			object obj = (StudentPersonId > 0) ? currentInstance[key] : null;
			bool flag = obj != null && obj is eTuteeStatus;
			eTuteeStatus result;
			if (flag)
			{
				result = (eTuteeStatus)obj;
			}
			else
			{
				bool flag2 = StudentPersonId < 1;
				if (flag2)
				{
					result = eTuteeStatus.NotAllowedToUseTutoring;
				}
				else
				{
					eTuteeStatus tuteeStatus = base.GetTuteeStatus(StudentPersonId);
					bool flag3 = tuteeStatus == eTuteeStatus.NotAllowedToUseTutoring;
					if (flag3)
					{
						currentInstance.Insert(key, tuteeStatus, TimeSpan.FromMinutes(10.0));
					}
					else
					{
						currentInstance.Insert(key, tuteeStatus, TimeSpan.FromMinutes(60.0));
					}
					result = tuteeStatus;
				}
			}
			return result;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004398 File Offset: 0x00002598
		public override void RecordConfidentialityAgreementSignedByStudent(int StudentPersonId)
		{
			base.RecordConfidentialityAgreementSignedByStudent(StudentPersonId);
			SessionCaching currentInstance = SessionCaching.CurrentInstance;
			string key = "StudentTuteeStatus";
			currentInstance.Remove(key);
		}

		// Token: 0x04000015 RID: 21
		private const string key_status = "StudentTuteeStatus";
	}
}
