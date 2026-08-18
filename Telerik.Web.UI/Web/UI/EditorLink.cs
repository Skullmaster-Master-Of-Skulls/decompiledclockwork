using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001077 RID: 4215
	[ParseChildren(true, "ChildLinks")]
	public class EditorLink : StateManager
	{
		// Token: 0x0600A9C0 RID: 43456 RVA: 0x0024DB59 File Offset: 0x0024BD59
		public EditorLink()
		{
		}

		// Token: 0x0600A9C1 RID: 43457 RVA: 0x0024DB61 File Offset: 0x0024BD61
		public EditorLink(string _name, string _href)
		{
			this.Name = _name;
			this.Href = _href;
		}

		// Token: 0x17003681 RID: 13953
		// (get) Token: 0x0600A9C2 RID: 43458 RVA: 0x0024DB77 File Offset: 0x0024BD77
		// (set) Token: 0x0600A9C3 RID: 43459 RVA: 0x0024DBA6 File Offset: 0x0024BDA6
		public string Name
		{
			get
			{
				if (base.ViewState["Name"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["Name"];
			}
			set
			{
				base.ViewState["Name"] = value;
			}
		}

		// Token: 0x17003682 RID: 13954
		// (get) Token: 0x0600A9C4 RID: 43460 RVA: 0x0024DBB9 File Offset: 0x0024BDB9
		// (set) Token: 0x0600A9C5 RID: 43461 RVA: 0x0024DBE8 File Offset: 0x0024BDE8
		public string Href
		{
			get
			{
				if (base.ViewState["Href"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["Href"];
			}
			set
			{
				base.ViewState["Href"] = value;
			}
		}

		// Token: 0x17003683 RID: 13955
		// (get) Token: 0x0600A9C6 RID: 43462 RVA: 0x0024DBFB File Offset: 0x0024BDFB
		// (set) Token: 0x0600A9C7 RID: 43463 RVA: 0x0024DC2A File Offset: 0x0024BE2A
		[DefaultValue("")]
		public string Target
		{
			get
			{
				if (base.ViewState["Target"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["Target"];
			}
			set
			{
				base.ViewState["Target"] = value;
			}
		}

		// Token: 0x17003684 RID: 13956
		// (get) Token: 0x0600A9C8 RID: 43464 RVA: 0x0024DC3D File Offset: 0x0024BE3D
		// (set) Token: 0x0600A9C9 RID: 43465 RVA: 0x0024DC6C File Offset: 0x0024BE6C
		[DefaultValue("")]
		public string ToolTip
		{
			get
			{
				if (base.ViewState["ToolTip"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["ToolTip"];
			}
			set
			{
				base.ViewState["ToolTip"] = value;
			}
		}

		// Token: 0x17003685 RID: 13957
		// (get) Token: 0x0600A9CA RID: 43466 RVA: 0x0024DC7F File Offset: 0x0024BE7F
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public EditorLinkCollection ChildLinks
		{
			get
			{
				if (this._childLinks == null)
				{
					this._childLinks = new EditorLinkCollection();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._childLinks).TrackViewState();
					}
				}
				return this._childLinks;
			}
		}

		// Token: 0x0600A9CB RID: 43467 RVA: 0x0024DCB0 File Offset: 0x0024BEB0
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.ChildLinks).LoadViewState(array[1]);
		}

		// Token: 0x0600A9CC RID: 43468 RVA: 0x0024DCDC File Offset: 0x0024BEDC
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.ChildLinks).SaveViewState()
			};
		}

		// Token: 0x0600A9CD RID: 43469 RVA: 0x0024DD0A File Offset: 0x0024BF0A
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.ChildLinks).TrackViewState();
		}

		// Token: 0x0600A9CE RID: 43470 RVA: 0x0024DD1D File Offset: 0x0024BF1D
		internal override void SetDirty()
		{
			base.SetDirty();
			this.ChildLinks.SetDirty();
		}

		// Token: 0x04002DB9 RID: 11705
		private EditorLinkCollection _childLinks;
	}
}
