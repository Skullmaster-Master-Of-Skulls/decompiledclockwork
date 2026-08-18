using System;

namespace Telerik.Web.UI
{
	// Token: 0x020004F0 RID: 1264
	public class AxisCrossingPoint : StateManager
	{
		// Token: 0x06002D12 RID: 11538 RVA: 0x00094203 File Offset: 0x00092403
		public AxisCrossingPoint()
		{
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x0009420B File Offset: 0x0009240B
		public AxisCrossingPoint(decimal? value)
		{
			this.Value = value;
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x0009421C File Offset: 0x0009241C
		public AxisCrossingPoint(int? value)
		{
			int? num = value;
			this.Value = ((num != null) ? new decimal?(num.GetValueOrDefault()) : null);
		}

		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x06002D15 RID: 11541 RVA: 0x0009425C File Offset: 0x0009245C
		// (set) Token: 0x06002D16 RID: 11542 RVA: 0x00094264 File Offset: 0x00092464
		public decimal? Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x04000C2F RID: 3119
		private decimal? _value;
	}
}
