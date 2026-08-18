using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.Web.Entity.Web;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Web
{
	// Token: 0x02000007 RID: 7
	public interface INavigatorClientManager
	{
		// Token: 0x0600000F RID: 15
		string ConvertIntParameterToUrlString(int parameter);

		// Token: 0x06000010 RID: 16
		int ConvertUrlStringToIntParameter(string urlParameter);

		// Token: 0x06000011 RID: 17
		string GetStringFromUrlParameter(string urlParameterName);

		// Token: 0x06000012 RID: 18
		string GetUrlParameterFromString(string s);

		// Token: 0x06000013 RID: 19
		string GetStudentUrlWithIntParameter(string url, string pname, int pvalue);

		// Token: 0x06000014 RID: 20
		string GetStudentUrlWithParameters(string url, Dictionary<string, int> args);

		// Token: 0x06000015 RID: 21
		void SetReturnUrl();

		// Token: 0x06000016 RID: 22
		void GotoLastReturnUrl(string folderEnforce, string defaultPage);

		// Token: 0x06000017 RID: 23
		string GetLastReturnUrl(string folderEnforce, string defaultPage);

		// Token: 0x06000018 RID: 24
		string GetLastReturnUrl(string defaultUrl);

		// Token: 0x06000019 RID: 25
		void SetReturnUrlSpecific(string relativeUrl);

		// Token: 0x0600001A RID: 26
		void NotAllowed(Setting setting, object currentPageObj);

		// Token: 0x0600001B RID: 27
		void NotAllowed(eNotAllowedCode notAllowedCode, IDictionary<string, string> args, object currentPageObj);

		// Token: 0x0600001C RID: 28
		void GotoModuleNotLicensedWarningPage(Group Group);

		// Token: 0x0600001D RID: 29
		void EnsurePageNotCached();

		// Token: 0x0600001E RID: 30
		void GotoHomePage();
	}
}
