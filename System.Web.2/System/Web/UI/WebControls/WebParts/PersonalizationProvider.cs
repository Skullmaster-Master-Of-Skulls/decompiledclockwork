using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Security.Principal;
using System.Web.Configuration;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200055A RID: 1370
	public abstract class PersonalizationProvider : ProviderBase
	{
		// Token: 0x1700148D RID: 5261
		// (get) Token: 0x060045AE RID: 17838
		// (set) Token: 0x060045AF RID: 17839
		public abstract string ApplicationName { get; set; }

		// Token: 0x060045B0 RID: 17840 RVA: 0x000E5AB0 File Offset: 0x000E3CB0
		protected virtual IList CreateSupportedUserCapabilities()
		{
			return new ArrayList
			{
				WebPartPersonalization.EnterSharedScopeUserCapability,
				WebPartPersonalization.ModifyStateUserCapability
			};
		}

		// Token: 0x060045B1 RID: 17841 RVA: 0x000E5ADC File Offset: 0x000E3CDC
		public virtual PersonalizationScope DetermineInitialScope(WebPartManager webPartManager, PersonalizationState loadedState)
		{
			if (webPartManager == null)
			{
				throw new ArgumentNullException("webPartManager");
			}
			Page page = webPartManager.Page;
			if (page == null)
			{
				throw new ArgumentException(SR.GetString("PropertyCannotBeNull", new object[]
				{
					"Page"
				}), "webPartManager");
			}
			HttpRequest requestInternal = page.RequestInternal;
			if (requestInternal == null)
			{
				throw new ArgumentException(SR.GetString("PropertyCannotBeNull", new object[]
				{
					"Page.Request"
				}), "webPartManager");
			}
			PersonalizationScope personalizationScope = webPartManager.Personalization.InitialScope;
			IPrincipal principal = null;
			if (requestInternal.IsAuthenticated)
			{
				principal = page.User;
			}
			if (principal == null)
			{
				personalizationScope = PersonalizationScope.Shared;
			}
			else
			{
				if (page.IsPostBack)
				{
					string a = page.Request["__WPPS"];
					if (a == "s")
					{
						personalizationScope = PersonalizationScope.Shared;
					}
					else if (a == "u")
					{
						personalizationScope = PersonalizationScope.User;
					}
				}
				else if (page.PreviousPage != null && !page.PreviousPage.IsCrossPagePostBack)
				{
					WebPartManager currentWebPartManager = WebPartManager.GetCurrentWebPartManager(page.PreviousPage);
					if (currentWebPartManager != null)
					{
						personalizationScope = currentWebPartManager.Personalization.Scope;
					}
				}
				else if (page.IsExportingWebPart)
				{
					personalizationScope = (page.IsExportingWebPartShared ? PersonalizationScope.Shared : PersonalizationScope.User);
				}
				if (personalizationScope == PersonalizationScope.Shared && !webPartManager.Personalization.CanEnterSharedScope)
				{
					personalizationScope = PersonalizationScope.User;
				}
			}
			string hiddenFieldInitialValue = (personalizationScope == PersonalizationScope.Shared) ? "s" : "u";
			page.ClientScript.RegisterHiddenField("__WPPS", hiddenFieldInitialValue);
			return personalizationScope;
		}

		// Token: 0x060045B2 RID: 17842 RVA: 0x000E5C38 File Offset: 0x000E3E38
		public virtual IDictionary DetermineUserCapabilities(WebPartManager webPartManager)
		{
			if (webPartManager == null)
			{
				throw new ArgumentNullException("webPartManager");
			}
			Page page = webPartManager.Page;
			if (page == null)
			{
				throw new ArgumentException(SR.GetString("PropertyCannotBeNull", new object[]
				{
					"Page"
				}), "webPartManager");
			}
			HttpRequest requestInternal = page.RequestInternal;
			if (requestInternal == null)
			{
				throw new ArgumentException(SR.GetString("PropertyCannotBeNull", new object[]
				{
					"Page.Request"
				}), "webPartManager");
			}
			IPrincipal principal = null;
			if (requestInternal.IsAuthenticated)
			{
				principal = page.User;
			}
			if (principal != null)
			{
				if (this._supportedUserCapabilities == null)
				{
					this._supportedUserCapabilities = this.CreateSupportedUserCapabilities();
				}
				if (this._supportedUserCapabilities != null && this._supportedUserCapabilities.Count != 0)
				{
					WebPartsSection webParts = RuntimeConfig.GetConfig().WebParts;
					if (webParts != null)
					{
						WebPartsPersonalizationAuthorization authorization = webParts.Personalization.Authorization;
						if (authorization != null)
						{
							IDictionary dictionary = new HybridDictionary();
							foreach (object obj in this._supportedUserCapabilities)
							{
								WebPartUserCapability webPartUserCapability = (WebPartUserCapability)obj;
								if (authorization.IsUserAllowed(principal, webPartUserCapability.Name))
								{
									dictionary[webPartUserCapability] = webPartUserCapability;
								}
							}
							return dictionary;
						}
					}
				}
			}
			return new HybridDictionary();
		}

		// Token: 0x060045B3 RID: 17843
		public abstract PersonalizationStateInfoCollection FindState(PersonalizationScope scope, PersonalizationStateQuery query, int pageIndex, int pageSize, out int totalRecords);

		// Token: 0x060045B4 RID: 17844
		public abstract int GetCountOfState(PersonalizationScope scope, PersonalizationStateQuery query);

		// Token: 0x060045B5 RID: 17845 RVA: 0x000E5D8C File Offset: 0x000E3F8C
		private void GetParameters(WebPartManager webPartManager, out string path, out string userName)
		{
			if (webPartManager == null)
			{
				throw new ArgumentNullException("webPartManager");
			}
			Page page = webPartManager.Page;
			if (page == null)
			{
				throw new ArgumentException(SR.GetString("PropertyCannotBeNull", new object[]
				{
					"Page"
				}), "webPartManager");
			}
			HttpRequest requestInternal = page.RequestInternal;
			if (requestInternal == null)
			{
				throw new ArgumentException(SR.GetString("PropertyCannotBeNull", new object[]
				{
					"Page.Request"
				}), "webPartManager");
			}
			path = requestInternal.AppRelativeCurrentExecutionFilePath;
			userName = null;
			if (webPartManager.Personalization.Scope == PersonalizationScope.User && page.Request.IsAuthenticated)
			{
				userName = page.User.Identity.Name;
			}
		}

		// Token: 0x060045B6 RID: 17846
		protected abstract void LoadPersonalizationBlobs(WebPartManager webPartManager, string path, string userName, ref byte[] sharedDataBlob, ref byte[] userDataBlob);

		// Token: 0x060045B7 RID: 17847 RVA: 0x000E5E38 File Offset: 0x000E4038
		public virtual PersonalizationState LoadPersonalizationState(WebPartManager webPartManager, bool ignoreCurrentUser)
		{
			if (webPartManager == null)
			{
				throw new ArgumentNullException("webPartManager");
			}
			string path;
			string userName;
			this.GetParameters(webPartManager, out path, out userName);
			if (ignoreCurrentUser)
			{
				userName = null;
			}
			byte[] sharedData = null;
			byte[] userData = null;
			this.LoadPersonalizationBlobs(webPartManager, path, userName, ref sharedData, ref userData);
			BlobPersonalizationState blobPersonalizationState = new BlobPersonalizationState(webPartManager);
			blobPersonalizationState.LoadDataBlobs(sharedData, userData);
			return blobPersonalizationState;
		}

		// Token: 0x060045B8 RID: 17848
		protected abstract void ResetPersonalizationBlob(WebPartManager webPartManager, string path, string userName);

		// Token: 0x060045B9 RID: 17849 RVA: 0x000E5E88 File Offset: 0x000E4088
		public virtual void ResetPersonalizationState(WebPartManager webPartManager)
		{
			if (webPartManager == null)
			{
				throw new ArgumentNullException("webPartManager");
			}
			string path;
			string userName;
			this.GetParameters(webPartManager, out path, out userName);
			this.ResetPersonalizationBlob(webPartManager, path, userName);
		}

		// Token: 0x060045BA RID: 17850
		public abstract int ResetState(PersonalizationScope scope, string[] paths, string[] usernames);

		// Token: 0x060045BB RID: 17851
		public abstract int ResetUserState(string path, DateTime userInactiveSinceDate);

		// Token: 0x060045BC RID: 17852
		protected abstract void SavePersonalizationBlob(WebPartManager webPartManager, string path, string userName, byte[] dataBlob);

		// Token: 0x060045BD RID: 17853 RVA: 0x000E5EB8 File Offset: 0x000E40B8
		public virtual void SavePersonalizationState(PersonalizationState state)
		{
			if (state == null)
			{
				throw new ArgumentNullException("state");
			}
			BlobPersonalizationState blobPersonalizationState = state as BlobPersonalizationState;
			if (blobPersonalizationState == null)
			{
				throw new ArgumentException(SR.GetString("PersonalizationProvider_WrongType"), "state");
			}
			WebPartManager webPartManager = blobPersonalizationState.WebPartManager;
			string path;
			string userName;
			this.GetParameters(webPartManager, out path, out userName);
			byte[] array = null;
			bool flag = blobPersonalizationState.IsEmpty;
			if (!flag)
			{
				array = blobPersonalizationState.SaveDataBlob();
				flag = (array == null || array.Length == 0);
			}
			if (flag)
			{
				this.ResetPersonalizationBlob(webPartManager, path, userName);
				return;
			}
			this.SavePersonalizationBlob(webPartManager, path, userName, array);
		}

		// Token: 0x04002677 RID: 9847
		private const string scopeFieldName = "__WPPS";

		// Token: 0x04002678 RID: 9848
		private const string sharedScopeFieldValue = "s";

		// Token: 0x04002679 RID: 9849
		private const string userScopeFieldValue = "u";

		// Token: 0x0400267A RID: 9850
		private ICollection _supportedUserCapabilities;
	}
}
