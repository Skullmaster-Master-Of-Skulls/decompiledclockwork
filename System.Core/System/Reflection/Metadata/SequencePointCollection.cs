using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Internal;
using System.Reflection.Metadata.Ecma335;

namespace System.Reflection.Metadata
{
	// Token: 0x02000065 RID: 101
	internal struct SequencePointCollection : IEnumerable<SequencePoint>, IEnumerable
	{
		// Token: 0x060002D3 RID: 723 RVA: 0x000076D1 File Offset: 0x000058D1
		internal SequencePointCollection(MemoryBlock block, DocumentHandle document)
		{
			this._block = block;
			this._document = document;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x000076E1 File Offset: 0x000058E1
		public SequencePointCollection.Enumerator GetEnumerator()
		{
			return new SequencePointCollection.Enumerator(this._block, this._document);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000076F4 File Offset: 0x000058F4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00007701 File Offset: 0x00005901
		IEnumerator<SequencePoint> IEnumerable<SequencePoint>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000361 RID: 865
		private readonly MemoryBlock _block;

		// Token: 0x04000362 RID: 866
		private readonly DocumentHandle _document;

		// Token: 0x020002FA RID: 762
		internal struct Enumerator : IEnumerator<SequencePoint>, IDisposable, IEnumerator
		{
			// Token: 0x06001A4F RID: 6735 RVA: 0x0006089B File Offset: 0x0005EA9B
			internal Enumerator(MemoryBlock block, DocumentHandle document)
			{
				this._reader = new BlobReader(block);
				this._current = new SequencePoint(document, -1);
				this._previousNonHiddenStartLine = -1;
				this._previousNonHiddenStartColumn = 0;
			}

			// Token: 0x06001A50 RID: 6736 RVA: 0x000608C4 File Offset: 0x0005EAC4
			public bool MoveNext()
			{
				if (this._reader.RemainingBytes == 0)
				{
					return false;
				}
				DocumentHandle document = this._current.Document;
				int offset;
				if (this._reader.Offset == 0)
				{
					this._reader.ReadCompressedInteger();
					if (document.IsNil)
					{
						document = this.ReadDocumentHandle();
					}
					offset = this._reader.ReadCompressedInteger();
				}
				else
				{
					int delta;
					while ((delta = this._reader.ReadCompressedInteger()) == 0)
					{
						document = this.ReadDocumentHandle();
					}
					offset = this.AddOffsets(this._current.Offset, delta);
				}
				int num;
				int num2;
				this.ReadDeltaLinesAndColumns(out num, out num2);
				if (num == 0 && num2 == 0)
				{
					this._current = new SequencePoint(document, offset);
					return true;
				}
				int num3;
				ushort num4;
				if (this._previousNonHiddenStartLine < 0)
				{
					num3 = this.ReadLine();
					num4 = this.ReadColumn();
				}
				else
				{
					num3 = this.AddLines(this._previousNonHiddenStartLine, this._reader.ReadCompressedSignedInteger());
					num4 = this.AddColumns(this._previousNonHiddenStartColumn, this._reader.ReadCompressedSignedInteger());
				}
				this._previousNonHiddenStartLine = num3;
				this._previousNonHiddenStartColumn = num4;
				this._current = new SequencePoint(document, offset, num3, num4, this.AddLines(num3, num), this.AddColumns(num4, num2));
				return true;
			}

			// Token: 0x06001A51 RID: 6737 RVA: 0x000609EE File Offset: 0x0005EBEE
			private void ReadDeltaLinesAndColumns(out int deltaLines, out int deltaColumns)
			{
				deltaLines = this._reader.ReadCompressedInteger();
				deltaColumns = ((deltaLines == 0) ? this._reader.ReadCompressedInteger() : this._reader.ReadCompressedSignedInteger());
			}

			// Token: 0x06001A52 RID: 6738 RVA: 0x00060A1B File Offset: 0x0005EC1B
			private int ReadLine()
			{
				return this._reader.ReadCompressedInteger();
			}

			// Token: 0x06001A53 RID: 6739 RVA: 0x00060A28 File Offset: 0x0005EC28
			private ushort ReadColumn()
			{
				int num = this._reader.ReadCompressedInteger();
				if (num > 65535)
				{
					Throw.SequencePointValueOutOfRange();
				}
				return (ushort)num;
			}

			// Token: 0x06001A54 RID: 6740 RVA: 0x00060A50 File Offset: 0x0005EC50
			private int AddOffsets(int value, int delta)
			{
				int num = value + delta;
				if (num < 0)
				{
					Throw.SequencePointValueOutOfRange();
				}
				return num;
			}

			// Token: 0x06001A55 RID: 6741 RVA: 0x00060A6C File Offset: 0x0005EC6C
			private int AddLines(int value, int delta)
			{
				int num = value + delta;
				if (num < 0 || num >= 16707566)
				{
					Throw.SequencePointValueOutOfRange();
				}
				return num;
			}

			// Token: 0x06001A56 RID: 6742 RVA: 0x00060A90 File Offset: 0x0005EC90
			private ushort AddColumns(ushort value, int delta)
			{
				int num = (int)value + delta;
				if (num < 0 || num >= 65535)
				{
					Throw.SequencePointValueOutOfRange();
				}
				return (ushort)num;
			}

			// Token: 0x06001A57 RID: 6743 RVA: 0x00060AB4 File Offset: 0x0005ECB4
			private DocumentHandle ReadDocumentHandle()
			{
				int num = this._reader.ReadCompressedInteger();
				if (num == 0 || !TokenTypeIds.IsValidRowId(num))
				{
					Throw.InvalidHandle();
				}
				return DocumentHandle.FromRowId(num);
			}

			// Token: 0x170004E2 RID: 1250
			// (get) Token: 0x06001A58 RID: 6744 RVA: 0x00060AE3 File Offset: 0x0005ECE3
			public SequencePoint Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x170004E3 RID: 1251
			// (get) Token: 0x06001A59 RID: 6745 RVA: 0x00060AEB File Offset: 0x0005ECEB
			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x06001A5A RID: 6746 RVA: 0x00060AF8 File Offset: 0x0005ECF8
			public void Reset()
			{
				this._reader.Reset();
				this._current = default(SequencePoint);
			}

			// Token: 0x06001A5B RID: 6747 RVA: 0x00060B11 File Offset: 0x0005ED11
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000DEE RID: 3566
			private BlobReader _reader;

			// Token: 0x04000DEF RID: 3567
			private SequencePoint _current;

			// Token: 0x04000DF0 RID: 3568
			private int _previousNonHiddenStartLine;

			// Token: 0x04000DF1 RID: 3569
			private ushort _previousNonHiddenStartColumn;
		}
	}
}
