using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Web.ApplicationServices;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace System.Web.Profile
{
	// Token: 0x02000007 RID: 7
	[ScriptService]
	internal sealed class ProfileService
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002468 File Offset: 0x00000668
		private static JavaScriptSerializer JavaScriptSerializer
		{
			get
			{
				if (ProfileService._javaScriptSerializer == null)
				{
					HttpContext httpContext = HttpContext.Current;
					WebServiceData webServiceData = WebServiceData.GetWebServiceData(httpContext, httpContext.Request.FilePath);
					ProfileService._javaScriptSerializer = webServiceData.Serializer;
				}
				return ProfileService._javaScriptSerializer;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000024A4 File Offset: 0x000006A4
		private static Dictionary<string, object> GetProfile(HttpContext context, IEnumerable<string> properties)
		{
			ProfileBase profile = context.Profile;
			if (profile == null)
			{
				return null;
			}
			Dictionary<string, object> profileAllowedGet = ApplicationServiceHelper.ProfileAllowedGet;
			if (profileAllowedGet == null || profileAllowedGet.Count == 0)
			{
				return new Dictionary<string, object>(0);
			}
			Dictionary<string, object> dictionary = null;
			if (properties == null)
			{
				dictionary = new Dictionary<string, object>(profileAllowedGet.Count, StringComparer.OrdinalIgnoreCase);
				using (Dictionary<string, object>.Enumerator enumerator = profileAllowedGet.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, object> keyValuePair = enumerator.Current;
						string key = keyValuePair.Key;
						dictionary.Add(key, profile[key]);
					}
					return dictionary;
				}
			}
			dictionary = new Dictionary<string, object>(profileAllowedGet.Count, StringComparer.OrdinalIgnoreCase);
			foreach (string text in properties)
			{
				if (profileAllowedGet.ContainsKey(text))
				{
					dictionary.Add(text, profile[text]);
				}
			}
			return dictionary;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000025A0 File Offset: 0x000007A0
		private static Collection<string> SetProfile(HttpContext context, IDictionary<string, object> values)
		{
			Collection<string> collection = new Collection<string>();
			if (values == null || values.Count == 0)
			{
				return collection;
			}
			ProfileBase profile = context.Profile;
			Dictionary<string, object> profileAllowedSet = ApplicationServiceHelper.ProfileAllowedSet;
			bool flag = false;
			foreach (KeyValuePair<string, object> keyValuePair in values)
			{
				string key = keyValuePair.Key;
				if (profile != null && profileAllowedSet != null && profileAllowedSet.ContainsKey(key))
				{
					SettingsProperty settingsProperty = ProfileBase.Properties[key];
					if (settingsProperty != null && !settingsProperty.IsReadOnly && (!profile.IsAnonymous || (bool)settingsProperty.Attributes["AllowAnonymous"]))
					{
						Type propertyType = settingsProperty.PropertyType;
						object value;
						if (ObjectConverter.TryConvertObjectToType(keyValuePair.Value, propertyType, ProfileService.JavaScriptSerializer, out value))
						{
							profile[key] = value;
							flag = true;
							continue;
						}
					}
				}
				collection.Add(key);
			}
			if (flag)
			{
				profile.Save();
			}
			return collection;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000026A4 File Offset: 0x000008A4
		[WebMethod]
		public Dictionary<string, object> GetAllPropertiesForCurrentUser(bool authenticatedUserOnly)
		{
			ApplicationServiceHelper.EnsureProfileServiceEnabled();
			HttpContext context = HttpContext.Current;
			if (authenticatedUserOnly)
			{
				ApplicationServiceHelper.EnsureAuthenticated(context);
			}
			return ProfileService.GetProfile(context, null);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000026CC File Offset: 0x000008CC
		[WebMethod]
		public Dictionary<string, object> GetPropertiesForCurrentUser(IEnumerable<string> properties, bool authenticatedUserOnly)
		{
			ApplicationServiceHelper.EnsureProfileServiceEnabled();
			HttpContext context = HttpContext.Current;
			if (authenticatedUserOnly)
			{
				ApplicationServiceHelper.EnsureAuthenticated(context);
			}
			return ProfileService.GetProfile(context, properties);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000026F4 File Offset: 0x000008F4
		[WebMethod]
		public Collection<ProfilePropertyMetadata> GetPropertiesMetadata()
		{
			ApplicationServiceHelper.EnsureProfileServiceEnabled();
			return ApplicationServiceHelper.GetProfilePropertiesMetadata();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002700 File Offset: 0x00000900
		[WebMethod]
		public Collection<string> SetPropertiesForCurrentUser(IDictionary<string, object> values, bool authenticatedUserOnly)
		{
			ApplicationServiceHelper.EnsureProfileServiceEnabled();
			HttpContext context = HttpContext.Current;
			if (authenticatedUserOnly)
			{
				ApplicationServiceHelper.EnsureAuthenticated(context);
			}
			return ProfileService.SetProfile(context, values);
		}

		// Token: 0x0400000C RID: 12
		private static JavaScriptSerializer _javaScriptSerializer;
	}
}
