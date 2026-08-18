using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x02000295 RID: 661
	[ToolboxItem(false)]
	[EmbeddedSkin("Editor")]
	[ClientScriptResource("Telerik.Web.UI.Editor.TabChooser", "Telerik.Web.UI.Common.Core.js")]
	[EmbeddedSkin("Editor", "Default")]
	[RequiredScript(typeof(RadEditorScripts))]
	public class TabChooser : RadWebControl
	{
		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x060017A4 RID: 6052 RVA: 0x0004F1AD File Offset: 0x0004D3AD
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x060017A5 RID: 6053 RVA: 0x0004F1B1 File Offset: 0x0004D3B1
		protected override string CssClassFormatString
		{
			get
			{
				return "reTabChooser";
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x060017A6 RID: 6054 RVA: 0x0004F1B8 File Offset: 0x0004D3B8
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Gets the collection containing the tabs.")]
		public TabChooserItemCollection Items
		{
			get
			{
				if (this._items == null)
				{
					this._items = new TabChooserItemCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._items).TrackViewState();
					}
				}
				return this._items;
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x0004F1E6 File Offset: 0x0004D3E6
		// (set) Token: 0x060017A8 RID: 6056 RVA: 0x0004F215 File Offset: 0x0004D415
		[DefaultValue("")]
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("itemSelected")]
		public virtual string OnClientItemSelected
		{
			get
			{
				if (this.ViewState["OnClientItemSelected"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientItemSelected"];
			}
			set
			{
				this.ViewState["OnClientItemSelected"] = value;
			}
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x0004F228 File Offset: 0x0004D428
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			JavaScriptSerializer serializer = new JavaScriptSerializer();
			if (this._items != null && this._items.Count > 0)
			{
				descriptor.AddScriptProperty("items", this._items.Serialize(serializer));
			}
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x0004F270 File Offset: 0x0004D470
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.Items).LoadViewState(array[1]);
			}
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x0004F2A0 File Offset: 0x0004D4A0
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				TabChooser.SaveState(this._items)
			};
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x0004F2CE File Offset: 0x0004D4CE
		protected override void TrackViewState()
		{
			base.TrackViewState();
			TabChooser.TrackState(this._items);
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x0004F2E1 File Offset: 0x0004D4E1
		private static void TrackState(IStateManager obj)
		{
			if (obj != null)
			{
				obj.TrackViewState();
			}
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x0004F2EC File Offset: 0x0004D4EC
		private static object SaveState(IStateManager obj)
		{
			if (obj != null)
			{
				return obj.SaveViewState();
			}
			return null;
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x0004F2F9 File Offset: 0x0004D4F9
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x0004F302 File Offset: 0x0004D502
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "itemSelected", this.OnClientItemSelected);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x04000626 RID: 1574
		private TabChooserItemCollection _items;
	}
}
