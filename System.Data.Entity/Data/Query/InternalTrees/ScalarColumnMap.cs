using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;
using System.Globalization;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x0200009B RID: 155
	internal class ScalarColumnMap : SimpleColumnMap
	{
		// Token: 0x06000A06 RID: 2566 RVA: 0x00035DFB File Offset: 0x00033FFB
		internal ScalarColumnMap(TypeUsage type, string name, int commandId, int columnPos) : base(type, name)
		{
			this.m_commandId = commandId;
			this.m_columnPos = columnPos;
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x00035E14 File Offset: 0x00034014
		internal int CommandId
		{
			get
			{
				return this.m_commandId;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x00035E1C File Offset: 0x0003401C
		internal int ColumnPos
		{
			get
			{
				return this.m_columnPos;
			}
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00035E24 File Offset: 0x00034024
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00035E2E File Offset: 0x0003402E
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00035E38 File Offset: 0x00034038
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "S({0},{1})", new object[]
			{
				this.CommandId,
				this.ColumnPos
			});
		}

		// Token: 0x040008B3 RID: 2227
		private int m_commandId;

		// Token: 0x040008B4 RID: 2228
		private int m_columnPos;
	}
}
