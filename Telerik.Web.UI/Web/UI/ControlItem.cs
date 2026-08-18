using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000938 RID: 2360
	public abstract class ControlItem : WebControl, IMarkableStateManager, IStateManager, INamingContainer, IXmlSerializable, IItem
	{
		// Token: 0x17001D87 RID: 7559
		// (get) Token: 0x06005987 RID: 22919 RVA: 0x00110996 File Offset: 0x0010EB96
		// (set) Token: 0x06005988 RID: 22920 RVA: 0x0011099E File Offset: 0x0010EB9E
		[Browsable(false)]
		public virtual object DataItem { get; set; }

		// Token: 0x17001D88 RID: 7560
		// (get) Token: 0x06005989 RID: 22921 RVA: 0x001109A7 File Offset: 0x0010EBA7
		// (set) Token: 0x0600598A RID: 22922 RVA: 0x001109C7 File Offset: 0x0010EBC7
		[Localizable(true)]
		[DefaultValue("")]
		public virtual string Text
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

		// Token: 0x17001D89 RID: 7561
		// (get) Token: 0x0600598B RID: 22923 RVA: 0x001109DA File Offset: 0x0010EBDA
		// (set) Token: 0x0600598C RID: 22924 RVA: 0x001109FA File Offset: 0x0010EBFA
		[Localizable(true)]
		[Category("Behavior")]
		[DefaultValue("")]
		public virtual string Value
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

		// Token: 0x17001D8A RID: 7562
		// (get) Token: 0x0600598D RID: 22925 RVA: 0x00110A0D File Offset: 0x0010EC0D
		// (set) Token: 0x0600598E RID: 22926 RVA: 0x00110A15 File Offset: 0x0010EC15
		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				this.TrackViewState();
				base.Enabled = value;
			}
		}

		// Token: 0x17001D8B RID: 7563
		// (get) Token: 0x0600598F RID: 22927 RVA: 0x00110A24 File Offset: 0x0010EC24
		// (set) Token: 0x06005990 RID: 22928 RVA: 0x00110A2C File Offset: 0x0010EC2C
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				this.TrackViewState();
				base.Visible = value;
			}
		}

		// Token: 0x17001D8C RID: 7564
		// (get) Token: 0x06005991 RID: 22929 RVA: 0x00110A3B File Offset: 0x0010EC3B
		// (set) Token: 0x06005992 RID: 22930 RVA: 0x00110A43 File Offset: 0x0010EC43
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new string ID
		{
			get
			{
				return base.ID;
			}
			internal set
			{
				base.ID = value;
			}
		}

		// Token: 0x17001D8D RID: 7565
		// (get) Token: 0x06005993 RID: 22931 RVA: 0x00110A4C File Offset: 0x0010EC4C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Index
		{
			get
			{
				return this.ItemContainer.Items.IndexOf(this);
			}
		}

		// Token: 0x17001D8E RID: 7566
		// (get) Token: 0x06005994 RID: 22932 RVA: 0x00110A5F File Offset: 0x0010EC5F
		// (set) Token: 0x06005995 RID: 22933 RVA: 0x00110A67 File Offset: 0x0010EC67
		[Localizable(true)]
		[DefaultValue("")]
		[Description("Gets or sets the access key that allows you to quickly navigate to the Web server control.")]
		public override string AccessKey
		{
			get
			{
				return base.AccessKey;
			}
			set
			{
				base.AccessKey = value;
			}
		}

		// Token: 0x17001D8F RID: 7567
		// (get) Token: 0x06005996 RID: 22934 RVA: 0x00110A70 File Offset: 0x0010EC70
		// (set) Token: 0x06005997 RID: 22935 RVA: 0x00110A78 File Offset: 0x0010EC78
		[Editor(typeof(ColorEditor), typeof(UITypeEditor))]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		// Token: 0x17001D90 RID: 7568
		// (get) Token: 0x06005998 RID: 22936 RVA: 0x00110A81 File Offset: 0x0010EC81
		// (set) Token: 0x06005999 RID: 22937 RVA: 0x00110A89 File Offset: 0x0010EC89
		[Editor(typeof(ColorEditor), typeof(UITypeEditor))]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		// Token: 0x17001D91 RID: 7569
		// (get) Token: 0x0600599A RID: 22938 RVA: 0x00110A92 File Offset: 0x0010EC92
		// (set) Token: 0x0600599B RID: 22939 RVA: 0x00110A9A File Offset: 0x0010EC9A
		[Editor(typeof(ColorEditor), typeof(UITypeEditor))]
		public override Color BorderColor
		{
			get
			{
				return base.BorderColor;
			}
			set
			{
				base.BorderColor = value;
			}
		}

		// Token: 0x0600599C RID: 22940 RVA: 0x00110AA3 File Offset: 0x0010ECA3
		void IMarkableStateManager.SetDirty()
		{
			this.ViewState.SetDirty(true);
			this.SetChildrenDirty();
			base.ControlStyle.SetDirty();
		}

		// Token: 0x0600599D RID: 22941 RVA: 0x00110AC2 File Offset: 0x0010ECC2
		protected virtual void SetChildrenDirty()
		{
		}

		// Token: 0x17001D92 RID: 7570
		// (get) Token: 0x0600599E RID: 22942 RVA: 0x00110AC4 File Offset: 0x0010ECC4
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return base.IsTrackingViewState;
			}
		}

		// Token: 0x0600599F RID: 22943 RVA: 0x00110ACC File Offset: 0x0010ECCC
		void IStateManager.LoadViewState(object state)
		{
			object[] array = (object[])state;
			this.LoadViewState(array[0]);
			this.LoadChildViewState(array[1]);
		}

		// Token: 0x060059A0 RID: 22944 RVA: 0x00110AF2 File Offset: 0x0010ECF2
		protected virtual void LoadChildViewState(object viewState)
		{
		}

		// Token: 0x060059A1 RID: 22945 RVA: 0x00110AF4 File Offset: 0x0010ECF4
		object IStateManager.SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				this.SaveChildViewState()
			};
		}

		// Token: 0x060059A2 RID: 22946 RVA: 0x00110B1B File Offset: 0x0010ED1B
		protected virtual object SaveChildViewState()
		{
			return null;
		}

		// Token: 0x060059A3 RID: 22947 RVA: 0x00110B20 File Offset: 0x0010ED20
		void IStateManager.TrackViewState()
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.A);
			webControl.CopyBaseAttributes(this);
			base.TrackViewState();
			this.TrackChildViewState();
			base.CopyBaseAttributes(webControl);
		}

		// Token: 0x060059A4 RID: 22948 RVA: 0x00110B4E File Offset: 0x0010ED4E
		protected virtual void TrackChildViewState()
		{
		}

		// Token: 0x17001D93 RID: 7571
		// (get) Token: 0x060059A5 RID: 22949 RVA: 0x00110B50 File Offset: 0x0010ED50
		internal IControlItemContainer ItemContainer
		{
			get
			{
				return this.Parent as IControlItemContainer;
			}
		}

		// Token: 0x17001D94 RID: 7572
		// (get) Token: 0x060059A6 RID: 22950 RVA: 0x00110B5D File Offset: 0x0010ED5D
		// (set) Token: 0x060059A7 RID: 22951 RVA: 0x00110B65 File Offset: 0x0010ED65
		protected internal virtual ITemplate Template { get; set; }

		// Token: 0x17001D95 RID: 7573
		// (get) Token: 0x060059A8 RID: 22952 RVA: 0x00110B6E File Offset: 0x0010ED6E
		// (set) Token: 0x060059A9 RID: 22953 RVA: 0x00110B76 File Offset: 0x0010ED76
		internal bool TemplateInstantiated { get; set; }

		// Token: 0x17001D96 RID: 7574
		// (get) Token: 0x060059AA RID: 22954 RVA: 0x00110B7F File Offset: 0x0010ED7F
		// (set) Token: 0x060059AB RID: 22955 RVA: 0x00110B87 File Offset: 0x0010ED87
		private protected ControlItemContainer Container { protected get; private set; }

		// Token: 0x17001D97 RID: 7575
		// (get) Token: 0x060059AC RID: 22956 RVA: 0x00110B90 File Offset: 0x0010ED90
		// (set) Token: 0x060059AD RID: 22957 RVA: 0x00110B99 File Offset: 0x0010ED99
		public override Version RenderingCompatibility
		{
			get
			{
				return new Version(3, 5);
			}
			set
			{
			}
		}

		// Token: 0x17001D98 RID: 7576
		// (get) Token: 0x060059AE RID: 22958 RVA: 0x00110B9B File Offset: 0x0010ED9B
		protected internal ControlItemCollection Children
		{
			get
			{
				if (this._children == null)
				{
					this._children = this.CreateChildItemCollection();
				}
				return this._children;
			}
		}

		// Token: 0x060059AF RID: 22959 RVA: 0x00110BB8 File Offset: 0x0010EDB8
		protected internal string GetHierarchicalIndex()
		{
			ControlItem controlItem = this.Parent as ControlItem;
			ControlItemContainer controlItemContainer = null;
			ControlItem item = this;
			List<string> list = new List<string>();
			while (controlItem != null)
			{
				list.Add(controlItem.Children.IndexOf(item).ToString());
				item = controlItem;
				Control parent = controlItem.Parent;
				controlItem = (parent as ControlItem);
				if (controlItem == null)
				{
					controlItemContainer = (parent as ControlItemContainer);
				}
			}
			if (this.Parent is ControlItemContainer)
			{
				controlItemContainer = (this.Parent as ControlItemContainer);
			}
			if (controlItemContainer != null)
			{
				list.Add(controlItemContainer.Children.IndexOf(item).ToString());
			}
			list.Reverse();
			string[] value = list.ToArray();
			return string.Join(":", value);
		}

		// Token: 0x060059B0 RID: 22960 RVA: 0x00110C69 File Offset: 0x0010EE69
		protected internal virtual void SetItemContainer(ControlItemContainer itemContainer)
		{
			itemContainer.InitializeItem(this);
			this.Container = itemContainer;
		}

		// Token: 0x060059B1 RID: 22961 RVA: 0x00110C7C File Offset: 0x0010EE7C
		protected internal virtual void LoadFromDictionary(IDictionary<string, object> dictionary)
		{
			if (dictionary.ContainsKey("text"))
			{
				this.Text = dictionary["text"].ToString();
			}
			if (dictionary.ContainsKey("value"))
			{
				this.Value = dictionary["value"].ToString();
			}
			if (dictionary.ContainsKey("enabled"))
			{
				this.Enabled = Convert.ToBoolean(dictionary["enabled"]);
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

		// Token: 0x060059B2 RID: 22962
		protected abstract ControlItemCollection CreateChildItemCollection();

		// Token: 0x060059B3 RID: 22963 RVA: 0x00110D68 File Offset: 0x0010EF68
		internal virtual void PopulateFromDataItem(PropertyDescriptorCache properties, object dataItem, string dataMember, int depth)
		{
			bool flag = dataItem is DataRowView;
			if (!string.IsNullOrEmpty(this.Container.DataTextField))
			{
				try
				{
					this.Text = properties.GetPropertyValue(dataItem, this.Container.DataTextField, this.Container.DataTextFormatString);
					goto IL_A5;
				}
				catch (ArgumentException)
				{
					if (base.DesignMode && flag)
					{
						this.Text = "Databound";
						goto IL_A5;
					}
					throw;
				}
			}
			if (!string.IsNullOrEmpty(this.Container.DataTextFormatString))
			{
				this.Text = string.Format(CultureInfo.CurrentCulture, this.Container.DataTextFormatString, new object[]
				{
					dataItem
				});
			}
			else
			{
				this.Text = dataItem.ToString();
			}
			IL_A5:
			if (!string.IsNullOrEmpty(this.Container.DataValueField))
			{
				try
				{
					this.Value = DataBinder.GetPropertyValue(dataItem, this.Container.DataValueField, null);
				}
				catch
				{
					if (!base.DesignMode || !flag)
					{
						throw;
					}
					this.Value = "DataboundValue";
				}
			}
		}

		// Token: 0x060059B4 RID: 22964 RVA: 0x00110E80 File Offset: 0x0010F080
		internal void AddAttributes(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(this.AccessKey))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, this.AccessKey);
			}
			if (this.TabIndex != 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, this.TabIndex.ToString());
			}
			if (!string.IsNullOrEmpty(this.ToolTip))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Title, this.ToolTip);
			}
			if (base.ControlStyleCreated && !base.ControlStyle.IsEmpty)
			{
				base.ControlStyle.AddAttributesToRender(writer);
			}
			foreach (object obj in base.Attributes.Keys)
			{
				string text = (string)obj;
				if (HtmlAttributes.IsHtmlAttribute(text))
				{
					writer.AddAttribute(text, base.Attributes[text]);
				}
			}
		}

		// Token: 0x060059B5 RID: 22965 RVA: 0x00110F68 File Offset: 0x0010F168
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060059B6 RID: 22966 RVA: 0x00110F6F File Offset: 0x0010F16F
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.ReadXml(reader);
		}

		// Token: 0x060059B7 RID: 22967 RVA: 0x00110F78 File Offset: 0x0010F178
		protected virtual void ReadXml(XmlReader reader)
		{
			XmlPersister.Deserialize(this, base.Attributes, this.PropertyMappings, reader);
		}

		// Token: 0x17001D99 RID: 7577
		// (get) Token: 0x060059B8 RID: 22968 RVA: 0x00110F8D File Offset: 0x0010F18D
		protected internal virtual IDictionary<string, string> PropertyMappings
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060059B9 RID: 22969 RVA: 0x00110F90 File Offset: 0x0010F190
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer);
		}

		// Token: 0x060059BA RID: 22970 RVA: 0x00110F99 File Offset: 0x0010F199
		protected virtual void WriteXml(XmlWriter writer)
		{
			XmlPersister.SerializePropertiesAsAttributes(this, writer);
			XmlPersister.SerializeAttributeCollectionAsAttributes(base.Attributes, writer);
		}

		// Token: 0x17001D9A RID: 7578
		// (get) Token: 0x060059BB RID: 22971 RVA: 0x00110FB0 File Offset: 0x0010F1B0
		internal virtual bool Templated
		{
			get
			{
				if (this.TemplateInstantiated)
				{
					return true;
				}
				if (!this._controlsTraversed)
				{
					this._controlsTraversed = true;
					foreach (object obj in this.Controls)
					{
						Control control = (Control)obj;
						if (!this.IsChildControl(control))
						{
							this._templated = true;
							break;
						}
					}
				}
				return this._templated;
			}
		}

		// Token: 0x060059BC RID: 22972 RVA: 0x00111034 File Offset: 0x0010F234
		protected virtual bool IsChildControl(Control control)
		{
			return control is ControlItem;
		}

		// Token: 0x060059BD RID: 22973 RVA: 0x00111058 File Offset: 0x0010F258
		protected internal IList<TControlItem> GetAllChildren<TControlItem>() where TControlItem : ControlItem
		{
			IList<TControlItem> childrenList = new List<TControlItem>();
			this.FindChild<TControlItem>(delegate(TControlItem item)
			{
				childrenList.Add(item);
				return false;
			});
			return childrenList;
		}

		// Token: 0x060059BE RID: 22974 RVA: 0x001110B4 File Offset: 0x0010F2B4
		protected internal IList<TControlItem> GetChildren<TControlItem>(Predicate<TControlItem> predicate) where TControlItem : ControlItem
		{
			IList<TControlItem> childrenList = new List<TControlItem>();
			this.FindChild<TControlItem>(delegate(TControlItem item)
			{
				if (predicate(item))
				{
					childrenList.Add(item);
				}
				return false;
			});
			return childrenList;
		}

		// Token: 0x060059BF RID: 22975 RVA: 0x001110F4 File Offset: 0x0010F2F4
		protected internal TControlItem FindChild<TControlItem>(Predicate<TControlItem> predicate) where TControlItem : ControlItem
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			Stack<TControlItem> stack = new Stack<TControlItem>();
			for (int i = this.Children.Count - 1; i >= 0; i--)
			{
				stack.Push((TControlItem)((object)this.Children[i]));
			}
			while (stack.Count > 0)
			{
				TControlItem tcontrolItem = stack.Pop();
				if (predicate(tcontrolItem))
				{
					return tcontrolItem;
				}
				if (tcontrolItem is IControlItemContainer)
				{
					for (int j = tcontrolItem.Children.Count - 1; j >= 0; j--)
					{
						stack.Push((TControlItem)((object)tcontrolItem.Children[j]));
					}
				}
			}
			return default(TControlItem);
		}

		// Token: 0x060059C0 RID: 22976 RVA: 0x001111B5 File Offset: 0x0010F3B5
		void IItem.PopulateFromDataItem(PropertyDescriptorCache properties, object dataItem, string dataMember, int depth)
		{
			this.PopulateFromDataItem(properties, dataItem, dataMember, depth);
		}

		// Token: 0x17001D9B RID: 7579
		// (get) Token: 0x060059C1 RID: 22977 RVA: 0x001111C2 File Offset: 0x0010F3C2
		IList IItem.Children
		{
			get
			{
				return this.Children;
			}
		}

		// Token: 0x040015CA RID: 5578
		private ControlItemCollection _children;

		// Token: 0x040015CB RID: 5579
		private bool _controlsTraversed;

		// Token: 0x040015CC RID: 5580
		private bool _templated;
	}
}
