using System;
using System.Web.Configuration;

namespace System.Web.Util
{
	// Token: 0x0200021A RID: 538
	public class RequestValidator
	{
		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x060019F7 RID: 6647 RVA: 0x00051195 File Offset: 0x0004F395
		// (set) Token: 0x060019F8 RID: 6648 RVA: 0x000511B2 File Offset: 0x0004F3B2
		public static RequestValidator Current
		{
			get
			{
				if (RequestValidator._customValidator == null)
				{
					RequestValidator._customValidator = RequestValidator._customValidatorResolver.Value;
				}
				return RequestValidator._customValidator;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				RequestValidator._customValidator = value;
			}
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x000511C8 File Offset: 0x0004F3C8
		private static RequestValidator GetCustomValidatorFromConfig()
		{
			RuntimeConfig appConfig = RuntimeConfig.GetAppConfig();
			HttpRuntimeSection httpRuntime = appConfig.HttpRuntime;
			string requestValidationType = httpRuntime.RequestValidationType;
			Type type = ConfigUtil.GetType(requestValidationType, "requestValidationType", httpRuntime);
			ConfigUtil.CheckBaseType(typeof(RequestValidator), type, "requestValidationType", httpRuntime);
			return (RequestValidator)HttpRuntime.CreatePublicInstanceByWebObjectActivator(type);
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x0005121C File Offset: 0x0004F41C
		internal static void InitializeOnFirstRequest()
		{
			RequestValidator value = RequestValidator._customValidatorResolver.Value;
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x00051234 File Offset: 0x0004F434
		public bool InvokeIsValidRequestString(HttpContext context, string value, RequestValidationSource requestValidationSource, string collectionKey, out int validationFailureIndex)
		{
			return this.IsValidRequestString(context, value, requestValidationSource, collectionKey, out validationFailureIndex);
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x00007D5F File Offset: 0x00005F5F
		private static bool IsAtoZ(char c)
		{
			return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x00051243 File Offset: 0x0004F443
		protected internal virtual bool IsValidRequestString(HttpContext context, string value, RequestValidationSource requestValidationSource, string collectionKey, out int validationFailureIndex)
		{
			if (requestValidationSource == RequestValidationSource.Headers)
			{
				validationFailureIndex = 0;
				return true;
			}
			return !CrossSiteScriptingValidation.IsDangerousString(value, out validationFailureIndex);
		}

		// Token: 0x04001804 RID: 6148
		private static RequestValidator _customValidator;

		// Token: 0x04001805 RID: 6149
		private static readonly Lazy<RequestValidator> _customValidatorResolver = new Lazy<RequestValidator>(new Func<RequestValidator>(RequestValidator.GetCustomValidatorFromConfig));
	}
}
