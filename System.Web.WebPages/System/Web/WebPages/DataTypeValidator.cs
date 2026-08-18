using System;

namespace System.Web.WebPages
{
	// Token: 0x0200005C RID: 92
	internal class DataTypeValidator : RequestFieldValidatorBase
	{
		// Token: 0x0600022F RID: 559 RVA: 0x00008DC0 File Offset: 0x00006FC0
		public DataTypeValidator(DataTypeValidator.SupportedValidationDataType type, string errorMessage = null) : base(errorMessage)
		{
			this._dataType = type;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00008DD0 File Offset: 0x00006FD0
		protected override bool IsValid(HttpContextBase httpContext, string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return true;
			}
			switch (this._dataType)
			{
			case DataTypeValidator.SupportedValidationDataType.DateTime:
				return value.IsDateTime();
			case DataTypeValidator.SupportedValidationDataType.Decimal:
				return value.IsDecimal();
			case DataTypeValidator.SupportedValidationDataType.Url:
				return Uri.IsWellFormedUriString(value, UriKind.Absolute);
			case DataTypeValidator.SupportedValidationDataType.Integer:
				return value.IsInt();
			case DataTypeValidator.SupportedValidationDataType.Float:
				return value.IsFloat();
			default:
				return true;
			}
		}

		// Token: 0x040000B8 RID: 184
		private readonly DataTypeValidator.SupportedValidationDataType _dataType;

		// Token: 0x0200005D RID: 93
		public enum SupportedValidationDataType
		{
			// Token: 0x040000BA RID: 186
			DateTime,
			// Token: 0x040000BB RID: 187
			Decimal,
			// Token: 0x040000BC RID: 188
			Url,
			// Token: 0x040000BD RID: 189
			Integer,
			// Token: 0x040000BE RID: 190
			Float
		}
	}
}
