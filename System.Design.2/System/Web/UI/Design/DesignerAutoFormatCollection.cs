using System;
using System.Collections;
using System.Drawing;

namespace System.Web.UI.Design
{
	// Token: 0x0200002F RID: 47
	public sealed class DesignerAutoFormatCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000C99A File Offset: 0x0000AB9A
		public int Count
		{
			get
			{
				return this._autoFormats.Count;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000C9A8 File Offset: 0x0000ABA8
		public Size PreviewSize
		{
			get
			{
				int num = 200;
				int num2 = 200;
				foreach (object obj in this._autoFormats)
				{
					DesignerAutoFormat designerAutoFormat = (DesignerAutoFormat)obj;
					int num3 = (int)designerAutoFormat.Style.Height.Value;
					if (num3 > num)
					{
						num = num3;
					}
					int num4 = (int)designerAutoFormat.Style.Width.Value;
					if (num4 > num2)
					{
						num2 = num4;
					}
				}
				return new Size(num2, num);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000CA50 File Offset: 0x0000AC50
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000044 RID: 68
		public DesignerAutoFormat this[int index]
		{
			get
			{
				return (DesignerAutoFormat)this._autoFormats[index];
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000CA66 File Offset: 0x0000AC66
		public int Add(DesignerAutoFormat format)
		{
			return this._autoFormats.Add(format);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000CA74 File Offset: 0x0000AC74
		public void Clear()
		{
			this._autoFormats.Clear();
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000CA81 File Offset: 0x0000AC81
		public bool Contains(DesignerAutoFormat format)
		{
			return this._autoFormats.Contains(format);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000CA8F File Offset: 0x0000AC8F
		public int IndexOf(DesignerAutoFormat format)
		{
			return this._autoFormats.IndexOf(format);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000CA9D File Offset: 0x0000AC9D
		public void Insert(int index, DesignerAutoFormat format)
		{
			this._autoFormats.Insert(index, format);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000CAAC File Offset: 0x0000ACAC
		public void Remove(DesignerAutoFormat format)
		{
			this._autoFormats.Remove(format);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000CABA File Offset: 0x0000ACBA
		public void RemoveAt(int index)
		{
			this._autoFormats.RemoveAt(index);
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000CAC8 File Offset: 0x0000ACC8
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000445B File Offset: 0x0000265B
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000445B File Offset: 0x0000265B
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000445B File Offset: 0x0000265B
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000049 RID: 73
		object IList.this[int index]
		{
			get
			{
				return this._autoFormats[index];
			}
			set
			{
				if (value is DesignerAutoFormat)
				{
					this._autoFormats[index] = value;
				}
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000CAF5 File Offset: 0x0000ACF5
		int IList.Add(object value)
		{
			if (value is DesignerAutoFormat)
			{
				return this.Add((DesignerAutoFormat)value);
			}
			return -1;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000CB0D File Offset: 0x0000AD0D
		bool IList.Contains(object value)
		{
			return value is DesignerAutoFormat && this.Contains((DesignerAutoFormat)value);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000CB25 File Offset: 0x0000AD25
		void ICollection.CopyTo(Array array, int index)
		{
			this._autoFormats.CopyTo(array, index);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000CB34 File Offset: 0x0000AD34
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._autoFormats.GetEnumerator();
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000CB41 File Offset: 0x0000AD41
		int IList.IndexOf(object value)
		{
			return this.IndexOf((DesignerAutoFormat)value);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000CB4F File Offset: 0x0000AD4F
		void IList.Insert(int index, object value)
		{
			if (value is DesignerAutoFormat)
			{
				this.Insert(index, (DesignerAutoFormat)value);
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000CB66 File Offset: 0x0000AD66
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000CB6F File Offset: 0x0000AD6F
		void IList.Remove(object value)
		{
			if (value is DesignerAutoFormat)
			{
				this.Remove((DesignerAutoFormat)value);
			}
		}

		// Token: 0x0400011A RID: 282
		private ArrayList _autoFormats = new ArrayList();
	}
}
