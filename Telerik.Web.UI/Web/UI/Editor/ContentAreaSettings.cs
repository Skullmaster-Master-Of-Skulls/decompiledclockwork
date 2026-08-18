using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x0200104C RID: 4172
	public class ContentAreaSettings : StateManager, IAttributeAccessor
	{
		// Token: 0x0600A3DE RID: 41950 RVA: 0x00246E34 File Offset: 0x00245034
		internal ContentAreaSettings()
		{
		}

		// Token: 0x170033B7 RID: 13239
		// (get) Token: 0x0600A3DF RID: 41951 RVA: 0x00246E3C File Offset: 0x0024503C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Telerik.Web.UI.Editor.AttributeCollection Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					if (this._attributeState == null)
					{
						this._attributeState = new StateBag(true);
						if (this.IsTrackingViewState)
						{
							((IStateManager)this._attributeState).TrackViewState();
						}
					}
					this._attributes = new Telerik.Web.UI.Editor.AttributeCollection(this._attributeState);
				}
				return this._attributes;
			}
		}

		// Token: 0x170033B8 RID: 13240
		// (get) Token: 0x0600A3E0 RID: 41952 RVA: 0x00246E8F File Offset: 0x0024508F
		// (set) Token: 0x0600A3E1 RID: 41953 RVA: 0x00246EAA File Offset: 0x002450AA
		[TypeConverter(typeof(WebColorConverter))]
		[DefaultValue(typeof(Color), "")]
		public virtual Color BackColor
		{
			get
			{
				if (!this.ControlStyleCreated)
				{
					return Color.Empty;
				}
				return this.ControlStyle.BackColor;
			}
			set
			{
				this.ControlStyle.BackColor = value;
			}
		}

		// Token: 0x170033B9 RID: 13241
		// (get) Token: 0x0600A3E2 RID: 41954 RVA: 0x00246EB8 File Offset: 0x002450B8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public FontInfo Font
		{
			get
			{
				return this.ControlStyle.Font;
			}
		}

		// Token: 0x170033BA RID: 13242
		// (get) Token: 0x0600A3E3 RID: 41955 RVA: 0x00246EC5 File Offset: 0x002450C5
		// (set) Token: 0x0600A3E4 RID: 41956 RVA: 0x00246EE0 File Offset: 0x002450E0
		[TypeConverter(typeof(WebColorConverter))]
		[DefaultValue(typeof(Color), "")]
		public virtual Color ForeColor
		{
			get
			{
				if (!this.ControlStyleCreated)
				{
					return Color.Empty;
				}
				return this.ControlStyle.ForeColor;
			}
			set
			{
				this.ControlStyle.ForeColor = value;
			}
		}

		// Token: 0x170033BB RID: 13243
		// (get) Token: 0x0600A3E5 RID: 41957 RVA: 0x00246EEE File Offset: 0x002450EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected Style ControlStyle
		{
			get
			{
				if (this._controlStyle == null)
				{
					this._controlStyle = this.CreateControlStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._controlStyle).TrackViewState();
					}
				}
				return this._controlStyle;
			}
		}

		// Token: 0x170033BC RID: 13244
		// (get) Token: 0x0600A3E6 RID: 41958 RVA: 0x00246F1D File Offset: 0x0024511D
		protected bool ControlStyleCreated
		{
			get
			{
				return this._controlStyle != null;
			}
		}

		// Token: 0x0600A3E7 RID: 41959 RVA: 0x00246F2B File Offset: 0x0024512B
		protected virtual Style CreateControlStyle()
		{
			return new Style(base.ViewState);
		}

		// Token: 0x0600A3E8 RID: 41960 RVA: 0x00246F38 File Offset: 0x00245138
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				Pair pair = (Pair)savedState;
				base.LoadViewState(pair.First);
				if (pair.Second != null)
				{
					if (this._attributeState == null)
					{
						this._attributeState = new StateBag(true);
						((IStateManager)this._attributeState).TrackViewState();
					}
					((IStateManager)this._attributeState).LoadViewState(pair.Second);
				}
			}
		}

		// Token: 0x0600A3E9 RID: 41961 RVA: 0x00246F94 File Offset: 0x00245194
		protected override object SaveViewState()
		{
			object obj = base.SaveViewState();
			object obj2 = null;
			if (this._attributeState != null)
			{
				obj2 = ((IStateManager)this._attributeState).SaveViewState();
			}
			if (obj == null && obj2 == null)
			{
				return null;
			}
			return new Pair(obj, obj2);
		}

		// Token: 0x0600A3EA RID: 41962 RVA: 0x00246FCD File Offset: 0x002451CD
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._attributeState != null)
			{
				((IStateManager)this._attributeState).TrackViewState();
			}
		}

		// Token: 0x0600A3EB RID: 41963 RVA: 0x00246FE8 File Offset: 0x002451E8
		string IAttributeAccessor.GetAttribute(string key)
		{
			if (this._attributeState == null)
			{
				return null;
			}
			return (string)this._attributeState[key];
		}

		// Token: 0x0600A3EC RID: 41964 RVA: 0x00247005 File Offset: 0x00245205
		void IAttributeAccessor.SetAttribute(string key, string value)
		{
			this.Attributes[key] = value;
		}

		// Token: 0x04002DAA RID: 11690
		private Telerik.Web.UI.Editor.AttributeCollection _attributes;

		// Token: 0x04002DAB RID: 11691
		private StateBag _attributeState;

		// Token: 0x04002DAC RID: 11692
		private Style _controlStyle;
	}
}
