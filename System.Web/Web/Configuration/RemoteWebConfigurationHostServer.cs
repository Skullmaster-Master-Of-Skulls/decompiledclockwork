using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Xml;

namespace System.Web.Configuration
{
	// Token: 0x02000238 RID: 568
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[Guid("CE6ED9D0-3BF2-4243-B0AB-919BAC47542B")]
	[ComVisible(true)]
	[ProgId("System.Web.Configuration.RemoteWebConfigurationHostServer_64")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
	public class RemoteWebConfigurationHostServer : IRemoteWebConfigurationHostServer
	{
		// Token: 0x06001E5F RID: 7775 RVA: 0x000885EC File Offset: 0x000875EC
		public byte[] GetData(string fileName, bool getReadTimeOnly, out long readTime)
		{
			if (!fileName.ToLowerInvariant().EndsWith(".config", StringComparison.Ordinal))
			{
				throw new Exception(SR.GetString("Can_not_access_files_other_than_config"));
			}
			byte[] result;
			if (File.Exists(fileName))
			{
				if (getReadTimeOnly)
				{
					result = new byte[0];
				}
				else
				{
					result = File.ReadAllBytes(fileName);
				}
				DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(fileName);
				readTime = ((DateTime.UtcNow > lastWriteTimeUtc) ? DateTime.UtcNow.Ticks : lastWriteTimeUtc.Ticks);
			}
			else
			{
				result = new byte[0];
				readTime = DateTime.UtcNow.Ticks;
			}
			return result;
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x0008867C File Offset: 0x0008767C
		public void WriteData(string fileName, string templateFileName, byte[] data, ref long readTime)
		{
			if (!fileName.ToLowerInvariant().EndsWith(".config", StringComparison.Ordinal))
			{
				throw new Exception(SR.GetString("Can_not_access_files_other_than_config"));
			}
			bool flag = File.Exists(fileName);
			FileAttributes fileAttributes = FileAttributes.Normal;
			string text = null;
			Exception ex = null;
			FileStream fileStream = null;
			if (flag && File.GetLastWriteTimeUtc(fileName).Ticks > readTime)
			{
				throw new Exception(SR.GetString("File_changed_since_read", new object[]
				{
					fileName
				}));
			}
			if (flag)
			{
				try
				{
					FileInfo fileInfo = new FileInfo(fileName);
					fileAttributes = fileInfo.Attributes;
				}
				catch
				{
				}
				if ((fileAttributes & (FileAttributes.ReadOnly | FileAttributes.Hidden)) != (FileAttributes)0)
				{
					throw new Exception(SR.GetString("File_is_read_only", new object[]
					{
						fileName
					}));
				}
			}
			text = fileName + "." + RemoteWebConfigurationHostServer.GetRandomFileExt() + ".tmp";
			int num = 0;
			while (File.Exists(text))
			{
				if (num > 100)
				{
					throw new Exception(SR.GetString("Unable_to_create_temp_file"));
				}
				text = fileName + "." + RemoteWebConfigurationHostServer.GetRandomFileExt() + ".tmp";
				num++;
			}
			try
			{
				fileStream = new FileStream(text, FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite, data.Length, FileOptions.WriteThrough);
				fileStream.Write(data, 0, data.Length);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
			if (ex != null)
			{
				try
				{
					File.Delete(text);
				}
				catch
				{
				}
				throw ex;
			}
			if (flag)
			{
				try
				{
					this.DuplicateFileAttributes(fileName, text);
					goto IL_170;
				}
				catch
				{
					goto IL_170;
				}
			}
			if (templateFileName != null)
			{
				try
				{
					this.DuplicateTemplateAttributes(fileName, templateFileName);
				}
				catch
				{
				}
			}
			IL_170:
			if (!UnsafeNativeMethods.MoveFileEx(text, fileName, 11U))
			{
				try
				{
					File.Delete(text);
				}
				catch
				{
				}
				Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
			}
			if (flag)
			{
				FileInfo fileInfo = new FileInfo(fileName);
				fileInfo.Attributes = fileAttributes;
			}
			long ticks = File.GetLastWriteTimeUtc(fileName).Ticks;
			long ticks2 = DateTime.UtcNow.Ticks;
			readTime = ((ticks2 > ticks) ? ticks2 : ticks);
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x000888B4 File Offset: 0x000878B4
		public string GetFilePaths(int webLevelAsInt, string path, string site, string locationSubPath)
		{
			IConfigMapPath instance = IISMapPath.GetInstance();
			VirtualPath virtualPath;
			string value;
			string text;
			string text2;
			string value2;
			WebConfigurationHost.GetConfigPaths(instance, (WebLevel)webLevelAsInt, VirtualPath.CreateNonRelativeAllowNull(path), site, locationSubPath, out virtualPath, out value, out text, out text2, out value2);
			ArrayList arrayList = new ArrayList();
			arrayList.Add(VirtualPath.GetVirtualPathString(virtualPath));
			arrayList.Add(value);
			arrayList.Add(text);
			arrayList.Add(text2);
			arrayList.Add(value2);
			string text3;
			VirtualPath virtualPath2;
			WebConfigurationHost.GetSiteIDAndVPathFromConfigPath(text2, out text3, out virtualPath2);
			arrayList.Add("machine");
			arrayList.Add(HttpConfigurationSystem.MachineConfigurationFilePath);
			if (webLevelAsInt != 1)
			{
				arrayList.Add("machine/webroot");
				arrayList.Add(HttpConfigurationSystem.RootWebConfigurationFilePath);
				VirtualPath virtualPath3 = virtualPath2;
				while (virtualPath3 != null)
				{
					string configPathFromSiteIDAndVPath = WebConfigurationHost.GetConfigPathFromSiteIDAndVPath(text, virtualPath3);
					string text4 = instance.MapPath(text, virtualPath3.VirtualPathString);
					text4 = Path.Combine(text4, "web.config");
					arrayList.Add(configPathFromSiteIDAndVPath);
					arrayList.Add(text4);
					virtualPath3 = virtualPath3.Parent;
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < arrayList.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append('<');
				}
				string value3 = (string)arrayList[i];
				stringBuilder.Append(value3);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001E62 RID: 7778 RVA: 0x00088A04 File Offset: 0x00087A04
		public string DoEncryptOrDecrypt(bool doEncrypt, string xmlString, string protectionProviderName, string protectionProviderType, string[] paramKeys, string[] paramValues)
		{
			Type type = Type.GetType(protectionProviderType, true);
			if (!typeof(ProtectedConfigurationProvider).IsAssignableFrom(type))
			{
				throw new Exception(SR.GetString("WrongType_of_Protected_provider"));
			}
			ProtectedConfigurationProvider protectedConfigurationProvider = (ProtectedConfigurationProvider)Activator.CreateInstance(type);
			NameValueCollection nameValueCollection = new NameValueCollection(paramKeys.Length);
			for (int i = 0; i < paramKeys.Length; i++)
			{
				nameValueCollection.Add(paramKeys[i], paramValues[i]);
			}
			protectedConfigurationProvider.Initialize(protectionProviderName, nameValueCollection);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			xmlDocument.LoadXml(xmlString);
			XmlNode xmlNode;
			if (doEncrypt)
			{
				xmlNode = protectedConfigurationProvider.Encrypt(xmlDocument.DocumentElement);
			}
			else
			{
				xmlNode = protectedConfigurationProvider.Decrypt(xmlDocument.DocumentElement);
			}
			return xmlNode.OuterXml;
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x00088ABC File Offset: 0x00087ABC
		public void GetFileDetails(string name, out bool exists, out long size, out long createDate, out long lastWriteDate)
		{
			if (!name.ToLowerInvariant().EndsWith(".config", StringComparison.Ordinal))
			{
				throw new Exception(SR.GetString("Can_not_access_files_other_than_config"));
			}
			UnsafeNativeMethods.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA;
			if (UnsafeNativeMethods.GetFileAttributesEx(name, 0, out win32_FILE_ATTRIBUTE_DATA) && (win32_FILE_ATTRIBUTE_DATA.fileAttributes & 16) == 0)
			{
				exists = true;
				size = (long)((ulong)win32_FILE_ATTRIBUTE_DATA.fileSizeHigh << 32 | (ulong)win32_FILE_ATTRIBUTE_DATA.fileSizeLow);
				createDate = (long)((ulong)win32_FILE_ATTRIBUTE_DATA.ftCreationTimeHigh << 32 | (ulong)win32_FILE_ATTRIBUTE_DATA.ftCreationTimeLow);
				lastWriteDate = (long)((ulong)win32_FILE_ATTRIBUTE_DATA.ftLastWriteTimeHigh << 32 | (ulong)win32_FILE_ATTRIBUTE_DATA.ftLastWriteTimeLow);
				return;
			}
			exists = false;
			size = 0L;
			createDate = 0L;
			lastWriteDate = 0L;
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x00088B5C File Offset: 0x00087B5C
		private static string GetRandomFileExt()
		{
			byte[] array = new byte[2];
			new RNGCryptoServiceProvider().GetBytes(array);
			return array[1].ToString("X", CultureInfo.InvariantCulture) + array[0].ToString("X", CultureInfo.InvariantCulture);
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x00088BAC File Offset: 0x00087BAC
		private void DuplicateFileAttributes(string oldFileName, string newFileName)
		{
			FileAttributes attributes = File.GetAttributes(oldFileName);
			File.SetAttributes(newFileName, attributes);
			DateTime creationTimeUtc = File.GetCreationTimeUtc(oldFileName);
			File.SetCreationTimeUtc(newFileName, creationTimeUtc);
			this.DuplicateTemplateAttributes(oldFileName, newFileName);
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x00088BE0 File Offset: 0x00087BE0
		private void DuplicateTemplateAttributes(string oldFileName, string newFileName)
		{
			FileSecurity accessControl;
			try
			{
				accessControl = File.GetAccessControl(oldFileName, AccessControlSections.Audit | AccessControlSections.Access);
				accessControl.SetAuditRuleProtection(accessControl.AreAuditRulesProtected, true);
			}
			catch (UnauthorizedAccessException)
			{
				accessControl = File.GetAccessControl(oldFileName, AccessControlSections.Access);
			}
			accessControl.SetAccessRuleProtection(accessControl.AreAccessRulesProtected, true);
			File.SetAccessControl(newFileName, accessControl);
		}

		// Token: 0x040019C0 RID: 6592
		internal const char FilePathsSeparatorChar = '<';

		// Token: 0x040019C1 RID: 6593
		private const int MOVEFILE_REPLACE_EXISTING = 1;

		// Token: 0x040019C2 RID: 6594
		private const int MOVEFILE_COPY_ALLOWED = 2;

		// Token: 0x040019C3 RID: 6595
		private const int MOVEFILE_DELAY_UNTIL_REBOOT = 4;

		// Token: 0x040019C4 RID: 6596
		private const int MOVEFILE_WRITE_THROUGH = 8;

		// Token: 0x040019C5 RID: 6597
		internal static readonly char[] FilePathsSeparatorParams = new char[]
		{
			'<'
		};
	}
}
