using System;
using System.ComponentModel.DataAnnotations;

namespace System.Web.ModelBinding
{
	// Token: 0x0200064B RID: 1611
	internal static class DataTypeUtil
	{
		// Token: 0x06004F8F RID: 20367 RVA: 0x001144BC File Offset: 0x001126BC
		internal static string ToDataTypeName(this DataTypeAttribute attribute, Func<DataTypeAttribute, bool> isDataType = null)
		{
			if (isDataType == null)
			{
				isDataType = ((DataTypeAttribute t) => t.GetType().Equals(typeof(DataTypeAttribute)));
			}
			if (isDataType(attribute))
			{
				switch (attribute.DataType)
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
				}
			}
			return attribute.GetDataTypeName();
		}

		// Token: 0x04002A7A RID: 10874
		internal static readonly string CurrencyTypeName = DataType.Currency.ToString();

		// Token: 0x04002A7B RID: 10875
		internal static readonly string DateTypeName = DataType.Date.ToString();

		// Token: 0x04002A7C RID: 10876
		internal static readonly string DateTimeTypeName = DataType.DateTime.ToString();

		// Token: 0x04002A7D RID: 10877
		internal static readonly string DurationTypeName = DataType.Duration.ToString();

		// Token: 0x04002A7E RID: 10878
		internal static readonly string EmailAddressTypeName = DataType.EmailAddress.ToString();

		// Token: 0x04002A7F RID: 10879
		internal static readonly string HtmlTypeName = DataType.Html.ToString();

		// Token: 0x04002A80 RID: 10880
		internal static readonly string ImageUrlTypeName = DataType.ImageUrl.ToString();

		// Token: 0x04002A81 RID: 10881
		internal static readonly string MultiLineTextTypeName = DataType.MultilineText.ToString();

		// Token: 0x04002A82 RID: 10882
		internal static readonly string PasswordTypeName = DataType.Password.ToString();

		// Token: 0x04002A83 RID: 10883
		internal static readonly string PhoneNumberTypeName = DataType.PhoneNumber.ToString();

		// Token: 0x04002A84 RID: 10884
		internal static readonly string TextTypeName = DataType.Text.ToString();

		// Token: 0x04002A85 RID: 10885
		internal static readonly string TimeTypeName = DataType.Time.ToString();

		// Token: 0x04002A86 RID: 10886
		internal static readonly string UrlTypeName = DataType.Url.ToString();

		// Token: 0x04002A87 RID: 10887
		internal static readonly string CreditCardTypeName = DataType.CreditCard.ToString();

		// Token: 0x04002A88 RID: 10888
		internal static readonly string PostalCodeTypeName = DataType.PostalCode.ToString();

		// Token: 0x04002A89 RID: 10889
		internal static readonly string UploadTypeName = DataType.Upload.ToString();
	}
}
