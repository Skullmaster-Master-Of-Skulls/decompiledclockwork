using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000608 RID: 1544
	internal sealed class NewRecordOp : ScalarOp
	{
		// Token: 0x06003CC7 RID: 15559 RVA: 0x001196F6 File Offset: 0x001178F6
		internal NewRecordOp(TypeUsage type) : base(OpType.NewRecord, type)
		{
			this.m_fields = new List<EdmProperty>(TypeHelpers.GetEdmType<RowType>(type).Properties);
		}

		// Token: 0x06003CC8 RID: 15560 RVA: 0x00119717 File Offset: 0x00117917
		internal NewRecordOp(TypeUsage type, List<EdmProperty> fields) : base(OpType.NewRecord, type)
		{
			this.m_fields = fields;
		}

		// Token: 0x06003CC9 RID: 15561 RVA: 0x00119729 File Offset: 0x00117929
		private NewRecordOp() : base(OpType.NewRecord)
		{
		}

		// Token: 0x06003CCA RID: 15562 RVA: 0x00119734 File Offset: 0x00117934
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

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06003CCB RID: 15563 RVA: 0x0011976F File Offset: 0x0011796F
		internal List<EdmProperty> Properties
		{
			get
			{
				return this.m_fields;
			}
		}

		// Token: 0x06003CCC RID: 15564 RVA: 0x00119777 File Offset: 0x00117977
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003CCD RID: 15565 RVA: 0x00119781 File Offset: 0x00117981
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040016BD RID: 5821
		private readonly List<EdmProperty> m_fields;

		// Token: 0x040016BE RID: 5822
		internal static readonly NewRecordOp Pattern = new NewRecordOp();
	}
}
