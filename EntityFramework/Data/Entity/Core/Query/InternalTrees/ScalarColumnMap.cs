using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Globalization;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200061E RID: 1566
	internal class ScalarColumnMap : SimpleColumnMap
	{
		// Token: 0x06003D44 RID: 15684 RVA: 0x0011AF81 File Offset: 0x00119181
		internal ScalarColumnMap(TypeUsage type, string name, int commandId, int columnPos) : base(type, name)
		{
			this.m_commandId = commandId;
			this.m_columnPos = columnPos;
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06003D45 RID: 15685 RVA: 0x0011AF9A File Offset: 0x0011919A
		internal int CommandId
		{
			get
			{
				return this.m_commandId;
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06003D46 RID: 15686 RVA: 0x0011AFA2 File Offset: 0x001191A2
		internal int ColumnPos
		{
			get
			{
				return this.m_columnPos;
			}
		}

		// Token: 0x06003D47 RID: 15687 RVA: 0x0011AFAA File Offset: 0x001191AA
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06003D48 RID: 15688 RVA: 0x0011AFB4 File Offset: 0x001191B4
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x06003D49 RID: 15689 RVA: 0x0011AFC0 File Offset: 0x001191C0
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "S({0},{1})", new object[]
			{
				this.CommandId,
				this.ColumnPos
			});
		}

		// Token: 0x04001729 RID: 5929
		private readonly int m_commandId;

		// Token: 0x0400172A RID: 5930
		private readonly int m_columnPos;
	}
}
