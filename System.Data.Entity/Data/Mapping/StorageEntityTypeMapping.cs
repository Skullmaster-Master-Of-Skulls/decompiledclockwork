using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Metadata.Edm;
using System.Text;

namespace System.Data.Mapping
{
	// Token: 0x02000243 RID: 579
	internal class StorageEntityTypeMapping : StorageTypeMapping
	{
		// Token: 0x0600246D RID: 9325 RVA: 0x00083C38 File Offset: 0x00081E38
		internal StorageEntityTypeMapping(StorageSetMapping setMapping) : base(setMapping)
		{
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x0600246E RID: 9326 RVA: 0x00083C61 File Offset: 0x00081E61
		internal override ReadOnlyCollection<EdmType> Types
		{
			get
			{
				return new List<EdmType>(this.m_entityTypes.Values).AsReadOnly();
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x0600246F RID: 9327 RVA: 0x00083C78 File Offset: 0x00081E78
		internal override ReadOnlyCollection<EdmType> IsOfTypes
		{
			get
			{
				return new List<EdmType>(this.m_isOfEntityTypes.Values).AsReadOnly();
			}
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x00083C8F File Offset: 0x00081E8F
		internal void AddType(EdmType type)
		{
			this.m_entityTypes.Add(type.FullName, type);
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x00083CA3 File Offset: 0x00081EA3
		internal void AddIsOfType(EdmType type)
		{
			this.m_isOfEntityTypes.Add(type.FullName, type);
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x00083CB8 File Offset: 0x00081EB8
		internal EntityType GetContainerType(string memberName)
		{
			foreach (EdmType edmType in this.m_entityTypes.Values)
			{
				EntityType entityType = (EntityType)edmType;
				if (entityType.Properties.Contains(memberName))
				{
					return entityType;
				}
			}
			foreach (EdmType edmType2 in this.m_isOfEntityTypes.Values)
			{
				EntityType entityType2 = (EntityType)edmType2;
				if (entityType2.Properties.Contains(memberName))
				{
					return entityType2;
				}
			}
			return null;
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x00083D80 File Offset: 0x00081F80
		internal override void Print(int index)
		{
			StorageEntityContainerMapping.GetPrettyPrintString(ref index);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("EntityTypeMapping");
			stringBuilder.Append("   ");
			foreach (EdmType edmType in this.m_entityTypes.Values)
			{
				stringBuilder.Append("Types:");
				stringBuilder.Append(edmType.FullName);
				stringBuilder.Append("   ");
			}
			foreach (EdmType edmType2 in this.m_isOfEntityTypes.Values)
			{
				stringBuilder.Append("Is-Of Types:");
				stringBuilder.Append(edmType2.FullName);
				stringBuilder.Append("   ");
			}
			Console.WriteLine(stringBuilder.ToString());
			foreach (StorageMappingFragment storageMappingFragment in base.MappingFragments)
			{
				storageMappingFragment.Print(index + 5);
			}
		}

		// Token: 0x04001022 RID: 4130
		private Dictionary<string, EdmType> m_entityTypes = new Dictionary<string, EdmType>(StringComparer.Ordinal);

		// Token: 0x04001023 RID: 4131
		private Dictionary<string, EdmType> m_isOfEntityTypes = new Dictionary<string, EdmType>(StringComparer.Ordinal);
	}
}
