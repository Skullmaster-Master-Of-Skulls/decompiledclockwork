using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x020000E2 RID: 226
	public class ClientDataTypeModelValidatorProvider : ModelValidatorProvider
	{
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x0000FC5F File Offset: 0x0000DE5F
		// (set) Token: 0x060005D4 RID: 1492 RVA: 0x0000FC6F File Offset: 0x0000DE6F
		public static string ResourceClassKey
		{
			get
			{
				return ClientDataTypeModelValidatorProvider._resourceClassKey ?? string.Empty;
			}
			set
			{
				ClientDataTypeModelValidatorProvider._resourceClassKey = value;
			}
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0000FC77 File Offset: 0x0000DE77
		public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
		{
			if (metadata == null)
			{
				throw new ArgumentNullException("metadata");
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			return ClientDataTypeModelValidatorProvider.GetValidatorsImpl(metadata, context);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0000FDDC File Offset: 0x0000DFDC
		private static IEnumerable<ModelValidator> GetValidatorsImpl(ModelMetadata metadata, ControllerContext context)
		{
			Type type = metadata.ModelType;
			if (ClientDataTypeModelValidatorProvider.IsDateTimeType(type, metadata))
			{
				yield return new ClientDataTypeModelValidatorProvider.DateModelValidator(metadata, context);
			}
			if (ClientDataTypeModelValidatorProvider.IsNumericType(type))
			{
				yield return new ClientDataTypeModelValidatorProvider.NumericModelValidator(metadata, context);
			}
			yield break;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0000FE00 File Offset: 0x0000E000
		private static bool IsNumericType(Type type)
		{
			return ClientDataTypeModelValidatorProvider._numericTypes.Contains(ClientDataTypeModelValidatorProvider.GetTypeToValidate(type));
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0000FE12 File Offset: 0x0000E012
		private static bool IsDateTimeType(Type type, ModelMetadata metadata)
		{
			return typeof(DateTime) == ClientDataTypeModelValidatorProvider.GetTypeToValidate(type) && !string.Equals(metadata.DataTypeName, "Time", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0000FE41 File Offset: 0x0000E041
		private static Type GetTypeToValidate(Type type)
		{
			return Nullable.GetUnderlyingType(type) ?? type;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0000FE50 File Offset: 0x0000E050
		private static string GetUserResourceString(ControllerContext controllerContext, string resourceName)
		{
			string result = null;
			if (!string.IsNullOrEmpty(ClientDataTypeModelValidatorProvider.ResourceClassKey) && controllerContext != null && controllerContext.HttpContext != null)
			{
				result = (controllerContext.HttpContext.GetGlobalResourceObject(ClientDataTypeModelValidatorProvider.ResourceClassKey, resourceName, CultureInfo.CurrentUICulture) as string);
			}
			return result;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0000FE93 File Offset: 0x0000E093
		private static string GetFieldMustBeNumericResource(ControllerContext controllerContext)
		{
			return ClientDataTypeModelValidatorProvider.GetUserResourceString(controllerContext, "FieldMustBeNumeric") ?? MvcResources.ClientDataTypeModelValidatorProvider_FieldMustBeNumeric;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0000FEA9 File Offset: 0x0000E0A9
		private static string GetFieldMustBeDateResource(ControllerContext controllerContext)
		{
			return ClientDataTypeModelValidatorProvider.GetUserResourceString(controllerContext, "FieldMustBeDate") ?? MvcResources.ClientDataTypeModelValidatorProvider_FieldMustBeDate;
		}

		// Token: 0x040001A2 RID: 418
		private static readonly HashSet<Type> _numericTypes = new HashSet<Type>(new Type[]
		{
			typeof(byte),
			typeof(sbyte),
			typeof(short),
			typeof(ushort),
			typeof(int),
			typeof(uint),
			typeof(long),
			typeof(ulong),
			typeof(float),
			typeof(double),
			typeof(decimal)
		});

		// Token: 0x040001A3 RID: 419
		private static string _resourceClassKey;

		// Token: 0x020000E3 RID: 227
		internal class ClientModelValidator : ModelValidator
		{
			// Token: 0x060005DF RID: 1503 RVA: 0x0000FF7C File Offset: 0x0000E17C
			public ClientModelValidator(ModelMetadata metadata, ControllerContext controllerContext, string validationType, string errorMessage) : base(metadata, controllerContext)
			{
				if (string.IsNullOrEmpty(validationType))
				{
					throw new ArgumentException(MvcResources.Common_NullOrEmpty, "validationType");
				}
				if (string.IsNullOrEmpty(errorMessage))
				{
					throw new ArgumentException(MvcResources.Common_NullOrEmpty, "errorMessage");
				}
				this._validationType = validationType;
				this._errorMessage = errorMessage;
			}

			// Token: 0x060005E0 RID: 1504 RVA: 0x0000FFD4 File Offset: 0x0000E1D4
			public sealed override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
			{
				ModelClientValidationRule modelClientValidationRule = new ModelClientValidationRule
				{
					ValidationType = this._validationType,
					ErrorMessage = this.FormatErrorMessage(base.Metadata.GetDisplayName())
				};
				return new ModelClientValidationRule[]
				{
					modelClientValidationRule
				};
			}

			// Token: 0x060005E1 RID: 1505 RVA: 0x00010018 File Offset: 0x0000E218
			private string FormatErrorMessage(string displayName)
			{
				return string.Format(CultureInfo.CurrentCulture, this._errorMessage, new object[]
				{
					displayName
				});
			}

			// Token: 0x060005E2 RID: 1506 RVA: 0x00010041 File Offset: 0x0000E241
			public sealed override IEnumerable<ModelValidationResult> Validate(object container)
			{
				return Enumerable.Empty<ModelValidationResult>();
			}

			// Token: 0x040001A4 RID: 420
			private string _errorMessage;

			// Token: 0x040001A5 RID: 421
			private string _validationType;
		}

		// Token: 0x020000E4 RID: 228
		internal sealed class DateModelValidator : ClientDataTypeModelValidatorProvider.ClientModelValidator
		{
			// Token: 0x060005E3 RID: 1507 RVA: 0x00010048 File Offset: 0x0000E248
			public DateModelValidator(ModelMetadata metadata, ControllerContext controllerContext) : base(metadata, controllerContext, "date", ClientDataTypeModelValidatorProvider.GetFieldMustBeDateResource(controllerContext))
			{
			}
		}

		// Token: 0x020000E5 RID: 229
		internal sealed class NumericModelValidator : ClientDataTypeModelValidatorProvider.ClientModelValidator
		{
			// Token: 0x060005E4 RID: 1508 RVA: 0x0001005D File Offset: 0x0000E25D
			public NumericModelValidator(ModelMetadata metadata, ControllerContext controllerContext) : base(metadata, controllerContext, "number", ClientDataTypeModelValidatorProvider.GetFieldMustBeNumericResource(controllerContext))
			{
			}
		}
	}
}
