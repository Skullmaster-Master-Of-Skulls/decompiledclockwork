using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Handlers;
using System.Web.Resources;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x0200007B RID: 123
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public sealed class ScriptResourceAttribute : Attribute
	{
		// Token: 0x0600053E RID: 1342 RVA: 0x00018FC9 File Offset: 0x000171C9
		public ScriptResourceAttribute(string scriptName) : this(scriptName, null, null)
		{
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00018FD4 File Offset: 0x000171D4
		public ScriptResourceAttribute(string scriptName, string stringResourceName, string stringResourceClientTypeName)
		{
			if (string.IsNullOrEmpty(scriptName))
			{
				throw new ArgumentException(AtlasWeb.Common_NullOrEmpty, "scriptName");
			}
			this._scriptName = scriptName;
			this._stringResourceName = stringResourceName;
			this._stringResourceClientTypeName = stringResourceClientTypeName;
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x00019009 File Offset: 0x00017209
		public string ScriptName
		{
			get
			{
				return this._scriptName;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x00019011 File Offset: 0x00017211
		[Obsolete("This property is obsolete. Use StringResourceName instead.")]
		public string ScriptResourceName
		{
			get
			{
				return this.StringResourceName;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x00019019 File Offset: 0x00017219
		public string StringResourceClientTypeName
		{
			get
			{
				return this._stringResourceClientTypeName;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x00019021 File Offset: 0x00017221
		public string StringResourceName
		{
			get
			{
				return this._stringResourceName;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x00019029 File Offset: 0x00017229
		[Obsolete("This property is obsolete. Use StringResourceClientTypeName instead.")]
		public string TypeName
		{
			get
			{
				return this.StringResourceClientTypeName;
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00019034 File Offset: 0x00017234
		private static void AddResources(Dictionary<string, string> resources, ResourceManager resourceManager, ResourceSet neutralSet)
		{
			foreach (object obj in neutralSet)
			{
				string text = (string)((DictionaryEntry)obj).Key;
				string text2 = resourceManager.GetObject(text) as string;
				if (text2 != null)
				{
					resources[text] = text2;
				}
			}
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x000190AC File Offset: 0x000172AC
		private static Dictionary<string, string> CombineResources(ResourceManager resourceManager, ResourceSet neutralSet, ResourceManager releaseResourceManager, ResourceSet releaseNeutralSet)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.Ordinal);
			ScriptResourceAttribute.AddResources(dictionary, releaseResourceManager, releaseNeutralSet);
			ScriptResourceAttribute.AddResources(dictionary, resourceManager, neutralSet);
			return dictionary;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x000190D8 File Offset: 0x000172D8
		private static void CopyScriptToStringBuilderWithSubstitution(string content, Assembly assembly, bool zip, StringBuilder output)
		{
			MatchCollection matchCollection = ScriptResourceAttribute._webResourceRegEx.Matches(content);
			int num = 0;
			foreach (object obj in matchCollection)
			{
				Match match = (Match)obj;
				output.Append(content.Substring(num, match.Index - num));
				Group group = match.Groups["resourceName"];
				string value = group.Value;
				bool flag = string.Equals(match.Groups["resourceType"].Value, "ScriptResource", StringComparison.Ordinal);
				try
				{
					if (flag)
					{
						output.Append(ScriptResourceHandler.GetScriptResourceUrl(assembly, value, CultureInfo.CurrentUICulture, zip));
					}
					else
					{
						output.Append(AssemblyResourceLoader.GetWebResourceUrlInternal(assembly, value, false, true, null));
					}
				}
				catch (HttpException innerException)
				{
					throw new HttpException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ScriptResourceHandler_UnknownResource, new object[]
					{
						value
					}), innerException);
				}
				num = match.Index + match.Length;
			}
			output.Append(content.Substring(num, content.Length - num));
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00019214 File Offset: 0x00017414
		internal static ResourceManager GetResourceManager(string resourceName, Assembly assembly)
		{
			if (string.IsNullOrEmpty(resourceName))
			{
				return null;
			}
			return new ResourceManager(ScriptResourceAttribute.GetResourceName(resourceName), assembly);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0001922C File Offset: 0x0001742C
		private static string GetResourceName(string rawResourceName)
		{
			if (rawResourceName.EndsWith(".resources", StringComparison.OrdinalIgnoreCase))
			{
				return rawResourceName.Substring(0, rawResourceName.Length - 10);
			}
			return rawResourceName;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00019250 File Offset: 0x00017450
		internal static string GetScriptFromWebResourceInternal(Assembly assembly, string resourceName, CultureInfo culture, bool zip, out string contentType)
		{
			ScriptResourceInfo instance = ScriptResourceInfo.GetInstance(assembly, resourceName);
			ScriptResourceInfo scriptResourceInfo = null;
			if (resourceName.EndsWith(".debug.js", StringComparison.OrdinalIgnoreCase))
			{
				string resourceName2 = resourceName.Substring(0, resourceName.Length - 9) + ".js";
				scriptResourceInfo = ScriptResourceInfo.GetInstance(assembly, resourceName2);
			}
			if (instance == ScriptResourceInfo.Empty && (scriptResourceInfo == null || scriptResourceInfo == ScriptResourceInfo.Empty))
			{
				throw new HttpException(AtlasWeb.ScriptResourceHandler_InvalidRequest);
			}
			ResourceManager resourceManager = null;
			ResourceSet resourceSet = null;
			ResourceManager resourceManager2 = null;
			ResourceSet resourceSet2 = null;
			CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
			string result;
			try
			{
				Thread.CurrentThread.CurrentUICulture = culture;
				if (!string.IsNullOrEmpty(instance.ScriptResourceName))
				{
					resourceManager = ScriptResourceAttribute.GetResourceManager(instance.ScriptResourceName, assembly);
					resourceSet = resourceManager.GetResourceSet(CultureInfo.InvariantCulture, true, true);
				}
				if (scriptResourceInfo != null && !string.IsNullOrEmpty(scriptResourceInfo.ScriptResourceName))
				{
					resourceManager2 = ScriptResourceAttribute.GetResourceManager(scriptResourceInfo.ScriptResourceName, assembly);
					resourceSet2 = resourceManager2.GetResourceSet(CultureInfo.InvariantCulture, true, true);
				}
				if (scriptResourceInfo != null && !string.IsNullOrEmpty(scriptResourceInfo.ScriptResourceName) && !string.IsNullOrEmpty(instance.ScriptResourceName) && scriptResourceInfo.TypeName != instance.TypeName)
				{
					throw new HttpException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ScriptResourceHandler_TypeNameMismatch, new object[]
					{
						scriptResourceInfo.ScriptResourceName
					}));
				}
				StringBuilder stringBuilder = new StringBuilder();
				ScriptResourceAttribute.WriteScript(assembly, instance, scriptResourceInfo, resourceManager, resourceSet, resourceManager2, resourceSet2, zip, stringBuilder);
				contentType = instance.ContentType;
				result = stringBuilder.ToString();
			}
			finally
			{
				Thread.CurrentThread.CurrentUICulture = currentUICulture;
				if (resourceSet2 != null)
				{
					resourceSet2.Dispose();
				}
				if (resourceSet != null)
				{
					resourceSet.Dispose();
				}
			}
			return result;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x000193E4 File Offset: 0x000175E4
		private static void RegisterNamespace(StringBuilder builder, string typeName, bool isDebug)
		{
			int num = typeName.LastIndexOf('.');
			if (num != -1)
			{
				builder.Append("Type.registerNamespace('");
				builder.Append(typeName.Substring(0, num));
				builder.Append("');");
				if (isDebug)
				{
					builder.AppendLine();
				}
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00019430 File Offset: 0x00017630
		private static void WriteResource(StringBuilder builder, Dictionary<string, string> resources, bool isDebug)
		{
			bool flag = true;
			foreach (KeyValuePair<string, string> keyValuePair in resources)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					builder.Append(',');
				}
				if (isDebug)
				{
					builder.AppendLine();
				}
				builder.Append('"');
				builder.Append(HttpUtility.JavaScriptStringEncode(keyValuePair.Key));
				builder.Append("\":\"");
				builder.Append(HttpUtility.JavaScriptStringEncode(keyValuePair.Value));
				builder.Append('"');
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x000194D8 File Offset: 0x000176D8
		private static void WriteResource(StringBuilder builder, ResourceManager resourceManager, ResourceSet neutralSet, bool isDebug)
		{
			bool flag = true;
			foreach (object obj in neutralSet)
			{
				string text = (string)((DictionaryEntry)obj).Key;
				string text2 = resourceManager.GetObject(text) as string;
				if (text2 != null)
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						builder.Append(',');
					}
					if (isDebug)
					{
						builder.AppendLine();
					}
					builder.Append('"');
					builder.Append(HttpUtility.JavaScriptStringEncode(text));
					builder.Append("\":\"");
					builder.Append(HttpUtility.JavaScriptStringEncode(text2));
					builder.Append('"');
				}
			}
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x000195A0 File Offset: 0x000177A0
		private static void WriteResourceToStringBuilder(ScriptResourceInfo resourceInfo, ScriptResourceInfo releaseResourceInfo, ResourceManager resourceManager, ResourceSet neutralSet, ResourceManager releaseResourceManager, ResourceSet releaseNeutralSet, StringBuilder builder)
		{
			if (resourceManager != null || releaseResourceManager != null)
			{
				string typeName = resourceInfo.TypeName;
				if (string.IsNullOrEmpty(typeName))
				{
					typeName = releaseResourceInfo.TypeName;
				}
				ScriptResourceAttribute.WriteResources(builder, typeName, resourceManager, neutralSet, releaseResourceManager, releaseNeutralSet, resourceInfo.IsDebug);
			}
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x000195E0 File Offset: 0x000177E0
		private static void WriteResources(StringBuilder builder, string typeName, ResourceManager resourceManager, ResourceSet neutralSet, ResourceManager releaseResourceManager, ResourceSet releaseNeutralSet, bool isDebug)
		{
			builder.AppendLine();
			ScriptResourceAttribute.RegisterNamespace(builder, typeName, isDebug);
			builder.Append(typeName);
			builder.Append("={");
			if (resourceManager != null && releaseResourceManager != null)
			{
				ScriptResourceAttribute.WriteResource(builder, ScriptResourceAttribute.CombineResources(resourceManager, neutralSet, releaseResourceManager, releaseNeutralSet), isDebug);
			}
			else if (resourceManager != null)
			{
				ScriptResourceAttribute.WriteResource(builder, resourceManager, neutralSet, isDebug);
			}
			else if (releaseResourceManager != null)
			{
				ScriptResourceAttribute.WriteResource(builder, releaseResourceManager, releaseNeutralSet, isDebug);
			}
			if (isDebug)
			{
				builder.AppendLine();
				builder.AppendLine("};");
				return;
			}
			builder.Append("};");
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00019670 File Offset: 0x00017870
		[SecuritySafeCritical]
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private static void WriteScript(Assembly assembly, ScriptResourceInfo resourceInfo, ScriptResourceInfo releaseResourceInfo, ResourceManager resourceManager, ResourceSet neutralSet, ResourceManager releaseResourceManager, ResourceSet releaseNeutralSet, bool zip, StringBuilder output)
		{
			using (StreamReader streamReader = new StreamReader(assembly.GetManifestResourceStream(resourceInfo.ScriptName), true))
			{
				if (resourceInfo.IsDebug)
				{
					AssemblyName name = assembly.GetName();
					output.AppendLine("// Name:        " + resourceInfo.ScriptName);
					output.AppendLine("// Assembly:    " + name.Name);
					output.AppendLine("// Version:     " + name.Version.ToString());
					output.AppendLine("// FileVersion: " + AssemblyUtil.GetAssemblyFileVersion(assembly));
				}
				if (resourceInfo.PerformSubstitution)
				{
					ScriptResourceAttribute.CopyScriptToStringBuilderWithSubstitution(streamReader.ReadToEnd(), assembly, zip, output);
				}
				else
				{
					output.Append(streamReader.ReadToEnd());
				}
				ScriptResourceAttribute.WriteResourceToStringBuilder(resourceInfo, releaseResourceInfo, resourceManager, neutralSet, releaseResourceManager, releaseNeutralSet, output);
			}
		}

		// Token: 0x040001E4 RID: 484
		private string _scriptName;

		// Token: 0x040001E5 RID: 485
		private string _stringResourceName;

		// Token: 0x040001E6 RID: 486
		private string _stringResourceClientTypeName;

		// Token: 0x040001E7 RID: 487
		private static readonly Regex _webResourceRegEx = new Regex("<%\\s*=\\s*(?<resourceType>WebResource|ScriptResource)\\(\"(?<resourceName>[^\"]*)\"\\)\\s*%>", RegexOptions.Multiline | RegexOptions.Singleline);
	}
}
