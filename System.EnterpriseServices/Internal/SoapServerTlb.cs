using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;

namespace System.EnterpriseServices.Internal
{
	// Token: 0x020000F9 RID: 249
	[Guid("F6B6768F-F99E-4152-8ED2-0412F78517FB")]
	public sealed class SoapServerTlb : ISoapServerTlb
	{
		// Token: 0x06000582 RID: 1410 RVA: 0x00013758 File Offset: 0x00012758
		public void AddServerTlb(string progId, string classId, string interfaceId, string srcTlbPath, string rootWebServer, string inBaseUrl, string inVirtualRoot, string clientActivated, string wellKnown, string discoFile, string operation, out string strAssemblyName, out string typeName)
		{
			strAssemblyName = "";
			typeName = "";
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = true;
			try
			{
				try
				{
					SecurityPermission securityPermission = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
					securityPermission.Demand();
				}
				catch (SecurityException)
				{
					string s = Resource.FormatString("Soap_SecurityFailure");
					ComSoapPublishError.Report(s);
					throw;
				}
				Platform.Assert(Platform.Whistler, "SoapServerTlb.AddServerTlb");
				if (operation != null && operation.ToLower(CultureInfo.InvariantCulture) == "delete")
				{
					flag = true;
				}
				if (srcTlbPath.Length > 0)
				{
					flag2 = SoapServerInfo.BoolFromString(discoFile, flag2);
					flag3 = SoapServerInfo.BoolFromString(wellKnown, flag3);
					flag4 = SoapServerInfo.BoolFromString(clientActivated, flag4);
					string str = SoapServerInfo.ServerPhysicalPath(rootWebServer, inBaseUrl, inVirtualRoot, !flag);
					string text = srcTlbPath.ToLower(CultureInfo.InvariantCulture);
					if (text.EndsWith("mscoree.dll", StringComparison.Ordinal))
					{
						Type typeFromProgID = Type.GetTypeFromProgID(progId);
						typeName = typeFromProgID.FullName;
						strAssemblyName = typeFromProgID.Assembly.GetName().Name;
					}
					else if (text.EndsWith("scrobj.dll", StringComparison.Ordinal))
					{
						if (!flag)
						{
							throw new ServicedComponentException(Resource.FormatString("ServicedComponentException_WSCNotSupported"));
						}
					}
					else
					{
						string text2 = "";
						GenerateMetadata generateMetadata = new GenerateMetadata();
						if (flag)
						{
							strAssemblyName = generateMetadata.GetAssemblyName(srcTlbPath, str + "\\bin\\");
						}
						else
						{
							strAssemblyName = generateMetadata.GenerateSigned(srcTlbPath, str + "\\bin\\", false, out text2);
						}
						if (strAssemblyName.Length > 0)
						{
							try
							{
								typeName = this.GetTypeName(str + "\\bin\\" + strAssemblyName + ".dll", progId, classId);
							}
							catch (DirectoryNotFoundException)
							{
								if (!flag)
								{
									throw;
								}
							}
							catch (FileNotFoundException)
							{
								if (!flag)
								{
									throw;
								}
							}
						}
					}
					if (progId.Length > 0 && strAssemblyName.Length > 0 && typeName.Length > 0)
					{
						DiscoFile discoFile2 = new DiscoFile();
						string assemblyFile = str + "\\bin\\" + strAssemblyName + ".dll";
						if (flag)
						{
							SoapServerConfig.DeleteComponent(str + "\\Web.Config", strAssemblyName, typeName, progId, assemblyFile);
							discoFile2.DeleteElement(str + "\\Default.disco", progId + ".soap?WSDL");
						}
						else
						{
							SoapServerConfig.AddComponent(str + "\\Web.Config", strAssemblyName, typeName, progId, assemblyFile, "SingleCall", flag3, flag4);
							if (flag2)
							{
								discoFile2.AddElement(str + "\\Default.disco", progId + ".soap?WSDL");
							}
						}
					}
				}
			}
			catch (ServicedComponentException e)
			{
				this.ThrowHelper("Soap_PublishServerTlbFailure", e);
			}
			catch (RegistrationException e2)
			{
				this.ThrowHelper("Soap_PublishServerTlbFailure", e2);
			}
			catch
			{
				this.ThrowHelper("Soap_PublishServerTlbFailure", null);
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00013A88 File Offset: 0x00012A88
		private void ThrowHelper(string messageId, Exception e)
		{
			string s = Resource.FormatString(messageId);
			ComSoapPublishError.Report(s);
			if (e != null)
			{
				throw e;
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00013AA8 File Offset: 0x00012AA8
		public void DeleteServerTlb(string progId, string classId, string interfaceId, string srcTlbPath, string rootWebServer, string baseUrl, string virtualRoot, string operation, string assemblyName, string typeName)
		{
			try
			{
				SecurityPermission securityPermission = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
				securityPermission.Demand();
			}
			catch (SecurityException)
			{
				string s = Resource.FormatString("Soap_SecurityFailure");
				ComSoapPublishError.Report(s);
				throw;
			}
			Platform.Assert(Platform.Whistler, "SoapServerTlb.DeleteServerTlb");
			string text = assemblyName;
			if (progId.Length <= 0 && classId.Length <= 0 && assemblyName.Length <= 0 && typeName.Length <= 0)
			{
				return;
			}
			if (baseUrl.Length <= 0 && virtualRoot.Length <= 0)
			{
				return;
			}
			string str = SoapServerInfo.ServerPhysicalPath(rootWebServer, baseUrl, virtualRoot, false);
			string text2 = srcTlbPath.ToLower(CultureInfo.InvariantCulture);
			if (text2.EndsWith("scrobj.dll", StringComparison.Ordinal))
			{
				return;
			}
			if (text2.EndsWith("mscoree.dll", StringComparison.Ordinal))
			{
				Type typeFromProgID = Type.GetTypeFromProgID(progId);
				typeName = typeFromProgID.FullName;
				text = typeFromProgID.Assembly.GetName().Name;
			}
			else
			{
				GenerateMetadata generateMetadata = new GenerateMetadata();
				text = generateMetadata.GetAssemblyName(srcTlbPath, str + "\\bin\\");
				if (text.Length > 0)
				{
					try
					{
						typeName = this.GetTypeName(str + "\\bin\\" + text + ".dll", progId, classId);
					}
					catch (DirectoryNotFoundException)
					{
					}
					catch (FileNotFoundException)
					{
					}
				}
			}
			if (progId.Length > 0 && text.Length > 0 && typeName.Length > 0)
			{
				DiscoFile discoFile = new DiscoFile();
				string assemblyFile = str + "\\bin\\" + text + ".dll";
				SoapServerConfig.DeleteComponent(str + "\\Web.Config", text, typeName, progId, assemblyFile);
				discoFile.DeleteElement(str + "\\Default.disco", progId + ".soap?WSDL");
			}
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00013C60 File Offset: 0x00012C60
		internal string GetTypeName(string assemblyPath, string progId, string classId)
		{
			string result = "";
			AppDomain appDomain = AppDomain.CreateDomain("SoapDomain");
			if (appDomain != null)
			{
				try
				{
					AssemblyName name = typeof(AssemblyManager).Assembly.GetName();
					Evidence evidence = AppDomain.CurrentDomain.Evidence;
					Evidence evidence2 = new Evidence(evidence);
					evidence2.AddAssembly(name);
					ObjectHandle objectHandle = appDomain.CreateInstance(name.FullName, typeof(AssemblyManager).FullName, false, BindingFlags.Default, null, null, null, null, evidence2);
					if (objectHandle != null)
					{
						AssemblyManager assemblyManager = (AssemblyManager)objectHandle.Unwrap();
						if (classId.Length > 0)
						{
							result = assemblyManager.InternalGetTypeNameFromClassId(assemblyPath, classId);
						}
						else
						{
							result = assemblyManager.InternalGetTypeNameFromProgId(assemblyPath, progId);
						}
					}
				}
				finally
				{
					AppDomain.Unload(appDomain);
				}
			}
			return result;
		}
	}
}
