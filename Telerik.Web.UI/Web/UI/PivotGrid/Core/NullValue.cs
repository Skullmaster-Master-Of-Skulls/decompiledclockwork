using System;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000D3B RID: 3387
	[Serializable]
	public class NullValue
	{
		// Token: 0x06007DDB RID: 32219 RVA: 0x001CC2BB File Offset: 0x001CA4BB
		private NullValue()
		{
		}

		// Token: 0x17002829 RID: 10281
		// (get) Token: 0x06007DDC RID: 32220 RVA: 0x001CC2C3 File Offset: 0x001CA4C3
		public static NullValue Instance
		{
			get
			{
				return NullValue.Singleton;
			}
		}

		// Token: 0x06007DDD RID: 32221 RVA: 0x001CC2CA File Offset: 0x001CA4CA
		public override string ToString()
		{
			return "(blank)";
		}

		// Token: 0x0400229D RID: 8861
		private static readonly NullValue Singleton = new NullValue();
	}
}
