using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x0200039C RID: 924
	[ClientScriptResource("Telerik.Web.UI.ControlItemContainer", "Telerik.Web.UI.Common.Navigation.OverlayScript.js")]
	[ClientScriptResource("Telerik.Web.UI.ControlItemContainer", "Telerik.Web.UI.Common.Navigation.NavigationScripts.js")]
	[RequiredScript(typeof(jQueryPlugins))]
	public abstract class ControlItemContainer : RadDataBoundControl, INamingContainer, IXmlSerializable, IControlItemContainer, IItemContainer
	{
		// Token: 0x0600216B RID: 8555 RVA: 0x00070818 File Offset: 0x0006EA18
		protected override void PerformDataBinding(IEnumerable data)
		{
			if (data == null && this.DataSource == null)
			{
				foreach (object obj in this.Children)
				{
					ControlItem controlItem = (ControlItem)obj;
					controlItem.DataBind();
				}
				return;
			}
			this.PrepareForDataBinding();
			ControlDataBinder controlDataBinder = new ControlDataBinder(this);
			controlDataBinder.BindToEnumerableData(data);
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x00070890 File Offset: 0x0006EA90
		protected void PrepareForDataBinding()
		{
			if (!this.AppendDataBoundItems)
			{
				this.Children.Clear();
				base.ClearChildViewState();
			}
			this.TrackViewState();
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x0600216D RID: 8557 RVA: 0x000708B1 File Offset: 0x0006EAB1
		// (set) Token: 0x0600216E RID: 8558 RVA: 0x000708D2 File Offset: 0x0006EAD2
		[Category("Data")]
		[DefaultValue(false)]
		public virtual bool AppendDataBoundItems
		{
			get
			{
				return (bool)(this.ViewState["AppendDataBoundItems"] ?? false);
			}
			set
			{
				this.ViewState["AppendDataBoundItems"] = value;
			}
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x0600216F RID: 8559 RVA: 0x000708EA File Offset: 0x0006EAEA
		// (set) Token: 0x06002170 RID: 8560 RVA: 0x0007090A File Offset: 0x0006EB0A
		[Category("Data")]
		[DefaultValue("")]
		public virtual string DataTextField
		{
			get
			{
				return (string)(this.ViewState["DataTextField"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DataTextField"] = value;
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06002171 RID: 8561 RVA: 0x0007091D File Offset: 0x0006EB1D
		// (set) Token: 0x06002172 RID: 8562 RVA: 0x0007093D File Offset: 0x0006EB3D
		[DefaultValue("")]
		[Category("Data")]
		public virtual string DataValueField
		{
			get
			{
				return (string)(this.ViewState["DataValueField"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DataValueField"] = value;
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06002173 RID: 8563 RVA: 0x00070950 File Offset: 0x0006EB50
		// (set) Token: 0x06002174 RID: 8564 RVA: 0x00070970 File Offset: 0x0006EB70
		[Category("Data")]
		[DefaultValue("")]
		public virtual string DataTextFormatString
		{
			get
			{
				return (string)(this.ViewState["DataTextFormatString"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DataTextFormatString"] = value;
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06002175 RID: 8565 RVA: 0x00070983 File Offset: 0x0006EB83
		// (set) Token: 0x06002176 RID: 8566 RVA: 0x000709A3 File Offset: 0x0006EBA3
		[DefaultValue("")]
		[Description("Gets or sets the name of the validation group to which this validation control belongs.")]
		[Bindable(true)]
		[Category("Behavior")]
		public virtual string ValidationGroup
		{
			get
			{
				return (string)(this.ViewState["ValidationGroup"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06002177 RID: 8567 RVA: 0x000709B6 File Offset: 0x0006EBB6
		// (set) Token: 0x06002178 RID: 8568 RVA: 0x000709D6 File Offset: 0x0006EBD6
		[DefaultValue("")]
		[Category("Behavior")]
		[Themeable(false)]
		[UrlProperty("*.aspx")]
		public virtual string PostBackUrl
		{
			get
			{
				return (string)(this.ViewState["PostBackUrl"] ?? "");
			}
			set
			{
				this.ViewState["PostBackUrl"] = value;
			}
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06002179 RID: 8569 RVA: 0x000709E9 File Offset: 0x0006EBE9
		// (set) Token: 0x0600217A RID: 8570 RVA: 0x00070A0A File Offset: 0x0006EC0A
		[DefaultValue(true)]
		[Category("Behavior")]
		[Description("Whether the control causes validation to fire.")]
		public virtual bool CausesValidation
		{
			get
			{
				return (bool)(this.ViewState["CausesValidation"] ?? true);
			}
			set
			{
				this.ViewState["CausesValidation"] = value;
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x0600217B RID: 8571 RVA: 0x00070A24 File Offset: 0x0006EC24
		[Description("Keyboard navigation settings")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[Category("Behavior")]
		public virtual KeyboardNavigationSettings KeyboardNavigationSettings
		{
			get
			{
				KeyboardNavigationSettings result;
				if ((result = this._keyboardNavigationSettings) == null)
				{
					result = (this._keyboardNavigationSettings = new KeyboardNavigationSettings());
				}
				return result;
			}
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x0600217C RID: 8572 RVA: 0x00070A49 File Offset: 0x0006EC49
		// (set) Token: 0x0600217D RID: 8573 RVA: 0x00070A51 File Offset: 0x0006EC51
		protected internal ITemplate Template { get; set; }

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x0600217E RID: 8574 RVA: 0x00070A5A File Offset: 0x0006EC5A
		protected internal ControlItemCollection Children
		{
			[DebuggerStepThrough]
			get
			{
				if (this._children == null)
				{
					this._children = this.CreateChildItemCollection();
					this._children.SetItemContainer(this);
				}
				return this._children;
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x0600217F RID: 8575 RVA: 0x00070A82 File Offset: 0x0006EC82
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x00070A86 File Offset: 0x0006EC86
		internal static void AddProperty(IScriptDescriptor descriptor, string name, object value, object defaultValue)
		{
			if (!value.Equals(defaultValue))
			{
				descriptor.AddProperty(name, value);
			}
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x00070A9C File Offset: 0x0006EC9C
		public string GetXml()
		{
			XmlSerializer serializer = new XmlSerializer(base.GetType());
			StringWriter stringWriter = new StringWriter();
			this.GetXml(serializer, stringWriter);
			return stringWriter.ToString();
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x00070ACC File Offset: 0x0006ECCC
		public void LoadXml(string xml)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(base.GetType());
			ControlItemContainer deserialized = (ControlItemContainer)xmlSerializer.Deserialize(new StringReader(xml));
			this.LoadXml(deserialized);
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x00070AFE File Offset: 0x0006ECFE
		protected internal virtual ControlItem CreateItem(ClientStateLogEntry logEntry)
		{
			return null;
		}

		// Token: 0x06002184 RID: 8580
		protected internal abstract ControlItem CreateItem();

		// Token: 0x06002185 RID: 8581
		protected internal abstract void RaiseItemDataBound(ControlItem item);

		// Token: 0x06002186 RID: 8582
		protected abstract void RaiseItemCreated(ControlItem item);

		// Token: 0x06002187 RID: 8583 RVA: 0x00070B01 File Offset: 0x0006ED01
		protected virtual void RaiseTemplateNeeded(ControlItem item)
		{
		}

		// Token: 0x06002188 RID: 8584
		protected abstract ControlItemCollection CreateChildItemCollection();

		// Token: 0x06002189 RID: 8585 RVA: 0x00070B04 File Offset: 0x0006ED04
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

		// Token: 0x0600218A RID: 8586 RVA: 0x00070BDC File Offset: 0x0006EDDC
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

		// Token: 0x0600218B RID: 8587 RVA: 0x00070C38 File Offset: 0x0006EE38
		public virtual TControlItem FindChildByValue<TControlItem>(string value) where TControlItem : ControlItem
		{
			return this.FindChild<TControlItem>((TControlItem item) => item.Value == value);
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x00070C90 File Offset: 0x0006EE90
		public virtual TControlItem FindChildByValue<TControlItem>(string value, bool ignoreCase) where TControlItem : ControlItem
		{
			return this.FindChild<TControlItem>((TControlItem item) => string.Compare(item.Value, value, ignoreCase) == 0);
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x00070CE8 File Offset: 0x0006EEE8
		protected internal TControlItem FindChildByText<TControlItem>(string text) where TControlItem : ControlItem
		{
			return this.FindChild<TControlItem>((TControlItem item) => item.Text == text);
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x00070D40 File Offset: 0x0006EF40
		protected internal TControlItem FindChildByText<TControlItem>(string text, bool ignoreCase) where TControlItem : ControlItem
		{
			return this.FindChild<TControlItem>((TControlItem item) => string.Compare(item.Text, text, ignoreCase) == 0);
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x00070DA0 File Offset: 0x0006EFA0
		protected internal TControlItem FindChildByAttribute<TControlItem>(string attributeName, string attributeValue) where TControlItem : ControlItem
		{
			return this.FindChild<TControlItem>((TControlItem node) => node.Attributes[attributeName] == attributeValue);
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x00070DD3 File Offset: 0x0006EFD3
		protected internal virtual void InitializeItem(ControlItem item)
		{
			this.RaiseTemplateNeeded(item);
			this.ApplyTemplate(item);
			this.RaiseItemCreated(item);
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x00070DEA File Offset: 0x0006EFEA
		protected virtual void ReadXml(XmlReader reader)
		{
			XmlPersister.Deserialize(this, base.Attributes, null, reader);
			this.ReadXmlForChildren(reader);
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x00070E01 File Offset: 0x0006F001
		protected virtual Type GetItemTypeFromXmlTagName(string xmlTagName)
		{
			return this.CreateItem().GetType();
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x00070E10 File Offset: 0x0006F010
		protected virtual void ReadXmlForChildren(XmlReader reader)
		{
			Type type = null;
			while (reader.Read())
			{
				if (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Comment)
				{
					string a = null;
					using (XmlReader xmlReader = reader.ReadSubtree())
					{
						if (a != xmlReader.Name)
						{
							a = xmlReader.Name;
							type = this.GetItemTypeFromXmlTagName(reader.Name);
						}
						XmlSerializer xmlSerializer = new XmlSerializer(type);
						ControlItem item = (ControlItem)xmlSerializer.Deserialize(xmlReader);
						this.Children.Add(item);
					}
					reader.MoveToContent();
				}
			}
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x00070EAC File Offset: 0x0006F0AC
		protected virtual void WriteXml(XmlWriter writer)
		{
			XmlPersister.SerializePropertiesAsAttributes(this, writer);
			XmlPersister.SerializeAttributeCollectionAsAttributes(base.Attributes, writer);
			this.WriteXmlForChildren(writer);
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x00070EC8 File Offset: 0x0006F0C8
		protected virtual void WriteXmlForChildren(XmlWriter writer)
		{
			foreach (object obj in this.Children)
			{
				ControlItem controlItem = (ControlItem)obj;
				XmlSerializer xmlSerializer = new XmlSerializer(controlItem.GetType());
				xmlSerializer.Serialize(writer, controlItem);
			}
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x00070F30 File Offset: 0x0006F130
		protected virtual void GetXml(XmlSerializer serializer, TextWriter output)
		{
			serializer.Serialize(output, this);
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x00070F3C File Offset: 0x0006F13C
		protected virtual void LoadXml(ControlItemContainer deserialized)
		{
			this.Children.Clear();
			XmlPersister.MergeObjects(deserialized, this);
			foreach (object obj in deserialized.Attributes.Keys)
			{
				string key = (string)obj;
				base.Attributes[key] = deserialized.Attributes[key];
			}
			ControlItem[] array = new ControlItem[deserialized.Children.Count];
			deserialized.Children.CopyTo(array, 0);
			this.Children.AddRange(array);
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x00070FE8 File Offset: 0x0006F1E8
		private void ApplyTemplate(ControlItem item)
		{
			if (item.TemplateInstantiated)
			{
				return;
			}
			if (item.Template == null && this.Template == null)
			{
				return;
			}
			int num = item.Controls.Count;
			if (item.Template != null)
			{
				item.Template.InstantiateIn(item);
			}
			else if (this.Template != null)
			{
				this.Template.InstantiateIn(item);
			}
			while (num > 0 && !item.Controls.IsReadOnly)
			{
				item.Controls.Add(item.Controls[0]);
				num--;
			}
			item.TemplateInstantiated = true;
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x0007107C File Offset: 0x0006F27C
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			if (array[1] == null)
			{
				this.Children.Clear();
				return;
			}
			((IStateManager)this.Children).LoadViewState(array[1]);
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x000710B8 File Offset: 0x0006F2B8
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList
			{
				base.SaveViewState(),
				((IStateManager)this.Children).SaveViewState()
			};
			return arrayList.ToArray();
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x000710F2 File Offset: 0x0006F2F2
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Children).TrackViewState();
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x00071105 File Offset: 0x0006F305
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x00071111 File Offset: 0x0006F311
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.ReadXml(reader);
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x0007111A File Offset: 0x0006F31A
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer);
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x0600219F RID: 8607 RVA: 0x00071123 File Offset: 0x0006F323
		ControlItemCollection IControlItemContainer.Items
		{
			get
			{
				return this.Children;
			}
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x0007112C File Offset: 0x0006F32C
		protected internal virtual ControlItem FindItemByHierarchicalIndex(string hierarchicalIndex)
		{
			if (string.IsNullOrEmpty(hierarchicalIndex))
			{
				return null;
			}
			int num = Convert.ToInt32(hierarchicalIndex);
			if (num >= this.Children.Count)
			{
				return null;
			}
			return this.Children[num];
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x00071168 File Offset: 0x0006F368
		internal PostBackOptions GetPostBackOptions(Control control, string argument)
		{
			string postBackUrl = string.Empty;
			if (!string.IsNullOrEmpty(this.PostBackUrl))
			{
				postBackUrl = HttpUtility.UrlPathEncode(base.ResolveClientUrl(this.PostBackUrl));
			}
			return this.GetPostBackOptions(control, argument, this.ValidationGroup, postBackUrl);
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x000711A9 File Offset: 0x0006F3A9
		internal virtual bool RequiresValidation()
		{
			return this.CausesValidation && this.Page.GetValidators(this.ValidationGroup).Count > 0;
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x000711D0 File Offset: 0x0006F3D0
		internal PostBackOptions GetPostBackOptions(Control control, string argument, string validationGroup, string postBackUrl)
		{
			PostBackOptions postBackOptions = new PostBackOptions(control, argument)
			{
				ClientSubmit = true
			};
			if (this.Page != null)
			{
				if (this.RequiresValidation())
				{
					postBackOptions.PerformValidation = true;
					postBackOptions.ValidationGroup = validationGroup;
				}
				if (!string.IsNullOrEmpty(postBackUrl))
				{
					postBackOptions.ActionUrl = postBackUrl;
				}
			}
			return postBackOptions;
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x00071220 File Offset: 0x0006F420
		protected virtual string GetPostbackEventReference()
		{
			string postBackEventReference = this.Page.ClientScript.GetPostBackEventReference(this.GetPostBackOptions(this, "arguments"));
			return postBackEventReference.Replace("\"", "'");
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x0007125C File Offset: 0x0006F45C
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			if (!base.IsEnabled)
			{
				descriptor.AddProperty("enabled", false);
			}
			if (!string.IsNullOrEmpty(this.ClientDataSourceID))
			{
				if (!string.IsNullOrEmpty(this.DataTextField))
				{
					descriptor.AddProperty("_dataTextField", this.DataTextField);
				}
				if (!string.IsNullOrEmpty(this.DataValueField))
				{
					descriptor.AddProperty("_dataValueField", this.DataValueField);
				}
				try
				{
					Control control = DataSourceControlHelper.FindControl(this, this.ClientDataSourceID);
					descriptor.AddProperty("_clientDataSourceID", control.ClientID);
				}
				catch (Exception)
				{
					descriptor.AddProperty("_clientDataSourceID", this.ClientDataSourceID);
				}
			}
			if (this._keyboardNavigationSettings != null)
			{
				this.KeyboardNavigationSettings.Describe(descriptor);
			}
			base.DescribeComponent(descriptor);
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x060021A6 RID: 8614 RVA: 0x0007132C File Offset: 0x0006F52C
		internal bool InDesignMode
		{
			get
			{
				return base.DesignMode;
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x060021A7 RID: 8615 RVA: 0x00071334 File Offset: 0x0006F534
		IList IItemContainer.Children
		{
			get
			{
				return this.Children;
			}
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x0007133C File Offset: 0x0006F53C
		IItem IItemContainer.CreateItem()
		{
			return this.CreateItem();
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x00071344 File Offset: 0x0006F544
		void IItemContainer.RaiseItemDataBound(IItem item)
		{
			this.RaiseItemDataBound((ControlItem)item);
		}

		// Token: 0x040008AE RID: 2222
		private ControlItemCollection _children;

		// Token: 0x040008AF RID: 2223
		private KeyboardNavigationSettings _keyboardNavigationSettings;

		// Token: 0x0200039D RID: 925
		internal static class Helpers
		{
			// Token: 0x060021AB RID: 8619 RVA: 0x0007135C File Offset: 0x0006F55C
			public static IList<T> GetRowItems<T>(int rowIndex, int numberOfRows, IList<T> items)
			{
				Queue<T>[] array = new Queue<T>[numberOfRows];
				for (int i = 0; i < numberOfRows; i++)
				{
					array[i] = new Queue<T>();
				}
				Queue<T> queue = array[numberOfRows - 1];
				foreach (T item in items)
				{
					queue.Enqueue(item);
					ControlItemContainer.Helpers.BalanceRows<T>(array);
				}
				return array[rowIndex].ToArray();
			}

			// Token: 0x060021AC RID: 8620 RVA: 0x000713D8 File Offset: 0x0006F5D8
			private static void BalanceRows<T>(Queue<T>[] rowQueues)
			{
				for (int i = rowQueues.Length - 1; i > 0; i--)
				{
					Queue<T> queue = rowQueues[i];
					Queue<T> queue2 = rowQueues[i - 1];
					if (queue.Count == queue2.Count)
					{
						return;
					}
					queue2.Enqueue(queue.Dequeue());
				}
			}

			// Token: 0x060021AD RID: 8621 RVA: 0x0007141C File Offset: 0x0006F61C
			public static IList<T> GetColumnItems<T>(int columnIndex, int numberOfColumns, IList<T> items)
			{
				List<T> list = new List<T>();
				int num = 0;
				for (int i = 0; i < items.Count; i++)
				{
					if (num == columnIndex)
					{
						list.Add(items[i]);
					}
					num = (num + 1) % numberOfColumns;
				}
				return list;
			}

			// Token: 0x060021AE RID: 8622 RVA: 0x0007145C File Offset: 0x0006F65C
			public static IList<T> GetFlattenedColumnItems<T>(int rowsCount, int rowIndex, int numberOfColumns, IList<T> items) where T : new()
			{
				List<T> list = new List<T>();
				if (rowsCount <= 1)
				{
					return items;
				}
				for (int i = 0; i < items.Count; i++)
				{
					if ((i - rowIndex) % rowsCount == 0)
					{
						list.Add(items[i]);
					}
				}
				return list;
			}
		}
	}
}
