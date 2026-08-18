using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000F50 RID: 3920
	internal class InvalidScriptEntry : ScriptEntry
	{
		// Token: 0x06009594 RID: 38292 RVA: 0x002169F3 File Offset: 0x00214BF3
		public InvalidScriptEntry(string assembly, string hashedScriptName) : base(assembly, string.Empty, string.Empty)
		{
			this._hashedScriptName = hashedScriptName;
		}

		// Token: 0x06009595 RID: 38293 RVA: 0x00216A0D File Offset: 0x00214C0D
		public override string GetScript()
		{
			return "/* ERROR: Unable to load script. Set EnableScriptCombine to 'false' to see detailed error info. */";
		}

		// Token: 0x06009596 RID: 38294 RVA: 0x00216A14 File Offset: 0x00214C14
		public override bool Equals(object obj)
		{
			ExternalStyleSheetEntry externalStyleSheetEntry = obj as ExternalStyleSheetEntry;
			if (externalStyleSheetEntry != null)
			{
				return this._hashedScriptName == ScriptEntry.GetHashCode(externalStyleSheetEntry.Path);
			}
			return base.Equals(obj);
		}

		// Token: 0x06009597 RID: 38295 RVA: 0x00216A49 File Offset: 0x00214C49
		public override int GetHashCode()
		{
			return this._hashedScriptName.GetHashCode();
		}

		// Token: 0x04002ACB RID: 10955
		private readonly string _hashedScriptName;
	}
}
