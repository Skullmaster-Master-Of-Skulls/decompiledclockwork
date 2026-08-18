using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;

namespace System.Data.Mapping
{
	// Token: 0x02000238 RID: 568
	internal class ObjectTypeMapping : Map
	{
		// Token: 0x0600240A RID: 9226 RVA: 0x00082754 File Offset: 0x00080954
		internal ObjectTypeMapping(EdmType clrType, EdmType cdmType)
		{
			this.m_clrType = clrType;
			this.m_cdmType = cdmType;
			this.identity = clrType.Identity + ":" + cdmType.Identity;
			if (Helper.IsStructuralType(cdmType))
			{
				this.m_memberMapping = new Dictionary<string, ObjectMemberMapping>(((StructuralType)cdmType).Members.Count);
				return;
			}
			this.m_memberMapping = ObjectTypeMapping.EmptyMemberMapping;
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x0600240B RID: 9227 RVA: 0x000827C0 File Offset: 0x000809C0
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.MetadataItem;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x0600240C RID: 9228 RVA: 0x000827C4 File Offset: 0x000809C4
		internal EdmType ClrType
		{
			get
			{
				return this.m_clrType;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x0600240D RID: 9229 RVA: 0x000827CC File Offset: 0x000809CC
		internal override MetadataItem EdmItem
		{
			get
			{
				return this.EdmType;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x0600240E RID: 9230 RVA: 0x000827D4 File Offset: 0x000809D4
		internal EdmType EdmType
		{
			get
			{
				return this.m_cdmType;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x0600240F RID: 9231 RVA: 0x000827DC File Offset: 0x000809DC
		internal override string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x000827E4 File Offset: 0x000809E4
		internal ObjectPropertyMapping GetPropertyMap(string propertyName)
		{
			ObjectMemberMapping memberMap = this.GetMemberMap(propertyName, false);
			if ((memberMap != null && memberMap.MemberMappingKind == MemberMappingKind.ScalarPropertyMapping) || memberMap.MemberMappingKind == MemberMappingKind.ComplexPropertyMapping)
			{
				return (ObjectPropertyMapping)memberMap;
			}
			return null;
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x00082816 File Offset: 0x00080A16
		internal void AddMemberMap(ObjectMemberMapping memberMapping)
		{
			this.m_memberMapping.Add(memberMapping.EdmMember.Name, memberMapping);
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x0008282F File Offset: 0x00080A2F
		internal ObjectMemberMapping GetMemberMapForClrMember(string clrMemberName, bool ignoreCase)
		{
			return this.GetMemberMap(clrMemberName, ignoreCase);
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x0008283C File Offset: 0x00080A3C
		private ObjectMemberMapping GetMemberMap(string propertyName, bool ignoreCase)
		{
			EntityUtil.CheckStringArgument(propertyName, "propertyName");
			ObjectMemberMapping objectMemberMapping = null;
			if (!ignoreCase)
			{
				this.m_memberMapping.TryGetValue(propertyName, out objectMemberMapping);
			}
			else
			{
				foreach (KeyValuePair<string, ObjectMemberMapping> keyValuePair in this.m_memberMapping)
				{
					if (keyValuePair.Key.Equals(propertyName, StringComparison.OrdinalIgnoreCase))
					{
						if (objectMemberMapping != null)
						{
							throw new MappingException(Strings.Mapping_Duplicate_PropertyMap_CaseInsensitive(propertyName));
						}
						objectMemberMapping = keyValuePair.Value;
					}
				}
			}
			return objectMemberMapping;
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x0006E5B4 File Offset: 0x0006C7B4
		public override string ToString()
		{
			return this.Identity;
		}

		// Token: 0x04000FFA RID: 4090
		private readonly EdmType m_clrType;

		// Token: 0x04000FFB RID: 4091
		private readonly EdmType m_cdmType;

		// Token: 0x04000FFC RID: 4092
		private readonly string identity;

		// Token: 0x04000FFD RID: 4093
		private readonly Dictionary<string, ObjectMemberMapping> m_memberMapping;

		// Token: 0x04000FFE RID: 4094
		private static readonly Dictionary<string, ObjectMemberMapping> EmptyMemberMapping = new Dictionary<string, ObjectMemberMapping>(0);
	}
}
