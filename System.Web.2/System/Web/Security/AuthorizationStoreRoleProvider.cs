using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.Security
{
	// Token: 0x020005D2 RID: 1490
	public class AuthorizationStoreRoleProvider : RoleProvider
	{
		// Token: 0x17001634 RID: 5684
		// (get) Token: 0x06004B65 RID: 19301 RVA: 0x000FFBE3 File Offset: 0x000FDDE3
		// (set) Token: 0x06004B66 RID: 19302 RVA: 0x000FFBEB File Offset: 0x000FDDEB
		public override string ApplicationName
		{
			get
			{
				return this._AppName;
			}
			set
			{
				if (this._AppName != value)
				{
					if (value.Length > 256)
					{
						throw new ProviderException(SR.GetString("Provider_application_name_too_long"));
					}
					this._AppName = value;
					this._InitAppDone = false;
				}
			}
		}

		// Token: 0x17001635 RID: 5685
		// (get) Token: 0x06004B67 RID: 19303 RVA: 0x000FFC26 File Offset: 0x000FDE26
		// (set) Token: 0x06004B68 RID: 19304 RVA: 0x000FFC2E File Offset: 0x000FDE2E
		public string ScopeName
		{
			get
			{
				return this._ScopeName;
			}
			set
			{
				if (this._ScopeName != value)
				{
					this._ScopeName = value;
					this._InitAppDone = false;
				}
			}
		}

		// Token: 0x17001636 RID: 5686
		// (get) Token: 0x06004B69 RID: 19305 RVA: 0x000FFC4C File Offset: 0x000FDE4C
		public int CacheRefreshInterval
		{
			get
			{
				return this._CacheRefreshInterval;
			}
		}

		// Token: 0x06004B6A RID: 19306 RVA: 0x000FFC54 File Offset: 0x000FDE54
		public override void Initialize(string name, NameValueCollection config)
		{
			HttpRuntime.CheckAspNetHostingPermission(AspNetHostingPermissionLevel.Low, "Feature_not_supported_at_this_level");
			if (string.IsNullOrEmpty(name))
			{
				name = "AuthorizationStoreRoleProvider";
			}
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			if (string.IsNullOrEmpty(config["description"]))
			{
				config.Remove("description");
				config.Add("description", SR.GetString("RoleAuthStoreProvider_description"));
			}
			base.Initialize(name, config);
			this._CacheRefreshInterval = SecUtility.GetIntValue(config, "cacheRefreshInterval", 60, false, 0);
			this._ScopeName = config["scopeName"];
			if (this._ScopeName != null && this._ScopeName.Length == 0)
			{
				this._ScopeName = null;
			}
			this._ConnectionString = config["connectionStringName"];
			if (this._ConnectionString == null || this._ConnectionString.Length < 1)
			{
				throw new ProviderException(SR.GetString("Connection_name_not_specified"));
			}
			ConnectionStringsSection connectionStrings = RuntimeConfig.GetAppConfig().ConnectionStrings;
			ConnectionStringSettings connectionStringSettings = connectionStrings.ConnectionStrings[this._ConnectionString];
			if (connectionStringSettings == null)
			{
				throw new ProviderException(SR.GetString("Connection_string_not_found", new object[]
				{
					this._ConnectionString
				}));
			}
			if (string.IsNullOrEmpty(connectionStringSettings.ConnectionString))
			{
				throw new ProviderException(SR.GetString("Connection_string_not_found", new object[]
				{
					this._ConnectionString
				}));
			}
			this._ConnectionString = connectionStringSettings.ConnectionString;
			this._AppName = config["applicationName"];
			if (string.IsNullOrEmpty(this._AppName))
			{
				this._AppName = SecUtility.GetDefaultAppName();
			}
			if (this._AppName.Length > 256)
			{
				throw new ProviderException(SR.GetString("Provider_application_name_too_long"));
			}
			config.Remove("connectionStringName");
			config.Remove("cacheRefreshInterval");
			config.Remove("applicationName");
			config.Remove("scopeName");
			if (config.Count > 0)
			{
				string key = config.GetKey(0);
				if (!string.IsNullOrEmpty(key))
				{
					throw new ProviderException(SR.GetString("Provider_unrecognized_attribute", new object[]
					{
						key
					}));
				}
			}
		}

		// Token: 0x06004B6B RID: 19307 RVA: 0x000FFE64 File Offset: 0x000FE064
		public override bool IsUserInRole(string username, string roleName)
		{
			SecUtility.CheckParameter(ref username, true, false, true, 0, "username");
			if (username.Length < 1)
			{
				return false;
			}
			SecUtility.CheckParameter(ref roleName, true, true, true, 0, "roleName");
			return this.IsUserInRoleCore(username, roleName);
		}

		// Token: 0x06004B6C RID: 19308 RVA: 0x000FFEA8 File Offset: 0x000FE0A8
		public override string[] GetRolesForUser(string username)
		{
			SecUtility.CheckParameter(ref username, true, false, true, 0, "username");
			if (username.Length < 1)
			{
				return new string[0];
			}
			return this.GetRolesForUserCore(username);
		}

		// Token: 0x06004B6D RID: 19309 RVA: 0x000FFEE0 File Offset: 0x000FE0E0
		public override void CreateRole(string roleName)
		{
			HttpRuntime.CheckAspNetHostingPermission(AspNetHostingPermissionLevel.Medium, "API_not_supported_at_this_level");
			SecUtility.CheckParameter(ref roleName, true, true, true, 0, "roleName");
			this.InitApp();
			object[] array = new object[]
			{
				roleName,
				null
			};
			object obj = this.CallMethod((this._ObjAzScope != null) ? this._ObjAzScope : this._ObjAzApplication, "CreateRole", array);
			array[0] = 0;
			array[1] = null;
			try
			{
				try
				{
					this.CallMethod(obj, "Submit", array);
				}
				finally
				{
					Marshal.FinalReleaseComObject(obj);
				}
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x06004B6E RID: 19310 RVA: 0x000FFF8C File Offset: 0x000FE18C
		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			HttpRuntime.CheckAspNetHostingPermission(AspNetHostingPermissionLevel.Medium, "API_not_supported_at_this_level");
			SecUtility.CheckParameter(ref roleName, true, true, true, 0, "roleName");
			this.InitApp();
			if (throwOnPopulatedRole)
			{
				string[] usersInRole;
				try
				{
					usersInRole = this.GetUsersInRole(roleName);
				}
				catch
				{
					return false;
				}
				if (usersInRole.Length != 0)
				{
					throw new ProviderException(SR.GetString("Role_is_not_empty"));
				}
			}
			object[] array = new object[]
			{
				roleName,
				null
			};
			this.CallMethod((this._ObjAzScope != null) ? this._ObjAzScope : this._ObjAzApplication, "DeleteRole", array);
			array[0] = 0;
			array[1] = null;
			this.CallMethod((this._ObjAzScope != null) ? this._ObjAzScope : this._ObjAzApplication, "Submit", array);
			return true;
		}

		// Token: 0x06004B6F RID: 19311 RVA: 0x0010005C File Offset: 0x000FE25C
		public override bool RoleExists(string roleName)
		{
			SecUtility.CheckParameter(ref roleName, true, true, true, 0, "roleName");
			bool result = false;
			object obj = null;
			try
			{
				obj = this.GetRole(roleName);
				result = (obj != null);
			}
			catch (TargetInvocationException ex)
			{
				COMException ex2 = ex.InnerException as COMException;
				if (ex2 != null && ex2.ErrorCode == -2147023728)
				{
					return false;
				}
				throw;
			}
			finally
			{
				if (obj != null)
				{
					Marshal.FinalReleaseComObject(obj);
				}
			}
			return result;
		}

		// Token: 0x06004B70 RID: 19312 RVA: 0x001000DC File Offset: 0x000FE2DC
		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			HttpRuntime.CheckAspNetHostingPermission(AspNetHostingPermissionLevel.Medium, "API_not_supported_at_this_level");
			SecUtility.CheckArrayParameter(ref roleNames, true, true, true, 0, "roleNames");
			SecUtility.CheckArrayParameter(ref usernames, true, true, true, 0, "usernames");
			int num = 0;
			object[] array = new object[2];
			object[] array2 = new object[roleNames.Length];
			foreach (string roleName in roleNames)
			{
				array2[num++] = this.GetRole(roleName);
			}
			try
			{
				try
				{
					foreach (object objectToCallOn in array2)
					{
						foreach (string text in usernames)
						{
							array[0] = text;
							array[1] = null;
							this.CallMethod(objectToCallOn, "AddMemberName", array);
						}
					}
					foreach (object objectToCallOn2 in array2)
					{
						array[0] = 0;
						array[1] = null;
						this.CallMethod(objectToCallOn2, "Submit", array);
					}
				}
				finally
				{
					foreach (object o in array2)
					{
						Marshal.FinalReleaseComObject(o);
					}
				}
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x06004B71 RID: 19313 RVA: 0x00100228 File Offset: 0x000FE428
		public override void RemoveUsersFromRoles(string[] userNames, string[] roleNames)
		{
			HttpRuntime.CheckAspNetHostingPermission(AspNetHostingPermissionLevel.Medium, "API_not_supported_at_this_level");
			SecUtility.CheckArrayParameter(ref roleNames, true, true, true, 0, "roleNames");
			SecUtility.CheckArrayParameter(ref userNames, true, true, true, 0, "userNames");
			int num = 0;
			object[] array = new object[2];
			object[] array2 = new object[roleNames.Length];
			foreach (string roleName in roleNames)
			{
				array2[num++] = this.GetRole(roleName);
			}
			try
			{
				try
				{
					foreach (object objectToCallOn in array2)
					{
						foreach (string text in userNames)
						{
							array[0] = text;
							array[1] = null;
							this.CallMethod(objectToCallOn, "DeleteMemberName", array);
						}
					}
					foreach (object objectToCallOn2 in array2)
					{
						array[0] = 0;
						array[1] = null;
						this.CallMethod(objectToCallOn2, "Submit", array);
					}
				}
				finally
				{
					foreach (object o in array2)
					{
						Marshal.FinalReleaseComObject(o);
					}
				}
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x06004B72 RID: 19314 RVA: 0x00100374 File Offset: 0x000FE574
		public override string[] GetUsersInRole(string roleName)
		{
			SecUtility.CheckParameter(ref roleName, true, true, true, 0, "roleName");
			object role = this.GetRole(roleName);
			object obj;
			try
			{
				try
				{
					obj = this.CallProperty(role, "MembersName", null);
				}
				finally
				{
					Marshal.FinalReleaseComObject(role);
				}
			}
			catch
			{
				throw;
			}
			StringCollection stringCollection = new StringCollection();
			try
			{
				if (HostingEnvironment.IsHosted && this._XmlFileName != null)
				{
					InternalSecurityPermissions.Unrestricted.Assert();
				}
				try
				{
					IEnumerable enumerable = (IEnumerable)obj;
					foreach (object obj2 in enumerable)
					{
						stringCollection.Add((string)obj2);
					}
				}
				finally
				{
					if (HostingEnvironment.IsHosted && this._XmlFileName != null)
					{
						CodeAccessPermission.RevertAssert();
					}
				}
			}
			catch
			{
				throw;
			}
			string[] array = new string[stringCollection.Count];
			stringCollection.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06004B73 RID: 19315 RVA: 0x00100494 File Offset: 0x000FE694
		public override string[] GetAllRoles()
		{
			this.InitApp();
			object obj = this.CallProperty((this._ObjAzScope != null) ? this._ObjAzScope : this._ObjAzApplication, "Roles", null);
			StringCollection stringCollection = new StringCollection();
			try
			{
				if (HostingEnvironment.IsHosted && this._XmlFileName != null)
				{
					InternalSecurityPermissions.Unrestricted.Assert();
				}
				try
				{
					IEnumerable enumerable = (IEnumerable)obj;
					foreach (object objectToCallOn in enumerable)
					{
						string value = (string)this.CallProperty(objectToCallOn, "Name", null);
						stringCollection.Add(value);
					}
				}
				finally
				{
					if (HostingEnvironment.IsHosted && this._XmlFileName != null)
					{
						CodeAccessPermission.RevertAssert();
					}
				}
			}
			catch
			{
				throw;
			}
			string[] array = new string[stringCollection.Count];
			stringCollection.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06004B74 RID: 19316 RVA: 0x00003ABB File Offset: 0x00001CBB
		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B75 RID: 19317 RVA: 0x0010059C File Offset: 0x000FE79C
		private object CallMethod(object objectToCallOn, string methodName, object[] args)
		{
			if (HostingEnvironment.IsHosted && this._XmlFileName != null)
			{
				InternalSecurityPermissions.Unrestricted.Assert();
			}
			object result;
			try
			{
				using (new ApplicationImpersonationContext())
				{
					result = objectToCallOn.GetType().InvokeMember(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, null, objectToCallOn, args, CultureInfo.InvariantCulture);
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x06004B76 RID: 19318 RVA: 0x00100610 File Offset: 0x000FE810
		private object CallProperty(object objectToCallOn, string propName, object[] args)
		{
			if (HostingEnvironment.IsHosted && this._XmlFileName != null)
			{
				InternalSecurityPermissions.Unrestricted.Assert();
			}
			object result;
			try
			{
				using (new ApplicationImpersonationContext())
				{
					result = objectToCallOn.GetType().InvokeMember(propName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, null, objectToCallOn, args, CultureInfo.InvariantCulture);
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x06004B77 RID: 19319 RVA: 0x00100684 File Offset: 0x000FE884
		private void InitApp()
		{
			try
			{
				using (new ApplicationImpersonationContext())
				{
					if (this._InitAppDone)
					{
						if (DateTime.Now > this._LastUpdateCacheDate.AddMinutes((double)this.CacheRefreshInterval))
						{
							this._LastUpdateCacheDate = DateTime.Now;
							this.CallMethod(this._ObjAzAuthorizationStoreClass, "UpdateCache", null);
						}
					}
					else
					{
						lock (this)
						{
							if (!this._InitAppDone)
							{
								if (this._ConnectionString.ToLower(CultureInfo.InvariantCulture).StartsWith("msxml://", StringComparison.Ordinal))
								{
									if (this._ConnectionString.Contains("/~/"))
									{
										string text = null;
										if (HostingEnvironment.IsHosted)
										{
											text = HttpRuntime.AppDomainAppPath;
										}
										else
										{
											Process currentProcess = Process.GetCurrentProcess();
											ProcessModule processModule = (currentProcess != null) ? currentProcess.MainModule : null;
											string text2 = (processModule != null) ? processModule.FileName : null;
											if (text2 != null)
											{
												text = Path.GetDirectoryName(text2);
											}
											if (text == null || text.Length < 1)
											{
												text = Environment.CurrentDirectory;
											}
										}
										text = text.Replace('\\', '/');
										this._ConnectionString = this._ConnectionString.Replace("~", text);
									}
									string text3 = this._ConnectionString.Substring("msxml://".Length).Replace('/', '\\');
									if (HostingEnvironment.IsHosted)
									{
										HttpRuntime.CheckFilePermission(text3, false);
									}
									if (!FileUtil.FileExists(text3))
									{
										throw new FileNotFoundException(SR.GetString("AuthStore_policy_file_not_found", new object[]
										{
											HttpRuntime.GetSafePath(text3)
										}));
									}
									this._XmlFileName = text3;
								}
								Type type = null;
								try
								{
									this._NewAuthInterface = true;
									type = Type.GetType("Microsoft.Interop.Security.AzRoles.AzAuthorizationStoreClass, Microsoft.Interop.Security.AzRoles, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", false);
									if (type == null)
									{
										type = Type.GetType("Microsoft.Interop.Security.AzRoles.AzAuthorizationStoreClass, Microsoft.Interop.Security.AzRoles, Version=1.2.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", false);
									}
									if (type == null)
									{
										this._NewAuthInterface = false;
										type = Type.GetType("Microsoft.Interop.Security.AzRoles.AzAuthorizationStoreClass, Microsoft.Interop.Security.AzRoles, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", true);
									}
								}
								catch (FileNotFoundException innerException)
								{
									HttpContext httpContext = HttpContext.Current;
									if (httpContext == null)
									{
										throw new ProviderException(SR.GetString("AuthStoreNotInstalled_Title"), innerException);
									}
									httpContext.Response.Clear();
									httpContext.Response.StatusCode = 500;
									httpContext.Response.Write(AuthStoreErrorFormatter.GetErrorText());
									httpContext.Response.End();
								}
								if (HostingEnvironment.IsHosted && this._XmlFileName != null)
								{
									InternalSecurityPermissions.Unrestricted.Assert();
								}
								this._ObjAzAuthorizationStoreClass = Activator.CreateInstance(type);
								object[] array = new object[3];
								array[0] = 0;
								array[1] = this._ConnectionString;
								object[] array2 = array;
								this.CallMethod(this._ObjAzAuthorizationStoreClass, "Initialize", array2);
								array2 = new object[]
								{
									this._AppName,
									null
								};
								if (this._NewAuthInterface)
								{
									this._ObjAzApplication = this.CallMethod(this._ObjAzAuthorizationStoreClass, "OpenApplication2", array2);
								}
								else
								{
									this._ObjAzApplication = this.CallMethod(this._ObjAzAuthorizationStoreClass, "OpenApplication", array2);
								}
								if (this._ObjAzApplication == null)
								{
									throw new ProviderException(SR.GetString("AuthStore_Application_not_found"));
								}
								this._ObjAzScope = null;
								if (!string.IsNullOrEmpty(this._ScopeName))
								{
									array2[0] = this._ScopeName;
									array2[1] = null;
									this._ObjAzScope = this.CallMethod(this._ObjAzApplication, "OpenScope", array2);
									if (this._ObjAzScope == null)
									{
										throw new ProviderException(SR.GetString("AuthStore_Scope_not_found"));
									}
								}
								this._LastUpdateCacheDate = DateTime.Now;
								this._InitAppDone = true;
							}
						}
					}
				}
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x06004B78 RID: 19320 RVA: 0x00100A58 File Offset: 0x000FEC58
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private IntPtr GetWindowsTokenWithAssert(string userName)
		{
			if (HostingEnvironment.IsHosted)
			{
				HttpContext httpContext = HttpContext.Current;
				if (httpContext != null && httpContext.User != null && httpContext.User.Identity != null && httpContext.User.Identity is WindowsIdentity && StringUtil.EqualsIgnoreCase(userName, httpContext.User.Identity.Name))
				{
					return ((WindowsIdentity)httpContext.User.Identity).Token;
				}
			}
			IPrincipal currentPrincipal = Thread.CurrentPrincipal;
			if (currentPrincipal != null && currentPrincipal.Identity != null && currentPrincipal.Identity is WindowsIdentity && StringUtil.EqualsIgnoreCase(userName, currentPrincipal.Identity.Name))
			{
				return ((WindowsIdentity)currentPrincipal.Identity).Token;
			}
			return IntPtr.Zero;
		}

		// Token: 0x06004B79 RID: 19321 RVA: 0x00100B14 File Offset: 0x000FED14
		private object GetClientContext(string userName)
		{
			this.InitApp();
			IntPtr windowsTokenWithAssert = this.GetWindowsTokenWithAssert(userName);
			if (windowsTokenWithAssert != IntPtr.Zero)
			{
				return this.GetClientContextFromToken(windowsTokenWithAssert);
			}
			return this.GetClientContextFromName(userName);
		}

		// Token: 0x06004B7A RID: 19322 RVA: 0x00100B4C File Offset: 0x000FED4C
		private object GetClientContextFromToken(IntPtr token)
		{
			if (this._NewAuthInterface)
			{
				object[] args = new object[]
				{
					(uint)((int)token),
					0,
					null
				};
				return this.CallMethod(this._ObjAzApplication, "InitializeClientContextFromToken2", args);
			}
			object[] args2 = new object[]
			{
				(ulong)((long)token),
				null
			};
			return this.CallMethod(this._ObjAzApplication, "InitializeClientContextFromToken", args2);
		}

		// Token: 0x06004B7B RID: 19323 RVA: 0x00100BC4 File Offset: 0x000FEDC4
		private object GetClientContextFromName(string userName)
		{
			string[] array = userName.Split(new char[]
			{
				'\\'
			});
			string text = null;
			if (array.Length > 1)
			{
				text = array[0];
				userName = array[1];
			}
			object[] args = new object[]
			{
				userName,
				text,
				null
			};
			return this.CallMethod(this._ObjAzApplication, "InitializeClientContextFromName", args);
		}

		// Token: 0x06004B7C RID: 19324 RVA: 0x00100C1C File Offset: 0x000FEE1C
		private bool IsUserInRoleCore(string username, string roleName)
		{
			object clientContext = this.GetClientContext(username);
			if (clientContext == null)
			{
				return false;
			}
			object obj = this.CallMethod(clientContext, "GetRoles", new object[]
			{
				this._ScopeName
			});
			if (obj == null || !(obj is IEnumerable))
			{
				return false;
			}
			bool result;
			try
			{
				if (HostingEnvironment.IsHosted && this._XmlFileName != null)
				{
					InternalSecurityPermissions.Unrestricted.Assert();
				}
				try
				{
					IEnumerable enumerable = (IEnumerable)obj;
					foreach (object obj2 in enumerable)
					{
						string text = (string)obj2;
						if (text != null && StringUtil.EqualsIgnoreCase(text, roleName))
						{
							return true;
						}
					}
					result = false;
				}
				finally
				{
					if (HostingEnvironment.IsHosted && this._XmlFileName != null)
					{
						CodeAccessPermission.RevertAssert();
					}
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x06004B7D RID: 19325 RVA: 0x00100D14 File Offset: 0x000FEF14
		private string[] GetRolesForUserCore(string username)
		{
			object clientContext = this.GetClientContext(username);
			if (clientContext == null)
			{
				return new string[0];
			}
			object obj = this.CallMethod(clientContext, "GetRoles", new object[]
			{
				this._ScopeName
			});
			if (obj == null || !(obj is IEnumerable))
			{
				return new string[0];
			}
			StringCollection stringCollection = new StringCollection();
			try
			{
				if (HostingEnvironment.IsHosted && this._XmlFileName != null)
				{
					InternalSecurityPermissions.Unrestricted.Assert();
				}
				try
				{
					IEnumerable enumerable = (IEnumerable)obj;
					foreach (object obj2 in enumerable)
					{
						string text = (string)obj2;
						if (text != null)
						{
							stringCollection.Add(text);
						}
					}
				}
				finally
				{
					if (HostingEnvironment.IsHosted && this._XmlFileName != null)
					{
						CodeAccessPermission.RevertAssert();
					}
				}
			}
			catch
			{
				throw;
			}
			string[] array = new string[stringCollection.Count];
			stringCollection.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06004B7E RID: 19326 RVA: 0x00100E2C File Offset: 0x000FF02C
		private object GetRole(string roleName)
		{
			this.InitApp();
			object[] args = new object[]
			{
				roleName,
				null
			};
			return this.CallMethod((this._ObjAzScope != null) ? this._ObjAzScope : this._ObjAzApplication, "OpenRole", args);
		}

		// Token: 0x040028A8 RID: 10408
		private string _AppName;

		// Token: 0x040028A9 RID: 10409
		private string _ConnectionString;

		// Token: 0x040028AA RID: 10410
		private int _CacheRefreshInterval;

		// Token: 0x040028AB RID: 10411
		private string _ScopeName;

		// Token: 0x040028AC RID: 10412
		private object _ObjAzApplication;

		// Token: 0x040028AD RID: 10413
		private bool _InitAppDone;

		// Token: 0x040028AE RID: 10414
		private object _ObjAzScope;

		// Token: 0x040028AF RID: 10415
		private DateTime _LastUpdateCacheDate;

		// Token: 0x040028B0 RID: 10416
		private object _ObjAzAuthorizationStoreClass;

		// Token: 0x040028B1 RID: 10417
		private bool _NewAuthInterface;

		// Token: 0x040028B2 RID: 10418
		private string _XmlFileName;
	}
}
