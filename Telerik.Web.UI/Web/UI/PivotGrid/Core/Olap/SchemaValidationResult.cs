using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D2F RID: 3375
	internal class SchemaValidationResult
	{
		// Token: 0x06007D9A RID: 32154 RVA: 0x001CBC8C File Offset: 0x001C9E8C
		public SchemaValidationResult(IEnumerable<string> validationErrors)
		{
			if (validationErrors == null)
			{
				this.ValidationErrors = new List<string>();
			}
			else
			{
				this.ValidationErrors = validationErrors;
			}
			int num = this.ValidationErrors.Count<string>();
			this.IsValid = (num <= 0);
		}

		// Token: 0x17002810 RID: 10256
		// (get) Token: 0x06007D9B RID: 32155 RVA: 0x001CBCCF File Offset: 0x001C9ECF
		// (set) Token: 0x06007D9C RID: 32156 RVA: 0x001CBCD7 File Offset: 0x001C9ED7
		public bool IsValid { get; private set; }

		// Token: 0x17002811 RID: 10257
		// (get) Token: 0x06007D9D RID: 32157 RVA: 0x001CBCE0 File Offset: 0x001C9EE0
		// (set) Token: 0x06007D9E RID: 32158 RVA: 0x001CBCE8 File Offset: 0x001C9EE8
		public IEnumerable<string> ValidationErrors { get; private set; }

		// Token: 0x06007D9F RID: 32159 RVA: 0x001CBCF4 File Offset: 0x001C9EF4
		public static SchemaValidationResult CreateInvalidResults()
		{
			return new SchemaValidationResult(null)
			{
				IsValid = false
			};
		}

		// Token: 0x06007DA0 RID: 32160 RVA: 0x001CBD10 File Offset: 0x001C9F10
		public string GetErrorsText()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string str in this.ValidationErrors)
			{
				stringBuilder.Append(str + "; ");
			}
			return stringBuilder.ToString();
		}
	}
}
