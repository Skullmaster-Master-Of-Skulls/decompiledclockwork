using System;
using System.Configuration.Provider;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web.Management;
using System.Web.Resources;
using System.Web.Security;

namespace System.Web.ApplicationServices
{
	// Token: 0x02000122 RID: 290
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	[ServiceContract(Namespace = "http://asp.net/ApplicationServices/v200")]
	[ServiceBehavior(Namespace = "http://asp.net/ApplicationServices/v200", InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
	public class RoleService
	{
		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06000F1C RID: 3868 RVA: 0x000368A4 File Offset: 0x00034AA4
		// (remove) Token: 0x06000F1D RID: 3869 RVA: 0x000368F4 File Offset: 0x00034AF4
		public static event EventHandler<SelectingProviderEventArgs> SelectingProvider
		{
			add
			{
				object selectingProviderEventHandlerLock = RoleService._selectingProviderEventHandlerLock;
				lock (selectingProviderEventHandlerLock)
				{
					RoleService._selectingProvider = (EventHandler<SelectingProviderEventArgs>)Delegate.Combine(RoleService._selectingProvider, value);
				}
			}
			remove
			{
				object selectingProviderEventHandlerLock = RoleService._selectingProviderEventHandlerLock;
				lock (selectingProviderEventHandlerLock)
				{
					RoleService._selectingProvider = (EventHandler<SelectingProviderEventArgs>)Delegate.Remove(RoleService._selectingProvider, value);
				}
			}
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x00036944 File Offset: 0x00034B44
		private static void EnsureProviderEnabled()
		{
			if (!Roles.Enabled)
			{
				throw new ProviderException(AtlasWeb.RoleService_RolesFeatureNotEnabled);
			}
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x00036958 File Offset: 0x00034B58
		private RoleProvider GetRoleProvider(IPrincipal user)
		{
			string text = Roles.Provider.Name;
			SelectingProviderEventArgs selectingProviderEventArgs = new SelectingProviderEventArgs(user, text);
			this.OnSelectingProvider(selectingProviderEventArgs);
			text = selectingProviderEventArgs.ProviderName;
			RoleProvider roleProvider = Roles.Providers[text];
			if (roleProvider == null)
			{
				throw new ProviderException(AtlasWeb.RoleService_RoleProviderNotFound);
			}
			return roleProvider;
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x000369A4 File Offset: 0x00034BA4
		[OperationContract]
		public string[] GetRolesForCurrentUser()
		{
			string[] rolesForUser;
			try
			{
				ApplicationServiceHelper.EnsureRoleServiceEnabled();
				RoleService.EnsureProviderEnabled();
				IPrincipal currentUser = ApplicationServiceHelper.GetCurrentUser(HttpContext.Current);
				string userName = ApplicationServiceHelper.GetUserName(currentUser);
				RoleProvider roleProvider = this.GetRoleProvider(currentUser);
				rolesForUser = roleProvider.GetRolesForUser(userName);
			}
			catch (Exception e)
			{
				this.LogException(e);
				throw;
			}
			return rolesForUser;
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x000369FC File Offset: 0x00034BFC
		[OperationContract]
		public bool IsCurrentUserInRole(string role)
		{
			if (role == null)
			{
				throw new ArgumentNullException("role");
			}
			bool result;
			try
			{
				ApplicationServiceHelper.EnsureRoleServiceEnabled();
				RoleService.EnsureProviderEnabled();
				IPrincipal currentUser = ApplicationServiceHelper.GetCurrentUser(HttpContext.Current);
				string userName = ApplicationServiceHelper.GetUserName(currentUser);
				RoleProvider roleProvider = this.GetRoleProvider(currentUser);
				result = roleProvider.IsUserInRole(userName, role);
			}
			catch (Exception e)
			{
				this.LogException(e);
				throw;
			}
			return result;
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x00036A64 File Offset: 0x00034C64
		private void LogException(Exception e)
		{
			WebServiceErrorEvent webServiceErrorEvent = new WebServiceErrorEvent(AtlasWeb.UnhandledExceptionEventLogMessage, this, e);
			webServiceErrorEvent.Raise();
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x00036A84 File Offset: 0x00034C84
		private void OnSelectingProvider(SelectingProviderEventArgs e)
		{
			EventHandler<SelectingProviderEventArgs> selectingProvider = RoleService._selectingProvider;
			if (selectingProvider != null)
			{
				selectingProvider(this, e);
			}
		}

		// Token: 0x04000447 RID: 1095
		private static object _selectingProviderEventHandlerLock = new object();

		// Token: 0x04000448 RID: 1096
		private static EventHandler<SelectingProviderEventArgs> _selectingProvider;
	}
}
