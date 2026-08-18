using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000CE4 RID: 3300
	internal class PivotDynamicProperty
	{
		// Token: 0x06007B42 RID: 31554 RVA: 0x001C4DDD File Offset: 0x001C2FDD
		public PivotDynamicProperty(string name, Type type)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.name = name;
			this.type = type;
		}

		// Token: 0x1700276D RID: 10093
		// (get) Token: 0x06007B43 RID: 31555 RVA: 0x001C4E15 File Offset: 0x001C3015
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700276E RID: 10094
		// (get) Token: 0x06007B44 RID: 31556 RVA: 0x001C4E1D File Offset: 0x001C301D
		[SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "Design choice.")]
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x040021BE RID: 8638
		private string name;

		// Token: 0x040021BF RID: 8639
		private Type type;
	}
}
