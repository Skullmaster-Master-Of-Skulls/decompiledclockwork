using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004E4 RID: 1252
	internal struct FacetValueContainer<T>
	{
		// Token: 0x170006D8 RID: 1752
		// (set) Token: 0x06002E8E RID: 11918 RVA: 0x000DEF67 File Offset: 0x000DD167
		internal T Value
		{
			set
			{
				this._isUnbounded = false;
				this._hasValue = true;
				this._value = value;
			}
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x000DEF7E File Offset: 0x000DD17E
		private void SetUnbounded()
		{
			this._isUnbounded = true;
			this._hasValue = true;
		}

		// Token: 0x06002E90 RID: 11920 RVA: 0x000DEF90 File Offset: 0x000DD190
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "unbounded")]
		public static implicit operator FacetValueContainer<T>(EdmConstants.Unbounded unbounded)
		{
			FacetValueContainer<T> result = default(FacetValueContainer<T>);
			result.SetUnbounded();
			return result;
		}

		// Token: 0x06002E91 RID: 11921 RVA: 0x000DEFB0 File Offset: 0x000DD1B0
		public static implicit operator FacetValueContainer<T>(T value)
		{
			return new FacetValueContainer<T>
			{
				Value = value
			};
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x000DEFCE File Offset: 0x000DD1CE
		internal object GetValueAsObject()
		{
			if (this._isUnbounded)
			{
				return EdmConstants.UnboundedValue;
			}
			return this._value;
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06002E93 RID: 11923 RVA: 0x000DEFE9 File Offset: 0x000DD1E9
		internal bool HasValue
		{
			get
			{
				return this._hasValue;
			}
		}

		// Token: 0x040011B6 RID: 4534
		private T _value;

		// Token: 0x040011B7 RID: 4535
		private bool _hasValue;

		// Token: 0x040011B8 RID: 4536
		private bool _isUnbounded;
	}
}
