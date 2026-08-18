using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.Context
{
	// Token: 0x02000772 RID: 1906
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(CustomDataPerDateContextDTO))]
	[KnownType(typeof(CustomDataPerSemesterContextDTO))]
	[KnownType(typeof(CustomDataPerStudentContextDTO))]
	public class CustomDataContextDTO
	{
		// Token: 0x0600272A RID: 10026 RVA: 0x000036BD File Offset: 0x000018BD
		public CustomDataContextDTO()
		{
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x000036BD File Offset: 0x000018BD
		public CustomDataContextDTO(string parameters)
		{
		}

		// Token: 0x0600272C RID: 10028 RVA: 0x0001239C File Offset: 0x0001059C
		protected IDictionary<string, string> Parse(string parameters)
		{
			List<string> source = parameters.Split(new char[]
			{
				'|'
			})[1].Split(new char[]
			{
				','
			}).ToList<string>();
			return source.ToDictionary((string i) => i.Split(new char[]
			{
				'='
			})[0], (string i) => i.Split(new char[]
			{
				'='
			})[1]);
		}
	}
}
