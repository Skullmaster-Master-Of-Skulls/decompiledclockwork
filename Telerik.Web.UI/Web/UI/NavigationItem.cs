using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Xml;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x020011B4 RID: 4532
	public abstract class NavigationItem : ControlItem, IControlItemContainer
	{
		// Token: 0x17003C05 RID: 15365
		// (get) Token: 0x0600BA26 RID: 47654 RVA: 0x00297387 File Offset: 0x00295587
		// (set) Token: 0x0600BA27 RID: 47655 RVA: 0x002973A7 File Offset: 0x002955A7
		[DefaultValue("")]
		public virtual string NavigateUrl
		{
			get
			{
				return (string)(this.ViewState["NavigateUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["NavigateUrl"] = value;
			}
		}

		// Token: 0x17003C06 RID: 15366
		// (get) Token: 0x0600BA28 RID: 47656 RVA: 0x002973BA File Offset: 0x002955BA
		// (set) Token: 0x0600BA29 RID: 47657 RVA: 0x002973DA File Offset: 0x002955DA
		[DefaultValue("")]
		public virtual string ImageUrl
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

		// Token: 0x17003C07 RID: 15367
		// (get) Token: 0x0600BA2A RID: 47658 RVA: 0x002973ED File Offset: 0x002955ED
		// (set) Token: 0x0600BA2B RID: 47659 RVA: 0x0029740D File Offset: 0x0029560D
		[DefaultValue("")]
		public virtual string HoveredImageUrl
		{
			get
			{
				return (string)(this.ViewState["HoveredImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["HoveredImageUrl"] = value;
			}
		}

		// Token: 0x17003C08 RID: 15368
		// (get) Token: 0x0600BA2C RID: 47660 RVA: 0x00297420 File Offset: 0x00295620
		// (set) Token: 0x0600BA2D RID: 47661 RVA: 0x00297440 File Offset: 0x00295640
		[DefaultValue("")]
		public virtual string Target
		{
			get
			{
				return (string)(this.ViewState["Target"] ?? string.Empty);
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		// Token: 0x17003C09 RID: 15369
		// (get) Token: 0x0600BA2E RID: 47662 RVA: 0x00297453 File Offset: 0x00295653
		ControlItemCollection IControlItemContainer.Items
		{
			get
			{
				return base.Children;
			}
		}

		// Token: 0x0600BA2F RID: 47663 RVA: 0x0029745B File Offset: 0x0029565B
		protected internal override void SetItemContainer(ControlItemContainer itemContainer)
		{
			base.SetItemContainer(itemContainer);
			base.Children.SetItemContainer(itemContainer);
		}

		// Token: 0x0600BA30 RID: 47664 RVA: 0x00297470 File Offset: 0x00295670
		internal override void PopulateFromDataItem(PropertyDescriptorCache properties, object dataItem, string dataMember, int depth)
		{
			base.PopulateFromDataItem(properties, dataItem, dataMember, depth);
			HierarchicalControlItemContainer hierarchicalControlItemContainer = (HierarchicalControlItemContainer)base.Container;
			if (!string.IsNullOrEmpty(hierarchicalControlItemContainer.DataNavigateUrlField))
			{
				this.NavigateUrl = DataBinder.GetPropertyValue(dataItem, hierarchicalControlItemContainer.DataNavigateUrlField, null);
			}
			NavigationItemBinding binding = ((HierarchicalControlItemContainer)base.Container).NavigationItemBindings.GetBinding(dataMember, depth);
			if (binding != null)
			{
				binding.ApplyTo(this, dataItem, properties);
				return;
			}
			INavigateUIData navigateUIData = dataItem as INavigateUIData;
			if (navigateUIData != null)
			{
				this.Text = navigateUIData.Name;
				this.NavigateUrl = navigateUIData.NavigateUrl;
				this.Value = navigateUIData.Value;
				this.ToolTip = navigateUIData.Description;
			}
		}

		// Token: 0x0600BA31 RID: 47665 RVA: 0x00297514 File Offset: 0x00295714
		protected internal override void LoadFromDictionary(IDictionary<string, object> dictionary)
		{
			base.LoadFromDictionary(dictionary);
			if (dictionary.ContainsKey("hoveredImageUrl"))
			{
				string hoveredImageUrl = (dictionary["hoveredImageUrl"] == null) ? string.Empty : dictionary["hoveredImageUrl"].ToString();
				this.HoveredImageUrl = hoveredImageUrl;
			}
			if (dictionary.ContainsKey("imageUrl"))
			{
				string imageUrl = (dictionary["imageUrl"] == null) ? string.Empty : dictionary["imageUrl"].ToString();
				this.ImageUrl = imageUrl;
			}
		}

		// Token: 0x0600BA32 RID: 47666 RVA: 0x0029759A File Offset: 0x0029579A
		protected override void WriteXml(XmlWriter writer)
		{
			base.WriteXml(writer);
			this.WriteXmlForChildren(writer);
		}

		// Token: 0x0600BA33 RID: 47667 RVA: 0x002975AC File Offset: 0x002957AC
		protected virtual void WriteXmlForChildren(XmlWriter writer)
		{
			foreach (object obj in base.Children)
			{
				NavigationItem navigationItem = (NavigationItem)obj;
				XmlSerializer xmlSerializer = new XmlSerializer(navigationItem.GetType());
				xmlSerializer.Serialize(writer, navigationItem);
			}
		}

		// Token: 0x0600BA34 RID: 47668 RVA: 0x00297614 File Offset: 0x00295814
		protected override void ReadXml(XmlReader reader)
		{
			base.ReadXml(reader);
			this.ReadXmlForChildren(reader);
		}

		// Token: 0x0600BA35 RID: 47669 RVA: 0x00297624 File Offset: 0x00295824
		protected virtual void ReadXmlForChildren(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Comment)
				{
					XmlSerializer xmlSerializer = new XmlSerializer(base.GetType());
					using (XmlReader xmlReader = reader.ReadSubtree())
					{
						ControlItem item = (ControlItem)xmlSerializer.Deserialize(xmlReader);
						base.Children.Add(item);
					}
					reader.MoveToContent();
				}
			}
		}

		// Token: 0x0600BA36 RID: 47670 RVA: 0x002976A0 File Offset: 0x002958A0
		protected override void LoadChildViewState(object viewState)
		{
			if (viewState == null)
			{
				base.Children.Clear();
				return;
			}
			((IStateManager)base.Children).LoadViewState(viewState);
		}

		// Token: 0x0600BA37 RID: 47671 RVA: 0x002976BD File Offset: 0x002958BD
		protected override object SaveChildViewState()
		{
			return ((IStateManager)base.Children).SaveViewState();
		}

		// Token: 0x0600BA38 RID: 47672 RVA: 0x002976CA File Offset: 0x002958CA
		protected override void TrackChildViewState()
		{
			((IStateManager)base.Children).TrackViewState();
		}

		// Token: 0x0600BA39 RID: 47673 RVA: 0x002976D8 File Offset: 0x002958D8
		protected override void SetChildrenDirty()
		{
			foreach (object obj in base.Children)
			{
				IMarkableStateManager markableStateManager = (IMarkableStateManager)obj;
				markableStateManager.SetDirty();
			}
		}

		// Token: 0x0600BA3A RID: 47674 RVA: 0x00297730 File Offset: 0x00295930
		protected override void AddedControl(Control control, int index)
		{
			base.AddedControl(control, index);
			if (!(control is ControlItem))
			{
				base.Children.ControlsCount++;
			}
		}

		// Token: 0x0600BA3B RID: 47675 RVA: 0x00297758 File Offset: 0x00295958
		protected bool ItemTextIsHTMLEncoded()
		{
			bool result = false;
			Regex regex = new Regex("&\\S*?;");
			Match match = regex.Match(this.Text);
			if (match.Success)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x17003C0A RID: 15370
		// (get) Token: 0x0600BA3C RID: 47676 RVA: 0x0029778C File Offset: 0x0029598C
		internal string HierarchicalIndex
		{
			get
			{
				List<string> list = new List<string>();
				for (NavigationItem navigationItem = this; navigationItem != null; navigationItem = (navigationItem.ItemContainer as NavigationItem))
				{
					list.Insert(0, navigationItem.ItemContainer.Items.VisibleItems.IndexOf(navigationItem).ToString());
				}
				return string.Join(":", list.ToArray());
			}
		}
	}
}
