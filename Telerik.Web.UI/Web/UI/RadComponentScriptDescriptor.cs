using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000F54 RID: 3924
	internal class RadComponentScriptDescriptor : ScriptComponentDescriptor, IScriptDescriptor
	{
		// Token: 0x060095A6 RID: 38310 RVA: 0x00216ADF File Offset: 0x00214CDF
		public RadComponentScriptDescriptor(string type) : base(type)
		{
		}

		// Token: 0x060095A7 RID: 38311 RVA: 0x00216AE8 File Offset: 0x00214CE8
		void IScriptDescriptor.AddComponentProperty(string A_1, string A_2)
		{
			base.AddComponentProperty(A_1, A_2);
		}

		// Token: 0x060095A8 RID: 38312 RVA: 0x00216AF2 File Offset: 0x00214CF2
		void IScriptDescriptor.AddElementProperty(string A_1, string A_2)
		{
			base.AddElementProperty(A_1, A_2);
		}

		// Token: 0x060095A9 RID: 38313 RVA: 0x00216AFC File Offset: 0x00214CFC
		void IScriptDescriptor.AddEvent(string A_1, string A_2)
		{
			base.AddEvent(A_1, A_2);
		}

		// Token: 0x060095AA RID: 38314 RVA: 0x00216B06 File Offset: 0x00214D06
		void IScriptDescriptor.AddProperty(string A_1, object A_2)
		{
			base.AddProperty(A_1, A_2);
		}

		// Token: 0x060095AB RID: 38315 RVA: 0x00216B10 File Offset: 0x00214D10
		void IScriptDescriptor.AddScriptProperty(string A_1, string A_2)
		{
			base.AddScriptProperty(A_1, A_2);
		}
	}
}
