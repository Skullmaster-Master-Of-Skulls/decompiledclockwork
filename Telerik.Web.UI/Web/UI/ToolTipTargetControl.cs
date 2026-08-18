using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x0200133E RID: 4926
	public class ToolTipTargetControl : StateManager
	{
		// Token: 0x0600CD67 RID: 52583 RVA: 0x002DBD25 File Offset: 0x002D9F25
		public ToolTipTargetControl()
		{
		}

		// Token: 0x0600CD68 RID: 52584 RVA: 0x002DBD2D File Offset: 0x002D9F2D
		public ToolTipTargetControl(string id) : this(id, false)
		{
		}

		// Token: 0x0600CD69 RID: 52585 RVA: 0x002DBD37 File Offset: 0x002D9F37
		public ToolTipTargetControl(string id, bool isClientID) : this(id, "", isClientID)
		{
		}

		// Token: 0x0600CD6A RID: 52586 RVA: 0x002DBD46 File Offset: 0x002D9F46
		public ToolTipTargetControl(string id, string val) : this(id, val, false)
		{
		}

		// Token: 0x0600CD6B RID: 52587 RVA: 0x002DBD51 File Offset: 0x002D9F51
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public ToolTipTargetControl(string id, string val, bool isClientID)
		{
			this.TargetControlID = id;
			this.Value = val;
			this.IsClientID = isClientID;
		}

		// Token: 0x17004201 RID: 16897
		// (get) Token: 0x0600CD6C RID: 52588 RVA: 0x002DBD6E File Offset: 0x002D9F6E
		// (set) Token: 0x0600CD6D RID: 52589 RVA: 0x002DBD99 File Offset: 0x002D9F99
		public virtual bool IsClientID
		{
			get
			{
				return base.ViewState["IsClientID"] != null && (bool)base.ViewState["IsClientID"];
			}
			set
			{
				base.ViewState["IsClientID"] = value;
			}
		}

		// Token: 0x17004202 RID: 16898
		// (get) Token: 0x0600CD6E RID: 52590 RVA: 0x002DBDB1 File Offset: 0x002D9FB1
		// (set) Token: 0x0600CD6F RID: 52591 RVA: 0x002DBDE0 File Offset: 0x002D9FE0
		public virtual string TargetControlID
		{
			get
			{
				if (base.ViewState["TargetControlID"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["TargetControlID"];
			}
			set
			{
				base.ViewState["TargetControlID"] = value;
			}
		}

		// Token: 0x17004203 RID: 16899
		// (get) Token: 0x0600CD70 RID: 52592 RVA: 0x002DBDF3 File Offset: 0x002D9FF3
		// (set) Token: 0x0600CD71 RID: 52593 RVA: 0x002DBE22 File Offset: 0x002DA022
		public virtual string Value
		{
			get
			{
				if (base.ViewState["Value"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["Value"];
			}
			set
			{
				base.ViewState["Value"] = value;
			}
		}
	}
}
