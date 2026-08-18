using System;
using System.Collections.Generic;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x0200006B RID: 107
	internal class PropertyRefList
	{
		// Token: 0x06000899 RID: 2201 RVA: 0x0002CD70 File Offset: 0x0002AF70
		internal PropertyRefList() : this(false)
		{
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0002CD79 File Offset: 0x0002AF79
		private PropertyRefList(bool allProps)
		{
			this.m_propertyReferences = new Dictionary<PropertyRef, PropertyRef>();
			if (allProps)
			{
				this.MakeAllProperties();
			}
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0002CD95 File Offset: 0x0002AF95
		private void MakeAllProperties()
		{
			this.m_allProperties = true;
			this.m_propertyReferences.Clear();
			this.m_propertyReferences.Add(AllPropertyRef.Instance, AllPropertyRef.Instance);
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0002CDBE File Offset: 0x0002AFBE
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

		// Token: 0x0600089D RID: 2205 RVA: 0x0002CDE8 File Offset: 0x0002AFE8
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

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x0002CE4C File Offset: 0x0002B04C
		internal bool AllProperties
		{
			get
			{
				return this.m_allProperties;
			}
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0002CE54 File Offset: 0x0002B054
		internal PropertyRefList Clone()
		{
			PropertyRefList propertyRefList = new PropertyRefList(this.m_allProperties);
			foreach (PropertyRef property in this.m_propertyReferences.Keys)
			{
				propertyRefList.Add(property);
			}
			return propertyRefList;
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0002CEBC File Offset: 0x0002B0BC
		internal bool Contains(PropertyRef p)
		{
			return this.m_allProperties || this.m_propertyReferences.ContainsKey(p);
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x0002CED4 File Offset: 0x0002B0D4
		internal IEnumerable<PropertyRef> Properties
		{
			get
			{
				return this.m_propertyReferences.Keys;
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0002CEE4 File Offset: 0x0002B0E4
		public override string ToString()
		{
			string text = "{";
			foreach (PropertyRef propertyRef in this.m_propertyReferences.Keys)
			{
				text = text + propertyRef.ToString() + ",";
			}
			text += "}";
			return text;
		}

		// Token: 0x040007FF RID: 2047
		private Dictionary<PropertyRef, PropertyRef> m_propertyReferences;

		// Token: 0x04000800 RID: 2048
		private bool m_allProperties;

		// Token: 0x04000801 RID: 2049
		internal static PropertyRefList All = new PropertyRefList(true);
	}
}
