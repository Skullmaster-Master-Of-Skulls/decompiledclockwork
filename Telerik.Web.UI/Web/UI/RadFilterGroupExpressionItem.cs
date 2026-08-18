using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Common.Helpers;

namespace Telerik.Web.UI
{
	// Token: 0x020018A5 RID: 6309
	public class RadFilterGroupExpressionItem : RadFilterExpressionItem
	{
		// Token: 0x17004988 RID: 18824
		// (get) Token: 0x0600F407 RID: 62471 RVA: 0x00377C15 File Offset: 0x00375E15
		public RadFilterGroupExpression Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x17004989 RID: 18825
		// (get) Token: 0x0600F408 RID: 62472 RVA: 0x00377C1D File Offset: 0x00375E1D
		public bool IsRootGroup
		{
			get
			{
				return this._isRootGroup;
			}
		}

		// Token: 0x1700498A RID: 18826
		// (get) Token: 0x0600F409 RID: 62473 RVA: 0x00377C25 File Offset: 0x00375E25
		public HyperLink GroupOperationChooserLink
		{
			get
			{
				if (this.groupOperationChooserLink == null)
				{
					this.groupOperationChooserLink = base.BuildLink("rfOper", this.RetrieveGroupOpertaionString());
				}
				return this.groupOperationChooserLink;
			}
		}

		// Token: 0x1700498B RID: 18827
		// (get) Token: 0x0600F40A RID: 62474 RVA: 0x00377C4C File Offset: 0x00375E4C
		public LinkButton AddExpressionButton
		{
			get
			{
				if (this.addExpressionButton == null)
				{
					this.addExpressionButton = new LinkButton();
				}
				return this.addExpressionButton;
			}
		}

		// Token: 0x1700498C RID: 18828
		// (get) Token: 0x0600F40B RID: 62475 RVA: 0x00377C67 File Offset: 0x00375E67
		public LinkButton AddGroupExpressionButton
		{
			get
			{
				if (this.addGroupExpressionButton == null)
				{
					this.addGroupExpressionButton = new LinkButton();
				}
				return this.addGroupExpressionButton;
			}
		}

		// Token: 0x1700498D RID: 18829
		// (get) Token: 0x0600F40C RID: 62476 RVA: 0x00377C82 File Offset: 0x00375E82
		public RadFilterExpressionContainer ExpressionContainer
		{
			get
			{
				if (this._expressionContainer == null)
				{
					this._expressionContainer = new RadFilterExpressionContainer();
					this.Controls.Add(this._expressionContainer);
				}
				return this._expressionContainer;
			}
		}

		// Token: 0x1700498E RID: 18830
		// (get) Token: 0x0600F40D RID: 62477 RVA: 0x00377CAE File Offset: 0x00375EAE
		public RadFilterItemsCollection ChildItems
		{
			get
			{
				if (this._childItemsCollection == null)
				{
					this._childItemsCollection = new RadFilterItemsCollection(this);
				}
				return this._childItemsCollection;
			}
		}

		// Token: 0x0600F40E RID: 62478 RVA: 0x00377CCA File Offset: 0x00375ECA
		public void AddChildItem(RadFilterExpressionItem item)
		{
			this.ChildItems.Add(item);
			this.ExpressionContainer.Controls.Add(item);
		}

		// Token: 0x0600F40F RID: 62479 RVA: 0x00377CE9 File Offset: 0x00375EE9
		public RadFilterGroupExpressionItem(RadFilterGroupExpression expression, bool isRootGroup)
		{
			this._isRootGroup = isRootGroup;
			this._expression = expression;
		}

		// Token: 0x0600F410 RID: 62480 RVA: 0x00377CFF File Offset: 0x00375EFF
		protected override void SetupFunctionInterface(Control container)
		{
			container.Controls.Add(this.GroupOperationChooserLink);
		}

		// Token: 0x0600F411 RID: 62481 RVA: 0x00377D14 File Offset: 0x00375F14
		protected override void SetupToolsInterface(Control container)
		{
			LinkButton linkButton = this.AddExpressionButton;
			linkButton.CausesValidation = false;
			linkButton.ToolTip = base.OwnerFilter.AddExpressionToolTip;
			linkButton.CssClass = "rfAddExp";
			linkButton.CommandName = "AddExpression";
			if (base.OwnerFilter.ResolvedRenderMode == RenderMode.Lightweight)
			{
				linkButton.Controls.Add(IconHelper.CreateIcon("filter-add-expression"));
			}
			else
			{
				linkButton.Text = base.OwnerFilter.AddExpressionToolTip;
			}
			if (base.OwnerFilter.IsClientOperationMode)
			{
				linkButton.OnClientClick = string.Format("$find('{0}').addExpression(this); return false;", base.OwnerFilter.ClientID);
			}
			container.Controls.Add(linkButton);
			LinkButton linkButton2 = this.AddGroupExpressionButton;
			linkButton2.Visible = base.OwnerFilter.ShowAddGroupExpressionButton;
			linkButton2.CausesValidation = false;
			linkButton2.ToolTip = base.OwnerFilter.AddGroupToolTip;
			linkButton2.CssClass = "rfAddGr";
			linkButton2.CommandName = "AddGroup";
			if (base.OwnerFilter.ResolvedRenderMode == RenderMode.Lightweight)
			{
				linkButton2.Controls.Add(IconHelper.CreateIcon("filter-add-group"));
			}
			else
			{
				linkButton2.Text = base.OwnerFilter.AddGroupToolTip;
			}
			if (base.OwnerFilter.IsClientOperationMode)
			{
				linkButton2.OnClientClick = string.Format("$find('{0}').addGroupItem(this); return false;", base.OwnerFilter.ClientID);
			}
			container.Controls.Add(linkButton2);
			LinkButton removeButton = this.RemoveButton;
			removeButton.CausesValidation = false;
			removeButton.CssClass = "rfDel";
			removeButton.Text = base.OwnerFilter.RemoveToolTip;
			removeButton.ToolTip = base.OwnerFilter.RemoveToolTip;
			removeButton.CommandName = "RemoveGroup";
			if (base.OwnerFilter.ResolvedRenderMode == RenderMode.Lightweight)
			{
				removeButton.Controls.Add(IconHelper.CreateIcon("delete"));
			}
			else
			{
				removeButton.Text = base.OwnerFilter.RemoveToolTip;
			}
			if (base.OwnerFilter.IsClientOperationMode)
			{
				removeButton.OnClientClick = string.Format("$find('{0}').removeExpression(this); return false;", base.OwnerFilter.ClientID);
			}
			container.Controls.Add(removeButton);
		}

		// Token: 0x0600F412 RID: 62482 RVA: 0x00377F20 File Offset: 0x00376120
		protected virtual string RetrieveGroupOpertaionString()
		{
			return base.OwnerFilter.Localization.RetrieveGroupLocalizationString(this._expression.GroupOperation);
		}

		// Token: 0x040045FE RID: 17918
		private bool _isRootGroup;

		// Token: 0x040045FF RID: 17919
		private RadFilterGroupExpression _expression;

		// Token: 0x04004600 RID: 17920
		private RadFilterExpressionContainer _expressionContainer;

		// Token: 0x04004601 RID: 17921
		private RadFilterItemsCollection _childItemsCollection;

		// Token: 0x04004602 RID: 17922
		private HyperLink groupOperationChooserLink;

		// Token: 0x04004603 RID: 17923
		private LinkButton addExpressionButton;

		// Token: 0x04004604 RID: 17924
		private LinkButton addGroupExpressionButton;
	}
}
