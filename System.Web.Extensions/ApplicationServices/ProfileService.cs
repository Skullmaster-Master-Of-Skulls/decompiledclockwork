using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Configuration.Provider;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web.Management;
using System.Web.Profile;
using System.Web.Resources;

namespace System.Web.ApplicationServices
{
	// Token: 0x02000121 RID: 289
	[ServiceContract(Namespace = "http://asp.net/ApplicationServices/v200")]
	[ServiceKnownType("GetKnownTypes", typeof(KnownTypesProvider))]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	[ServiceBehavior(Namespace = "http://asp.net/ApplicationServices/v200", InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
	public class ProfileService
	{
		// Token: 0x1400004B RID: 75
		// (add) Token: 0x06000F10 RID: 3856 RVA: 0x00036294 File Offset: 0x00034494
		// (remove) Token: 0x06000F11 RID: 3857 RVA: 0x000362E4 File Offset: 0x000344E4
		public static event EventHandler<ValidatingPropertiesEventArgs> ValidatingProperties
		{
			add
			{
				object validatingPropertiesEventHandlerLock = ProfileService._validatingPropertiesEventHandlerLock;
				lock (validatingPropertiesEventHandlerLock)
				{
					ProfileService._validatingProperties = (EventHandler<ValidatingPropertiesEventArgs>)Delegate.Combine(ProfileService._validatingProperties, value);
				}
			}
			remove
			{
				object validatingPropertiesEventHandlerLock = ProfileService._validatingPropertiesEventHandlerLock;
				lock (validatingPropertiesEventHandlerLock)
				{
					ProfileService._validatingProperties = (EventHandler<ValidatingPropertiesEventArgs>)Delegate.Remove(ProfileService._validatingProperties, value);
				}
			}
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x00036334 File Offset: 0x00034534
		private void OnValidatingProperties(ValidatingPropertiesEventArgs e)
		{
			EventHandler<ValidatingPropertiesEventArgs> validatingProperties = ProfileService._validatingProperties;
			if (validatingProperties != null)
			{
				validatingProperties(this, e);
			}
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x00036354 File Offset: 0x00034554
		[OperationContract]
		public Dictionary<string, object> GetPropertiesForCurrentUser(IEnumerable<string> properties, bool authenticatedUserOnly)
		{
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			ApplicationServiceHelper.EnsureProfileServiceEnabled();
			if (authenticatedUserOnly)
			{
				ApplicationServiceHelper.EnsureAuthenticated(HttpContext.Current);
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			ProfileBase profileBase = null;
			try
			{
				profileBase = ProfileService.GetProfileForCurrentUser(authenticatedUserOnly);
			}
			catch (Exception e)
			{
				this.LogException(e);
				throw;
			}
			if (profileBase == null)
			{
				return null;
			}
			Dictionary<string, object> profileAllowedGet = ApplicationServiceHelper.ProfileAllowedGet;
			if (profileAllowedGet == null || profileAllowedGet.Count == 0)
			{
				return dictionary;
			}
			foreach (string text in properties)
			{
				if (text == null)
				{
					throw new ArgumentNullException("properties");
				}
				if (profileAllowedGet.ContainsKey(text))
				{
					try
					{
						SettingsPropertyValue propertyValue = ProfileService.GetPropertyValue(profileBase, text);
						if (propertyValue != null)
						{
							dictionary.Add(text, propertyValue.PropertyValue);
							propertyValue.IsDirty = false;
						}
					}
					catch (Exception e2)
					{
						this.LogException(e2);
						throw;
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x00036454 File Offset: 0x00034654
		[OperationContract]
		public Dictionary<string, object> GetAllPropertiesForCurrentUser(bool authenticatedUserOnly)
		{
			ApplicationServiceHelper.EnsureProfileServiceEnabled();
			if (authenticatedUserOnly)
			{
				ApplicationServiceHelper.EnsureAuthenticated(HttpContext.Current);
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			try
			{
				ProfileBase profileForCurrentUser = ProfileService.GetProfileForCurrentUser(authenticatedUserOnly);
				if (profileForCurrentUser == null)
				{
					return null;
				}
				Dictionary<string, object> profileAllowedGet = ApplicationServiceHelper.ProfileAllowedGet;
				if (profileAllowedGet == null || profileAllowedGet.Count == 0)
				{
					return dictionary;
				}
				foreach (KeyValuePair<string, object> keyValuePair in profileAllowedGet)
				{
					string key = keyValuePair.Key;
					SettingsPropertyValue propertyValue = ProfileService.GetPropertyValue(profileForCurrentUser, key);
					if (propertyValue != null)
					{
						dictionary.Add(key, propertyValue.PropertyValue);
						propertyValue.IsDirty = false;
					}
				}
			}
			catch (Exception e)
			{
				this.LogException(e);
				throw;
			}
			return dictionary;
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x00036524 File Offset: 0x00034724
		[OperationContract]
		public Collection<string> SetPropertiesForCurrentUser(IDictionary<string, object> values, bool authenticatedUserOnly)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			ApplicationServiceHelper.EnsureProfileServiceEnabled();
			if (authenticatedUserOnly)
			{
				ApplicationServiceHelper.EnsureAuthenticated(HttpContext.Current);
			}
			Collection<string> collection = new Collection<string>();
			try
			{
				ValidatingPropertiesEventArgs validatingPropertiesEventArgs = new ValidatingPropertiesEventArgs(values);
				this.OnValidatingProperties(validatingPropertiesEventArgs);
				Dictionary<string, object> profileAllowedSet = ApplicationServiceHelper.ProfileAllowedSet;
				ProfileBase profileForCurrentUser = ProfileService.GetProfileForCurrentUser(authenticatedUserOnly);
				foreach (KeyValuePair<string, object> keyValuePair in values)
				{
					string key = keyValuePair.Key;
					if (profileForCurrentUser == null)
					{
						collection.Add(key);
					}
					else if (validatingPropertiesEventArgs.FailedProperties.Contains(key))
					{
						collection.Add(key);
					}
					else if (profileAllowedSet == null)
					{
						collection.Add(key);
					}
					else if (!profileAllowedSet.ContainsKey(key))
					{
						collection.Add(key);
					}
					else
					{
						SettingsProperty settingsProperty = ProfileBase.Properties[key];
						if (settingsProperty == null)
						{
							collection.Add(key);
						}
						else if (settingsProperty.IsReadOnly || (profileForCurrentUser.IsAnonymous && !(bool)settingsProperty.Attributes["AllowAnonymous"]))
						{
							collection.Add(key);
						}
						else if (ProfileService.GetPropertyValue(profileForCurrentUser, keyValuePair.Key) == null)
						{
							collection.Add(key);
						}
						else
						{
							try
							{
								profileForCurrentUser[key] = keyValuePair.Value;
							}
							catch (ProviderException)
							{
								collection.Add(key);
							}
							catch (SettingsPropertyNotFoundException)
							{
								collection.Add(key);
							}
							catch (SettingsPropertyWrongTypeException)
							{
								collection.Add(key);
							}
						}
					}
				}
				profileForCurrentUser.Save();
			}
			catch (Exception e)
			{
				this.LogException(e);
				throw;
			}
			return collection;
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x00036734 File Offset: 0x00034934
		[OperationContract]
		public ProfilePropertyMetadata[] GetPropertiesMetadata()
		{
			ApplicationServiceHelper.EnsureProfileServiceEnabled();
			ProfilePropertyMetadata[] result;
			try
			{
				Collection<ProfilePropertyMetadata> profilePropertiesMetadata = ApplicationServiceHelper.GetProfilePropertiesMetadata();
				ProfilePropertyMetadata[] array = new ProfilePropertyMetadata[profilePropertiesMetadata.Count];
				profilePropertiesMetadata.CopyTo(array, 0);
				result = array;
			}
			catch (Exception e)
			{
				this.LogException(e);
				throw;
			}
			return result;
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x00036780 File Offset: 0x00034980
		private static SettingsPropertyValue GetPropertyValue(ProfileBase pb, string name)
		{
			if (ProfileBase.Properties[name] == null)
			{
				return null;
			}
			SettingsPropertyValue settingsPropertyValue = pb.PropertyValues[name];
			if (settingsPropertyValue == null)
			{
				pb.GetPropertyValue(name);
				settingsPropertyValue = pb.PropertyValues[name];
				if (settingsPropertyValue == null)
				{
					return null;
				}
			}
			return settingsPropertyValue;
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x000367CC File Offset: 0x000349CC
		private static ProfileBase GetProfileForCurrentUser(bool authenticatedUserOnly)
		{
			HttpContext httpContext = HttpContext.Current;
			IPrincipal currentUser = ApplicationServiceHelper.GetCurrentUser(httpContext);
			string text = null;
			bool flag;
			if (currentUser == null || currentUser.Identity == null || string.IsNullOrEmpty(currentUser.Identity.Name))
			{
				flag = false;
				if (!authenticatedUserOnly && httpContext != null && !string.IsNullOrEmpty(httpContext.Request.AnonymousID))
				{
					text = httpContext.Request.AnonymousID;
				}
			}
			else
			{
				text = currentUser.Identity.Name;
				flag = currentUser.Identity.IsAuthenticated;
			}
			if (flag || (!authenticatedUserOnly && !string.IsNullOrEmpty(text)))
			{
				return ProfileBase.Create(text, flag);
			}
			if (httpContext != null)
			{
				throw new HttpException(AtlasWeb.UserIsNotAuthenticated);
			}
			throw new Exception(AtlasWeb.UserIsNotAuthenticated);
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x00036878 File Offset: 0x00034A78
		private void LogException(Exception e)
		{
			WebServiceErrorEvent webServiceErrorEvent = new WebServiceErrorEvent(AtlasWeb.UnhandledExceptionEventLogMessage, this, e);
			webServiceErrorEvent.Raise();
		}

		// Token: 0x04000445 RID: 1093
		private static object _validatingPropertiesEventHandlerLock = new object();

		// Token: 0x04000446 RID: 1094
		private static EventHandler<ValidatingPropertiesEventArgs> _validatingProperties;
	}
}
