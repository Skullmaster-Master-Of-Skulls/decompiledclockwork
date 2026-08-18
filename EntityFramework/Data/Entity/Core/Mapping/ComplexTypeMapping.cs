using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003D6 RID: 982
	public class ComplexTypeMapping : StructuralTypeMapping
	{
		// Token: 0x060023C2 RID: 9154 RVA: 0x000A5A2C File Offset: 0x000A3C2C
		public ComplexTypeMapping(ComplexType complexType)
		{
			Check.NotNull<ComplexType>(complexType, "complexType");
			this.AddType(complexType);
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x000A5A94 File Offset: 0x000A3C94
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "isPartial")]
		internal ComplexTypeMapping(bool isPartial)
		{
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060023C4 RID: 9156 RVA: 0x000A5AE7 File Offset: 0x000A3CE7
		public ComplexType ComplexType
		{
			get
			{
				return this.m_types.Values.SingleOrDefault<ComplexType>();
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060023C5 RID: 9157 RVA: 0x000A5AF9 File Offset: 0x000A3CF9
		internal ReadOnlyCollection<ComplexType> Types
		{
			get
			{
				return new ReadOnlyCollection<ComplexType>(new List<ComplexType>(this.m_types.Values));
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060023C6 RID: 9158 RVA: 0x000A5B10 File Offset: 0x000A3D10
		internal ReadOnlyCollection<ComplexType> IsOfTypes
		{
			get
			{
				return new ReadOnlyCollection<ComplexType>(new List<ComplexType>(this.m_isOfTypes.Values));
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060023C7 RID: 9159 RVA: 0x000A5B27 File Offset: 0x000A3D27
		public override ReadOnlyCollection<PropertyMapping> PropertyMappings
		{
			get
			{
				return new ReadOnlyCollection<PropertyMapping>(new List<PropertyMapping>(this.m_properties.Values));
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060023C8 RID: 9160 RVA: 0x000A5B3E File Offset: 0x000A3D3E
		public override ReadOnlyCollection<ConditionPropertyMapping> Conditions
		{
			get
			{
				return new ReadOnlyCollection<ConditionPropertyMapping>(new List<ConditionPropertyMapping>(this.m_conditionProperties.Values));
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060023C9 RID: 9161 RVA: 0x000A5B58 File Offset: 0x000A3D58
		internal ReadOnlyCollection<PropertyMapping> AllProperties
		{
			get
			{
				List<PropertyMapping> list = new List<PropertyMapping>();
				list.AddRange(this.m_properties.Values);
				list.AddRange(this.m_conditionProperties.Values);
				return new ReadOnlyCollection<PropertyMapping>(list);
			}
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x000A5B93 File Offset: 0x000A3D93
		internal void AddType(ComplexType type)
		{
			this.m_types.Add(type.FullName, type);
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x000A5BA7 File Offset: 0x000A3DA7
		internal void AddIsOfType(ComplexType type)
		{
			this.m_isOfTypes.Add(type.FullName, type);
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x000A5BBB File Offset: 0x000A3DBB
		public override void AddPropertyMapping(PropertyMapping propertyMapping)
		{
			Check.NotNull<PropertyMapping>(propertyMapping, "propertyMapping");
			base.ThrowIfReadOnly();
			this.m_properties.Add(propertyMapping.Property.Name, propertyMapping);
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x000A5BE6 File Offset: 0x000A3DE6
		public override void RemovePropertyMapping(PropertyMapping propertyMapping)
		{
			Check.NotNull<PropertyMapping>(propertyMapping, "propertyMapping");
			base.ThrowIfReadOnly();
			this.m_properties.Remove(propertyMapping.Property.Name);
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x000A5C13 File Offset: 0x000A3E13
		public override void AddCondition(ConditionPropertyMapping condition)
		{
			Check.NotNull<ConditionPropertyMapping>(condition, "condition");
			base.ThrowIfReadOnly();
			this.AddConditionProperty(condition, delegate(EdmMember _)
			{
			});
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x000A5C4B File Offset: 0x000A3E4B
		public override void RemoveCondition(ConditionPropertyMapping condition)
		{
			Check.NotNull<ConditionPropertyMapping>(condition, "condition");
			base.ThrowIfReadOnly();
			this.m_conditionProperties.Remove(condition.Property ?? condition.Column);
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x000A5C7B File Offset: 0x000A3E7B
		internal override void SetReadOnly()
		{
			MappingItem.SetReadOnly(this.m_properties.Values);
			MappingItem.SetReadOnly(this.m_conditionProperties.Values);
			base.SetReadOnly();
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x000A5CA4 File Offset: 0x000A3EA4
		internal void AddConditionProperty(ConditionPropertyMapping conditionPropertyMap, Action<EdmMember> duplicateMemberConditionError)
		{
			EdmProperty edmProperty = (conditionPropertyMap.Property != null) ? conditionPropertyMap.Property : conditionPropertyMap.Column;
			if (!this.m_conditionProperties.ContainsKey(edmProperty))
			{
				this.m_conditionProperties.Add(edmProperty, conditionPropertyMap);
				return;
			}
			duplicateMemberConditionError(edmProperty);
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x000A5CEC File Offset: 0x000A3EEC
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

		// Token: 0x04000C8E RID: 3214
		private readonly Dictionary<string, PropertyMapping> m_properties = new Dictionary<string, PropertyMapping>(StringComparer.Ordinal);

		// Token: 0x04000C8F RID: 3215
		private readonly Dictionary<EdmProperty, ConditionPropertyMapping> m_conditionProperties = new Dictionary<EdmProperty, ConditionPropertyMapping>(EqualityComparer<EdmProperty>.Default);

		// Token: 0x04000C90 RID: 3216
		private readonly Dictionary<string, ComplexType> m_types = new Dictionary<string, ComplexType>(StringComparer.Ordinal);

		// Token: 0x04000C91 RID: 3217
		private readonly Dictionary<string, ComplexType> m_isOfTypes = new Dictionary<string, ComplexType>(StringComparer.Ordinal);
	}
}
