using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005D0 RID: 1488
	internal abstract class ColumnMap
	{
		// Token: 0x06003B95 RID: 15253 RVA: 0x001183DB File Offset: 0x001165DB
		internal ColumnMap(TypeUsage type, string name)
		{
			this._type = type;
			this._name = name;
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06003B96 RID: 15254 RVA: 0x001183F1 File Offset: 0x001165F1
		// (set) Token: 0x06003B97 RID: 15255 RVA: 0x001183F9 File Offset: 0x001165F9
		internal TypeUsage Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06003B98 RID: 15256 RVA: 0x00118402 File Offset: 0x00116602
		// (set) Token: 0x06003B99 RID: 15257 RVA: 0x0011840A File Offset: 0x0011660A
		internal string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06003B9A RID: 15258 RVA: 0x00118413 File Offset: 0x00116613
		internal bool IsNamed
		{
			get
			{
				return this._name != null;
			}
		}

		// Token: 0x06003B9B RID: 15259
		[DebuggerNonUserCode]
		internal abstract void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg);

		// Token: 0x06003B9C RID: 15260
		[DebuggerNonUserCode]
		internal abstract TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg);

		// Token: 0x04001657 RID: 5719
		internal const string DefaultColumnName = "Value";

		// Token: 0x04001658 RID: 5720
		private TypeUsage _type;

		// Token: 0x04001659 RID: 5721
		private string _name;
	}
}
