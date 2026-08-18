using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000EF6 RID: 3830
	[ToolboxItem(false)]
	public class SearchBoxButton : WebControl, IMarkableStateManager, IStateManager
	{
		// Token: 0x06009125 RID: 37157 RVA: 0x0020AF6C File Offset: 0x0020916C
		protected internal void LoadFromDictionary(IDictionary<string, object> dictionary)
		{
			if (dictionary.ContainsKey("commandName"))
			{
				this.CommandName = dictionary["commandName"].ToString();
			}
			if (dictionary.ContainsKey("commandArgument"))
			{
				this.CommandArgument = dictionary["commandArgument"].ToString();
			}
			if (dictionary.ContainsKey("imageUrl"))
			{
				this.ImageUrl = dictionary["imageUrl"].ToString();
			}
			if (dictionary.ContainsKey("position"))
			{
				this.Position = (SearchBoxButtonPosition)Enum.Parse(typeof(SearchBoxButtonPosition), dictionary["position"].ToString());
			}
		}

		// Token: 0x17002DF7 RID: 11767
		// (get) Token: 0x06009126 RID: 37158 RVA: 0x0020B019 File Offset: 0x00209219
		// (set) Token: 0x06009127 RID: 37159 RVA: 0x0020B039 File Offset: 0x00209239
		[Category("Appearance")]
		[UrlProperty]
		[Description("The URL of the image displayed for the button.")]
		[DefaultValue("")]
		public string ImageUrl
		{
			get
			{
				return (string)(this.ViewState["ImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x17002DF8 RID: 11768
		// (get) Token: 0x06009128 RID: 37160 RVA: 0x0020B04C File Offset: 0x0020924C
		// (set) Token: 0x06009129 RID: 37161 RVA: 0x0020B06C File Offset: 0x0020926C
		[Category("Appearance")]
		[Description("The image element alt tag value for the button.")]
		public string AlternateText
		{
			get
			{
				return (string)(this.ViewState["AlternateText"] ?? "image");
			}
			set
			{
				this.ViewState["AlternateText"] = value;
			}
		}

		// Token: 0x17002DF9 RID: 11769
		// (get) Token: 0x0600912A RID: 37162 RVA: 0x0020B07F File Offset: 0x0020927F
		// (set) Token: 0x0600912B RID: 37163 RVA: 0x0020B09F File Offset: 0x0020929F
		[Description("Gets or sets the command name associated with the Button that is passed to the ButtonCommand event.")]
		[DefaultValue("")]
		[Category("Behavior")]
		public string CommandName
		{
			get
			{
				return ((string)this.ViewState["CommandName"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["CommandName"] = value;
			}
		}

		// Token: 0x17002DFA RID: 11770
		// (get) Token: 0x0600912C RID: 37164 RVA: 0x0020B0B2 File Offset: 0x002092B2
		// (set) Token: 0x0600912D RID: 37165 RVA: 0x0020B0D2 File Offset: 0x002092D2
		[DefaultValue("")]
		[Category("Behavior")]
		[Description("Gets or sets an optional parameter passed to the ButtonCommand event along with the associated CommandName.")]
		public string CommandArgument
		{
			get
			{
				return ((string)this.ViewState["CommandArgument"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["CommandArgument"] = value;
			}
		}

		// Token: 0x17002DFB RID: 11771
		// (get) Token: 0x0600912E RID: 37166 RVA: 0x0020B0E5 File Offset: 0x002092E5
		// (set) Token: 0x0600912F RID: 37167 RVA: 0x0020B106 File Offset: 0x00209306
		[DefaultValue(SearchBoxButtonPosition.Left)]
		[Category("Appearance")]
		[Description("The position of the Button relative to the input field of the SearchBox control.")]
		public SearchBoxButtonPosition Position
		{
			get
			{
				return (SearchBoxButtonPosition)(this.ViewState["Position"] ?? SearchBoxButtonPosition.Left);
			}
			set
			{
				this.ViewState["Position"] = value;
			}
		}

		// Token: 0x17002DFC RID: 11772
		// (get) Token: 0x06009130 RID: 37168 RVA: 0x0020B11E File Offset: 0x0020931E
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return base.IsTrackingViewState;
			}
		}

		// Token: 0x06009131 RID: 37169 RVA: 0x0020B128 File Offset: 0x00209328
		void IStateManager.LoadViewState(object state)
		{
			object[] array = (object[])state;
			this.LoadViewState(array[0]);
		}

		// Token: 0x06009132 RID: 37170 RVA: 0x0020B148 File Offset: 0x00209348
		object IStateManager.SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState()
			};
		}

		// Token: 0x06009133 RID: 37171 RVA: 0x0020B166 File Offset: 0x00209366
		protected virtual object SaveChildViewState()
		{
			return null;
		}

		// Token: 0x06009134 RID: 37172 RVA: 0x0020B169 File Offset: 0x00209369
		void IStateManager.TrackViewState()
		{
			base.TrackViewState();
		}

		// Token: 0x06009135 RID: 37173 RVA: 0x0020B171 File Offset: 0x00209371
		public void SetDirty()
		{
			this.ViewState.SetDirty(true);
		}
	}
}
