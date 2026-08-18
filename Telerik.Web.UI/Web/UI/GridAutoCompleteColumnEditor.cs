using System;
using System.ComponentModel;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x020004B9 RID: 1209
	[TelerikToolboxCategory("Data")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	public class GridAutoCompleteColumnEditor : GridColumnEditorBase
	{
		// Token: 0x06002AFA RID: 11002 RVA: 0x0008B657 File Offset: 0x00089857
		public GridAutoCompleteColumnEditor()
		{
		}

		// Token: 0x06002AFB RID: 11003 RVA: 0x0008B65F File Offset: 0x0008985F
		public GridAutoCompleteColumnEditor(GridAutoCompleteColumn owner)
		{
			this._owner = owner;
		}

		// Token: 0x06002AFC RID: 11004 RVA: 0x0008B66E File Offset: 0x0008986E
		public override void SetOwner(IGridEditableColumn owner)
		{
			this._owner = (owner as GridAutoCompleteColumn);
		}

		// Token: 0x06002AFD RID: 11005 RVA: 0x0008B67C File Offset: 0x0008987C
		protected override void AddControlsToContainer()
		{
			this.ContainerControl.Controls.Add(this._radAutoCompleteBox);
			this._radAutoCompleteBox.DataSource = this.DataSource;
			this._radAutoCompleteBox.InputType = this.InputType;
			this._radAutoCompleteBox.Filter = this.Filter;
			this._radAutoCompleteBox.AllowCustomEntry = this.AllowCustomEntry;
			this._radAutoCompleteBox.TextSettings.SelectionMode = this.SelectionMode;
			this._radAutoCompleteBox.TokensSettings.AllowTokenEditing = this.AllowTokenEditing;
			this._radAutoCompleteBox.Delimiter = this.Delimiter;
			this._radAutoCompleteBox.DataTextField = this.DataTextField;
			this._radAutoCompleteBox.DataValueField = this.DataValueField;
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x0008B742 File Offset: 0x00089942
		protected override void LoadControlsFromContainer()
		{
			this._radAutoCompleteBox = (this.ContainerControl.FindControl(string.Format("RACB_{0}", this._owner.UniqueName)) as RadAutoCompleteBox);
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x06002AFF RID: 11007 RVA: 0x0008B76F File Offset: 0x0008996F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override bool IsInitialized
		{
			get
			{
				return base.IsInitialized && this._radAutoCompleteBox != null;
			}
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x0008B7A8 File Offset: 0x000899A8
		protected override void CreateControls()
		{
			this._radAutoCompleteBox = new RadAutoCompleteBox();
			this._radAutoCompleteBox.ID = string.Format("RACB_{0}", this._owner.UniqueName);
			this._radAutoCompleteBox.RenderMode = this._owner.Owner.OwnerGrid.RenderMode;
			this.InputType = this._owner.InputType;
			this.Filter = this._owner.Filter;
			this.AllowCustomEntry = this._owner.AllowCustomEntry;
			this.SelectionMode = this._owner.SelectionMode;
			this.AllowTokenEditing = this._owner.AllowTokenEditing;
			this.Delimiter = this._owner.Delimiter;
			this.DataTextField = this._owner.DataTextField;
			this.DataValueField = this._owner.DataValueField;
			this._radAutoCompleteBox.EnableEmbeddedSkins = this._owner.Owner.OwnerGrid.EnableEmbeddedSkins;
			this._radAutoCompleteBox.Skin = this._owner.Owner.OwnerGrid.RuntimeSkin;
			RadSkinManager current = RadSkinManager.GetCurrent(this._owner.Owner.OwnerGrid.Page);
			if (current != null && current.ShowChooser)
			{
				current.SkinChanged += delegate(object sender, SkinChangedEventArgs args)
				{
					string runtimeSkin = this._radAutoCompleteBox.RuntimeSkin;
					this._radAutoCompleteBox.Skin = args.Skin;
				};
			}
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x0008B907 File Offset: 0x00089B07
		public override void DataBind()
		{
			this.AutoCompleteBox.DataBind();
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x06002B02 RID: 11010 RVA: 0x0008B914 File Offset: 0x00089B14
		// (set) Token: 0x06002B03 RID: 11011 RVA: 0x0008B921 File Offset: 0x00089B21
		public virtual string Text
		{
			get
			{
				return this.AutoCompleteBox.Text;
			}
			set
			{
				this.AutoCompleteBox.PopulateFromString(value);
			}
		}

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x06002B04 RID: 11012 RVA: 0x0008B92F File Offset: 0x00089B2F
		// (set) Token: 0x06002B05 RID: 11013 RVA: 0x0008B937 File Offset: 0x00089B37
		public virtual object DataSource { get; set; }

		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x06002B06 RID: 11014 RVA: 0x0008B940 File Offset: 0x00089B40
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public RadAutoCompleteBox AutoCompleteBox
		{
			get
			{
				this.EnsureControlsCreated();
				return this._radAutoCompleteBox;
			}
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x0008B950 File Offset: 0x00089B50
		internal override void CopySettingsFrom(IGridColumnEditor editor)
		{
			base.CopySettingsFrom(editor);
			GridAutoCompleteColumnEditor gridAutoCompleteColumnEditor = editor as GridAutoCompleteColumnEditor;
			if (gridAutoCompleteColumnEditor != null)
			{
				GridAutoCompleteColumnEditor gridAutoCompleteColumnEditor2 = (GridAutoCompleteColumnEditor)gridAutoCompleteColumnEditor.MemberwiseClone();
				if (gridAutoCompleteColumnEditor2._owner == null)
				{
					gridAutoCompleteColumnEditor2.SetOwner(this._owner);
				}
				if (gridAutoCompleteColumnEditor2.AutoCompleteBox != null)
				{
					this.EnsureControlsCreated();
					this._radAutoCompleteBox = gridAutoCompleteColumnEditor2.AutoCompleteBox;
					if (this._owner != null)
					{
						this.AutoCompleteBox.ID = string.Format("RACB_{0}", this._owner.UniqueName);
					}
				}
				this.InputType = gridAutoCompleteColumnEditor.InputType;
				this.Filter = gridAutoCompleteColumnEditor.Filter;
				this.AllowCustomEntry = gridAutoCompleteColumnEditor.AllowCustomEntry;
				this.SelectionMode = gridAutoCompleteColumnEditor.SelectionMode;
				this.AllowTokenEditing = gridAutoCompleteColumnEditor.AllowTokenEditing;
				this.Delimiter = gridAutoCompleteColumnEditor.Delimiter;
				this.DataTextField = gridAutoCompleteColumnEditor.DataTextField;
				this.DataValueField = gridAutoCompleteColumnEditor.DataValueField;
			}
		}

		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x06002B08 RID: 11016 RVA: 0x0008BA33 File Offset: 0x00089C33
		// (set) Token: 0x06002B09 RID: 11017 RVA: 0x0008BA3B File Offset: 0x00089C3B
		public RadAutoCompleteInputType InputType { get; set; }

		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x06002B0A RID: 11018 RVA: 0x0008BA44 File Offset: 0x00089C44
		// (set) Token: 0x06002B0B RID: 11019 RVA: 0x0008BA4C File Offset: 0x00089C4C
		public RadAutoCompleteFilter Filter { get; set; }

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x06002B0C RID: 11020 RVA: 0x0008BA55 File Offset: 0x00089C55
		// (set) Token: 0x06002B0D RID: 11021 RVA: 0x0008BA5D File Offset: 0x00089C5D
		public bool AllowCustomEntry { get; set; }

		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x06002B0E RID: 11022 RVA: 0x0008BA66 File Offset: 0x00089C66
		// (set) Token: 0x06002B0F RID: 11023 RVA: 0x0008BA6E File Offset: 0x00089C6E
		public RadAutoCompleteSelectionMode SelectionMode { get; set; }

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x06002B10 RID: 11024 RVA: 0x0008BA77 File Offset: 0x00089C77
		// (set) Token: 0x06002B11 RID: 11025 RVA: 0x0008BA7F File Offset: 0x00089C7F
		public bool AllowTokenEditing { get; set; }

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x06002B12 RID: 11026 RVA: 0x0008BA88 File Offset: 0x00089C88
		// (set) Token: 0x06002B13 RID: 11027 RVA: 0x0008BA90 File Offset: 0x00089C90
		public string Delimiter { get; set; }

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x06002B14 RID: 11028 RVA: 0x0008BA99 File Offset: 0x00089C99
		// (set) Token: 0x06002B15 RID: 11029 RVA: 0x0008BAA1 File Offset: 0x00089CA1
		public virtual string DataTextField { get; set; }

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x06002B16 RID: 11030 RVA: 0x0008BAAA File Offset: 0x00089CAA
		// (set) Token: 0x06002B17 RID: 11031 RVA: 0x0008BAB2 File Offset: 0x00089CB2
		public virtual string DataValueField { get; set; }

		// Token: 0x04000B40 RID: 2880
		private GridAutoCompleteColumn _owner;

		// Token: 0x04000B41 RID: 2881
		private RadAutoCompleteBox _radAutoCompleteBox;
	}
}
