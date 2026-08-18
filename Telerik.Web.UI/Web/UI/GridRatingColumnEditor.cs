using System;
using System.ComponentModel;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x020018F8 RID: 6392
	[TelerikToolboxCategory("Data")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	public class GridRatingColumnEditor : GridColumnEditorBase
	{
		// Token: 0x0600F62F RID: 63023 RVA: 0x0037DB25 File Offset: 0x0037BD25
		public GridRatingColumnEditor()
		{
		}

		// Token: 0x0600F630 RID: 63024 RVA: 0x0037DB2D File Offset: 0x0037BD2D
		public GridRatingColumnEditor(GridRatingColumn owner)
		{
			this._owner = owner;
		}

		// Token: 0x0600F631 RID: 63025 RVA: 0x0037DB3C File Offset: 0x0037BD3C
		public override void SetOwner(IGridEditableColumn owner)
		{
			this._owner = (owner as GridRatingColumn);
		}

		// Token: 0x0600F632 RID: 63026 RVA: 0x0037DB4C File Offset: 0x0037BD4C
		protected override void AddControlsToContainer()
		{
			this._radRating.EnableEmbeddedSkins = this._owner.Owner.OwnerGrid.EnableEmbeddedSkins;
			this._radRating.EnableEmbeddedBaseStylesheet = this._owner.Owner.OwnerGrid.EnableEmbeddedBaseStylesheet;
			this._radRating.EnableEmbeddedScripts = this._owner.Owner.OwnerGrid.EnableEmbeddedScripts;
			this._radRating.RegisterWithScriptManager = this._owner.Owner.OwnerGrid.RegisterWithScriptManager;
			this._radRating.ItemCount = this._owner.ItemCount;
			this._radRating.SelectionMode = this._owner.SelectionMode;
			this._radRating.Precision = this._owner.Precision;
			this._radRating.IsDirectionReversed = this._owner.IsDirectionReversed;
			this._radRating.PreRender += this._radRating_PreRender;
			this.ContainerControl.Controls.Add(this._radRating);
		}

		// Token: 0x0600F633 RID: 63027 RVA: 0x0037DC5E File Offset: 0x0037BE5E
		private void _radRating_PreRender(object sender, EventArgs e)
		{
			(sender as RadRating).Skin = this._owner.Owner.OwnerGrid.RuntimeSkin;
		}

		// Token: 0x0600F634 RID: 63028 RVA: 0x0037DC80 File Offset: 0x0037BE80
		protected override void LoadControlsFromContainer()
		{
			this._radRating = (this.ContainerControl.FindControl(this.GetRatingControlID()) as RadRating);
		}

		// Token: 0x17004A1A RID: 18970
		// (get) Token: 0x0600F635 RID: 63029 RVA: 0x0037DC9E File Offset: 0x0037BE9E
		public RadRating RatingControl
		{
			get
			{
				this.EnsureControlsCreated();
				return this._radRating;
			}
		}

		// Token: 0x17004A1B RID: 18971
		// (get) Token: 0x0600F636 RID: 63030 RVA: 0x0037DCAC File Offset: 0x0037BEAC
		public decimal Value
		{
			get
			{
				return this.RatingControl.Value;
			}
		}

		// Token: 0x0600F637 RID: 63031 RVA: 0x0037DCB9 File Offset: 0x0037BEB9
		protected override void CreateControls()
		{
			this._radRating = new RadRating();
			this._radRating.RenderMode = this._owner.Owner.OwnerGrid.RenderMode;
			this._radRating.ID = this.GetRatingControlID();
		}

		// Token: 0x0600F638 RID: 63032 RVA: 0x0037DCF7 File Offset: 0x0037BEF7
		internal string GetRatingControlID()
		{
			return string.Format("Rating_{0}", this._owner.UniqueName);
		}

		// Token: 0x17004A1C RID: 18972
		// (get) Token: 0x0600F639 RID: 63033 RVA: 0x0037DD0E File Offset: 0x0037BF0E
		public override bool IsInitialized
		{
			get
			{
				return base.IsInitialized && this._radRating != null;
			}
		}

		// Token: 0x0400467C RID: 18044
		private GridRatingColumn _owner;

		// Token: 0x0400467D RID: 18045
		private RadRating _radRating;
	}
}
