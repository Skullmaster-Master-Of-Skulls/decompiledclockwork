using System;

namespace System.Web.UI
{
	// Token: 0x02000066 RID: 102
	public sealed class RegisteredScript
	{
		// Token: 0x060003B0 RID: 944 RVA: 0x00013B3F File Offset: 0x00011D3F
		internal RegisteredScript(Control control, Type type, string key, string url)
		{
			this._scriptType = RegisteredScriptType.ClientScriptInclude;
			this._control = control;
			this._type = type;
			this._key = key;
			this._url = url;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00013B6B File Offset: 0x00011D6B
		internal RegisteredScript(RegisteredScriptType scriptType, Control control, Type type, string key, string script, bool addScriptTags)
		{
			this._scriptType = scriptType;
			this._control = control;
			this._type = type;
			this._key = key;
			this._script = script;
			this._addScriptTags = addScriptTags;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x00013BA0 File Offset: 0x00011DA0
		public bool AddScriptTags
		{
			get
			{
				return this._addScriptTags;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x00013BA8 File Offset: 0x00011DA8
		public Control Control
		{
			get
			{
				return this._control;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x00013BB0 File Offset: 0x00011DB0
		public string Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00013BB8 File Offset: 0x00011DB8
		public string Script
		{
			get
			{
				return this._script;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x00013BC0 File Offset: 0x00011DC0
		public RegisteredScriptType ScriptType
		{
			get
			{
				return this._scriptType;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00013BC8 File Offset: 0x00011DC8
		public Type Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x00013BD0 File Offset: 0x00011DD0
		public string Url
		{
			get
			{
				return this._url;
			}
		}

		// Token: 0x04000160 RID: 352
		private RegisteredScriptType _scriptType;

		// Token: 0x04000161 RID: 353
		private Control _control;

		// Token: 0x04000162 RID: 354
		private string _key;

		// Token: 0x04000163 RID: 355
		private string _script;

		// Token: 0x04000164 RID: 356
		private Type _type;

		// Token: 0x04000165 RID: 357
		private bool _addScriptTags;

		// Token: 0x04000166 RID: 358
		private string _url;
	}
}
