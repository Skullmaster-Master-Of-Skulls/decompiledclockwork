using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.MailMergeValues;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000468 RID: 1128
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeCodeDTO
	{
		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x0600180C RID: 6156 RVA: 0x0000B17A File Offset: 0x0000937A
		// (set) Token: 0x0600180D RID: 6157 RVA: 0x0000B182 File Offset: 0x00009382
		[DataMember]
		public string Name { get; set; }

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x0000B18B File Offset: 0x0000938B
		// (set) Token: 0x0600180F RID: 6159 RVA: 0x0000B193 File Offset: 0x00009393
		[DataMember]
		public string OriginalCode { get; set; }

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001810 RID: 6160 RVA: 0x0000B19C File Offset: 0x0000939C
		// (set) Token: 0x06001811 RID: 6161 RVA: 0x0000B1A4 File Offset: 0x000093A4
		[DataMember]
		public MailMergeValueFormatDTO ValueFormat { get; set; }

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001812 RID: 6162 RVA: 0x0000B1AD File Offset: 0x000093AD
		// (set) Token: 0x06001813 RID: 6163 RVA: 0x0000B1B5 File Offset: 0x000093B5
		[DataMember]
		public Dictionary<string, string> Args { get; set; }

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x0000B1BE File Offset: 0x000093BE
		// (set) Token: 0x06001815 RID: 6165 RVA: 0x0000B1C6 File Offset: 0x000093C6
		[DataMember]
		public string DefaultValue { get; set; }

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001816 RID: 6166 RVA: 0x0000B1CF File Offset: 0x000093CF
		// (set) Token: 0x06001817 RID: 6167 RVA: 0x0000B1D7 File Offset: 0x000093D7
		[DataMember]
		public IList<MailMergeValueBaseDTO> MailMergeValues { get; set; }

		// Token: 0x06001818 RID: 6168 RVA: 0x0000B1E0 File Offset: 0x000093E0
		public object GetValueObject()
		{
			bool flag = this.MailMergeValues == null || this.MailMergeValues.Count < 1;
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<object> list = (from h in this.MailMergeValues.ToList<MailMergeValueBaseDTO>().ConvertAll<object>((MailMergeValueBaseDTO g) => g.GetValueObject())
				where h != null
				select h).ToList<object>();
				bool flag2 = list.Count < 1;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = ((list.Count > 1) ? list : list[0]);
				}
			}
			return result;
		}
	}
}
