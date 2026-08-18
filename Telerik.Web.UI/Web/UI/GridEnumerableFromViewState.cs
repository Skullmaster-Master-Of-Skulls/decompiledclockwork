using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020010F6 RID: 4342
	internal class GridEnumerableFromViewState : GridEnumerableBase
	{
		// Token: 0x0600B1EB RID: 45547 RVA: 0x0026AA0C File Offset: 0x00268C0C
		public GridEnumerableFromViewState(IDictionary ViewState)
		{
			this._viewState = ViewState;
		}

		// Token: 0x1700399D RID: 14749
		// (get) Token: 0x0600B1EC RID: 45548 RVA: 0x0026AA1C File Offset: 0x00268C1C
		public override int Count
		{
			get
			{
				object obj = this._viewState["_!ItemCount"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
		}

		// Token: 0x1700399E RID: 14750
		// (get) Token: 0x0600B1ED RID: 45549 RVA: 0x0026AA48 File Offset: 0x00268C48
		public override int DataSourceCount
		{
			get
			{
				object obj = this._viewState["_!DSIC"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
		}

		// Token: 0x1700399F RID: 14751
		// (get) Token: 0x0600B1EE RID: 45550 RVA: 0x0026AA71 File Offset: 0x00268C71
		public override bool SupportsPaging
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600B1EF RID: 45551 RVA: 0x0026AA74 File Offset: 0x00268C74
		public override IEnumerable GetRowEnumerable()
		{
			return new GridDummyDataSource(this.Count);
		}

		// Token: 0x0600B1F0 RID: 45552 RVA: 0x0026AA8E File Offset: 0x00268C8E
		public override void TransformEnumerable()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x04002EB2 RID: 11954
		private IDictionary _viewState;
	}
}
