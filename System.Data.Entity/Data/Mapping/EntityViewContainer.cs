using System;
using System.Collections.Generic;

namespace System.Data.Mapping
{
	// Token: 0x02000231 RID: 561
	public abstract class EntityViewContainer
	{
		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x060023EE RID: 9198 RVA: 0x0008268C File Offset: 0x0008088C
		internal IEnumerable<KeyValuePair<string, string>> ExtentViews
		{
			get
			{
				int num;
				for (int i = 0; i < this.ViewCount; i = num + 1)
				{
					yield return this.GetViewAt(i);
					num = i;
				}
				yield break;
			}
		}

		// Token: 0x060023EF RID: 9199
		protected abstract KeyValuePair<string, string> GetViewAt(int index);

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x060023F0 RID: 9200 RVA: 0x000826A9 File Offset: 0x000808A9
		// (set) Token: 0x060023F1 RID: 9201 RVA: 0x000826B1 File Offset: 0x000808B1
		public string EdmEntityContainerName
		{
			get
			{
				return this.m_storededmEntityContainerName;
			}
			set
			{
				this.m_storededmEntityContainerName = value;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x060023F2 RID: 9202 RVA: 0x000826BA File Offset: 0x000808BA
		// (set) Token: 0x060023F3 RID: 9203 RVA: 0x000826C2 File Offset: 0x000808C2
		public string StoreEntityContainerName
		{
			get
			{
				return this.m_storedStoreEntityContainerName;
			}
			set
			{
				this.m_storedStoreEntityContainerName = value;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x060023F4 RID: 9204 RVA: 0x000826CB File Offset: 0x000808CB
		// (set) Token: 0x060023F5 RID: 9205 RVA: 0x000826D3 File Offset: 0x000808D3
		public string HashOverMappingClosure
		{
			get
			{
				return this.m_storedHashOverMappingClosure;
			}
			set
			{
				this.m_storedHashOverMappingClosure = value;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x060023F6 RID: 9206 RVA: 0x000826DC File Offset: 0x000808DC
		// (set) Token: 0x060023F7 RID: 9207 RVA: 0x000826E4 File Offset: 0x000808E4
		public string HashOverAllExtentViews
		{
			get
			{
				return this.m_storedhashOverAllExtentViews;
			}
			set
			{
				this.m_storedhashOverAllExtentViews = value;
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x060023F8 RID: 9208 RVA: 0x000826ED File Offset: 0x000808ED
		// (set) Token: 0x060023F9 RID: 9209 RVA: 0x000826F5 File Offset: 0x000808F5
		public int ViewCount
		{
			get
			{
				return this._viewCount;
			}
			protected set
			{
				this._viewCount = value;
			}
		}

		// Token: 0x04000FEE RID: 4078
		private string m_storedHashOverMappingClosure;

		// Token: 0x04000FEF RID: 4079
		private string m_storedhashOverAllExtentViews;

		// Token: 0x04000FF0 RID: 4080
		private string m_storededmEntityContainerName;

		// Token: 0x04000FF1 RID: 4081
		private string m_storedStoreEntityContainerName;

		// Token: 0x04000FF2 RID: 4082
		private int _viewCount;
	}
}
