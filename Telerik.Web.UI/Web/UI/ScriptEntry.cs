using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web.UI;
using Telerik.Web.Cryptography;

namespace Telerik.Web.UI
{
	// Token: 0x02000860 RID: 2144
	internal class ScriptEntry
	{
		// Token: 0x170019C6 RID: 6598
		// (get) Token: 0x06004EDE RID: 20190 RVA: 0x000F74C8 File Offset: 0x000F56C8
		public string Assembly
		{
			get
			{
				return this._assembly;
			}
		}

		// Token: 0x170019C7 RID: 6599
		// (get) Token: 0x06004EDF RID: 20191 RVA: 0x000F74D0 File Offset: 0x000F56D0
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170019C8 RID: 6600
		// (get) Token: 0x06004EE0 RID: 20192 RVA: 0x000F74D8 File Offset: 0x000F56D8
		public string Culture
		{
			get
			{
				return this._culture;
			}
		}

		// Token: 0x170019C9 RID: 6601
		// (get) Token: 0x06004EE1 RID: 20193 RVA: 0x000F74E0 File Offset: 0x000F56E0
		public virtual string Path
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x170019CA RID: 6602
		// (get) Token: 0x06004EE2 RID: 20194 RVA: 0x000F74E8 File Offset: 0x000F56E8
		public virtual bool HasInitialPath
		{
			get
			{
				return this._hasInitialPath;
			}
		}

		// Token: 0x170019CB RID: 6603
		// (get) Token: 0x06004EE3 RID: 20195 RVA: 0x000F74F0 File Offset: 0x000F56F0
		public virtual bool IsExternal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170019CC RID: 6604
		// (get) Token: 0x06004EE4 RID: 20196 RVA: 0x000F74F3 File Offset: 0x000F56F3
		// (set) Token: 0x06004EE5 RID: 20197 RVA: 0x000F74FB File Offset: 0x000F56FB
		internal bool EnableSelectorLimitCheck
		{
			get
			{
				return this._enableSelectorLimitCheck;
			}
			set
			{
				this._enableSelectorLimitCheck = value;
			}
		}

		// Token: 0x170019CD RID: 6605
		// (get) Token: 0x06004EE6 RID: 20198 RVA: 0x000F7504 File Offset: 0x000F5704
		// (set) Token: 0x06004EE7 RID: 20199 RVA: 0x000F750C File Offset: 0x000F570C
		internal bool LoadSeparately
		{
			get
			{
				return this._loadSeparately;
			}
			set
			{
				this._loadSeparately = value;
			}
		}

		// Token: 0x06004EE8 RID: 20200 RVA: 0x000F7515 File Offset: 0x000F5715
		public ScriptEntry()
		{
		}

		// Token: 0x06004EE9 RID: 20201 RVA: 0x000F7524 File Offset: 0x000F5724
		public ScriptEntry(string assembly, string name, string culture)
		{
			this._assembly = assembly;
			this._name = name;
			this._culture = culture;
			this._path = string.Empty;
		}

		// Token: 0x06004EEA RID: 20202 RVA: 0x000F7554 File Offset: 0x000F5754
		public ScriptEntry(ScriptReference scriptReference) : this(scriptReference.Assembly, scriptReference.Name, null)
		{
			this._scriptReference = scriptReference;
			this._path = scriptReference.Path;
			this._hasInitialPath = !string.IsNullOrEmpty(this._scriptReference.Path);
		}

		// Token: 0x06004EEB RID: 20203 RVA: 0x000F75A0 File Offset: 0x000F57A0
		public virtual void SetPath(string path)
		{
			if (this._scriptReference != null)
			{
				this._scriptReference.Path = path;
			}
		}

		// Token: 0x06004EEC RID: 20204 RVA: 0x000F75B6 File Offset: 0x000F57B6
		public void ResetReference()
		{
			if (this._scriptReference != null)
			{
				this._scriptReference.Assembly = string.Empty;
				this._scriptReference.Name = string.Empty;
			}
		}

		// Token: 0x06004EED RID: 20205 RVA: 0x000F75E0 File Offset: 0x000F57E0
		public virtual string GetScript()
		{
			string result = string.Empty;
			using (Stream manifestResourceStream = this.LoadAssembly().GetManifestResourceStream(this.Name))
			{
				if (manifestResourceStream != null)
				{
					using (StreamReader streamReader = new StreamReader(manifestResourceStream))
					{
						result = streamReader.ReadToEnd();
					}
				}
			}
			return result;
		}

		// Token: 0x06004EEE RID: 20206 RVA: 0x000F764C File Offset: 0x000F584C
		public Assembly LoadAssembly()
		{
			if (null == this._loadedAssembly)
			{
				try
				{
					this._loadedAssembly = System.Reflection.Assembly.Load(this.Assembly);
				}
				catch (Exception)
				{
				}
			}
			return this._loadedAssembly;
		}

		// Token: 0x06004EEF RID: 20207 RVA: 0x000F7694 File Offset: 0x000F5894
		public virtual Stream GetResourceStream()
		{
			return this.LoadAssembly().GetManifestResourceStream(this.Name);
		}

		// Token: 0x06004EF0 RID: 20208 RVA: 0x000F76A8 File Offset: 0x000F58A8
		public static bool AssembliesEqual(ScriptEntry scriptEntry1, ScriptEntry scriptEntry2)
		{
			return scriptEntry1.Assembly == scriptEntry2.Assembly || scriptEntry1.Assembly.StartsWith(scriptEntry2.Assembly + ",") || scriptEntry2.Assembly.StartsWith(scriptEntry1.Assembly + ",");
		}

		// Token: 0x06004EF1 RID: 20209 RVA: 0x000F7704 File Offset: 0x000F5904
		internal virtual string GetSerializedAssemblyInfo()
		{
			return string.Concat(new object[]
			{
				";",
				this.Assembly,
				":",
				CultureInfo.CurrentUICulture.IetfLanguageTag,
				":",
				this.LoadAssembly().ManifestModule.ModuleVersionId
			});
		}

		// Token: 0x06004EF2 RID: 20210 RVA: 0x000F7764 File Offset: 0x000F5964
		internal virtual string GetSerializedScriptEntryInfo()
		{
			return ":" + ScriptEntry.GetHashCode(this.Name);
		}

		// Token: 0x06004EF3 RID: 20211 RVA: 0x000F777C File Offset: 0x000F597C
		public override bool Equals(object obj)
		{
			ScriptEntry scriptEntry = obj as ScriptEntry;
			return scriptEntry != null && ScriptEntry.AssembliesEqual(scriptEntry, this) && scriptEntry.Name == this.Name;
		}

		// Token: 0x06004EF4 RID: 20212 RVA: 0x000F77B1 File Offset: 0x000F59B1
		public override int GetHashCode()
		{
			return this.Assembly.GetHashCode() ^ this.Name.GetHashCode();
		}

		// Token: 0x06004EF5 RID: 20213 RVA: 0x000F77CC File Offset: 0x000F59CC
		public static string GetHashCode(string value)
		{
			return Crc32.Compute(Encoding.UTF8.GetBytes(value)).ToString("x", CultureInfo.InvariantCulture);
		}

		// Token: 0x06004EF6 RID: 20214 RVA: 0x000F77FC File Offset: 0x000F59FC
		public static List<ScriptEntry> Deserialize(string serializedScriptEntries)
		{
			List<ScriptEntry> list = new List<ScriptEntry>();
			foreach (string text in serializedScriptEntries.Split(new char[]
			{
				';'
			}))
			{
				string text2 = null;
				string text3 = null;
				string text4 = null;
				Dictionary<string, string> dictionary = null;
				foreach (string text5 in text.Split(new char[]
				{
					':'
				}))
				{
					if (text2 == null)
					{
						text2 = text5;
					}
					else if (text2.StartsWith("||"))
					{
						string text6 = text5;
						string securePathFromHash = ExternalScriptHelper.GetSecurePathFromHash(text6);
						if (securePathFromHash == null)
						{
							list.Add(new InvalidScriptEntry(string.Empty, text6));
						}
						else
						{
							list.Add(new ExternalScriptEntry(securePathFromHash));
						}
					}
					else if (text2.StartsWith("|"))
					{
						string text7 = text5;
						string securePathFromHash2 = ExternalStyleSheetUtils.GetSecurePathFromHash(text7);
						if (securePathFromHash2 == null)
						{
							list.Add(new InvalidScriptEntry(string.Empty, text7));
						}
						else
						{
							list.Add(new ExternalStyleSheetEntry(securePathFromHash2));
						}
					}
					else if (text3 == null)
					{
						text3 = text5;
					}
					else if (text4 == null)
					{
						text4 = text5;
					}
					else
					{
						string text8 = text5;
						if (dictionary == null)
						{
							dictionary = ScriptEntry.GetResourceNameHashes(text2);
						}
						string name;
						ScriptEntry item;
						if (!dictionary.TryGetValue(text8, out name))
						{
							item = new InvalidScriptEntry(text2, text8);
						}
						else
						{
							item = new ScriptEntry(text2, name, text3);
						}
						list.Add(item);
					}
				}
			}
			return list;
		}

		// Token: 0x06004EF7 RID: 20215 RVA: 0x000F796C File Offset: 0x000F5B6C
		private static Dictionary<string, string> GetResourceNameHashes(string assemblyName)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			Assembly assembly = new ScriptEntry(assemblyName, null, null).LoadAssembly();
			if (assembly == null)
			{
				return dictionary;
			}
			foreach (string value in assembly.GetManifestResourceNames())
			{
				string hashCode = ScriptEntry.GetHashCode(value);
				if (dictionary.ContainsKey(hashCode))
				{
					throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, "Assembly \"{0}\" contains multiple scripts with hash code \"{1}\".", new object[]
					{
						assemblyName,
						hashCode
					}));
				}
				dictionary[hashCode] = value;
			}
			return dictionary;
		}

		// Token: 0x0400139E RID: 5022
		public const string CombinedScriptsParamName = "_TSM_CombinedScripts_";

		// Token: 0x0400139F RID: 5023
		public const string HiddenFieldParamName = "_TSM_HiddenField_";

		// Token: 0x040013A0 RID: 5024
		private readonly string _assembly;

		// Token: 0x040013A1 RID: 5025
		private readonly string _name;

		// Token: 0x040013A2 RID: 5026
		private readonly string _culture;

		// Token: 0x040013A3 RID: 5027
		private readonly ScriptReference _scriptReference;

		// Token: 0x040013A4 RID: 5028
		private Assembly _loadedAssembly;

		// Token: 0x040013A5 RID: 5029
		private readonly string _path;

		// Token: 0x040013A6 RID: 5030
		private readonly bool _hasInitialPath;

		// Token: 0x040013A7 RID: 5031
		private bool _enableSelectorLimitCheck = true;

		// Token: 0x040013A8 RID: 5032
		private bool _loadSeparately;
	}
}
