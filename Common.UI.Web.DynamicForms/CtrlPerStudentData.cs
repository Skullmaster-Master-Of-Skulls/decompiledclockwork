using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.ClientManager.Core.DynamicForms;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.UI.Web.DynamicControls.Controls;
using TechnoPro.Common.UI.Web.DynamicControls.Entity;

namespace TechnoPro.Common.UI.Web.DynamicForms.Controls
{
	// Token: 0x02000002 RID: 2
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:CtrlPerStudentData runat=server></{0}:CtrlPerStudentData>")]
	public class CtrlPerStudentData : WebControl, INamingContainer
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public CtrlPerStudentData()
		{
			Page page = (Page)HttpContext.Current.Handler;
			if (page != null)
			{
				string webResourceUrl = page.ClientScript.GetWebResourceUrl(base.GetType(), "TechnoPro.Common.UI.Web.DynamicForms.js.dform.js");
				page.ClientScript.RegisterClientScriptInclude(base.GetType(), "cwjs", webResourceUrl);
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020C5 File Offset: 0x000002C5
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020D0 File Offset: 0x000002D0
		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			foreach (Control control in this.ctrls)
			{
				if (control is IDynamicWebControl)
				{
					IDynamicWebControl dynamicWebControl = (IDynamicWebControl)control;
					string viewStateKey = dynamicWebControl.ViewStateKey;
					if (!string.IsNullOrEmpty(viewStateKey))
					{
						object dataFromViewState = this.ViewState[viewStateKey];
						dynamicWebControl.ChildLoadViewState(dataFromViewState);
					}
				}
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002158 File Offset: 0x00000358
		protected override object SaveViewState()
		{
			foreach (Control control in this.ctrls)
			{
				if (control is IDynamicWebControl)
				{
					IDynamicWebControl dynamicWebControl = (IDynamicWebControl)control;
					string viewStateKey = dynamicWebControl.ViewStateKey;
					if (!string.IsNullOrEmpty(viewStateKey))
					{
						object obj = dynamicWebControl.ChildSaveViewState();
						if (obj != null)
						{
							this.ViewState[viewStateKey] = obj;
						}
					}
				}
			}
			return base.SaveViewState();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000021E4 File Offset: 0x000003E4
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000021EC File Offset: 0x000003EC
		public int ScreenNum
		{
			get
			{
				return this.screenNum;
			}
			set
			{
				this.screenNum = value;
				if (this.screenNum > 0)
				{
					this.CreateDynamicControls();
				}
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002204 File Offset: 0x00000404
		public override void Dispose()
		{
			if (this.Controls != null)
			{
				foreach (object obj in this.Controls)
				{
					Control control = (Control)obj;
					if (control != null)
					{
						control.Dispose();
					}
				}
			}
			base.Dispose();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002270 File Offset: 0x00000470
		private Control FindControl(int controlId)
		{
			foreach (Control control in this.ctrls)
			{
				int num;
				if (control is IDynamicWebControl)
				{
					IDynamicWebControl dynamicWebControl = (IDynamicWebControl)control;
					if (((dynamicWebControl.DynamicField == null) ? 0 : dynamicWebControl.DynamicField.ControlId) == controlId)
					{
						return control;
					}
				}
				else if (control.ID != null && control.ID.Contains("_") && int.TryParse(control.ID.Substring(control.ID.IndexOf("_") + 1), out num) && num == controlId)
				{
					return control;
				}
			}
			return null;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002338 File Offset: 0x00000538
		protected override void CreateChildControls()
		{
			this.BuildControlHeiarchy();
			base.CreateChildControls();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002346 File Offset: 0x00000546
		protected override void Render(HtmlTextWriter writer)
		{
			writer.Write("<div class='cxform' id='cxform'>");
			base.Render(writer);
			writer.Write("</div>");
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002365 File Offset: 0x00000565
		protected override void OnInit(EventArgs e)
		{
			this.CreatAndInitializeControls();
			base.OnInit(e);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002374 File Offset: 0x00000574
		private void CreateDynamicControls()
		{
			string key = "formcontrols_" + this.screenNum.ToString();
			Cache cache = HttpContext.Current.Cache;
			Forest<DynamicFieldDTO> forest = (Forest<DynamicFieldDTO>)cache[key];
			forest = null;
			if (forest == null)
			{
				List<DynamicFieldDTO> list;
				forest = ((IDynamicFieldClientManager)new DynamicFieldClientManager()).LoadFieldsAsTree(new DynamicFormDTO
				{
					ScreenNum = this.screenNum
				}, out list);
				cache.Insert(key, forest, null, DateTime.Now.AddMinutes(5.0), TimeSpan.Zero);
			}
			this.dynamicControls = forest;
			this.CreateControls(this.dynamicControls.Nodes);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002410 File Offset: 0x00000610
		private void CreatAndInitializeControls()
		{
			this.wizard.ID = "wizard1";
			this.wizard.DisplaySideBar = false;
			this.wizard.Width = Unit.Percentage(100.0);
			this.wizard.DisplayCancelButton = true;
			this.wizard.CancelButtonClick += this.wizard_CancelButtonClick;
			this.wizard.FinishButtonClick += this.wizard_FinishButtonClick;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000248C File Offset: 0x0000068C
		private void wizard_FinishButtonClick(object sender, WizardNavigationEventArgs e)
		{
			HttpContext.Current.Response.Redirect("http://tpro.ca");
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000024A2 File Offset: 0x000006A2
		private void wizard_CancelButtonClick(object sender, EventArgs e)
		{
			HttpContext.Current.Response.Redirect("http://google.ca");
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000024B8 File Offset: 0x000006B8
		private void CreateControls(TreeNodeCollection<DynamicFieldDTO> parent)
		{
			foreach (TreeNode<DynamicFieldDTO> treeNode in parent)
			{
				DynamicFieldDTO value = treeNode.Value;
				if (value.ControlCode == eControlCode.TabPageStart)
				{
					WizardStep newWizardStep = this.GetNewWizardStep(value);
					this.ctrls.Add(newWizardStep);
				}
				else
				{
					Control control = this.CreateControl(value);
					if (control != null)
					{
						this.ctrls.Add(control);
						if (control is CtrlFileChooser)
						{
							((CtrlFileChooser)control).OnUploadCompleted += this.fc_OnUploadCompleted;
						}
					}
				}
				if (treeNode.Nodes.Count > 0)
				{
					this.CreateControls(treeNode.Nodes);
				}
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000257C File Offset: 0x0000077C
		private void fc_OnUploadCompleted(object sender, FileUploadedArgs e)
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002580 File Offset: 0x00000780
		private WizardStep GetNewWizardStep(DynamicFieldDTO field)
		{
			return new WizardStep
			{
				ID = "step_" + field.ControlId.ToString(),
				Title = field.ControlCaption
			};
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000025BC File Offset: 0x000007BC
		private Control CreateControl(DynamicFieldDTO field)
		{
			DynamicControlAttribute dynamicControlAttribute = field.ControlCode.GetAttribute<DynamicControlAttribute>();
			if (dynamicControlAttribute == null)
			{
				dynamicControlAttribute = new DynamicControlAttribute();
			}
			if (!string.IsNullOrEmpty(dynamicControlAttribute.WebFormsControlType))
			{
				Type type = Type.GetType(dynamicControlAttribute.WebFormsControlType);
				if (type != null)
				{
					object obj = Activator.CreateInstance(type, new object[]
					{
						field
					});
					if (obj == null)
					{
						return null;
					}
					return (Control)obj;
				}
			}
			return null;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002624 File Offset: 0x00000824
		private void BuildControlHeiarchy()
		{
			Stack<Control> stack = new Stack<Control>();
			if (this.dynamicControls.Find((DynamicFieldDTO p) => p.ControlCode == eControlCode.TabPageStart) != null)
			{
				stack.Push(this.wizard);
			}
			this.AddControls(this.dynamicControls.Nodes, ref stack);
			if (this.wizard.WizardSteps.Count > 0)
			{
				this.wizard.ActiveStepIndex = 0;
				this.Controls.Add(this.wizard);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000026B4 File Offset: 0x000008B4
		private void AddControls(TreeNodeCollection<DynamicFieldDTO> treeParent, ref Stack<Control> parentControls)
		{
			foreach (TreeNode<DynamicFieldDTO> treeNode in treeParent)
			{
				DynamicFieldDTO value = treeNode.Value;
				Control control = this.FindControl(value.ControlId);
				if (control != null)
				{
					if (parentControls.Count > 0)
					{
						Control control2 = parentControls.Peek();
						if (control2 is Wizard && control is WizardStep)
						{
							WizardStep wizardStep = (WizardStep)control;
							this.wizard.WizardSteps.Add(wizardStep);
						}
						else
						{
							control2.Controls.Add(control);
						}
					}
					else
					{
						this.Controls.Add(control);
					}
					if (value.ControlCode == eControlCode.PanelStart)
					{
						parentControls.Push(control);
						this.AddControls(treeNode.Nodes, ref parentControls);
						parentControls.Pop();
					}
					else if (value.ControlCode == eControlCode.TabPageStart)
					{
						parentControls.Push(control);
						this.AddControls(treeNode.Nodes, ref parentControls);
						parentControls.Pop();
					}
				}
				else if (treeNode.Nodes.Count > 0)
				{
					this.AddControls(treeNode.Nodes, ref parentControls);
				}
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000027E0 File Offset: 0x000009E0
		public void ShowData(List<DynamicDataDTO> data)
		{
			this.ShowData(data, this.dynamicControls.Nodes);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000027F4 File Offset: 0x000009F4
		private void ShowData(List<DynamicDataDTO> data, TreeNodeCollection<DynamicFieldDTO> parentNodes)
		{
			foreach (TreeNode<DynamicFieldDTO> treeNode in parentNodes)
			{
				DynamicFieldDTO field = treeNode.Value;
				if (field.ControlCode.GetAttribute<DynamicControlAttribute>().IsDataHolding)
				{
					DynamicDataDTO dynamicDataDTO = data.Find((DynamicDataDTO d) => d.Field.ControlId.Equals(field.ControlId));
					Control control = this.FindControl(field.ControlId);
					if (control != null && control is IDynamicWebControl)
					{
						IDynamicWebControl dynamicWebControl = (IDynamicWebControl)control;
						if (dynamicDataDTO == null)
						{
							dynamicWebControl.ClearData();
						}
						else
						{
							dynamicWebControl.ShowData(dynamicDataDTO);
						}
					}
				}
				if (treeNode.Nodes.Count > 0)
				{
					this.ShowData(data, treeNode.Nodes);
				}
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000028D4 File Offset: 0x00000AD4
		public IList<DynamicDataDTO> GetCurrentData()
		{
			List<DynamicDataDTO> result = new List<DynamicDataDTO>();
			this.GetCurrentData(this.dynamicControls.Nodes, ref result);
			return result;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000028FC File Offset: 0x00000AFC
		private void GetCurrentData(TreeNodeCollection<DynamicFieldDTO> parentNodes, ref List<DynamicDataDTO> data)
		{
			foreach (TreeNode<DynamicFieldDTO> treeNode in parentNodes)
			{
				DynamicFieldDTO value = treeNode.Value;
				if (value.ControlCode.GetAttribute<DynamicControlAttribute>().IsDataHolding)
				{
					Control control = this.FindControl(value.ControlId);
					if (control != null && control is IDynamicWebControl)
					{
						bool flag;
						DynamicDataDTO currentData = ((IDynamicWebControl)control).GetCurrentData(out flag);
						if (!flag)
						{
							data.Add(currentData);
						}
					}
				}
				if (treeNode.Nodes.Count > 0)
				{
					this.GetCurrentData(treeNode.Nodes, ref data);
				}
			}
		}

		// Token: 0x04000001 RID: 1
		private Forest<DynamicFieldDTO> dynamicControls = new Forest<DynamicFieldDTO>();

		// Token: 0x04000002 RID: 2
		private List<Control> ctrls = new List<Control>();

		// Token: 0x04000003 RID: 3
		private Wizard wizard = new Wizard();

		// Token: 0x04000004 RID: 4
		private int screenNum;
	}
}
