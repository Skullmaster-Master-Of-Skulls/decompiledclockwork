using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Common.Helpers;

namespace Telerik.Web.UI
{
	// Token: 0x020018A8 RID: 6312
	public class RadFilterSingleExpressionItem : RadFilterExpressionItem
	{
		// Token: 0x17004993 RID: 18835
		// (get) Token: 0x0600F423 RID: 62499 RVA: 0x0037849D File Offset: 0x0037669D
		public RadFilterNonGroupExpression Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x17004994 RID: 18836
		// (get) Token: 0x0600F424 RID: 62500 RVA: 0x003784A5 File Offset: 0x003766A5
		public string FieldName
		{
			get
			{
				return this.Expression.FieldName;
			}
		}

		// Token: 0x17004995 RID: 18837
		// (get) Token: 0x0600F425 RID: 62501 RVA: 0x003784B4 File Offset: 0x003766B4
		public bool IsSingleValue
		{
			get
			{
				IRadFilterValueExpression radFilterValueExpression = this.Expression as IRadFilterValueExpression;
				return radFilterValueExpression != null && radFilterValueExpression.Values.Count == 1;
			}
		}

		// Token: 0x17004996 RID: 18838
		// (get) Token: 0x0600F426 RID: 62502 RVA: 0x003784E0 File Offset: 0x003766E0
		public bool IsDoubleValue
		{
			get
			{
				IRadFilterValueExpression radFilterValueExpression = this.Expression as IRadFilterValueExpression;
				return radFilterValueExpression != null && radFilterValueExpression.Values.Count == 2;
			}
		}

		// Token: 0x17004997 RID: 18839
		// (get) Token: 0x0600F427 RID: 62503 RVA: 0x0037850C File Offset: 0x0037670C
		public WebControl InputControl
		{
			get
			{
				if (this.Editor == null)
				{
					return null;
				}
				return this.Editor.GetFirstInputControl(base.FunctionalInterfaceContainer);
			}
		}

		// Token: 0x17004998 RID: 18840
		// (get) Token: 0x0600F428 RID: 62504 RVA: 0x00378529 File Offset: 0x00376729
		public WebControl SecondInputControl
		{
			get
			{
				if (this.Editor == null)
				{
					return null;
				}
				return this.Editor.GetSecondInputControl(base.FunctionalInterfaceContainer);
			}
		}

		// Token: 0x17004999 RID: 18841
		// (get) Token: 0x0600F429 RID: 62505 RVA: 0x00378546 File Offset: 0x00376746
		public HyperLink FieldNameChooserLink
		{
			get
			{
				if (this.fieldNameChooserLink == null)
				{
					this.fieldNameChooserLink = base.BuildLink("rfField", this.Editor.RetrieveDisplayText());
				}
				return this.fieldNameChooserLink;
			}
		}

		// Token: 0x1700499A RID: 18842
		// (get) Token: 0x0600F42A RID: 62506 RVA: 0x00378572 File Offset: 0x00376772
		public HyperLink FilterFunctionChooserLink
		{
			get
			{
				if (this.filterFunctionChooserLink == null)
				{
					this.filterFunctionChooserLink = base.BuildLink("rfExp", this.RetrieveFilterFunctionString());
				}
				return this.filterFunctionChooserLink;
			}
		}

		// Token: 0x1700499B RID: 18843
		// (get) Token: 0x0600F42B RID: 62507 RVA: 0x00378599 File Offset: 0x00376799
		public Control BetweenDelimeter
		{
			get
			{
				if (this.Editor == null)
				{
					return null;
				}
				return this.Editor.GetBetweenDelimeterControl();
			}
		}

		// Token: 0x0600F42C RID: 62508 RVA: 0x003785B0 File Offset: 0x003767B0
		public RadFilterSingleExpressionItem(RadFilterNonGroupExpression expression)
		{
			this._expression = expression;
		}

		// Token: 0x0600F42D RID: 62509 RVA: 0x003785C0 File Offset: 0x003767C0
		protected override void SetupFunctionInterface(Control container)
		{
			this.Editor = base.OwnerFilter.FieldEditors.RetrieveEditorForFieldName(this.FieldName);
			HyperLink child = this.FieldNameChooserLink;
			container.Controls.Add(child);
			HyperLink child2 = this.FilterFunctionChooserLink;
			container.Controls.Add(child2);
			IRadFilterValueExpression radFilterValueExpression = this.Expression as IRadFilterValueExpression;
			if (radFilterValueExpression != null)
			{
				ArrayList values = radFilterValueExpression.Values;
				this.Editor.IsSingleValue = (values.Count == 1);
				this.Editor.BetweenDelimeterText = base.OwnerFilter.BetweenDelimeterText;
				this.Editor.InitializeEditor(container);
				this.Editor.SetEditorValues(values);
			}
			else if (base.OwnerFilter.IsClientOperationMode)
			{
				this.Editor.IsSingleValue = false;
				this.Editor.BetweenDelimeterText = base.OwnerFilter.BetweenDelimeterText;
				this.Editor.InitializeEditor(container);
			}
			this.ShowHideInputControls(radFilterValueExpression, base.OwnerFilter.IsClientOperationMode);
		}

		// Token: 0x0600F42E RID: 62510 RVA: 0x003786B8 File Offset: 0x003768B8
		private void ShowHideInputControls(IRadFilterValueExpression valueExpression, bool isClientOperationMode)
		{
			bool flag = valueExpression != null && valueExpression.Values.Count == 2;
			WebControl firstInputControl = this.Editor.GetFirstInputControl(null);
			WebControl secondInputControl = this.Editor.GetSecondInputControl(null);
			if (isClientOperationMode)
			{
				if (valueExpression == null)
				{
					this.HideControl(firstInputControl);
					this.HideControl(this.BetweenDelimeter as Label);
					this.HideControl(secondInputControl);
				}
				else if (!flag)
				{
					this.HideControl(this.BetweenDelimeter as Label);
					this.HideControl(secondInputControl);
				}
			}
			this.SetInputControlCssClass(firstInputControl);
			this.SetInputControlCssClass(secondInputControl);
		}

		// Token: 0x0600F42F RID: 62511 RVA: 0x00378744 File Offset: 0x00376944
		private void HideControl(WebControl control)
		{
			RadInputControl radInputControl = control as RadInputControl;
			if (radInputControl != null)
			{
				radInputControl.Display = false;
				return;
			}
			if (control != null)
			{
				control.Style.Add(HtmlTextWriterStyle.Display, "none");
			}
		}

		// Token: 0x0600F430 RID: 62512 RVA: 0x00378778 File Offset: 0x00376978
		private void SetInputControlCssClass(WebControl control)
		{
			RadInputControl radInputControl = control as RadInputControl;
			if (radInputControl != null)
			{
				radInputControl.WrapperCssClass = radInputControl.WrapperCssClass.Trim();
				RadInputControl radInputControl2 = radInputControl;
				radInputControl2.WrapperCssClass += " rfControl";
				return;
			}
			if (control != null)
			{
				control.CssClass = control.CssClass.Trim();
				control.CssClass += " rfControl";
			}
		}

		// Token: 0x0600F431 RID: 62513 RVA: 0x003787E1 File Offset: 0x003769E1
		internal ArrayList ExtractValues()
		{
			if ((this.Expression is IRadFilterValueExpression || base.OwnerFilter.IsClientOperationMode) && this.Editor != null)
			{
				return this.Editor.ExtractValues();
			}
			return new ArrayList();
		}

		// Token: 0x0600F432 RID: 62514 RVA: 0x00378818 File Offset: 0x00376A18
		protected override void SetupToolsInterface(Control container)
		{
			LinkButton removeButton = this.RemoveButton;
			removeButton.CausesValidation = false;
			removeButton.CssClass = "rfDel";
			removeButton.CommandName = "RemoveExpression";
			removeButton.ToolTip = base.OwnerFilter.RemoveToolTip;
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

		// Token: 0x0600F433 RID: 62515 RVA: 0x003788C4 File Offset: 0x00376AC4
		protected virtual string RetrieveFilterFunctionString()
		{
			return base.OwnerFilter.Localization.RetrieveFilterFunctionLocalizationString(this._expression.FilterFunction);
		}

		// Token: 0x04004608 RID: 17928
		private RadFilterNonGroupExpression _expression;

		// Token: 0x04004609 RID: 17929
		private HyperLink fieldNameChooserLink;

		// Token: 0x0400460A RID: 17930
		private HyperLink filterFunctionChooserLink;

		// Token: 0x0400460B RID: 17931
		protected RadFilterDataFieldEditor Editor;
	}
}
