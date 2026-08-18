using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Web.UI.ExportInfrastructure
{
	// Token: 0x02000A4D RID: 2637
	public class ImageCollection : IEnumerable<Image>, IEnumerable
	{
		// Token: 0x06006628 RID: 26152 RVA: 0x0017E119 File Offset: 0x0017C319
		public IEnumerator<Image> GetEnumerator()
		{
			return this._imageCollection.GetEnumerator();
		}

		// Token: 0x06006629 RID: 26153 RVA: 0x0017E12B File Offset: 0x0017C32B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._imageCollection.GetEnumerator();
		}

		// Token: 0x170021AF RID: 8623
		// (get) Token: 0x0600662A RID: 26154 RVA: 0x0017E13D File Offset: 0x0017C33D
		public int Count
		{
			get
			{
				return this._imageCollection.Count;
			}
		}

		// Token: 0x0600662B RID: 26155 RVA: 0x0017E14A File Offset: 0x0017C34A
		public void Add(Image image)
		{
			this._imageCollection.Add(image);
		}

		// Token: 0x170021B0 RID: 8624
		public Image this[int idx]
		{
			get
			{
				if (this._imageCollection.Count < idx)
				{
					throw new IndexOutOfRangeException();
				}
				return this._imageCollection[idx];
			}
			set
			{
				if (this._imageCollection.Count < idx)
				{
					throw new IndexOutOfRangeException();
				}
				this._imageCollection[idx] = value;
			}
		}

		// Token: 0x040018BB RID: 6331
		private List<Image> _imageCollection = new List<Image>();
	}
}
