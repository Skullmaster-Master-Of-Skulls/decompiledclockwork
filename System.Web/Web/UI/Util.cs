using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000483 RID: 1155
	internal static class Util
	{
		// Token: 0x06003608 RID: 13832 RVA: 0x000E99EC File Offset: 0x000E89EC
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		internal static string SerializeWithAssert(IStateFormatter formatter, object stateGraph)
		{
			return formatter.Serialize(stateGraph);
		}

		// Token: 0x06003609 RID: 13833 RVA: 0x000E99F5 File Offset: 0x000E89F5
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		internal static object DeserializeWithAssert(IStateFormatter formatter, string serializedState)
		{
			return formatter.Deserialize(serializedState);
		}

		// Token: 0x0600360A RID: 13834 RVA: 0x000E99FE File Offset: 0x000E89FE
		internal static bool CanConvertToFrom(TypeConverter converter, Type type)
		{
			return converter != null && converter.CanConvertTo(type) && converter.CanConvertFrom(type) && !(converter is ReferenceConverter);
		}

		// Token: 0x0600360B RID: 13835 RVA: 0x000E9A24 File Offset: 0x000E8A24
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

		// Token: 0x0600360C RID: 13836 RVA: 0x000E9A7C File Offset: 0x000E8A7C
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

		// Token: 0x0600360D RID: 13837 RVA: 0x000E9D58 File Offset: 0x000E8D58
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

		// Token: 0x0600360E RID: 13838 RVA: 0x000E9D84 File Offset: 0x000E8D84
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

		// Token: 0x0600360F RID: 13839 RVA: 0x000E9DF4 File Offset: 0x000E8DF4
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

		// Token: 0x06003610 RID: 13840 RVA: 0x000E9E1C File Offset: 0x000E8E1C
		internal static void DeleteFileIfExistsNoException(string path)
		{
			if (File.Exists(path))
			{
				Util.DeleteFileNoException(path);
			}
		}

		// Token: 0x06003611 RID: 13841 RVA: 0x000E9E2C File Offset: 0x000E8E2C
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
				result = (fileSystemEntries.Length > 0);
			}
			catch
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06003612 RID: 13842 RVA: 0x000E9E6C File Offset: 0x000E8E6C
		internal static bool IsValidFileName(string fileName)
		{
			return !(fileName == ".") && !(fileName == "..") && fileName.IndexOfAny(Util.invalidFileNameChars) < 0;
		}

		// Token: 0x06003613 RID: 13843 RVA: 0x000E9E9C File Offset: 0x000E8E9C
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

		// Token: 0x06003614 RID: 13844 RVA: 0x000E9ED8 File Offset: 0x000E8ED8
		internal static bool HasWriteAccessToDirectory(string dir)
		{
			if (!Directory.Exists(dir))
			{
				return false;
			}
			string path = Path.Combine(dir, "~AspAccessCheck_" + HostingEnvironment.AppDomainUniqueInteger.ToString("x", CultureInfo.InvariantCulture) + ".tmp");
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

		// Token: 0x06003615 RID: 13845 RVA: 0x000E9F60 File Offset: 0x000E8F60
		internal static VirtualPath GetScriptLocation()
		{
			string text = (string)RuntimeConfig.GetRootWebConfig().WebControls["clientScriptsLocation"];
			if (text.IndexOf("{0}", StringComparison.Ordinal) >= 0)
			{
				string text2 = "system_web";
				string text3 = VersionInfo.EngineVersion.Substring(0, VersionInfo.EngineVersion.LastIndexOf('.')).Replace('.', '_');
				text = string.Format(CultureInfo.InvariantCulture, text, new object[]
				{
					text2,
					text3
				});
			}
			return VirtualPath.Create(text);
		}

		// Token: 0x06003616 RID: 13846 RVA: 0x000E9FE0 File Offset: 0x000E8FE0
		internal static StreamReader ReaderFromStream(Stream stream, VirtualPath configPath)
		{
			Encoding encodingFromConfigPath = Util.GetEncodingFromConfigPath(configPath);
			return new StreamReader(stream, encodingFromConfigPath, true, 4096);
		}

		// Token: 0x06003617 RID: 13847 RVA: 0x000EA004 File Offset: 0x000E9004
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

		// Token: 0x06003618 RID: 13848 RVA: 0x000EA044 File Offset: 0x000E9044
		internal static string StringFromFile(string path)
		{
			Encoding @default = Encoding.Default;
			return Util.StringFromFile(path, ref @default);
		}

		// Token: 0x06003619 RID: 13849 RVA: 0x000EA060 File Offset: 0x000E9060
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

		// Token: 0x0600361A RID: 13850 RVA: 0x000EA0A8 File Offset: 0x000E90A8
		internal static string StringFromFileIfExists(string path)
		{
			if (!File.Exists(path))
			{
				return null;
			}
			return Util.StringFromFile(path);
		}

		// Token: 0x0600361B RID: 13851 RVA: 0x000EA0BC File Offset: 0x000E90BC
		internal static void RemoveOrRenameFile(string filename)
		{
			FileInfo f = new FileInfo(filename);
			Util.RemoveOrRenameFile(f);
		}

		// Token: 0x0600361C RID: 13852 RVA: 0x000EA0D8 File Offset: 0x000E90D8
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

		// Token: 0x0600361D RID: 13853 RVA: 0x000EA170 File Offset: 0x000E9170
		internal static void ClearReadOnlyAttribute(string path)
		{
			FileAttributes attributes = File.GetAttributes(path);
			if ((attributes & FileAttributes.ReadOnly) != (FileAttributes)0)
			{
				File.SetAttributes(path, attributes & ~FileAttributes.ReadOnly);
			}
		}

		// Token: 0x0600361E RID: 13854 RVA: 0x000EA194 File Offset: 0x000E9194
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

		// Token: 0x0600361F RID: 13855 RVA: 0x000EA1D0 File Offset: 0x000E91D0
		internal static bool VirtualFileExistsWithAssert(VirtualPath virtualPath)
		{
			string text = virtualPath.MapPathInternal();
			if (text != null)
			{
				InternalSecurityPermissions.PathDiscovery(text).Assert();
			}
			return virtualPath.FileExists();
		}

		// Token: 0x06003620 RID: 13856 RVA: 0x000EA1F8 File Offset: 0x000E91F8
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

		// Token: 0x06003621 RID: 13857 RVA: 0x000EA258 File Offset: 0x000E9258
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

		// Token: 0x06003622 RID: 13858 RVA: 0x000EA288 File Offset: 0x000E9288
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

		// Token: 0x06003623 RID: 13859 RVA: 0x000EA2CC File Offset: 0x000E92CC
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

		// Token: 0x06003624 RID: 13860 RVA: 0x000EA30C File Offset: 0x000E930C
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

		// Token: 0x06003625 RID: 13861 RVA: 0x000EA354 File Offset: 0x000E9354
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

		// Token: 0x06003626 RID: 13862 RVA: 0x000EA388 File Offset: 0x000E9388
		internal static Type GetNonPrivateFieldType(Type classType, string fieldName)
		{
			FieldInfo field = classType.GetField(fieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (field == null || field.IsPrivate)
			{
				return null;
			}
			return field.FieldType;
		}

		// Token: 0x06003627 RID: 13863 RVA: 0x000EA3B4 File Offset: 0x000E93B4
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

		// Token: 0x06003628 RID: 13864 RVA: 0x000EA414 File Offset: 0x000E9414
		private static string FirstDictionaryKey(IDictionary dict)
		{
			IDictionaryEnumerator enumerator = dict.GetEnumerator();
			enumerator.MoveNext();
			return (string)enumerator.Key;
		}

		// Token: 0x06003629 RID: 13865 RVA: 0x000EA43C File Offset: 0x000E943C
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

		// Token: 0x0600362A RID: 13866 RVA: 0x000EA468 File Offset: 0x000E9468
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

		// Token: 0x0600362B RID: 13867 RVA: 0x000EA4A8 File Offset: 0x000E94A8
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

		// Token: 0x0600362C RID: 13868 RVA: 0x000EA4E4 File Offset: 0x000E94E4
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

		// Token: 0x0600362D RID: 13869 RVA: 0x000EA516 File Offset: 0x000E9516
		internal static string GetAndRemoveNonEmptyAttribute(IDictionary directives, string key)
		{
			return Util.GetAndRemoveNonEmptyAttribute(directives, key, false);
		}

		// Token: 0x0600362E RID: 13870 RVA: 0x000EA520 File Offset: 0x000E9520
		internal static VirtualPath GetAndRemoveVirtualPathAttribute(IDictionary directives, string key)
		{
			return Util.GetAndRemoveVirtualPathAttribute(directives, key, false);
		}

		// Token: 0x0600362F RID: 13871 RVA: 0x000EA52C File Offset: 0x000E952C
		internal static VirtualPath GetAndRemoveVirtualPathAttribute(IDictionary directives, string key, bool required)
		{
			string andRemoveNonEmptyAttribute = Util.GetAndRemoveNonEmptyAttribute(directives, key, required);
			if (andRemoveNonEmptyAttribute == null)
			{
				return null;
			}
			return VirtualPath.Create(andRemoveNonEmptyAttribute);
		}

		// Token: 0x06003630 RID: 13872 RVA: 0x000EA550 File Offset: 0x000E9550
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
				result = array[0];
				propName = array[1];
			}
			return result;
		}

		// Token: 0x06003631 RID: 13873 RVA: 0x000EA5C5 File Offset: 0x000E95C5
		public static string CreateFilteredName(string deviceName, string name)
		{
			if (deviceName.Length > 0)
			{
				return deviceName + ':' + name;
			}
			return name;
		}

		// Token: 0x06003632 RID: 13874 RVA: 0x000EA5E0 File Offset: 0x000E95E0
		internal static string GetAndRemoveRequiredAttribute(IDictionary directives, string key)
		{
			return Util.GetAndRemoveNonEmptyAttribute(directives, key, true);
		}

		// Token: 0x06003633 RID: 13875 RVA: 0x000EA5EC File Offset: 0x000E95EC
		internal static string GetAndRemoveNonEmptyNoSpaceAttribute(IDictionary directives, string key, bool required)
		{
			string andRemoveNonEmptyAttribute = Util.GetAndRemoveNonEmptyAttribute(directives, key, required);
			if (andRemoveNonEmptyAttribute == null)
			{
				return null;
			}
			return Util.GetNonEmptyNoSpaceAttribute(key, andRemoveNonEmptyAttribute);
		}

		// Token: 0x06003634 RID: 13876 RVA: 0x000EA60E File Offset: 0x000E960E
		internal static string GetAndRemoveNonEmptyNoSpaceAttribute(IDictionary directives, string key)
		{
			return Util.GetAndRemoveNonEmptyNoSpaceAttribute(directives, key, false);
		}

		// Token: 0x06003635 RID: 13877 RVA: 0x000EA618 File Offset: 0x000E9618
		internal static string GetNonEmptyNoSpaceAttribute(string name, string value)
		{
			value = Util.GetNonEmptyAttribute(name, value);
			return Util.GetNoSpaceAttribute(name, value);
		}

		// Token: 0x06003636 RID: 13878 RVA: 0x000EA62C File Offset: 0x000E962C
		internal static string GetAndRemoveNonEmptyIdentifierAttribute(IDictionary directives, string key, bool required)
		{
			string andRemoveNonEmptyNoSpaceAttribute = Util.GetAndRemoveNonEmptyNoSpaceAttribute(directives, key, required);
			if (andRemoveNonEmptyNoSpaceAttribute == null)
			{
				return null;
			}
			return Util.GetNonEmptyIdentifierAttribute(key, andRemoveNonEmptyNoSpaceAttribute);
		}

		// Token: 0x06003637 RID: 13879 RVA: 0x000EA650 File Offset: 0x000E9650
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

		// Token: 0x06003638 RID: 13880 RVA: 0x000EA690 File Offset: 0x000E9690
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

		// Token: 0x06003639 RID: 13881 RVA: 0x000EA71F File Offset: 0x000E971F
		internal static void CheckUnknownDirectiveAttributes(string directiveName, IDictionary directive)
		{
			Util.CheckUnknownDirectiveAttributes(directiveName, directive, "Attr_not_supported_in_directive");
		}

		// Token: 0x0600363A RID: 13882 RVA: 0x000EA730 File Offset: 0x000E9730
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

		// Token: 0x0600363B RID: 13883 RVA: 0x000EA768 File Offset: 0x000E9768
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

		// Token: 0x0600363C RID: 13884 RVA: 0x000EA78C File Offset: 0x000E978C
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

		// Token: 0x0600363D RID: 13885 RVA: 0x000EA7D0 File Offset: 0x000E97D0
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

		// Token: 0x0600363E RID: 13886 RVA: 0x000EA7F4 File Offset: 0x000E97F4
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

		// Token: 0x0600363F RID: 13887 RVA: 0x000EA860 File Offset: 0x000E9860
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

		// Token: 0x06003640 RID: 13888 RVA: 0x000EA8D8 File Offset: 0x000E98D8
		internal static object GetAndRemoveEnumAttribute(IDictionary directives, Type enumType, string key)
		{
			string andRemove = Util.GetAndRemove(directives, key);
			if (andRemove == null)
			{
				return null;
			}
			return Util.GetEnumAttribute(key, andRemove, enumType);
		}

		// Token: 0x06003641 RID: 13889 RVA: 0x000EA8FA File Offset: 0x000E98FA
		internal static object GetEnumAttribute(string name, string value, Type enumType)
		{
			return Util.GetEnumAttribute(name, value, enumType, false);
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x000EA908 File Offset: 0x000E9908
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

		// Token: 0x06003643 RID: 13891 RVA: 0x000EA9E0 File Offset: 0x000E99E0
		internal static bool IsWhiteSpaceString(string s)
		{
			return s.Trim().Length == 0;
		}

		// Token: 0x06003644 RID: 13892 RVA: 0x000EA9F0 File Offset: 0x000E99F0
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

		// Token: 0x06003645 RID: 13893 RVA: 0x000EAA24 File Offset: 0x000E9A24
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

		// Token: 0x06003646 RID: 13894 RVA: 0x000EAA53 File Offset: 0x000E9A53
		internal static bool IsTrueString(string s)
		{
			return s != null && StringUtil.EqualsIgnoreCase(s, "true");
		}

		// Token: 0x06003647 RID: 13895 RVA: 0x000EAA65 File Offset: 0x000E9A65
		internal static bool IsFalseString(string s)
		{
			return s != null && StringUtil.EqualsIgnoreCase(s, "false");
		}

		// Token: 0x06003648 RID: 13896 RVA: 0x000EAA77 File Offset: 0x000E9A77
		internal static string GetStringFromBool(bool flag)
		{
			if (!flag)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x06003649 RID: 13897 RVA: 0x000EAA87 File Offset: 0x000E9A87
		internal static string MakeFullTypeName(string ns, string typeName)
		{
			if (string.IsNullOrEmpty(ns))
			{
				return typeName;
			}
			return ns + "." + typeName;
		}

		// Token: 0x0600364A RID: 13898 RVA: 0x000EAAA0 File Offset: 0x000E9AA0
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

		// Token: 0x0600364B RID: 13899 RVA: 0x000EAB0C File Offset: 0x000E9B0C
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

		// Token: 0x0600364C RID: 13900 RVA: 0x000EABC8 File Offset: 0x000E9BC8
		internal static string GetNamespaceFromVirtualPath(VirtualPath virtualPath)
		{
			string text;
			return Util.GetNamespaceAndTypeNameFromVirtualPath(virtualPath, 1, out text);
		}

		// Token: 0x0600364D RID: 13901 RVA: 0x000EABE0 File Offset: 0x000E9BE0
		internal static string FilePathFromFileUrl(string url)
		{
			Uri uri = new Uri(url);
			string localPath = uri.LocalPath;
			return HttpUtility.UrlDecode(localPath);
		}

		// Token: 0x0600364E RID: 13902 RVA: 0x000EAC04 File Offset: 0x000E9C04
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

		// Token: 0x0600364F RID: 13903 RVA: 0x000EAC6C File Offset: 0x000E9C6C
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

		// Token: 0x06003650 RID: 13904 RVA: 0x000EACAE File Offset: 0x000E9CAE
		internal static bool TypeNameContainsAssembly(string typeName)
		{
			return Util.CommaIndexInTypeName(typeName) > 0;
		}

		// Token: 0x06003651 RID: 13905 RVA: 0x000EACBC File Offset: 0x000E9CBC
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

		// Token: 0x06003652 RID: 13906 RVA: 0x000EACF4 File Offset: 0x000E9CF4
		internal static string GetAssemblyPathFromType(Type t)
		{
			return Util.FilePathFromFileUrl(t.Assembly.EscapedCodeBase);
		}

		// Token: 0x06003653 RID: 13907 RVA: 0x000EAD06 File Offset: 0x000E9D06
		internal static string GetAssemblySafePathFromType(Type t)
		{
			return HttpRuntime.GetSafePath(Util.GetAssemblyPathFromType(t));
		}

		// Token: 0x06003654 RID: 13908 RVA: 0x000EAD13 File Offset: 0x000E9D13
		internal static string GetAssemblyQualifiedTypeName(Type t)
		{
			if (t.Assembly.GlobalAssemblyCache)
			{
				return t.AssemblyQualifiedName;
			}
			return t.FullName + ", " + t.Assembly.GetName().Name;
		}

		// Token: 0x06003655 RID: 13909 RVA: 0x000EAD49 File Offset: 0x000E9D49
		internal static string GetAssemblyShortName(Assembly a)
		{
			InternalSecurityPermissions.Unrestricted.Assert();
			return a.GetName().Name;
		}

		// Token: 0x06003656 RID: 13910 RVA: 0x000EAD60 File Offset: 0x000E9D60
		internal static bool IsLateBoundComClassicType(Type t)
		{
			return string.Compare(t.FullName, "System.__ComObject", StringComparison.Ordinal) == 0;
		}

		// Token: 0x06003657 RID: 13911 RVA: 0x000EAD78 File Offset: 0x000E9D78
		internal static string GetAssemblyCodeBase(Assembly assembly)
		{
			string location = assembly.Location;
			if (string.IsNullOrEmpty(location))
			{
				return null;
			}
			return location;
		}

		// Token: 0x06003658 RID: 13912 RVA: 0x000EAD98 File Offset: 0x000E9D98
		internal static void AddAssemblyToStringCollection(Assembly assembly, StringCollection toList)
		{
			string assemblyCodeBase = Util.GetAssemblyCodeBase(assembly);
			if (!toList.Contains(assemblyCodeBase))
			{
				toList.Add(assemblyCodeBase);
			}
		}

		// Token: 0x06003659 RID: 13913 RVA: 0x000EADC0 File Offset: 0x000E9DC0
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

		// Token: 0x0600365A RID: 13914 RVA: 0x000EAE1C File Offset: 0x000E9E1C
		internal static AssemblySet GetReferencedAssemblies(Assembly a)
		{
			AssemblySet assemblySet = new AssemblySet();
			AssemblyName[] referencedAssemblies = a.GetReferencedAssemblies();
			foreach (AssemblyName assemblyRef in referencedAssemblies)
			{
				Assembly assembly = Assembly.Load(assemblyRef);
				if (assembly != typeof(string).Assembly)
				{
					assemblySet.Add(assembly);
				}
			}
			return assemblySet;
		}

		// Token: 0x0600365B RID: 13915 RVA: 0x000EAE73 File Offset: 0x000E9E73
		internal static string GetAssemblyNameFromFileName(string fileName)
		{
			if (StringUtil.EqualsIgnoreCase(Path.GetExtension(fileName), ".dll"))
			{
				return fileName.Substring(0, fileName.Length - 4);
			}
			return fileName;
		}

		// Token: 0x0600365C RID: 13916 RVA: 0x000EAE98 File Offset: 0x000E9E98
		[ReflectionPermission(SecurityAction.Assert, Flags = ReflectionPermissionFlag.MemberAccess)]
		internal static Type GetTypeFromAssemblies(ICollection assemblies, string typeName, bool ignoreCase)
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
				if (type2 != null)
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

		// Token: 0x0600365D RID: 13917 RVA: 0x000EAF3C File Offset: 0x000E9F3C
		internal static string GetCurrentAccountName()
		{
			string result;
			try
			{
				result = WindowsIdentity.GetCurrent().Name;
			}
			catch
			{
				result = "?";
			}
			return result;
		}

		// Token: 0x0600365E RID: 13918 RVA: 0x000EAF70 File Offset: 0x000E9F70
		internal static string QuoteJScriptString(string value)
		{
			return Util.QuoteJScriptString(value, false);
		}

		// Token: 0x0600365F RID: 13919 RVA: 0x000EAF7C File Offset: 0x000E9F7C
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
						goto IL_1EE;
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
							goto IL_1EE;
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
				else
				{
					switch (c)
					{
					case '%':
						if (!forUrl)
						{
							goto IL_1EE;
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
						break;
					case '&':
						goto IL_1EE;
					case '\'':
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
						break;
					default:
						if (c != '\\')
						{
							goto IL_1EE;
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
						break;
					}
				}
				IL_1F2:
				i++;
				continue;
				IL_1EE:
				num++;
				goto IL_1F2;
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

		// Token: 0x06003660 RID: 13920 RVA: 0x000EB1A4 File Offset: 0x000EA1A4
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

		// Token: 0x06003661 RID: 13921 RVA: 0x000EB1E8 File Offset: 0x000EA1E8
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

		// Token: 0x06003662 RID: 13922 RVA: 0x000EB268 File Offset: 0x000EA268
		internal static string GetClientValidateEvent(string validationGroup)
		{
			if (validationGroup == null)
			{
				validationGroup = string.Empty;
			}
			return "if (typeof(Page_ClientValidate) == 'function') Page_ClientValidate('" + validationGroup + "'); ";
		}

		// Token: 0x06003663 RID: 13923 RVA: 0x000EB284 File Offset: 0x000EA284
		internal static string GetClientValidatedPostback(Control control, string validationGroup)
		{
			return Util.GetClientValidatedPostback(control, validationGroup, string.Empty);
		}

		// Token: 0x06003664 RID: 13924 RVA: 0x000EB294 File Offset: 0x000EA294
		internal static string GetClientValidatedPostback(Control control, string validationGroup, string argument)
		{
			string postBackEventReference = control.Page.ClientScript.GetPostBackEventReference(control, argument, true);
			return Util.GetClientValidateEvent(validationGroup) + postBackEventReference;
		}

		// Token: 0x06003665 RID: 13925 RVA: 0x000EB2C4 File Offset: 0x000EA2C4
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

		// Token: 0x06003666 RID: 13926 RVA: 0x000EB384 File Offset: 0x000EA384
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

		// Token: 0x06003667 RID: 13927 RVA: 0x000EB3B9 File Offset: 0x000EA3B9
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

		// Token: 0x06003668 RID: 13928 RVA: 0x000EB3F1 File Offset: 0x000EA3F1
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

		// Token: 0x06003669 RID: 13929 RVA: 0x000EB41F File Offset: 0x000EA41F
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private static bool IsUserAllowedToPathWithAssert(HttpContext context, VirtualPath virtualPath)
		{
			return Util.IsUserAllowedToPathWithNoAssert(context, virtualPath);
		}

		// Token: 0x0600366A RID: 13930 RVA: 0x000EB428 File Offset: 0x000EA428
		private static bool IsUserAllowedToPathWithNoAssert(HttpContext context, VirtualPath virtualPath)
		{
			return FileAuthorizationModule.IsUserAllowedToPath(context, virtualPath);
		}

		// Token: 0x0400257E RID: 9598
		internal const char DeviceFilterSeparator = ':';

		// Token: 0x0400257F RID: 9599
		internal const string XmlnsAttribute = "xmlns:";

		// Token: 0x04002580 RID: 9600
		private static string[] s_invalidCultureNames = new string[]
		{
			"aspx",
			"ascx",
			"master"
		};

		// Token: 0x04002581 RID: 9601
		private static char[] invalidFileNameChars = new char[]
		{
			'/',
			'\\',
			'?',
			'*',
			':'
		};
	}
}
