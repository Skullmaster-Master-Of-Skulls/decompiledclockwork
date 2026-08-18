using System;
using System.Collections.Generic;

namespace System.Web.WebPages.Scope
{
	// Token: 0x0200007A RID: 122
	internal class ScopeStorageComparer : IEqualityComparer<object>
	{
		// Token: 0x060003A6 RID: 934 RVA: 0x0000C55B File Offset: 0x0000A75B
		private ScopeStorageComparer()
		{
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000C579 File Offset: 0x0000A779
		public static IEqualityComparer<object> Instance
		{
			get
			{
				if (ScopeStorageComparer._instance == null)
				{
					ScopeStorageComparer._instance = new ScopeStorageComparer();
				}
				return ScopeStorageComparer._instance;
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000C594 File Offset: 0x0000A794
		public bool Equals(object x, object y)
		{
			string text = x as string;
			string text2 = y as string;
			if (text != null && text2 != null)
			{
				return this._stringComparer.Equals(text, text2);
			}
			return this._defaultComparer.Equals(x, y);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000C5D0 File Offset: 0x0000A7D0
		public int GetHashCode(object obj)
		{
			string text = obj as string;
			if (text != null)
			{
				return this._stringComparer.GetHashCode(text);
			}
			return this._defaultComparer.GetHashCode(obj);
		}

		// Token: 0x04000113 RID: 275
		private static IEqualityComparer<object> _instance;

		// Token: 0x04000114 RID: 276
		private readonly IEqualityComparer<object> _defaultComparer = EqualityComparer<object>.Default;

		// Token: 0x04000115 RID: 277
		private readonly IEqualityComparer<string> _stringComparer = StringComparer.OrdinalIgnoreCase;
	}
}
