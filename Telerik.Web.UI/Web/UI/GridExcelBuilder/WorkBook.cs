using System;
using System.Text;
using Telerik.Web.UI.GridExcelBuilder.Abstract;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B2E RID: 6958
	public class WorkBook : ElementBase
	{
		// Token: 0x17005208 RID: 21000
		// (get) Token: 0x06010D55 RID: 68949 RVA: 0x003BC1FE File Offset: 0x003BA3FE
		public IStylesCollection Styles
		{
			get
			{
				return this._styles.Styles;
			}
		}

		// Token: 0x17005209 RID: 21001
		// (get) Token: 0x06010D56 RID: 68950 RVA: 0x003BC20B File Offset: 0x003BA40B
		protected override string StartTag
		{
			get
			{
				return "<Workbook{0}>";
			}
		}

		// Token: 0x1700520A RID: 21002
		// (get) Token: 0x06010D57 RID: 68951 RVA: 0x003BC212 File Offset: 0x003BA412
		protected override string EndTag
		{
			get
			{
				return "</Workbook>";
			}
		}

		// Token: 0x1700520B RID: 21003
		// (get) Token: 0x06010D58 RID: 68952 RVA: 0x003BC219 File Offset: 0x003BA419
		public IWorksheetCollection Worksheets
		{
			get
			{
				if (this._worksheets == null)
				{
					this._worksheets = new WorksheetCollection();
				}
				return this._worksheets;
			}
		}

		// Token: 0x06010D59 RID: 68953 RVA: 0x003BC234 File Offset: 0x003BA434
		public override void Render(StringBuilder sb)
		{
			this.RenderDocHeaders(sb);
			base.Render(sb);
		}

		// Token: 0x06010D5A RID: 68954 RVA: 0x003BC244 File Offset: 0x003BA444
		protected override void RenderChildElements(StringBuilder sb)
		{
			if (this.Worksheets.Count == 0)
			{
				throw new Exception("Worksheet collection cannot be empty.");
			}
			if (this.Styles.Count > 0)
			{
				((IElement)this._styles).Render(sb);
			}
			foreach (object obj in this.Worksheets)
			{
				WorksheetElement worksheetElement = (WorksheetElement)obj;
				worksheetElement.Render(sb);
			}
			base.RenderChildElements(sb);
		}

		// Token: 0x06010D5B RID: 68955 RVA: 0x003BC2D8 File Offset: 0x003BA4D8
		protected override void AppendAttributes(StringBuilder sb)
		{
			base.Attributes.Add("xmlns", "urn:schemas-microsoft-com:office:spreadsheet");
			base.Attributes.Add("xmlns:o", "urn:schemas-microsoft-com:office:office");
			base.Attributes.Add("xmlns:x", "urn:schemas-microsoft-com:office:excel");
			base.Attributes.Add("xmlns:ss", "urn:schemas-microsoft-com:office:spreadsheet");
			base.AppendAttributes(sb);
		}

		// Token: 0x06010D5C RID: 68956 RVA: 0x003BC340 File Offset: 0x003BA540
		protected virtual void RenderDocHeaders(StringBuilder sb)
		{
			sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
			sb.Append("<?mso-application progid=\"Excel.Sheet\"?>");
		}

		// Token: 0x04004B45 RID: 19269
		private IWorksheetCollection _worksheets;

		// Token: 0x04004B46 RID: 19270
		private StylesElement _styles = new StylesElement();
	}
}
