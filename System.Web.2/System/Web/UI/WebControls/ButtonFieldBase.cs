using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200038B RID: 907
	public abstract class ButtonFieldBase : DataControlField
	{
		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06002A3A RID: 10810 RVA: 0x00088B28 File Offset: 0x00086D28
		// (set) Token: 0x06002A3B RID: 10811 RVA: 0x00088B54 File Offset: 0x00086D54
		[WebCategory("Appearance")]
		[DefaultValue(ButtonType.Link)]
		[WebSysDescription("ButtonFieldBase_ButtonType")]
		public virtual ButtonType ButtonType
		{
			get
			{
				object obj = base.ViewState["ButtonType"];
				if (obj != null)
				{
					return (ButtonType)obj;
				}
				return ButtonType.Link;
			}
			set
			{
				if (value < ButtonType.Button || value > ButtonType.Link)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				object obj = base.ViewState["ButtonType"];
				if (obj == null || (ButtonType)obj != value)
				{
					base.ViewState["ButtonType"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06002A3C RID: 10812 RVA: 0x00088BB0 File Offset: 0x00086DB0
		// (set) Token: 0x06002A3D RID: 10813 RVA: 0x00088BDC File Offset: 0x00086DDC
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		[WebSysDescription("ButtonFieldBase_CausesValidation")]
		public virtual bool CausesValidation
		{
			get
			{
				object obj = base.ViewState["CausesValidation"];
				return obj != null && (bool)obj;
			}
			set
			{
				object obj = base.ViewState["CausesValidation"];
				if (obj == null || (bool)obj != value)
				{
					base.ViewState["CausesValidation"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06002A3E RID: 10814 RVA: 0x00088C24 File Offset: 0x00086E24
		// (set) Token: 0x06002A3F RID: 10815 RVA: 0x00088C50 File Offset: 0x00086E50
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		[WebSysDescription("DataControlField_ShowHeader")]
		public override bool ShowHeader
		{
			get
			{
				object obj = base.ViewState["ShowHeader"];
				return obj != null && (bool)obj;
			}
			set
			{
				object obj = base.ViewState["ShowHeader"];
				if (obj == null || (bool)obj != value)
				{
					base.ViewState["ShowHeader"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06002A40 RID: 10816 RVA: 0x00088C98 File Offset: 0x00086E98
		// (set) Token: 0x06002A41 RID: 10817 RVA: 0x00088CC5 File Offset: 0x00086EC5
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("ButtonFieldBase_ValidationGroup")]
		public virtual string ValidationGroup
		{
			get
			{
				object obj = base.ViewState["ValidationGroup"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (!object.Equals(value, base.ViewState["ValidationGroup"]))
				{
					base.ViewState["ValidationGroup"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x06002A42 RID: 10818 RVA: 0x00088CF6 File Offset: 0x00086EF6
		protected override void CopyProperties(DataControlField newField)
		{
			((ButtonFieldBase)newField).ButtonType = this.ButtonType;
			((ButtonFieldBase)newField).CausesValidation = this.CausesValidation;
			((ButtonFieldBase)newField).ValidationGroup = this.ValidationGroup;
			base.CopyProperties(newField);
		}
	}
}
