using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000571 RID: 1393
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class EditCommandColumn : DataGridColumn
	{
		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x06004478 RID: 17528 RVA: 0x00119D8C File Offset: 0x00118D8C
		// (set) Token: 0x06004479 RID: 17529 RVA: 0x00119DB5 File Offset: 0x00118DB5
		[DefaultValue(ButtonColumnType.LinkButton)]
		public virtual ButtonColumnType ButtonType
		{
			get
			{
				object obj = base.ViewState["ButtonType"];
				if (obj != null)
				{
					return (ButtonColumnType)obj;
				}
				return ButtonColumnType.LinkButton;
			}
			set
			{
				if (value < ButtonColumnType.LinkButton || value > ButtonColumnType.PushButton)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["ButtonType"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x170010C6 RID: 4294
		// (get) Token: 0x0600447A RID: 17530 RVA: 0x00119DE8 File Offset: 0x00118DE8
		// (set) Token: 0x0600447B RID: 17531 RVA: 0x00119E15 File Offset: 0x00118E15
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string CancelText
		{
			get
			{
				object obj = base.ViewState["CancelText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["CancelText"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x0600447C RID: 17532 RVA: 0x00119E30 File Offset: 0x00118E30
		// (set) Token: 0x0600447D RID: 17533 RVA: 0x00119E59 File Offset: 0x00118E59
		[DefaultValue(true)]
		public virtual bool CausesValidation
		{
			get
			{
				object obj = base.ViewState["CausesValidation"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["CausesValidation"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x0600447E RID: 17534 RVA: 0x00119E78 File Offset: 0x00118E78
		// (set) Token: 0x0600447F RID: 17535 RVA: 0x00119EA5 File Offset: 0x00118EA5
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string EditText
		{
			get
			{
				object obj = base.ViewState["EditText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["EditText"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x170010C9 RID: 4297
		// (get) Token: 0x06004480 RID: 17536 RVA: 0x00119EC0 File Offset: 0x00118EC0
		// (set) Token: 0x06004481 RID: 17537 RVA: 0x00119EED File Offset: 0x00118EED
		[Localizable(true)]
		[DefaultValue("")]
		public virtual string UpdateText
		{
			get
			{
				object obj = base.ViewState["UpdateText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["UpdateText"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x06004482 RID: 17538 RVA: 0x00119F08 File Offset: 0x00118F08
		// (set) Token: 0x06004483 RID: 17539 RVA: 0x00119F35 File Offset: 0x00118F35
		[DefaultValue("")]
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
				base.ViewState["ValidationGroup"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x06004484 RID: 17540 RVA: 0x00119F50 File Offset: 0x00118F50
		private void AddButtonToCell(TableCell cell, string commandName, string buttonText, bool causesValidation, string validationGroup)
		{
			ControlCollection controls = cell.Controls;
			WebControl child;
			if (this.ButtonType == ButtonColumnType.LinkButton)
			{
				LinkButton linkButton = new DataGridLinkButton();
				child = linkButton;
				linkButton.CommandName = commandName;
				linkButton.Text = buttonText;
				linkButton.CausesValidation = causesValidation;
				linkButton.ValidationGroup = validationGroup;
			}
			else
			{
				Button button = new Button();
				child = button;
				button.CommandName = commandName;
				button.Text = buttonText;
				button.CausesValidation = causesValidation;
				button.ValidationGroup = validationGroup;
			}
			controls.Add(child);
		}

		// Token: 0x06004485 RID: 17541 RVA: 0x00119FCC File Offset: 0x00118FCC
		public override void InitializeCell(TableCell cell, int columnIndex, ListItemType itemType)
		{
			base.InitializeCell(cell, columnIndex, itemType);
			bool causesValidation = this.CausesValidation;
			if (itemType != ListItemType.Header && itemType != ListItemType.Footer)
			{
				if (itemType == ListItemType.EditItem)
				{
					ControlCollection controls = cell.Controls;
					this.AddButtonToCell(cell, "Update", this.UpdateText, causesValidation, this.ValidationGroup);
					LiteralControl child = new LiteralControl("&nbsp;");
					controls.Add(child);
					this.AddButtonToCell(cell, "Cancel", this.CancelText, false, string.Empty);
					return;
				}
				this.AddButtonToCell(cell, "Edit", this.EditText, false, string.Empty);
			}
		}
	}
}
