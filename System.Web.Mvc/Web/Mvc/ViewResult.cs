using System;
using System.Globalization;
using System.Text;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x020001F4 RID: 500
	public class ViewResult : ViewResultBase
	{
		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x00027F7B File Offset: 0x0002617B
		// (set) Token: 0x06000F3F RID: 3903 RVA: 0x00027F8C File Offset: 0x0002618C
		public string MasterName
		{
			get
			{
				return this._masterName ?? string.Empty;
			}
			set
			{
				this._masterName = value;
			}
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x00027F98 File Offset: 0x00026198
		protected override ViewEngineResult FindView(ControllerContext context)
		{
			ViewEngineResult viewEngineResult = base.ViewEngineCollection.FindView(context, base.ViewName, this.MasterName);
			if (viewEngineResult.View != null)
			{
				return viewEngineResult;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string value in viewEngineResult.SearchedLocations)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append(value);
			}
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.Common_ViewNotFound, new object[]
			{
				base.ViewName,
				stringBuilder
			}));
		}

		// Token: 0x040003F9 RID: 1017
		private string _masterName;
	}
}
