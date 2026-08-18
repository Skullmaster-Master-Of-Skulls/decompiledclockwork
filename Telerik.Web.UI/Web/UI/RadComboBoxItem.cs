using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web;
using System.Web.UI;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000A11 RID: 2577
	[DefaultProperty("Text")]
	[XmlRoot("Item")]
	[ToolboxItem(false)]
	public class RadComboBoxItem : ControlItem, IComparable
	{
		// Token: 0x17001FF9 RID: 8185
		// (get) Token: 0x06006195 RID: 24981 RVA: 0x00170248 File Offset: 0x0016E448
		protected RenderMode RenderMode
		{
			get
			{
				if (this.Owner == null)
				{
					return RenderMode.Classic;
				}
				return this.Owner.RenderMode;
			}
		}

		// Token: 0x06006196 RID: 24982 RVA: 0x00170260 File Offset: 0x0016E460
		protected virtual void WriteAttributesToOptionBeginTag(HtmlTextWriter writer)
		{
			string text = string.Empty;
			if (this.IsSeparator)
			{
				text += " rcbSeparator";
			}
			if (!string.IsNullOrEmpty(this.CssClass))
			{
				text = text + " " + this.CssClass;
			}
			if (!string.IsNullOrEmpty(text))
			{
				writer.WriteAttribute("class", text);
			}
			if (this.Selected && base.Index > 0)
			{
				writer.WriteAttribute("selected", "selected");
			}
			if (!string.IsNullOrEmpty(this.Value))
			{
				writer.WriteAttribute("value", this.Value);
			}
			if (!this.Enabled || this.IsSeparator)
			{
				writer.WriteAttribute("disabled", "disabled");
			}
		}

		// Token: 0x06006197 RID: 24983 RVA: 0x00170319 File Offset: 0x0016E519
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			if (!this.Owner.IsNativeMode)
			{
				base.RenderBeginTag(writer);
			}
		}

		// Token: 0x06006198 RID: 24984 RVA: 0x0017032F File Offset: 0x0016E52F
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			if (!this.Owner.IsNativeMode)
			{
				base.RenderEndTag(writer);
			}
		}

		// Token: 0x17001FFA RID: 8186
		// (get) Token: 0x06006199 RID: 24985 RVA: 0x00170345 File Offset: 0x0016E545
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Li;
			}
		}

		// Token: 0x0600619A RID: 24986 RVA: 0x00170349 File Offset: 0x0016E549
		protected override ControlItemCollection CreateChildItemCollection()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600619B RID: 24987 RVA: 0x00170350 File Offset: 0x0016E550
		protected override object SaveViewState()
		{
			object obj = base.SaveViewState();
			if (obj != null)
			{
				return obj;
			}
			return new object[]
			{
				obj
			};
		}

		// Token: 0x0600619C RID: 24988 RVA: 0x00170378 File Offset: 0x0016E578
		protected override void LoadViewState(object savedState)
		{
			if (!(savedState is object[]))
			{
				base.LoadViewState(savedState);
			}
		}

		// Token: 0x0600619D RID: 24989 RVA: 0x00170398 File Offset: 0x0016E598
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string text = "rcbItem " + this.CssClass;
			if (!this.Enabled)
			{
				text = "rcbDisabled " + this.CssClass;
			}
			if (this.IsSeparator)
			{
				text = text + " rcbSeparator " + this.CssClass;
			}
			if (this.ComboBoxParent != null)
			{
				if (this.Templated)
				{
					text += " rcbTemplate";
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, text.TrimEnd(new char[0]));
			}
			string cssClass = this.CssClass;
			this.CssClass = string.Empty;
			base.AddAttributes(writer);
			this.CssClass = cssClass;
		}

		// Token: 0x0600619E RID: 24990 RVA: 0x0017043C File Offset: 0x0016E63C
		protected override void RenderContents(HtmlTextWriter writer)
		{
			bool checkBoxes = this.Owner.CheckBoxes;
			if (this.Owner.IsNativeMode)
			{
				writer.WriteBeginTag("option");
				this.WriteAttributesToOptionBeginTag(writer);
				writer.Write(">");
				writer.Write(HttpUtility.HtmlEncode(this.Text));
				writer.WriteEndTag("option");
				return;
			}
			if (this.Owner.ItemTemplate != null || this.Controls.Count > 0 || this.Controls.IsReadOnly)
			{
				if (checkBoxes)
				{
					this.RenderTemplateWithCheckBoxes(writer);
					return;
				}
				this.RenderChildren(writer);
				return;
			}
			else
			{
				if (checkBoxes)
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Label);
					this.RenderCheckBox(writer);
					this.RenderItemContent(writer);
					writer.RenderEndTag();
					return;
				}
				this.RenderItemContent(writer);
				return;
			}
		}

		// Token: 0x0600619F RID: 24991 RVA: 0x001704FE File Offset: 0x0016E6FE
		private void RenderTemplateWithCheckBoxes(HtmlTextWriter writer)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Label);
			this.RenderCheckBox(writer);
			writer.RenderEndTag();
			this.RenderChildren(writer);
		}

		// Token: 0x060061A0 RID: 24992 RVA: 0x0017051C File Offset: 0x0016E71C
		private void RenderItemContent(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(this.CurrentImageUrl))
			{
				this.RenderImage(writer);
			}
			writer.Write(HttpUtility.HtmlEncode(this.Text));
		}

		// Token: 0x060061A1 RID: 24993 RVA: 0x00170544 File Offset: 0x0016E744
		private void RenderCheckBox(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcbCheckBox");
			if (this.Checked)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
			}
			if (!this.Enabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
		}

		// Token: 0x060061A2 RID: 24994 RVA: 0x001705A3 File Offset: 0x0016E7A3
		private void RenderImage(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Alt, this.ToolTip);
			writer.AddAttribute(HtmlTextWriterAttribute.Src, base.ResolveClientUrl(this.CurrentImageUrl));
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcbImage");
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
		}

		// Token: 0x17001FFB RID: 8187
		// (get) Token: 0x060061A3 RID: 24995 RVA: 0x001705E1 File Offset: 0x0016E7E1
		internal string CurrentImageUrl
		{
			get
			{
				if (!this.Enabled && !string.IsNullOrEmpty(this.DisabledImageUrl))
				{
					return this.DisabledImageUrl;
				}
				return this.ImageUrl;
			}
		}

		// Token: 0x17001FFC RID: 8188
		// (get) Token: 0x060061A4 RID: 24996 RVA: 0x00170605 File Offset: 0x0016E805
		// (set) Token: 0x060061A5 RID: 24997 RVA: 0x00170626 File Offset: 0x0016E826
		internal bool VisibleInternal
		{
			get
			{
				return (bool)(this.ViewState["VisibleInternal"] ?? true);
			}
			set
			{
				this.ViewState["VisibleInternal"] = value;
			}
		}

		// Token: 0x060061A6 RID: 24998 RVA: 0x00170640 File Offset: 0x0016E840
		internal override void PopulateFromDataItem(PropertyDescriptorCache properties, object dataItem, string dataMember, int depth)
		{
			base.PopulateFromDataItem(properties, dataItem, dataMember, depth);
			if (string.IsNullOrEmpty(base.Container.DataTextField) && string.IsNullOrEmpty(base.Container.DataValueField))
			{
				this.Value = dataItem.ToString();
			}
			if ((base.Container as RadComboBox).CheckBoxes)
			{
				string dataCheckedField = (base.Container as RadComboBox).DataCheckedField;
				if (!string.IsNullOrEmpty(dataCheckedField))
				{
					this.Checked = bool.Parse(DataBinder.GetPropertyValue(dataItem, dataCheckedField, null));
				}
			}
		}

		// Token: 0x060061A7 RID: 24999 RVA: 0x001706C8 File Offset: 0x0016E8C8
		protected internal override void LoadFromDictionary(IDictionary<string, object> dictionary)
		{
			base.LoadFromDictionary(dictionary);
			if (dictionary.ContainsKey("checked") && (this.Parent as RadComboBox).CheckBoxes)
			{
				this.Checked = Convert.ToBoolean(dictionary["checked"].ToString());
			}
			if (dictionary.ContainsKey("isSeparator"))
			{
				this.IsSeparator = Convert.ToBoolean(dictionary["isSeparator"].ToString());
			}
		}

		// Token: 0x060061A8 RID: 25000 RVA: 0x0017073E File Offset: 0x0016E93E
		public RadComboBoxItem()
		{
		}

		// Token: 0x060061A9 RID: 25001 RVA: 0x00170746 File Offset: 0x0016E946
		public RadComboBoxItem(string text) : this()
		{
			this.Text = text;
		}

		// Token: 0x060061AA RID: 25002 RVA: 0x00170755 File Offset: 0x0016E955
		public RadComboBoxItem(string text, string value) : this()
		{
			this.Text = text;
			this.Value = value;
		}

		// Token: 0x17001FFD RID: 8189
		// (get) Token: 0x060061AB RID: 25003 RVA: 0x0017076B File Offset: 0x0016E96B
		// (set) Token: 0x060061AC RID: 25004 RVA: 0x00170773 File Offset: 0x0016E973
		[DefaultValue("")]
		[Description("The display text of the item.")]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		// Token: 0x17001FFE RID: 8190
		// (get) Token: 0x060061AD RID: 25005 RVA: 0x0017077C File Offset: 0x0016E97C
		// (set) Token: 0x060061AE RID: 25006 RVA: 0x00170784 File Offset: 0x0016E984
		[DefaultValue("")]
		[Category("Misc")]
		[Description("The value of the combobox item")]
		public override string Value
		{
			get
			{
				return base.Value;
			}
			set
			{
				base.Value = value;
			}
		}

		// Token: 0x17001FFF RID: 8191
		// (get) Token: 0x060061AF RID: 25007 RVA: 0x0017078D File Offset: 0x0016E98D
		// (set) Token: 0x060061B0 RID: 25008 RVA: 0x00170795 File Offset: 0x0016E995
		[Browsable(false)]
		public RadComboBox Owner
		{
			get
			{
				return this._owner;
			}
			internal set
			{
				this._owner = value;
			}
		}

		// Token: 0x17002000 RID: 8192
		// (get) Token: 0x060061B1 RID: 25009 RVA: 0x0017079E File Offset: 0x0016E99E
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public RadComboBox ComboBoxParent
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x17002001 RID: 8193
		// (get) Token: 0x060061B2 RID: 25010 RVA: 0x001707A6 File Offset: 0x0016E9A6
		// (set) Token: 0x060061B3 RID: 25011 RVA: 0x001707C7 File Offset: 0x0016E9C7
		[DefaultValue(false)]
		public bool Selected
		{
			get
			{
				return (bool)(this.ViewState["Selected"] ?? false);
			}
			set
			{
				if (value && this.Owner != null)
				{
					this.Owner.InternalClearSelection();
				}
				this.ViewState["Selected"] = value;
			}
		}

		// Token: 0x17002002 RID: 8194
		// (get) Token: 0x060061B4 RID: 25012 RVA: 0x001707F5 File Offset: 0x0016E9F5
		// (set) Token: 0x060061B5 RID: 25013 RVA: 0x001707FD File Offset: 0x0016E9FD
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				this.VisibleInternal = value;
				base.Visible = value;
			}
		}

		// Token: 0x17002003 RID: 8195
		// (get) Token: 0x060061B6 RID: 25014 RVA: 0x0017080D File Offset: 0x0016EA0D
		// (set) Token: 0x060061B7 RID: 25015 RVA: 0x0017082E File Offset: 0x0016EA2E
		[Description("Whether the RadComboBoxItem is checked or not")]
		[DefaultValue(false)]
		[Category("Behavior")]
		public bool Checked
		{
			get
			{
				return (bool)(this.ViewState["Checked"] ?? false);
			}
			set
			{
				this.ViewState["Checked"] = value;
			}
		}

		// Token: 0x17002004 RID: 8196
		// (get) Token: 0x060061B8 RID: 25016 RVA: 0x00170846 File Offset: 0x0016EA46
		// (set) Token: 0x060061B9 RID: 25017 RVA: 0x00170866 File Offset: 0x0016EA66
		[DefaultValue("")]
		public override string ToolTip
		{
			get
			{
				return (string)(this.ViewState["ToolTip"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ToolTip"] = value;
			}
		}

		// Token: 0x17002005 RID: 8197
		// (get) Token: 0x060061BA RID: 25018 RVA: 0x00170879 File Offset: 0x0016EA79
		// (set) Token: 0x060061BB RID: 25019 RVA: 0x0017089A File Offset: 0x0016EA9A
		[Description("Sets/gets that the item is separator. It also represents a logical state of the item. Might be used in some applications like keyboard navigation to omit processing of items that are marked like separators.")]
		[DefaultValue(false)]
		[Category("Behavior")]
		public bool IsSeparator
		{
			get
			{
				return (bool)(this.ViewState["IsSeparator"] ?? false);
			}
			set
			{
				this.ViewState["IsSeparator"] = value;
			}
		}

		// Token: 0x17002006 RID: 8198
		// (get) Token: 0x060061BC RID: 25020 RVA: 0x001708B4 File Offset: 0x0016EAB4
		[DefaultValue(null)]
		internal Hashtable CustomAttributes
		{
			get
			{
				Hashtable hashtable = new Hashtable();
				ICollection keys = base.Attributes.Keys;
				foreach (object obj in keys)
				{
					string key = (string)obj;
					hashtable.Add(key, base.Attributes[key]);
				}
				if (hashtable.Count > 0)
				{
					return hashtable;
				}
				return null;
			}
		}

		// Token: 0x17002007 RID: 8199
		// (get) Token: 0x060061BD RID: 25021 RVA: 0x00170938 File Offset: 0x0016EB38
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool EnableViewState
		{
			get
			{
				return base.EnableViewState;
			}
		}

		// Token: 0x17002008 RID: 8200
		// (get) Token: 0x060061BE RID: 25022 RVA: 0x00170940 File Offset: 0x0016EB40
		// (set) Token: 0x060061BF RID: 25023 RVA: 0x0017096F File Offset: 0x0016EB6F
		[Description("The URL for the image for the Item.")]
		[DefaultValue("")]
		[Category("Appearance")]
		[UrlProperty]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string ImageUrl
		{
			get
			{
				if (this.ViewState["ImageUrl"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["ImageUrl"];
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x17002009 RID: 8201
		// (get) Token: 0x060061C0 RID: 25024 RVA: 0x00170982 File Offset: 0x0016EB82
		// (set) Token: 0x060061C1 RID: 25025 RVA: 0x001709B1 File Offset: 0x0016EBB1
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The path to an image to display for the item when it is disabled.")]
		[UrlProperty]
		[Category("Appearance")]
		[DefaultValue("")]
		public string DisabledImageUrl
		{
			get
			{
				if (this.ViewState["DisabledImageUrl"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["DisabledImageUrl"];
			}
			set
			{
				this.ViewState["DisabledImageUrl"] = value;
			}
		}

		// Token: 0x060061C2 RID: 25026 RVA: 0x001709C4 File Offset: 0x0016EBC4
		public int CompareTo(object obj)
		{
			RadComboBoxItem radComboBoxItem = obj as RadComboBoxItem;
			if (radComboBoxItem == null)
			{
				throw new ArgumentException();
			}
			if (this.ComboBoxParent == null)
			{
				throw new Exception("Cannot sort items that are not added to a combobox instance.");
			}
			int result = 0;
			if (this.ComboBoxParent.Sort == RadComboBoxSort.Ascending)
			{
				result = string.Compare(this.Text, radComboBoxItem.Text, !this.ComboBoxParent.SortCaseSensitive);
			}
			if (this.ComboBoxParent.Sort == RadComboBoxSort.Descending)
			{
				result = string.Compare(this.Text, radComboBoxItem.Text, !this.ComboBoxParent.SortCaseSensitive) * -1;
			}
			return result;
		}

		// Token: 0x060061C3 RID: 25027 RVA: 0x00170A55 File Offset: 0x0016EC55
		public void Remove()
		{
			if (this.ComboBoxParent != null)
			{
				this.ComboBoxParent.Items.Remove(this.ComboBoxParent.Items.IndexOf(this));
				return;
			}
			throw new Exception("Cannot remove a RadComboBoxItem that has not been added to a RadComboBox parent.");
		}

		// Token: 0x040017F4 RID: 6132
		private RadComboBox _owner;
	}
}
