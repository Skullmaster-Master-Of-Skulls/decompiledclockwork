using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020018A4 RID: 6308
	public abstract class RadFilterExpressionItem : Control, INamingContainer
	{
		// Token: 0x0600F3ED RID: 62445 RVA: 0x00377920 File Offset: 0x00375B20
		internal void SetItemIndex(int index)
		{
			this._itemIndex = index;
		}

		// Token: 0x0600F3EE RID: 62446 RVA: 0x00377929 File Offset: 0x00375B29
		internal void SetOwnerGroup(RadFilterGroupExpressionItem ownerGroup)
		{
			this._ownerGroup = ownerGroup;
		}

		// Token: 0x0600F3EF RID: 62447 RVA: 0x00377932 File Offset: 0x00375B32
		internal void SetOwnerFilter(RadFilter owner)
		{
			this._ownerFilter = owner;
		}

		// Token: 0x1700497E RID: 18814
		// (get) Token: 0x0600F3F0 RID: 62448 RVA: 0x0037793B File Offset: 0x00375B3B
		public RadFilterGroupExpressionItem OwnerGroup
		{
			get
			{
				return this._ownerGroup;
			}
		}

		// Token: 0x1700497F RID: 18815
		// (get) Token: 0x0600F3F1 RID: 62449 RVA: 0x00377943 File Offset: 0x00375B43
		public RadFilter OwnerFilter
		{
			get
			{
				return this._ownerFilter;
			}
		}

		// Token: 0x17004980 RID: 18816
		// (get) Token: 0x0600F3F2 RID: 62450 RVA: 0x0037794B File Offset: 0x00375B4B
		public Panel FunctionalInterfaceContainer
		{
			get
			{
				if (this.functionalInterfaceContainer == null)
				{
					this.functionalInterfaceContainer = new Panel();
					this.functionalInterfaceContainer.CssClass = "rfMid";
				}
				return this.functionalInterfaceContainer;
			}
		}

		// Token: 0x17004981 RID: 18817
		// (get) Token: 0x0600F3F3 RID: 62451 RVA: 0x00377976 File Offset: 0x00375B76
		public Panel ToolsInterfaceContainer
		{
			get
			{
				if (this.toolsInternfaceContainer == null)
				{
					this.toolsInternfaceContainer = new Panel();
					this.toolsInternfaceContainer.CssClass = "rfTools";
				}
				return this.toolsInternfaceContainer;
			}
		}

		// Token: 0x17004982 RID: 18818
		// (get) Token: 0x0600F3F4 RID: 62452 RVA: 0x003779A1 File Offset: 0x00375BA1
		public virtual LinkButton RemoveButton
		{
			get
			{
				if (this.removeButton == null)
				{
					this.removeButton = new LinkButton();
				}
				return this.removeButton;
			}
		}

		// Token: 0x0600F3F5 RID: 62453 RVA: 0x003779BC File Offset: 0x00375BBC
		public void InitializeItem()
		{
			if (this._ownerFilter.RenderMode != RenderMode.Lightweight)
			{
				this.CreateLeftDecorator();
			}
			this.CreateFunctionalInterface();
			this.CreateToolsInterface();
			if (this._ownerFilter.RenderMode != RenderMode.Lightweight)
			{
				this.CreateRightDecorator();
			}
		}

		// Token: 0x17004983 RID: 18819
		// (get) Token: 0x0600F3F6 RID: 62454 RVA: 0x003779F4 File Offset: 0x00375BF4
		protected Control InterfaceContainer
		{
			get
			{
				if (this._interfaceContainer == null)
				{
					this._interfaceContainer = new Panel
					{
						CssClass = "rfDiv"
					};
					this.Controls.Add(this._interfaceContainer);
				}
				return this._interfaceContainer;
			}
		}

		// Token: 0x17004984 RID: 18820
		// (get) Token: 0x0600F3F7 RID: 62455 RVA: 0x00377A38 File Offset: 0x00375C38
		// (set) Token: 0x0600F3F8 RID: 62456 RVA: 0x00377A40 File Offset: 0x00375C40
		internal bool IsLastItem { get; set; }

		// Token: 0x0600F3F9 RID: 62457 RVA: 0x00377A49 File Offset: 0x00375C49
		protected override void RenderChildren(HtmlTextWriter writer)
		{
			if (this.IsLastItem)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rfLast");
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			base.RenderChildren(writer);
			writer.RenderEndTag();
		}

		// Token: 0x0600F3FA RID: 62458
		protected abstract void SetupFunctionInterface(Control container);

		// Token: 0x0600F3FB RID: 62459
		protected abstract void SetupToolsInterface(Control container);

		// Token: 0x0600F3FC RID: 62460 RVA: 0x00377A83 File Offset: 0x00375C83
		protected virtual void CreateFunctionalInterface()
		{
			this.InterfaceContainer.Controls.Add(this.FunctionalInterfaceContainer);
			this.SetupFunctionInterface(this.FunctionalInterfaceContainer);
		}

		// Token: 0x0600F3FD RID: 62461 RVA: 0x00377AA7 File Offset: 0x00375CA7
		protected virtual void CreateToolsInterface()
		{
			this.InterfaceContainer.Controls.Add(this.ToolsInterfaceContainer);
			this.SetupToolsInterface(this.ToolsInterfaceContainer);
		}

		// Token: 0x0600F3FE RID: 62462 RVA: 0x00377ACC File Offset: 0x00375CCC
		protected virtual void CreateLeftDecorator()
		{
			LiteralControl child = new LiteralControl(string.Format("<div class=\"{0}\"></div>", "rfLeft"));
			this.InterfaceContainer.Controls.Add(child);
		}

		// Token: 0x0600F3FF RID: 62463 RVA: 0x00377B00 File Offset: 0x00375D00
		protected virtual void CreateRightDecorator()
		{
			LiteralControl child = new LiteralControl(string.Format("<div class=\"{0}\"></div>", "rfRight"));
			this.InterfaceContainer.Controls.Add(child);
		}

		// Token: 0x0600F400 RID: 62464 RVA: 0x00377B34 File Offset: 0x00375D34
		protected override bool OnBubbleEvent(object source, EventArgs args)
		{
			CommandEventArgs commandEventArgs = args as CommandEventArgs;
			if (commandEventArgs != null && !(args is RadFilterCommandEventArgs))
			{
				RadFilterCommandEventArgs args2 = RadFilterCommandEventArgsFactory.CreateCommandEventArgs(this, source, commandEventArgs);
				base.RaiseBubbleEvent(this, args2);
				return true;
			}
			return base.OnBubbleEvent(source, args);
		}

		// Token: 0x0600F401 RID: 62465 RVA: 0x00377B70 File Offset: 0x00375D70
		internal void FireCommandEvent(string commandName, object commandArgument)
		{
			CommandEventArgs args = new CommandEventArgs(commandName, commandArgument);
			this.OnBubbleEvent(this, args);
		}

		// Token: 0x0600F402 RID: 62466 RVA: 0x00377B90 File Offset: 0x00375D90
		protected HyperLink BuildLink(string className, string text)
		{
			return new HyperLink
			{
				CssClass = className,
				NavigateUrl = "#",
				Text = text
			};
		}

		// Token: 0x17004985 RID: 18821
		// (get) Token: 0x0600F403 RID: 62467 RVA: 0x00377BBD File Offset: 0x00375DBD
		public virtual string HierarchicalIndex
		{
			get
			{
				if (this.OwnerGroup == null)
				{
					return "0";
				}
				return string.Format("{0}_{1}", this.ItemIndex, this.OwnerGroup.HierarchicalIndex);
			}
		}

		// Token: 0x17004986 RID: 18822
		// (get) Token: 0x0600F404 RID: 62468 RVA: 0x00377BED File Offset: 0x00375DED
		public override string ClientID
		{
			get
			{
				return string.Format("{0}__{1}", base.ClientID, this.HierarchicalIndex);
			}
		}

		// Token: 0x17004987 RID: 18823
		// (get) Token: 0x0600F405 RID: 62469 RVA: 0x00377C05 File Offset: 0x00375E05
		public virtual int ItemIndex
		{
			get
			{
				return this._itemIndex;
			}
		}

		// Token: 0x040045EC RID: 17900
		protected const string LeftDecoratorClassName = "rfLeft";

		// Token: 0x040045ED RID: 17901
		protected const string RightDecoratorClassName = "rfRight";

		// Token: 0x040045EE RID: 17902
		protected const string FunctionalInterfaceWrapperClassName = "rfMid";

		// Token: 0x040045EF RID: 17903
		protected const string ToolsInterfaceWrapperClassName = "rfTools";

		// Token: 0x040045F0 RID: 17904
		protected const string FieldNameChooserClassName = "rfField";

		// Token: 0x040045F1 RID: 17905
		protected const string FilterFunctionChooserClassName = "rfExp";

		// Token: 0x040045F2 RID: 17906
		protected const string RemoveExpressionClassName = "rfDel";

		// Token: 0x040045F3 RID: 17907
		protected const string AddExpressionClassName = "rfAddExp";

		// Token: 0x040045F4 RID: 17908
		protected const string AddExpressionGroupClassName = "rfAddGr";

		// Token: 0x040045F5 RID: 17909
		protected const string GroupOpertaionClassName = "rfOper";

		// Token: 0x040045F6 RID: 17910
		private RadFilter _ownerFilter;

		// Token: 0x040045F7 RID: 17911
		private int _itemIndex;

		// Token: 0x040045F8 RID: 17912
		private RadFilterGroupExpressionItem _ownerGroup;

		// Token: 0x040045F9 RID: 17913
		private Panel functionalInterfaceContainer;

		// Token: 0x040045FA RID: 17914
		private Panel toolsInternfaceContainer;

		// Token: 0x040045FB RID: 17915
		private LinkButton removeButton;

		// Token: 0x040045FC RID: 17916
		private Panel _interfaceContainer;
	}
}
