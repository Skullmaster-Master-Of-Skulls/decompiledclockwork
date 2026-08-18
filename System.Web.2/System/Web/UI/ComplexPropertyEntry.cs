using System;

namespace System.Web.UI
{
	// Token: 0x0200025A RID: 602
	public class ComplexPropertyEntry : BuilderPropertyEntry
	{
		// Token: 0x06001BB0 RID: 7088 RVA: 0x0005752A File Offset: 0x0005572A
		internal ComplexPropertyEntry()
		{
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x00057532 File Offset: 0x00055732
		internal ComplexPropertyEntry(bool isCollectionItem)
		{
			this._isCollectionItem = isCollectionItem;
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06001BB2 RID: 7090 RVA: 0x00057541 File Offset: 0x00055741
		public bool IsCollectionItem
		{
			get
			{
				return this._isCollectionItem;
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06001BB3 RID: 7091 RVA: 0x00057549 File Offset: 0x00055749
		// (set) Token: 0x06001BB4 RID: 7092 RVA: 0x00057551 File Offset: 0x00055751
		public bool ReadOnly
		{
			get
			{
				return this._readOnly;
			}
			set
			{
				this._readOnly = value;
			}
		}

		// Token: 0x040018D0 RID: 6352
		private bool _readOnly;

		// Token: 0x040018D1 RID: 6353
		private bool _isCollectionItem;
	}
}
