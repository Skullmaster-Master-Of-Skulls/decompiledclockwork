using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Metadata.Edm;

namespace System.Data.Common
{
	// Token: 0x02000329 RID: 809
	public class DataRecordInfo
	{
		// Token: 0x06002F90 RID: 12176 RVA: 0x000B3EDC File Offset: 0x000B20DC
		public DataRecordInfo(TypeUsage metadata, IEnumerable<EdmMember> memberInfo)
		{
			EntityUtil.CheckArgumentNull<TypeUsage>(metadata, "metadata");
			IBaseList<EdmMember> allStructuralMembers = TypeHelpers.GetAllStructuralMembers(metadata.EdmType);
			List<FieldMetadata> list = new List<FieldMetadata>(allStructuralMembers.Count);
			if (memberInfo != null)
			{
				foreach (EdmMember edmMember in memberInfo)
				{
					if (edmMember == null || 0 > allStructuralMembers.IndexOf(edmMember) || (BuiltInTypeKind.EdmProperty != edmMember.BuiltInTypeKind && edmMember.BuiltInTypeKind != BuiltInTypeKind.AssociationEndMember))
					{
						throw EntityUtil.Argument("memberInfo");
					}
					if (edmMember.DeclaringType != metadata.EdmType && !edmMember.DeclaringType.IsBaseTypeOf(metadata.EdmType))
					{
						throw EntityUtil.Argument(Strings.EdmMembersDefiningTypeDoNotAgreeWithMetadataType);
					}
					list.Add(new FieldMetadata(list.Count, edmMember));
				}
			}
			if (Helper.IsStructuralType(metadata.EdmType) == 0 < list.Count)
			{
				this._fieldMetadata = new ReadOnlyCollection<FieldMetadata>(list);
				this._metadata = metadata;
				return;
			}
			throw EntityUtil.Argument("memberInfo");
		}

		// Token: 0x06002F91 RID: 12177 RVA: 0x000B3FEC File Offset: 0x000B21EC
		internal DataRecordInfo(TypeUsage metadata)
		{
			IBaseList<EdmMember> allStructuralMembers = TypeHelpers.GetAllStructuralMembers(metadata);
			FieldMetadata[] array = new FieldMetadata[allStructuralMembers.Count];
			for (int i = 0; i < array.Length; i++)
			{
				EdmMember fieldType = allStructuralMembers[i];
				array[i] = new FieldMetadata(i, fieldType);
			}
			this._fieldMetadata = new ReadOnlyCollection<FieldMetadata>(array);
			this._metadata = metadata;
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x000B4049 File Offset: 0x000B2249
		internal DataRecordInfo(DataRecordInfo recordInfo)
		{
			this._fieldMetadata = recordInfo._fieldMetadata;
			this._metadata = recordInfo._metadata;
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06002F93 RID: 12179 RVA: 0x000B4069 File Offset: 0x000B2269
		public ReadOnlyCollection<FieldMetadata> FieldMetadata
		{
			get
			{
				return this._fieldMetadata;
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06002F94 RID: 12180 RVA: 0x000B4071 File Offset: 0x000B2271
		public TypeUsage RecordType
		{
			get
			{
				return this._metadata;
			}
		}

		// Token: 0x0400147B RID: 5243
		private readonly ReadOnlyCollection<FieldMetadata> _fieldMetadata;

		// Token: 0x0400147C RID: 5244
		private readonly TypeUsage _metadata;
	}
}
