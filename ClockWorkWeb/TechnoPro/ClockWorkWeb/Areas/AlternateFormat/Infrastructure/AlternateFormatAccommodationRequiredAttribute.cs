using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkWeb.Infrastructure;
using TechnoPro.Common.ClientManager.Core.DynamicForms;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Cache;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Infrastructure
{
	// Token: 0x02000181 RID: 385
	public class AlternateFormatAccommodationRequiredAttribute : AuthorizeAttribute
	{
		// Token: 0x06000B73 RID: 2931 RVA: 0x00049710 File Offset: 0x00047910
		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			int logonStudentPersonId = LogonPerson.Instance.GetLogonStudentPersonId(httpContext.Session);
			bool flag = logonStudentPersonId == 0;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				object obj = SessionCaching.CurrentInstance["altFormatAuthCoreRes"];
				bool flag2 = obj != null && obj is bool && (bool)obj;
				if (flag2)
				{
					result = true;
				}
				else
				{
					IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
					string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.ALTERNATEFORMAT_Accommodation_Template_Control_Id);
					int[] array;
					if (!string.IsNullOrWhiteSpace(settingValue))
					{
						array = (from g in settingValue.Split(new char[]
						{
							','
						}).Select(delegate(string g)
						{
							string text = (g ?? "").Trim();
							int num;
							return (string.IsNullOrWhiteSpace(text) || !int.TryParse(text, out num)) ? 0 : num;
						}).Distinct<int>()
						where g > 0
						select g).ToArray<int>();
					}
					else
					{
						array = new int[0];
					}
					int[] array2 = array;
					bool flag3 = array2.Length < 1;
					if (flag3)
					{
						result = true;
					}
					else
					{
						IDynamicDataClientManager dynamicDataClientManager = new DynamicDataClientManager();
						bool flag4 = dynamicDataClientManager.DoesAtLeastOneSavedDataItemExistByControlIds(new DynamicDataContextDTO
						{
							PrimaryId = logonStudentPersonId
						}, eDynamicFormTypeDTO.AccommodationTemplateOnly, array2);
						SessionCaching.CurrentInstance.Insert("altFormatAuthCoreRes", flag4, TimeSpan.FromHours(8.0));
						result = flag4;
					}
				}
			}
			return result;
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0004985C File Offset: 0x00047A5C
		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			bool flag = !filterContext.HttpContext.User.Identity.IsAuthenticated;
			if (flag)
			{
				base.HandleUnauthorizedRequest(filterContext);
			}
			else
			{
				filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
				{
					controller = "MessageHandler",
					action = "StudentWithoutAlternateFormatAccommodations"
				}));
			}
		}
	}
}
