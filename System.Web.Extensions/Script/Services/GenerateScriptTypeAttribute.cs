using System;

namespace System.Web.Script.Services
{
	// Token: 0x020000EB RID: 235
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface, AllowMultiple = true)]
	public sealed class GenerateScriptTypeAttribute : Attribute
	{
		// Token: 0x06000CD4 RID: 3284 RVA: 0x0002B1EF File Offset: 0x000293EF
		public GenerateScriptTypeAttribute(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this._type = type;
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0002B212 File Offset: 0x00029412
		public Type Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x0002B21A File Offset: 0x0002941A
		// (set) Token: 0x06000CD7 RID: 3287 RVA: 0x0002B22B File Offset: 0x0002942B
		public string ScriptTypeId
		{
			get
			{
				return this._typeId ?? string.Empty;
			}
			set
			{
				this._typeId = value;
			}
		}

		// Token: 0x0400038B RID: 907
		private Type _type;

		// Token: 0x0400038C RID: 908
		private string _typeId;
	}
}
