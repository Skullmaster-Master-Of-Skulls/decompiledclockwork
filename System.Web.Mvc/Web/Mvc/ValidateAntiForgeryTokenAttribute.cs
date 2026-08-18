using System;
using System.ComponentModel;
using System.Web.Helpers;

namespace System.Web.Mvc
{
	// Token: 0x0200016E RID: 366
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class ValidateAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
	{
		// Token: 0x06000993 RID: 2451 RVA: 0x0001AACC File Offset: 0x00018CCC
		public ValidateAntiForgeryTokenAttribute() : this(new Action(AntiForgery.Validate))
		{
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0001AAE0 File Offset: 0x00018CE0
		internal ValidateAntiForgeryTokenAttribute(Action validateAction)
		{
			this.ValidateAction = validateAction;
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x0001AAEF File Offset: 0x00018CEF
		// (set) Token: 0x06000996 RID: 2454 RVA: 0x0001AAF7 File Offset: 0x00018CF7
		[Obsolete("The 'Salt' property is deprecated. To specify custom data to be embedded within the token, use the static AntiForgeryConfig.AdditionalDataProvider property.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string Salt
		{
			get
			{
				return this._salt;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					throw new NotSupportedException("The 'Salt' property is deprecated. To specify custom data to be embedded within the token, use the static AntiForgeryConfig.AdditionalDataProvider property.");
				}
				this._salt = value;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x0001AB13 File Offset: 0x00018D13
		// (set) Token: 0x06000998 RID: 2456 RVA: 0x0001AB1B File Offset: 0x00018D1B
		internal Action ValidateAction { get; private set; }

		// Token: 0x06000999 RID: 2457 RVA: 0x0001AB24 File Offset: 0x00018D24
		public void OnAuthorization(AuthorizationContext filterContext)
		{
			if (filterContext == null)
			{
				throw new ArgumentNullException("filterContext");
			}
			this.ValidateAction();
		}

		// Token: 0x04000294 RID: 660
		private string _salt;
	}
}
