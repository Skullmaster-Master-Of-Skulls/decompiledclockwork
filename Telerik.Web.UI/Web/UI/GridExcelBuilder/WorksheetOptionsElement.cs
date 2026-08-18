using System;
using System.Text;
using Telerik.Web.UI.GridExcelBuilder.Abstract;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02000F7F RID: 3967
	public class WorksheetOptionsElement : ElementBase
	{
		// Token: 0x17003006 RID: 12294
		// (get) Token: 0x060097EC RID: 38892 RVA: 0x00220788 File Offset: 0x0021E988
		// (set) Token: 0x060097ED RID: 38893 RVA: 0x00220790 File Offset: 0x0021E990
		public bool AllowFreezePanes { get; set; }

		// Token: 0x17003007 RID: 12295
		// (get) Token: 0x060097EE RID: 38894 RVA: 0x00220799 File Offset: 0x0021E999
		// (set) Token: 0x060097EF RID: 38895 RVA: 0x002207A1 File Offset: 0x0021E9A1
		public bool FitToPage { get; set; }

		// Token: 0x17003008 RID: 12296
		// (get) Token: 0x060097F0 RID: 38896 RVA: 0x002207AC File Offset: 0x0021E9AC
		public PrintElement Print
		{
			get
			{
				PrintElement result;
				if ((result = this._printElement) == null)
				{
					result = (this._printElement = new PrintElement());
				}
				return result;
			}
		}

		// Token: 0x17003009 RID: 12297
		// (get) Token: 0x060097F1 RID: 38897 RVA: 0x002207D4 File Offset: 0x0021E9D4
		public PageSetupElement PageSetup
		{
			get
			{
				PageSetupElement result;
				if ((result = this._pageSetup) == null)
				{
					result = (this._pageSetup = new PageSetupElement());
				}
				return result;
			}
		}

		// Token: 0x1700300A RID: 12298
		// (get) Token: 0x060097F2 RID: 38898 RVA: 0x002207F9 File Offset: 0x0021E9F9
		// (set) Token: 0x060097F3 RID: 38899 RVA: 0x00220801 File Offset: 0x0021EA01
		public int Zoom
		{
			get
			{
				return this._zoom;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("Zoom cannot be less than 1");
				}
				this._zoom = value;
			}
		}

		// Token: 0x1700300B RID: 12299
		// (get) Token: 0x060097F4 RID: 38900 RVA: 0x00220819 File Offset: 0x0021EA19
		// (set) Token: 0x060097F5 RID: 38901 RVA: 0x00220821 File Offset: 0x0021EA21
		public int ActivePane
		{
			get
			{
				return this._activePane;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("ActivePane cannot be less than 1");
				}
				this._activePane = value;
			}
		}

		// Token: 0x1700300C RID: 12300
		// (get) Token: 0x060097F6 RID: 38902 RVA: 0x00220839 File Offset: 0x0021EA39
		// (set) Token: 0x060097F7 RID: 38903 RVA: 0x00220841 File Offset: 0x0021EA41
		public int SplitVerticalOffest
		{
			get
			{
				return this._splitVerticalOffest;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("SplitVerticalOffest cannot be less then 0");
				}
				this._splitVerticalOffest = value;
			}
		}

		// Token: 0x1700300D RID: 12301
		// (get) Token: 0x060097F8 RID: 38904 RVA: 0x00220859 File Offset: 0x0021EA59
		// (set) Token: 0x060097F9 RID: 38905 RVA: 0x00220861 File Offset: 0x0021EA61
		public int SplitHorizontalOffset
		{
			get
			{
				return this._splitHorizontalOffset;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("SplitHorizontalOffset cannot be less then 0");
				}
				this._splitHorizontalOffset = value;
			}
		}

		// Token: 0x1700300E RID: 12302
		// (get) Token: 0x060097FA RID: 38906 RVA: 0x00220879 File Offset: 0x0021EA79
		// (set) Token: 0x060097FB RID: 38907 RVA: 0x00220881 File Offset: 0x0021EA81
		public int LeftColumnRightPaneNumber
		{
			get
			{
				return this._leftColumnRightPaneNumber;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("LeftColumnRightPaneNumber cannot be less then 0");
				}
				this._leftColumnRightPaneNumber = value;
			}
		}

		// Token: 0x1700300F RID: 12303
		// (get) Token: 0x060097FC RID: 38908 RVA: 0x00220899 File Offset: 0x0021EA99
		// (set) Token: 0x060097FD RID: 38909 RVA: 0x002208A1 File Offset: 0x0021EAA1
		public int TopRowBottomPaneNumber
		{
			get
			{
				return this._topRowBottomPaneNumber;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("TopRowBottomPaneNumber cannot be less then 0");
				}
				this._topRowBottomPaneNumber = value;
			}
		}

		// Token: 0x17003010 RID: 12304
		// (get) Token: 0x060097FE RID: 38910 RVA: 0x002208B9 File Offset: 0x0021EAB9
		protected override string EndTag
		{
			get
			{
				return "</WorksheetOptions>";
			}
		}

		// Token: 0x17003011 RID: 12305
		// (get) Token: 0x060097FF RID: 38911 RVA: 0x002208C0 File Offset: 0x0021EAC0
		protected override string StartTag
		{
			get
			{
				return "<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\"{0}>";
			}
		}

		// Token: 0x06009800 RID: 38912 RVA: 0x002208C8 File Offset: 0x0021EAC8
		protected override void RenderChildElements(StringBuilder sb)
		{
			if (this.ActivePane > 0)
			{
				sb.Append(string.Format("<x:ActivePane>{0}</x:ActivePane>", this.ActivePane));
			}
			if (this.AllowFreezePanes)
			{
				sb.Append("<x:FreezePanes />");
			}
			if (this.FitToPage)
			{
				sb.Append("<FitToPage/>");
			}
			if (this.Zoom > 0)
			{
				sb.Append(string.Format("<Zoom>{0}</Zoom>", this.Zoom));
			}
			if (this.LeftColumnRightPaneNumber > 0)
			{
				sb.Append(string.Format("<x:LeftColumnRightPane>{0}</x:LeftColumnRightPane>", this.LeftColumnRightPaneNumber));
			}
			if (this.TopRowBottomPaneNumber > 0)
			{
				sb.Append(string.Format("<x:TopRowBottomPane>{0}</x:TopRowBottomPane>", this.TopRowBottomPaneNumber));
			}
			if (this.SplitHorizontalOffset > 0)
			{
				sb.Append(string.Format("<x:SplitHorizontal>{0}</x:SplitHorizontal>", this.SplitHorizontalOffset));
			}
			if (this.SplitVerticalOffest > 0)
			{
				sb.Append(string.Format("<x:SplitVertical>{0}</x:SplitVertical>", this.SplitVerticalOffest));
			}
			if (this._printElement != null)
			{
				this.Print.Render(sb);
			}
			if (this._pageSetup != null)
			{
				this.PageSetup.Render(sb);
			}
			base.RenderChildElements(sb);
		}

		// Token: 0x04002B6A RID: 11114
		private PageSetupElement _pageSetup;

		// Token: 0x04002B6B RID: 11115
		private PrintElement _printElement;

		// Token: 0x04002B6C RID: 11116
		private int _leftColumnRightPaneNumber;

		// Token: 0x04002B6D RID: 11117
		private int _topRowBottomPaneNumber;

		// Token: 0x04002B6E RID: 11118
		private int _splitHorizontalOffset;

		// Token: 0x04002B6F RID: 11119
		private int _splitVerticalOffest;

		// Token: 0x04002B70 RID: 11120
		private int _activePane;

		// Token: 0x04002B71 RID: 11121
		private int _zoom;
	}
}
