using System;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.UI.Web.Entity.People;

namespace TechnoPro.Common.UI.Web.Entity.Adapters
{
	// Token: 0x02000058 RID: 88
	public static class PersonBaseAdapter
	{
		// Token: 0x06000280 RID: 640 RVA: 0x00005EFC File Offset: 0x000040FC
		public static string GetStudentName(this PersonBaseView Person)
		{
			bool flag = Person == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = string.Format("{0}, {1} . {2}", Person.LastName, Person.FirstName, Person.Student_no);
			}
			return result;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00005F3C File Offset: 0x0000413C
		public static string GetName(this PersonBaseView Person)
		{
			bool flag = Person == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				eCoreGroup? coreGroup = Person.CoreGroup;
				eCoreGroup? eCoreGroup = coreGroup;
				if (eCoreGroup != null)
				{
					eCoreGroup valueOrDefault = eCoreGroup.GetValueOrDefault();
					if (valueOrDefault == eCoreGroup.Students)
					{
						return Person.GetStudentName();
					}
					if (valueOrDefault - eCoreGroup.Rooms <= 1)
					{
						return Person.FirstName ?? "";
					}
				}
				result = string.Format("{0} {1}", Person.FirstName ?? "", Person.LastName ?? "");
			}
			return result;
		}
	}
}
