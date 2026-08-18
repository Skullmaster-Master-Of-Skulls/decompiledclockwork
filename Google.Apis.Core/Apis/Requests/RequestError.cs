using System;
using System.Collections.Generic;
using System.Text;
using Google.Apis.Util;

namespace Google.Apis.Requests
{
	// Token: 0x02000013 RID: 19
	public class RequestError
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002D37 File Offset: 0x00000F37
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002D3F File Offset: 0x00000F3F
		public IList<SingleError> Errors { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002D48 File Offset: 0x00000F48
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002D50 File Offset: 0x00000F50
		public int Code { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002D59 File Offset: 0x00000F59
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002D61 File Offset: 0x00000F61
		public string Message { get; set; }

		// Token: 0x06000056 RID: 86 RVA: 0x00002D6C File Offset: 0x00000F6C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(base.GetType().FullName).Append(this.Message).AppendFormat(" [{0}]", this.Code).AppendLine();
			if (this.Errors.IsNullOrEmpty<SingleError>())
			{
				stringBuilder.AppendLine("No individual errors");
			}
			else
			{
				stringBuilder.AppendLine("Errors [");
				foreach (SingleError singleError in this.Errors)
				{
					stringBuilder.Append('\t').AppendLine(singleError.ToString());
				}
				stringBuilder.AppendLine("]");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0200003D RID: 61
		public enum ErrorCodes
		{
			// Token: 0x0400007E RID: 126
			ETagConditionFailed = 412
		}
	}
}
