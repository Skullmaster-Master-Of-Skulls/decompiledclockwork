using System;
using System.IO;
using System.Web;
using Telerik.Web.UI.Editor.Import;

namespace Telerik.Web.UI.Editor.Dpl
{
	// Token: 0x02000288 RID: 648
	public class DplImportProxy : DplProxy, IDplImportProxy
	{
		// Token: 0x06001719 RID: 5913 RVA: 0x0004E008 File Offset: 0x0004C208
		public virtual string ConvertRadFlowDocumentToHtml(object radFlowDocument, object htmlFormatProvider)
		{
			string result;
			try
			{
				result = (string)ReflectionHelper.InvokeMethod(htmlFormatProvider, "Export", new object[]
				{
					radFlowDocument
				});
			}
			catch (Exception innerException)
			{
				throw new RadEditorImportException("The Document Processing Library fails to convert the RadFlowDocument to HTML", innerException);
			}
			return result;
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x0004E054 File Offset: 0x0004C254
		public virtual object ConvertStreamToRadFlowDocument(Stream stream, string formatProviderType)
		{
			object result;
			try
			{
				object target = ReflectionHelper.CreateInstance(base.DocumentsFlow, formatProviderType, null);
				result = ReflectionHelper.InvokeMethod(target, "Import", new object[]
				{
					stream
				});
			}
			catch (Exception innerException)
			{
				throw new RadEditorImportException("The Document Processing Library fails to convert the input file stream to RadFlowDocument", innerException);
			}
			return result;
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x0004E0A8 File Offset: 0x0004C2A8
		public virtual void ApplyImportSettings(object htmlFormatProvider, IDplImportSettings settings)
		{
			object obj = ReflectionHelper.CreateInstance(base.DocumentsFlow, "Telerik.Windows.Documents.Flow.FormatProviders.Html.HtmlExportSettings", null);
			ReflectionHelper.SetProperty(obj, "DocumentExportLevel", settings.DocumentLevel);
			ReflectionHelper.SetProperty(obj, "StylesExportMode", settings.StylesMode);
			ReflectionHelper.SetProperty(obj, "StylesFilePath", HttpContext.Current.Server.MapPath(settings.StylesFilePath));
			ReflectionHelper.SetProperty(obj, "StylesSourcePath", settings.StylesSourcePath);
			ReflectionHelper.SetProperty(obj, "ImagesExportMode", settings.ImagesMode);
			ReflectionHelper.SetProperty(obj, "ImagesFolderPath", HttpContext.Current.Server.MapPath(settings.ImagesFolderPath));
			ReflectionHelper.SetProperty(obj, "ImagesSourceBasePath", settings.ImagesSourceBasePath);
			ReflectionHelper.SetProperty(htmlFormatProvider, "ExportSettings", obj);
		}
	}
}
