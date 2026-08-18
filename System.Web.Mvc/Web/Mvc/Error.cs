using System;
using System.Globalization;
using System.Web.Mvc.Async;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x02000154 RID: 340
	internal static class Error
	{
		// Token: 0x060008B9 RID: 2233 RVA: 0x00018148 File Offset: 0x00016348
		public static InvalidOperationException AsyncActionMethodSelector_CouldNotFindMethod(string methodName, Type controllerType)
		{
			string message = string.Format(CultureInfo.CurrentCulture, MvcResources.AsyncActionMethodSelector_CouldNotFindMethod, new object[]
			{
				methodName,
				controllerType
			});
			return new InvalidOperationException(message);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0001817B File Offset: 0x0001637B
		public static InvalidOperationException AsyncCommon_AsyncResultAlreadyConsumed()
		{
			return new InvalidOperationException(MvcResources.AsyncCommon_AsyncResultAlreadyConsumed);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00018188 File Offset: 0x00016388
		public static InvalidOperationException AsyncCommon_ControllerMustImplementIAsyncManagerContainer(Type actualControllerType)
		{
			string message = string.Format(CultureInfo.CurrentCulture, MvcResources.AsyncCommon_ControllerMustImplementIAsyncManagerContainer, new object[]
			{
				actualControllerType
			});
			return new InvalidOperationException(message);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x000181B7 File Offset: 0x000163B7
		public static ArgumentException AsyncCommon_InvalidAsyncResult(string parameterName)
		{
			return new ArgumentException(MvcResources.AsyncCommon_InvalidAsyncResult, parameterName);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x000181C4 File Offset: 0x000163C4
		public static ArgumentOutOfRangeException AsyncCommon_InvalidTimeout(string parameterName)
		{
			return new ArgumentOutOfRangeException(parameterName, MvcResources.AsyncCommon_InvalidTimeout);
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x000181D4 File Offset: 0x000163D4
		public static InvalidOperationException ChildActionOnlyAttribute_MustBeInChildRequest(ActionDescriptor actionDescriptor)
		{
			string message = string.Format(CultureInfo.CurrentCulture, MvcResources.ChildActionOnlyAttribute_MustBeInChildRequest, new object[]
			{
				actionDescriptor.ActionName
			});
			return new InvalidOperationException(message);
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00018208 File Offset: 0x00016408
		public static ArgumentException ParameterCannotBeNullOrEmpty(string parameterName)
		{
			return new ArgumentException(MvcResources.Common_NullOrEmpty, parameterName);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00018218 File Offset: 0x00016418
		public static InvalidOperationException PropertyCannotBeNullOrEmpty(string propertyName)
		{
			string message = string.Format(CultureInfo.CurrentCulture, MvcResources.Common_PropertyCannotBeNullOrEmpty, new object[]
			{
				propertyName
			});
			return new InvalidOperationException(message);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00018247 File Offset: 0x00016447
		public static SynchronousOperationException SynchronizationContextUtil_ExceptionThrown(Exception innerException)
		{
			return new SynchronousOperationException(MvcResources.SynchronizationContextUtil_ExceptionThrown, innerException);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00018254 File Offset: 0x00016454
		public static InvalidOperationException ViewDataDictionary_WrongTModelType(Type valueType, Type modelType)
		{
			string message = string.Format(CultureInfo.CurrentCulture, MvcResources.ViewDataDictionary_WrongTModelType, new object[]
			{
				valueType,
				modelType
			});
			return new InvalidOperationException(message);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00018288 File Offset: 0x00016488
		public static InvalidOperationException ViewDataDictionary_ModelCannotBeNull(Type modelType)
		{
			string message = string.Format(CultureInfo.CurrentCulture, MvcResources.ViewDataDictionary_ModelCannotBeNull, new object[]
			{
				modelType
			});
			return new InvalidOperationException(message);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x000182B8 File Offset: 0x000164B8
		public static ArgumentOutOfRangeException ArgumentMustBeGreaterThanOrEqualTo(string parameterName, int actualValue, int minValue)
		{
			string message = string.Format(CultureInfo.CurrentCulture, MvcResources.ArgumentMustBeGreaterThanOrEqualTo, new object[]
			{
				minValue
			});
			return new ArgumentOutOfRangeException(parameterName, actualValue, message);
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x000182F3 File Offset: 0x000164F3
		public static Exception ArgumentNull(string parameterName)
		{
			return new ArgumentNullException(parameterName);
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x000182FC File Offset: 0x000164FC
		public static InvalidOperationException InvalidOperation(string messageFormat, params object[] args)
		{
			string message = string.Format(CultureInfo.CurrentCulture, messageFormat, args);
			return new InvalidOperationException(message);
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0001831C File Offset: 0x0001651C
		internal static string Format(string format, params object[] args)
		{
			return string.Format(CultureInfo.CurrentCulture, format, args);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0001832A File Offset: 0x0001652A
		internal static ArgumentException Argument(string parameterName, string messageFormat, params object[] messageArgs)
		{
			return new ArgumentException(Error.Format(messageFormat, messageArgs), parameterName);
		}
	}
}
