using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Web.UI.WebControls;
using Telerik.Web.UI.GridExcelBuilder.Abstract;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B15 RID: 6933
	public class ColumnElement : ElementBase
	{
		// Token: 0x170051B0 RID: 20912
		// (get) Token: 0x06010C65 RID: 68709 RVA: 0x003B97C6 File Offset: 0x003B79C6
		// (set) Token: 0x06010C66 RID: 68710 RVA: 0x003B97CE File Offset: 0x003B79CE
		public bool Hidden { get; set; }

		// Token: 0x170051B1 RID: 20913
		// (get) Token: 0x06010C67 RID: 68711 RVA: 0x003B97D7 File Offset: 0x003B79D7
		// (set) Token: 0x06010C68 RID: 68712 RVA: 0x003B97DF File Offset: 0x003B79DF
		public Unit Width
		{
			get
			{
				return this._width;
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("Value", "Column width cannot be less then 0");
				}
				this._width = value;
			}
		}

		// Token: 0x170051B2 RID: 20914
		// (get) Token: 0x06010C69 RID: 68713 RVA: 0x003B980A File Offset: 0x003B7A0A
		protected override string StartTag
		{
			get
			{
				return "<Column{0}>";
			}
		}

		// Token: 0x170051B3 RID: 20915
		// (get) Token: 0x06010C6A RID: 68714 RVA: 0x003B9811 File Offset: 0x003B7A11
		protected override string EndTag
		{
			get
			{
				return "</Column>";
			}
		}

		// Token: 0x06010C6B RID: 68715 RVA: 0x003B9818 File Offset: 0x003B7A18
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Double.ToString")]
		protected override void AppendAttributes(StringBuilder sb)
		{
			if (this.Width != Unit.Empty)
			{
				base.Attributes.Add("ss:Width", Utils.ConvertUnitsToPoints(this.Width).ToString());
			}
			if (this.Hidden)
			{
				base.Attributes.Add("ss:Hidden", "1");
			}
			base.AppendAttributes(sb);
		}

		// Token: 0x04004AE0 RID: 19168
		private Unit _width = Unit.Empty;
	}
}
