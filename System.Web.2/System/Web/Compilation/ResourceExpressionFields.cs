using System;

namespace System.Web.Compilation
{
	// Token: 0x02000859 RID: 2137
	public sealed class ResourceExpressionFields
	{
		// Token: 0x06006543 RID: 25923 RVA: 0x00164930 File Offset: 0x00162B30
		internal ResourceExpressionFields(string classKey, string resourceKey)
		{
			this._classKey = classKey;
			this._resourceKey = resourceKey;
		}

		// Token: 0x17001C74 RID: 7284
		// (get) Token: 0x06006544 RID: 25924 RVA: 0x00164946 File Offset: 0x00162B46
		public string ClassKey
		{
			get
			{
				if (this._classKey == null)
				{
					return string.Empty;
				}
				return this._classKey;
			}
		}

		// Token: 0x17001C75 RID: 7285
		// (get) Token: 0x06006545 RID: 25925 RVA: 0x0016495C File Offset: 0x00162B5C
		public string ResourceKey
		{
			get
			{
				if (this._resourceKey == null)
				{
					return string.Empty;
				}
				return this._resourceKey;
			}
		}

		// Token: 0x04003433 RID: 13363
		private string _classKey;

		// Token: 0x04003434 RID: 13364
		private string _resourceKey;
	}
}
