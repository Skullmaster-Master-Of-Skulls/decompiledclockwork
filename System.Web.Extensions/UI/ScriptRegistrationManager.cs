using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Handlers;
using System.Web.Resources;
using System.Web.Script.Serialization;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x0200007A RID: 122
	internal sealed class ScriptRegistrationManager
	{
		// Token: 0x06000520 RID: 1312 RVA: 0x00018049 File Offset: 0x00016249
		public ScriptRegistrationManager(ScriptManager scriptManager)
		{
			this._scriptManager = scriptManager;
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00018058 File Offset: 0x00016258
		public List<RegisteredArrayDeclaration> ScriptArrays
		{
			get
			{
				if (this._scriptArrays == null)
				{
					this._scriptArrays = new List<RegisteredArrayDeclaration>();
				}
				return this._scriptArrays;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x00018073 File Offset: 0x00016273
		public List<RegisteredScript> ScriptBlocks
		{
			get
			{
				if (this._clientScriptBlocks == null)
				{
					this._clientScriptBlocks = new List<RegisteredScript>();
				}
				return this._clientScriptBlocks;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x0001808E File Offset: 0x0001628E
		public List<RegisteredDisposeScript> ScriptDisposes
		{
			get
			{
				if (this._scriptDisposes == null)
				{
					this._scriptDisposes = new List<RegisteredDisposeScript>();
				}
				return this._scriptDisposes;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x000180A9 File Offset: 0x000162A9
		public List<RegisteredExpandoAttribute> ScriptExpandos
		{
			get
			{
				if (this._expandos == null)
				{
					this._expandos = new List<RegisteredExpandoAttribute>();
				}
				return this._expandos;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x000180C4 File Offset: 0x000162C4
		public List<RegisteredHiddenField> ScriptHiddenFields
		{
			get
			{
				if (this._hiddenFields == null)
				{
					this._hiddenFields = new List<RegisteredHiddenField>();
				}
				return this._hiddenFields;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x000180DF File Offset: 0x000162DF
		public List<RegisteredScript> ScriptStartupBlocks
		{
			get
			{
				if (this._startupScriptBlocks == null)
				{
					this._startupScriptBlocks = new List<RegisteredScript>();
				}
				return this._startupScriptBlocks;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x000180FA File Offset: 0x000162FA
		public List<RegisteredScript> ScriptSubmitStatements
		{
			get
			{
				if (this._submitStatements == null)
				{
					this._submitStatements = new List<RegisteredScript>();
				}
				return this._submitStatements;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x00018115 File Offset: 0x00016315
		private Dictionary<ScriptKey, string> FallbackScripts
		{
			get
			{
				if (ScriptRegistrationManager._fallbackScripts == null)
				{
					ScriptRegistrationManager._fallbackScripts = new Dictionary<ScriptKey, string>();
				}
				return ScriptRegistrationManager._fallbackScripts;
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00018130 File Offset: 0x00016330
		private static void CheckScriptTagTweenSpace(RegisteredScript entry, string text, int start, int length)
		{
			string text2 = text.Substring(start, length);
			if (text2.Trim().Length != 0)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.ScriptRegistrationManager_InvalidChars, new object[]
				{
					entry.Type.FullName,
					entry.Key,
					text2
				}));
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0001818C File Offset: 0x0001638C
		private bool IsControlRegistrationActive(List<UpdatePanel> updatingUpdatePanels, Control child, bool pageAlwaysActive)
		{
			if (pageAlwaysActive)
			{
				Page page = child as Page;
				if (page == this._scriptManager.Page)
				{
					return true;
				}
			}
			if (updatingUpdatePanels != null && updatingUpdatePanels.Count > 0)
			{
				while (child != null)
				{
					if (child is UpdatePanel)
					{
						for (int i = 0; i < updatingUpdatePanels.Count; i++)
						{
							if (child == updatingUpdatePanels[i])
							{
								return true;
							}
						}
					}
					child = child.Parent;
				}
			}
			return false;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x000181F4 File Offset: 0x000163F4
		public static void RegisterArrayDeclaration(Control control, string arrayName, string arrayValue)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (control.Page == null)
			{
				throw new ArgumentException(AtlasWeb.ScriptRegistrationManager_ControlNotOnPage, "control");
			}
			control.Page.ClientScript.RegisterArrayDeclaration(arrayName, arrayValue);
			ScriptManager current = ScriptManager.GetCurrent(control.Page);
			if (current != null)
			{
				RegisteredArrayDeclaration item = new RegisteredArrayDeclaration(control, arrayName, arrayValue);
				current.ScriptRegistration.ScriptArrays.Add(item);
			}
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00018264 File Offset: 0x00016464
		public static void RegisterClientScriptBlock(Control control, Type type, string key, string script, bool addScriptTags)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (control.Page == null)
			{
				throw new ArgumentException(AtlasWeb.ScriptRegistrationManager_ControlNotOnPage, "control");
			}
			control.Page.ClientScript.RegisterClientScriptBlock(type, key, script, addScriptTags);
			ScriptManager current = ScriptManager.GetCurrent(control.Page);
			if (current != null)
			{
				RegisteredScript item = new RegisteredScript(RegisteredScriptType.ClientScriptBlock, control, type, key, script, addScriptTags);
				current.ScriptRegistration.ScriptBlocks.Add(item);
			}
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x000182DC File Offset: 0x000164DC
		public static void RegisterClientScriptInclude(Control control, Type type, string key, string url)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (control.Page == null)
			{
				throw new ArgumentException(AtlasWeb.ScriptRegistrationManager_ControlNotOnPage, "control");
			}
			control.Page.ClientScript.RegisterClientScriptInclude(type, key, url);
			ScriptManager current = ScriptManager.GetCurrent(control.Page);
			if (current != null)
			{
				RegisteredScript item = new RegisteredScript(control, type, key, url);
				current.ScriptRegistration.ScriptBlocks.Add(item);
			}
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001834C File Offset: 0x0001654C
		public static void RegisterFallbackScriptForAjaxPostbacks(Control control, Type type, string key, string fallbackExpression, string fallbackPath)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (control.Page == null)
			{
				throw new ArgumentException(AtlasWeb.ScriptRegistrationManager_ControlNotOnPage, "control");
			}
			ScriptManager current = ScriptManager.GetCurrent(control.Page);
			if (current != null)
			{
				current.ScriptRegistration.FallbackScripts[new ScriptKey(type, key)] = fallbackPath;
			}
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x000183A8 File Offset: 0x000165A8
		public static void RegisterClientScriptResource(Control control, Type type, string resourceName)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (control.Page == null)
			{
				throw new ArgumentException(AtlasWeb.ScriptRegistrationManager_ControlNotOnPage, "control");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (string.IsNullOrEmpty(resourceName))
			{
				throw new ArgumentNullException("resourceName");
			}
			ScriptManager current = ScriptManager.GetCurrent(control.Page);
			if (current == null)
			{
				control.Page.ClientScript.RegisterClientScriptResource(type, resourceName);
				return;
			}
			Assembly assemblyFromType = AssemblyResourceLoader.GetAssemblyFromType(type);
			ScriptReference scriptReference = new ScriptReference
			{
				Name = resourceName,
				Assembly = assemblyFromType.FullName,
				IsDirectRegistration = true,
				ClientUrlResolver = current
			};
			string urlInternal = scriptReference.GetUrlInternal(current, current.Zip);
			control.Page.ClientScript.RegisterClientScriptInclude(type, resourceName, urlInternal, true);
			RegisteredScript item = new RegisteredScript(control, type, resourceName, urlInternal);
			current.ScriptRegistration.ScriptBlocks.Add(item);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00018494 File Offset: 0x00016694
		internal void RegisterDispose(Control control, string disposeScript)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (control.Page == null)
			{
				throw new ArgumentException(AtlasWeb.ScriptRegistrationManager_ControlNotOnPage, "control");
			}
			if (disposeScript == null)
			{
				throw new ArgumentNullException("disposeScript");
			}
			Control parent = control.Parent;
			UpdatePanel updatePanel = null;
			while (parent != null)
			{
				updatePanel = (parent as UpdatePanel);
				if (updatePanel != null)
				{
					break;
				}
				parent = parent.Parent;
			}
			if (updatePanel != null)
			{
				RegisteredDisposeScript item = new RegisteredDisposeScript(control, disposeScript, updatePanel);
				this.ScriptDisposes.Add(item);
				if (!this._scriptManager.IsInAsyncPostBack)
				{
					JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
					StringBuilder stringBuilder = new StringBuilder(256);
					stringBuilder.Append("Sys.WebForms.PageRequestManager.getInstance()._registerDisposeScript(");
					javaScriptSerializer.Serialize(updatePanel.ClientID, stringBuilder);
					stringBuilder.Append(", ");
					javaScriptSerializer.Serialize(disposeScript, stringBuilder);
					stringBuilder.AppendLine(");");
					this._scriptManager.IPage.ClientScript.RegisterStartupScript(typeof(ScriptRegistrationManager), this._scriptManager.CreateUniqueScriptKey(), stringBuilder.ToString(), true);
				}
			}
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x000185A4 File Offset: 0x000167A4
		public static void RegisterExpandoAttribute(Control control, string controlId, string attributeName, string attributeValue, bool encode)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (control.Page == null)
			{
				throw new ArgumentException(AtlasWeb.ScriptRegistrationManager_ControlNotOnPage, "control");
			}
			control.Page.ClientScript.RegisterExpandoAttribute(controlId, attributeName, attributeValue, encode);
			ScriptManager current = ScriptManager.GetCurrent(control.Page);
			if (current != null)
			{
				RegisteredExpandoAttribute item = new RegisteredExpandoAttribute(control, controlId, attributeName, attributeValue, encode);
				current.ScriptRegistration.ScriptExpandos.Add(item);
			}
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00018618 File Offset: 0x00016818
		public static void RegisterHiddenField(Control control, string hiddenFieldName, string hiddenFieldInitialValue)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (control.Page == null)
			{
				throw new ArgumentException(AtlasWeb.ScriptRegistrationManager_ControlNotOnPage, "control");
			}
			control.Page.ClientScript.RegisterHiddenField(hiddenFieldName, hiddenFieldInitialValue);
			ScriptManager current = ScriptManager.GetCurrent(control.Page);
			if (current != null)
			{
				RegisteredHiddenField item = new RegisteredHiddenField(control, hiddenFieldName, hiddenFieldInitialValue);
				current.ScriptRegistration.ScriptHiddenFields.Add(item);
			}
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00018688 File Offset: 0x00016888
		public static void RegisterOnSubmitStatement(Control control, Type type, string key, string script)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (control.Page == null)
			{
				throw new ArgumentException(AtlasWeb.ScriptRegistrationManager_ControlNotOnPage, "control");
			}
			control.Page.ClientScript.RegisterOnSubmitStatement(type, key, script);
			ScriptManager current = ScriptManager.GetCurrent(control.Page);
			if (current != null)
			{
				RegisteredScript item = new RegisteredScript(RegisteredScriptType.OnSubmitStatement, control, type, key, script, false);
				current.ScriptRegistration.ScriptSubmitStatements.Add(item);
			}
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x000186FC File Offset: 0x000168FC
		public static void RegisterStartupScript(Control control, Type type, string key, string script, bool addScriptTags)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (control.Page == null)
			{
				throw new ArgumentException(AtlasWeb.ScriptRegistrationManager_ControlNotOnPage, "control");
			}
			control.Page.ClientScript.RegisterStartupScript(type, key, script, addScriptTags);
			ScriptManager current = ScriptManager.GetCurrent(control.Page);
			if (current != null)
			{
				RegisteredScript item = new RegisteredScript(RegisteredScriptType.ClientStartupScript, control, type, key, script, addScriptTags);
				current.ScriptRegistration.ScriptStartupBlocks.Add(item);
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00018774 File Offset: 0x00016974
		public void RenderActiveArrayDeclarations(List<UpdatePanel> updatePanels, HtmlTextWriter writer)
		{
			List<RegisteredArrayDeclaration> list = new List<RegisteredArrayDeclaration>();
			Control control = null;
			foreach (RegisteredArrayDeclaration registeredArrayDeclaration in this.ScriptArrays)
			{
				Control control2 = registeredArrayDeclaration.Control;
				bool flag = (control != null && control2 == control) || this.IsControlRegistrationActive(updatePanels, control2, true);
				if (flag)
				{
					control = control2;
					if (!list.Contains(registeredArrayDeclaration))
					{
						list.Add(registeredArrayDeclaration);
					}
				}
			}
			foreach (RegisteredArrayDeclaration registeredArrayDeclaration2 in list)
			{
				PageRequestManager.EncodeString(writer, "arrayDeclaration", registeredArrayDeclaration2.Name, registeredArrayDeclaration2.Value);
			}
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00018850 File Offset: 0x00016A50
		public void RenderActiveExpandos(List<UpdatePanel> updatePanels, HtmlTextWriter writer)
		{
			if (updatePanels == null)
			{
				return;
			}
			List<RegisteredExpandoAttribute> list = new List<RegisteredExpandoAttribute>();
			Control control = null;
			foreach (RegisteredExpandoAttribute registeredExpandoAttribute in this.ScriptExpandos)
			{
				Control control2 = registeredExpandoAttribute.Control;
				bool flag = (control != null && control2 == control) || this.IsControlRegistrationActive(updatePanels, control2, false);
				if (flag)
				{
					control = control2;
					if (!list.Contains(registeredExpandoAttribute))
					{
						list.Add(registeredExpandoAttribute);
					}
				}
			}
			foreach (RegisteredExpandoAttribute registeredExpandoAttribute2 in list)
			{
				string id = string.Concat(new string[]
				{
					"document.getElementById('",
					registeredExpandoAttribute2.ControlId,
					"')['",
					registeredExpandoAttribute2.Name,
					"']"
				});
				string content;
				if (registeredExpandoAttribute2.Encode)
				{
					content = "\"" + HttpUtility.JavaScriptStringEncode(registeredExpandoAttribute2.Value) + "\"";
				}
				else if (registeredExpandoAttribute2.Value != null)
				{
					content = "\"" + registeredExpandoAttribute2.Value + "\"";
				}
				else
				{
					content = "null";
				}
				PageRequestManager.EncodeString(writer, "expando", id, content);
			}
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x000189B8 File Offset: 0x00016BB8
		public void RenderActiveHiddenFields(List<UpdatePanel> updatePanels, HtmlTextWriter writer)
		{
			List<RegisteredHiddenField> list = new List<RegisteredHiddenField>();
			ListDictionary listDictionary = new ListDictionary(StringComparer.Ordinal);
			Control control = null;
			foreach (RegisteredHiddenField registeredHiddenField in this.ScriptHiddenFields)
			{
				Control control2 = registeredHiddenField.Control;
				bool flag = (control != null && control2 == control) || this.IsControlRegistrationActive(updatePanels, control2, true);
				if (flag)
				{
					control = control2;
					if (!listDictionary.Contains(registeredHiddenField.Name))
					{
						list.Add(registeredHiddenField);
						listDictionary.Add(registeredHiddenField.Name, registeredHiddenField);
					}
				}
			}
			foreach (RegisteredHiddenField registeredHiddenField2 in list)
			{
				PageRequestManager.EncodeString(writer, "hiddenField", registeredHiddenField2.Name, registeredHiddenField2.InitialValue);
			}
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00018AB8 File Offset: 0x00016CB8
		private void RenderActiveScriptBlocks(List<UpdatePanel> updatePanels, HtmlTextWriter writer, string token, List<RegisteredScript> scriptRegistrations)
		{
			List<RegisteredScript> list = new List<RegisteredScript>();
			ListDictionary listDictionary = new ListDictionary();
			Control control = null;
			foreach (RegisteredScript registeredScript in scriptRegistrations)
			{
				Control control2 = registeredScript.Control;
				bool flag = (control != null && control2 == control) || this.IsControlRegistrationActive(updatePanels, control2, true);
				if (flag)
				{
					control = control2;
					ScriptKey key = new ScriptKey(registeredScript.Type, registeredScript.Key);
					if (!listDictionary.Contains(key))
					{
						list.Add(registeredScript);
						listDictionary.Add(key, registeredScript);
					}
				}
			}
			foreach (RegisteredScript registeredScript2 in list)
			{
				if (string.IsNullOrEmpty(registeredScript2.Url))
				{
					if (registeredScript2.AddScriptTags)
					{
						PageRequestManager.EncodeString(writer, token, "ScriptContentNoTags", registeredScript2.Script);
					}
					else
					{
						ScriptRegistrationManager.WriteScriptWithTags(writer, token, registeredScript2);
					}
				}
				else
				{
					PageRequestManager.EncodeString(writer, token, "ScriptPath", registeredScript2.Url);
				}
				string id;
				if (ScriptRegistrationManager._fallbackScripts != null && ScriptRegistrationManager._fallbackScripts.TryGetValue(new ScriptKey(registeredScript2.Type, registeredScript2.Key), out id))
				{
					PageRequestManager.EncodeString(writer, "fallbackScript", id, null);
				}
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00018C28 File Offset: 0x00016E28
		public void RenderActiveScriptDisposes(List<UpdatePanel> updatePanels, HtmlTextWriter writer)
		{
			if (updatePanels == null)
			{
				return;
			}
			foreach (RegisteredDisposeScript registeredDisposeScript in this.ScriptDisposes)
			{
				if (this.IsControlRegistrationActive(updatePanels, registeredDisposeScript.ParentUpdatePanel, false))
				{
					PageRequestManager.EncodeString(writer, "scriptDispose", registeredDisposeScript.ParentUpdatePanel.ClientID, registeredDisposeScript.Script);
				}
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00018CA4 File Offset: 0x00016EA4
		public void RenderActiveScripts(List<UpdatePanel> updatePanels, HtmlTextWriter writer)
		{
			this.RenderActiveScriptBlocks(updatePanels, writer, "scriptBlock", this.ScriptBlocks);
			this.RenderActiveScriptBlocks(updatePanels, writer, "scriptStartupBlock", this.ScriptStartupBlocks);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00018CCC File Offset: 0x00016ECC
		public void RenderActiveSubmitStatements(List<UpdatePanel> updatePanels, HtmlTextWriter writer)
		{
			List<RegisteredScript> list = new List<RegisteredScript>();
			ListDictionary listDictionary = new ListDictionary();
			Control control = null;
			foreach (RegisteredScript registeredScript in this.ScriptSubmitStatements)
			{
				Control control2 = registeredScript.Control;
				bool flag = (control != null && control2 == control) || this.IsControlRegistrationActive(updatePanels, control2, true);
				if (flag)
				{
					control = control2;
					ScriptKey key = new ScriptKey(registeredScript.Type, registeredScript.Key);
					if (!listDictionary.Contains(key))
					{
						list.Add(registeredScript);
						listDictionary.Add(key, registeredScript);
					}
				}
			}
			foreach (RegisteredScript registeredScript2 in list)
			{
				PageRequestManager.EncodeString(writer, "onSubmit", null, registeredScript2.Script);
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00018DCC File Offset: 0x00016FCC
		private static void WriteScriptWithTags(HtmlTextWriter writer, string token, RegisteredScript activeRegistration)
		{
			string script = activeRegistration.Script;
			int num = 0;
			Match match = ScriptRegistrationManager.ScriptTagRegex.Match(script, num);
			while (match.Success)
			{
				ScriptRegistrationManager.CheckScriptTagTweenSpace(activeRegistration, script, num, match.Index - num);
				OrderedDictionary orderedDictionary = new OrderedDictionary();
				if (match.Groups["empty"].Captures.Count > 0)
				{
					num = match.Index + match.Length;
				}
				else
				{
					int num2 = match.Index + match.Length;
					int num3 = script.IndexOf("</script>", num2, StringComparison.OrdinalIgnoreCase);
					if (num3 == -1)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.ScriptRegistrationManager_NoCloseTag, new object[]
						{
							activeRegistration.Type.FullName,
							activeRegistration.Key
						}));
					}
					string value = script.Substring(num2, num3 - num2);
					orderedDictionary.Add("text", value);
					num = num3 + 9;
				}
				CaptureCollection captures = match.Groups["attrname"].Captures;
				CaptureCollection captures2 = match.Groups["attrval"].Captures;
				for (int i = 0; i < captures.Count; i++)
				{
					string key = captures[i].ToString();
					string text = captures2[i].ToString();
					text = HttpUtility.HtmlDecode(text);
					orderedDictionary.Add(key, text);
				}
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				if (AppSettings.UpdatePanelMaxScriptLength > 0)
				{
					javaScriptSerializer.MaxJsonLength = AppSettings.UpdatePanelMaxScriptLength;
				}
				string content = javaScriptSerializer.Serialize(orderedDictionary);
				PageRequestManager.EncodeString(writer, token, "ScriptContentWithTags", content);
				match = ScriptRegistrationManager.ScriptTagRegex.Match(script, num);
			}
			ScriptRegistrationManager.CheckScriptTagTweenSpace(activeRegistration, script, num, script.Length - num);
			if (num == 0)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.ScriptRegistrationManager_NoTags, new object[]
				{
					activeRegistration.Type.FullName,
					activeRegistration.Key
				}));
			}
		}

		// Token: 0x040001DA RID: 474
		private static Regex ScriptTagRegex = new Regex("<script(\\s+(?<attrname>\\w[-\\w:]*)(\\s*=\\s*\"(?<attrval>[^\"]*)\"|\\s*=\\s*'(?<attrval>[^']*)'))*\\s*(?<empty>/)?>", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x040001DB RID: 475
		private static Dictionary<ScriptKey, string> _fallbackScripts;

		// Token: 0x040001DC RID: 476
		private ScriptManager _scriptManager;

		// Token: 0x040001DD RID: 477
		private List<RegisteredDisposeScript> _scriptDisposes;

		// Token: 0x040001DE RID: 478
		private List<RegisteredArrayDeclaration> _scriptArrays;

		// Token: 0x040001DF RID: 479
		private List<RegisteredScript> _clientScriptBlocks;

		// Token: 0x040001E0 RID: 480
		private List<RegisteredScript> _startupScriptBlocks;

		// Token: 0x040001E1 RID: 481
		private List<RegisteredHiddenField> _hiddenFields;

		// Token: 0x040001E2 RID: 482
		private List<RegisteredExpandoAttribute> _expandos;

		// Token: 0x040001E3 RID: 483
		private List<RegisteredScript> _submitStatements;
	}
}
