using System;
using System.Collections.Generic;
using System.Text;

namespace TechnoPro.Common.Public.Entities.Membership
{
	// Token: 0x020002AA RID: 682
	public static class RoleAdapter
	{
		// Token: 0x06001497 RID: 5271 RVA: 0x0001A0A0 File Offset: 0x000182A0
		public static string GetCSVRoles(this IList<Role> roles)
		{
			bool flag = roles.Count == 0;
			string result;
			if (flag)
			{
				result = "User";
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (Role role in roles)
				{
					stringBuilder.Append(role.Name + ",");
				}
				result = stringBuilder.ToString(0, stringBuilder.Length - 1);
			}
			return result;
		}
	}
}
