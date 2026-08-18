using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common
{
	// Token: 0x0200032D RID: 813
	public struct FieldMetadata
	{
		// Token: 0x06002FC4 RID: 12228 RVA: 0x000B4882 File Offset: 0x000B2A82
		public FieldMetadata(int ordinal, EdmMember fieldType)
		{
			if (ordinal < 0)
			{
				throw EntityUtil.ArgumentOutOfRange("ordinal");
			}
			if (fieldType == null)
			{
				throw EntityUtil.ArgumentNull("fieldType");
			}
			this._fieldType = fieldType;
			this._ordinal = ordinal;
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06002FC5 RID: 12229 RVA: 0x000B48AF File Offset: 0x000B2AAF
		public EdmMember FieldType
		{
			get
			{
				return this._fieldType;
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06002FC6 RID: 12230 RVA: 0x000B48B7 File Offset: 0x000B2AB7
		public int Ordinal
		{
			get
			{
				return this._ordinal;
			}
		}

		// Token: 0x04001480 RID: 5248
		private readonly EdmMember _fieldType;

		// Token: 0x04001481 RID: 5249
		private readonly int _ordinal;
	}
}
