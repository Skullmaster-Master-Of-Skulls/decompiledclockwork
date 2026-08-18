using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005D5 RID: 1493
	internal abstract class Var
	{
		// Token: 0x06003BB4 RID: 15284 RVA: 0x0011856B File Offset: 0x0011676B
		internal Var(int id, VarType varType, TypeUsage type)
		{
			this._id = id;
			this._varType = varType;
			this._type = type;
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06003BB5 RID: 15285 RVA: 0x00118588 File Offset: 0x00116788
		internal int Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06003BB6 RID: 15286 RVA: 0x00118590 File Offset: 0x00116790
		internal VarType VarType
		{
			get
			{
				return this._varType;
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06003BB7 RID: 15287 RVA: 0x00118598 File Offset: 0x00116798
		internal TypeUsage Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x06003BB8 RID: 15288 RVA: 0x001185A0 File Offset: 0x001167A0
		internal virtual bool TryGetName(out string name)
		{
			name = null;
			return false;
		}

		// Token: 0x06003BB9 RID: 15289 RVA: 0x001185A8 File Offset: 0x001167A8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
			{
				this.Id
			});
		}

		// Token: 0x04001667 RID: 5735
		private readonly int _id;

		// Token: 0x04001668 RID: 5736
		private readonly VarType _varType;

		// Token: 0x04001669 RID: 5737
		private readonly TypeUsage _type;
	}
}
