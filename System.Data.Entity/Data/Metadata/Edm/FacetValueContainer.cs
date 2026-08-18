using System;

namespace System.Data.Metadata.Edm
{
	// Token: 0x02000211 RID: 529
	internal struct FacetValueContainer<T>
	{
		// Token: 0x170006E5 RID: 1765
		// (set) Token: 0x060022F9 RID: 8953 RVA: 0x0007C5BC File Offset: 0x0007A7BC
		internal T Value
		{
			set
			{
				this._isUnbounded = false;
				this._hasValue = true;
				this._value = value;
			}
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x0007C5D3 File Offset: 0x0007A7D3
		private void SetUnbounded()
		{
			this._isUnbounded = true;
			this._hasValue = true;
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x0007C5E4 File Offset: 0x0007A7E4
		public static implicit operator FacetValueContainer<T>(EdmConstants.Unbounded unbounded)
		{
			FacetValueContainer<T> result = default(FacetValueContainer<T>);
			result.SetUnbounded();
			return result;
		}

		// Token: 0x060022FC RID: 8956 RVA: 0x0007C604 File Offset: 0x0007A804
		public static implicit operator FacetValueContainer<T>(T value)
		{
			return new FacetValueContainer<T>
			{
				Value = value
			};
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x0007C622 File Offset: 0x0007A822
		internal object GetValueAsObject()
		{
			if (this._isUnbounded)
			{
				return EdmConstants.UnboundedValue;
			}
			return this._value;
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x060022FE RID: 8958 RVA: 0x0007C63D File Offset: 0x0007A83D
		internal bool HasValue
		{
			get
			{
				return this._hasValue;
			}
		}

		// Token: 0x04000F8F RID: 3983
		private T _value;

		// Token: 0x04000F90 RID: 3984
		private bool _hasValue;

		// Token: 0x04000F91 RID: 3985
		private bool _isUnbounded;
	}
}
