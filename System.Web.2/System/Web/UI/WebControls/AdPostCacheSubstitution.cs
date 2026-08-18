using System;
using System.Globalization;
using System.IO;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000373 RID: 883
	internal class AdPostCacheSubstitution
	{
		// Token: 0x060028A4 RID: 10404 RVA: 0x000030B5 File Offset: 0x000012B5
		private AdPostCacheSubstitution()
		{
		}

		// Token: 0x060028A5 RID: 10405 RVA: 0x000832D8 File Offset: 0x000814D8
		internal AdPostCacheSubstitution(AdRotator adRotator)
		{
			this._adRotatorHelper = new AdRotator();
			this._adRotatorHelper.CopyFrom(adRotator);
			this._adRotatorHelper.IsPostCacheAdHelper = true;
			this._adRotatorHelper.Page = new Page();
		}

		// Token: 0x060028A6 RID: 10406 RVA: 0x00083314 File Offset: 0x00081514
		internal void RegisterPostCacheCallBack(HttpContext context, Page page, HtmlTextWriter writer)
		{
			HttpResponseSubstitutionCallback callback = new HttpResponseSubstitutionCallback(this.Render);
			context.Response.WriteSubstitution(callback);
		}

		// Token: 0x060028A7 RID: 10407 RVA: 0x0008333C File Offset: 0x0008153C
		internal string Render(HttpContext context)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.CurrentCulture);
			HtmlTextWriter writer = this._adRotatorHelper.Page.CreateHtmlTextWriter(stringWriter);
			this._adRotatorHelper.RenderControl(writer);
			return stringWriter.ToString();
		}

		// Token: 0x04001E0F RID: 7695
		private AdRotator _adRotatorHelper;
	}
}
