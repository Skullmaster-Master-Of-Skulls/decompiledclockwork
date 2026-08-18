using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web.Resources;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000071 RID: 113
	internal class ScriptControlManager
	{
		// Token: 0x060003F9 RID: 1017 RVA: 0x00014740 File Offset: 0x00012940
		public ScriptControlManager(ScriptManager scriptManager)
		{
			this._scriptManager = scriptManager;
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0001474F File Offset: 0x0001294F
		private OrderedDictionary<IExtenderControl, List<Control>> ExtenderControls
		{
			get
			{
				if (this._extenderControls == null)
				{
					this._extenderControls = new OrderedDictionary<IExtenderControl, List<Control>>();
				}
				return this._extenderControls;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0001476A File Offset: 0x0001296A
		private OrderedDictionary<IScriptControl, int> ScriptControls
		{
			get
			{
				if (this._scriptControls == null)
				{
					this._scriptControls = new OrderedDictionary<IScriptControl, int>();
				}
				return this._scriptControls;
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00014785 File Offset: 0x00012985
		public void AddScriptReferences(List<ScriptReferenceBase> scriptReferences)
		{
			this.AddScriptReferencesForScriptControls(scriptReferences);
			this.AddScriptReferencesForExtenderControls(scriptReferences);
			this._scriptReferencesRegistered = true;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0001479C File Offset: 0x0001299C
		private void AddScriptReferencesForScriptControls(List<ScriptReferenceBase> scriptReferences)
		{
			if (this._scriptControls != null)
			{
				foreach (IScriptControl scriptControl in this._scriptControls.Keys)
				{
					ScriptControlManager.AddScriptReferenceForScriptControl(scriptReferences, scriptControl);
				}
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x000147F8 File Offset: 0x000129F8
		private static void AddScriptReferenceForScriptControl(List<ScriptReferenceBase> scriptReferences, IScriptControl scriptControl)
		{
			IEnumerable<ScriptReference> scriptReferences2 = scriptControl.GetScriptReferences();
			if (scriptReferences2 != null)
			{
				Control control = (Control)scriptControl;
				ClientUrlResolverWrapper clientUrlResolverWrapper = null;
				foreach (ScriptReference scriptReference in scriptReferences2)
				{
					if (scriptReference != null)
					{
						if (clientUrlResolverWrapper == null)
						{
							clientUrlResolverWrapper = new ClientUrlResolverWrapper(control);
						}
						scriptReference.ClientUrlResolver = clientUrlResolverWrapper;
						scriptReference.IsStaticReference = false;
						scriptReference.ContainingControl = control;
						scriptReferences.Add(scriptReference);
					}
				}
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0001487C File Offset: 0x00012A7C
		private void AddScriptReferencesForExtenderControls(List<ScriptReferenceBase> scriptReferences)
		{
			if (this._extenderControls != null)
			{
				foreach (IExtenderControl extenderControl in this._extenderControls.Keys)
				{
					ScriptControlManager.AddScriptReferenceForExtenderControl(scriptReferences, extenderControl);
				}
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x000148D8 File Offset: 0x00012AD8
		private static void AddScriptReferenceForExtenderControl(List<ScriptReferenceBase> scriptReferences, IExtenderControl extenderControl)
		{
			IEnumerable<ScriptReference> scriptReferences2 = extenderControl.GetScriptReferences();
			if (scriptReferences2 != null)
			{
				Control control = (Control)extenderControl;
				ClientUrlResolverWrapper clientUrlResolverWrapper = null;
				foreach (ScriptReference scriptReference in scriptReferences2)
				{
					if (scriptReference != null)
					{
						if (clientUrlResolverWrapper == null)
						{
							clientUrlResolverWrapper = new ClientUrlResolverWrapper(control);
						}
						scriptReference.ClientUrlResolver = clientUrlResolverWrapper;
						scriptReference.IsStaticReference = false;
						scriptReference.ContainingControl = control;
						scriptReferences.Add(scriptReference);
					}
				}
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001495C File Offset: 0x00012B5C
		private bool InControlTree(Control targetControl)
		{
			for (Control parent = targetControl.Parent; parent != null; parent = parent.Parent)
			{
				if (parent == this._scriptManager.Page)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0001498D File Offset: 0x00012B8D
		public void OnPagePreRender(object sender, EventArgs e)
		{
			this._pagePreRenderRaised = true;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00014998 File Offset: 0x00012B98
		public void RegisterExtenderControl<TExtenderControl>(TExtenderControl extenderControl, Control targetControl) where TExtenderControl : Control, IExtenderControl
		{
			if (extenderControl == null)
			{
				throw new ArgumentNullException("extenderControl");
			}
			if (targetControl == null)
			{
				throw new ArgumentNullException("targetControl");
			}
			ScriptControlManager.VerifyTargetControlType<TExtenderControl>(extenderControl, targetControl);
			if (!this._pagePreRenderRaised)
			{
				throw new InvalidOperationException(AtlasWeb.ScriptControlManager_RegisterExtenderControlTooEarly);
			}
			if (this._scriptReferencesRegistered)
			{
				throw new InvalidOperationException(AtlasWeb.ScriptControlManager_RegisterExtenderControlTooLate);
			}
			List<Control> list;
			if (!this.ExtenderControls.TryGetValue(extenderControl, out list))
			{
				list = new List<Control>();
				this.ExtenderControls[extenderControl] = list;
			}
			list.Add(targetControl);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00014A28 File Offset: 0x00012C28
		public void RegisterScriptControl<TScriptControl>(TScriptControl scriptControl) where TScriptControl : Control, IScriptControl
		{
			if (scriptControl == null)
			{
				throw new ArgumentNullException("scriptControl");
			}
			if (!this._pagePreRenderRaised)
			{
				throw new InvalidOperationException(AtlasWeb.ScriptControlManager_RegisterScriptControlTooEarly);
			}
			if (this._scriptReferencesRegistered)
			{
				throw new InvalidOperationException(AtlasWeb.ScriptControlManager_RegisterScriptControlTooLate);
			}
			int num;
			this.ScriptControls.TryGetValue(scriptControl, out num);
			num++;
			this.ScriptControls[scriptControl] = num;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00014A98 File Offset: 0x00012C98
		public void RegisterScriptDescriptors(IExtenderControl extenderControl)
		{
			if (extenderControl == null)
			{
				throw new ArgumentNullException("extenderControl");
			}
			Control control = extenderControl as Control;
			if (control == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.Common_ArgumentInvalidType, new object[]
				{
					typeof(Control).FullName
				}), "extenderControl");
			}
			List<Control> list;
			if (!this.ExtenderControls.TryGetValue(extenderControl, out list))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.ScriptControlManager_ExtenderControlNotRegistered, new object[]
				{
					control.ID
				}), "extenderControl");
			}
			foreach (Control control2 in list)
			{
				if (control2.Visible && this.InControlTree(control2))
				{
					IEnumerable<ScriptDescriptor> scriptDescriptors = extenderControl.GetScriptDescriptors(control2);
					this.RegisterScriptsForScriptDescriptors(scriptDescriptors, control);
				}
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00014B88 File Offset: 0x00012D88
		public void RegisterScriptDescriptors(IScriptControl scriptControl)
		{
			if (scriptControl == null)
			{
				throw new ArgumentNullException("scriptControl");
			}
			Control control = scriptControl as Control;
			if (control == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.Common_ArgumentInvalidType, new object[]
				{
					typeof(Control).FullName
				}), "scriptControl");
			}
			int num;
			if (!this.ScriptControls.TryGetValue(scriptControl, out num))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.ScriptControlManager_ScriptControlNotRegistered, new object[]
				{
					control.ID
				}), "scriptControl");
			}
			for (int i = 0; i < num; i++)
			{
				IEnumerable<ScriptDescriptor> scriptDescriptors = scriptControl.GetScriptDescriptors();
				this.RegisterScriptsForScriptDescriptors(scriptDescriptors, control);
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00014C34 File Offset: 0x00012E34
		private void RegisterScriptsForScriptDescriptors(IEnumerable<ScriptDescriptor> scriptDescriptors, Control control)
		{
			if (scriptDescriptors != null)
			{
				StringBuilder stringBuilder = null;
				foreach (ScriptDescriptor scriptDescriptor in scriptDescriptors)
				{
					if (scriptDescriptor != null)
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder();
							stringBuilder.AppendLine("Sys.Application.add_init(function() {");
						}
						stringBuilder.Append("    ");
						stringBuilder.AppendLine(scriptDescriptor.GetScript());
						scriptDescriptor.RegisterDisposeForDescriptor(this._scriptManager, control);
					}
				}
				if (stringBuilder != null)
				{
					stringBuilder.AppendLine("});");
					string script = stringBuilder.ToString();
					string key = this._scriptManager.CreateUniqueScriptKey();
					this._scriptManager.RegisterStartupScriptInternal(control, typeof(ScriptManager), key, script, true);
				}
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00014CF8 File Offset: 0x00012EF8
		private static void VerifyTargetControlType<TExtenderControl>(TExtenderControl extenderControl, Control targetControl) where TExtenderControl : Control, IExtenderControl
		{
			Type type = extenderControl.GetType();
			Type[] targetControlTypes = TargetControlTypeCache.GetTargetControlTypes(type);
			if (targetControlTypes.Length == 0)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.ScriptControlManager_NoTargetControlTypes, new object[]
				{
					type,
					typeof(TargetControlTypeAttribute)
				}));
			}
			Type type2 = targetControl.GetType();
			foreach (Type type3 in targetControlTypes)
			{
				if (type3.IsAssignableFrom(type2))
				{
					return;
				}
			}
			throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.ScriptControlManager_TargetControlTypeInvalid, new object[]
			{
				extenderControl.ID,
				targetControl.ID,
				type,
				type2
			}));
		}

		// Token: 0x0400017E RID: 382
		private OrderedDictionary<IExtenderControl, List<Control>> _extenderControls;

		// Token: 0x0400017F RID: 383
		private bool _pagePreRenderRaised;

		// Token: 0x04000180 RID: 384
		private OrderedDictionary<IScriptControl, int> _scriptControls;

		// Token: 0x04000181 RID: 385
		private ScriptManager _scriptManager;

		// Token: 0x04000182 RID: 386
		private bool _scriptReferencesRegistered;
	}
}
