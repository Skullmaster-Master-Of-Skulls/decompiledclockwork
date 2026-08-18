using System;
using System.Text;
using Telerik.Web.UI.GridExcelBuilder.Abstract;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B0C RID: 6924
	public class AutoFilterAndElement : ElementBase
	{
		// Token: 0x1700517F RID: 20863
		// (get) Token: 0x06010BF2 RID: 68594 RVA: 0x003B8EA4 File Offset: 0x003B70A4
		public virtual AutoFilterConditionElement FilterCondition
		{
			get
			{
				if (this._filterCondition == null)
				{
					this._filterCondition = new AutoFilterConditionElement();
				}
				return this._filterCondition;
			}
		}

		// Token: 0x06010BF3 RID: 68595 RVA: 0x003B8EBF File Offset: 0x003B70BF
		protected override void RenderChildElements(StringBuilder sb)
		{
			if (this.FilterCondition.IsEmpty)
			{
				throw new Exception("You have to specify FilterCondition.");
			}
			((IElement)this.FilterCondition).Render(sb);
			base.RenderChildElements(sb);
		}

		// Token: 0x17005180 RID: 20864
		// (get) Token: 0x06010BF4 RID: 68596 RVA: 0x003B8EEC File Offset: 0x003B70EC
		protected override string StartTag
		{
			get
			{
				return "<AutoFilterAnd{0}>";
			}
		}

		// Token: 0x17005181 RID: 20865
		// (get) Token: 0x06010BF5 RID: 68597 RVA: 0x003B8EF3 File Offset: 0x003B70F3
		protected override string EndTag
		{
			get
			{
				return "</AutoFilterAnd>";
			}
		}

		// Token: 0x04004AC1 RID: 19137
		private AutoFilterConditionElement _filterCondition;
	}
}
