using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001252 RID: 4690
	public class TreeListHeaderItem : TreeListItem
	{
		// Token: 0x0600C16C RID: 49516 RVA: 0x002B24A7 File Offset: 0x002B06A7
		public TreeListHeaderItem(RadTreeList ownerTreeList, TreeListItemType itemType, bool isDataBinding) : base(ownerTreeList, itemType, isDataBinding)
		{
		}

		// Token: 0x0600C16D RID: 49517 RVA: 0x002B24B2 File Offset: 0x002B06B2
		protected override TableCell CreateCellObject()
		{
			return new TreeListTableHeaderCell(true);
		}

		// Token: 0x17003E5E RID: 15966
		// (get) Token: 0x0600C16E RID: 49518 RVA: 0x002B24BA File Offset: 0x002B06BA
		// (set) Token: 0x0600C16F RID: 49519 RVA: 0x002B24BD File Offset: 0x002B06BD
		public override TableRowSection TableSection
		{
			get
			{
				return TableRowSection.TableHeader;
			}
			set
			{
			}
		}

		// Token: 0x0600C170 RID: 49520 RVA: 0x002B24BF File Offset: 0x002B06BF
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtlHeader");
		}

		// Token: 0x17003E5F RID: 15967
		[SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToUpper")]
		[SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
		public TableCell this[string columnUniqueName]
		{
			get
			{
				TreeListColumn[] renderColumns = base.OwnerTreeList.RenderColumns;
				int num = 0;
				bool flag = false;
				foreach (TreeListColumn treeListColumn in renderColumns)
				{
					if (treeListColumn.UniqueName.Trim().ToUpper() == columnUniqueName.Trim().ToUpper())
					{
						flag = true;
						break;
					}
					num++;
				}
				if (flag)
				{
					return this.Cells[num];
				}
				throw new Exception("Cannot find a cell bound to column name '" + columnUniqueName + "'");
			}
		}
	}
}
