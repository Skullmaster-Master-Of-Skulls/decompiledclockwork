using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200069D RID: 1693
	internal class SimplePropertyRef : PropertyRef
	{
		// Token: 0x0600431E RID: 17182 RVA: 0x0013E4EA File Offset: 0x0013C6EA
		internal SimplePropertyRef(EdmMember property)
		{
			this.m_property = property;
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x0600431F RID: 17183 RVA: 0x0013E4F9 File Offset: 0x0013C6F9
		internal EdmMember Property
		{
			get
			{
				return this.m_property;
			}
		}

		// Token: 0x06004320 RID: 17184 RVA: 0x0013E504 File Offset: 0x0013C704
		public override bool Equals(object obj)
		{
			SimplePropertyRef simplePropertyRef = obj as SimplePropertyRef;
			return simplePropertyRef != null && Command.EqualTypes(this.m_property.DeclaringType, simplePropertyRef.m_property.DeclaringType) && simplePropertyRef.m_property.Name.Equals(this.m_property.Name);
		}

		// Token: 0x06004321 RID: 17185 RVA: 0x0013E555 File Offset: 0x0013C755
		public override int GetHashCode()
		{
			return this.m_property.Name.GetHashCode();
		}

		// Token: 0x06004322 RID: 17186 RVA: 0x0013E567 File Offset: 0x0013C767
		public override string ToString()
		{
			return this.m_property.Name;
		}

		// Token: 0x040018D7 RID: 6359
		private readonly EdmMember m_property;
	}
}
