using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Internal;
using System.Reflection.Metadata.Ecma335;

namespace System.Reflection.Metadata
{
	// Token: 0x020000A5 RID: 165
	public struct SequencePointCollection : IEnumerable<SequencePoint>, IEnumerable
	{
		// Token: 0x060006F8 RID: 1784 RVA: 0x0000FD2D File Offset: 0x0000DF2D
		internal SequencePointCollection(MemoryBlock block, DocumentHandle document)
		{
			this._block = block;
			this._document = document;
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0000FD3D File Offset: 0x0000DF3D
		public SequencePointCollection.Enumerator GetEnumerator()
		{
			return new SequencePointCollection.Enumerator(this._block, this._document);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0000FD50 File Offset: 0x0000DF50
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0000FD50 File Offset: 0x0000DF50
		IEnumerator<SequencePoint> IEnumerable<SequencePoint>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400041F RID: 1055
		private readonly MemoryBlock _block;

		// Token: 0x04000420 RID: 1056
		private readonly DocumentHandle _document;

		// Token: 0x02000192 RID: 402
		public struct Enumerator : IEnumerator<SequencePoint>, IEnumerator, IDisposable
		{
			// Token: 0x06000C1A RID: 3098 RVA: 0x00021D29 File Offset: 0x0001FF29
			internal Enumerator(MemoryBlock block, DocumentHandle document)
			{
				this._reader = new BlobReader(block);
				this._current = new SequencePoint(document, -1);
				this._previousNonHiddenStartLine = -1;
				this._previousNonHiddenStartColumn = 0;
			}

			// Token: 0x06000C1B RID: 3099 RVA: 0x00021D54 File Offset: 0x0001FF54
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

			// Token: 0x06000C1C RID: 3100 RVA: 0x00021E7E File Offset: 0x0002007E
			private void ReadDeltaLinesAndColumns(out int deltaLines, out int deltaColumns)
			{
				deltaLines = this._reader.ReadCompressedInteger();
				deltaColumns = ((deltaLines == 0) ? this._reader.ReadCompressedInteger() : this._reader.ReadCompressedSignedInteger());
			}

			// Token: 0x06000C1D RID: 3101 RVA: 0x00021EAB File Offset: 0x000200AB
			private int ReadLine()
			{
				return this._reader.ReadCompressedInteger();
			}

			// Token: 0x06000C1E RID: 3102 RVA: 0x00021EB8 File Offset: 0x000200B8
			private ushort ReadColumn()
			{
				int num = this._reader.ReadCompressedInteger();
				if (num > 65535)
				{
					Throw.SequencePointValueOutOfRange();
				}
				return (ushort)num;
			}

			// Token: 0x06000C1F RID: 3103 RVA: 0x00021ED4 File Offset: 0x000200D4
			private int AddOffsets(int value, int delta)
			{
				int num = value + delta;
				if (num < 0 || num > 2147483647)
				{
					Throw.SequencePointValueOutOfRange();
				}
				return num;
			}

			// Token: 0x06000C20 RID: 3104 RVA: 0x00021EF8 File Offset: 0x000200F8
			private int AddLines(int value, int delta)
			{
				int num = value + delta;
				if (num < 0 || num >= 16707566)
				{
					Throw.SequencePointValueOutOfRange();
				}
				return num;
			}

			// Token: 0x06000C21 RID: 3105 RVA: 0x00021F1C File Offset: 0x0002011C
			private ushort AddColumns(ushort value, int delta)
			{
				int num = (int)value + delta;
				if (num < 0 || num >= 65535)
				{
					Throw.SequencePointValueOutOfRange();
				}
				return (ushort)num;
			}

			// Token: 0x06000C22 RID: 3106 RVA: 0x00021F40 File Offset: 0x00020140
			private DocumentHandle ReadDocumentHandle()
			{
				int num = this._reader.ReadCompressedInteger();
				if (num == 0 || !TokenTypeIds.IsValidRowId(num))
				{
					Throw.InvalidHandle();
				}
				return DocumentHandle.FromRowId(num);
			}

			// Token: 0x170002FB RID: 763
			// (get) Token: 0x06000C23 RID: 3107 RVA: 0x00021F6F File Offset: 0x0002016F
			public SequencePoint Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x170002FC RID: 764
			// (get) Token: 0x06000C24 RID: 3108 RVA: 0x00021F77 File Offset: 0x00020177
			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x06000C25 RID: 3109 RVA: 0x00021F84 File Offset: 0x00020184
			public void Reset()
			{
				this._reader.SeekOffset(0);
				this._current = default(SequencePoint);
			}

			// Token: 0x06000C26 RID: 3110 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A23 RID: 2595
			private BlobReader _reader;

			// Token: 0x04000A24 RID: 2596
			private SequencePoint _current;

			// Token: 0x04000A25 RID: 2597
			private int _previousNonHiddenStartLine;

			// Token: 0x04000A26 RID: 2598
			private ushort _previousNonHiddenStartColumn;
		}
	}
}
