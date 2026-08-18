using System;
using System.Globalization;

namespace System.Web.ModelBinding
{
	// Token: 0x0200063F RID: 1599
	internal static class Error
	{
		// Token: 0x06004F29 RID: 20265 RVA: 0x00113108 File Offset: 0x00111308
		public static InvalidOperationException BindingBehavior_ValueNotFound(string fieldName)
		{
			string message = string.Format(CultureInfo.CurrentCulture, SR.GetString("BindingBehavior_ValueNotFound"), new object[]
			{
				fieldName
			});
			return new InvalidOperationException(message);
		}

		// Token: 0x06004F2A RID: 20266 RVA: 0x0011313C File Offset: 0x0011133C
		public static ArgumentException Common_TypeMustImplementInterface(Type providedType, Type requiredInterfaceType, string parameterName)
		{
			string message = string.Format(CultureInfo.CurrentCulture, SR.GetString("Common_TypeMustImplementInterface"), new object[]
			{
				providedType,
				requiredInterfaceType
			});
			return new ArgumentException(message, parameterName);
		}

		// Token: 0x06004F2B RID: 20267 RVA: 0x00113174 File Offset: 0x00111374
		public static ArgumentException GenericModelBinderProvider_ParameterMustSpecifyOpenGenericType(Type specifiedType, string parameterName)
		{
			string message = string.Format(CultureInfo.CurrentCulture, SR.GetString("GenericModelBinderProvider_ParameterMustSpecifyOpenGenericType"), new object[]
			{
				specifiedType
			});
			return new ArgumentException(message, parameterName);
		}

		// Token: 0x06004F2C RID: 20268 RVA: 0x001131A8 File Offset: 0x001113A8
		public static ArgumentException GenericModelBinderProvider_TypeArgumentCountMismatch(Type modelType, Type modelBinderType)
		{
			string message = string.Format(CultureInfo.CurrentCulture, SR.GetString("GenericModelBinderProvider_TypeArgumentCountMismatch"), new object[]
			{
				modelType,
				modelType.GetGenericArguments().Length,
				modelBinderType,
				modelBinderType.GetGenericArguments().Length
			});
			return new ArgumentException(message, "modelBinderType");
		}

		// Token: 0x06004F2D RID: 20269 RVA: 0x00113204 File Offset: 0x00111404
		public static InvalidOperationException ModelBinderProviderCollection_BinderForTypeNotFound(Type modelType)
		{
			string message = string.Format(CultureInfo.CurrentCulture, SR.GetString("ModelBinderProviderCollection_BinderForTypeNotFound"), new object[]
			{
				modelType
			});
			return new InvalidOperationException(message);
		}

		// Token: 0x06004F2E RID: 20270 RVA: 0x00113238 File Offset: 0x00111438
		public static ArgumentException ModelBinderUtil_ModelCannotBeNull(Type expectedType)
		{
			string message = string.Format(CultureInfo.CurrentCulture, SR.GetString("ModelBinderUtil_ModelCannotBeNull"), new object[]
			{
				expectedType
			});
			return new ArgumentException(message, "bindingContext");
		}

		// Token: 0x06004F2F RID: 20271 RVA: 0x00113270 File Offset: 0x00111470
		public static ArgumentException ModelBinderUtil_ModelInstanceIsWrong(Type actualType, Type expectedType)
		{
			string message = string.Format(CultureInfo.CurrentCulture, SR.GetString("ModelBinderUtil_ModelInstanceIsWrong"), new object[]
			{
				actualType,
				expectedType
			});
			return new ArgumentException(message, "bindingContext");
		}

		// Token: 0x06004F30 RID: 20272 RVA: 0x001132AB File Offset: 0x001114AB
		public static ArgumentException ModelBinderUtil_ModelMetadataCannotBeNull()
		{
			return new ArgumentException(SR.GetString("ModelBinderUtil_ModelMetadataCannotBeNull"), "bindingContext");
		}

		// Token: 0x06004F31 RID: 20273 RVA: 0x001132C4 File Offset: 0x001114C4
		public static ArgumentException ModelBinderUtil_ModelTypeIsWrong(Type actualType, Type expectedType)
		{
			string message = string.Format(CultureInfo.CurrentCulture, SR.GetString("ModelBinderUtil_ModelTypeIsWrong"), new object[]
			{
				actualType,
				expectedType
			});
			return new ArgumentException(message, "bindingContext");
		}

		// Token: 0x06004F32 RID: 20274 RVA: 0x001132FF File Offset: 0x001114FF
		public static InvalidOperationException ModelBindingContext_ModelMetadataMustBeSet()
		{
			return new InvalidOperationException(SR.GetString("ModelBindingContext_ModelMetadataMustBeSet"));
		}
	}
}
