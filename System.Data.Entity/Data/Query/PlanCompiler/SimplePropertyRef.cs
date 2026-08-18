using System;
using System.Data.Metadata.Edm;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000064 RID: 100
	internal class SimplePropertyRef : PropertyRef
	{
		// Token: 0x0600087B RID: 2171 RVA: 0x0002CB70 File Offset: 0x0002AD70
		internal SimplePropertyRef(EdmMember property)
		{
			this.m_property = property;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x0002CB7F File Offset: 0x0002AD7F
		internal EdmMember Property
		{
			get
			{
				return this.m_property;
			}
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0002CB88 File Offset: 0x0002AD88
		public override bool Equals(object obj)
		{
			SimplePropertyRef simplePropertyRef = obj as SimplePropertyRef;
			return simplePropertyRef != null && Command.EqualTypes(this.m_property.DeclaringType, simplePropertyRef.m_property.DeclaringType) && simplePropertyRef.m_property.Name.Equals(this.m_property.Name);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0002CBD9 File Offset: 0x0002ADD9
		public override int GetHashCode()
		{
			return this.m_property.Name.GetHashCode();
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0002CBEB File Offset: 0x0002ADEB
		public override string ToString()
		{
			return this.m_property.Name;
		}

		// Token: 0x040007F7 RID: 2039
		private EdmMember m_property;
	}
}
