using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Security;
using System.Web.Security.Cryptography;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000322 RID: 802
	internal static class Util
	{
		// Token: 0x06002547 RID: 9543 RVA: 0x0007AC45 File Offset: 0x00078E45
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		internal static string SerializeWithAssert(IStateFormatter formatter, object stateGraph)
		{
			return formatter.Serialize(stateGraph);
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x0007AC4E File Offset: 0x00078E4E
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		internal static string SerializeWithAssert(IStateFormatter2 formatter, object stateGraph, Purpose purpose)
		{
			return formatter.Serialize(stateGraph, purpose);
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x0007AC58 File Offset: 0x00078E58
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		internal static object DeserializeWithAssert(IStateFormatter2 formatter, string serializedState, Purpose purpose)
		{
			return formatter.Deserialize(serializedState, purpose);
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x0007AC62 File Offset: 0x00078E62
		internal static bool CanConvertToFrom(TypeConverter converter, Type type)
		{
			return converter != null && converter.CanConvertTo(type) && converter.CanConvertFrom(type) && !(converter is ReferenceConverter);
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x0007AC88 File Offset: 0x00078E88
		internal static void CopyBaseAttributesToInnerControl(WebControl control, WebControl child)
		{
			short tabIndex = control.TabIndex;
			string accessKey = control.AccessKey;
			try
			{
				control.AccessKey = string.Empty;
				control.TabIndex = 0;
				child.CopyBaseAttributes(control);
			}
			finally
			{
				control.TabIndex = tabIndex;
				control.AccessKey = accessKey;
			}
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x0007ACE0 File Offset: 0x00078EE0
		internal static long GetRecompilationHash(PagesSection ps)
		{
			HashCodeCombiner hashCodeCombiner = new HashCodeCombiner();
			hashCodeCombiner.AddObject(ps.Buffer);
			hashCodeCombiner.AddObject(ps.EnableViewState);
			hashCodeCombiner.AddObject(ps.EnableViewStateMac);
			hashCodeCombiner.AddObject(ps.EnableEventValidation);
			hashCodeCombiner.AddObject(ps.SmartNavigation);
			hashCodeCombiner.AddObject(ps.ValidateRequest);
			hashCodeCombiner.AddObject(ps.AutoEventWireup);
			if (ps.PageBaseTypeInternal != null)
			{
				hashCodeCombiner.AddObject(ps.PageBaseTypeInternal.FullName);
			}
			if (ps.UserControlBaseTypeInternal != null)
			{
				hashCodeCombiner.AddObject(ps.UserControlBaseTypeInternal.FullName);
			}
			if (ps.PageParserFilterTypeInternal != null)
			{
				hashCodeCombiner.AddObject(ps.PageParserFilterTypeInternal.FullName);
			}
			hashCodeCombiner.AddObject(ps.MasterPageFile);
			hashCodeCombiner.AddObject(ps.Theme);
			hashCodeCombiner.AddObject(ps.StyleSheetTheme);
			hashCodeCombiner.AddObject(ps.EnableSessionState);
			hashCodeCombiner.AddObject(ps.CompilationMode);
			hashCodeCombiner.AddObject(ps.MaxPageStateFieldLength);
			hashCodeCombiner.AddObject(ps.ViewStateEncryptionMode);
			hashCodeCombiner.AddObject(ps.MaintainScrollPositionOnPostBack);
			NamespaceCollection namespaces = ps.Namespaces;
			hashCodeCombiner.AddObject(namespaces.AutoImportVBNamespace);
			if (namespaces.Count == 0)
			{
				hashCodeCombiner.AddObject("__clearnamespaces");
			}
			else
			{
				foreach (object obj in namespaces)
				{
					NamespaceInfo namespaceInfo = (NamespaceInfo)obj;
					hashCodeCombiner.AddObject(namespaceInfo.Namespace);
				}
			}
			TagPrefixCollection controls = ps.Controls;
			if (controls.Count == 0)
			{
				hashCodeCombiner.AddObject("__clearcontrols");
			}
			else
			{
				foreach (object obj2 in controls)
				{
					TagPrefixInfo tagPrefixInfo = (TagPrefixInfo)obj2;
					hashCodeCombiner.AddObject(tagPrefixInfo.TagPrefix);
					if (tagPrefixInfo.TagName != null && tagPrefixInfo.TagName.Length != 0)
					{
						hashCodeCombiner.AddObject(tagPrefixInfo.TagName);
						hashCodeCombiner.AddObject(tagPrefixInfo.Source);
					}
					else
					{
						hashCodeCombiner.AddObject(tagPrefixInfo.Namespace);
						hashCodeCombiner.AddObject(tagPrefixInfo.Assembly);
					}
				}
			}
			TagMapCollection tagMapping = ps.TagMapping;
			if (tagMapping.Count == 0)
			{
				hashCodeCombiner.AddObject("__cleartagmapping");
			}
			else
			{
				foreach (object obj3 in tagMapping)
				{
					TagMapInfo tagMapInfo = (TagMapInfo)obj3;
					hashCodeCombiner.AddObject(tagMapInfo.TagType);
					hashCodeCombiner.AddObject(tagMapInfo.MappedTagType);
				}
			}
			return hashCodeCombiner.CombinedHash;
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x0007AFD0 File Offset: 0x000791D0
		internal static Encoding GetEncodingFromConfigPath(VirtualPath configPath)
		{
			GlobalizationSection globalization = RuntimeConfig.GetConfig(configPath).Globalization;
			Encoding encoding = globalization.FileEncoding;
			if (encoding == null)
			{
				encoding = Encoding.Default;
			}
			return encoding;
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x0007AFFC File Offset: 0x000791FC
		internal static StreamReader ReaderFromFile(string filename, VirtualPath configPath)
		{
			Encoding encoding = Encoding.Default;
			if (configPath != null)
			{
				encoding = Util.GetEncodingFromConfigPath(configPath);
			}
			StreamReader result;
			try
			{
				result = new StreamReader(filename, encoding, true, 4096);
			}
			catch (UnauthorizedAccessException)
			{
				if (FileUtil.DirectoryExists(filename))
				{
					throw new HttpException(SR.GetString("Unexpected_Directory", new object[]
					{
						HttpRuntime.GetSafePath(filename)
					}));
				}
				throw;
			}
			return result;
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x0007B06C File Offset: 0x0007926C
		internal static void DeleteFileNoException(string path)
		{
			try
			{
				File.Delete(path);
			}
			catch
			{
			}
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x0007B094 File Offset: 0x00079294
		internal static void DeleteFileIfExistsNoException(string path)
		{
			if (File.Exists(path))
			{
				Util.DeleteFileNoException(path);
			}
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x0007B0A4 File Offset: 0x000792A4
		internal static bool IsNonEmptyDirectory(string dir)
		{
			if (!Directory.Exists(dir))
			{
				return false;
			}
			bool result;
			try
			{
				string[] fileSystemEntries = Directory.GetFileSystemEntries(dir);
				result = (fileSystemEntries.Length != 0);
			}
			catch
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x0007B0E4 File Offset: 0x000792E4
		internal static bool IsValidFileName(string fileName)
		{
			return !(fileName == ".") && !(fileName == "..") && fileName.IndexOfAny(Util.invalidFileNameChars) < 0;
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x0007B114 File Offset: 0x00079314
		internal static string MakeValidFileName(string fileName)
		{
			if (Util.IsValidFileName(fileName))
			{
				return fileName;
			}
			for (int i = 0; i < Util.invalidFileNameChars.Length; i++)
			{
				fileName = fileName.Replace(Util.invalidFileNameChars[i], '_');
			}
			return fileName;
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x0007B150 File Offset: 0x00079350
		internal static bool HasWriteAccessToDirectory(string dir)
		{
			if (!Directory.Exists(dir))
			{
				return false;
			}
			string path = Path.Combine(dir, "~AspAccessCheck_" + HostingEnvironment.AppDomainUniqueInteger.ToString("x", CultureInfo.InvariantCulture) + SafeNativeMethods.GetCurrentThreadId().ToString() + ".tmp");
			FileStream fileStream = null;
			bool result = false;
			try
			{
				fileStream = new FileStream(path, FileMode.Create);
			}
			catch
			{
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
					File.Delete(path);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06002555 RID: 9557 RVA: 0x0007B1E4 File Offset: 0x000793E4
		internal static VirtualPath GetScriptLocation()
		{
			string text = (string)RuntimeConfig.GetAppConfig().WebControls["clientScriptsLocation"];
			if (text.IndexOf("{0}", StringComparison.Ordinal) >= 0)
			{
				string text2 = "system_web";
				string text3 = VersionInfo.SystemWebVersion.Substring(0, VersionInfo.SystemWebVersion.LastIndexOf('.')).Replace('.', '_');
				text = string.Format(CultureInfo.InvariantCulture, text, new object[]
				{
					text2,
					text3
				});
			}
			return VirtualPath.Create(text);
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x0007B264 File Offset: 0x00079464
		internal static StreamReader ReaderFromStream(Stream stream, VirtualPath configPath)
		{
			Encoding encodingFromConfigPath = Util.GetEncodingFromConfigPath(configPath);
			return new StreamReader(stream, encodingFromConfigPath, true, 4096);
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x0007B288 File Offset: 0x00079488
		internal static string StringFromVirtualPath(VirtualPath virtualPath)
		{
			string result;
			using (Stream stream = virtualPath.OpenFile())
			{
				TextReader textReader = Util.ReaderFromStream(stream, virtualPath);
				result = textReader.ReadToEnd();
			}
			return result;
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x0007B2C8 File Offset: 0x000794C8
		internal static string StringFromFile(string path)
		{
			Encoding @default = Encoding.Default;
			return Util.StringFromFile(path, ref @default);
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x0007B2E4 File Offset: 0x000794E4
		internal static string StringFromFile(string path, ref Encoding encoding)
		{
			StreamReader streamReader = new StreamReader(path, encoding, true);
			string result;
			try
			{
				string text = streamReader.ReadToEnd();
				encoding = streamReader.CurrentEncoding;
				result = text;
			}
			finally
			{
				if (streamReader != null)
				{
					streamReader.Close();
				}
			}
			return result;
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x0007B32C File Offset: 0x0007952C
		internal static string StringFromFileIfExists(string path)
		{
			if (!File.Exists(path))
			{
				return null;
			}
			return Util.StringFromFile(path);
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x0007B340 File Offset: 0x00079540
		internal static void RemoveOrRenameFile(string filename)
		{
			FileInfo f = new FileInfo(filename);
			Util.RemoveOrRenameFile(f);
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x0007B35C File Offset: 0x0007955C
		internal static bool RemoveOrRenameFile(FileInfo f)
		{
			try
			{
				f.Delete();
				return true;
			}
			catch
			{
				try
				{
					if (f.Extension != ".delete")
					{
						string str = DateTime.Now.Ticks.GetHashCode().ToString("x", CultureInfo.InvariantCulture);
						string destFileName = f.FullName + "." + str + ".delete";
						f.MoveTo(destFileName);
					}
				}
				catch
				{
				}
			}
			return false;
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x0007B3F4 File Offset: 0x000795F4
		internal static void ClearReadOnlyAttribute(string path)
		{
			FileAttributes attributes = File.GetAttributes(path);
			if ((attributes & FileAttributes.ReadOnly) != (FileAttributes)0)
			{
				File.SetAttributes(path, attributes & ~FileAttributes.ReadOnly);
			}
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x0007B417 File Offset: 0x00079617
		internal static void CheckVirtualFileExists(VirtualPath virtualPath)
		{
			if (!virtualPath.FileExists())
			{
				throw new HttpException(404, SR.GetString("FileName_does_not_exist", new object[]
				{
					virtualPath.VirtualPathString
				}));
			}
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x0007B448 File Offset: 0x00079648
		internal static bool VirtualFileExistsWithAssert(VirtualPath virtualPath)
		{
			string text = virtualPath.MapPathInternal();
			if (text != null)
			{
				InternalSecurityPermissions.PathDiscovery(text).Assert();
			}
			return virtualPath.FileExists();
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x0007B470 File Offset: 0x00079670
		internal static void CheckThemeAttribute(string themeName)
		{
			if (themeName.Length > 0)
			{
				if (!FileUtil.IsValidDirectoryName(themeName))
				{
					throw new HttpException(SR.GetString("Page_theme_invalid_name", new object[]
					{
						themeName
					}));
				}
				if (!Util.ThemeExists(themeName))
				{
					throw new HttpException(SR.GetString("Page_theme_not_found", new object[]
					{
						themeName
					}));
				}
			}
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x0007B4CC File Offset: 0x000796CC
		internal static bool ThemeExists(string themeName)
		{
			VirtualPath virtualDir = ThemeDirectoryCompiler.GetAppThemeVirtualDir(themeName);
			if (!Util.VirtualDirectoryExistsWithAssert(virtualDir))
			{
				virtualDir = ThemeDirectoryCompiler.GetGlobalThemeVirtualDir(themeName);
				if (!Util.VirtualDirectoryExistsWithAssert(virtualDir))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x0007B4FC File Offset: 0x000796FC
		private static bool VirtualDirectoryExistsWithAssert(VirtualPath virtualDir)
		{
			bool result;
			try
			{
				string text = virtualDir.MapPathInternal();
				if (text != null)
				{
					new FileIOPermission(FileIOPermissionAccess.Read, text).Assert();
				}
				result = virtualDir.DirectoryExists();
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x0007B540 File Offset: 0x00079740
		internal static void CheckAssignableType(Type baseType, Type type)
		{
			if (!baseType.IsAssignableFrom(type))
			{
				throw new HttpException(SR.GetString("Type_doesnt_inherit_from_type", new object[]
				{
					type.FullName,
					baseType.FullName
				}));
			}
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x0007B574 File Offset: 0x00079774
		internal static int LineCount(string text, int offset, int newoffset)
		{
			int num = 0;
			while (offset < newoffset)
			{
				if (text[offset] == '\r' || (text[offset] == '\n' && (offset == 0 || text[offset - 1] != '\r')))
				{
					num++;
				}
				offset++;
			}
			return num;
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x0007B5BC File Offset: 0x000797BC
		internal static object InvokeMethod(MethodInfo methodInfo, object obj, object[] parameters)
		{
			object result;
			try
			{
				result = methodInfo.Invoke(obj, parameters);
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
			return result;
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x0007B5F0 File Offset: 0x000797F0
		internal static Type GetNonPrivateFieldType(Type classType, string fieldName)
		{
			FieldInfo field = classType.GetField(fieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (field == null || field.IsPrivate)
			{
				return null;
			}
			return field.FieldType;
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x0007B620 File Offset: 0x00079820
		internal static Type GetNonPrivatePropertyType(Type classType, string propName)
		{
			PropertyInfo propertyInfo = null;
			BindingFlags bindingFlags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			try
			{
				propertyInfo = classType.GetProperty(propName, bindingFlags);
			}
			catch (AmbiguousMatchException)
			{
				bindingFlags |= BindingFlags.DeclaredOnly;
				propertyInfo = classType.GetProperty(propName, bindingFlags);
			}
			if (propertyInfo == null)
			{
				return null;
			}
			MethodInfo setMethod = propertyInfo.GetSetMethod(true);
			if (setMethod == null || setMethod.IsPrivate)
			{
				return null;
			}
			return propertyInfo.PropertyType;
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x0007B68C File Offset: 0x0007988C
		internal static bool IsMultiInstanceTemplateProperty(PropertyInfo pInfo)
		{
			object[] customAttributes = pInfo.GetCustomAttributes(typeof(TemplateInstanceAttribute), false);
			return customAttributes == null || customAttributes.Length == 0 || ((TemplateInstanceAttribute)customAttributes[0]).Instances == TemplateInstance.Multiple;
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x0007B6C4 File Offset: 0x000798C4
		private static string FirstDictionaryKey(IDictionary dict)
		{
			IDictionaryEnumerator enumerator = dict.GetEnumerator();
			enumerator.MoveNext();
			return (string)enumerator.Key;
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x0007B6EC File Offset: 0x000798EC
		private static string GetAndRemove(IDictionary dict, string key)
		{
			string text = (string)dict[key];
			if (text != null)
			{
				dict.Remove(key);
				text = text.Trim();
			}
			return text;
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x0007B718 File Offset: 0x00079918
		internal static string GetAndRemoveNonEmptyAttribute(IDictionary directives, string key, bool required)
		{
			string andRemove = Util.GetAndRemove(directives, key);
			if (andRemove != null)
			{
				return Util.GetNonEmptyAttribute(key, andRemove);
			}
			if (required)
			{
				throw new HttpException(SR.GetString("Missing_attr", new object[]
				{
					key
				}));
			}
			return null;
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x0007B756 File Offset: 0x00079956
		internal static string GetNonEmptyAttribute(string name, string value)
		{
			value = value.Trim();
			if (value.Length == 0)
			{
				throw new HttpException(SR.GetString("Empty_attribute", new object[]
				{
					name
				}));
			}
			return value;
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x0007B783 File Offset: 0x00079983
		internal static string GetNoSpaceAttribute(string name, string value)
		{
			if (Util.ContainsWhiteSpace(value))
			{
				throw new HttpException(SR.GetString("Space_attribute", new object[]
				{
					name
				}));
			}
			return value;
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x0007B7A8 File Offset: 0x000799A8
		internal static string GetAndRemoveNonEmptyAttribute(IDictionary directives, string key)
		{
			return Util.GetAndRemoveNonEmptyAttribute(directives, key, false);
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x0007B7B2 File Offset: 0x000799B2
		internal static VirtualPath GetAndRemoveVirtualPathAttribute(IDictionary directives, string key)
		{
			return Util.GetAndRemoveVirtualPathAttribute(directives, key, false);
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x0007B7BC File Offset: 0x000799BC
		internal static VirtualPath GetAndRemoveVirtualPathAttribute(IDictionary directives, string key, bool required)
		{
			string andRemoveNonEmptyAttribute = Util.GetAndRemoveNonEmptyAttribute(directives, key, required);
			if (andRemoveNonEmptyAttribute == null)
			{
				return null;
			}
			return VirtualPath.Create(andRemoveNonEmptyAttribute);
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x0007B7E0 File Offset: 0x000799E0
		public static string ParsePropertyDeviceFilter(string input, out string propName)
		{
			string result = string.Empty;
			if (input.IndexOf(':') < 0)
			{
				propName = input;
			}
			else if (StringUtil.StringStartsWithIgnoreCase(input, "xmlns:"))
			{
				propName = input;
			}
			else
			{
				string[] array = input.Split(new char[]
				{
					':'
				});
				if (array.Length > 2)
				{
					throw new HttpException(SR.GetString("Too_many_filters", new object[]
					{
						input
					}));
				}
				if (MTConfigUtil.GetPagesConfig().IgnoreDeviceFilters[array[0]] != null)
				{
					propName = input;
				}
				else
				{
					result = array[0];
					propName = array[1];
				}
			}
			return result;
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x0007B86A File Offset: 0x00079A6A
		public static string CreateFilteredName(string deviceName, string name)
		{
			if (deviceName.Length > 0)
			{
				return deviceName + ":" + name;
			}
			return name;
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x0007B883 File Offset: 0x00079A83
		internal static string GetAndRemoveRequiredAttribute(IDictionary directives, string key)
		{
			return Util.GetAndRemoveNonEmptyAttribute(directives, key, true);
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x0007B890 File Offset: 0x00079A90
		internal static string GetAndRemoveNonEmptyNoSpaceAttribute(IDictionary directives, string key, bool required)
		{
			string andRemoveNonEmptyAttribute = Util.GetAndRemoveNonEmptyAttribute(directives, key, required);
			if (andRemoveNonEmptyAttribute == null)
			{
				return null;
			}
			return Util.GetNonEmptyNoSpaceAttribute(key, andRemoveNonEmptyAttribute);
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x0007B8B2 File Offset: 0x00079AB2
		internal static string GetAndRemoveNonEmptyNoSpaceAttribute(IDictionary directives, string key)
		{
			return Util.GetAndRemoveNonEmptyNoSpaceAttribute(directives, key, false);
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x0007B8BC File Offset: 0x00079ABC
		internal static string GetNonEmptyNoSpaceAttribute(string name, string value)
		{
			value = Util.GetNonEmptyAttribute(name, value);
			return Util.GetNoSpaceAttribute(name, value);
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x0007B8D0 File Offset: 0x00079AD0
		internal static string GetAndRemoveNonEmptyIdentifierAttribute(IDictionary directives, string key, bool required)
		{
			string andRemoveNonEmptyNoSpaceAttribute = Util.GetAndRemoveNonEmptyNoSpaceAttribute(directives, key, required);
			if (andRemoveNonEmptyNoSpaceAttribute == null)
			{
				return null;
			}
			return Util.GetNonEmptyIdentifierAttribute(key, andRemoveNonEmptyNoSpaceAttribute);
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x0007B8F2 File Offset: 0x00079AF2
		internal static string GetNonEmptyIdentifierAttribute(string name, string value)
		{
			value = Util.GetNonEmptyNoSpaceAttribute(name, value);
			if (!CodeGenerator.IsValidLanguageIndependentIdentifier(value))
			{
				throw new HttpException(SR.GetString("Invalid_attribute_value", new object[]
				{
					value,
					name
				}));
			}
			return value;
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x0007B924 File Offset: 0x00079B24
		internal static string GetNonEmptyFullClassNameAttribute(string name, string value, ref string ns)
		{
			value = Util.GetNonEmptyNoSpaceAttribute(name, value);
			string[] array = value.Split(new char[]
			{
				'.'
			});
			foreach (string value2 in array)
			{
				if (!CodeGenerator.IsValidLanguageIndependentIdentifier(value2))
				{
					throw new HttpException(SR.GetString("Invalid_attribute_value", new object[]
					{
						value,
						name
					}));
				}
			}
			if (array.Length > 1)
			{
				ns = string.Join(".", array, 0, array.Length - 1);
			}
			return array[array.Length - 1];
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x0007B9A6 File Offset: 0x00079BA6
		internal static void CheckUnknownDirectiveAttributes(string directiveName, IDictionary directive)
		{
			Util.CheckUnknownDirectiveAttributes(directiveName, directive, "Attr_not_supported_in_directive");
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x0007B9B4 File Offset: 0x00079BB4
		internal static void CheckUnknownDirectiveAttributes(string directiveName, IDictionary directive, string resourceKey)
		{
			if (directive.Count > 0)
			{
				throw new HttpException(SR.GetString(resourceKey, new object[]
				{
					Util.FirstDictionaryKey(directive),
					directiveName
				}));
			}
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x0007B9E0 File Offset: 0x00079BE0
		internal static bool GetAndRemoveBooleanAttribute(IDictionary directives, string key, ref bool val)
		{
			string andRemove = Util.GetAndRemove(directives, key);
			if (andRemove == null)
			{
				return false;
			}
			val = Util.GetBooleanAttribute(key, andRemove);
			return true;
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x0007BA04 File Offset: 0x00079C04
		internal static bool GetBooleanAttribute(string name, string value)
		{
			bool result;
			try
			{
				result = bool.Parse(value);
			}
			catch
			{
				throw new HttpException(SR.GetString("Invalid_boolean_attribute", new object[]
				{
					name
				}));
			}
			return result;
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x0007BA48 File Offset: 0x00079C48
		internal static bool GetAndRemoveNonNegativeIntegerAttribute(IDictionary directives, string key, ref int val)
		{
			string andRemove = Util.GetAndRemove(directives, key);
			if (andRemove == null)
			{
				return false;
			}
			val = Util.GetNonNegativeIntegerAttribute(key, andRemove);
			return true;
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x0007BA6C File Offset: 0x00079C6C
		internal static int GetNonNegativeIntegerAttribute(string name, string value)
		{
			int num;
			try
			{
				num = int.Parse(value, CultureInfo.InvariantCulture);
			}
			catch
			{
				throw new HttpException(SR.GetString("Invalid_nonnegative_integer_attribute", new object[]
				{
					name
				}));
			}
			if (num < 0)
			{
				throw new HttpException(SR.GetString("Invalid_nonnegative_integer_attribute", new object[]
				{
					name
				}));
			}
			return num;
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x0007BAD4 File Offset: 0x00079CD4
		internal static bool GetAndRemovePositiveIntegerAttribute(IDictionary directives, string key, ref int val)
		{
			string andRemove = Util.GetAndRemove(directives, key);
			if (andRemove == null)
			{
				return false;
			}
			try
			{
				val = int.Parse(andRemove, CultureInfo.InvariantCulture);
			}
			catch
			{
				throw new HttpException(SR.GetString("Invalid_positive_integer_attribute", new object[]
				{
					key
				}));
			}
			if (val <= 0)
			{
				throw new HttpException(SR.GetString("Invalid_positive_integer_attribute", new object[]
				{
					key
				}));
			}
			return true;
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x0007BB4C File Offset: 0x00079D4C
		internal static object GetAndRemoveEnumAttribute(IDictionary directives, Type enumType, string key)
		{
			string andRemove = Util.GetAndRemove(directives, key);
			if (andRemove == null)
			{
				return null;
			}
			return Util.GetEnumAttribute(key, andRemove, enumType);
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x0007BB6E File Offset: 0x00079D6E
		internal static object GetEnumAttribute(string name, string value, Type enumType)
		{
			return Util.GetEnumAttribute(name, value, enumType, false);
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x0007BB7C File Offset: 0x00079D7C
		internal static object GetEnumAttribute(string name, string value, Type enumType, bool allowMultiple)
		{
			object result;
			try
			{
				if (char.IsDigit(value[0]) || value[0] == '-' || (!allowMultiple && value.IndexOf(',') >= 0))
				{
					throw new FormatException(SR.GetString("EnumAttributeInvalidString", new object[]
					{
						value,
						name,
						enumType.FullName
					}));
				}
				result = Enum.Parse(enumType, value, true);
			}
			catch
			{
				string text = null;
				foreach (string text2 in Enum.GetNames(enumType))
				{
					if (text == null)
					{
						text = text2;
					}
					else
					{
						text = text + ", " + text2;
					}
				}
				throw new HttpException(SR.GetString("Invalid_enum_attribute", new object[]
				{
					name,
					text
				}));
			}
			return result;
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x0007BC44 File Offset: 0x00079E44
		internal static bool IsWhiteSpaceString(string s)
		{
			return s.Trim().Length == 0;
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x0007BC54 File Offset: 0x00079E54
		internal static bool ContainsWhiteSpace(string s)
		{
			for (int i = s.Length - 1; i >= 0; i--)
			{
				if (char.IsWhiteSpace(s[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x0007BC88 File Offset: 0x00079E88
		internal static int FirstNonWhiteSpaceIndex(string s)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (!char.IsWhiteSpace(s[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x0007BCB7 File Offset: 0x00079EB7
		internal static bool IsTrueString(string s)
		{
			return s != null && StringUtil.EqualsIgnoreCase(s, "true");
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x0007BCC9 File Offset: 0x00079EC9
		internal static bool IsFalseString(string s)
		{
			return s != null && StringUtil.EqualsIgnoreCase(s, "false");
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x0007BCDB File Offset: 0x00079EDB
		internal static string GetStringFromBool(bool flag)
		{
			if (!flag)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x0007BCEB File Offset: 0x00079EEB
		internal static string MakeFullTypeName(string ns, string typeName)
		{
			if (string.IsNullOrEmpty(ns))
			{
				return typeName;
			}
			return ns + "." + typeName;
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x0007BD04 File Offset: 0x00079F04
		internal static string MakeValidTypeNameFromString(string s)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < s.Length; i++)
			{
				if (i == 0 && char.IsDigit(s[0]))
				{
					stringBuilder.Append('_');
				}
				if (char.IsLetterOrDigit(s[i]))
				{
					stringBuilder.Append(s[i]);
				}
				else
				{
					stringBuilder.Append('_');
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x0007BD70 File Offset: 0x00079F70
		internal static string GetNamespaceAndTypeNameFromVirtualPath(VirtualPath virtualPath, int chunksToIgnore, out string typeName)
		{
			string fileName = virtualPath.FileName;
			string[] array = fileName.Split(new char[]
			{
				'.'
			});
			int num = array.Length - chunksToIgnore;
			if (Util.IsWhiteSpaceString(array[num - 1]))
			{
				throw new HttpException(SR.GetString("Unsupported_filename", new object[]
				{
					fileName
				}));
			}
			typeName = Util.MakeValidTypeNameFromString(array[num - 1]);
			for (int i = 0; i < num - 1; i++)
			{
				if (Util.IsWhiteSpaceString(array[i]))
				{
					throw new HttpException(SR.GetString("Unsupported_filename", new object[]
					{
						fileName
					}));
				}
				array[i] = Util.MakeValidTypeNameFromString(array[i]);
			}
			return string.Join(".", array, 0, num - 1);
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x0007BE1C File Offset: 0x0007A01C
		internal static string GetNamespaceFromVirtualPath(VirtualPath virtualPath)
		{
			string text;
			return Util.GetNamespaceAndTypeNameFromVirtualPath(virtualPath, 1, out text);
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x0007BE34 File Offset: 0x0007A034
		internal static string FilePathFromFileUrl(string url)
		{
			Uri uri = new Uri(url);
			string localPath = uri.LocalPath;
			return HttpUtility.UrlDecode(localPath);
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x0007BE58 File Offset: 0x0007A058
		internal static bool IsCultureName(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return false;
			}
			foreach (string s2 in Util.s_invalidCultureNames)
			{
				if (StringUtil.EqualsIgnoreCase(s2, s))
				{
					return false;
				}
			}
			CultureInfo cultureInfo = null;
			try
			{
				cultureInfo = HttpServerUtility.CreateReadOnlyCultureInfo(s);
			}
			catch
			{
			}
			return cultureInfo != null;
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x0007BEB4 File Offset: 0x0007A0B4
		internal static string GetCultureName(string virtualPath)
		{
			if (virtualPath == null)
			{
				return null;
			}
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(virtualPath);
			if (fileNameWithoutExtension == null)
			{
				return null;
			}
			int num = fileNameWithoutExtension.LastIndexOf('.');
			if (num < 0)
			{
				return null;
			}
			string text = fileNameWithoutExtension.Substring(num + 1);
			if (!Util.IsCultureName(text))
			{
				return null;
			}
			return text;
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x0007BEF6 File Offset: 0x0007A0F6
		internal static bool TypeNameContainsAssembly(string typeName)
		{
			return Util.CommaIndexInTypeName(typeName) > 0;
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x0007BF04 File Offset: 0x0007A104
		internal static int CommaIndexInTypeName(string typeName)
		{
			int num = typeName.LastIndexOf(',');
			if (num < 0)
			{
				return -1;
			}
			int num2 = typeName.LastIndexOf(']');
			if (num2 > num)
			{
				return -1;
			}
			return typeName.IndexOf(',', num2 + 1);
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x0007BF3C File Offset: 0x0007A13C
		internal static string GetAssemblyPathFromType(Type t)
		{
			return Util.FilePathFromFileUrl(t.Assembly.EscapedCodeBase);
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x0007BF4E File Offset: 0x0007A14E
		internal static string GetAssemblySafePathFromType(Type t)
		{
			return HttpRuntime.GetSafePath(Util.GetAssemblyPathFromType(t));
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x0007BF5B File Offset: 0x0007A15B
		[FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.PathDiscovery)]
		internal static string GetAssemblyQualifiedTypeName(Type t)
		{
			if (t.Assembly.GlobalAssemblyCache)
			{
				return t.AssemblyQualifiedName;
			}
			return t.FullName + ", " + t.Assembly.GetName().Name;
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x0007BF91 File Offset: 0x0007A191
		internal static string GetAssemblyShortName(Assembly a)
		{
			InternalSecurityPermissions.Unrestricted.Assert();
			return a.GetName().Name;
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x0007BFA8 File Offset: 0x0007A1A8
		internal static bool IsLateBoundComClassicType(Type t)
		{
			return string.Compare(t.FullName, "System.__ComObject", StringComparison.Ordinal) == 0;
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x0007BFC0 File Offset: 0x0007A1C0
		[FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.PathDiscovery)]
		internal static string GetAssemblyCodeBase(Assembly assembly)
		{
			string location = assembly.Location;
			if (string.IsNullOrEmpty(location))
			{
				return null;
			}
			return location;
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x0007BFE0 File Offset: 0x0007A1E0
		internal static void AddAssemblyToStringCollection(Assembly assembly, StringCollection toList)
		{
			string value = null;
			if (BuildManagerHost.InClientBuildManager && !MultiTargetingUtil.IsTargetFramework20 && !MultiTargetingUtil.IsTargetFramework35 && assembly.FullName == typeof(string).Assembly.FullName)
			{
				return;
			}
			if (!MultiTargetingUtil.EnableReferenceAssemblyResolution)
			{
				value = Util.GetAssemblyCodeBase(assembly);
			}
			else
			{
				ReferenceAssemblyType pathToReferenceAssembly = AssemblyResolver.GetPathToReferenceAssembly(assembly, out value);
				if (pathToReferenceAssembly == ReferenceAssemblyType.FrameworkAssemblyOnlyPresentInHigherVersion)
				{
					return;
				}
			}
			if (!toList.Contains(value))
			{
				toList.Add(value);
			}
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x0007C058 File Offset: 0x0007A258
		internal static void AddAssembliesToStringCollection(ICollection fromList, StringCollection toList)
		{
			if (fromList == null || toList == null)
			{
				return;
			}
			foreach (object obj in fromList)
			{
				Assembly assembly = (Assembly)obj;
				Util.AddAssemblyToStringCollection(assembly, toList);
			}
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x0007C0B4 File Offset: 0x0007A2B4
		internal static AssemblySet GetReferencedAssemblies(Assembly a)
		{
			AssemblySet assemblySet = new AssemblySet();
			AssemblyName[] referencedAssemblies = a.GetReferencedAssemblies();
			foreach (AssemblyName assemblyRef in referencedAssemblies)
			{
				Assembly assembly = Assembly.Load(assemblyRef);
				if (!(assembly == typeof(string).Assembly))
				{
					assemblySet.Add(assembly);
				}
			}
			return assemblySet;
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x0007C10D File Offset: 0x0007A30D
		internal static string GetAssemblyNameFromFileName(string fileName)
		{
			if (StringUtil.EqualsIgnoreCase(Path.GetExtension(fileName), ".dll"))
			{
				return fileName.Substring(0, fileName.Length - 4);
			}
			return fileName;
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x0007C134 File Offset: 0x0007A334
		[ReflectionPermission(SecurityAction.Assert, Flags = ReflectionPermissionFlag.MemberAccess)]
		internal static Type GetTypeFromAssemblies(IEnumerable assemblies, string typeName, bool ignoreCase)
		{
			if (assemblies == null)
			{
				return null;
			}
			Type type = null;
			foreach (object obj in assemblies)
			{
				Assembly assembly = (Assembly)obj;
				Type type2 = assembly.GetType(typeName, false, ignoreCase);
				if (!(type2 == null))
				{
					if (type != null && type2 != type)
					{
						throw new HttpException(SR.GetString("Ambiguous_type", new object[]
						{
							typeName,
							Util.GetAssemblySafePathFromType(type),
							Util.GetAssemblySafePathFromType(type2)
						}));
					}
					type = type2;
				}
			}
			return type;
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x0007C1E0 File Offset: 0x0007A3E0
		internal static string GetCurrentAccountName()
		{
			string result;
			try
			{
				result = HttpApplication.GetCurrentWindowsIdentityWithAssert().Name;
			}
			catch
			{
				result = "?";
			}
			return result;
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x0007C214 File Offset: 0x0007A414
		internal static string GetUrlWithApplicationPath(HttpContextBase context, string url)
		{
			string text = context.Request.ApplicationPath ?? string.Empty;
			if (!text.EndsWith("/", StringComparison.OrdinalIgnoreCase))
			{
				text += "/";
			}
			return context.Response.ApplyAppPathModifier(text + url);
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x0007C262 File Offset: 0x0007A462
		internal static string QuoteJScriptString(string value)
		{
			return Util.QuoteJScriptString(value, false);
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x0007C26C File Offset: 0x0007A46C
		internal static string QuoteJScriptString(string value, bool forUrl)
		{
			StringBuilder stringBuilder = null;
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			int startIndex = 0;
			int num = 0;
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				if (c <= '"')
				{
					switch (c)
					{
					case '\t':
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(value.Length + 5);
						}
						if (num > 0)
						{
							stringBuilder.Append(value, startIndex, num);
						}
						stringBuilder.Append("\\t");
						startIndex = i + 1;
						num = 0;
						break;
					case '\n':
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(value.Length + 5);
						}
						if (num > 0)
						{
							stringBuilder.Append(value, startIndex, num);
						}
						stringBuilder.Append("\\n");
						startIndex = i + 1;
						num = 0;
						break;
					case '\v':
					case '\f':
						goto IL_1EA;
					case '\r':
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(value.Length + 5);
						}
						if (num > 0)
						{
							stringBuilder.Append(value, startIndex, num);
						}
						stringBuilder.Append("\\r");
						startIndex = i + 1;
						num = 0;
						break;
					default:
						if (c != '"')
						{
							goto IL_1EA;
						}
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(value.Length + 5);
						}
						if (num > 0)
						{
							stringBuilder.Append(value, startIndex, num);
						}
						stringBuilder.Append("\\\"");
						startIndex = i + 1;
						num = 0;
						break;
					}
				}
				else if (c != '%')
				{
					if (c != '\'')
					{
						if (c != '\\')
						{
							goto IL_1EA;
						}
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(value.Length + 5);
						}
						if (num > 0)
						{
							stringBuilder.Append(value, startIndex, num);
						}
						stringBuilder.Append("\\\\");
						startIndex = i + 1;
						num = 0;
					}
					else
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(value.Length + 5);
						}
						if (num > 0)
						{
							stringBuilder.Append(value, startIndex, num);
						}
						stringBuilder.Append("\\'");
						startIndex = i + 1;
						num = 0;
					}
				}
				else
				{
					if (!forUrl)
					{
						goto IL_1EA;
					}
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(value.Length + 6);
					}
					if (num > 0)
					{
						stringBuilder.Append(value, startIndex, num);
					}
					stringBuilder.Append("%25");
					startIndex = i + 1;
					num = 0;
				}
				IL_1EE:
				i++;
				continue;
				IL_1EA:
				num++;
				goto IL_1EE;
			}
			if (stringBuilder == null)
			{
				return value;
			}
			if (num > 0)
			{
				stringBuilder.Append(value, startIndex, num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x0007C490 File Offset: 0x0007A690
		private static ArrayList GetSpecificCultures(string shortName)
		{
			CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < cultures.Length; i++)
			{
				if (StringUtil.StringStartsWith(cultures[i].Name, shortName))
				{
					arrayList.Add(cultures[i]);
				}
			}
			return arrayList;
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x0007C4D4 File Offset: 0x0007A6D4
		internal static string GetSpecificCulturesFormattedList(CultureInfo cultureInfo)
		{
			ArrayList specificCultures = Util.GetSpecificCultures(cultureInfo.Name);
			string text = null;
			foreach (object obj in specificCultures)
			{
				CultureInfo cultureInfo2 = (CultureInfo)obj;
				if (text == null)
				{
					text = cultureInfo2.Name;
				}
				else
				{
					text = text + ", " + cultureInfo2.Name;
				}
			}
			return text;
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x0007C554 File Offset: 0x0007A754
		internal static string GetClientValidateEvent(string validationGroup)
		{
			if (validationGroup == null)
			{
				validationGroup = string.Empty;
			}
			return "if (typeof(Page_ClientValidate) == 'function') Page_ClientValidate('" + validationGroup + "'); ";
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x0007C570 File Offset: 0x0007A770
		internal static string GetClientValidatedPostback(Control control, string validationGroup)
		{
			return Util.GetClientValidatedPostback(control, validationGroup, string.Empty);
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x0007C580 File Offset: 0x0007A780
		internal static string GetClientValidatedPostback(Control control, string validationGroup, string argument)
		{
			string postBackEventReference = control.Page.ClientScript.GetPostBackEventReference(control, argument, true);
			return Util.GetClientValidateEvent(validationGroup) + postBackEventReference;
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x0007C5B0 File Offset: 0x0007A7B0
		internal static void WriteOnClickAttribute(HtmlTextWriter writer, HtmlControl control, bool submitsAutomatically, bool submitsProgramatically, bool causesValidation, string validationGroup)
		{
			AttributeCollection attributes = control.Attributes;
			string text = null;
			if (submitsAutomatically)
			{
				if (causesValidation)
				{
					text = Util.GetClientValidateEvent(validationGroup);
				}
				control.Page.ClientScript.RegisterForEventValidation(control.UniqueID);
			}
			else if (submitsProgramatically)
			{
				if (causesValidation)
				{
					text = Util.GetClientValidatedPostback(control, validationGroup);
				}
				else
				{
					text = control.Page.ClientScript.GetPostBackEventReference(control, string.Empty, true);
				}
			}
			else
			{
				control.Page.ClientScript.RegisterForEventValidation(control.UniqueID);
			}
			if (text != null)
			{
				string text2 = attributes["onclick"];
				if (text2 != null)
				{
					attributes.Remove("onclick");
					writer.WriteAttribute("onclick", text2 + " " + text);
					return;
				}
				writer.WriteAttribute("onclick", text);
			}
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x0007C670 File Offset: 0x0007A870
		internal static string EnsureEndWithSemiColon(string value)
		{
			if (value != null)
			{
				int length = value.Length;
				if (length > 0 && value[length - 1] != ';')
				{
					return value + ";";
				}
			}
			return value;
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x0007C6A5 File Offset: 0x0007A8A5
		internal static string MergeScript(string firstScript, string secondScript)
		{
			if (!string.IsNullOrEmpty(firstScript))
			{
				return firstScript + secondScript;
			}
			if (secondScript.TrimStart(new char[0]).StartsWith("javascript:", StringComparison.Ordinal))
			{
				return secondScript;
			}
			return "javascript:" + secondScript;
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x0007C6DD File Offset: 0x0007A8DD
		internal static bool IsUserAllowedToPath(HttpContext context, VirtualPath virtualPath)
		{
			if (FileAuthorizationModule.IsWindowsIdentity(context))
			{
				if (HttpRuntime.IsFullTrust)
				{
					if (!Util.IsUserAllowedToPathWithNoAssert(context, virtualPath))
					{
						return false;
					}
				}
				else if (!Util.IsUserAllowedToPathWithAssert(context, virtualPath))
				{
					return false;
				}
			}
			return UrlAuthorizationModule.IsUserAllowedToPath(context, virtualPath);
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x0007C70B File Offset: 0x0007A90B
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private static bool IsUserAllowedToPathWithAssert(HttpContext context, VirtualPath virtualPath)
		{
			return Util.IsUserAllowedToPathWithNoAssert(context, virtualPath);
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x0007C714 File Offset: 0x0007A914
		private static bool IsUserAllowedToPathWithNoAssert(HttpContext context, VirtualPath virtualPath)
		{
			return FileAuthorizationModule.IsUserAllowedToPath(context, virtualPath);
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x0007C720 File Offset: 0x0007A920
		// Note: this type is marked as 'beforefieldinit'.
		static Util()
		{
			char[] array;
			if (!AppSettings.FileNameUtilUseLegacyInvalidChars)
			{
				array = Path.GetInvalidFileNameChars();
			}
			else
			{
				RuntimeHelpers.InitializeArray(array = new char[5], fieldof(<PrivateImplementationDetails>.B35CFDC43A69A4A7061FC6EDB93C7A305E1BC0660E4BC31A86514D9559807816).FieldHandle);
			}
			Util.invalidFileNameChars = array;
		}

		// Token: 0x04001D76 RID: 7542
		private static string[] s_invalidCultureNames = new string[]
		{
			"aspx",
			"ascx",
			"master"
		};

		// Token: 0x04001D77 RID: 7543
		private static char[] invalidFileNameChars;

		// Token: 0x04001D78 RID: 7544
		internal const char DeviceFilterSeparator = ':';

		// Token: 0x04001D79 RID: 7545
		internal const string XmlnsAttribute = "xmlns:";
	}
}
