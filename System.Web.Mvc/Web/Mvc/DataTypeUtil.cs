using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc
{
	// Token: 0x0200008A RID: 138
	internal static class DataTypeUtil
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x0000BC80 File Offset: 0x00009E80
		internal static string ToDataTypeName(this DataTypeAttribute attribute, Func<DataTypeAttribute, bool> isDataType = null)
		{
			if (isDataType == null)
			{
				isDataType = ((DataTypeAttribute t) => t.GetType().Equals(typeof(DataTypeAttribute)));
			}
			if (isDataType(attribute))
			{
				string text = DataTypeUtil.KnownDataTypeToString(attribute.DataType);
				if (text == null)
				{
					DataTypeUtil._dataTypeToName.Value.TryGetValue(attribute.DataType, out text);
				}
				if (text != null)
				{
					return text;
				}
			}
			return attribute.GetDataTypeName();
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000BCF0 File Offset: 0x00009EF0
		private static string KnownDataTypeToString(DataType dataType)
		{
			switch (dataType)
			{
			case DataType.DateTime:
				return DataTypeUtil.DateTimeTypeName;
			case DataType.Date:
				return DataTypeUtil.DateTypeName;
			case DataType.Time:
				return DataTypeUtil.TimeTypeName;
			case DataType.Duration:
				return DataTypeUtil.DurationTypeName;
			case DataType.PhoneNumber:
				return DataTypeUtil.PhoneNumberTypeName;
			case DataType.Currency:
				return DataTypeUtil.CurrencyTypeName;
			case DataType.Text:
				return DataTypeUtil.TextTypeName;
			case DataType.Html:
				return DataTypeUtil.HtmlTypeName;
			case DataType.MultilineText:
				return DataTypeUtil.MultiLineTextTypeName;
			case DataType.EmailAddress:
				return DataTypeUtil.EmailAddressTypeName;
			case DataType.Password:
				return DataTypeUtil.PasswordTypeName;
			case DataType.Url:
				return DataTypeUtil.UrlTypeName;
			case DataType.ImageUrl:
				return DataTypeUtil.ImageUrlTypeName;
			case DataType.CreditCard:
				return DataTypeUtil.CreditCardTypeName;
			case DataType.PostalCode:
				return DataTypeUtil.PostalCodeTypeName;
			case DataType.Upload:
				return DataTypeUtil.UploadTypeName;
			default:
				return null;
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000BDAC File Offset: 0x00009FAC
		private static Dictionary<object, string> CreateDataTypeToName()
		{
			Dictionary<object, string> dictionary = new Dictionary<object, string>();
			foreach (object obj in Enum.GetValues(typeof(DataType)))
			{
				DataType dataType = (DataType)obj;
				if (dataType != DataType.Custom && DataTypeUtil.KnownDataTypeToString(dataType) == null)
				{
					string name = Enum.GetName(typeof(DataType), dataType);
					dictionary[dataType] = name;
				}
			}
			return dictionary;
		}

		// Token: 0x04000114 RID: 276
		private static readonly string CreditCardTypeName = DataType.CreditCard.ToString();

		// Token: 0x04000115 RID: 277
		private static readonly string CurrencyTypeName = DataType.Currency.ToString();

		// Token: 0x04000116 RID: 278
		private static readonly string DateTypeName = DataType.Date.ToString();

		// Token: 0x04000117 RID: 279
		private static readonly string DateTimeTypeName = DataType.DateTime.ToString();

		// Token: 0x04000118 RID: 280
		private static readonly string DurationTypeName = DataType.Duration.ToString();

		// Token: 0x04000119 RID: 281
		private static readonly string EmailAddressTypeName = DataType.EmailAddress.ToString();

		// Token: 0x0400011A RID: 282
		internal static readonly string HtmlTypeName = DataType.Html.ToString();

		// Token: 0x0400011B RID: 283
		private static readonly string ImageUrlTypeName = DataType.ImageUrl.ToString();

		// Token: 0x0400011C RID: 284
		private static readonly string MultiLineTextTypeName = DataType.MultilineText.ToString();

		// Token: 0x0400011D RID: 285
		private static readonly string PasswordTypeName = DataType.Password.ToString();

		// Token: 0x0400011E RID: 286
		private static readonly string PhoneNumberTypeName = DataType.PhoneNumber.ToString();

		// Token: 0x0400011F RID: 287
		private static readonly string PostalCodeTypeName = DataType.PostalCode.ToString();

		// Token: 0x04000120 RID: 288
		private static readonly string TextTypeName = DataType.Text.ToString();

		// Token: 0x04000121 RID: 289
		private static readonly string TimeTypeName = DataType.Time.ToString();

		// Token: 0x04000122 RID: 290
		private static readonly string UploadTypeName = DataType.Upload.ToString();

		// Token: 0x04000123 RID: 291
		private static readonly string UrlTypeName = DataType.Url.ToString();

		// Token: 0x04000124 RID: 292
		private static readonly Lazy<Dictionary<object, string>> _dataTypeToName = new Lazy<Dictionary<object, string>>(new Func<Dictionary<object, string>>(DataTypeUtil.CreateDataTypeToName), true);
	}
}
