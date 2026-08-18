using System;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x0200038E RID: 910
	public sealed class SnapLine
	{
		// Token: 0x06002534 RID: 9524 RVA: 0x000E8C94 File Offset: 0x000E6E94
		public SnapLine(SnapLineType type, int offset) : this(type, offset, null, SnapLinePriority.Low)
		{
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x000E8CA0 File Offset: 0x000E6EA0
		public SnapLine(SnapLineType type, int offset, string filter) : this(type, offset, filter, SnapLinePriority.Low)
		{
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x000E8CAC File Offset: 0x000E6EAC
		public SnapLine(SnapLineType type, int offset, SnapLinePriority priority) : this(type, offset, null, priority)
		{
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x000E8CB8 File Offset: 0x000E6EB8
		public SnapLine(SnapLineType type, int offset, string filter, SnapLinePriority priority)
		{
			this.type = type;
			this.offset = offset;
			this.filter = filter;
			this.priority = priority;
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06002538 RID: 9528 RVA: 0x000E8CDD File Offset: 0x000E6EDD
		public string Filter
		{
			get
			{
				return this.filter;
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06002539 RID: 9529 RVA: 0x000E8CE5 File Offset: 0x000E6EE5
		public bool IsHorizontal
		{
			get
			{
				return this.type == SnapLineType.Top || this.type == SnapLineType.Bottom || this.type == SnapLineType.Horizontal || this.type == SnapLineType.Baseline;
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x0600253A RID: 9530 RVA: 0x000E8D0C File Offset: 0x000E6F0C
		public bool IsVertical
		{
			get
			{
				return this.type == SnapLineType.Left || this.type == SnapLineType.Right || this.type == SnapLineType.Vertical;
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x0600253B RID: 9531 RVA: 0x000E8D2B File Offset: 0x000E6F2B
		public int Offset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x0600253C RID: 9532 RVA: 0x000E8D33 File Offset: 0x000E6F33
		public SnapLinePriority Priority
		{
			get
			{
				return this.priority;
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x0600253D RID: 9533 RVA: 0x000E8D3B File Offset: 0x000E6F3B
		public SnapLineType SnapLineType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x000E8D43 File Offset: 0x000E6F43
		public void AdjustOffset(int adjustment)
		{
			this.offset += adjustment;
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x000E8D54 File Offset: 0x000E6F54
		public static bool ShouldSnap(SnapLine line1, SnapLine line2)
		{
			if (line1.SnapLineType != line2.SnapLineType)
			{
				return false;
			}
			if (line1.Filter == null && line2.Filter == null)
			{
				return true;
			}
			if (line1.Filter == null || line2.Filter == null)
			{
				return false;
			}
			if (line1.Filter.Contains("Margin"))
			{
				return (line1.Filter.Equals("Margin.Right") && (line2.Filter.Equals("Margin.Left") || line2.Filter.Equals("Padding.Right"))) || (line1.Filter.Equals("Margin.Left") && (line2.Filter.Equals("Margin.Right") || line2.Filter.Equals("Padding.Left"))) || (line1.Filter.Equals("Margin.Top") && (line2.Filter.Equals("Margin.Bottom") || line2.Filter.Equals("Padding.Top"))) || (line1.Filter.Equals("Margin.Bottom") && line2.Filter.Equals("Margin.Top")) || line2.Filter.Equals("Padding.Bottom");
			}
			if (line1.Filter.Contains("Padding"))
			{
				return (line1.Filter.Equals("Padding.Left") && line2.Filter.Equals("Margin.Left")) || (line1.Filter.Equals("Padding.Right") && line2.Filter.Equals("Margin.Right")) || (line1.Filter.Equals("Padding.Top") && line2.Filter.Equals("Margin.Top")) || (line1.Filter.Equals("Padding.Bottom") && line2.Filter.Equals("Margin.Bottom"));
			}
			return line1.Filter.Equals(line2.Filter);
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x000E8F4C File Offset: 0x000E714C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"SnapLine: {type = ",
				this.type.ToString(),
				", offset  = ",
				this.offset.ToString(),
				", priority = ",
				this.priority.ToString(),
				", filter = ",
				(this.filter == null) ? "<null>" : this.filter,
				"}"
			});
		}

		// Token: 0x04001B0B RID: 6923
		private SnapLineType type;

		// Token: 0x04001B0C RID: 6924
		private SnapLinePriority priority;

		// Token: 0x04001B0D RID: 6925
		private int offset;

		// Token: 0x04001B0E RID: 6926
		private string filter;

		// Token: 0x04001B0F RID: 6927
		internal const string Margin = "Margin";

		// Token: 0x04001B10 RID: 6928
		internal const string MarginRight = "Margin.Right";

		// Token: 0x04001B11 RID: 6929
		internal const string MarginLeft = "Margin.Left";

		// Token: 0x04001B12 RID: 6930
		internal const string MarginBottom = "Margin.Bottom";

		// Token: 0x04001B13 RID: 6931
		internal const string MarginTop = "Margin.Top";

		// Token: 0x04001B14 RID: 6932
		internal const string Padding = "Padding";

		// Token: 0x04001B15 RID: 6933
		internal const string PaddingRight = "Padding.Right";

		// Token: 0x04001B16 RID: 6934
		internal const string PaddingLeft = "Padding.Left";

		// Token: 0x04001B17 RID: 6935
		internal const string PaddingBottom = "Padding.Bottom";

		// Token: 0x04001B18 RID: 6936
		internal const string PaddingTop = "Padding.Top";
	}
}
