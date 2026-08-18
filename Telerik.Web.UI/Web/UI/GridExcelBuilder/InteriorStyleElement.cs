using System;
using System.Drawing;
using System.Text;
using Telerik.Web.UI.GridExcelBuilder.Abstract;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B1C RID: 6940
	public class InteriorStyleElement : ElementBase
	{
		// Token: 0x170051CD RID: 20941
		// (get) Token: 0x06010CAF RID: 68783 RVA: 0x003BA378 File Offset: 0x003B8578
		protected override string StartTag
		{
			get
			{
				return "<Interior{0}>";
			}
		}

		// Token: 0x170051CE RID: 20942
		// (get) Token: 0x06010CB0 RID: 68784 RVA: 0x003BA37F File Offset: 0x003B857F
		protected override string EndTag
		{
			get
			{
				return "</Interior>";
			}
		}

		// Token: 0x170051CF RID: 20943
		// (get) Token: 0x06010CB1 RID: 68785 RVA: 0x003BA386 File Offset: 0x003B8586
		// (set) Token: 0x06010CB2 RID: 68786 RVA: 0x003BA38E File Offset: 0x003B858E
		public InteriorPatternType Pattern
		{
			get
			{
				return this._pattern;
			}
			set
			{
				this._pattern = value;
			}
		}

		// Token: 0x170051D0 RID: 20944
		// (get) Token: 0x06010CB3 RID: 68787 RVA: 0x003BA397 File Offset: 0x003B8597
		// (set) Token: 0x06010CB4 RID: 68788 RVA: 0x003BA39F File Offset: 0x003B859F
		public Color Color
		{
			get
			{
				return this._color;
			}
			set
			{
				this._color = value;
			}
		}

		// Token: 0x06010CB5 RID: 68789 RVA: 0x003BA3A8 File Offset: 0x003B85A8
		protected override void AppendAttributes(StringBuilder sb)
		{
			if (this.Color != Color.Empty)
			{
				base.Attributes.Add("ss:Color", Utils.ConvertColor(this.Color));
			}
			if (this.Pattern != InteriorPatternType.None)
			{
				base.Attributes.Add("ss:Pattern", this.Pattern.ToString());
			}
			base.AppendAttributes(sb);
		}

		// Token: 0x04004B07 RID: 19207
		private InteriorPatternType _pattern;

		// Token: 0x04004B08 RID: 19208
		private Color _color = Color.Empty;
	}
}
