using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Reflection;
using System.Web.Handlers;
using System.Web.Resources;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000048 RID: 72
	[DefaultProperty("Path")]
	[TypeConverter(typeof(EmptyStringExpandableObjectConverter))]
	public class CompositeScriptReference : ScriptReferenceBase
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x000115F8 File Offset: 0x0000F7F8
		[ResourceDescription("CompositeScriptReference_Scripts")]
		[Category("Behavior")]
		[Editor("System.Web.UI.Design.CollectionEditorBase, System.Web.Extensions.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", typeof(UITypeEditor))]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[MergableProperty(false)]
		public ScriptReferenceCollection Scripts
		{
			get
			{
				if (this._scripts == null)
				{
					this._scripts = new ScriptReferenceCollection();
				}
				return this._scripts;
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00011614 File Offset: 0x0000F814
		protected internal override string GetUrl(ScriptManager scriptManager, bool zip)
		{
			bool flag = !scriptManager.DeploymentSectionRetail && (base.ScriptMode == ScriptMode.Debug || ((base.ScriptMode == ScriptMode.Inherit || base.ScriptMode == ScriptMode.Auto) && scriptManager.IsDebuggingEnabled));
			if (!string.IsNullOrEmpty(base.Path))
			{
				string text = base.Path;
				if (flag)
				{
					text = ScriptReferenceBase.GetDebugPath(text);
				}
				if (scriptManager.EnableScriptLocalization && base.ResourceUICultures != null && base.ResourceUICultures.Length != 0)
				{
					CultureInfo cultureInfo = CultureInfo.CurrentUICulture;
					string text2 = null;
					bool flag2 = false;
					while (!cultureInfo.Equals(CultureInfo.InvariantCulture))
					{
						text2 = cultureInfo.ToString();
						foreach (string text3 in base.ResourceUICultures)
						{
							if (string.Equals(text2, text3.Trim(), StringComparison.OrdinalIgnoreCase))
							{
								flag2 = true;
								break;
							}
						}
						if (flag2)
						{
							break;
						}
						cultureInfo = cultureInfo.Parent;
					}
					if (flag2)
					{
						text = text.Substring(0, text.Length - 2) + text2 + ".js";
					}
				}
				return base.ClientUrlResolver.ResolveClientUrl(text);
			}
			List<Tuple<Assembly, List<Tuple<string, CultureInfo>>>> list = new List<Tuple<Assembly, List<Tuple<string, CultureInfo>>>>();
			Tuple<Assembly, List<Tuple<string, CultureInfo>>> tuple = null;
			foreach (ScriptReference scriptReference in this.Scripts)
			{
				if (scriptManager.AjaxFrameworkMode != AjaxFrameworkMode.Explicit || !scriptReference.IsAjaxFrameworkScript(scriptManager) || !scriptReference.EffectiveResourceName.StartsWith("MicrosoftAjax.", StringComparison.Ordinal))
				{
					bool flag3 = !string.IsNullOrEmpty(scriptReference.EffectivePath);
					bool flag4 = !string.IsNullOrEmpty(scriptManager.ScriptPath) && !scriptReference.IgnoreScriptPath;
					Assembly assembly = null;
					string text4 = null;
					Assembly assembly2 = null;
					ScriptMode effectiveScriptMode = scriptReference.EffectiveScriptMode;
					bool isDebuggingEnabled = (effectiveScriptMode == ScriptMode.Inherit) ? flag : (effectiveScriptMode == ScriptMode.Debug);
					if (!flag3)
					{
						assembly = scriptReference.GetAssembly(scriptManager);
						text4 = scriptReference.EffectiveResourceName;
						scriptReference.DetermineResourceNameAndAssembly(scriptManager, isDebuggingEnabled, ref text4, ref assembly);
						if (assembly != scriptManager.AjaxFrameworkAssembly && assembly != AssemblyCache.SystemWebExtensions && AssemblyCache.IsAjaxFrameworkAssembly(assembly))
						{
							throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, AtlasWeb.ScriptReference_ResourceRequiresAjaxAssembly, new object[]
							{
								text4,
								assembly
							}));
						}
						if (!flag4)
						{
							assembly2 = assembly;
						}
					}
					CultureInfo cultureInfo2 = scriptReference.DetermineCulture(scriptManager);
					if (tuple == null || tuple.Item1 != assembly2)
					{
						tuple = new Tuple<Assembly, List<Tuple<string, CultureInfo>>>(assembly2, new List<Tuple<string, CultureInfo>>());
						list.Add(tuple);
					}
					if (flag3 || flag4)
					{
						if (flag3)
						{
							if (string.IsNullOrEmpty(scriptReference.Path))
							{
								text4 = scriptReference.GetPath(scriptManager, scriptReference.EffectivePath, scriptReference.ScriptInfo.DebugPath, isDebuggingEnabled);
							}
							else
							{
								text4 = scriptReference.GetPath(scriptManager, scriptReference.Path, null, isDebuggingEnabled);
							}
						}
						else
						{
							text4 = ScriptReference.GetScriptPath(text4, assembly, cultureInfo2, scriptManager.ScriptPath);
						}
						if (UrlPath.IsRelativeUrl(text4) && !UrlPath.IsAppRelativePath(text4))
						{
							text4 = UrlPath.Combine(base.ClientUrlResolver.AppRelativeTemplateSourceDirectory, text4);
						}
					}
					tuple.Item2.Add(new Tuple<string, CultureInfo>(text4, cultureInfo2));
				}
			}
			return ScriptResourceHandler.GetScriptResourceUrl(list, zip);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00011950 File Offset: 0x0000FB50
		[Obsolete("Use IsAjaxFrameworkScript(ScriptManager)")]
		protected internal override bool IsFromSystemWebExtensions()
		{
			foreach (ScriptReference scriptReference in this.Scripts)
			{
				if (scriptReference.EffectiveAssembly == AssemblyCache.SystemWebExtensions)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000119B0 File Offset: 0x0000FBB0
		protected internal override bool IsAjaxFrameworkScript(ScriptManager scriptManager)
		{
			foreach (ScriptReference scriptReference in this.Scripts)
			{
				if (scriptReference.IsAjaxFrameworkScript(scriptManager))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400010B RID: 267
		private ScriptReferenceCollection _scripts;
	}
}
