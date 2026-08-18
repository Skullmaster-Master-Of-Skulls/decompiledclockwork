using System;
using System.Diagnostics;
using System.Reflection.Internal;

namespace System.Reflection.Metadata
{
	// Token: 0x020000A4 RID: 164
	[DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
	public struct SequencePoint : IEquatable<SequencePoint>
	{
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x0000FB48 File Offset: 0x0000DD48
		public DocumentHandle Document
		{
			get
			{
				return this._document;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x0000FB50 File Offset: 0x0000DD50
		public int Offset
		{
			get
			{
				return this._offset;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x0000FB58 File Offset: 0x0000DD58
		public int StartLine
		{
			get
			{
				return this._startLine;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x0000FB60 File Offset: 0x0000DD60
		public int EndLine
		{
			get
			{
				return this._endLine;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x0000FB68 File Offset: 0x0000DD68
		public int StartColumn
		{
			get
			{
				return (int)this._startColumn;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0000FB70 File Offset: 0x0000DD70
		public int EndColumn
		{
			get
			{
				return (int)this._endColumn;
			}
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0000FB78 File Offset: 0x0000DD78
		internal SequencePoint(DocumentHandle document, int offset)
		{
			this._document = document;
			this._offset = offset;
			this._startLine = 16707566;
			this._startColumn = 0;
			this._endLine = 16707566;
			this._endColumn = 0;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0000FBAC File Offset: 0x0000DDAC
		internal SequencePoint(DocumentHandle document, int offset, int startLine, ushort startColumn, int endLine, ushort endColumn)
		{
			this._document = document;
			this._offset = offset;
			this._startLine = startLine;
			this._startColumn = startColumn;
			this._endLine = endLine;
			this._endColumn = endColumn;
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0000FBDC File Offset: 0x0000DDDC
		public override int GetHashCode()
		{
			return Hash.Combine(this._document.RowId, Hash.Combine(this._offset, Hash.Combine(this._startLine, Hash.Combine((int)this._startColumn, Hash.Combine(this._endLine, (int)this._endColumn)))));
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0000FC2B File Offset: 0x0000DE2B
		public override bool Equals(object obj)
		{
			return obj is SequencePoint && this.Equals((SequencePoint)obj);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0000FC44 File Offset: 0x0000DE44
		public bool Equals(SequencePoint other)
		{
			return this._document == other._document && this._offset == other._offset && this._startLine == other._startLine && this._startColumn == other._startColumn && this._endLine == other._endLine && this._endColumn == other._endColumn;
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0000FCAC File Offset: 0x0000DEAC
		public bool IsHidden
		{
			get
			{
				return this._startLine == 16707566;
			}
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0000FCBC File Offset: 0x0000DEBC
		private string GetDebuggerDisplay()
		{
			if (!this.IsHidden)
			{
				return string.Format("{0}: ({1}, {2}) - ({3}, {4})", new object[]
				{
					this._offset,
					this._startLine,
					this._startColumn,
					this._endLine,
					this._endColumn
				});
			}
			return "<hidden>";
		}

		// Token: 0x04000418 RID: 1048
		public const int HiddenLine = 16707566;

		// Token: 0x04000419 RID: 1049
		private DocumentHandle _document;

		// Token: 0x0400041A RID: 1050
		private int _offset;

		// Token: 0x0400041B RID: 1051
		private int _startLine;

		// Token: 0x0400041C RID: 1052
		private int _endLine;

		// Token: 0x0400041D RID: 1053
		private ushort _startColumn;

		// Token: 0x0400041E RID: 1054
		private ushort _endColumn;
	}
}
