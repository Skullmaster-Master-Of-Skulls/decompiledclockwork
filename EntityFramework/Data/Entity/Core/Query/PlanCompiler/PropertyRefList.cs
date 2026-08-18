using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000696 RID: 1686
	internal class PropertyRefList
	{
		// Token: 0x060042CA RID: 17098 RVA: 0x0013C85A File Offset: 0x0013AA5A
		internal PropertyRefList() : this(false)
		{
		}

		// Token: 0x060042CB RID: 17099 RVA: 0x0013C863 File Offset: 0x0013AA63
		private PropertyRefList(bool allProps)
		{
			this.m_propertyReferences = new Dictionary<PropertyRef, PropertyRef>();
			if (allProps)
			{
				this.MakeAllProperties();
			}
		}

		// Token: 0x060042CC RID: 17100 RVA: 0x0013C87F File Offset: 0x0013AA7F
		private void MakeAllProperties()
		{
			this.m_allProperties = true;
			this.m_propertyReferences.Clear();
			this.m_propertyReferences.Add(AllPropertyRef.Instance, AllPropertyRef.Instance);
		}

		// Token: 0x060042CD RID: 17101 RVA: 0x0013C8A8 File Offset: 0x0013AAA8
		internal void Add(PropertyRef property)
		{
			if (this.m_allProperties)
			{
				return;
			}
			if (property is AllPropertyRef)
			{
				this.MakeAllProperties();
				return;
			}
			this.m_propertyReferences[property] = property;
		}

		// Token: 0x060042CE RID: 17102 RVA: 0x0013C8D0 File Offset: 0x0013AAD0
		internal void Append(PropertyRefList propertyRefs)
		{
			if (this.m_allProperties)
			{
				return;
			}
			foreach (PropertyRef property in propertyRefs.m_propertyReferences.Keys)
			{
				this.Add(property);
			}
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x060042CF RID: 17103 RVA: 0x0013C934 File Offset: 0x0013AB34
		internal bool AllProperties
		{
			get
			{
				return this.m_allProperties;
			}
		}

		// Token: 0x060042D0 RID: 17104 RVA: 0x0013C93C File Offset: 0x0013AB3C
		internal PropertyRefList Clone()
		{
			PropertyRefList propertyRefList = new PropertyRefList(this.m_allProperties);
			foreach (PropertyRef property in this.m_propertyReferences.Keys)
			{
				propertyRefList.Add(property);
			}
			return propertyRefList;
		}

		// Token: 0x060042D1 RID: 17105 RVA: 0x0013C9A4 File Offset: 0x0013ABA4
		internal bool Contains(PropertyRef p)
		{
			return this.m_allProperties || this.m_propertyReferences.ContainsKey(p);
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x060042D2 RID: 17106 RVA: 0x0013C9BC File Offset: 0x0013ABBC
		internal IEnumerable<PropertyRef> Properties
		{
			get
			{
				return this.m_propertyReferences.Keys;
			}
		}

		// Token: 0x060042D3 RID: 17107 RVA: 0x0013C9CC File Offset: 0x0013ABCC
		public override string ToString()
		{
			string text = "{";
			foreach (PropertyRef arg in this.m_propertyReferences.Keys)
			{
				text = text + arg + ",";
			}
			text += "}";
			return text;
		}

		// Token: 0x040018AF RID: 6319
		private readonly Dictionary<PropertyRef, PropertyRef> m_propertyReferences;

		// Token: 0x040018B0 RID: 6320
		private bool m_allProperties;

		// Token: 0x040018B1 RID: 6321
		internal static PropertyRefList All = new PropertyRefList(true);
	}
}
