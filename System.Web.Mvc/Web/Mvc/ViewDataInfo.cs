using System;
using System.ComponentModel;

namespace System.Web.Mvc
{
	// Token: 0x0200015A RID: 346
	public class ViewDataInfo
	{
		// Token: 0x060008D6 RID: 2262 RVA: 0x0001840E File Offset: 0x0001660E
		public ViewDataInfo()
		{
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00018416 File Offset: 0x00016616
		public ViewDataInfo(Func<object> valueAccessor)
		{
			this._valueAccessor = valueAccessor;
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x00018425 File Offset: 0x00016625
		// (set) Token: 0x060008D9 RID: 2265 RVA: 0x0001842D File Offset: 0x0001662D
		public object Container { get; set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x00018436 File Offset: 0x00016636
		// (set) Token: 0x060008DB RID: 2267 RVA: 0x0001843E File Offset: 0x0001663E
		public PropertyDescriptor PropertyDescriptor { get; set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x00018447 File Offset: 0x00016647
		// (set) Token: 0x060008DD RID: 2269 RVA: 0x0001846F File Offset: 0x0001666F
		public object Value
		{
			get
			{
				if (this._valueAccessor != null)
				{
					this._value = this._valueAccessor();
					this._valueAccessor = null;
				}
				return this._value;
			}
			set
			{
				this._value = value;
				this._valueAccessor = null;
			}
		}

		// Token: 0x0400027A RID: 634
		private object _value;

		// Token: 0x0400027B RID: 635
		private Func<object> _valueAccessor;
	}
}
