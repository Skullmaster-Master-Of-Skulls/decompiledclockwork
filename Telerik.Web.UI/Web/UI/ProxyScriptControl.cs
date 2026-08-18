using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000FCF RID: 4047
	internal class ProxyScriptControl : Control, IScriptControl
	{
		// Token: 0x06009D19 RID: 40217 RVA: 0x0022F420 File Offset: 0x0022D620
		public ProxyScriptControl(Control control)
		{
			this.target = control;
			this.isRadControl = ProxyScriptControl.InheritsFromRadControl(control);
		}

		// Token: 0x06009D1A RID: 40218 RVA: 0x0022F43C File Offset: 0x0022D63C
		internal static bool GetKeepOriginalOrderOfScriptDescriptorsDuringAjax()
		{
			string text = ConfigurationManager.AppSettings["KeepOriginalOrderOfScriptDescriptorsDuringAjax"];
			return !string.IsNullOrEmpty(text) && text == "true";
		}

		// Token: 0x06009D1B RID: 40219 RVA: 0x0022F470 File Offset: 0x0022D670
		private bool IsAjaxRequestByManager(bool expected)
		{
			Control control = this;
			while (control.Parent != null)
			{
				control = control.Parent;
				if (control is RadAjaxManager)
				{
					return ((RadAjaxManager)control).IsAjaxRequest == expected;
				}
			}
			return RadAjaxManager.GetCurrent(this.Page) != null && RadAjaxManager.GetCurrent(this.Page).IsAjaxRequest == expected;
		}

		// Token: 0x06009D1C RID: 40220 RVA: 0x0022F4CC File Offset: 0x0022D6CC
		public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			if (!ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack || !this.IsAjaxRequestByManager(false))
			{
				if (this.target is RadAjaxPanel)
				{
					if (!((RadAjaxPanel)this.target).IsAjaxRequest)
					{
						return null;
					}
				}
				else if (!this.IsAjaxRequestByManager(true) || !this.target.Visible)
				{
					return null;
				}
			}
			if (!(this.target is RadAjaxControl))
			{
				RenderOnceChecker renderOnceChecker = new RenderOnceChecker(this.Page.Items);
				if (!renderOnceChecker.ShouldRenderScripts(this.target))
				{
					return null;
				}
				renderOnceChecker.ScriptRendered(this.target);
			}
			ArrayList arrayList = new ArrayList();
			if (ProxyScriptControl.GetKeepOriginalOrderOfScriptDescriptorsDuringAjax() && this.target.HasControls() && !(this.target is RadAjaxManager))
			{
				this.GetChildScriptDescriptorsRecursive(this.target, arrayList);
			}
			if (this.target is IScriptControl)
			{
				IEnumerable<ScriptDescriptor> scriptDescriptors = ((IScriptControl)this.target).GetScriptDescriptors();
				if (scriptDescriptors != null)
				{
					foreach (ScriptDescriptor value in scriptDescriptors)
					{
						arrayList.Add(value);
					}
				}
			}
			if (this.target is IExtenderControl)
			{
				Control targetControl = this.target.NamingContainer.FindControl(((ExtenderControl)this.target).TargetControlID);
				IEnumerable<ScriptDescriptor> scriptDescriptors2 = ((IExtenderControl)this.target).GetScriptDescriptors(targetControl);
				if (scriptDescriptors2 != null)
				{
					foreach (ScriptDescriptor scriptDescriptor in scriptDescriptors2)
					{
						ScriptControlDescriptor value2 = (ScriptControlDescriptor)scriptDescriptor;
						arrayList.Add(value2);
					}
				}
			}
			if (this.isRadControl)
			{
				ProxyScriptControl.AddRadControlScriptDescriptors(arrayList, this.target);
			}
			if (!ProxyScriptControl.GetKeepOriginalOrderOfScriptDescriptorsDuringAjax() && this.target.HasControls() && !(this.target is RadAjaxManager))
			{
				this.GetChildScriptDescriptorsRecursive(this.target, arrayList);
				arrayList.Reverse();
			}
			if (arrayList.Count > 0)
			{
				return (ScriptDescriptor[])arrayList.ToArray(typeof(ScriptDescriptor));
			}
			return null;
		}

		// Token: 0x06009D1D RID: 40221 RVA: 0x0022F6F4 File Offset: 0x0022D8F4
		internal UpdatePanel GetUpdatePanel(Control parent)
		{
			foreach (object obj in parent.Controls)
			{
				Control control = (Control)obj;
				if (control is OurUpdatePanel)
				{
					return (UpdatePanel)control;
				}
			}
			return null;
		}

		// Token: 0x06009D1E RID: 40222 RVA: 0x0022F75C File Offset: 0x0022D95C
		internal RadAjaxPanel FindAjaxPanel(Control control)
		{
			for (Control parent = control.Parent; parent != null; parent = parent.Parent)
			{
				RadAjaxPanel radAjaxPanel = parent as RadAjaxPanel;
				if (radAjaxPanel != null)
				{
					return radAjaxPanel;
				}
			}
			return null;
		}

		// Token: 0x06009D1F RID: 40223 RVA: 0x0022F78C File Offset: 0x0022D98C
		private void GetChildScriptDescriptorsRecursive(Control parent, ArrayList scriptDescriptors)
		{
			foreach (object obj in parent.Controls)
			{
				Control control = (Control)obj;
				if (control.Visible)
				{
					if (ProxyScriptControl.GetKeepOriginalOrderOfScriptDescriptorsDuringAjax() && control.HasControls() && !(control is OurUpdatePanel))
					{
						this.GetChildScriptDescriptorsRecursive(control, scriptDescriptors);
					}
					RenderOnceChecker renderOnceChecker = new RenderOnceChecker(this.Page.Items);
					if (renderOnceChecker.ShouldRenderScripts(control))
					{
						renderOnceChecker.ScriptRendered(control);
						if ((ProxyScriptControl.InheritsFrom(control, typeof(BaseValidator).Name) || ProxyScriptControl.InheritsFrom(control, typeof(ValidationSummary).Name)) && RadAjaxControl.HasReflectionPermission())
						{
							RadAjaxManager current = RadAjaxManager.GetCurrent(control.Page);
							if (current != null && !(this.target is RadAjaxPanel))
							{
								FieldInfo field = typeof(Control).GetField("_parent", BindingFlags.Instance | BindingFlags.NonPublic);
								field.SetValue(control, current.selfUpdatePanel);
							}
							else
							{
								RadAjaxPanel radAjaxPanel = this.FindAjaxPanel(control);
								if (radAjaxPanel != null)
								{
									UpdatePanel updatePanel = this.GetUpdatePanel(radAjaxPanel);
									if (updatePanel != null)
									{
										FieldInfo field2 = typeof(Control).GetField("_parent", BindingFlags.Instance | BindingFlags.NonPublic);
										field2.SetValue(control, updatePanel);
									}
								}
							}
						}
						IScriptControl scriptControl = control as IScriptControl;
						if (scriptControl != null)
						{
							IEnumerable<ScriptDescriptor> scriptDescriptors2 = scriptControl.GetScriptDescriptors();
							if (scriptDescriptors2 != null)
							{
								foreach (ScriptDescriptor value in scriptDescriptors2)
								{
									scriptDescriptors.Add(value);
								}
							}
						}
						IExtenderControl extenderControl = control as IExtenderControl;
						if (extenderControl != null)
						{
							Control targetControl = control.NamingContainer.FindControl(((ExtenderControl)control).TargetControlID);
							IEnumerable<ScriptDescriptor> scriptDescriptors3 = extenderControl.GetScriptDescriptors(targetControl);
							if (scriptDescriptors3 != null)
							{
								foreach (ScriptDescriptor value2 in scriptDescriptors3)
								{
									scriptDescriptors.Add(value2);
								}
							}
						}
						if (ProxyScriptControl.InheritsFromRadControl(control))
						{
							ProxyScriptControl.AddRadControlScriptDescriptors(scriptDescriptors, control);
						}
						RadScriptBlock radScriptBlock = control as RadScriptBlock;
						if (radScriptBlock != null)
						{
							for (Control control2 = this; control2 != null; control2 = control2.Parent)
							{
								if (control2 is OurUpdatePanel)
								{
									radScriptBlock.RegisterInScriptManager(control2, typeof(UpdatePanel));
									break;
								}
							}
						}
						if (!ProxyScriptControl.GetKeepOriginalOrderOfScriptDescriptorsDuringAjax() && control.HasControls() && !(control is OurUpdatePanel))
						{
							this.GetChildScriptDescriptorsRecursive(control, scriptDescriptors);
						}
					}
				}
			}
		}

		// Token: 0x06009D20 RID: 40224 RVA: 0x0022FA4C File Offset: 0x0022DC4C
		private static void AddRadControlScriptDescriptors(ArrayList scriptDescriptors, Control child)
		{
			PropertyInfo property = child.GetType().GetProperty("LocalizationScript", BindingFlags.Instance | BindingFlags.NonPublic);
			if (property != null)
			{
				string scriptCode = (string)property.GetValue(child, null);
				scriptDescriptors.Add((new ScriptDescriptor[]
				{
					new InitScriptDescriptor(scriptCode)
				})[0]);
			}
			property = child.GetType().GetProperty("InitScript", BindingFlags.Instance | BindingFlags.NonPublic);
			if (property != null)
			{
				string scriptCode2 = (string)property.GetValue(child, null);
				scriptDescriptors.Add((new ScriptDescriptor[]
				{
					new InitScriptDescriptor(scriptCode2)
				})[0]);
			}
		}

		// Token: 0x06009D21 RID: 40225 RVA: 0x0022FAE4 File Offset: 0x0022DCE4
		public IEnumerable<ScriptReference> GetScriptReferences()
		{
			if (!ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack || RadAjaxManager.GetCurrent(this.Page) == null || RadAjaxManager.GetCurrent(this.Page).IsAjaxRequest)
			{
				if (!(this.target is RadAjaxPanel))
				{
					if (RadAjaxManager.GetCurrent(this.Page) == null || !RadAjaxManager.GetCurrent(this.Page).IsAjaxRequest || !this.target.Visible)
					{
						return null;
					}
				}
				else if (!((RadAjaxPanel)this.target).IsAjaxRequest)
				{
					return null;
				}
			}
			List<ScriptReference> list = new List<ScriptReference>();
			this.GetControlScriptReferences(this.target, list);
			if (this.target.HasControls() && !(this.target is RadAjaxManager))
			{
				this.GetChildScriptReferencesRecursive(this.target, list);
			}
			return list;
		}

		// Token: 0x06009D22 RID: 40226 RVA: 0x0022FBAC File Offset: 0x0022DDAC
		private void GetChildScriptReferencesRecursive(Control parent, List<ScriptReference> scriptReferences)
		{
			foreach (object obj in parent.Controls)
			{
				Control control = (Control)obj;
				this.GetControlScriptReferences(control, scriptReferences);
				if (control.HasControls() && (!(control is OurUpdatePanel) || (!(control.Parent is RadAjaxPanel) && !(control.Parent is RadAjaxManager))))
				{
					this.GetChildScriptReferencesRecursive(control, scriptReferences);
				}
			}
		}

		// Token: 0x06009D23 RID: 40227 RVA: 0x0022FC38 File Offset: 0x0022DE38
		private void GetControlScriptReferences(Control control, List<ScriptReference> scriptReferences)
		{
			if (!control.Visible)
			{
				return;
			}
			IScriptControl scriptControl = control as IScriptControl;
			if (scriptControl != null)
			{
				IEnumerable<ScriptReference> scriptReferences2 = scriptControl.GetScriptReferences();
				if (scriptReferences2 != null)
				{
					foreach (ScriptReference item in scriptReferences2)
					{
						scriptReferences.Add(item);
					}
				}
			}
			IExtenderControl extenderControl = control as IExtenderControl;
			if (extenderControl != null)
			{
				IEnumerable<ScriptReference> scriptReferences3 = extenderControl.GetScriptReferences();
				if (scriptReferences3 != null)
				{
					foreach (ScriptReference item2 in scriptReferences3)
					{
						scriptReferences.Add(item2);
					}
				}
			}
			if (ProxyScriptControl.InheritsFromRadControl(control))
			{
				this.AddRadControlScriptReferences(scriptReferences, control);
			}
		}

		// Token: 0x06009D24 RID: 40228 RVA: 0x0022FD08 File Offset: 0x0022DF08
		private void AddRadControlScriptReferences(List<ScriptReference> result, Control control)
		{
			PropertyInfo property = control.GetType().GetProperty("JavaScriptFiles", BindingFlags.Instance | BindingFlags.NonPublic);
			if (property != null)
			{
				string[] array = (string[])property.GetValue(control, null);
				MethodInfo method = control.GetType().GetMethod("GetScriptInfo", BindingFlags.Instance | BindingFlags.NonPublic);
				if (method != null)
				{
					foreach (string text in array)
					{
						Pair pair = (Pair)method.Invoke(control, new object[]
						{
							text
						});
						bool flag = (bool)pair.First;
						ScriptReference scriptReference = null;
						if (flag)
						{
							MethodInfo method2 = control.GetType().GetMethod("GetWebResourceType", BindingFlags.Instance | BindingFlags.NonPublic);
							if (method2 != null)
							{
								Type type = (Type)method2.Invoke(control, null);
								scriptReference = new ScriptReference(pair.Second.ToString(), type.Assembly.FullName);
							}
						}
						else
						{
							scriptReference = new ScriptReference(pair.Second.ToString());
						}
						if (scriptReference != null)
						{
							result.Add(scriptReference);
						}
					}
				}
			}
		}

		// Token: 0x170031B9 RID: 12729
		// (get) Token: 0x06009D25 RID: 40229 RVA: 0x0022FE23 File Offset: 0x0022E023
		private ScriptManager scriptManager
		{
			get
			{
				if (this._scriptManager == null)
				{
					this._scriptManager = ScriptRegistrar.GetScriptManager(this);
				}
				return this._scriptManager;
			}
		}

		// Token: 0x06009D26 RID: 40230 RVA: 0x0022FE3F File Offset: 0x0022E03F
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.scriptManager.RegisterScriptControl<ProxyScriptControl>(this);
			base.EnsureID();
		}

		// Token: 0x06009D27 RID: 40231 RVA: 0x0022FE5A File Offset: 0x0022E05A
		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
			if (!base.DesignMode)
			{
				this.scriptManager.RegisterScriptDescriptors(this);
			}
		}

		// Token: 0x06009D28 RID: 40232 RVA: 0x0022FE78 File Offset: 0x0022E078
		public static bool InheritsFromRadControl(Control target)
		{
			Type type = target.GetType();
			while (type != typeof(object))
			{
				if (type.Name == "RadControl" && type.Namespace != "Telerik.Web.UI")
				{
					return true;
				}
				type = type.BaseType;
			}
			return false;
		}

		// Token: 0x06009D29 RID: 40233 RVA: 0x0022FED0 File Offset: 0x0022E0D0
		public static bool InheritsFrom(Control target, string typeToLookFor)
		{
			Type type = target.GetType();
			while (type != typeof(object))
			{
				if (type.Name == typeToLookFor)
				{
					return true;
				}
				type = type.BaseType;
			}
			return false;
		}

		// Token: 0x04002C3A RID: 11322
		private Control target;

		// Token: 0x04002C3B RID: 11323
		private bool isRadControl;

		// Token: 0x04002C3C RID: 11324
		private ScriptManager _scriptManager;
	}
}
