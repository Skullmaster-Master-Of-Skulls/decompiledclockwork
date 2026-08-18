using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000B2F RID: 2863
	[XmlRoot("Item")]
	[DefaultProperty("Text")]
	[ToolboxItem(false)]
	public class DropDownTreeEntry : WebControl, IMarkableStateManager, IStateManager
	{
		// Token: 0x1700233F RID: 9023
		// (get) Token: 0x06006B97 RID: 27543 RVA: 0x00191215 File Offset: 0x0018F415
		// (set) Token: 0x06006B98 RID: 27544 RVA: 0x0019121D File Offset: 0x0018F41D
		internal RadDropDownTree ParentContainer
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

		// Token: 0x17002340 RID: 9024
		// (get) Token: 0x06006B99 RID: 27545 RVA: 0x00191226 File Offset: 0x0018F426
		// (set) Token: 0x06006B9A RID: 27546 RVA: 0x00191246 File Offset: 0x0018F446
		[DefaultValue("")]
		internal string ParentInformation
		{
			get
			{
				return (string)(this.ViewState["ParentInformation"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ParentInformation"] = value;
			}
		}

		// Token: 0x06006B9B RID: 27547 RVA: 0x00191259 File Offset: 0x0018F459
		public void SetDirty()
		{
			this.ViewState.SetDirty(true);
		}

		// Token: 0x17002341 RID: 9025
		// (get) Token: 0x06006B9C RID: 27548 RVA: 0x00191267 File Offset: 0x0018F467
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return base.IsTrackingViewState;
			}
		}

		// Token: 0x06006B9D RID: 27549 RVA: 0x00191270 File Offset: 0x0018F470
		void IStateManager.LoadViewState(object state)
		{
			object[] array = (object[])state;
			this.LoadViewState(array[0]);
		}

		// Token: 0x06006B9E RID: 27550 RVA: 0x00191290 File Offset: 0x0018F490
		object IStateManager.SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState()
			};
		}

		// Token: 0x06006B9F RID: 27551 RVA: 0x001912AE File Offset: 0x0018F4AE
		void IStateManager.TrackViewState()
		{
			base.TrackViewState();
		}

		// Token: 0x06006BA0 RID: 27552 RVA: 0x001912B6 File Offset: 0x0018F4B6
		protected virtual object SaveChildViewState()
		{
			return null;
		}

		// Token: 0x06006BA1 RID: 27553 RVA: 0x001912B9 File Offset: 0x0018F4B9
		protected internal void SetItemContainer(RadDropDownTree parent)
		{
			this.ParentContainer = parent;
		}

		// Token: 0x06006BA2 RID: 27554 RVA: 0x001912C4 File Offset: 0x0018F4C4
		protected internal void LoadFromDictionary(IDictionary<string, object> dictionary)
		{
			if (dictionary.ContainsKey("text") && dictionary["text"] != null)
			{
				this.Text = HttpUtility.HtmlDecode(dictionary["text"].ToString());
			}
			if (dictionary.ContainsKey("value") && dictionary["value"] != null)
			{
				this.Value = HttpUtility.HtmlDecode(dictionary["value"].ToString());
			}
			if (dictionary.ContainsKey("fullPath") && dictionary["fullPath"] != null)
			{
				this.FullPath = HttpUtility.HtmlDecode(dictionary["fullPath"].ToString());
			}
		}

		// Token: 0x06006BA3 RID: 27555 RVA: 0x00191370 File Offset: 0x0018F570
		public DropDownTreeEntry()
		{
		}

		// Token: 0x06006BA4 RID: 27556 RVA: 0x00191378 File Offset: 0x0018F578
		public DropDownTreeEntry(string text) : this()
		{
			this.Text = text;
		}

		// Token: 0x06006BA5 RID: 27557 RVA: 0x00191387 File Offset: 0x0018F587
		public DropDownTreeEntry(string text, string value) : this()
		{
			this.Text = text;
			this.Value = value;
		}

		// Token: 0x17002342 RID: 9026
		// (get) Token: 0x06006BA6 RID: 27558 RVA: 0x0019139D File Offset: 0x0018F59D
		// (set) Token: 0x06006BA7 RID: 27559 RVA: 0x001913BD File Offset: 0x0018F5BD
		[Localizable(true)]
		[DefaultValue("")]
		public string Text
		{
			get
			{
				return (string)(this.ViewState["Text"] ?? string.Empty);
			}
			internal set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x17002343 RID: 9027
		// (get) Token: 0x06006BA8 RID: 27560 RVA: 0x001913D0 File Offset: 0x0018F5D0
		// (set) Token: 0x06006BA9 RID: 27561 RVA: 0x001913F0 File Offset: 0x0018F5F0
		[Localizable(true)]
		[DefaultValue("")]
		public string Value
		{
			get
			{
				return (string)(this.ViewState["Value"] ?? string.Empty);
			}
			internal set
			{
				this.ViewState["Value"] = value;
			}
		}

		// Token: 0x17002344 RID: 9028
		// (get) Token: 0x06006BAA RID: 27562 RVA: 0x00191403 File Offset: 0x0018F603
		// (set) Token: 0x06006BAB RID: 27563 RVA: 0x00191423 File Offset: 0x0018F623
		[DefaultValue("")]
		public string FullPath
		{
			get
			{
				return (string)(this.ViewState["FullPath"] ?? string.Empty);
			}
			internal set
			{
				this.ViewState["FullPath"] = value;
			}
		}

		// Token: 0x04001D05 RID: 7429
		private RadDropDownTree _parentContainer;
	}
}
