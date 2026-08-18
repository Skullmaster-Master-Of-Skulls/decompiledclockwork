using System;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x0200006A RID: 106
	internal class RelPropertyRef : PropertyRef
	{
		// Token: 0x06000894 RID: 2196 RVA: 0x0002CD15 File Offset: 0x0002AF15
		internal RelPropertyRef(RelProperty property)
		{
			this.m_property = property;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x0002CD24 File Offset: 0x0002AF24
		internal RelProperty Property
		{
			get
			{
				return this.m_property;
			}
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0002CD2C File Offset: 0x0002AF2C
		public override bool Equals(object obj)
		{
			RelPropertyRef relPropertyRef = obj as RelPropertyRef;
			return relPropertyRef != null && this.m_property.Equals(relPropertyRef.m_property);
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0002CD56 File Offset: 0x0002AF56
		public override int GetHashCode()
		{
			return this.m_property.GetHashCode();
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0002CD63 File Offset: 0x0002AF63
		public override string ToString()
		{
			return this.m_property.ToString();
		}

		// Token: 0x040007FE RID: 2046
		private RelProperty m_property;
	}
}
