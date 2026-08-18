using System;
using System.Text;

namespace Telerik.Web.UI.Upload
{
	// Token: 0x02001345 RID: 4933
	internal class RequestParser
	{
		// Token: 0x1700420F RID: 16911
		// (get) Token: 0x0600CD99 RID: 52633 RVA: 0x002DC43F File Offset: 0x002DA63F
		private Encoding Encoding
		{
			get
			{
				return this._encoding;
			}
		}

		// Token: 0x17004210 RID: 16912
		// (get) Token: 0x0600CD9A RID: 52634 RVA: 0x002DC447 File Offset: 0x002DA647
		private byte[] Boundary
		{
			get
			{
				return this._boundary;
			}
		}

		// Token: 0x17004211 RID: 16913
		// (get) Token: 0x0600CD9B RID: 52635 RVA: 0x002DC44F File Offset: 0x002DA64F
		private byte[] FirstBoundary
		{
			get
			{
				if (this._firstBoundary == null)
				{
					this._firstBoundary = this.MergeArrays(this.Boundary, this.CrLf);
				}
				return this._firstBoundary;
			}
		}

		// Token: 0x17004212 RID: 16914
		// (get) Token: 0x0600CD9C RID: 52636 RVA: 0x002DC477 File Offset: 0x002DA677
		private byte[] LastBoundary
		{
			get
			{
				if (this._lastBoundary == null)
				{
					this._lastBoundary = this.MergeArrays(this.MergeArrays(this.CrLf, this.Boundary), this.LastBoundarySuffix);
				}
				return this._lastBoundary;
			}
		}

		// Token: 0x17004213 RID: 16915
		// (get) Token: 0x0600CD9D RID: 52637 RVA: 0x002DC4AB File Offset: 0x002DA6AB
		private byte[] ChunkBoundary
		{
			get
			{
				if (this._chunkBoundary == null)
				{
					this._chunkBoundary = this.MergeArrays(this.MergeArrays(this.CrLf, this.Boundary), this.CrLf);
				}
				return this._chunkBoundary;
			}
		}

		// Token: 0x17004214 RID: 16916
		// (get) Token: 0x0600CD9E RID: 52638 RVA: 0x002DC4DF File Offset: 0x002DA6DF
		private byte[] LastBoundarySuffix
		{
			get
			{
				if (this._lastBoundarySuffix == null)
				{
					this._lastBoundarySuffix = this.Encoding.GetBytes("--");
				}
				return this._lastBoundarySuffix;
			}
		}

		// Token: 0x17004215 RID: 16917
		// (get) Token: 0x0600CD9F RID: 52639 RVA: 0x002DC505 File Offset: 0x002DA705
		private RequestStateStore RequestStateStore
		{
			get
			{
				return this._requestStateStore;
			}
		}

		// Token: 0x17004216 RID: 16918
		// (get) Token: 0x0600CDA0 RID: 52640 RVA: 0x002DC510 File Offset: 0x002DA710
		private byte[] CrLf
		{
			get
			{
				if (this._crLf == null)
				{
					this._crLf = new byte[]
					{
						13,
						10
					};
				}
				return this._crLf;
			}
		}

		// Token: 0x17004217 RID: 16919
		// (get) Token: 0x0600CDA1 RID: 52641 RVA: 0x002DC543 File Offset: 0x002DA743
		private int BufferedBytesLength
		{
			get
			{
				if (this._bufferedBytesLength < 0)
				{
					this._bufferedBytesLength = this.Boundary.Length + 2 * this.CrLf.Length;
				}
				return this._bufferedBytesLength;
			}
		}

		// Token: 0x0600CDA2 RID: 52642 RVA: 0x002DC570 File Offset: 0x002DA770
		public RequestParser(byte[] boundary, Encoding encoding, RequestStateStore requestStateStore)
		{
			this._boundary = boundary;
			this._encoding = encoding;
			this._requestStateStore = requestStateStore;
			this._searchedContentBoundary = this.FirstBoundary;
		}

		// Token: 0x0600CDA3 RID: 52643 RVA: 0x002DC5C0 File Offset: 0x002DA7C0
		public void Parse(byte[] chunk, int validChunkBytes)
		{
			if (this._lastBoundaryFound)
			{
				return;
			}
			this.RequestStateStore.UpdateCurrentRequestBytesCount(validChunkBytes);
			int num = -1;
			for (;;)
			{
				int num2 = validChunkBytes - this.BufferedBytesLength + this._bufferedBytes.Length;
				int nextBoundaryIndex = this.GetNextBoundaryIndex(chunk, num + 1, num2, out this._lastBoundaryFound);
				bool flag = nextBoundaryIndex >= 0;
				if (!this._firstBoundaryFound)
				{
					if (!flag)
					{
						break;
					}
					this._firstBoundaryFound = true;
					this._currentStartingBoundaryLength = this._searchedContentBoundary.Length;
					this._searchedContentBoundary = this.ChunkBoundary;
					num = nextBoundaryIndex;
				}
				else
				{
					int num3 = this.GetFieldStartIndex(num);
					int num4 = this.GetFieldLength(chunk, nextBoundaryIndex, num2, num3);
					if (num4 < 0)
					{
						this._currentFieldStartIndexInBuffer = num3 - chunk.Length;
					}
					else
					{
						if (this._currentFieldStartIndexInBuffer >= 0)
						{
							num3 = this._currentFieldStartIndexInBuffer;
							num4 -= this._currentFieldStartIndexInBuffer;
						}
						this._currentFieldStartIndexInBuffer = -1;
						this.UpdateStateStore(chunk, num3, num4, nextBoundaryIndex >= 0);
					}
					this._currentStartingBoundaryLength = this._searchedContentBoundary.Length;
					num = nextBoundaryIndex;
				}
				if (!flag || this._lastBoundaryFound)
				{
					goto IL_FB;
				}
			}
			this.UpdateBufferedBytes(chunk);
			return;
			IL_FB:
			if (!this._lastBoundaryFound)
			{
				this.UpdateBufferedBytes(chunk);
			}
		}

		// Token: 0x0600CDA4 RID: 52644 RVA: 0x002DC6D7 File Offset: 0x002DA8D7
		private int GetFieldLength(byte[] chunk, int nextBoundaryStartIndex, int maxCountOfBytes, int fieldStartIndex)
		{
			if (nextBoundaryStartIndex < 0)
			{
				return maxCountOfBytes - fieldStartIndex;
			}
			return nextBoundaryStartIndex - fieldStartIndex;
		}

		// Token: 0x0600CDA5 RID: 52645 RVA: 0x002DC6E6 File Offset: 0x002DA8E6
		private int GetFieldStartIndex(int currentBoundaryStartIndex)
		{
			if (currentBoundaryStartIndex < 0)
			{
				return 0;
			}
			return currentBoundaryStartIndex + this._currentStartingBoundaryLength;
		}

		// Token: 0x0600CDA6 RID: 52646 RVA: 0x002DC6F8 File Offset: 0x002DA8F8
		private int GetNextBoundaryIndex(byte[] chunk, int searchStart, int countOfBytesToSearch, out bool lastBoundary)
		{
			lastBoundary = false;
			int num = ByteComparer.IndexOf(this.LastBoundary, this._bufferedBytes, chunk, searchStart);
			int num2 = ByteComparer.IndexOf(this._searchedContentBoundary, this._bufferedBytes, chunk, searchStart);
			if (num2 >= 0 && num2 < countOfBytesToSearch)
			{
				return num2;
			}
			if (num < countOfBytesToSearch + this._bufferedBytes.Length)
			{
				if (num >= 0)
				{
					lastBoundary = true;
				}
				return num;
			}
			return -1;
		}

		// Token: 0x0600CDA7 RID: 52647 RVA: 0x002DC754 File Offset: 0x002DA954
		private byte[] MergeArrays(byte[] array1, byte[] array2)
		{
			int length = array1.Length + array2.Length;
			byte[] array3 = (byte[])Array.CreateInstance(typeof(byte), length);
			Array.Copy(array1, array3, array1.Length);
			Array.Copy(array2, 0, array3, array1.Length, array2.Length);
			return array3;
		}

		// Token: 0x0600CDA8 RID: 52648 RVA: 0x002DC798 File Offset: 0x002DA998
		private void UpdateBufferedBytes(byte[] chunk)
		{
			int num = this.BufferedBytesLength;
			int num2 = this._bufferedBytes.Length;
			if (this._bufferedBytes.Length + chunk.Length < this.BufferedBytesLength)
			{
				num = this._bufferedBytes.Length + chunk.Length;
			}
			if (this._bufferedBytes.Length != num)
			{
				Array.Resize<byte>(ref this._bufferedBytes, num);
			}
			if (chunk.Length < num && num2 + chunk.Length > this.BufferedBytesLength)
			{
				this.ShiftBufferBytes(num2, chunk.Length);
			}
			if (chunk.Length >= this._bufferedBytes.Length)
			{
				Array.Copy(chunk, chunk.Length - this._bufferedBytes.Length, this._bufferedBytes, 0, this._bufferedBytes.Length);
				return;
			}
			Array.Copy(chunk, 0, this._bufferedBytes, this._bufferedBytes.Length - chunk.Length, chunk.Length);
		}

		// Token: 0x0600CDA9 RID: 52649 RVA: 0x002DC858 File Offset: 0x002DAA58
		private void ShiftBufferBytes(int currentMeaningBytes, int freeSpace)
		{
			int num = currentMeaningBytes - this._bufferedBytes.Length + freeSpace;
			for (int i = num; i < currentMeaningBytes; i++)
			{
				this._bufferedBytes[i - num] = this._bufferedBytes[i];
			}
		}

		// Token: 0x0600CDAA RID: 52650 RVA: 0x002DC890 File Offset: 0x002DAA90
		private void UpdateStateStore(byte[] chunk, int fieldStartIndex, int fieldBytesCount, bool isFinal)
		{
			byte[] fieldBytes = this.GetFieldBytes(chunk, fieldStartIndex, fieldBytesCount);
			this.RequestStateStore.Record(fieldBytes, isFinal);
		}

		// Token: 0x0600CDAB RID: 52651 RVA: 0x002DC8B8 File Offset: 0x002DAAB8
		private byte[] GetFieldBytes(byte[] chunk, int fieldStartIndex, int fieldBytesCount)
		{
			byte[] array = (byte[])Array.CreateInstance(typeof(byte), fieldBytesCount);
			if (fieldStartIndex >= this._bufferedBytes.Length)
			{
				Array.Copy(chunk, fieldStartIndex - this._bufferedBytes.Length, array, 0, fieldBytesCount);
				return array;
			}
			if (this._bufferedBytes.Length >= fieldStartIndex + fieldBytesCount)
			{
				Array.Copy(this._bufferedBytes, fieldStartIndex, array, 0, fieldBytesCount);
				return array;
			}
			int num = this._bufferedBytes.Length - fieldStartIndex;
			int length = fieldBytesCount - num;
			Array.Copy(this._bufferedBytes, fieldStartIndex, array, 0, num);
			Array.Copy(chunk, 0, array, num, length);
			return array;
		}

		// Token: 0x040036ED RID: 14061
		private byte[] _bufferedBytes = new byte[0];

		// Token: 0x040036EE RID: 14062
		private bool _lastBoundaryFound;

		// Token: 0x040036EF RID: 14063
		private bool _firstBoundaryFound;

		// Token: 0x040036F0 RID: 14064
		private int _currentStartingBoundaryLength;

		// Token: 0x040036F1 RID: 14065
		private int _currentFieldStartIndexInBuffer = -1;

		// Token: 0x040036F2 RID: 14066
		private Encoding _encoding;

		// Token: 0x040036F3 RID: 14067
		private byte[] _boundary;

		// Token: 0x040036F4 RID: 14068
		private byte[] _firstBoundary;

		// Token: 0x040036F5 RID: 14069
		private byte[] _lastBoundary;

		// Token: 0x040036F6 RID: 14070
		private byte[] _chunkBoundary;

		// Token: 0x040036F7 RID: 14071
		private byte[] _lastBoundarySuffix;

		// Token: 0x040036F8 RID: 14072
		private RequestStateStore _requestStateStore;

		// Token: 0x040036F9 RID: 14073
		private byte[] _crLf;

		// Token: 0x040036FA RID: 14074
		private int _bufferedBytesLength = -1;

		// Token: 0x040036FB RID: 14075
		private byte[] _searchedContentBoundary;
	}
}
