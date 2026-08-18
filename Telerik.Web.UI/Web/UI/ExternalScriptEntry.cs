using System;
using System.IO;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000861 RID: 2145
	internal class ExternalScriptEntry : ScriptEntry
	{
		// Token: 0x170019CE RID: 6606
		// (get) Token: 0x06004EF8 RID: 20216 RVA: 0x000F79FB File Offset: 0x000F5BFB
		public override string Path
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x170019CF RID: 6607
		// (get) Token: 0x06004EF9 RID: 20217 RVA: 0x000F7A03 File Offset: 0x000F5C03
		public long LastModified
		{
			get
			{
				if (this._lastModified == -1L && !this._disableLastModifiedLookup)
				{
					this._lastModified = ExternalScriptHelper.GetLastModifiedInTicks(this._path);
				}
				return this._lastModified;
			}
		}

		// Token: 0x170019D0 RID: 6608
		// (set) Token: 0x06004EFA RID: 20218 RVA: 0x000F7A2E File Offset: 0x000F5C2E
		internal bool DisableLastModifiedLookup
		{
			set
			{
				this._disableLastModifiedLookup = value;
			}
		}

		// Token: 0x170019D1 RID: 6609
		// (get) Token: 0x06004EFB RID: 20219 RVA: 0x000F7A37 File Offset: 0x000F5C37
		public override bool IsExternal
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170019D2 RID: 6610
		// (get) Token: 0x06004EFC RID: 20220 RVA: 0x000F7A3A File Offset: 0x000F5C3A
		public override bool HasInitialPath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004EFD RID: 20221 RVA: 0x000F7A3D File Offset: 0x000F5C3D
		public ExternalScriptEntry()
		{
		}

		// Token: 0x06004EFE RID: 20222 RVA: 0x000F7A45 File Offset: 0x000F5C45
		public ExternalScriptEntry(string path) : base(string.Empty, string.Empty, null)
		{
			this._path = path;
			this._lastModified = -1L;
		}

		// Token: 0x06004EFF RID: 20223 RVA: 0x000F7A67 File Offset: 0x000F5C67
		public ExternalScriptEntry(ScriptReference scriptReference) : base(scriptReference)
		{
			this._path = ExternalScriptHelper.ResolveSecurePath(scriptReference.Path);
			this._lastModified = -1L;
		}

		// Token: 0x06004F00 RID: 20224 RVA: 0x000F7A8C File Offset: 0x000F5C8C
		public override bool Equals(object obj)
		{
			ExternalScriptEntry externalScriptEntry = obj as ExternalScriptEntry;
			if (externalScriptEntry != null)
			{
				return this._path.Equals(externalScriptEntry._path);
			}
			InvalidScriptEntry invalidScriptEntry = obj as InvalidScriptEntry;
			return invalidScriptEntry != null && invalidScriptEntry.Equals(this);
		}

		// Token: 0x06004F01 RID: 20225 RVA: 0x000F7AC8 File Offset: 0x000F5CC8
		public override int GetHashCode()
		{
			return this._path.GetHashCode();
		}

		// Token: 0x06004F02 RID: 20226 RVA: 0x000F7AD5 File Offset: 0x000F5CD5
		internal override string GetSerializedAssemblyInfo()
		{
			return ";" + "||";
		}

		// Token: 0x06004F03 RID: 20227 RVA: 0x000F7AE6 File Offset: 0x000F5CE6
		internal override string GetSerializedScriptEntryInfo()
		{
			return ":" + ScriptEntry.GetHashCode(this._path);
		}

		// Token: 0x06004F04 RID: 20228 RVA: 0x000F7AFD File Offset: 0x000F5CFD
		public override string GetScript()
		{
			return ExternalScriptHelper.LoadContent(this._path);
		}

		// Token: 0x06004F05 RID: 20229 RVA: 0x000F7B0C File Offset: 0x000F5D0C
		public override Stream GetResourceStream()
		{
			string s = ExternalScriptHelper.LoadContent(this._path);
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			return new MemoryStream(bytes);
		}

		// Token: 0x040013A9 RID: 5033
		internal const string AssemblyInfoExternal = "||";

		// Token: 0x040013AA RID: 5034
		private readonly string _path;

		// Token: 0x040013AB RID: 5035
		private long _lastModified;

		// Token: 0x040013AC RID: 5036
		private bool _disableLastModifiedLookup;
	}
}
