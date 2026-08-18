using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000F1E RID: 3870
	public class SkinReference : StateManager
	{
		// Token: 0x17002EBE RID: 11966
		// (get) Token: 0x060093CA RID: 37834 RVA: 0x00212B23 File Offset: 0x00210D23
		// (set) Token: 0x060093CB RID: 37835 RVA: 0x00212B61 File Offset: 0x00210D61
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Category("Behavior")]
		public string Path
		{
			get
			{
				if (base.ViewState["SkinPath"] == null)
				{
					base.ViewState["SkinPath"] = string.Empty;
				}
				return base.ViewState["SkinPath"].ToString();
			}
			set
			{
				if (string.IsNullOrEmpty(this.Assembly))
				{
					base.ViewState["SkinPath"] = value;
					return;
				}
				throw new Exception("Either the Path or Assembly property should be set, not both!");
			}
		}

		// Token: 0x17002EBF RID: 11967
		// (get) Token: 0x060093CC RID: 37836 RVA: 0x00212B8C File Offset: 0x00210D8C
		// (set) Token: 0x060093CD RID: 37837 RVA: 0x00212BCA File Offset: 0x00210DCA
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Category("Behavior")]
		public string Assembly
		{
			get
			{
				if (base.ViewState["Assembly"] == null)
				{
					base.ViewState["Assembly"] = string.Empty;
				}
				return base.ViewState["Assembly"].ToString();
			}
			set
			{
				if (string.IsNullOrEmpty(this.Path))
				{
					base.ViewState["Assembly"] = value;
					return;
				}
				throw new Exception("Either the Path or Assembly property of the ScriptReference object should be set, not both!");
			}
		}
	}
}
