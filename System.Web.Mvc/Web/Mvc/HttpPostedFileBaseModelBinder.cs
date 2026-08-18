using System;

namespace System.Web.Mvc
{
	// Token: 0x0200019B RID: 411
	public class HttpPostedFileBaseModelBinder : IModelBinder
	{
		// Token: 0x06000B95 RID: 2965 RVA: 0x0001E6FC File Offset: 0x0001C8FC
		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}
			if (bindingContext == null)
			{
				throw new ArgumentNullException("bindingContext");
			}
			HttpPostedFileBase rawFile = controllerContext.HttpContext.Request.Files[bindingContext.ModelName];
			return HttpPostedFileBaseModelBinder.ChooseFileOrNull(rawFile);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0001E747 File Offset: 0x0001C947
		internal static HttpPostedFileBase ChooseFileOrNull(HttpPostedFileBase rawFile)
		{
			if (rawFile == null)
			{
				return null;
			}
			if (rawFile.ContentLength == 0 && string.IsNullOrEmpty(rawFile.FileName))
			{
				return null;
			}
			return rawFile;
		}
	}
}
