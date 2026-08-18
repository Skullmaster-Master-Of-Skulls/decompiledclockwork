using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Web.Handlers;
using System.Web.Resources;

namespace System.Web.UI
{
	// Token: 0x0200007E RID: 126
	internal class ScriptResourceInfo
	{
		// Token: 0x06000575 RID: 1397 RVA: 0x00002050 File Offset: 0x00000250
		private ScriptResourceInfo()
		{
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00019B10 File Offset: 0x00017D10
		public ScriptResourceInfo(WebResourceAttribute wra, ScriptResourceAttribute sra, Assembly assembly) : this()
		{
			this._scriptName = wra.WebResource;
			this._cdnPath = wra.CdnPath;
			this._contentType = wra.ContentType;
			this._performSubstitution = wra.PerformSubstitution;
			this._loadSuccessExpression = wra.LoadSuccessExpression;
			this._isDebug = (!string.IsNullOrEmpty(this._scriptName) && this._scriptName.EndsWith(".debug.js", StringComparison.OrdinalIgnoreCase));
			if (sra != null)
			{
				this._scriptResourceName = sra.StringResourceName;
				this._typeName = sra.StringResourceClientTypeName;
			}
			if (!string.IsNullOrEmpty(this._cdnPath))
			{
				this._cdnPath = AssemblyResourceLoader.FormatCdnUrl(assembly, this._cdnPath);
				this._cdnPathSecureConnection = AssemblyResourceLoader.FormatCdnUrl(assembly, wra.CdnPathSecureConnection);
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00019BD2 File Offset: 0x00017DD2
		public string CdnPath
		{
			get
			{
				return this._cdnPath;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x00019BDA File Offset: 0x00017DDA
		public string CdnPathSecureConnection
		{
			get
			{
				return this._cdnPathSecureConnection;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00019BE2 File Offset: 0x00017DE2
		public string LoadSuccessExpression
		{
			get
			{
				return this._loadSuccessExpression;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x00019BEA File Offset: 0x00017DEA
		public string ContentType
		{
			get
			{
				return this._contentType;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x00019BF2 File Offset: 0x00017DF2
		public bool IsDebug
		{
			get
			{
				return this._isDebug;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x00019BFA File Offset: 0x00017DFA
		public bool PerformSubstitution
		{
			get
			{
				return this._performSubstitution;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x00019C02 File Offset: 0x00017E02
		public string ScriptName
		{
			get
			{
				return this._scriptName;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x00019C0A File Offset: 0x00017E0A
		public string ScriptResourceName
		{
			get
			{
				return this._scriptResourceName;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x00019C12 File Offset: 0x00017E12
		public string TypeName
		{
			get
			{
				return this._typeName;
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00019C1C File Offset: 0x00017E1C
		public static ScriptResourceInfo GetInstance(Assembly assembly, string resourceName)
		{
			if (!ScriptResourceInfo._duplicateScriptAttributesChecked.Contains(assembly))
			{
				Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
				foreach (ScriptResourceAttribute scriptResourceAttribute in assembly.GetCustomAttributes(typeof(ScriptResourceAttribute), false))
				{
					string scriptName = scriptResourceAttribute.ScriptName;
					if (dictionary.ContainsKey(scriptName))
					{
						throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.ScriptResourceHandler_DuplicateScriptResources, new object[]
						{
							scriptName,
							assembly.GetName()
						}));
					}
					dictionary.Add(scriptName, true);
				}
				ScriptResourceInfo._duplicateScriptAttributesChecked[assembly] = true;
			}
			Tuple<Assembly, string> key = new Tuple<Assembly, string>(assembly, resourceName);
			ScriptResourceInfo scriptResourceInfo = (ScriptResourceInfo)ScriptResourceInfo._scriptCache[key];
			if (scriptResourceInfo == null)
			{
				WebResourceAttribute webResourceAttribute = null;
				ScriptResourceAttribute sra = null;
				object[] customAttributes2 = assembly.GetCustomAttributes(typeof(WebResourceAttribute), false);
				foreach (WebResourceAttribute webResourceAttribute2 in customAttributes2)
				{
					if (string.Equals(webResourceAttribute2.WebResource, resourceName, StringComparison.Ordinal))
					{
						webResourceAttribute = webResourceAttribute2;
						break;
					}
				}
				if (webResourceAttribute != null)
				{
					customAttributes2 = assembly.GetCustomAttributes(typeof(ScriptResourceAttribute), false);
					foreach (ScriptResourceAttribute scriptResourceAttribute2 in customAttributes2)
					{
						if (string.Equals(scriptResourceAttribute2.ScriptName, resourceName, StringComparison.Ordinal))
						{
							sra = scriptResourceAttribute2;
							break;
						}
					}
					scriptResourceInfo = new ScriptResourceInfo(webResourceAttribute, sra, assembly);
				}
				else
				{
					scriptResourceInfo = ScriptResourceInfo.Empty;
				}
				ScriptResourceInfo._scriptCache[key] = scriptResourceInfo;
			}
			return scriptResourceInfo;
		}

		// Token: 0x040001F3 RID: 499
		private string _contentType;

		// Token: 0x040001F4 RID: 500
		private bool _performSubstitution;

		// Token: 0x040001F5 RID: 501
		private string _scriptName;

		// Token: 0x040001F6 RID: 502
		private string _scriptResourceName;

		// Token: 0x040001F7 RID: 503
		private string _typeName;

		// Token: 0x040001F8 RID: 504
		private bool _isDebug;

		// Token: 0x040001F9 RID: 505
		private string _cdnPath;

		// Token: 0x040001FA RID: 506
		private string _cdnPathSecureConnection;

		// Token: 0x040001FB RID: 507
		private readonly string _loadSuccessExpression;

		// Token: 0x040001FC RID: 508
		private static readonly IDictionary _scriptCache = Hashtable.Synchronized(new Hashtable());

		// Token: 0x040001FD RID: 509
		private static readonly IDictionary _duplicateScriptAttributesChecked = Hashtable.Synchronized(new Hashtable());

		// Token: 0x040001FE RID: 510
		public static readonly ScriptResourceInfo Empty = new ScriptResourceInfo();
	}
}
