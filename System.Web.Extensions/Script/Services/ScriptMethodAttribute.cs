using System;

namespace System.Web.Script.Services
{
	// Token: 0x020000F4 RID: 244
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class ScriptMethodAttribute : Attribute
	{
		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x0002BBCF File Offset: 0x00029DCF
		// (set) Token: 0x06000D00 RID: 3328 RVA: 0x0002BBD7 File Offset: 0x00029DD7
		public ResponseFormat ResponseFormat
		{
			get
			{
				return this._responseFormat;
			}
			set
			{
				this._responseFormat = value;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x0002BBE0 File Offset: 0x00029DE0
		// (set) Token: 0x06000D02 RID: 3330 RVA: 0x0002BBE8 File Offset: 0x00029DE8
		public bool UseHttpGet
		{
			get
			{
				return this._useHttpGet;
			}
			set
			{
				this._useHttpGet = value;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x0002BBF1 File Offset: 0x00029DF1
		// (set) Token: 0x06000D04 RID: 3332 RVA: 0x0002BBF9 File Offset: 0x00029DF9
		public bool XmlSerializeString
		{
			get
			{
				return this._xmlSerializeString;
			}
			set
			{
				this._xmlSerializeString = value;
			}
		}

		// Token: 0x04000398 RID: 920
		private ResponseFormat _responseFormat;

		// Token: 0x04000399 RID: 921
		private bool _useHttpGet;

		// Token: 0x0400039A RID: 922
		private bool _xmlSerializeString;
	}
}
