using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000456 RID: 1110
	[ControlBuilder(typeof(ListItemControlBuilder))]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true, "Text")]
	public sealed class ListItem : IStateManager, IParserAccessor, IAttributeAccessor
	{
		// Token: 0x060035AD RID: 13741 RVA: 0x000AE0C0 File Offset: 0x000AC2C0
		public ListItem() : this(null, null)
		{
		}

		// Token: 0x060035AE RID: 13742 RVA: 0x000AE0CA File Offset: 0x000AC2CA
		public ListItem(string text) : this(text, null)
		{
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x000AE0D4 File Offset: 0x000AC2D4
		public ListItem(string text, string value) : this(text, value, true)
		{
		}

		// Token: 0x060035B0 RID: 13744 RVA: 0x000AE0DF File Offset: 0x000AC2DF
		public ListItem(string text, string value, bool enabled)
		{
			this.text = text;
			this.value = value;
			this.enabled = enabled;
		}

		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x060035B1 RID: 13745 RVA: 0x000AE0FC File Offset: 0x000AC2FC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AttributeCollection Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					this._attributes = new AttributeCollection(new StateBag(true));
				}
				return this._attributes;
			}
		}

		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x060035B2 RID: 13746 RVA: 0x000AE11D File Offset: 0x000AC31D
		// (set) Token: 0x060035B3 RID: 13747 RVA: 0x000AE137 File Offset: 0x000AC337
		internal bool Dirty
		{
			get
			{
				return this.textisdirty || this.valueisdirty || this.enabledisdirty;
			}
			set
			{
				this.textisdirty = value;
				this.valueisdirty = value;
				this.enabledisdirty = value;
			}
		}

		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x060035B4 RID: 13748 RVA: 0x000AE14E File Offset: 0x000AC34E
		// (set) Token: 0x060035B5 RID: 13749 RVA: 0x000AE156 File Offset: 0x000AC356
		[DefaultValue(true)]
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
				if (((IStateManager)this).IsTrackingViewState)
				{
					this.enabledisdirty = true;
				}
			}
		}

		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x060035B6 RID: 13750 RVA: 0x000AE16E File Offset: 0x000AC36E
		internal bool HasAttributes
		{
			get
			{
				return this._attributes != null && this._attributes.Count > 0;
			}
		}

		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x060035B7 RID: 13751 RVA: 0x000AE188 File Offset: 0x000AC388
		// (set) Token: 0x060035B8 RID: 13752 RVA: 0x000AE190 File Offset: 0x000AC390
		[DefaultValue(false)]
		[TypeConverter(typeof(MinimizableAttributeTypeConverter))]
		public bool Selected
		{
			get
			{
				return this.selected;
			}
			set
			{
				this.selected = value;
			}
		}

		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x060035B9 RID: 13753 RVA: 0x000AE199 File Offset: 0x000AC399
		// (set) Token: 0x060035BA RID: 13754 RVA: 0x000AE1BE File Offset: 0x000AC3BE
		[Localizable(true)]
		[DefaultValue("")]
		[PersistenceMode(PersistenceMode.EncodedInnerDefaultProperty)]
		public string Text
		{
			get
			{
				if (this.text != null)
				{
					return this.text;
				}
				if (this.value != null)
				{
					return this.value;
				}
				return string.Empty;
			}
			set
			{
				this.text = value;
				if (((IStateManager)this).IsTrackingViewState)
				{
					this.textisdirty = true;
				}
			}
		}

		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x060035BB RID: 13755 RVA: 0x000AE1D6 File Offset: 0x000AC3D6
		// (set) Token: 0x060035BC RID: 13756 RVA: 0x000AE1FB File Offset: 0x000AC3FB
		[Localizable(true)]
		[DefaultValue("")]
		public string Value
		{
			get
			{
				if (this.value != null)
				{
					return this.value;
				}
				if (this.text != null)
				{
					return this.text;
				}
				return string.Empty;
			}
			set
			{
				this.value = value;
				if (((IStateManager)this).IsTrackingViewState)
				{
					this.valueisdirty = true;
				}
			}
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x000AE213 File Offset: 0x000AC413
		public override int GetHashCode()
		{
			return HashCodeCombiner.CombineHashCodes(this.Value.GetHashCode(), this.Text.GetHashCode());
		}

		// Token: 0x060035BE RID: 13758 RVA: 0x000AE230 File Offset: 0x000AC430
		public override bool Equals(object o)
		{
			ListItem listItem = o as ListItem;
			return listItem != null && this.Value.Equals(listItem.Value) && this.Text.Equals(listItem.Text);
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x000AE26F File Offset: 0x000AC46F
		public static ListItem FromString(string s)
		{
			return new ListItem(s);
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x000AE277 File Offset: 0x000AC477
		public override string ToString()
		{
			return this.Text;
		}

		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x060035C1 RID: 13761 RVA: 0x000AE27F File Offset: 0x000AC47F
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.marked;
			}
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x000AE287 File Offset: 0x000AC487
		void IStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x000AE290 File Offset: 0x000AC490
		internal void LoadViewState(object state)
		{
			if (state != null)
			{
				if (state is Triplet)
				{
					Triplet triplet = (Triplet)state;
					if (triplet.First != null)
					{
						this.Text = (string)triplet.First;
					}
					if (triplet.Second != null)
					{
						this.Value = (string)triplet.Second;
					}
					if (triplet.Third == null)
					{
						return;
					}
					try
					{
						this.Enabled = (bool)triplet.Third;
						return;
					}
					catch
					{
						return;
					}
				}
				if (state is Pair)
				{
					Pair pair = (Pair)state;
					if (pair.First != null)
					{
						this.Text = (string)pair.First;
					}
					this.Value = (string)pair.Second;
					return;
				}
				this.Text = (string)state;
			}
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x000AE358 File Offset: 0x000AC558
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x000AE360 File Offset: 0x000AC560
		internal void TrackViewState()
		{
			this.marked = true;
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x000AE369 File Offset: 0x000AC569
		internal void RenderAttributes(HtmlTextWriter writer)
		{
			if (this._attributes != null)
			{
				this._attributes.AddAttributes(writer);
			}
		}

		// Token: 0x060035C7 RID: 13767 RVA: 0x000AE37F File Offset: 0x000AC57F
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x060035C8 RID: 13768 RVA: 0x000AE388 File Offset: 0x000AC588
		internal object SaveViewState()
		{
			string text = null;
			string y = null;
			if (this.textisdirty)
			{
				text = this.Text;
			}
			if (this.valueisdirty)
			{
				y = this.Value;
			}
			if (this.enabledisdirty)
			{
				return new Triplet(text, y, this.Enabled);
			}
			if (this.valueisdirty)
			{
				return new Pair(text, y);
			}
			if (this.textisdirty)
			{
				return text;
			}
			return null;
		}

		// Token: 0x060035C9 RID: 13769 RVA: 0x000AE3ED File Offset: 0x000AC5ED
		string IAttributeAccessor.GetAttribute(string name)
		{
			return this.Attributes[name];
		}

		// Token: 0x060035CA RID: 13770 RVA: 0x000AE3FB File Offset: 0x000AC5FB
		void IAttributeAccessor.SetAttribute(string name, string value)
		{
			this.Attributes[name] = value;
		}

		// Token: 0x060035CB RID: 13771 RVA: 0x000AE40C File Offset: 0x000AC60C
		void IParserAccessor.AddParsedSubObject(object obj)
		{
			if (obj is LiteralControl)
			{
				this.Text = ((LiteralControl)obj).Text;
				return;
			}
			if (obj is DataBoundLiteralControl)
			{
				throw new HttpException(SR.GetString("Control_Cannot_Databind", new object[]
				{
					"ListItem"
				}));
			}
			throw new HttpException(SR.GetString("Cannot_Have_Children_Of_Type", new object[]
			{
				"ListItem",
				obj.GetType().Name.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x060035CC RID: 13772 RVA: 0x000AE48E File Offset: 0x000AC68E
		private void ResetText()
		{
			this.Text = null;
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x000AE497 File Offset: 0x000AC697
		private void ResetValue()
		{
			this.Value = null;
		}

		// Token: 0x060035CE RID: 13774 RVA: 0x000AE4A0 File Offset: 0x000AC6A0
		private bool ShouldSerializeText()
		{
			return this.text != null && this.text.Length != 0;
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x000AE4BA File Offset: 0x000AC6BA
		private bool ShouldSerializeValue()
		{
			return this.value != null && this.value.Length != 0;
		}

		// Token: 0x040021C9 RID: 8649
		private bool selected;

		// Token: 0x040021CA RID: 8650
		private bool marked;

		// Token: 0x040021CB RID: 8651
		private bool textisdirty;

		// Token: 0x040021CC RID: 8652
		private bool valueisdirty;

		// Token: 0x040021CD RID: 8653
		private bool enabled;

		// Token: 0x040021CE RID: 8654
		private bool enabledisdirty;

		// Token: 0x040021CF RID: 8655
		private string text;

		// Token: 0x040021D0 RID: 8656
		private string value;

		// Token: 0x040021D1 RID: 8657
		private AttributeCollection _attributes;
	}
}
