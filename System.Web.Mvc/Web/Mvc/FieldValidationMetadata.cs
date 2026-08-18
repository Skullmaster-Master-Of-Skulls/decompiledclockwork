using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Web.Mvc
{
	// Token: 0x02000147 RID: 327
	public class FieldValidationMetadata
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x00017743 File Offset: 0x00015943
		// (set) Token: 0x06000873 RID: 2163 RVA: 0x00017754 File Offset: 0x00015954
		public string FieldName
		{
			get
			{
				return this._fieldName ?? string.Empty;
			}
			set
			{
				this._fieldName = value;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x0001775D File Offset: 0x0001595D
		// (set) Token: 0x06000875 RID: 2165 RVA: 0x00017765 File Offset: 0x00015965
		public bool ReplaceValidationMessageContents { get; set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x0001776E File Offset: 0x0001596E
		// (set) Token: 0x06000877 RID: 2167 RVA: 0x00017776 File Offset: 0x00015976
		public string ValidationMessageId { get; set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x0001777F File Offset: 0x0001597F
		public ICollection<ModelClientValidationRule> ValidationRules
		{
			get
			{
				return this._validationRules;
			}
		}

		// Token: 0x0400025D RID: 605
		private readonly Collection<ModelClientValidationRule> _validationRules = new Collection<ModelClientValidationRule>();

		// Token: 0x0400025E RID: 606
		private string _fieldName;
	}
}
