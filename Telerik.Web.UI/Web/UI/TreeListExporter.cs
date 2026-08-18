using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200121D RID: 4637
	internal abstract class TreeListExporter
	{
		// Token: 0x0600BF57 RID: 48983 RVA: 0x002A5C02 File Offset: 0x002A3E02
		protected TreeListExporter(RadTreeList treeList)
		{
			this.treeList = treeList;
		}

		// Token: 0x0600BF58 RID: 48984 RVA: 0x002A5C11 File Offset: 0x002A3E11
		protected Page GetPage(RadTreeList treeList)
		{
			if (treeList.Page == null)
			{
				throw new TreeListExportException("RadTreeList must be databound before exporting.");
			}
			return treeList.Page;
		}

		// Token: 0x0600BF59 RID: 48985
		protected abstract void PrepareForExport();

		// Token: 0x0600BF5A RID: 48986 RVA: 0x002A5C2C File Offset: 0x002A3E2C
		private void ReplaceControlWithLiteral(Control control, string text)
		{
			Control parent = control.Parent;
			int index = parent.Controls.IndexOf(control);
			LiteralControl child = new LiteralControl(text);
			parent.Controls.Remove(control);
			parent.Controls.AddAt(index, child);
		}

		// Token: 0x0600BF5B RID: 48987 RVA: 0x002A5C70 File Offset: 0x002A3E70
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		protected void ClearControlsRecursively(Control ctrl)
		{
			IEnumerable<Control> collection = ctrl.Controls.OfType<Control>();
			Stack<Control> stack = new Stack<Control>(collection);
			while (stack.Count > 0)
			{
				Control control = stack.Pop();
				if (this.treeList.ExportSettings.ExportMode == TreeListExportMode.RemoveAll || this.treeList.ExportSettings.ExportMode == TreeListExportMode.RemoveControls)
				{
					if (control is IButtonControl)
					{
						Button button = control as Button;
						if (button != null && !Regex.IsMatch(button.CssClass, "rtlExpand|rtlCollapse"))
						{
							ctrl.Controls.Remove(control);
						}
						if (control is ImageButton && this.treeList.ExportSettings.ExportMode == TreeListExportMode.RemoveAll)
						{
							ctrl.Controls.Remove(control);
						}
					}
					else if (control is ICheckBoxControl || control is ITextControl || control is IScriptControl || control is HyperLink)
					{
						ctrl.Controls.Remove(control);
					}
					else if (this.treeList.ExportSettings.ExportMode == TreeListExportMode.RemoveAll && control is Image)
					{
						ctrl.Controls.Remove(control);
					}
				}
				else if (control is RadMonthYearPicker || control is RadDatePicker)
				{
					control.Visible = false;
				}
				else if (control is ITextControl)
				{
					this.ReplaceControlWithLiteral(control, (control as ITextControl).Text);
				}
				else if (control is IButtonControl)
				{
					string pattern = "rtlExpand|rtlCollapse";
					if (((control is Button && !Regex.IsMatch((control as Button).CssClass, pattern)) || !(control is Button)) && !(control is ImageButton))
					{
						this.ReplaceControlWithLiteral(control, (control as IButtonControl).Text);
					}
				}
				else if (control is ICheckBoxControl)
				{
					this.ReplaceControlWithLiteral(control, (control as ICheckBoxControl).Checked.ToString());
				}
				else if (control is HyperLink)
				{
					this.ReplaceControlWithLiteral(control, (control as HyperLink).Text);
				}
				else if (!(ctrl is TreeListTable) && !(ctrl is TableCell) && !(ctrl is TableRow) && !(ctrl is RadTreeList))
				{
					ctrl.Controls.Remove(control);
				}
				if (control.HasControls())
				{
					this.ClearControlsRecursively(control);
				}
			}
		}

		// Token: 0x0600BF5C RID: 48988 RVA: 0x002A5EA2 File Offset: 0x002A40A2
		protected virtual void ExportRenderPage(HtmlTextWriter writer, Control pageCtrl)
		{
			this.page.Form.SetRenderMethodDelegate(new RenderMethod(this.ExportRenderForm));
			this.page.Form.RenderControl(new HtmlTextWriter(TextWriter.Null));
		}

		// Token: 0x0600BF5D RID: 48989
		protected abstract void ExportRenderForm(HtmlTextWriter writer, Control page);

		// Token: 0x0600BF5E RID: 48990 RVA: 0x002A5EDC File Offset: 0x002A40DC
		internal static string GetEmbeddedResource(string resource)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string result;
			using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(resource))
			{
				using (TextReader textReader = new StreamReader(manifestResourceStream))
				{
					result = textReader.ReadToEnd();
				}
			}
			return result;
		}

		// Token: 0x0600BF5F RID: 48991
		protected abstract void ConfigureResponse(ExportFormat exportFormat, HttpResponse response);

		// Token: 0x0600BF60 RID: 48992 RVA: 0x002A5F38 File Offset: 0x002A4138
		protected static string GetTemporaryDir()
		{
			string text = Path.GetTempPath();
			if (string.IsNullOrEmpty(text))
			{
				foreach (string text2 in TreeListExporter.tempDirEnvVars)
				{
					text = Environment.GetEnvironmentVariable(text2);
					if (!string.IsNullOrEmpty(text2))
					{
						break;
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					text = "/tmp";
				}
			}
			return text;
		}

		// Token: 0x0400323B RID: 12859
		protected RadTreeList treeList;

		// Token: 0x0400323C RID: 12860
		protected Page page;

		// Token: 0x0400323D RID: 12861
		private static string[] tempDirEnvVars = new string[]
		{
			"Temp",
			"TMP",
			"TEMP"
		};
	}
}
