using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Metadata.Edm;
using System.Text;

namespace System.Data.Mapping
{
	// Token: 0x0200023C RID: 572
	internal class StorageComplexTypeMapping
	{
		// Token: 0x06002424 RID: 9252 RVA: 0x00082BFC File Offset: 0x00080DFC
		internal StorageComplexTypeMapping(bool isPartial)
		{
			this.m_isPartial = isPartial;
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06002425 RID: 9253 RVA: 0x00082C56 File Offset: 0x00080E56
		internal ReadOnlyCollection<ComplexType> Types
		{
			get
			{
				return new List<ComplexType>(this.m_types.Values).AsReadOnly();
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06002426 RID: 9254 RVA: 0x00082C6D File Offset: 0x00080E6D
		internal ReadOnlyCollection<ComplexType> IsOfTypes
		{
			get
			{
				return new List<ComplexType>(this.m_isOfTypes.Values).AsReadOnly();
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06002427 RID: 9255 RVA: 0x00082C84 File Offset: 0x00080E84
		internal ReadOnlyCollection<StoragePropertyMapping> Properties
		{
			get
			{
				return new List<StoragePropertyMapping>(this.m_properties.Values).AsReadOnly();
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06002428 RID: 9256 RVA: 0x00082C9C File Offset: 0x00080E9C
		internal ReadOnlyCollection<StoragePropertyMapping> AllProperties
		{
			get
			{
				List<StoragePropertyMapping> list = new List<StoragePropertyMapping>();
				list.AddRange(this.m_properties.Values);
				list.AddRange(this.m_conditionProperties.Values);
				return list.AsReadOnly();
			}
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x00082CD7 File Offset: 0x00080ED7
		internal void AddType(ComplexType type)
		{
			this.m_types.Add(type.FullName, type);
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x00082CEB File Offset: 0x00080EEB
		internal void AddIsOfType(ComplexType type)
		{
			this.m_isOfTypes.Add(type.FullName, type);
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x00082CFF File Offset: 0x00080EFF
		internal void AddProperty(StoragePropertyMapping prop)
		{
			this.m_properties.Add(prop.EdmProperty.Name, prop);
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x00082D18 File Offset: 0x00080F18
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

		// Token: 0x0600242D RID: 9261 RVA: 0x00082D60 File Offset: 0x00080F60
		internal ComplexType GetOwnerType(string memberName)
		{
			foreach (ComplexType complexType in this.m_types.Values)
			{
				EdmMember edmMember;
				if (complexType.Members.TryGetValue(memberName, false, out edmMember) && edmMember is EdmProperty)
				{
					return complexType;
				}
			}
			foreach (ComplexType complexType2 in this.m_isOfTypes.Values)
			{
				EdmMember edmMember2;
				if (complexType2.Members.TryGetValue(memberName, false, out edmMember2) && edmMember2 is EdmProperty)
				{
					return complexType2;
				}
			}
			return null;
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x00082E34 File Offset: 0x00081034
		internal void Print(int index)
		{
			StorageEntityContainerMapping.GetPrettyPrintString(ref index);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ComplexTypeMapping");
			stringBuilder.Append("   ");
			if (this.m_isPartial)
			{
				stringBuilder.Append("IsPartial:True");
			}
			stringBuilder.Append("   ");
			foreach (ComplexType complexType in this.m_types.Values)
			{
				stringBuilder.Append("Types:");
				stringBuilder.Append(complexType.FullName);
				stringBuilder.Append("   ");
			}
			foreach (ComplexType complexType2 in this.m_isOfTypes.Values)
			{
				stringBuilder.Append("Is-Of Types:");
				stringBuilder.Append(complexType2.FullName);
				stringBuilder.Append("   ");
			}
			Console.WriteLine(stringBuilder.ToString());
			foreach (StoragePropertyMapping storagePropertyMapping in this.m_conditionProperties.Values)
			{
				StorageConditionPropertyMapping storageConditionPropertyMapping = (StorageConditionPropertyMapping)storagePropertyMapping;
				storageConditionPropertyMapping.Print(index + 5);
			}
			foreach (StoragePropertyMapping storagePropertyMapping2 in this.Properties)
			{
				storagePropertyMapping2.Print(index + 5);
			}
		}

		// Token: 0x04001002 RID: 4098
		private Dictionary<string, StoragePropertyMapping> m_properties = new Dictionary<string, StoragePropertyMapping>(StringComparer.Ordinal);

		// Token: 0x04001003 RID: 4099
		private Dictionary<EdmProperty, StoragePropertyMapping> m_conditionProperties = new Dictionary<EdmProperty, StoragePropertyMapping>(EqualityComparer<EdmProperty>.Default);

		// Token: 0x04001004 RID: 4100
		private bool m_isPartial;

		// Token: 0x04001005 RID: 4101
		private Dictionary<string, ComplexType> m_types = new Dictionary<string, ComplexType>(StringComparer.Ordinal);

		// Token: 0x04001006 RID: 4102
		private Dictionary<string, ComplexType> m_isOfTypes = new Dictionary<string, ComplexType>(StringComparer.Ordinal);
	}
}
