using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Microsoft.Internal.Web.Utils;

namespace System.Web.WebPages
{
	// Token: 0x0200005A RID: 90
	public abstract class RequestFieldValidatorBase : IValidator
	{
		// Token: 0x06000223 RID: 547 RVA: 0x00008C91 File Offset: 0x00006E91
		protected RequestFieldValidatorBase(string errorMessage) : this(errorMessage, false)
		{
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00008C9B File Offset: 0x00006E9B
		protected RequestFieldValidatorBase(string errorMessage, bool useUnvalidatedValues)
		{
			if (string.IsNullOrEmpty(errorMessage))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "errorMessage");
			}
			this._errorMessage = errorMessage;
			this._useUnvalidatedValues = useUnvalidatedValues;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00008CC9 File Offset: 0x00006EC9
		public virtual ModelClientValidationRule ClientValidationRule
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00008CCC File Offset: 0x00006ECC
		// (set) Token: 0x06000227 RID: 551 RVA: 0x00008CD3 File Offset: 0x00006ED3
		internal static bool IgnoreUseUnvalidatedValues { get; set; }

		// Token: 0x06000228 RID: 552
		protected abstract bool IsValid(HttpContextBase httpContext, string value);

		// Token: 0x06000229 RID: 553 RVA: 0x00008CDC File Offset: 0x00006EDC
		public virtual ValidationResult Validate(ValidationContext validationContext)
		{
			HttpContextBase httpContext = RequestFieldValidatorBase.GetHttpContext(validationContext);
			string memberName = validationContext.MemberName;
			string requestValue = this.GetRequestValue(httpContext.Request, memberName);
			if (this.IsValid(httpContext, requestValue))
			{
				return ValidationResult.Success;
			}
			return new ValidationResult(this._errorMessage, new string[]
			{
				memberName
			});
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00008D2C File Offset: 0x00006F2C
		protected static HttpContextBase GetHttpContext(ValidationContext validationContext)
		{
			return (HttpContextBase)validationContext.ObjectInstance;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00008D39 File Offset: 0x00006F39
		protected string GetRequestValue(HttpRequestBase request, string field)
		{
			if (RequestFieldValidatorBase.IgnoreUseUnvalidatedValues)
			{
				return request.Form[field];
			}
			if (!this._useUnvalidatedValues)
			{
				return request.Form[field];
			}
			return request.Unvalidated[field];
		}

		// Token: 0x040000B3 RID: 179
		private readonly string _errorMessage;

		// Token: 0x040000B4 RID: 180
		private readonly bool _useUnvalidatedValues;
	}
}
