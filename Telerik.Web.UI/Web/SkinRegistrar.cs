using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using Telerik.Web.UI.Common;

namespace Telerik.Web
{
	// Token: 0x02000F58 RID: 3928
	public class SkinRegistrar
	{
		// Token: 0x17002F5C RID: 12124
		// (get) Token: 0x060095C1 RID: 38337 RVA: 0x00216F24 File Offset: 0x00215124
		private static Dictionary<string, MultiSkinAttributeCollection> EmbeddedSkinAttributesCache
		{
			get
			{
				if (SkinRegistrar.HasContext)
				{
					if (HttpContext.Current.Items["Telerik.EmbeddedSkinAttributeCache"] == null)
					{
						HttpContext.Current.Items["Telerik.EmbeddedSkinAttributeCache"] = new Dictionary<string, MultiSkinAttributeCollection>();
					}
					return HttpContext.Current.Items["Telerik.EmbeddedSkinAttributeCache"] as Dictionary<string, MultiSkinAttributeCollection>;
				}
				return new Dictionary<string, MultiSkinAttributeCollection>();
			}
		}

		// Token: 0x17002F5D RID: 12125
		// (get) Token: 0x060095C2 RID: 38338 RVA: 0x00216F88 File Offset: 0x00215188
		private static Dictionary<string, EmbeddedSkinAttribute> EmbeddedSkinsCache
		{
			get
			{
				if (SkinRegistrar.HasContext)
				{
					if (HttpContext.Current.Items["Telerik.EmbeddedSkinsCache"] == null)
					{
						HttpContext.Current.Items["Telerik.EmbeddedSkinsCache"] = new Dictionary<string, EmbeddedSkinAttribute>();
					}
					return HttpContext.Current.Items["Telerik.EmbeddedSkinsCache"] as Dictionary<string, EmbeddedSkinAttribute>;
				}
				return new Dictionary<string, EmbeddedSkinAttribute>();
			}
		}

		// Token: 0x17002F5E RID: 12126
		// (get) Token: 0x060095C3 RID: 38339 RVA: 0x00216FEA File Offset: 0x002151EA
		private static bool HasContext
		{
			get
			{
				return HttpContext.Current != null;
			}
		}

		// Token: 0x060095C4 RID: 38340 RVA: 0x00216FF8 File Offset: 0x002151F8
		public static string GetRuntimeSkin(ISkinnableControl control)
		{
			RadSkinManager radSkinManager = null;
			if (control != null && control.Page != null)
			{
				radSkinManager = RadSkinManager.GetCurrent(control.Page);
			}
			if (control.IsSkinSet)
			{
				return control.Skin;
			}
			if (radSkinManager != null && radSkinManager.Enabled && !string.IsNullOrEmpty(radSkinManager.Skin))
			{
				return radSkinManager.Skin;
			}
			return SkinRegistrar.GetGlobalSkin(control) ?? control.Skin;
		}

		// Token: 0x060095C5 RID: 38341 RVA: 0x0021705C File Offset: 0x0021525C
		public static List<string> GetEmbeddedSkinNames(Type controlType)
		{
			List<string> list = new List<string>();
			foreach (EmbeddedSkinAttribute embeddedSkinAttribute in SkinRegistrar.GetAllEmbeddedSkinAttributes(controlType))
			{
				if (!embeddedSkinAttribute.IsCommonCss)
				{
					list.Add(embeddedSkinAttribute.Skin);
				}
			}
			return list;
		}

		// Token: 0x060095C6 RID: 38342 RVA: 0x002170C0 File Offset: 0x002152C0
		public static string GetWebResourceUrl(Control control, string resourceName)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (string.IsNullOrEmpty(resourceName))
			{
				throw new ArgumentNullException("resourceName");
			}
			return SkinRegistrar.GetWebResourceUrl(control.Page, control.GetType(), resourceName);
		}

		// Token: 0x060095C7 RID: 38343 RVA: 0x002170F8 File Offset: 0x002152F8
		public static string GetWebResourceUrl(Page page, Type type, string resourceName)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (string.IsNullOrEmpty(resourceName))
			{
				throw new ArgumentNullException("resourceName");
			}
			if (SkinRegistrar.suppressRegistration || page == null)
			{
				return string.Empty;
			}
			string skinFromResourceName = SkinRegistrar.GetSkinFromResourceName(resourceName);
			if (!string.IsNullOrEmpty(skinFromResourceName))
			{
				return page.ClientScript.GetWebResourceUrl(SkinRegistrar.GetWebResourceType(type, skinFromResourceName, page), resourceName);
			}
			return page.ClientScript.GetWebResourceUrl(type, resourceName);
		}

		// Token: 0x060095C8 RID: 38344 RVA: 0x00217170 File Offset: 0x00215370
		public static string GetGlobalSkin(ISkinnableControl control)
		{
			IList<EmbeddedSkinAttribute> allEmbeddedSkinAttributes = SkinRegistrar.GetAllEmbeddedSkinAttributes(control.GetType());
			if (allEmbeddedSkinAttributes.Count == 0)
			{
				return null;
			}
			string text = ConfigurationManager.AppSettings[string.Format("Telerik.{0}.Skin", allEmbeddedSkinAttributes[0].ShortControlName)];
			if (text == null)
			{
				text = ConfigurationManager.AppSettings["Telerik.Skin"];
				if (text == null)
				{
					return null;
				}
			}
			return text;
		}

		// Token: 0x060095C9 RID: 38345 RVA: 0x002171CD File Offset: 0x002153CD
		public static string GetDesignTimeStyleSheet(ISkinnableControl control)
		{
			return SkinRegistrar.GetDesignTimeStyleSheet(control, string.Empty);
		}

		// Token: 0x060095CA RID: 38346 RVA: 0x002171DC File Offset: 0x002153DC
		internal static string GetDesignTimeStyleSheet(ISkinnableControl control, string suffix)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<style type='text/css'>");
			foreach (EmbeddedSkinAttribute embeddedSkinAttribute in SkinRegistrar.GetEmbeddedSkinAttributes(control, control.GetType(), true))
			{
				if (!string.IsNullOrEmpty(suffix))
				{
					embeddedSkinAttribute.Suffix = suffix;
				}
				stringBuilder.Append(SkinRegistrar.PerformSubstitution(control, embeddedSkinAttribute));
			}
			stringBuilder.Append("</style>");
			return stringBuilder.ToString();
		}

		// Token: 0x060095CB RID: 38347 RVA: 0x00217270 File Offset: 0x00215470
		internal static void RegisterCssReference(Page page, Control control, string url)
		{
			SkinRegistrar.RegisterCssReference(page, control.GetType(), url);
		}

		// Token: 0x060095CC RID: 38348 RVA: 0x0021727F File Offset: 0x0021547F
		internal static void RegisterCssReference(Page page, Control control, string url, int index)
		{
			SkinRegistrar.RegisterCssReference(page, control.GetType(), url, index);
		}

		// Token: 0x060095CD RID: 38349 RVA: 0x0021728F File Offset: 0x0021548F
		internal static void RegisterCssReference(Page page, string url)
		{
			SkinRegistrar.RegisterCssReference(page, page.GetType(), url);
		}

		// Token: 0x060095CE RID: 38350 RVA: 0x002172A0 File Offset: 0x002154A0
		public static void RegisterCssReferences(ISkinnableControl control)
		{
			if (SkinRegistrar.suppressRegistration || control.ResolvedRenderMode == RenderMode.Native)
			{
				return;
			}
			bool loadOnAjaxRequest = false;
			Control control2 = (Control)control;
			Type type = control.GetType();
			Page page = control2.Page;
			ScriptManager scriptManager = SkinRegistrar.GetScriptManager(control);
			ClientScriptManager clientScript = page.ClientScript;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (RequiredCssAttribute requiredCssAttribute in SkinRegistrar.GetRequiredCssAttributes(control, type))
			{
				StyleSheetReference styleSheetReference = new StyleSheetReference
				{
					Name = requiredCssAttribute.CssResourceName,
					Assembly = control.GetType().Assembly.FullName,
					IsRequiredCss = true,
					IsCommonCss = true
				};
				SkinRegistrar.ResolveStyleSheetReference(styleSheetReference, page, requiredCssAttribute.Type, requiredCssAttribute.CssResourceName);
				SkinRegistrar.RegisterStyleSheetReference(control, styleSheetReference, stringBuilder, requiredCssAttribute.Type);
			}
			foreach (EmbeddedSkinAttribute embeddedSkinAttribute in SkinRegistrar.GetEmbeddedSkinAttributes(control, type))
			{
				embeddedSkinAttribute.Suffix = control.GetSkinSuffix();
				StyleSheetReference styleSheetReference2 = new StyleSheetReference
				{
					Name = embeddedSkinAttribute.CssResourceName,
					Assembly = embeddedSkinAttribute.Type.Assembly.FullName,
					IsCommonCss = embeddedSkinAttribute.IsCommonCss
				};
				SkinRegistrar.ResolveStyleSheetReference(styleSheetReference2, page, embeddedSkinAttribute.Type, embeddedSkinAttribute.CssResourceName);
				SkinRegistrar.RegisterStyleSheetReference(control, styleSheetReference2, stringBuilder, embeddedSkinAttribute.Type);
			}
			if (control.EnableAjaxSkinRendering || !control.RegisterWithScriptManager)
			{
				loadOnAjaxRequest = true;
				control.EnableAjaxSkinRendering = false;
				foreach (object obj in control2.Controls)
				{
					Control control3 = (Control)obj;
					ISkinnableControl skinnableControl = control3 as ISkinnableControl;
					if (skinnableControl != null)
					{
						skinnableControl.EnableAjaxSkinRendering = true;
					}
				}
				control.AjaxCssRegistrations = stringBuilder.ToString();
				if (scriptManager.IsInAsyncPostBack && !clientScript.IsStartupScriptRegistered(page.GetType(), "CSS"))
				{
					ScriptManager.RegisterStartupScript(page, page.GetType(), "CSS", "if(typeof ($telerik)!='undefined'){$telerik.registerSkins();};", true);
				}
			}
			else
			{
				control.AjaxCssRegistrations = string.Empty;
			}
			SkinRegistrar.RegisterCustomSkinCssReferences(control, loadOnAjaxRequest);
		}

		// Token: 0x060095CF RID: 38351 RVA: 0x00217518 File Offset: 0x00215718
		internal static void RegisterStyleSheetReference(ISkinnableControl control, StyleSheetReference reference, StringBuilder cssRegs, Type controlToRegisterType)
		{
			Page page = control.Page;
			ClientScriptManager clientScript = page.ClientScript;
			ScriptManager scriptManager = SkinRegistrar.GetScriptManager(control);
			string text = reference.Path;
			string name = reference.Name;
			Type type = reference.IsRequiredCss ? page.GetType() : controlToRegisterType;
			if (clientScript.IsClientScriptBlockRegistered(type, name) && control.RegisterWithScriptManager)
			{
				return;
			}
			if (control.RegisterWithScriptManager)
			{
				clientScript.RegisterClientScriptBlock(type, name, string.Empty, false);
			}
			if (scriptManager.IsInAsyncPostBack || !control.RegisterWithScriptManager)
			{
				string str = text.Contains("?") ? "&" : "?";
				string arg = (scriptManager.IsInAsyncPostBack && HttpContext.Current.Request.Browser.IsBrowser("IE") && HttpContext.Current.Request.Browser.MajorVersion == 7) ? (str + "ie7CacheFix") : string.Empty;
				text = ((HttpContext.Current != null) ? HttpContext.Current.Server.HtmlEncode(text) : text.Replace("&amp;", "&").Replace("&", "&amp;"));
				cssRegs.Append(string.Format("<link class='Telerik_stylesheet' type='text/css' rel='stylesheet' href='{0}{1}' />", text, arg));
				return;
			}
			SkinRegistrar.RegisterCssReference(page, (Control)control, text);
		}

		// Token: 0x060095D0 RID: 38352 RVA: 0x00217664 File Offset: 0x00215864
		internal static ScriptManager GetScriptManager(ISkinnableControl control)
		{
			if (control.Page == null)
			{
				throw new InvalidOperationException("Page cannot be null. Please ensure that this operation is being performed in the context of an ASP.NET request.");
			}
			ScriptManager scriptManager = ScriptManager.GetCurrent(control.Page);
			if (scriptManager == null && control != null && !control.RegisterWithScriptManager)
			{
				scriptManager = new ScriptManager
				{
					ID = "dummyScriptManager"
				};
			}
			if (scriptManager == null)
			{
				throw new InvalidOperationException(string.Format("The control with ID '{0}' requires a ScriptManager on the page. The ScriptManager must appear before any controls that need it.", control.ID));
			}
			return scriptManager;
		}

		// Token: 0x060095D1 RID: 38353 RVA: 0x002176CB File Offset: 0x002158CB
		internal static List<EmbeddedSkinAttribute> GetEmbeddedSkinAttributes(ISkinnableControl control, Type controlType)
		{
			return SkinRegistrar.GetEmbeddedSkinAttributes(control, controlType, false);
		}

		// Token: 0x060095D2 RID: 38354 RVA: 0x002176FC File Offset: 0x002158FC
		internal static List<RequiredCssAttribute> GetRequiredCssAttributes(ISkinnableControl control, Type controlType)
		{
			return (from p in controlType.GetCustomAttributes(typeof(RequiredCssAttribute), true)
			select p as RequiredCssAttribute into p
			where p.RenderMode == control.ResolvedRenderMode
			select p).ToList<RequiredCssAttribute>();
		}

		// Token: 0x060095D3 RID: 38355 RVA: 0x00217760 File Offset: 0x00215960
		internal static List<EmbeddedSkinAttribute> GetEmbeddedSkinAttributes(ISkinnableControl control, Type controlType, bool designTime)
		{
			List<EmbeddedSkinAttribute> list = new List<EmbeddedSkinAttribute>();
			string runtimeSkin = SkinRegistrar.GetRuntimeSkin(control);
			bool flag = !control.EnableEmbeddedSkins;
			bool flag2 = !control.EnableEmbeddedBaseStylesheet;
			foreach (EmbeddedSkinAttribute embeddedSkinAttribute in SkinRegistrar.GetAllEmbeddedSkinAttributes(controlType, control.Page, designTime))
			{
				if (embeddedSkinAttribute.IsCommonCss && !flag2)
				{
					list.Insert(0, embeddedSkinAttribute);
					flag2 = true;
				}
				else if (embeddedSkinAttribute.Skin == runtimeSkin && !flag)
				{
					list.Add(embeddedSkinAttribute);
					flag = true;
				}
				if (flag && flag2)
				{
					break;
				}
			}
			if (!flag && control.EnableEmbeddedSkins && !string.IsNullOrEmpty(runtimeSkin))
			{
				throw new InvalidOperationException(string.Format("{0} with ID='{1}' was unable to find an embedded skin with the name '{2}'. Please, make sure that the skin name is spelled correctly and that you have added a reference to the Telerik.Web.UI.Skins.dll assembly in your project. If you want to use a custom skin, set EnableEmbeddedSkins=false.", controlType.FullName, ((Control)control).ID, runtimeSkin));
			}
			return list;
		}

		// Token: 0x060095D4 RID: 38356 RVA: 0x00217848 File Offset: 0x00215A48
		internal static Dictionary<string, EmbeddedSkinAttribute> GetAllEmbeddedSkins(RadSkinManager skinManager)
		{
			if (SkinRegistrar.EmbeddedSkinsCache.Count == 0)
			{
				Type typeFromHandle = typeof(RadEditor);
				List<EmbeddedSkinAttribute> attributes = new List<EmbeddedSkinAttribute>((EmbeddedSkinAttribute[])typeFromHandle.GetCustomAttributes(typeof(EmbeddedSkinAttribute), true));
				SkinRegistrar.AddSkinNames(SkinRegistrar.EmbeddedSkinsCache, attributes);
				string defaultSkinsAssemblyName = SkinRegistrar.GetDefaultSkinsAssemblyName(null);
				List<string> list = new List<string>();
				if (!string.IsNullOrEmpty(defaultSkinsAssemblyName))
				{
					list.Add(defaultSkinsAssemblyName);
				}
				SkinRegistrar.FillCustomSkinAssemblies(list, skinManager);
				for (int i = 0; i < list.Count; i++)
				{
					Assembly assembly = Assembly.Load(list[i]);
					Type[] exportedTypes = assembly.GetExportedTypes();
					foreach (Type type in exportedTypes)
					{
						if (TargetControl.typePredicates.Keys.Contains(type.Name))
						{
							List<EmbeddedSkinAttribute> list2 = new List<EmbeddedSkinAttribute>((EmbeddedSkinAttribute[])type.GetCustomAttributes(typeof(EmbeddedSkinAttribute), true));
							if (list2 != null)
							{
								SkinRegistrar.AddSkinNames(SkinRegistrar.EmbeddedSkinsCache, list2);
							}
						}
					}
				}
			}
			return SkinRegistrar.EmbeddedSkinsCache;
		}

		// Token: 0x060095D5 RID: 38357 RVA: 0x00217958 File Offset: 0x00215B58
		internal static IList<EmbeddedSkinAttribute> GetAllEmbeddedSkinAttributes(Type controlType, Page page)
		{
			return SkinRegistrar.GetAllEmbeddedSkinAttributes(controlType, page, false);
		}

		// Token: 0x060095D6 RID: 38358 RVA: 0x00217964 File Offset: 0x00215B64
		internal static IList<EmbeddedSkinAttribute> GetAllEmbeddedSkinAttributes(Type controlType, Page page, bool designTime)
		{
			RadSkinManager radSkinManager = null;
			if (page != null)
			{
				radSkinManager = RadSkinManager.GetCurrent(page);
				if (radSkinManager == null)
				{
					if (designTime)
					{
						radSkinManager = SkinRegistrar.GetSkinManager(page);
					}
					if (radSkinManager != null)
					{
						page.Items[typeof(RadSkinManager)] = radSkinManager;
					}
				}
			}
			return SkinRegistrar.GetAllEmbeddedSkinAttributes(radSkinManager, controlType, page);
		}

		// Token: 0x060095D7 RID: 38359 RVA: 0x002179AC File Offset: 0x00215BAC
		internal static RadSkinManager GetSkinManager(Control control)
		{
			RadSkinManager result = null;
			foreach (object obj in control.Controls)
			{
				Control control2 = (Control)obj;
				if (control2.GetType() == typeof(RadSkinManager))
				{
					result = (RadSkinManager)control2;
					break;
				}
				result = SkinRegistrar.GetSkinManager(control2);
			}
			return result;
		}

		// Token: 0x060095D8 RID: 38360 RVA: 0x00217A28 File Offset: 0x00215C28
		internal static IList<EmbeddedSkinAttribute> GetAllEmbeddedSkinAttributes(Type controlType)
		{
			return SkinRegistrar.GetAllEmbeddedSkinAttributes(null, controlType, null);
		}

		// Token: 0x060095D9 RID: 38361 RVA: 0x00217A34 File Offset: 0x00215C34
		internal static IList<EmbeddedSkinAttribute> GetAllEmbeddedSkinAttributes(RadSkinManager skinManager, Type controlType, Page page = null)
		{
			string assemblyQualifiedName = controlType.AssemblyQualifiedName;
			if (SkinRegistrar.EmbeddedSkinAttributesCache.ContainsKey(assemblyQualifiedName) && (SkinRegistrar.EmbeddedSkinAttributesCache[assemblyQualifiedName].AllSkinsRegistered || skinManager == null))
			{
				return SkinRegistrar.EmbeddedSkinAttributesCache[assemblyQualifiedName].SkinAttributes;
			}
			List<EmbeddedSkinAttribute> list = new List<EmbeddedSkinAttribute>((EmbeddedSkinAttribute[])controlType.GetCustomAttributes(typeof(EmbeddedSkinAttribute), true));
			string defaultSkinsAssemblyName = SkinRegistrar.GetDefaultSkinsAssemblyName(page);
			List<string> list2 = new List<string>();
			if (!string.IsNullOrEmpty(defaultSkinsAssemblyName))
			{
				list2.Add(defaultSkinsAssemblyName);
			}
			SkinRegistrar.FillCustomSkinAssemblies(list2, skinManager);
			foreach (string assemblyString in list2)
			{
				List<EmbeddedSkinAttribute> list3 = null;
				Assembly assembly = Assembly.Load(assemblyString);
				Type[] exportedTypes = assembly.GetExportedTypes();
				if (exportedTypes != null)
				{
					List<Type> allBaseTypes = SkinRegistrar.GetAllBaseTypes(controlType);
					int num = 0;
					while (num < allBaseTypes.Count && list3 == null)
					{
						for (int i = 0; i < exportedTypes.Length; i++)
						{
							if (allBaseTypes[num].Name == exportedTypes[i].Name)
							{
								list3 = new List<EmbeddedSkinAttribute>((EmbeddedSkinAttribute[])exportedTypes[i].GetCustomAttributes(typeof(EmbeddedSkinAttribute), true));
								break;
							}
						}
						num++;
					}
				}
				if (list3 != null)
				{
					foreach (EmbeddedSkinAttribute embeddedSkinAttribute in list)
					{
						int num2 = 0;
						while (num2 < list3.Count && (!list3[num2].IsCommonCss || !embeddedSkinAttribute.IsCommonCss) && !(list3[num2].Skin == embeddedSkinAttribute.Skin))
						{
							num2++;
						}
						if (num2 == list3.Count)
						{
							list3.Add(embeddedSkinAttribute);
						}
					}
					list = list3;
				}
			}
			SkinRegistrar.EmbeddedSkinAttributesCache[assemblyQualifiedName] = new MultiSkinAttributeCollection
			{
				SkinAttributes = list,
				AllSkinsRegistered = (skinManager != null)
			};
			return list;
		}

		// Token: 0x060095DA RID: 38362 RVA: 0x00217C74 File Offset: 0x00215E74
		internal static List<EmbeddedSkinAttribute> MergeSkinAttributes(List<EmbeddedSkinAttribute> source, List<EmbeddedSkinAttribute> toMerge)
		{
			if (toMerge != null && source != null)
			{
				foreach (EmbeddedSkinAttribute embeddedSkinAttribute in source)
				{
					int num = 0;
					while (num < toMerge.Count && (!toMerge[num].IsCommonCss || !embeddedSkinAttribute.IsCommonCss) && !(toMerge[num].Skin == embeddedSkinAttribute.Skin))
					{
						num++;
					}
					if (num == toMerge.Count)
					{
						toMerge.Add(embeddedSkinAttribute);
					}
				}
			}
			return toMerge;
		}

		// Token: 0x060095DB RID: 38363 RVA: 0x00217D14 File Offset: 0x00215F14
		internal static string GetSkinFromResourceName(string resourceName)
		{
			string value = "Telerik.Web.UI.Skins";
			if (!string.IsNullOrEmpty(resourceName) && resourceName.StartsWith(value))
			{
				string[] array = resourceName.Split(new char[]
				{
					'.'
				});
				if (array.Length > 4)
				{
					return array[4].Trim();
				}
			}
			return string.Empty;
		}

		// Token: 0x060095DC RID: 38364 RVA: 0x00217D60 File Offset: 0x00215F60
		internal static Type GetWebResourceType(Type controlType, string runtimeSkin, Page page)
		{
			IList<EmbeddedSkinAttribute> allEmbeddedSkinAttributes = SkinRegistrar.GetAllEmbeddedSkinAttributes(controlType, page);
			foreach (EmbeddedSkinAttribute embeddedSkinAttribute in allEmbeddedSkinAttributes)
			{
				if (embeddedSkinAttribute.Skin == runtimeSkin)
				{
					return embeddedSkinAttribute.Type;
				}
			}
			return controlType;
		}

		// Token: 0x060095DD RID: 38365 RVA: 0x00217DC4 File Offset: 0x00215FC4
		internal static void SuppressRegistration()
		{
			SkinRegistrar.suppressRegistration = true;
		}

		// Token: 0x060095DE RID: 38366 RVA: 0x00217DCC File Offset: 0x00215FCC
		internal static void EnableRegistration()
		{
			SkinRegistrar.suppressRegistration = false;
		}

		// Token: 0x060095DF RID: 38367 RVA: 0x00217E38 File Offset: 0x00216038
		private static string PerformSubstitution(ISkinnableControl control, EmbeddedSkinAttribute attribute)
		{
			string input;
			using (StreamReader streamReader = new StreamReader(attribute.Type.Assembly.GetManifestResourceStream(attribute.CssResourceName)))
			{
				input = streamReader.ReadToEnd();
			}
			return Regex.Replace(input, "<%\\s*=\\s*WebResource\\(\"(?<resourceName>[^\"]*)\"\\)\\s*%>", delegate(Match match)
			{
				if (control.Page != null)
				{
					return ((Control)control).Page.ClientScript.GetWebResourceUrl(attribute.Type, match.Groups["resourceName"].Value);
				}
				return string.Empty;
			});
		}

		// Token: 0x060095E0 RID: 38368 RVA: 0x00217EBC File Offset: 0x002160BC
		private static void RegisterCustomSkinCssReferences(ISkinnableControl control, bool loadOnAjaxRequest)
		{
			RadSkinManager radSkinManager = null;
			StringBuilder stringBuilder = new StringBuilder();
			if (control != null && control.Page != null)
			{
				radSkinManager = RadSkinManager.GetCurrent(control.Page);
			}
			if (SkinRegistrar.suppressRegistration || radSkinManager == null)
			{
				return;
			}
			Control control2 = (Control)control;
			Page page = control2.Page;
			ScriptManager scriptManager = SkinRegistrar.GetScriptManager(control);
			ClientScriptManager clientScript = page.ClientScript;
			string shortControlName = BaseClass.GetShortControlName(control2);
			string runtimeSkin = SkinRegistrar.GetRuntimeSkin(control);
			string resourceName = new StringBuilder().Append(shortControlName).Append(".").Append(runtimeSkin).Append(".css").ToString();
			CustomNonEmbeddedSkin skinByResourceName = radSkinManager.CustomNonEmbeddedSkins.GetSkinByResourceName(resourceName);
			if (skinByResourceName != null && !skinByResourceName.Registered)
			{
				if (scriptManager.IsInAsyncPostBack || !control.RegisterWithScriptManager)
				{
					stringBuilder.Append(string.Format("<link class='Telerik_stylesheet' type='text/css' rel='stylesheet' href='{0}{1}' />", page.ResolveUrl(skinByResourceName.Url), string.Empty));
				}
				else
				{
					SkinRegistrar.RegisterCssReference(page, control2, skinByResourceName.Url);
				}
				skinByResourceName.Registered = true;
			}
			if (loadOnAjaxRequest)
			{
				control.AjaxCssRegistrations += stringBuilder.ToString();
				if (scriptManager.IsInAsyncPostBack && !clientScript.IsStartupScriptRegistered(page.GetType(), "CSS"))
				{
					ScriptManager.RegisterStartupScript(page, page.GetType(), "CSS", "if(typeof ($telerik)!='undefined'){$telerik.registerSkins();};", true);
				}
			}
		}

		// Token: 0x060095E1 RID: 38369 RVA: 0x00218008 File Offset: 0x00216208
		private static void ResolveStyleSheetReference(StyleSheetReference styleSheet, Page page, Type type, string cssResourceName)
		{
			if (HttpContext.Current != null)
			{
				RadStyleSheetManager current = RadStyleSheetManager.GetCurrent(page);
				if (current != null && current.CdnSettings.TelerikCdnResolved == TelerikCdnMode.Enabled)
				{
					current.TelerikCdn.ResolveStyleSheetReference(styleSheet);
				}
			}
			if (string.IsNullOrEmpty(styleSheet.Path))
			{
				ClientScriptManager clientScript = page.ClientScript;
				styleSheet.Path = clientScript.GetWebResourceUrl(type, cssResourceName);
			}
		}

		// Token: 0x060095E2 RID: 38370 RVA: 0x00218064 File Offset: 0x00216264
		private static void RegisterCssReference(Page page, Type registerType, string url, int index)
		{
			if (page.Header == null)
			{
				url = ((HttpContext.Current != null) ? HttpContext.Current.Server.HtmlEncode(url) : (url = url.Replace("&amp;", "&").Replace("&", "&amp;")));
				page.ClientScript.RegisterClientScriptBlock(registerType, url, string.Format("<link class='Telerik_stylesheet' type='text/css' rel='stylesheet' href='{0}{1}' />", url, string.Empty), false);
				return;
			}
			HtmlLink htmlLink = new HtmlLink
			{
				Href = url
			};
			htmlLink.Attributes.Add("type", "text/css");
			htmlLink.Attributes.Add("rel", "stylesheet");
			htmlLink.Attributes.Add("class", "Telerik_stylesheet");
			try
			{
				page.Header.Controls.AddAt(index, htmlLink);
			}
			catch (HttpException innerException)
			{
				throw new HttpException("Please, see whether wrapping the code block, generating the exception, within RadCodeBlock resolves the error.", innerException);
			}
		}

		// Token: 0x060095E3 RID: 38371 RVA: 0x00218154 File Offset: 0x00216354
		private static void RegisterCssReference(Page page, Type registerType, string url)
		{
			SkinRegistrar.RegisterCssReference(page, registerType, url, -1);
		}

		// Token: 0x060095E4 RID: 38372 RVA: 0x00218160 File Offset: 0x00216360
		private static void AddSkinNames(Dictionary<string, EmbeddedSkinAttribute> skinNames, IList<EmbeddedSkinAttribute> attributes)
		{
			foreach (EmbeddedSkinAttribute embeddedSkinAttribute in attributes)
			{
				if (!embeddedSkinAttribute.IsCommonCss && !skinNames.ContainsKey(embeddedSkinAttribute.Skin))
				{
					skinNames.Add(embeddedSkinAttribute.Skin, embeddedSkinAttribute);
				}
			}
		}

		// Token: 0x060095E5 RID: 38373 RVA: 0x002181C4 File Offset: 0x002163C4
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		private static string GetAppSetting(string name, Page page)
		{
			if (page != null)
			{
				IWebApplication webApplication = (IWebApplication)page.Site.GetService(typeof(IWebApplication));
				if (webApplication != null)
				{
					Configuration configuration = webApplication.OpenWebConfiguration(true);
					if (configuration != null)
					{
						return configuration.AppSettings.Settings[name].Value;
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x060095E6 RID: 38374 RVA: 0x0021821C File Offset: 0x0021641C
		private static string GetDefaultSkinsAssemblyName(Page page = null)
		{
			string text = ConfigurationManager.AppSettings["Telerik.Web.SkinsAssembly"];
			if (string.IsNullOrEmpty(text))
			{
				try
				{
					text = SkinRegistrar.GetAppSetting("Telerik.Web.SkinsAssembly", page);
				}
				catch (Exception)
				{
				}
				if (string.IsNullOrEmpty(text))
				{
					text = ", Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4".Replace(", Telerik.Web.Design", "Telerik.Web.UI.Skins");
				}
				string typeName = "Telerik.Web.UI.Skins.Common, " + text;
				Type left;
				try
				{
					left = Type.GetType(typeName);
				}
				catch (FileLoadException)
				{
					left = null;
				}
				catch (FileNotFoundException)
				{
					left = null;
				}
				catch (InvalidOperationException)
				{
					left = null;
				}
				lock (SkinRegistrar.thisLock)
				{
					if (left != null)
					{
						if (ConfigurationManager.AppSettings.Get("Telerik.Web.SkinsAssembly") == null)
						{
							ConfigurationManager.AppSettings["Telerik.Web.SkinsAssembly"] = text;
						}
					}
					else
					{
						ConfigurationManager.AppSettings["Telerik.Web.SkinsAssembly"] = "DISABLED";
						text = string.Empty;
					}
					return text;
				}
			}
			if (text == "DISABLED")
			{
				return string.Empty;
			}
			return text;
		}

		// Token: 0x060095E7 RID: 38375 RVA: 0x00218350 File Offset: 0x00216550
		private static List<Type> GetAllBaseTypes(Type controlType)
		{
			List<Type> list = new List<Type>();
			Type baseType = controlType.BaseType;
			string fullName = typeof(RadWebControl).Assembly.FullName;
			while (baseType != null && baseType.Assembly.FullName != fullName)
			{
				baseType = baseType.BaseType;
			}
			list.Add(controlType);
			while (baseType != null && baseType.Assembly.FullName == fullName)
			{
				list.Add(baseType);
				baseType = baseType.BaseType;
			}
			return list;
		}

		// Token: 0x060095E8 RID: 38376 RVA: 0x002183D8 File Offset: 0x002165D8
		private static void FillCustomSkinAssemblies(List<string> assemblies, RadSkinManager skinManager)
		{
			if (skinManager != null)
			{
				foreach (object obj in skinManager.Skins)
				{
					SkinReference skinReference = (SkinReference)obj;
					if (!string.IsNullOrEmpty(skinReference.Assembly))
					{
						assemblies.Add(skinReference.Assembly);
					}
				}
			}
		}

		// Token: 0x04002AD2 RID: 10962
		private const string CssLinkFormat = "<link class='Telerik_stylesheet' type='text/css' rel='stylesheet' href='{0}{1}' />";

		// Token: 0x04002AD3 RID: 10963
		private const string CopyCssScript = "if(typeof ($telerik)!='undefined'){$telerik.registerSkins();};";

		// Token: 0x04002AD4 RID: 10964
		private const string SkinAttributeCacheKey = "Telerik.EmbeddedSkinAttributeCache";

		// Token: 0x04002AD5 RID: 10965
		private const string SkinsCacheKey = "Telerik.EmbeddedSkinsCache";

		// Token: 0x04002AD6 RID: 10966
		private static readonly object thisLock = new object();

		// Token: 0x04002AD7 RID: 10967
		private static bool suppressRegistration;
	}
}
