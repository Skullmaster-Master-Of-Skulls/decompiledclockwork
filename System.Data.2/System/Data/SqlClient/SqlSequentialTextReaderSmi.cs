using System;
using System.Data.Common;
using System.IO;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x020001F9 RID: 505
	internal sealed class SqlSequentialTextReaderSmi : TextReader
	{
		// Token: 0x06001F54 RID: 8020 RVA: 0x000D8CA8 File Offset: 0x000D80A8
		internal SqlSequentialTextReaderSmi(SmiEventSink_Default sink, ITypedGettersV3 getters, int columnIndex, long length)
		{
			this._sink = sink;
			this._getters = getters;
			this._columnIndex = columnIndex;
			this._length = length;
			this._position = 0L;
			this._peekedChar = -1;
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001F55 RID: 8021 RVA: 0x000D8CE8 File Offset: 0x000D80E8
		internal int ColumnIndex
		{
			get
			{
				return this._columnIndex;
			}
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x000D8CFC File Offset: 0x000D80FC
		public override int Peek()
		{
			if (!this.HasPeekedChar)
			{
				this._peekedChar = this.Read();
			}
			return this._peekedChar;
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x000D8D24 File Offset: 0x000D8124
		public override int Read()
		{
			if (this.IsClosed)
			{
				throw ADP.ObjectDisposed(this);
			}
			int result = -1;
			if (this.HasPeekedChar)
			{
				result = this._peekedChar;
				this._peekedChar = -1;
			}
			else if (this._position < this._length)
			{
				char[] array = new char[1];
				int chars_Unchecked = ValueUtilsSmi.GetChars_Unchecked(this._sink, this._getters, this._columnIndex, this._position, array, 0, 1);
				if (chars_Unchecked == 1)
				{
					result = (int)array[0];
					this._position += 1L;
				}
			}
			return result;
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x000D8DA8 File Offset: 0x000D81A8
		public override int Read(char[] buffer, int index, int count)
		{
			SqlSequentialTextReader.ValidateReadParameters(buffer, index, count);
			if (this.IsClosed)
			{
				throw ADP.ObjectDisposed(this);
			}
			int num = 0;
			if (count > 0 && this.HasPeekedChar)
			{
				buffer[index + num] = (char)this._peekedChar;
				num++;
				this._peekedChar = -1;
			}
			int num2 = (int)Math.Min((long)(count - num), this._length - this._position);
			if (num2 > 0)
			{
				int chars_Unchecked = ValueUtilsSmi.GetChars_Unchecked(this._sink, this._getters, this._columnIndex, this._position, buffer, index + num, num2);
				this._position += (long)chars_Unchecked;
				num += chars_Unchecked;
			}
			return num;
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x000D8E44 File Offset: 0x000D8244
		internal void SetClosed()
		{
			this._sink = null;
			this._getters = null;
			this._peekedChar = -1;
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001F5A RID: 8026 RVA: 0x000D8E68 File Offset: 0x000D8268
		private bool IsClosed
		{
			get
			{
				return this._sink == null || this._getters == null;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001F5B RID: 8027 RVA: 0x000D8E88 File Offset: 0x000D8288
		private bool HasPeekedChar
		{
			get
			{
				return this._peekedChar >= 0;
			}
		}

		// Token: 0x040011B0 RID: 4528
		private SmiEventSink_Default _sink;

		// Token: 0x040011B1 RID: 4529
		private ITypedGettersV3 _getters;

		// Token: 0x040011B2 RID: 4530
		private int _columnIndex;

		// Token: 0x040011B3 RID: 4531
		private long _position;

		// Token: 0x040011B4 RID: 4532
		private long _length;

		// Token: 0x040011B5 RID: 4533
		private int _peekedChar;
	}
}
