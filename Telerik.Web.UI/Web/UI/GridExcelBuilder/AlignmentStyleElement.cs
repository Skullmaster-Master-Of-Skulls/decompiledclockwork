using System;
using System.Text;
using Telerik.Web.UI.GridExcelBuilder.Abstract;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B06 RID: 6918
	public class AlignmentStyleElement : ElementBase
	{
		// Token: 0x1700515C RID: 20828
		// (get) Token: 0x06010BAE RID: 68526 RVA: 0x003B8928 File Offset: 0x003B6B28
		// (set) Token: 0x06010BAF RID: 68527 RVA: 0x003B8930 File Offset: 0x003B6B30
		public VerticalAlignmentType VerticalAlignment
		{
			get
			{
				return this._verticalAlignment;
			}
			set
			{
				this._verticalAlignment = value;
			}
		}

		// Token: 0x1700515D RID: 20829
		// (get) Token: 0x06010BB0 RID: 68528 RVA: 0x003B8939 File Offset: 0x003B6B39
		// (set) Token: 0x06010BB1 RID: 68529 RVA: 0x003B8941 File Offset: 0x003B6B41
		public HorizontalAlignmentType HorizontalAlignment
		{
			get
			{
				return this._horizontalAlignment;
			}
			set
			{
				this._horizontalAlignment = value;
			}
		}

		// Token: 0x1700515E RID: 20830
		// (get) Token: 0x06010BB2 RID: 68530 RVA: 0x003B894A File Offset: 0x003B6B4A
		protected override string StartTag
		{
			get
			{
				return "<Alignment{0}>";
			}
		}

		// Token: 0x1700515F RID: 20831
		// (get) Token: 0x06010BB3 RID: 68531 RVA: 0x003B8951 File Offset: 0x003B6B51
		protected override string EndTag
		{
			get
			{
				return "</Alignment>";
			}
		}

		// Token: 0x06010BB4 RID: 68532 RVA: 0x003B8958 File Offset: 0x003B6B58
		protected override void AppendAttributes(StringBuilder sb)
		{
			if (this.HorizontalAlignment != HorizontalAlignmentType.Automatic)
			{
				base.Attributes.Add("ss:Horizontal", this.HorizontalAlignment.ToString());
			}
			if (this.VerticalAlignment != VerticalAlignmentType.Automatic)
			{
				base.Attributes.Add("ss:Vertical", this.VerticalAlignment.ToString());
			}
			base.AppendAttributes(sb);
		}

		// Token: 0x04004AA5 RID: 19109
		private HorizontalAlignmentType _horizontalAlignment;

		// Token: 0x04004AA6 RID: 19110
		private VerticalAlignmentType _verticalAlignment;
	}
}
