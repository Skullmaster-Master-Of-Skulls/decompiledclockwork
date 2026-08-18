using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Metadata.Edm;
using System.Text;

namespace System.Data.Mapping
{
	// Token: 0x0200024B RID: 587
	internal class StorageMappingFragment
	{
		// Token: 0x06002483 RID: 9347 RVA: 0x0008440F File Offset: 0x0008260F
		internal StorageMappingFragment(EntitySet tableExtent, StorageTypeMapping typeMapping, bool distinctFlag)
		{
			this.m_tableExtent = tableExtent;
			this.m_typeMapping = typeMapping;
			this.m_isSQueryDistinct = distinctFlag;
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06002484 RID: 9348 RVA: 0x00084447 File Offset: 0x00082647
		internal EntitySet TableSet
		{
			get
			{
				return this.m_tableExtent;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06002485 RID: 9349 RVA: 0x0008444F File Offset: 0x0008264F
		internal bool IsSQueryDistinct
		{
			get
			{
				return this.m_isSQueryDistinct;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06002486 RID: 9350 RVA: 0x00084458 File Offset: 0x00082658
		internal ReadOnlyCollection<StoragePropertyMapping> AllProperties
		{
			get
			{
				List<StoragePropertyMapping> list = new List<StoragePropertyMapping>();
				list.AddRange(this.m_properties);
				list.AddRange(this.m_conditionProperties.Values);
				return list.AsReadOnly();
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06002487 RID: 9351 RVA: 0x0008448E File Offset: 0x0008268E
		internal ReadOnlyCollection<StoragePropertyMapping> Properties
		{
			get
			{
				return this.m_properties.AsReadOnly();
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06002488 RID: 9352 RVA: 0x0008449B File Offset: 0x0008269B
		// (set) Token: 0x06002489 RID: 9353 RVA: 0x000844A3 File Offset: 0x000826A3
		internal int StartLineNumber
		{
			get
			{
				return this.m_startLineNumber;
			}
			set
			{
				this.m_startLineNumber = value;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x0600248A RID: 9354 RVA: 0x000844AC File Offset: 0x000826AC
		// (set) Token: 0x0600248B RID: 9355 RVA: 0x000844B4 File Offset: 0x000826B4
		internal int StartLinePosition
		{
			get
			{
				return this.m_startLinePosition;
			}
			set
			{
				this.m_startLinePosition = value;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x0600248C RID: 9356 RVA: 0x000844BD File Offset: 0x000826BD
		internal string SourceLocation
		{
			get
			{
				return this.m_typeMapping.SetMapping.EntityContainerMapping.SourceLocation;
			}
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x000844D4 File Offset: 0x000826D4
		internal void AddProperty(StoragePropertyMapping prop)
		{
			this.m_properties.Add(prop);
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x000844E4 File Offset: 0x000826E4
		internal void AddConditionProperty(StorageConditionPropertyMapping conditionPropertyMap, Action<EdmMember> duplicateMemberConditionError)
		{
			EdmProperty edmProperty = (conditionPropertyMap.EdmProperty != null) ? conditionPropertyMap.EdmProperty : conditionPropertyMap.ColumnProperty;
			if (!this.m_conditionProperties.ContainsKey(edmProperty))
			{
				this.m_conditionProperties.Add(edmProperty, conditionPropertyMap);
				return;
			}
			duplicateMemberConditionError(edmProperty);
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x0008452C File Offset: 0x0008272C
		internal virtual void Print(int index)
		{
			StorageEntityContainerMapping.GetPrettyPrintString(ref index);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MappingFragment");
			stringBuilder.Append("   ");
			stringBuilder.Append("Table Name:");
			stringBuilder.Append(this.m_tableExtent.Name);
			Console.WriteLine(stringBuilder.ToString());
			foreach (StoragePropertyMapping storagePropertyMapping in this.m_conditionProperties.Values)
			{
				StorageConditionPropertyMapping storageConditionPropertyMapping = (StorageConditionPropertyMapping)storagePropertyMapping;
				storageConditionPropertyMapping.Print(index + 5);
			}
			foreach (StoragePropertyMapping storagePropertyMapping2 in this.m_properties)
			{
				storagePropertyMapping2.Print(index + 5);
			}
		}

		// Token: 0x040010A0 RID: 4256
		private EntitySet m_tableExtent;

		// Token: 0x040010A1 RID: 4257
		private StorageTypeMapping m_typeMapping;

		// Token: 0x040010A2 RID: 4258
		private Dictionary<EdmProperty, StoragePropertyMapping> m_conditionProperties = new Dictionary<EdmProperty, StoragePropertyMapping>(EqualityComparer<EdmProperty>.Default);

		// Token: 0x040010A3 RID: 4259
		private List<StoragePropertyMapping> m_properties = new List<StoragePropertyMapping>();

		// Token: 0x040010A4 RID: 4260
		private int m_startLineNumber;

		// Token: 0x040010A5 RID: 4261
		private int m_startLinePosition;

		// Token: 0x040010A6 RID: 4262
		private bool m_isSQueryDistinct;
	}
}
