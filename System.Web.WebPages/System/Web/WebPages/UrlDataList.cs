using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.WebPages.Resources;

namespace System.Web.WebPages
{
	// Token: 0x02000090 RID: 144
	internal class UrlDataList : IList<string>, ICollection<string>, IEnumerable<string>, IEnumerable
	{
		// Token: 0x06000480 RID: 1152 RVA: 0x0000E030 File Offset: 0x0000C230
		public UrlDataList(string pathInfo)
		{
			if (string.IsNullOrEmpty(pathInfo))
			{
				this._urlData = new List<string>();
				return;
			}
			this._urlData = pathInfo.Split(new char[]
			{
				'/'
			}).ToList<string>();
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0000E075 File Offset: 0x0000C275
		public int Count
		{
			get
			{
				return this._urlData.Count;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0000E082 File Offset: 0x0000C282
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000FF RID: 255
		public string this[int index]
		{
			get
			{
				if (index >= this._urlData.Count)
				{
					return string.Empty;
				}
				return this._urlData[index];
			}
			set
			{
				throw new NotSupportedException(WebPageResources.UrlData_ReadOnly);
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000E0B3 File Offset: 0x0000C2B3
		public int IndexOf(string item)
		{
			return this._urlData.IndexOf(item);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000E0C1 File Offset: 0x0000C2C1
		public void Insert(int index, string item)
		{
			throw new NotSupportedException(WebPageResources.UrlData_ReadOnly);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000E0CD File Offset: 0x0000C2CD
		public void RemoveAt(int index)
		{
			throw new NotSupportedException(WebPageResources.UrlData_ReadOnly);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000E0D9 File Offset: 0x0000C2D9
		public void Add(string item)
		{
			throw new NotSupportedException(WebPageResources.UrlData_ReadOnly);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000E0E5 File Offset: 0x0000C2E5
		public void Clear()
		{
			throw new NotSupportedException(WebPageResources.UrlData_ReadOnly);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000E0F1 File Offset: 0x0000C2F1
		public bool Contains(string item)
		{
			return this._urlData.Contains(item);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000E0FF File Offset: 0x0000C2FF
		public void CopyTo(string[] array, int arrayIndex)
		{
			this._urlData.CopyTo(array, arrayIndex);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000E10E File Offset: 0x0000C30E
		public bool Remove(string item)
		{
			throw new NotSupportedException(WebPageResources.UrlData_ReadOnly);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000E11A File Offset: 0x0000C31A
		public IEnumerator<string> GetEnumerator()
		{
			return this._urlData.GetEnumerator();
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000E12C File Offset: 0x0000C32C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._urlData.GetEnumerator();
		}

		// Token: 0x0400013E RID: 318
		private List<string> _urlData;
	}
}
