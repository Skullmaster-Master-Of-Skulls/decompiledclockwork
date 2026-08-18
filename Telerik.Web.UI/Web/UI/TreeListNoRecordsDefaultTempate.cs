using System;
using System.Globalization;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001274 RID: 4724
	internal class TreeListNoRecordsDefaultTempate : ITemplate
	{
		// Token: 0x0600C4E5 RID: 50405 RVA: 0x002C0208 File Offset: 0x002BE408
		public TreeListNoRecordsDefaultTempate(RadTreeList treeList)
		{
			this._treeList = treeList;
		}

		// Token: 0x0600C4E6 RID: 50406 RVA: 0x002C0218 File Offset: 0x002BE418
		public void InstantiateIn(Control container)
		{
			if (this._treeList.Width.IsEmpty)
			{
				container.Controls.Add(new LiteralControl(string.Format(CultureInfo.InvariantCulture, "<div>{0}</div>", new object[]
				{
					this._treeList.NoRecordsText
				})));
				return;
			}
			container.Controls.Add(new LiteralControl(string.Format(CultureInfo.InvariantCulture, "<div style=\"width:{0};\">{1}</div>", new object[]
			{
				this._treeList.Width.Value,
				this._treeList.NoRecordsText
			})));
		}

		// Token: 0x04003415 RID: 13333
		private RadTreeList _treeList;
	}
}
