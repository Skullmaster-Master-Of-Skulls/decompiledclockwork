using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common
{
	// Token: 0x02000206 RID: 518
	public class DataRecordInfo
	{
		// Token: 0x060012B7 RID: 4791 RVA: 0x0004E8C4 File Offset: 0x0004CAC4
		internal DataRecordInfo()
		{
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x0004E8CC File Offset: 0x0004CACC
		public DataRecordInfo(TypeUsage metadata, IEnumerable<EdmMember> memberInfo)
		{
			Check.NotNull<TypeUsage>(metadata, "metadata");
			IBaseList<EdmMember> allStructuralMembers = TypeHelpers.GetAllStructuralMembers(metadata.EdmType);
			List<FieldMetadata> list = new List<FieldMetadata>(allStructuralMembers.Count);
			if (memberInfo != null)
			{
				foreach (EdmMember edmMember in memberInfo)
				{
					if (edmMember == null || 0 > allStructuralMembers.IndexOf(edmMember) || (BuiltInTypeKind.EdmProperty != edmMember.BuiltInTypeKind && edmMember.BuiltInTypeKind != BuiltInTypeKind.AssociationEndMember))
					{
						throw Error.InvalidEdmMemberInstance();
					}
					if (edmMember.DeclaringType != metadata.EdmType && !edmMember.DeclaringType.IsBaseTypeOf(metadata.EdmType))
					{
						throw new ArgumentException(Strings.EdmMembersDefiningTypeDoNotAgreeWithMetadataType);
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
			throw Error.InvalidEdmMemberInstance();
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x0004E9D4 File Offset: 0x0004CBD4
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

		// Token: 0x060012BA RID: 4794 RVA: 0x0004EA36 File Offset: 0x0004CC36
		internal DataRecordInfo(DataRecordInfo recordInfo)
		{
			this._fieldMetadata = recordInfo._fieldMetadata;
			this._metadata = recordInfo._metadata;
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060012BB RID: 4795 RVA: 0x0004EA56 File Offset: 0x0004CC56
		public ReadOnlyCollection<FieldMetadata> FieldMetadata
		{
			get
			{
				return this._fieldMetadata;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x0004EA5E File Offset: 0x0004CC5E
		public virtual TypeUsage RecordType
		{
			get
			{
				return this._metadata;
			}
		}

		// Token: 0x04000573 RID: 1395
		private readonly ReadOnlyCollection<FieldMetadata> _fieldMetadata;

		// Token: 0x04000574 RID: 1396
		private readonly TypeUsage _metadata;
	}
}
