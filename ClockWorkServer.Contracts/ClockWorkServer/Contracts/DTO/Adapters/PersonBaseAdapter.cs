using System;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Adapters
{
	// Token: 0x02000C8A RID: 3210
	public static class PersonBaseAdapter
	{
		// Token: 0x060042DE RID: 17118 RVA: 0x00022A14 File Offset: 0x00020C14
		public static BasicPersonDTO BasicPersonFromPersonBase(this PersonBaseDTO personBase)
		{
			bool flag = personBase == null;
			BasicPersonDTO result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new BasicPersonDTO
				{
					PersonId = personBase.PersonId,
					FirstName = personBase.FirstName,
					MiddleName = personBase.MiddleName,
					LastName = personBase.LastName,
					StudentNumber = personBase.Student_no
				};
			}
			return result;
		}

		// Token: 0x060042DF RID: 17119 RVA: 0x00022A78 File Offset: 0x00020C78
		public static string GetName(this PersonBaseDTO Person)
		{
			return (Person == null) ? "" : (Person.FirstName + " " + Person.LastName);
		}

		// Token: 0x060042E0 RID: 17120 RVA: 0x00022AAC File Offset: 0x00020CAC
		public static string GetNameLastFirst(this PersonBaseDTO Person)
		{
			bool flag = Person == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = string.Join(", ", (from g in new string[]
				{
					Person.LastName ?? "",
					Person.FirstName ?? ""
				}
				where g.Length > 0
				select g).ToArray<string>());
			}
			return result;
		}

		// Token: 0x060042E1 RID: 17121 RVA: 0x00022B2C File Offset: 0x00020D2C
		public static string GetStudentName(this PersonBaseDTO Person)
		{
			return (Person == null) ? "" : string.Concat(new string[]
			{
				Person.LastName,
				", ",
				Person.FirstName,
				" . ",
				Person.Student_no
			});
		}

		// Token: 0x060042E2 RID: 17122 RVA: 0x00022B80 File Offset: 0x00020D80
		public static string GetName(this BasicPersonDTO Person)
		{
			return (Person == null) ? "" : (Person.FirstName + " " + Person.LastName);
		}

		// Token: 0x060042E3 RID: 17123 RVA: 0x00022BB4 File Offset: 0x00020DB4
		public static string GetStudentName(this BasicPersonDTO Person)
		{
			return (Person == null) ? "" : string.Concat(new string[]
			{
				Person.LastName,
				", ",
				Person.FirstName,
				" . ",
				Person.StudentNumber
			});
		}

		// Token: 0x060042E4 RID: 17124 RVA: 0x00022C08 File Offset: 0x00020E08
		public static string GetStudentNameWithMiddleName(this PersonBase Person)
		{
			return (Person == null) ? "" : string.Concat(new string[]
			{
				Person.LastName,
				", ",
				string.IsNullOrEmpty(Person.MiddleName) ? Person.FirstName : (Person.FirstName + " " + Person.MiddleName),
				" . ",
				Person.Student_no
			});
		}

		// Token: 0x060042E5 RID: 17125 RVA: 0x00022C80 File Offset: 0x00020E80
		public static eCoreGroupDTO GetCoreGroupFromGroup(this GroupDTO Group)
		{
			bool flag = Enum.IsDefined(typeof(eCoreGroupDTO), Group.GroupId);
			eCoreGroupDTO result;
			if (flag)
			{
				result = (eCoreGroupDTO)Group.GroupId;
			}
			else
			{
				result = eCoreGroupDTO.Unknown;
			}
			return result;
		}
	}
}
