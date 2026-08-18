using System;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000698 RID: 1688
	internal class RelPropertyRef : PropertyRef
	{
		// Token: 0x060042D7 RID: 17111 RVA: 0x0013CB57 File Offset: 0x0013AD57
		internal RelPropertyRef(RelProperty property)
		{
			this.m_property = property;
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x060042D8 RID: 17112 RVA: 0x0013CB66 File Offset: 0x0013AD66
		internal RelProperty Property
		{
			get
			{
				return this.m_property;
			}
		}

		// Token: 0x060042D9 RID: 17113 RVA: 0x0013CB70 File Offset: 0x0013AD70
		public override bool Equals(object obj)
		{
			RelPropertyRef relPropertyRef = obj as RelPropertyRef;
			return relPropertyRef != null && this.m_property.Equals(relPropertyRef.m_property);
		}

		// Token: 0x060042DA RID: 17114 RVA: 0x0013CB9A File Offset: 0x0013AD9A
		public override int GetHashCode()
		{
			return this.m_property.GetHashCode();
		}

		// Token: 0x060042DB RID: 17115 RVA: 0x0013CBA7 File Offset: 0x0013ADA7
		public override string ToString()
		{
			return this.m_property.ToString();
		}

		// Token: 0x040018B2 RID: 6322
		private readonly RelProperty m_property;
	}
}
