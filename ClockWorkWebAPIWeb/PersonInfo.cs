using System;
using System.Collections.Generic;

namespace ClockWorkWebAPIWeb
{
	// Token: 0x02000016 RID: 22
	[Serializable]
	public class PersonInfo
	{
		// Token: 0x0600012F RID: 303 RVA: 0x0000F988 File Offset: 0x0000DB88
		public PersonInfo(string id, string name, string summary)
		{
			this.id = id;
			this.name = name;
			this.summary = summary;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000F9A8 File Offset: 0x0000DBA8
		public string Summary
		{
			get
			{
				return this.summary;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000131 RID: 305 RVA: 0x0000F9C0 File Offset: 0x0000DBC0
		public string ID
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000F9D8 File Offset: 0x0000DBD8
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000F9F0 File Offset: 0x0000DBF0
		public static void RemovePersonFromList(ref List<PersonInfo> people, string displayName)
		{
			PersonInfo personInfo = null;
			foreach (PersonInfo personInfo2 in people)
			{
				bool flag = personInfo2.Name.Equals(displayName);
				if (flag)
				{
					personInfo = personInfo2;
					break;
				}
			}
			bool flag2 = personInfo != null;
			if (flag2)
			{
				people.Remove(personInfo);
			}
			personInfo = null;
		}

		// Token: 0x0400007C RID: 124
		private string id;

		// Token: 0x0400007D RID: 125
		private string name;

		// Token: 0x0400007E RID: 126
		private string summary;
	}
}
