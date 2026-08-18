using System;
using System.Web.Util;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200055F RID: 1375
	[Serializable]
	public abstract class PersonalizationStateInfo
	{
		// Token: 0x060045D8 RID: 17880 RVA: 0x000E6386 File Offset: 0x000E4586
		internal PersonalizationStateInfo(string path, DateTime lastUpdatedDate, int size)
		{
			this._path = StringUtil.CheckAndTrimString(path, "path");
			PersonalizationProviderHelper.CheckNegativeInteger(size, "size");
			this._lastUpdatedDate = lastUpdatedDate.ToUniversalTime();
			this._size = size;
		}

		// Token: 0x17001492 RID: 5266
		// (get) Token: 0x060045D9 RID: 17881 RVA: 0x000E63BE File Offset: 0x000E45BE
		public string Path
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x17001493 RID: 5267
		// (get) Token: 0x060045DA RID: 17882 RVA: 0x000E63C6 File Offset: 0x000E45C6
		public DateTime LastUpdatedDate
		{
			get
			{
				return this._lastUpdatedDate.ToLocalTime();
			}
		}

		// Token: 0x17001494 RID: 5268
		// (get) Token: 0x060045DB RID: 17883 RVA: 0x000E63D3 File Offset: 0x000E45D3
		public int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x04002680 RID: 9856
		private string _path;

		// Token: 0x04002681 RID: 9857
		private DateTime _lastUpdatedDate;

		// Token: 0x04002682 RID: 9858
		private int _size;
	}
}
