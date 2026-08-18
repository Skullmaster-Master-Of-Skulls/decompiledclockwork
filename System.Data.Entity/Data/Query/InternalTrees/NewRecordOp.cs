using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x02000102 RID: 258
	internal sealed class NewRecordOp : ScalarOp
	{
		// Token: 0x06000D5E RID: 3422 RVA: 0x0003CE34 File Offset: 0x0003B034
		internal NewRecordOp(TypeUsage type) : base(OpType.NewRecord, type)
		{
			this.m_fields = new List<EdmProperty>(TypeHelpers.GetEdmType<RowType>(type).Properties);
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0003CE55 File Offset: 0x0003B055
		internal NewRecordOp(TypeUsage type, List<EdmProperty> fields) : base(OpType.NewRecord, type)
		{
			this.m_fields = fields;
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0003CE67 File Offset: 0x0003B067
		private NewRecordOp() : base(OpType.NewRecord)
		{
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0003CE74 File Offset: 0x0003B074
		internal bool GetFieldPosition(EdmProperty field, out int fieldPosition)
		{
			fieldPosition = 0;
			for (int i = 0; i < this.m_fields.Count; i++)
			{
				if (this.m_fields[i] == field)
				{
					fieldPosition = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x0003CEAF File Offset: 0x0003B0AF
		internal List<EdmProperty> Properties
		{
			get
			{
				return this.m_fields;
			}
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x0003CEB7 File Offset: 0x0003B0B7
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0003CEC1 File Offset: 0x0003B0C1
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040009C0 RID: 2496
		private List<EdmProperty> m_fields;

		// Token: 0x040009C1 RID: 2497
		internal static readonly NewRecordOp Pattern = new NewRecordOp();
	}
}
