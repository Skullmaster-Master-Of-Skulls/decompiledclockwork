using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000BDA RID: 3034
	public class Transport
	{
		// Token: 0x060073B7 RID: 29623 RVA: 0x001B0502 File Offset: 0x001AE702
		public Transport()
		{
			this._read = new Read();
		}

		// Token: 0x170025AF RID: 9647
		// (get) Token: 0x060073B8 RID: 29624 RVA: 0x001B0515 File Offset: 0x001AE715
		// (set) Token: 0x060073B9 RID: 29625 RVA: 0x001B051D File Offset: 0x001AE71D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[Category("Action")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Description("Gets or sets the settings for the Read data service")]
		public Read Read
		{
			get
			{
				return this._read;
			}
			set
			{
				this._read = value;
			}
		}

		// Token: 0x04001F76 RID: 8054
		private Read _read;
	}
}
