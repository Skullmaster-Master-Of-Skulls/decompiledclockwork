using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000E83 RID: 3715
	public class RadScriptReference : ScriptReference
	{
		// Token: 0x17002C82 RID: 11394
		// (get) Token: 0x06008CE7 RID: 36071 RVA: 0x00200001 File Offset: 0x001FE201
		// (set) Token: 0x06008CE8 RID: 36072 RVA: 0x00200009 File Offset: 0x001FE209
		public bool Combine
		{
			get
			{
				return this._combine;
			}
			set
			{
				this._combine = value;
			}
		}

		// Token: 0x17002C83 RID: 11395
		// (get) Token: 0x06008CE9 RID: 36073 RVA: 0x00200012 File Offset: 0x001FE212
		// (set) Token: 0x06008CEA RID: 36074 RVA: 0x0020001A File Offset: 0x001FE21A
		public ScriptReferenceOutputPosition OutputPosition
		{
			get
			{
				return this._outputPosition;
			}
			set
			{
				this._outputPosition = value;
			}
		}

		// Token: 0x04002791 RID: 10129
		private bool _combine = true;

		// Token: 0x04002792 RID: 10130
		private ScriptReferenceOutputPosition _outputPosition = ScriptReferenceOutputPosition.Same;
	}
}
