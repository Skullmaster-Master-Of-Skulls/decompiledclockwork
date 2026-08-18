using System;

namespace AjaxControlToolkit
{
	// Token: 0x02000091 RID: 145
	[AttributeUsage(AttributeTargets.Property, Inherited = true)]
	public sealed class ExtenderControlEventAttribute : Attribute
	{
		// Token: 0x060004A8 RID: 1192 RVA: 0x0000CD20 File Offset: 0x0000AF20
		public ExtenderControlEventAttribute() : this(true)
		{
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0000CD29 File Offset: 0x0000AF29
		public ExtenderControlEventAttribute(bool isScriptEvent)
		{
			this._isScriptEvent = isScriptEvent;
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0000CD38 File Offset: 0x0000AF38
		public bool IsScriptEvent
		{
			get
			{
				return this._isScriptEvent;
			}
		}

		// Token: 0x040002A0 RID: 672
		private bool _isScriptEvent;
	}
}
