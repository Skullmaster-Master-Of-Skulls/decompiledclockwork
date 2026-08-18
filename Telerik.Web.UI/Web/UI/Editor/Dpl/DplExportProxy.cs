using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using Telerik.Web.UI.Editor.Content;
using Telerik.Web.UI.Editor.Export;

namespace Telerik.Web.UI.Editor.Dpl
{
	// Token: 0x02000286 RID: 646
	public class DplExportProxy : DplProxy, IDplExportProxy
	{
		// Token: 0x0600170A RID: 5898 RVA: 0x0004DBE0 File Offset: 0x0004BDE0
		public virtual object ConvertHtmlToRadFlowDocument(string editorContent)
		{
			object result;
			try
			{
				result = ReflectionHelper.InvokeMethod(this.CreateHtmlFormatProvider(), "Import", new object[]
				{
					editorContent
				});
			}
			catch (Exception innerException)
			{
				throw new RadEditorExportException("The Document Processing Library fails to convert the RadEditor's content to RadFlowDocument", innerException);
			}
			return result;
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x0004DC2C File Offset: 0x0004BE2C
		public virtual string ExportToDocx(object radFlowDocument)
		{
			string result;
			try
			{
				object target = ReflectionHelper.CreateInstance(base.DocumentsFlow, "Telerik.Windows.Documents.Flow.FormatProviders.Docx.DocxFormatProvider", null);
				byte[] binaryData = (byte[])ReflectionHelper.InvokeMethod(target, "Export", new object[]
				{
					radFlowDocument
				});
				result = this.ConvertBinaryDataToString(binaryData);
			}
			catch (Exception innerException)
			{
				throw new RadEditorExportException("The Document Processing Library fails to export the RadFlowDocument in Docx", innerException);
			}
			return result;
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x0004DC94 File Offset: 0x0004BE94
		public virtual string ExportToRtf(object radFlowDocument)
		{
			string result;
			try
			{
				object target = ReflectionHelper.CreateInstance(base.DocumentsFlow, "Telerik.Windows.Documents.Flow.FormatProviders.Rtf.RtfFormatProvider", null);
				result = (string)ReflectionHelper.InvokeMethod(target, "Export", new object[]
				{
					radFlowDocument
				});
			}
			catch (Exception innerException)
			{
				throw new RadEditorExportException("The Document Processing Library fails to export the RadFlowDocument in Rtf", innerException);
			}
			return result;
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x0004DCF0 File Offset: 0x0004BEF0
		public virtual void SetPageHeader(object radFlowDocument, string pageHeader, decimal headerFontSizeInPoints)
		{
			if (string.IsNullOrEmpty(pageHeader))
			{
				return;
			}
			object target = (ReflectionHelper.GetProperty(radFlowDocument, "Sections") as IEnumerable).Cast<object>().First<object>();
			object property = ReflectionHelper.GetProperty(target, "Headers");
			object target2 = ReflectionHelper.InvokeMethod(property, "Add", null);
			object property2 = ReflectionHelper.GetProperty(target2, "Blocks");
			object target3 = ReflectionHelper.InvokeMethod(property2, "AddParagraph", null);
			object property3 = ReflectionHelper.GetProperty(target3, "Inlines");
			object target4 = ReflectionHelper.InvokeMethod(property3, "AddRun", new object[]
			{
				pageHeader
			});
			double num = this.ConvertsToPointsDips((double)headerFontSizeInPoints);
			ReflectionHelper.SetProperty(target4, "FontSize", num);
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x0004DDA4 File Offset: 0x0004BFA4
		public virtual void SetDefaultFont(object radFlowDocument, string defaultFontName, decimal defaultFontSizeInPoints)
		{
			object property = ReflectionHelper.GetProperty(radFlowDocument, "DefaultStyle");
			object property2 = ReflectionHelper.GetProperty(property, "CharacterProperties");
			object property3 = ReflectionHelper.GetProperty(property2, "FontSize");
			double num = this.ConvertsToPointsDips((double)defaultFontSizeInPoints);
			ReflectionHelper.SetProperty(property3, "LocalValue", num);
			if (string.IsNullOrEmpty(defaultFontName))
			{
				return;
			}
			object property4 = ReflectionHelper.GetProperty(property2, "FontFamily");
			Assembly assembly = ReflectionHelper.GetAssembly("Telerik.Windows.Documents.Core.dll");
			object propertyValue = ReflectionHelper.CreateInstance(assembly, "Telerik.Windows.Documents.Spreadsheet.Model.ThemableFontFamily", new object[]
			{
				defaultFontName
			});
			ReflectionHelper.SetProperty(property4, "LocalValue", propertyValue);
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x0004DE44 File Offset: 0x0004C044
		public virtual string ValidateHtmlForExport(string html)
		{
			string content = string.IsNullOrEmpty(html) ? " " : html;
			return this.ConvertRelativeImagesToAbsolute(content);
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x0004DEF4 File Offset: 0x0004C0F4
		private string ConvertRelativeImagesToAbsolute(string content)
		{
			Regex pattern = new Regex("<img\\s[^>]+>", RegexOptions.IgnoreCase);
			Regex reBase64 = new Regex("data:image", RegexOptions.IgnoreCase);
			content = HtmlTagSanitizer.Sanitize(content, pattern, delegate(Match match)
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(match.Value);
				XmlAttribute xmlAttribute = xmlDocument.FirstChild.Attributes["src"];
				string text = (xmlAttribute != null) ? xmlAttribute.Value : null;
				Uri uri;
				if (!string.IsNullOrEmpty(text) && !reBase64.IsMatch(text) && !Uri.TryCreate(text, UriKind.Absolute, out uri))
				{
					xmlAttribute.Value = HttpContext.Current.Server.MapPath(text);
				}
				return xmlDocument.InnerXml;
			});
			return content;
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x0004DF3C File Offset: 0x0004C13C
		private double ConvertsToPointsDips(double value)
		{
			Assembly assembly = ReflectionHelper.GetAssembly("Telerik.Windows.Documents.Core.dll");
			Type type = assembly.GetType("Telerik.Windows.Documents.Media.Unit");
			MethodInfo method = type.GetMethod("PointToDip", new Type[]
			{
				typeof(double)
			});
			return (double)method.Invoke(type, new object[]
			{
				value
			});
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x0004DFA4 File Offset: 0x0004C1A4
		private string ConvertBinaryDataToString(byte[] binaryData)
		{
			string result;
			using (Stream stream = new MemoryStream(binaryData))
			{
				using (StreamReader streamReader = new StreamReader(stream, Encoding.Default))
				{
					result = streamReader.ReadToEnd();
				}
			}
			return result;
		}
	}
}
