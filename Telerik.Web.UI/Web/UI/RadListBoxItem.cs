using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web;
using System.Web.UI;
using System.Xml;
using System.Xml.Serialization;
using Telerik.Web.UI.ListBox.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x02001940 RID: 6464
	[XmlRoot("Item")]
	[ToolboxItem(false)]
	public class RadListBoxItem : ControlItem, IComparable
	{
		// Token: 0x17004B97 RID: 19351
		// (get) Token: 0x0600FA24 RID: 64036 RVA: 0x0038596E File Offset: 0x00383B6E
		protected virtual IRenderer Renderer
		{
			get
			{
				if (this._renderer == null)
				{
					this._renderer = this.CreateItemRenderer();
				}
				return this._renderer;
			}
		}

		// Token: 0x0600FA25 RID: 64037 RVA: 0x0038598A File Offset: 0x00383B8A
		internal void CallBaseRenderChildren(HtmlTextWriter writer)
		{
			base.RenderChildren(writer);
		}

		// Token: 0x0600FA26 RID: 64038 RVA: 0x00385993 File Offset: 0x00383B93
		internal void CallBaseAddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
		}

		// Token: 0x17004B98 RID: 19352
		// (get) Token: 0x0600FA27 RID: 64039 RVA: 0x0038599C File Offset: 0x00383B9C
		internal bool IsItemEnabled
		{
			get
			{
				return base.IsEnabled;
			}
		}

		// Token: 0x0600FA28 RID: 64040 RVA: 0x003859A4 File Offset: 0x00383BA4
		protected internal override void LoadFromDictionary(IDictionary<string, object> dictionary)
		{
			base.LoadFromDictionary(dictionary);
			if (dictionary.ContainsKey("imageUrl"))
			{
				this.ImageUrl = dictionary["imageUrl"].ToString();
			}
			if (dictionary.ContainsKey("checked") && (this.Parent as RadListBox).CheckBoxes)
			{
				this.Checked = true;
			}
		}

		// Token: 0x0600FA29 RID: 64041 RVA: 0x00385A01 File Offset: 0x00383C01
		protected override ControlItemCollection CreateChildItemCollection()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600FA2A RID: 64042 RVA: 0x00385A08 File Offset: 0x00383C08
		internal override void PopulateFromDataItem(PropertyDescriptorCache propertues, object dataItem, string dataMember, int depth)
		{
			base.PopulateFromDataItem(propertues, dataItem, dataMember, depth);
			if (!string.IsNullOrEmpty(this.ListBox.DataKeyField))
			{
				this.ListBox.AddDataKey(propertues.GetPropertyValue(dataItem, this.ListBox.DataKeyField));
			}
		}

		// Token: 0x0600FA2B RID: 64043 RVA: 0x00385A44 File Offset: 0x00383C44
		protected override void WriteXml(XmlWriter writer)
		{
			XmlPersister.SerializePropertiesAsAttributes(this, writer, new string[]
			{
				"Value"
			});
			if (this.Value != this.Text)
			{
				writer.WriteAttributeString("Value", this.Value);
			}
			XmlPersister.SerializeAttributeCollectionAsAttributes(base.Attributes, writer);
		}

		// Token: 0x0600FA2C RID: 64044 RVA: 0x00385A98 File Offset: 0x00383C98
		protected internal virtual IRenderer CreateItemRenderer()
		{
			return RendererFactory.CreateItemRenderer(this);
		}

		// Token: 0x0600FA2D RID: 64045 RVA: 0x00385AA0 File Offset: 0x00383CA0
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x0600FA2E RID: 64046 RVA: 0x00385AAE File Offset: 0x00383CAE
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.Renderer.AddAttributesToRender(writer);
		}

		// Token: 0x17004B99 RID: 19353
		// (get) Token: 0x0600FA2F RID: 64047 RVA: 0x00385ABC File Offset: 0x00383CBC
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Li;
			}
		}

		// Token: 0x0600FA30 RID: 64048 RVA: 0x00385AC0 File Offset: 0x00383CC0
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			this.AddAttributesToRender(writer);
			writer.RenderBeginTag(this.TagKey);
		}

		// Token: 0x0600FA31 RID: 64049 RVA: 0x00385AD5 File Offset: 0x00383CD5
		private bool ShouldSerializeValue()
		{
			return !string.IsNullOrEmpty((string)this.ViewState["Value"]);
		}

		// Token: 0x0600FA32 RID: 64050 RVA: 0x00385AF4 File Offset: 0x00383CF4
		internal IDictionary ExtractValues()
		{
			IEnumerable currentDataSource = this.ListBox.GetCurrentDataSource();
			if (currentDataSource == null)
			{
				return new Dictionary<string, object>();
			}
			object obj = this.FindDataItem(currentDataSource);
			if (obj == null)
			{
				return new Dictionary<string, object>();
			}
			Dictionary<string, object> dictionary = this.ListBox.DataItemToDictionary(obj);
			dictionary.Remove(this.ListBox.DataKeyField);
			return dictionary;
		}

		// Token: 0x0600FA33 RID: 64051 RVA: 0x00385B48 File Offset: 0x00383D48
		internal object FindDataItem(IEnumerable data)
		{
			object dataKey = this.DataKey;
			PropertyDescriptorCache propertyDescriptorCache = new PropertyDescriptorCache();
			foreach (object obj in data)
			{
				if (dataKey.Equals(propertyDescriptorCache.GetPropertyValue(obj, this.ListBox.DataKeyField)))
				{
					return obj;
				}
			}
			return null;
		}

		// Token: 0x0600FA34 RID: 64052 RVA: 0x00385BC8 File Offset: 0x00383DC8
		public RadListBoxItem()
		{
		}

		// Token: 0x0600FA35 RID: 64053 RVA: 0x00385BD0 File Offset: 0x00383DD0
		public RadListBoxItem(string text) : this()
		{
			this.Text = text;
		}

		// Token: 0x0600FA36 RID: 64054 RVA: 0x00385BDF File Offset: 0x00383DDF
		public RadListBoxItem(string text, string value) : this()
		{
			this.Text = text;
			this.Value = value;
		}

		// Token: 0x17004B9A RID: 19354
		// (get) Token: 0x0600FA37 RID: 64055 RVA: 0x00385BF5 File Offset: 0x00383DF5
		// (set) Token: 0x0600FA38 RID: 64056 RVA: 0x00385BFD File Offset: 0x00383DFD
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public new bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				throw new NotSupportedException("The Visible property is not supported");
			}
		}

		// Token: 0x17004B9B RID: 19355
		// (get) Token: 0x0600FA39 RID: 64057 RVA: 0x00385C0C File Offset: 0x00383E0C
		// (set) Token: 0x0600FA3A RID: 64058 RVA: 0x00385C5E File Offset: 0x00383E5E
		[DefaultValue("")]
		[Description("The value of the item")]
		public override string Value
		{
			get
			{
				if (HttpContext.Current == null)
				{
					return (string)(this.ViewState["Value"] ?? string.Empty);
				}
				return (string)(this.ViewState["Value"] ?? this.Text);
			}
			set
			{
				this.ViewState["Value"] = value;
			}
		}

		// Token: 0x17004B9C RID: 19356
		// (get) Token: 0x0600FA3B RID: 64059 RVA: 0x00385C71 File Offset: 0x00383E71
		// (set) Token: 0x0600FA3C RID: 64060 RVA: 0x00385C79 File Offset: 0x00383E79
		[Browsable(false)]
		public RadListBox ListBox { get; internal set; }

		// Token: 0x17004B9D RID: 19357
		// (get) Token: 0x0600FA3D RID: 64061 RVA: 0x00385C82 File Offset: 0x00383E82
		// (set) Token: 0x0600FA3E RID: 64062 RVA: 0x00385CA2 File Offset: 0x00383EA2
		[UrlProperty]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Appearance")]
		[ClientPersistedProperty]
		[DefaultValue("")]
		[Description("The URL for the image for the Item.")]
		public string ImageUrl
		{
			get
			{
				return ((string)this.ViewState["ImageUrl"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x17004B9E RID: 19358
		// (get) Token: 0x0600FA3F RID: 64063 RVA: 0x00385CB5 File Offset: 0x00383EB5
		// (set) Token: 0x0600FA40 RID: 64064 RVA: 0x00385CBD File Offset: 0x00383EBD
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("Whether the item is selected or not.")]
		public bool Selected { get; set; }

		// Token: 0x17004B9F RID: 19359
		// (get) Token: 0x0600FA41 RID: 64065 RVA: 0x00385CC6 File Offset: 0x00383EC6
		// (set) Token: 0x0600FA42 RID: 64066 RVA: 0x00385CE7 File Offset: 0x00383EE7
		[DefaultValue(false)]
		[Description("Whether the ListItem is checked or not")]
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

		// Token: 0x17004BA0 RID: 19360
		// (get) Token: 0x0600FA43 RID: 64067 RVA: 0x00385CFF File Offset: 0x00383EFF
		// (set) Token: 0x0600FA44 RID: 64068 RVA: 0x00385D20 File Offset: 0x00383F20
		[DefaultValue(true)]
		[Category("Behavior")]
		[Description("Whether the ListItem is checkable or not")]
		[ClientPersistedProperty]
		public bool Checkable
		{
			get
			{
				return (bool)(this.ViewState["Checkable"] ?? true);
			}
			set
			{
				this.ViewState["Checkable"] = value;
			}
		}

		// Token: 0x17004BA1 RID: 19361
		// (get) Token: 0x0600FA45 RID: 64069 RVA: 0x00385D38 File Offset: 0x00383F38
		// (set) Token: 0x0600FA46 RID: 64070 RVA: 0x00385D59 File Offset: 0x00383F59
		[Category("Behavior")]
		[DefaultValue(true)]
		[Description("Enable/disable dragging and dropping of this Item")]
		[ClientPersistedProperty]
		public bool AllowDrag
		{
			get
			{
				return (bool)(this.ViewState["AllowDrag"] ?? true);
			}
			set
			{
				this.ViewState["AllowDrag"] = value;
			}
		}

		// Token: 0x17004BA2 RID: 19362
		// (get) Token: 0x0600FA47 RID: 64071 RVA: 0x00385D71 File Offset: 0x00383F71
		internal RadListBoxItem NextItem
		{
			get
			{
				if (base.Index + 1 != this.ListBox.Items.Count)
				{
					return this.ListBox.Items[base.Index + 1];
				}
				return null;
			}
		}

		// Token: 0x17004BA3 RID: 19363
		// (get) Token: 0x0600FA48 RID: 64072 RVA: 0x00385DA7 File Offset: 0x00383FA7
		internal RadListBoxItem PreviousItem
		{
			get
			{
				if (base.Index != 0)
				{
					return this.ListBox.Items[base.Index - 1];
				}
				return null;
			}
		}

		// Token: 0x17004BA4 RID: 19364
		// (get) Token: 0x0600FA49 RID: 64073 RVA: 0x00385DCB File Offset: 0x00383FCB
		[Browsable(false)]
		public object DataKey
		{
			get
			{
				if (this.ListBox == null || this.ListBox.DataKeys.Count < 1)
				{
					return null;
				}
				return this.ListBox.DataKeys[base.Index];
			}
		}

		// Token: 0x0600FA4A RID: 64074 RVA: 0x00385E00 File Offset: 0x00384000
		public RadListBoxItem Clone()
		{
			RadListBoxItem radListBoxItem = new RadListBoxItem
			{
				Text = this.Text,
				Value = this.Value,
				AllowDrag = this.AllowDrag,
				Checkable = this.Checkable,
				Checked = this.Checked,
				CssClass = this.CssClass,
				Enabled = this.Enabled,
				ImageUrl = this.ImageUrl
			};
			foreach (object obj in base.Attributes.Keys)
			{
				string key = (string)obj;
				radListBoxItem.Attributes[key] = base.Attributes[key];
			}
			return radListBoxItem;
		}

		// Token: 0x0600FA4B RID: 64075 RVA: 0x00385EDC File Offset: 0x003840DC
		public int CompareTo(object obj)
		{
			RadListBoxItem radListBoxItem = obj as RadListBoxItem;
			if (radListBoxItem == null)
			{
				throw new ArgumentException();
			}
			if (this.ListBox == null)
			{
				throw new Exception("Cannot sort items that are not added to a ListBox instance.");
			}
			RadListBoxItem radListBoxItem2 = radListBoxItem;
			int result = 0;
			if (this.ListBox.Sort == RadListBoxSort.Ascending)
			{
				result = string.Compare(this.Text, radListBoxItem2.Text, !this.ListBox.SortCaseSensitive);
			}
			if (this.ListBox.Sort == RadListBoxSort.Descending)
			{
				result = string.Compare(this.Text, radListBoxItem2.Text, !this.ListBox.SortCaseSensitive) * -1;
			}
			return result;
		}

		// Token: 0x0600FA4C RID: 64076 RVA: 0x00385F6F File Offset: 0x0038416F
		public void Remove()
		{
			if (this.ListBox == null)
			{
				return;
			}
			this.ListBox.Items.Remove(this);
		}

		// Token: 0x0400473A RID: 18234
		private IRenderer _renderer;
	}
}
