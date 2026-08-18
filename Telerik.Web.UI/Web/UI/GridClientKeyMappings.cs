using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001902 RID: 6402
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridClientKeyMappings : ObjectWithState
	{
		// Token: 0x0600F719 RID: 63257 RVA: 0x00381107 File Offset: 0x0037F307
		public GridClientKeyMappings(StateBag OwnerStateBag) : base("cs_keymaps_", OwnerStateBag)
		{
		}

		// Token: 0x17004A67 RID: 19047
		// (get) Token: 0x0600F71A RID: 63258 RVA: 0x00381118 File Offset: 0x0037F318
		[DefaultValue(27)]
		[Description("Exit edit/insert mode key")]
		[Category("Client")]
		[NotifyParentProperty(true)]
		public virtual int ExitEditInsertModeKey
		{
			get
			{
				object obj = base.ViewState["_eemk"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 27;
			}
		}

		// Token: 0x17004A68 RID: 19048
		// (get) Token: 0x0600F71B RID: 63259 RVA: 0x00381144 File Offset: 0x0037F344
		[Description("Update/insert item key")]
		[DefaultValue(13)]
		[Category("Client")]
		[NotifyParentProperty(true)]
		public virtual int UpdateInsertItemKey
		{
			get
			{
				object obj = base.ViewState["_uiik"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 13;
			}
		}
	}
}
