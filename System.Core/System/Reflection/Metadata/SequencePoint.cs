using System;
using System.Diagnostics;
using System.Reflection.Internal;

namespace System.Reflection.Metadata
{
	// Token: 0x02000064 RID: 100
	[DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
	internal struct SequencePoint : IEquatable<SequencePoint>
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x000074E1 File Offset: 0x000056E1
		public DocumentHandle Document { get; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x000074E9 File Offset: 0x000056E9
		public int Offset { get; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x000074F1 File Offset: 0x000056F1
		public int StartLine { get; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x000074F9 File Offset: 0x000056F9
		public int EndLine { get; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00007501 File Offset: 0x00005701
		public int StartColumn { get; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00007509 File Offset: 0x00005709
		public int EndColumn { get; }

		// Token: 0x060002CC RID: 716 RVA: 0x00007511 File Offset: 0x00005711
		internal SequencePoint(DocumentHandle document, int offset)
		{
			this.Document = document;
			this.Offset = offset;
			this.StartLine = 16707566;
			this.StartColumn = 0;
			this.EndLine = 16707566;
			this.EndColumn = 0;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00007545 File Offset: 0x00005745
		internal SequencePoint(DocumentHandle document, int offset, int startLine, ushort startColumn, int endLine, ushort endColumn)
		{
			this.Document = document;
			this.Offset = offset;
			this.StartLine = startLine;
			this.StartColumn = startColumn;
			this.EndLine = endLine;
			this.EndColumn = endColumn;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00007574 File Offset: 0x00005774
		public override int GetHashCode()
		{
			return Hash.Combine(this.Document.RowId, Hash.Combine(this.Offset, Hash.Combine(this.StartLine, Hash.Combine(this.StartColumn, Hash.Combine(this.EndLine, this.EndColumn)))));
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000075C6 File Offset: 0x000057C6
		public override bool Equals(object obj)
		{
			return obj is SequencePoint && this.Equals((SequencePoint)obj);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000075E0 File Offset: 0x000057E0
		public bool Equals(SequencePoint other)
		{
			return this.Document == other.Document && this.Offset == other.Offset && this.StartLine == other.StartLine && this.StartColumn == other.StartColumn && this.EndLine == other.EndLine && this.EndColumn == other.EndColumn;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000764E File Offset: 0x0000584E
		public bool IsHidden
		{
			get
			{
				return this.StartLine == 16707566;
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00007660 File Offset: 0x00005860
		private string GetDebuggerDisplay()
		{
			if (!this.IsHidden)
			{
				return string.Format("{0}: ({1}, {2}) - ({3}, {4})", new object[]
				{
					this.Offset,
					this.StartLine,
					this.StartColumn,
					this.EndLine,
					this.EndColumn
				});
			}
			return "<hidden>";
		}

		// Token: 0x0400035A RID: 858
		public const int HiddenLine = 16707566;
	}
}
