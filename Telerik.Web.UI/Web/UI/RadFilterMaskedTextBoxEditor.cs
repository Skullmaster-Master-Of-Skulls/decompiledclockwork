using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000470 RID: 1136
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	public class RadFilterMaskedTextBoxEditor : RadFilterDataFieldEditor
	{
		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x060028CB RID: 10443 RVA: 0x00083F96 File Offset: 0x00082196
		// (set) Token: 0x060028CC RID: 10444 RVA: 0x00083FB6 File Offset: 0x000821B6
		[DefaultValue("")]
		[Description("Gets or sets the Mask property of the RadMaskedTextBox control.")]
		public string Mask
		{
			get
			{
				return ((string)base.ViewState["Mask"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["Mask"] = value;
			}
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x060028CD RID: 10445 RVA: 0x00083FC9 File Offset: 0x000821C9
		// (set) Token: 0x060028CE RID: 10446 RVA: 0x00083FE9 File Offset: 0x000821E9
		[Description("Gets or sets the DisplayMask property of the RadMaskedTextBox control.")]
		[DefaultValue("")]
		public string DisplayMask
		{
			get
			{
				return ((string)base.ViewState["DisplayMask"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["DisplayMask"] = value;
			}
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x060028CF RID: 10447 RVA: 0x00083FFC File Offset: 0x000821FC
		// (set) Token: 0x060028D0 RID: 10448 RVA: 0x0008401C File Offset: 0x0008221C
		[DefaultValue("")]
		[Description("Gets or sets the PromptChar property of the RadMaskedTextBox control.")]
		public string PromptChar
		{
			get
			{
				return ((string)base.ViewState["PromptChar"]) ?? "_";
			}
			set
			{
				base.ViewState["PromptChar"] = value;
			}
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x060028D1 RID: 10449 RVA: 0x0008402F File Offset: 0x0008222F
		// (set) Token: 0x060028D2 RID: 10450 RVA: 0x0008404F File Offset: 0x0008224F
		[Description("Gets or sets the DisplayPromptChar property of the RadMaskedTextBox control.")]
		[DefaultValue("")]
		public string DisplayPromptChar
		{
			get
			{
				return ((string)base.ViewState["DisplayPromptChar"]) ?? "_";
			}
			set
			{
				base.ViewState["DisplayPromptChar"] = value;
			}
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x00084064 File Offset: 0x00082264
		public override void InitializeEditor(Control container)
		{
			this.maskedTextBox = this.CreateMaskedTextBox();
			container.Controls.Add(this.maskedTextBox);
			if (!base.IsSingleValue || base.Owner.IsClientOperationMode)
			{
				base.AddBetweenDelimeterControl(container);
				this.secondMaskedTextBox = this.CreateMaskedTextBox();
				container.Controls.Add(this.secondMaskedTextBox);
			}
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x000840F8 File Offset: 0x000822F8
		private RadMaskedTextBox CreateMaskedTextBox()
		{
			RadMaskedTextBox textBox = new RadMaskedTextBox();
			textBox.RenderMode = base.Owner.ResolvedRenderMode;
			textBox.Mask = this.Mask;
			textBox.DisplayMask = this.DisplayMask;
			textBox.PromptChar = this.PromptChar;
			textBox.DisplayPromptChar = this.DisplayPromptChar;
			textBox.PreRender += delegate(object sender, EventArgs args)
			{
				textBox.Skin = ((RadFilterExpressionItem)textBox.NamingContainer).OwnerFilter.RuntimeSkin;
			};
			textBox.EnableEmbeddedSkins = base.Owner.EnableEmbeddedSkins;
			textBox.EnableEmbeddedScripts = base.Owner.EnableEmbeddedScripts;
			textBox.EnableEmbeddedBaseStylesheet = base.Owner.EnableEmbeddedBaseStylesheet;
			textBox.RegisterWithScriptManager = base.Owner.RegisterWithScriptManager;
			textBox.EnableAriaSupport = base.Owner.EnableAriaSupport;
			textBox.RenderMode = base.Owner.ResolvedRenderMode;
			return textBox;
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x00084214 File Offset: 0x00082414
		public override ArrayList ExtractValues()
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(this.maskedTextBox.Text);
			if (!base.IsSingleValue || this.secondMaskedTextBox != null)
			{
				arrayList.Add(this.secondMaskedTextBox.Text);
			}
			return arrayList;
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x0008425C File Offset: 0x0008245C
		public override void SetEditorValues(ArrayList values)
		{
			if (values != null)
			{
				if (values[0] != null)
				{
					this.maskedTextBox.Text = values[0].ToString();
				}
				if (!base.IsSingleValue && this.secondMaskedTextBox != null && values.Count > 1 && values[1] != null)
				{
					this.secondMaskedTextBox.Text = values[1].ToString();
				}
			}
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x000842C8 File Offset: 0x000824C8
		protected override void CopySettings(RadFilterDataFieldEditor baseEditor)
		{
			base.CopySettings(baseEditor);
			RadFilterMaskedTextBoxEditor radFilterMaskedTextBoxEditor = baseEditor as RadFilterMaskedTextBoxEditor;
			if (radFilterMaskedTextBoxEditor != null)
			{
				this.Mask = radFilterMaskedTextBoxEditor.Mask;
				this.DisplayMask = radFilterMaskedTextBoxEditor.DisplayMask;
				this.PromptChar = radFilterMaskedTextBoxEditor.PromptChar;
				this.DisplayPromptChar = radFilterMaskedTextBoxEditor.DisplayPromptChar;
			}
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x00084316 File Offset: 0x00082516
		internal override WebControl GetFirstInputControl(Control container)
		{
			return this.maskedTextBox;
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x0008431E File Offset: 0x0008251E
		internal override WebControl GetSecondInputControl(Control container)
		{
			return this.secondMaskedTextBox;
		}

		// Token: 0x04000A57 RID: 2647
		private RadMaskedTextBox maskedTextBox;

		// Token: 0x04000A58 RID: 2648
		private RadMaskedTextBox secondMaskedTextBox;
	}
}
