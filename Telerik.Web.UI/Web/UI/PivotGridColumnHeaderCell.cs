using System;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000C24 RID: 3108
	public class PivotGridColumnHeaderCell : PivotGridHeaderCell
	{
		// Token: 0x17002669 RID: 9833
		// (get) Token: 0x0600761E RID: 30238 RVA: 0x001B6E08 File Offset: 0x001B5008
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Th;
			}
		}

		// Token: 0x0600761F RID: 30239 RVA: 0x001B6E0C File Offset: 0x001B500C
		public PivotGridColumnHeaderCell(RadPivotGrid ownerPivotGrid) : base(ownerPivotGrid)
		{
		}

		// Token: 0x1700266A RID: 9834
		// (get) Token: 0x06007620 RID: 30240 RVA: 0x001B6E18 File Offset: 0x001B5018
		protected override bool IsExpanded
		{
			get
			{
				return !this.CanExpand && !base.OwnerPivotGrid.CollapsedRowIndexes.Contains(base.ParentIndexes);
			}
		}

		// Token: 0x06007621 RID: 30241 RVA: 0x001B6E50 File Offset: 0x001B5050
		protected override void SetExpandedState(bool shouldExpand)
		{
			if (!this.CanExpand)
			{
				return;
			}
			if (shouldExpand)
			{
				if (!this.IsExpanded)
				{
					base.OwnerPivotGrid.CollapsedRowIndexes.Add(base.ParentIndexes);
					base.OwnerPivotGrid.SetRequiresDataBindingIfInitialized();
					return;
				}
			}
			else if (this.IsExpanded)
			{
				base.OwnerPivotGrid.CollapsedRowIndexes.Remove(base.ParentIndexes);
				base.OwnerPivotGrid.SetRequiresDataBindingIfInitialized();
			}
		}

		// Token: 0x06007622 RID: 30242 RVA: 0x001B6EC0 File Offset: 0x001B50C0
		public override string GetToolTipString()
		{
			if (base.ParentIndexes == null || base.ParentIndexes.Length == 0 || base.Field == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0} ({1});", base.ParentIndexes[base.ParentIndexes.Length - 1], base.Field.DataField);
			stringBuilder.Append(base.OwnerPivotGrid.Localization.ToolTipsColumnText);
			for (int i = 0; i < base.ParentIndexes.Length; i++)
			{
				object obj = base.ParentIndexes[i];
				stringBuilder.Append(obj.ToString());
				if (i < base.ParentIndexes.Length - 1)
				{
					stringBuilder.Append(" - ");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06007623 RID: 30243 RVA: 0x001B6F7A File Offset: 0x001B517A
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.AddScopeAttribute(writer);
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06007624 RID: 30244 RVA: 0x001B6F8A File Offset: 0x001B518A
		private void AddScopeAttribute(HtmlTextWriter writer)
		{
			writer.AddAttribute("scope", "col");
		}
	}
}
