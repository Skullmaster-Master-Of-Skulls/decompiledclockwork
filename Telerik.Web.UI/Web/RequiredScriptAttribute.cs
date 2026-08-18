using System;

namespace Telerik.Web
{
	// Token: 0x02000F67 RID: 3943
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class RequiredScriptAttribute : Attribute
	{
		// Token: 0x17002F69 RID: 12137
		// (get) Token: 0x0600961C RID: 38428 RVA: 0x0021880E File Offset: 0x00216A0E
		public Type ExtenderType
		{
			get
			{
				return this._extenderType;
			}
		}

		// Token: 0x17002F6A RID: 12138
		// (get) Token: 0x0600961D RID: 38429 RVA: 0x00218816 File Offset: 0x00216A16
		public string ScriptName
		{
			get
			{
				return this._scriptName;
			}
		}

		// Token: 0x17002F6B RID: 12139
		// (get) Token: 0x0600961E RID: 38430 RVA: 0x0021881E File Offset: 0x00216A1E
		public int LoadOrder
		{
			get
			{
				return this._order;
			}
		}

		// Token: 0x0600961F RID: 38431 RVA: 0x00218826 File Offset: 0x00216A26
		public RequiredScriptAttribute()
		{
		}

		// Token: 0x06009620 RID: 38432 RVA: 0x0021882E File Offset: 0x00216A2E
		public RequiredScriptAttribute(string scriptName)
		{
			this._scriptName = scriptName;
		}

		// Token: 0x06009621 RID: 38433 RVA: 0x0021883D File Offset: 0x00216A3D
		public RequiredScriptAttribute(Type extenderType) : this(extenderType, 0)
		{
		}

		// Token: 0x06009622 RID: 38434 RVA: 0x00218847 File Offset: 0x00216A47
		public RequiredScriptAttribute(Type extenderType, int loadOrder)
		{
			this._extenderType = extenderType;
			this._order = loadOrder;
		}

		// Token: 0x06009623 RID: 38435 RVA: 0x0021885D File Offset: 0x00216A5D
		public override bool IsDefaultAttribute()
		{
			return this._extenderType == null;
		}

		// Token: 0x04002AF9 RID: 11001
		private readonly int _order;

		// Token: 0x04002AFA RID: 11002
		private readonly Type _extenderType;

		// Token: 0x04002AFB RID: 11003
		private readonly string _scriptName;
	}
}
