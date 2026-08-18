using System;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Web.UI.Editor;

namespace Telerik.Web.UI
{
	// Token: 0x0200028B RID: 651
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class EditorToolBase : StateManager, IAttributeAccessor
	{
		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x0600171F RID: 5919
		// (set) Token: 0x06001720 RID: 5920
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public abstract EditorToolType Type { get; set; }

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x0004E18F File Offset: 0x0004C38F
		// (set) Token: 0x06001722 RID: 5922 RVA: 0x0004E1BA File Offset: 0x0004C3BA
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public virtual bool Visible
		{
			get
			{
				return base.ViewState["Visible"] == null || (bool)base.ViewState["Visible"];
			}
			set
			{
				base.ViewState["Visible"] = value;
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x0004E1D2 File Offset: 0x0004C3D2
		public virtual Telerik.Web.UI.Editor.AttributeCollection Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					this._attributes = new Telerik.Web.UI.Editor.AttributeCollection(this.AttributeState);
				}
				return this._attributes;
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06001724 RID: 5924 RVA: 0x0004E1F3 File Offset: 0x0004C3F3
		// (set) Token: 0x06001725 RID: 5925 RVA: 0x0004E222 File Offset: 0x0004C422
		[DefaultValue(RenderMode.Classic)]
		public virtual RenderMode RenderMode
		{
			get
			{
				return (RenderMode)Enum.Parse(typeof(RenderMode), this.Attributes["RenderMode"] ?? "Classic");
			}
			set
			{
				this.Attributes["RenderMode"] = value.ToString();
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06001726 RID: 5926 RVA: 0x0004E23F File Offset: 0x0004C43F
		// (set) Token: 0x06001727 RID: 5927 RVA: 0x0004E24D File Offset: 0x0004C44D
		[DesignOnly(true)]
		[Browsable(false)]
		[ScriptIgnore]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual string PopUpWidth
		{
			get
			{
				return this.Attributes[StandardDropDownProperties.PopUpWidth];
			}
			set
			{
				this.Attributes[StandardDropDownProperties.PopUpWidth] = value;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06001728 RID: 5928 RVA: 0x0004E25C File Offset: 0x0004C45C
		// (set) Token: 0x06001729 RID: 5929 RVA: 0x0004E26A File Offset: 0x0004C46A
		[ScriptIgnore]
		[Browsable(false)]
		[DesignOnly(true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual string PopUpHeight
		{
			get
			{
				return this.Attributes[StandardDropDownProperties.PopUpHeight];
			}
			set
			{
				this.Attributes[StandardDropDownProperties.PopUpHeight] = value;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x0600172A RID: 5930 RVA: 0x0004E279 File Offset: 0x0004C479
		// (set) Token: 0x0600172B RID: 5931 RVA: 0x0004E287 File Offset: 0x0004C487
		[Browsable(false)]
		[ScriptIgnore]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignOnly(true)]
		public virtual string PopUpClassName
		{
			get
			{
				return this.Attributes[StandardDropDownProperties.PopUpClassName];
			}
			set
			{
				this.Attributes[StandardDropDownProperties.PopUpClassName] = value;
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x0600172C RID: 5932 RVA: 0x0004E296 File Offset: 0x0004C496
		// (set) Token: 0x0600172D RID: 5933 RVA: 0x0004E2A4 File Offset: 0x0004C4A4
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ScriptIgnore]
		[DesignOnly(true)]
		public virtual string SizeToFit
		{
			get
			{
				return this.Attributes[StandardDropDownProperties.SizeToFit];
			}
			set
			{
				this.Attributes[StandardDropDownProperties.SizeToFit] = value;
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x0600172E RID: 5934 RVA: 0x0004E2B3 File Offset: 0x0004C4B3
		// (set) Token: 0x0600172F RID: 5935 RVA: 0x0004E2C1 File Offset: 0x0004C4C1
		[ScriptIgnore]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignOnly(true)]
		public virtual string ItemsPerRow
		{
			get
			{
				return this.Attributes[StandardDropDownProperties.ItemsPerRow];
			}
			set
			{
				this.Attributes[StandardDropDownProperties.ItemsPerRow] = value;
			}
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x0004E2D0 File Offset: 0x0004C4D0
		protected override object SaveViewState()
		{
			return new Pair(base.SaveViewState(), (this._attributeState == null) ? null : ((IStateManager)this._attributeState).SaveViewState());
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x0004E2F4 File Offset: 0x0004C4F4
		protected override void LoadViewState(object state)
		{
			Pair pair = (Pair)state;
			base.LoadViewState(pair.First);
			if (pair.Second != null)
			{
				((IStateManager)this.AttributeState).LoadViewState(pair.Second);
			}
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x0004E32D File Offset: 0x0004C52D
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._attributeState != null)
			{
				((IStateManager)this._attributeState).TrackViewState();
			}
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x0004E348 File Offset: 0x0004C548
		internal override void SetDirty()
		{
			base.SetDirty();
			if (this._attributeState != null)
			{
				this._attributeState.SetDirty(true);
			}
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x0004E364 File Offset: 0x0004C564
		string IAttributeAccessor.GetAttribute(string key)
		{
			if (this._attributeState == null)
			{
				return null;
			}
			return this.Attributes[key];
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x0004E37C File Offset: 0x0004C57C
		void IAttributeAccessor.SetAttribute(string key, string value)
		{
			this.Attributes[key] = value;
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06001736 RID: 5942 RVA: 0x0004E38B File Offset: 0x0004C58B
		private StateBag AttributeState
		{
			get
			{
				if (this._attributeState == null)
				{
					this._attributeState = new StateBag(true);
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._attributeState).TrackViewState();
					}
				}
				return this._attributeState;
			}
		}

		// Token: 0x04000616 RID: 1558
		private Telerik.Web.UI.Editor.AttributeCollection _attributes;

		// Token: 0x04000617 RID: 1559
		private StateBag _attributeState;
	}
}
