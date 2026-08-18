using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003EF RID: 1007
	public class EditCommandColumn : DataGridColumn
	{
		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x0600309F RID: 12447 RVA: 0x0009E9BC File Offset: 0x0009CBBC
		// (set) Token: 0x060030A0 RID: 12448 RVA: 0x00088291 File Offset: 0x00086491
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

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x060030A1 RID: 12449 RVA: 0x0009E9E8 File Offset: 0x0009CBE8
		// (set) Token: 0x060030A2 RID: 12450 RVA: 0x0009EA15 File Offset: 0x0009CC15
		[Localizable(true)]
		[DefaultValue("")]
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

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x060030A3 RID: 12451 RVA: 0x0009EA30 File Offset: 0x0009CC30
		// (set) Token: 0x060030A4 RID: 12452 RVA: 0x000882ED File Offset: 0x000864ED
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

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x060030A5 RID: 12453 RVA: 0x0009EA5C File Offset: 0x0009CC5C
		// (set) Token: 0x060030A6 RID: 12454 RVA: 0x0009EA89 File Offset: 0x0009CC89
		[Localizable(true)]
		[DefaultValue("")]
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

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x060030A7 RID: 12455 RVA: 0x0009EAA4 File Offset: 0x0009CCA4
		// (set) Token: 0x060030A8 RID: 12456 RVA: 0x0009EAD1 File Offset: 0x0009CCD1
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

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x060030A9 RID: 12457 RVA: 0x0009EAEC File Offset: 0x0009CCEC
		// (set) Token: 0x060030AA RID: 12458 RVA: 0x00088459 File Offset: 0x00086659
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

		// Token: 0x060030AB RID: 12459 RVA: 0x0009EB1C File Offset: 0x0009CD1C
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

		// Token: 0x060030AC RID: 12460 RVA: 0x0009EB98 File Offset: 0x0009CD98
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
