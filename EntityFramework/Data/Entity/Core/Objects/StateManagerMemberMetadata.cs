using System;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020005C0 RID: 1472
	internal class StateManagerMemberMetadata
	{
		// Token: 0x06003AF5 RID: 15093 RVA: 0x00117C20 File Offset: 0x00115E20
		internal StateManagerMemberMetadata()
		{
		}

		// Token: 0x06003AF6 RID: 15094 RVA: 0x00117C28 File Offset: 0x00115E28
		internal StateManagerMemberMetadata(ObjectPropertyMapping memberMap, EdmProperty memberMetadata, bool isPartOfKey)
		{
			this._clrProperty = memberMap.ClrProperty;
			this._edmProperty = memberMetadata;
			this._isPartOfKey = isPartOfKey;
			this._isComplexType = (Helper.IsEntityType(this._edmProperty.TypeUsage.EdmType) || Helper.IsComplexType(this._edmProperty.TypeUsage.EdmType));
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06003AF7 RID: 15095 RVA: 0x00117C8A File Offset: 0x00115E8A
		internal string CLayerName
		{
			get
			{
				return this._edmProperty.Name;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06003AF8 RID: 15096 RVA: 0x00117C97 File Offset: 0x00115E97
		internal Type ClrType
		{
			get
			{
				return this._clrProperty.TypeUsage.EdmType.ClrType;
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06003AF9 RID: 15097 RVA: 0x00117CAE File Offset: 0x00115EAE
		internal virtual bool IsComplex
		{
			get
			{
				return this._isComplexType;
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06003AFA RID: 15098 RVA: 0x00117CB6 File Offset: 0x00115EB6
		internal virtual EdmProperty CdmMetadata
		{
			get
			{
				return this._edmProperty;
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06003AFB RID: 15099 RVA: 0x00117CBE File Offset: 0x00115EBE
		internal EdmProperty ClrMetadata
		{
			get
			{
				return this._clrProperty;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06003AFC RID: 15100 RVA: 0x00117CC6 File Offset: 0x00115EC6
		internal bool IsPartOfKey
		{
			get
			{
				return this._isPartOfKey;
			}
		}

		// Token: 0x06003AFD RID: 15101 RVA: 0x00117CD0 File Offset: 0x00115ED0
		public virtual object GetValue(object userObject)
		{
			return DelegateFactory.GetValue(this._clrProperty, userObject);
		}

		// Token: 0x06003AFE RID: 15102 RVA: 0x00117CEB File Offset: 0x00115EEB
		public void SetValue(object userObject, object value)
		{
			if (DBNull.Value == value)
			{
				value = null;
			}
			if (this.IsComplex && value == null)
			{
				throw new InvalidOperationException(Strings.ComplexObject_NullableComplexTypesNotSupported(this.CLayerName));
			}
			DelegateFactory.SetValue(this._clrProperty, userObject, value);
		}

		// Token: 0x04001648 RID: 5704
		private readonly EdmProperty _clrProperty;

		// Token: 0x04001649 RID: 5705
		private readonly EdmProperty _edmProperty;

		// Token: 0x0400164A RID: 5706
		private readonly bool _isPartOfKey;

		// Token: 0x0400164B RID: 5707
		private readonly bool _isComplexType;
	}
}
