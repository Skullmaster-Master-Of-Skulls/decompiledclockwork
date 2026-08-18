using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000F53 RID: 3923
	internal class RadControlScriptDescriptor : ScriptControlDescriptor, IScriptDescriptor
	{
		// Token: 0x0600959F RID: 38303 RVA: 0x00216A9B File Offset: 0x00214C9B
		public RadControlScriptDescriptor(string type, string elementID) : base(type, elementID)
		{
		}

		// Token: 0x17002F59 RID: 12121
		// (get) Token: 0x060095A0 RID: 38304 RVA: 0x00216AA5 File Offset: 0x00214CA5
		public string Script
		{
			get
			{
				return this.GetScript();
			}
		}

		// Token: 0x060095A1 RID: 38305 RVA: 0x00216AAD File Offset: 0x00214CAD
		void IScriptDescriptor.AddComponentProperty(string A_1, string A_2)
		{
			base.AddComponentProperty(A_1, A_2);
		}

		// Token: 0x060095A2 RID: 38306 RVA: 0x00216AB7 File Offset: 0x00214CB7
		void IScriptDescriptor.AddElementProperty(string A_1, string A_2)
		{
			base.AddElementProperty(A_1, A_2);
		}

		// Token: 0x060095A3 RID: 38307 RVA: 0x00216AC1 File Offset: 0x00214CC1
		void IScriptDescriptor.AddEvent(string A_1, string A_2)
		{
			base.AddEvent(A_1, A_2);
		}

		// Token: 0x060095A4 RID: 38308 RVA: 0x00216ACB File Offset: 0x00214CCB
		void IScriptDescriptor.AddProperty(string A_1, object A_2)
		{
			base.AddProperty(A_1, A_2);
		}

		// Token: 0x060095A5 RID: 38309 RVA: 0x00216AD5 File Offset: 0x00214CD5
		void IScriptDescriptor.AddScriptProperty(string A_1, string A_2)
		{
			base.AddScriptProperty(A_1, A_2);
		}
	}
}
