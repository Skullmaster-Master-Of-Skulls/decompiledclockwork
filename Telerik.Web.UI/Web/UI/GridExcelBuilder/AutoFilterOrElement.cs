using System;
using System.Text;
using Telerik.Web.UI.GridExcelBuilder.Abstract;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B0D RID: 6925
	public class AutoFilterOrElement : ElementBase
	{
		// Token: 0x17005182 RID: 20866
		// (get) Token: 0x06010BF7 RID: 68599 RVA: 0x003B8F02 File Offset: 0x003B7102
		public AutoFilterConditionElement SecondFilterCondition
		{
			get
			{
				if (this._secondFilterCondition == null)
				{
					this._secondFilterCondition = new AutoFilterConditionElement();
				}
				return this._secondFilterCondition;
			}
		}

		// Token: 0x17005183 RID: 20867
		// (get) Token: 0x06010BF8 RID: 68600 RVA: 0x003B8F1D File Offset: 0x003B711D
		public virtual AutoFilterConditionElement FirstFilterCondition
		{
			get
			{
				if (this._firstFilterCondition == null)
				{
					this._firstFilterCondition = new AutoFilterConditionElement();
				}
				return this._firstFilterCondition;
			}
		}

		// Token: 0x06010BF9 RID: 68601 RVA: 0x003B8F38 File Offset: 0x003B7138
		protected override void RenderChildElements(StringBuilder sb)
		{
			if (this.FirstFilterCondition.IsEmpty || this.SecondFilterCondition.IsEmpty)
			{
				throw new Exception("You have to specify FirstFilterCondition and SecondFilterCondition.");
			}
			((IElement)this.FirstFilterCondition).Render(sb);
			((IElement)this.SecondFilterCondition).Render(sb);
			base.RenderChildElements(sb);
		}

		// Token: 0x17005184 RID: 20868
		// (get) Token: 0x06010BFA RID: 68602 RVA: 0x003B8F89 File Offset: 0x003B7189
		protected override string StartTag
		{
			get
			{
				return "<AutoFilterOr{0}>";
			}
		}

		// Token: 0x17005185 RID: 20869
		// (get) Token: 0x06010BFB RID: 68603 RVA: 0x003B8F90 File Offset: 0x003B7190
		protected override string EndTag
		{
			get
			{
				return "</AutoFilterOr>";
			}
		}

		// Token: 0x04004AC2 RID: 19138
		private AutoFilterConditionElement _firstFilterCondition;

		// Token: 0x04004AC3 RID: 19139
		private AutoFilterConditionElement _secondFilterCondition;
	}
}
