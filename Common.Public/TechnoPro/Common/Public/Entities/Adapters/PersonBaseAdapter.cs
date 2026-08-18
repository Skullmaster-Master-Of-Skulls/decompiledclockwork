using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005DD RID: 1501
	public static class PersonBaseAdapter
	{
		// Token: 0x06003047 RID: 12359 RVA: 0x0003E4AC File Offset: 0x0003C6AC
		public static string GetName(this PersonBase Person)
		{
			return string.Format("{0} {1}", Person.FirstName, Person.LastName);
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x0003E4D4 File Offset: 0x0003C6D4
		public static string GetStudentName(this PersonBase Person)
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

		// Token: 0x06003049 RID: 12361 RVA: 0x0003E514 File Offset: 0x0003C714
		public static string GetStudentNameWithMiddleName(this PersonBase Person)
		{
			bool flag = Person == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = string.Format("{0}, {1} . {2}", Person.LastName, string.IsNullOrEmpty(Person.MiddleName) ? Person.FirstName : (Person.FirstName + " " + Person.MiddleName), Person.Student_no);
			}
			return result;
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x0003E578 File Offset: 0x0003C778
		public static BasicPerson ToBasicPerson(this PersonBase person)
		{
			bool flag = person == null;
			BasicPerson result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new BasicPerson
				{
					PersonId = person.PersonId,
					FirstName = person.FirstName,
					MiddleName = person.MiddleName,
					LastName = person.LastName,
					StudentNumber = person.Student_no
				};
			}
			return result;
		}
	}
}
