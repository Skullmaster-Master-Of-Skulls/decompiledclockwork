using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000074 RID: 116
	public class RawTaggedData : ITaggedData
	{
		// Token: 0x0600048A RID: 1162 RVA: 0x000177DA File Offset: 0x000167DA
		public RawTaggedData(short tag)
		{
			this._tag = tag;
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x000177E9 File Offset: 0x000167E9
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x000177F1 File Offset: 0x000167F1
		public short TagID
		{
			get
			{
				return this._tag;
			}
			set
			{
				this._tag = value;
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x000177FA File Offset: 0x000167FA
		public void SetData(byte[] data, int offset, int count)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this._data = new byte[count];
			Array.Copy(data, offset, this._data, 0, count);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00017825 File Offset: 0x00016825
		public byte[] GetData()
		{
			return this._data;
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0001782D File Offset: 0x0001682D
		// (set) Token: 0x06000490 RID: 1168 RVA: 0x00017835 File Offset: 0x00016835
		public byte[] Data
		{
			get
			{
				return this._data;
			}
			set
			{
				this._data = value;
			}
		}

		// Token: 0x040002EB RID: 747
		private short _tag;

		// Token: 0x040002EC RID: 748
		private byte[] _data;
	}
}
