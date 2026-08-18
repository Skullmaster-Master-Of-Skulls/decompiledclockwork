using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x020019F0 RID: 6640
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[TelerikToolboxCategory("Data")]
	public class GridHTMLEditorColumnEditor : GridTextColumnEditor
	{
		// Token: 0x060100F8 RID: 65784 RVA: 0x0039ADA0 File Offset: 0x00398FA0
		public GridHTMLEditorColumnEditor()
		{
		}

		// Token: 0x060100F9 RID: 65785 RVA: 0x0039ADA8 File Offset: 0x00398FA8
		public GridHTMLEditorColumnEditor(GridHTMLEditorColumn owner)
		{
			this.owner = owner;
		}

		// Token: 0x060100FA RID: 65786 RVA: 0x0039ADB7 File Offset: 0x00398FB7
		public override void SetOwner(IGridEditableColumn owner)
		{
			this.owner = (owner as GridHTMLEditorColumn);
		}

		// Token: 0x17004D91 RID: 19857
		// (get) Token: 0x060100FB RID: 65787 RVA: 0x0039ADC5 File Offset: 0x00398FC5
		// (set) Token: 0x060100FC RID: 65788 RVA: 0x0039ADD2 File Offset: 0x00398FD2
		public override string Text
		{
			get
			{
				return this.Editor.Content;
			}
			set
			{
				this.Editor.Content = value;
			}
		}

		// Token: 0x17004D92 RID: 19858
		// (get) Token: 0x060100FD RID: 65789 RVA: 0x0039ADE0 File Offset: 0x00398FE0
		public override bool IsInitialized
		{
			get
			{
				return this._editor != null;
			}
		}

		// Token: 0x17004D93 RID: 19859
		// (get) Token: 0x060100FE RID: 65790 RVA: 0x0039ADEE File Offset: 0x00398FEE
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets the RadEditor instance.")]
		[Browsable(true)]
		public RadEditor Editor
		{
			get
			{
				this.EnsureControlsCreated();
				return this._editor;
			}
		}

		// Token: 0x060100FF RID: 65791 RVA: 0x0039ADFC File Offset: 0x00398FFC
		protected override void CreateControls()
		{
			this._editor = new RadEditor();
			if (this.owner != null)
			{
				this._editor.ID = string.Format("gridEditor_{0}", this.owner.UniqueName);
				this._editor.EnableAriaSupport = this.owner.Owner.OwnerGrid.EnableAriaSupport;
				this._editor.RenderMode = this.owner.Owner.OwnerGrid.RenderMode;
				GridColumnValidationSettings columnValidationSettings = this.owner.ColumnValidationSettings;
				if (columnValidationSettings.EnableRequiredFieldValidation)
				{
					this.requiredFieldValidator = columnValidationSettings.GetClonedValidator();
					this.requiredFieldValidator.ControlToValidate = this._editor.ID;
					this.requiredFieldValidator.ID = string.Format("RFV_{0}", this.owner.UniqueName);
				}
				if (columnValidationSettings.EnableModelErrorMessageValidation)
				{
					this.errorMessageValidator = columnValidationSettings.GetClonedModelValidator();
					this.errorMessageValidator.AssociatedControlID = this._editor.ID;
					this.errorMessageValidator.ModelStateKey = this.owner.DataField;
					this.errorMessageValidator.ID = string.Format("EMV_{0}", this.owner.UniqueName);
				}
			}
			this._editor.Width = Unit.Pixel(500);
			this._editor.Height = Unit.Pixel(300);
		}

		// Token: 0x06010100 RID: 65792 RVA: 0x0039AF5F File Offset: 0x0039915F
		internal ModelErrorMessage GetModelErrorMessageValidator()
		{
			return this.errorMessageValidator;
		}

		// Token: 0x06010101 RID: 65793 RVA: 0x0039AF67 File Offset: 0x00399167
		internal RequiredFieldValidator GetRequiredFieldValidator()
		{
			return this.requiredFieldValidator;
		}

		// Token: 0x06010102 RID: 65794 RVA: 0x0039AF70 File Offset: 0x00399170
		protected override void AddControlsToContainer()
		{
			this.EnsureControlsCreated();
			this._editor.EnableEmbeddedSkins = this.owner.Owner.OwnerGrid.EnableEmbeddedSkins;
			EditorToolGroup editorToolGroup = new EditorToolGroup();
			editorToolGroup.Tools.Add(new EditorTool("Italic"));
			editorToolGroup.Tools.Add(new EditorTool("Bold"));
			editorToolGroup.Tools.Add(new EditorTool("Underline"));
			editorToolGroup.Tools.Add(new EditorTool("StrikeThrough"));
			editorToolGroup.Tools.Add(new EditorTool("JustifyLeft"));
			editorToolGroup.Tools.Add(new EditorTool("JustifyCenter"));
			editorToolGroup.Tools.Add(new EditorTool("JustifyRight"));
			editorToolGroup.Tools.Add(new EditorTool("JustifyFull"));
			this._editor.Tools.Add(editorToolGroup);
			this._editor.Load += this._editor_Load;
			this._editor.PreRender += this._editor_PreRender;
			GridColumnValidationSettings columnValidationSettings = this.owner.ColumnValidationSettings;
			if (columnValidationSettings.EnableRequiredFieldValidation && columnValidationSettings.RenderValidatorBeforeEditor)
			{
				this.ContainerControl.Controls.Add(this.requiredFieldValidator);
			}
			if (columnValidationSettings.EnableModelErrorMessageValidation && columnValidationSettings.RenderValidatorBeforeEditor)
			{
				this.ContainerControl.Controls.Add(this.errorMessageValidator);
			}
			this.ContainerControl.Controls.Add(this._editor);
			if (columnValidationSettings.EnableRequiredFieldValidation && !columnValidationSettings.RenderValidatorBeforeEditor)
			{
				this.ContainerControl.Controls.Add(this.requiredFieldValidator);
			}
			if (columnValidationSettings.EnableModelErrorMessageValidation && !columnValidationSettings.RenderValidatorBeforeEditor)
			{
				this.ContainerControl.Controls.Add(this.errorMessageValidator);
			}
		}

		// Token: 0x06010103 RID: 65795 RVA: 0x0039B14C File Offset: 0x0039934C
		private void _editor_PreRender(object sender, EventArgs e)
		{
			RadEditor radEditor = sender as RadEditor;
			radEditor.Skin = this.owner.Owner.OwnerGrid.RuntimeSkin;
		}

		// Token: 0x06010104 RID: 65796 RVA: 0x0039B17B File Offset: 0x0039937B
		private void _editor_Load(object sender, EventArgs e)
		{
			this._editor.Modules.Clear();
		}

		// Token: 0x06010105 RID: 65797 RVA: 0x0039B190 File Offset: 0x00399390
		protected override void LoadControlsFromContainer()
		{
			this._editor = (this.ContainerControl.FindControl(string.Format("gridEditor_{0}", this.owner.UniqueName)) as RadEditor);
			if (this.owner.ColumnValidationSettings.EnableRequiredFieldValidation)
			{
				this.requiredFieldValidator = (this.ContainerControl.FindControl(string.Format("RFV_{0}", this.owner.UniqueName)) as RequiredFieldValidator);
			}
			if (this.owner.ColumnValidationSettings.EnableModelErrorMessageValidation)
			{
				this.errorMessageValidator = (this.ContainerControl.FindControl(string.Format("EMV_{0}", this.owner.UniqueName)) as ModelErrorMessage);
			}
		}

		// Token: 0x06010106 RID: 65798 RVA: 0x0039B244 File Offset: 0x00399444
		internal override void CopySettingsFrom(IGridColumnEditor editor)
		{
			base.CopySettingsFrom(editor);
			GridHTMLEditorColumnEditor gridHTMLEditorColumnEditor = editor as GridHTMLEditorColumnEditor;
			if (gridHTMLEditorColumnEditor != null)
			{
				GridHTMLEditorColumnEditor gridHTMLEditorColumnEditor2 = (GridHTMLEditorColumnEditor)gridHTMLEditorColumnEditor.MemberwiseClone();
				if (gridHTMLEditorColumnEditor2.owner == null)
				{
					gridHTMLEditorColumnEditor2.SetOwner(this.owner);
				}
				if (gridHTMLEditorColumnEditor2.Editor != null)
				{
					this.EnsureControlsCreated();
					this._editor = gridHTMLEditorColumnEditor2.Editor;
					this._editor.ID = string.Format("gridEditor_{0}", this.owner.UniqueName);
					if (gridHTMLEditorColumnEditor.owner.ColumnValidationSettings.EnableRequiredFieldValidation)
					{
						this.requiredFieldValidator = gridHTMLEditorColumnEditor2.requiredFieldValidator;
						this.requiredFieldValidator.ControlToValidate = this._editor.ID;
						this.requiredFieldValidator.ID = string.Format("RFV_{0}", this.owner.UniqueName);
					}
					if (gridHTMLEditorColumnEditor.owner.ColumnValidationSettings.EnableModelErrorMessageValidation)
					{
						this.errorMessageValidator = gridHTMLEditorColumnEditor2.errorMessageValidator;
						this.errorMessageValidator.AssociatedControlID = this._editor.ID;
						this.errorMessageValidator.ModelStateKey = gridHTMLEditorColumnEditor2.owner.DataField;
						this.errorMessageValidator.ID = string.Format("EMV_{0}", this.owner.UniqueName);
					}
				}
			}
		}

		// Token: 0x040048D7 RID: 18647
		private RadEditor _editor;

		// Token: 0x040048D8 RID: 18648
		private GridHTMLEditorColumn owner;

		// Token: 0x040048D9 RID: 18649
		private RequiredFieldValidator requiredFieldValidator;

		// Token: 0x040048DA RID: 18650
		private ModelErrorMessage errorMessageValidator;
	}
}
