using System;
using System.ComponentModel;
using Telerik.Web.UI.ODataSource.Filters;

namespace Telerik.Web.UI
{
	// Token: 0x02000BCD RID: 3021
	public class FilterEntry : EntryBase
	{
		// Token: 0x06007372 RID: 29554 RVA: 0x001AFF7C File Offset: 0x001AE17C
		public FilterEntry()
		{
			this._operator = ODataSourceFilters.None;
			this._value = "";
		}

		// Token: 0x17002597 RID: 9623
		// (get) Token: 0x06007373 RID: 29555 RVA: 0x001AFF97 File Offset: 0x001AE197
		// (set) Token: 0x06007374 RID: 29556 RVA: 0x001AFF9F File Offset: 0x001AE19F
		[Description("Gets or sets the filtering operator.")]
		[DefaultValue("")]
		[Category("Behavior")]
		public ODataSourceFilters Operator
		{
			get
			{
				return this._operator;
			}
			set
			{
				this._operator = value;
			}
		}

		// Token: 0x17002598 RID: 9624
		// (get) Token: 0x06007375 RID: 29557 RVA: 0x001AFFA8 File Offset: 0x001AE1A8
		// (set) Token: 0x06007376 RID: 29558 RVA: 0x001AFFB0 File Offset: 0x001AE1B0
		[Category("Behavior")]
		[Description("Gets or sets the value for the filtering operation.")]
		[DefaultValue("")]
		public string Value
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

		// Token: 0x04001F5C RID: 8028
		private ODataSourceFilters _operator;

		// Token: 0x04001F5D RID: 8029
		private string _value;
	}
}
