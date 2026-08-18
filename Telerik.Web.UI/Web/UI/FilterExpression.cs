using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000BD0 RID: 3024
	public class FilterExpression : ExpressionBase
	{
		// Token: 0x06007379 RID: 29561 RVA: 0x001AFFC9 File Offset: 0x001AE1C9
		public FilterExpression() : this("")
		{
		}

		// Token: 0x0600737A RID: 29562 RVA: 0x001AFFD6 File Offset: 0x001AE1D6
		public FilterExpression(string modelID) : base(modelID)
		{
			this._logic = ODataSourceFilterLogic.And;
		}

		// Token: 0x17002599 RID: 9625
		// (get) Token: 0x0600737B RID: 29563 RVA: 0x001AFFE6 File Offset: 0x001AE1E6
		// (set) Token: 0x0600737C RID: 29564 RVA: 0x001AFFEE File Offset: 0x001AE1EE
		[DefaultValue(ODataSourceFilterLogic.And)]
		[Category("Behavior")]
		[Description("Gets or sets the filter logic, AND or OR.")]
		public ODataSourceFilterLogic LogicOperator
		{
			get
			{
				return this._logic;
			}
			set
			{
				this._logic = value;
			}
		}

		// Token: 0x1700259A RID: 9626
		// (get) Token: 0x0600737D RID: 29565 RVA: 0x001AFFF7 File Offset: 0x001AE1F7
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public FilterEntryCollection FilterExpressionEntries
		{
			get
			{
				if (this._filterEntryCollection == null)
				{
					this._filterEntryCollection = new FilterEntryCollection();
				}
				return this._filterEntryCollection;
			}
		}

		// Token: 0x04001F5E RID: 8030
		private ODataSourceFilterLogic _logic;

		// Token: 0x04001F5F RID: 8031
		private FilterEntryCollection _filterEntryCollection;
	}
}
