using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Web.ApplicationServices;
using System.Web.Profile;
using System.Web.Resources;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace System.Web.UI
{
	// Token: 0x02000061 RID: 97
	[DefaultProperty("Path")]
	[TypeConverter(typeof(EmptyStringExpandableObjectConverter))]
	public class ProfileServiceManager
	{
		// Token: 0x06000393 RID: 915 RVA: 0x000135C4 File Offset: 0x000117C4
		internal static void ConfigureProfileService(ref StringBuilder sb, HttpContext context, ScriptManager scriptManager, List<ScriptManagerProxy> proxies)
		{
			string text = null;
			ArrayList arrayList = null;
			if (scriptManager.HasProfileServiceManager)
			{
				ProfileServiceManager profileService = scriptManager.ProfileService;
				text = profileService.Path.Trim();
				if (text.Length > 0)
				{
					text = scriptManager.ResolveClientUrl(text);
				}
				if (profileService.HasLoadProperties)
				{
					arrayList = new ArrayList(profileService._loadProperties);
				}
			}
			if (proxies != null)
			{
				foreach (ScriptManagerProxy scriptManagerProxy in proxies)
				{
					if (scriptManagerProxy.HasProfileServiceManager)
					{
						ProfileServiceManager profileService = scriptManagerProxy.ProfileService;
						text = ApplicationServiceManager.MergeServiceUrls(profileService.Path, text, scriptManagerProxy);
						if (profileService.HasLoadProperties)
						{
							if (arrayList == null)
							{
								arrayList = new ArrayList(profileService._loadProperties);
							}
							else
							{
								arrayList = ProfileServiceManager.MergeProperties(arrayList, profileService._loadProperties);
							}
						}
					}
				}
			}
			ProfileServiceManager.GenerateInitializationScript(ref sb, context, scriptManager, text, arrayList);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x000136A4 File Offset: 0x000118A4
		private static void GenerateInitializationScript(ref StringBuilder sb, HttpContext context, ScriptManager scriptManager, string serviceUrl, ArrayList loadedProperties)
		{
			string text = null;
			bool flag = loadedProperties != null && loadedProperties.Count > 0;
			if (ApplicationServiceHelper.ProfileServiceEnabled)
			{
				if (sb == null)
				{
					sb = new StringBuilder(128);
				}
				text = scriptManager.ResolveClientUrl("~/Profile_JSON_AppService.axd");
				sb.Append("Sys.Services._ProfileService.DefaultWebServicePath = '");
				sb.Append(HttpUtility.JavaScriptStringEncode(text));
				sb.Append("';\n");
			}
			if (!string.IsNullOrEmpty(serviceUrl))
			{
				if (text == null)
				{
					text = scriptManager.ResolveClientUrl("~/Profile_JSON_AppService.axd");
				}
				if (flag && !string.Equals(serviceUrl, text, StringComparison.OrdinalIgnoreCase))
				{
					throw new InvalidOperationException(AtlasWeb.ProfileServiceManager_LoadProperitesWithNonDefaultPath);
				}
				if (sb == null)
				{
					sb = new StringBuilder(128);
				}
				sb.Append("Sys.Services.ProfileService.set_path('");
				sb.Append(HttpUtility.JavaScriptStringEncode(serviceUrl));
				sb.Append("');\n");
			}
			if (flag)
			{
				if (sb == null)
				{
					sb = new StringBuilder(128);
				}
				if (scriptManager.DesignMode)
				{
					sb.Append("// loadProperties\n");
					return;
				}
				if (context != null)
				{
					SortedList<string, object> topLevelSettings = new SortedList<string, object>(loadedProperties.Count);
					SortedList<string, SortedList<string, object>> profileGroups = null;
					ProfileBase profile = context.Profile;
					foreach (object obj in loadedProperties)
					{
						string fullPropertyName = (string)obj;
						ProfileServiceManager.GetSettingsProperty(profile, fullPropertyName, topLevelSettings, ref profileGroups, true);
					}
					ProfileServiceManager.RenderProfileProperties(sb, topLevelSettings, profileGroups);
				}
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0001381C File Offset: 0x00011A1C
		internal static ArrayList MergeProperties(ArrayList existingProperties, string[] newProperties)
		{
			foreach (string text in newProperties)
			{
				if (!string.IsNullOrEmpty(text))
				{
					string text2 = text.Trim();
					if (text2.Length > 0 && !existingProperties.Contains(text2))
					{
						existingProperties.Add(text2);
					}
				}
			}
			return existingProperties;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00013868 File Offset: 0x00011A68
		internal static void GetSettingsProperty(ProfileBase profile, string fullPropertyName, SortedList<string, object> topLevelSettings, ref SortedList<string, SortedList<string, object>> profileGroups, bool ensureExists)
		{
			int num = fullPropertyName.IndexOf('.');
			string key;
			SortedList<string, object> sortedList;
			if (num == -1)
			{
				key = fullPropertyName;
				sortedList = topLevelSettings;
			}
			else
			{
				string key2 = fullPropertyName.Substring(0, num);
				key = fullPropertyName.Substring(num + 1);
				if (profileGroups == null)
				{
					profileGroups = new SortedList<string, SortedList<string, object>>();
					sortedList = new SortedList<string, object>();
					profileGroups.Add(key2, sortedList);
				}
				else
				{
					sortedList = profileGroups[key2];
					if (sortedList == null)
					{
						sortedList = new SortedList<string, object>();
						profileGroups.Add(key2, sortedList);
					}
				}
			}
			bool flag = ProfileBase.Properties[fullPropertyName] != null;
			if (ensureExists && !flag)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.AppService_UnknownProfileProperty, new object[]
				{
					fullPropertyName
				}));
			}
			if (flag)
			{
				sortedList[key] = ((profile == null) ? null : profile[fullPropertyName]);
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00013924 File Offset: 0x00011B24
		private static void RenderProfileProperties(StringBuilder sb, SortedList<string, object> topLevelSettings, SortedList<string, SortedList<string, object>> profileGroups)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			sb.Append("Sys.Services.ProfileService.properties = ");
			sb.Append(javaScriptSerializer.Serialize(topLevelSettings, JavaScriptSerializer.SerializationFormat.JavaScript));
			sb.Append(";\n");
			if (profileGroups != null)
			{
				foreach (KeyValuePair<string, SortedList<string, object>> keyValuePair in profileGroups)
				{
					sb.Append("Sys.Services.ProfileService.properties.");
					sb.Append(keyValuePair.Key);
					sb.Append(" = new Sys.Services.ProfileGroup(");
					sb.Append(javaScriptSerializer.Serialize(keyValuePair.Value, JavaScriptSerializer.SerializationFormat.JavaScript));
					sb.Append(");\n");
				}
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000398 RID: 920 RVA: 0x000139DC File Offset: 0x00011BDC
		internal bool HasLoadProperties
		{
			get
			{
				return this._loadProperties != null && this._loadProperties.Length != 0;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000399 RID: 921 RVA: 0x000139F2 File Offset: 0x00011BF2
		// (set) Token: 0x0600039A RID: 922 RVA: 0x00013A18 File Offset: 0x00011C18
		[DefaultValue(null)]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(StringArrayConverter))]
		[ResourceDescription("ProfileServiceManager_LoadProperties")]
		public string[] LoadProperties
		{
			get
			{
				if (this._loadProperties == null)
				{
					this._loadProperties = new string[0];
				}
				return (string[])this._loadProperties.Clone();
			}
			set
			{
				if (value != null)
				{
					value = (string[])value.Clone();
				}
				this._loadProperties = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00013A31 File Offset: 0x00011C31
		// (set) Token: 0x0600039C RID: 924 RVA: 0x00013A42 File Offset: 0x00011C42
		[DefaultValue("")]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[ResourceDescription("ApplicationServiceManager_Path")]
		[UrlProperty]
		public string Path
		{
			get
			{
				return this._path ?? string.Empty;
			}
			set
			{
				this._path = value;
			}
		}

		// Token: 0x04000150 RID: 336
		private string[] _loadProperties;

		// Token: 0x04000151 RID: 337
		private string _path;
	}
}
