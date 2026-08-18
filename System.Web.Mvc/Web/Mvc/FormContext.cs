using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace System.Web.Mvc
{
	// Token: 0x02000148 RID: 328
	public class FormContext
	{
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x0001779A File Offset: 0x0001599A
		public IDictionary<string, FieldValidationMetadata> FieldValidators
		{
			get
			{
				return this._fieldValidators;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x000177A2 File Offset: 0x000159A2
		// (set) Token: 0x0600087C RID: 2172 RVA: 0x000177AA File Offset: 0x000159AA
		public string FormId { get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x000177B3 File Offset: 0x000159B3
		// (set) Token: 0x0600087E RID: 2174 RVA: 0x000177BB File Offset: 0x000159BB
		public bool ReplaceValidationSummary { get; set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x000177C4 File Offset: 0x000159C4
		// (set) Token: 0x06000880 RID: 2176 RVA: 0x000177CC File Offset: 0x000159CC
		public string ValidationSummaryId { get; set; }

		// Token: 0x06000881 RID: 2177 RVA: 0x000177D8 File Offset: 0x000159D8
		public string GetJsonValidationMetadata()
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			SortedDictionary<string, object> sortedDictionary = new SortedDictionary<string, object>
			{
				{
					"Fields",
					this.FieldValidators.Values
				},
				{
					"FormId",
					this.FormId
				}
			};
			if (!string.IsNullOrEmpty(this.ValidationSummaryId))
			{
				sortedDictionary["ValidationSummaryId"] = this.ValidationSummaryId;
			}
			sortedDictionary["ReplaceValidationSummary"] = this.ReplaceValidationSummary;
			return javaScriptSerializer.Serialize(sortedDictionary);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00017855 File Offset: 0x00015A55
		public FieldValidationMetadata GetValidationMetadataForField(string fieldName)
		{
			return this.GetValidationMetadataForField(fieldName, false);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00017860 File Offset: 0x00015A60
		public FieldValidationMetadata GetValidationMetadataForField(string fieldName, bool createIfNotFound)
		{
			if (string.IsNullOrEmpty(fieldName))
			{
				throw Error.ParameterCannotBeNullOrEmpty("fieldName");
			}
			FieldValidationMetadata fieldValidationMetadata;
			if (!this.FieldValidators.TryGetValue(fieldName, out fieldValidationMetadata) && createIfNotFound)
			{
				fieldValidationMetadata = new FieldValidationMetadata
				{
					FieldName = fieldName
				};
				this.FieldValidators[fieldName] = fieldValidationMetadata;
			}
			return fieldValidationMetadata;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x000178B0 File Offset: 0x00015AB0
		public bool RenderedField(string fieldName)
		{
			bool result;
			this._renderedFields.TryGetValue(fieldName, out result);
			return result;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x000178CD File Offset: 0x00015ACD
		public void RenderedField(string fieldName, bool value)
		{
			this._renderedFields[fieldName] = value;
		}

		// Token: 0x04000261 RID: 609
		private readonly Dictionary<string, FieldValidationMetadata> _fieldValidators = new Dictionary<string, FieldValidationMetadata>();

		// Token: 0x04000262 RID: 610
		private readonly Dictionary<string, bool> _renderedFields = new Dictionary<string, bool>();
	}
}
