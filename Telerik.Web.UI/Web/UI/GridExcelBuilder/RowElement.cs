using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Web.UI.WebControls;
using Telerik.Web.UI.GridExcelBuilder.Abstract;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B27 RID: 6951
	public class RowElement : ElementBase
	{
		// Token: 0x170051DA RID: 20954
		// (get) Token: 0x06010CEA RID: 68842 RVA: 0x003BB799 File Offset: 0x003B9999
		// (set) Token: 0x06010CEB RID: 68843 RVA: 0x003BB7A1 File Offset: 0x003B99A1
		public Unit Height
		{
			get
			{
				return this._height;
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("Value", "Row height cannot be less then 0");
				}
				this._height = value;
			}
		}

		// Token: 0x170051DB RID: 20955
		// (get) Token: 0x06010CEC RID: 68844 RVA: 0x003BB7CC File Offset: 0x003B99CC
		protected override string StartTag
		{
			get
			{
				return "<Row{0}>";
			}
		}

		// Token: 0x170051DC RID: 20956
		// (get) Token: 0x06010CED RID: 68845 RVA: 0x003BB7D3 File Offset: 0x003B99D3
		protected override string EndTag
		{
			get
			{
				return "</Row>";
			}
		}

		// Token: 0x170051DD RID: 20957
		// (get) Token: 0x06010CEE RID: 68846 RVA: 0x003BB7DA File Offset: 0x003B99DA
		public virtual CellsCollection Cells
		{
			get
			{
				if (this._cells == null)
				{
					this._cells = new CellsCollection();
				}
				return this._cells;
			}
		}

		// Token: 0x06010CEF RID: 68847 RVA: 0x003BB7F8 File Offset: 0x003B99F8
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Double.ToString")]
		protected override void AppendAttributes(StringBuilder sb)
		{
			if (this.Height != Unit.Empty)
			{
				base.Attributes.Add("ss:Height", Utils.ConvertUnitsToPoints(this.Height).ToString());
			}
			base.AppendAttributes(sb);
		}

		// Token: 0x170051DE RID: 20958
		// (get) Token: 0x06010CF0 RID: 68848 RVA: 0x003BB841 File Offset: 0x003B9A41
		public override IElementsCollection InnerElements
		{
			get
			{
				return new ElementsCollection();
			}
		}

		// Token: 0x06010CF1 RID: 68849 RVA: 0x003BB848 File Offset: 0x003B9A48
		protected override void RenderChildElements(StringBuilder sb)
		{
			foreach (object obj in this.Cells)
			{
				CellElement cellElement = (CellElement)obj;
				cellElement.Render(sb);
			}
		}

		// Token: 0x04004B37 RID: 19255
		private CellsCollection _cells;

		// Token: 0x04004B38 RID: 19256
		private Unit _height = Unit.Empty;
	}
}
