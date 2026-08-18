using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D22 RID: 3362
	internal class SchemaElementValidator
	{
		// Token: 0x06007D39 RID: 32057 RVA: 0x001CB5BC File Offset: 0x001C97BC
		public virtual SchemaValidationResult Validate(SchemaElement element)
		{
			if (element == null)
			{
				return SchemaValidationResult.CreateInvalidResults();
			}
			IList<string> validationErrors = this.GetValidationErrors(element);
			return new SchemaValidationResult(validationErrors);
		}

		// Token: 0x06007D3A RID: 32058 RVA: 0x001CB5E2 File Offset: 0x001C97E2
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)", Justification = "Will fix soon.")]
		protected static string GetErrorForMissingProperty(string propertyName)
		{
			return string.Format("Required property is missing: {0}", propertyName);
		}

		// Token: 0x06007D3B RID: 32059 RVA: 0x001CB5F0 File Offset: 0x001C97F0
		protected virtual IList<string> GetValidationErrors(SchemaElement element)
		{
			List<string> list = new List<string>();
			if (SchemaElementValidator.NameIsInvalid(element))
			{
				string errorForMissingProperty = SchemaElementValidator.GetErrorForMissingProperty("Name");
				list.Add(errorForMissingProperty);
			}
			if (SchemaElementValidator.CatalogNameIsInvalid(element))
			{
				string errorForMissingProperty2 = SchemaElementValidator.GetErrorForMissingProperty("CatalogName");
				list.Add(errorForMissingProperty2);
			}
			if (SchemaElementValidator.CubeNameIsInvalid(element))
			{
				string errorForMissingProperty3 = SchemaElementValidator.GetErrorForMissingProperty("CubeName");
				list.Add(errorForMissingProperty3);
			}
			if (SchemaElementValidator.CaptionIsInvalid(element))
			{
				string errorForMissingProperty4 = SchemaElementValidator.GetErrorForMissingProperty("Caption");
				list.Add(errorForMissingProperty4);
			}
			return list;
		}

		// Token: 0x06007D3C RID: 32060 RVA: 0x001CB66E File Offset: 0x001C986E
		private static bool CaptionIsInvalid(SchemaElement element)
		{
			return element.Caption == null || element.Caption.Trim().Length == 0;
		}

		// Token: 0x06007D3D RID: 32061 RVA: 0x001CB68D File Offset: 0x001C988D
		private static bool CubeNameIsInvalid(SchemaElement element)
		{
			return element.CubeName == null || element.CubeName.Trim().Length == 0;
		}

		// Token: 0x06007D3E RID: 32062 RVA: 0x001CB6AC File Offset: 0x001C98AC
		private static bool CatalogNameIsInvalid(SchemaElement element)
		{
			return element.CatalogName == null || element.CatalogName.Trim().Length == 0;
		}

		// Token: 0x06007D3F RID: 32063 RVA: 0x001CB6CB File Offset: 0x001C98CB
		private static bool NameIsInvalid(SchemaElement element)
		{
			return element.Name == null || element.Name.Trim().Length == 0;
		}
	}
}
