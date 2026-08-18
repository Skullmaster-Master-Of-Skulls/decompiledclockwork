using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000BC6 RID: 3014
	public class SortExpression : ExpressionBase
	{
		// Token: 0x0600735A RID: 29530 RVA: 0x001AFDA6 File Offset: 0x001ADFA6
		public SortExpression() : this("")
		{
		}

		// Token: 0x0600735B RID: 29531 RVA: 0x001AFDB3 File Offset: 0x001ADFB3
		public SortExpression(string modelID) : base(modelID)
		{
		}

		// Token: 0x1700258E RID: 9614
		// (get) Token: 0x0600735C RID: 29532 RVA: 0x001AFDBC File Offset: 0x001ADFBC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public SortEntryCollection SortExpressionEntries
		{
			get
			{
				if (this._sortEntryCollection == null)
				{
					this._sortEntryCollection = new SortEntryCollection();
				}
				return this._sortEntryCollection;
			}
		}

		// Token: 0x04001F41 RID: 8001
		private SortEntryCollection _sortEntryCollection;
	}
}
