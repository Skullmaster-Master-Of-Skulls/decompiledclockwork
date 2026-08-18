using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000B38 RID: 2872
	public class DropDownSettings : ObjectWithState
	{
		// Token: 0x06006C78 RID: 27768 RVA: 0x001931FD File Offset: 0x001913FD
		public DropDownSettings(StateBag ownerViewState) : base("DropDownSettings", ownerViewState)
		{
		}

		// Token: 0x17002397 RID: 9111
		// (get) Token: 0x06006C79 RID: 27769 RVA: 0x0019320B File Offset: 0x0019140B
		// (set) Token: 0x06006C7A RID: 27770 RVA: 0x0019322C File Offset: 0x0019142C
		[Bindable(false)]
		[Description("Gets or sets whether to enable/disable the RadDropDownTree drop down auto width.")]
		[DefaultValue(DropDownTreeAutoWidth.Disabled)]
		[Browsable(true)]
		public DropDownTreeAutoWidth AutoWidth
		{
			get
			{
				return (DropDownTreeAutoWidth)(base.ViewState["AutoWidth"] ?? DropDownTreeAutoWidth.Disabled);
			}
			set
			{
				base.ViewState["AutoWidth"] = value;
			}
		}

		// Token: 0x17002398 RID: 9112
		// (get) Token: 0x06006C7B RID: 27771 RVA: 0x00193244 File Offset: 0x00191444
		// (set) Token: 0x06006C7C RID: 27772 RVA: 0x00193265 File Offset: 0x00191465
		[NotifyParentProperty(true)]
		[Bindable(false)]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool OpenDropDownOnLoad
		{
			get
			{
				return (bool)(base.ViewState["OpenDropDownOnLoad"] ?? false);
			}
			set
			{
				base.ViewState["OpenDropDownOnLoad"] = value;
			}
		}

		// Token: 0x17002399 RID: 9113
		// (get) Token: 0x06006C7D RID: 27773 RVA: 0x0019327D File Offset: 0x0019147D
		// (set) Token: 0x06006C7E RID: 27774 RVA: 0x0019329E File Offset: 0x0019149E
		[Category("Behavior")]
		[Bindable(false)]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool CloseDropDownOnSelection
		{
			get
			{
				return (bool)(base.ViewState["CloseDropDownOnSelection"] ?? false);
			}
			set
			{
				base.ViewState["CloseDropDownOnSelection"] = value;
			}
		}

		// Token: 0x1700239A RID: 9114
		// (get) Token: 0x06006C7F RID: 27775 RVA: 0x001932B6 File Offset: 0x001914B6
		// (set) Token: 0x06006C80 RID: 27776 RVA: 0x001932DB File Offset: 0x001914DB
		[DefaultValue(typeof(Unit), "")]
		[NotifyParentProperty(true)]
		public Unit Width
		{
			get
			{
				return (Unit)(base.ViewState["Width"] ?? Unit.Empty);
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x1700239B RID: 9115
		// (get) Token: 0x06006C81 RID: 27777 RVA: 0x00193310 File Offset: 0x00191510
		// (set) Token: 0x06006C82 RID: 27778 RVA: 0x00193335 File Offset: 0x00191535
		[DefaultValue(typeof(Unit), "")]
		[NotifyParentProperty(true)]
		[Category("Layout")]
		public Unit Height
		{
			get
			{
				return (Unit)(base.ViewState["Height"] ?? Unit.Empty);
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["Height"] = value;
			}
		}

		// Token: 0x1700239C RID: 9116
		// (get) Token: 0x06006C83 RID: 27779 RVA: 0x0019336A File Offset: 0x0019156A
		// (set) Token: 0x06006C84 RID: 27780 RVA: 0x0019338A File Offset: 0x0019158A
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(string), "")]
		[Category("Layout")]
		public string CssClass
		{
			get
			{
				return (string)(base.ViewState["CssClass"] ?? "");
			}
			set
			{
				base.ViewState["CssClass"] = value;
			}
		}

		// Token: 0x06006C85 RID: 27781 RVA: 0x0019339D File Offset: 0x0019159D
		internal JavaScriptConverter GetConverter()
		{
			return new DropDownSettingsConverter();
		}

		// Token: 0x06006C86 RID: 27782 RVA: 0x001933A4 File Offset: 0x001915A4
		internal void Describe(string propertyName, JavaScriptSerializer serializer, IScriptDescriptor descriptor)
		{
			JavaScriptConverter converter = this.GetConverter();
			serializer.RegisterConverters(new JavaScriptConverter[]
			{
				converter
			});
			IDictionary<string, object> dictionary = converter.Serialize(this, serializer);
			if (dictionary.Count > 0)
			{
				descriptor.AddProperty(propertyName, serializer.Serialize(this));
			}
		}
	}
}
