using System;
using System.ComponentModel;

namespace Telerik.Web.UI.ODataSource.Filters
{
	// Token: 0x02000BCC RID: 3020
	public abstract class EntryBase
	{
		// Token: 0x0600736F RID: 29551 RVA: 0x001AFF58 File Offset: 0x001AE158
		public EntryBase()
		{
			this._fieldName = "";
		}

		// Token: 0x17002596 RID: 9622
		// (get) Token: 0x06007370 RID: 29552 RVA: 0x001AFF6B File Offset: 0x001AE16B
		// (set) Token: 0x06007371 RID: 29553 RVA: 0x001AFF73 File Offset: 0x001AE173
		[Category("Behavior")]
		[DefaultValue("")]
		[Description("Gets or sets the field name for the operation.")]
		public string FieldName
		{
			get
			{
				return this._fieldName;
			}
			set
			{
				this._fieldName = value;
			}
		}

		// Token: 0x04001F5B RID: 8027
		private string _fieldName;
	}
}
