using System;
using System.Data.Mapping;
using System.Data.Metadata.Edm;

namespace System.Data.Objects
{
	// Token: 0x0200014B RID: 331
	internal sealed class StateManagerMemberMetadata
	{
		// Token: 0x06001847 RID: 6215 RVA: 0x00053540 File Offset: 0x00051740
		internal StateManagerMemberMetadata(ObjectPropertyMapping memberMap, EdmProperty memberMetadata, bool isPartOfKey)
		{
			this._clrProperty = memberMap.ClrProperty;
			this._edmProperty = memberMetadata;
			this._isPartOfKey = isPartOfKey;
			this._isComplexType = (Helper.IsEntityType(this._edmProperty.TypeUsage.EdmType) || Helper.IsComplexType(this._edmProperty.TypeUsage.EdmType));
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001848 RID: 6216 RVA: 0x000535A2 File Offset: 0x000517A2
		internal string CLayerName
		{
			get
			{
				return this._edmProperty.Name;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001849 RID: 6217 RVA: 0x000535AF File Offset: 0x000517AF
		internal Type ClrType
		{
			get
			{
				return this._clrProperty.TypeUsage.EdmType.ClrType;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x0600184A RID: 6218 RVA: 0x000535C6 File Offset: 0x000517C6
		internal bool IsComplex
		{
			get
			{
				return this._isComplexType;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x000535CE File Offset: 0x000517CE
		internal EdmProperty CdmMetadata
		{
			get
			{
				return this._edmProperty;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x0600184C RID: 6220 RVA: 0x000535D6 File Offset: 0x000517D6
		internal EdmProperty ClrMetadata
		{
			get
			{
				return this._clrProperty;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x0600184D RID: 6221 RVA: 0x000535DE File Offset: 0x000517DE
		internal bool IsPartOfKey
		{
			get
			{
				return this._isPartOfKey;
			}
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x000535E8 File Offset: 0x000517E8
		public object GetValue(object userObject)
		{
			return LightweightCodeGenerator.GetValue(this._clrProperty, userObject);
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x00053603 File Offset: 0x00051803
		public void SetValue(object userObject, object value)
		{
			if (DBNull.Value == value)
			{
				value = null;
			}
			if (this.IsComplex && value == null)
			{
				throw EntityUtil.NullableComplexTypesNotSupported(this.CLayerName);
			}
			LightweightCodeGenerator.SetValue(this._clrProperty, userObject, value);
		}

		// Token: 0x04000AB9 RID: 2745
		private readonly EdmProperty _clrProperty;

		// Token: 0x04000ABA RID: 2746
		private readonly EdmProperty _edmProperty;

		// Token: 0x04000ABB RID: 2747
		private readonly bool _isPartOfKey;

		// Token: 0x04000ABC RID: 2748
		private readonly bool _isComplexType;
	}
}
