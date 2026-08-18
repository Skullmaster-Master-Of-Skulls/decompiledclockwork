using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Public.Entities.Membership;

namespace TechnoPro.Common.Core.Membership
{
	// Token: 0x020000B6 RID: 182
	internal static class AuthenticationSessionAdapter
	{
		// Token: 0x060006D1 RID: 1745 RVA: 0x00027C9C File Offset: 0x00025E9C
		public static IList<AuthenticationSession> GetAllSessionsWithDistinctIpAddress(this IEnumerable<AuthenticationSession> sessions)
		{
			Dictionary<string, AuthenticationSession> dictionary = new Dictionary<string, AuthenticationSession>();
			foreach (AuthenticationSession authenticationSession in sessions)
			{
				bool flag = authenticationSession.IsTimeout();
				if (!flag)
				{
					string key = authenticationSession.ClientParameters["IP"];
					bool flag2 = !dictionary.ContainsKey(key);
					if (flag2)
					{
						dictionary.Add(key, authenticationSession);
					}
				}
			}
			return dictionary.Values.ToList<AuthenticationSession>();
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00027D34 File Offset: 0x00025F34
		public static bool IsTimeout(this AuthenticationSession session)
		{
			return !session.NeverExpires && (DateTime.Now.Subtract(session.LastCheckedTime) > MembershipManager.TokenMaxIdleTimeInterval || DateTime.Now.Subtract(session.IssuedOn) > MembershipManager.TokenMaxLifeTimeInterval);
		}
	}
}
