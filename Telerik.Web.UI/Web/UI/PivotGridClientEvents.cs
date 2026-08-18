using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000DEB RID: 3563
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class PivotGridClientEvents : StateManager
	{
		// Token: 0x170029CD RID: 10701
		// (get) Token: 0x06008442 RID: 33858 RVA: 0x001E2A2C File Offset: 0x001E0C2C
		// (set) Token: 0x06008443 RID: 33859 RVA: 0x001E2A59 File Offset: 0x001E0C59
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("OnPivotGridCreating")]
		public virtual string OnPivotGridCreating
		{
			get
			{
				object obj = base.ViewState["OnPivotGridCreating"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnPivotGridCreating"] = value;
			}
		}

		// Token: 0x170029CE RID: 10702
		// (get) Token: 0x06008444 RID: 33860 RVA: 0x001E2A6C File Offset: 0x001E0C6C
		// (set) Token: 0x06008445 RID: 33861 RVA: 0x001E2A99 File Offset: 0x001E0C99
		[Description("OnPivotGridCreated")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnPivotGridCreated
		{
			get
			{
				object obj = base.ViewState["OnPivotGridCreated"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnPivotGridCreated"] = value;
			}
		}

		// Token: 0x170029CF RID: 10703
		// (get) Token: 0x06008446 RID: 33862 RVA: 0x001E2AAC File Offset: 0x001E0CAC
		// (set) Token: 0x06008447 RID: 33863 RVA: 0x001E2AD9 File Offset: 0x001E0CD9
		[DefaultValue("")]
		[Description("OnPivotGridDestroying")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnPivotGridDestroying
		{
			get
			{
				object obj = base.ViewState["OnPivotGridDestroying"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnPivotGridDestroying"] = value;
			}
		}

		// Token: 0x170029D0 RID: 10704
		// (get) Token: 0x06008448 RID: 33864 RVA: 0x001E2AEC File Offset: 0x001E0CEC
		// (set) Token: 0x06008449 RID: 33865 RVA: 0x001E2B19 File Offset: 0x001E0D19
		[Category("Client-side events")]
		[DefaultValue("")]
		[Description("OnCommand")]
		[NotifyParentProperty(true)]
		public virtual string OnCommand
		{
			get
			{
				object obj = base.ViewState["OnCommand"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnCommand"] = value;
			}
		}

		// Token: 0x170029D1 RID: 10705
		// (get) Token: 0x0600844A RID: 33866 RVA: 0x001E2B2C File Offset: 0x001E0D2C
		// (set) Token: 0x0600844B RID: 33867 RVA: 0x001E2B59 File Offset: 0x001E0D59
		[DefaultValue("")]
		[Description("OnToolTipShow")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnToolTipShow
		{
			get
			{
				object obj = base.ViewState["OnToolTipShow"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnToolTipShow"] = value;
			}
		}

		// Token: 0x170029D2 RID: 10706
		// (get) Token: 0x0600844C RID: 33868 RVA: 0x001E2B6C File Offset: 0x001E0D6C
		// (set) Token: 0x0600844D RID: 33869 RVA: 0x001E2B99 File Offset: 0x001E0D99
		[Description("OnCellClick")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Category("Client-side events")]
		public virtual string OnCellClick
		{
			get
			{
				object obj = base.ViewState["OnCellClick"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnCellClick"] = value;
			}
		}

		// Token: 0x170029D3 RID: 10707
		// (get) Token: 0x0600844E RID: 33870 RVA: 0x001E2BAC File Offset: 0x001E0DAC
		// (set) Token: 0x0600844F RID: 33871 RVA: 0x001E2BD9 File Offset: 0x001E0DD9
		[Description("OnCellMouseOver")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnCellMouseOver
		{
			get
			{
				object obj = base.ViewState["OnCellMouseOver"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnCellMouseOver"] = value;
			}
		}

		// Token: 0x170029D4 RID: 10708
		// (get) Token: 0x06008450 RID: 33872 RVA: 0x001E2BEC File Offset: 0x001E0DEC
		// (set) Token: 0x06008451 RID: 33873 RVA: 0x001E2C19 File Offset: 0x001E0E19
		[Category("Client-side events")]
		[DefaultValue("")]
		[Description("OnCellMouseOut")]
		[NotifyParentProperty(true)]
		public virtual string OnCellMouseOut
		{
			get
			{
				object obj = base.ViewState["OnCellMouseOut"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnCellMouseOut"] = value;
			}
		}

		// Token: 0x170029D5 RID: 10709
		// (get) Token: 0x06008452 RID: 33874 RVA: 0x001E2C2C File Offset: 0x001E0E2C
		// (set) Token: 0x06008453 RID: 33875 RVA: 0x001E2C59 File Offset: 0x001E0E59
		[DefaultValue("")]
		[Description("OnCellDoubleClick")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnCellDoubleClick
		{
			get
			{
				object obj = base.ViewState["OnCellDoubleClick"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnCellDoubleClick"] = value;
			}
		}

		// Token: 0x170029D6 RID: 10710
		// (get) Token: 0x06008454 RID: 33876 RVA: 0x001E2C6C File Offset: 0x001E0E6C
		// (set) Token: 0x06008455 RID: 33877 RVA: 0x001E2C99 File Offset: 0x001E0E99
		[Category("Client-side events")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Description("OnCellContextMenu")]
		public virtual string OnCellContextMenu
		{
			get
			{
				object obj = base.ViewState["OnCellContextMenu"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnCellContextMenu"] = value;
			}
		}

		// Token: 0x170029D7 RID: 10711
		// (get) Token: 0x06008456 RID: 33878 RVA: 0x001E2CAC File Offset: 0x001E0EAC
		// (set) Token: 0x06008457 RID: 33879 RVA: 0x001E2CD9 File Offset: 0x001E0ED9
		[DefaultValue("")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		[Description("OnMenuShowing")]
		public virtual string OnMenuShowing
		{
			get
			{
				object obj = base.ViewState["OnMenuShowing"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnMenuShowing"] = value;
			}
		}

		// Token: 0x170029D8 RID: 10712
		// (get) Token: 0x06008458 RID: 33880 RVA: 0x001E2CEC File Offset: 0x001E0EEC
		// (set) Token: 0x06008459 RID: 33881 RVA: 0x001E2D19 File Offset: 0x001E0F19
		[Category("Client-side events")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Description("OnMenuShown")]
		public virtual string OnMenuShown
		{
			get
			{
				object obj = base.ViewState["OnMenuShown"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnMenuShown"] = value;
			}
		}

		// Token: 0x170029D9 RID: 10713
		// (get) Token: 0x0600845A RID: 33882 RVA: 0x001E2D2C File Offset: 0x001E0F2C
		// (set) Token: 0x0600845B RID: 33883 RVA: 0x001E2D59 File Offset: 0x001E0F59
		[Category("Client-side events")]
		[DefaultValue("")]
		[Description("OnColumnResizing")]
		[NotifyParentProperty(true)]
		public virtual string OnColumnResizing
		{
			get
			{
				object obj = base.ViewState["OnColumnResizing"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnColumnResizing"] = value;
			}
		}

		// Token: 0x170029DA RID: 10714
		// (get) Token: 0x0600845C RID: 33884 RVA: 0x001E2D6C File Offset: 0x001E0F6C
		// (set) Token: 0x0600845D RID: 33885 RVA: 0x001E2D99 File Offset: 0x001E0F99
		[DefaultValue("")]
		[Description("OnColumnResized")]
		[Category("Client-side events")]
		[NotifyParentProperty(true)]
		public virtual string OnColumnResized
		{
			get
			{
				object obj = base.ViewState["OnColumnResized"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["OnColumnResized"] = value;
			}
		}
	}
}
