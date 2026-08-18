using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.Impl.Adapters
{
	// Token: 0x02000184 RID: 388
	public static class PeopleGroupAdapter
	{
		// Token: 0x06000B73 RID: 2931 RVA: 0x00079368 File Offset: 0x00077568
		public static eCoreGroup GetCoreGroup(this Group group)
		{
			return (group == null) ? eCoreGroup.Unknown : group.GroupId.GetCoreGroup();
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0007938C File Offset: 0x0007758C
		public static eCoreGroup GetCoreGroup(this int GroupId)
		{
			bool flag = Enum.IsDefined(typeof(eCoreGroup), GroupId);
			eCoreGroup result;
			if (flag)
			{
				result = (eCoreGroup)GroupId;
			}
			else
			{
				result = eCoreGroup.Unknown;
			}
			return result;
		}
	}
}
