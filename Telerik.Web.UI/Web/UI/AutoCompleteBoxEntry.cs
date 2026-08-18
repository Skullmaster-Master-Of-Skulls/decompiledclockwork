using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x020009AA RID: 2474
	[XmlRoot("Item")]
	[DefaultProperty("Text")]
	[ToolboxItem(false)]
	public class AutoCompleteBoxEntry : WebControl, IMarkableStateManager, IStateManager
	{
		// Token: 0x06005EE4 RID: 24292 RVA: 0x00121B9A File Offset: 0x0011FD9A
		public AutoCompleteBoxEntry()
		{
		}

		// Token: 0x06005EE5 RID: 24293 RVA: 0x00121BA2 File Offset: 0x0011FDA2
		public AutoCompleteBoxEntry(string text) : this()
		{
			this.Text = text;
		}

		// Token: 0x06005EE6 RID: 24294 RVA: 0x00121BB1 File Offset: 0x0011FDB1
		public AutoCompleteBoxEntry(string text, string value) : this(text)
		{
			this.Value = value;
		}

		// Token: 0x06005EE7 RID: 24295 RVA: 0x00121BC4 File Offset: 0x0011FDC4
		public AutoCompleteBoxEntry(string text, string value, Dictionary<string, object> attributes) : this(text, value)
		{
			foreach (string key in attributes.Keys)
			{
				object obj = attributes[key];
				if (obj != null)
				{
					base.Attributes[key] = obj.ToString();
				}
			}
		}

		// Token: 0x17001F53 RID: 8019
		// (get) Token: 0x06005EE8 RID: 24296 RVA: 0x00121C38 File Offset: 0x0011FE38
		// (set) Token: 0x06005EE9 RID: 24297 RVA: 0x00121C40 File Offset: 0x0011FE40
		internal RadAutoCompleteBox ParentContainer
		{
			get
			{
				return this._parentContainer;
			}
			set
			{
				this._parentContainer = value;
			}
		}

		// Token: 0x17001F54 RID: 8020
		// (get) Token: 0x06005EEA RID: 24298 RVA: 0x00121C49 File Offset: 0x0011FE49
		// (set) Token: 0x06005EEB RID: 24299 RVA: 0x00121C51 File Offset: 0x0011FE51
		[Browsable(false)]
		public object DataItem { get; set; }

		// Token: 0x17001F55 RID: 8021
		// (get) Token: 0x06005EEC RID: 24300 RVA: 0x00121C5A File Offset: 0x0011FE5A
		// (set) Token: 0x06005EED RID: 24301 RVA: 0x00121C7A File Offset: 0x0011FE7A
		[Localizable(true)]
		[DefaultValue("")]
		public string Text
		{
			get
			{
				return (string)(this.ViewState["Text"] ?? string.Empty);
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x17001F56 RID: 8022
		// (get) Token: 0x06005EEE RID: 24302 RVA: 0x00121C8D File Offset: 0x0011FE8D
		// (set) Token: 0x06005EEF RID: 24303 RVA: 0x00121CAD File Offset: 0x0011FEAD
		[Localizable(true)]
		[DefaultValue("")]
		public string Value
		{
			get
			{
				return (string)(this.ViewState["Value"] ?? string.Empty);
			}
			set
			{
				this.ViewState["Value"] = value;
			}
		}

		// Token: 0x17001F57 RID: 8023
		// (get) Token: 0x06005EF0 RID: 24304 RVA: 0x00121CC0 File Offset: 0x0011FEC0
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return base.IsTrackingViewState;
			}
		}

		// Token: 0x06005EF1 RID: 24305 RVA: 0x00121CC8 File Offset: 0x0011FEC8
		void IStateManager.LoadViewState(object state)
		{
			object[] array = (object[])state;
			this.LoadViewState(array[0]);
		}

		// Token: 0x06005EF2 RID: 24306 RVA: 0x00121CE8 File Offset: 0x0011FEE8
		object IStateManager.SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState()
			};
		}

		// Token: 0x06005EF3 RID: 24307 RVA: 0x00121D06 File Offset: 0x0011FF06
		protected virtual object SaveChildViewState()
		{
			return null;
		}

		// Token: 0x06005EF4 RID: 24308 RVA: 0x00121D09 File Offset: 0x0011FF09
		void IStateManager.TrackViewState()
		{
			base.TrackViewState();
		}

		// Token: 0x06005EF5 RID: 24309 RVA: 0x00121D11 File Offset: 0x0011FF11
		protected internal void SetItemContainer(RadAutoCompleteBox parent)
		{
			this.ParentContainer = parent;
		}

		// Token: 0x06005EF6 RID: 24310 RVA: 0x00121D1C File Offset: 0x0011FF1C
		protected internal void LoadFromDictionary(IDictionary<string, object> dictionary)
		{
			if (dictionary.ContainsKey("text"))
			{
				this.Text = dictionary["text"].ToString();
			}
			if (dictionary.ContainsKey("value"))
			{
				this.Value = dictionary["value"].ToString();
			}
			if (dictionary.ContainsKey("attributes"))
			{
				IDictionary<string, object> dictionary2 = (IDictionary<string, object>)dictionary["attributes"];
				foreach (string key in dictionary2.Keys)
				{
					object obj = dictionary2[key];
					if (obj != null)
					{
						base.Attributes[key] = obj.ToString();
					}
				}
			}
		}

		// Token: 0x06005EF7 RID: 24311 RVA: 0x00121DE4 File Offset: 0x0011FFE4
		public void SetDirty()
		{
			this.ViewState.SetDirty(true);
		}

		// Token: 0x040016D4 RID: 5844
		private RadAutoCompleteBox _parentContainer;
	}
}
