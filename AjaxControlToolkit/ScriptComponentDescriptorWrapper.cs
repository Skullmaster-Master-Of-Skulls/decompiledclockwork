using System;
using System.Web.UI;

namespace AjaxControlToolkit
{
	// Token: 0x02000016 RID: 22
	internal class ScriptComponentDescriptorWrapper : IScriptComponentDescriptor
	{
		// Token: 0x060000EA RID: 234 RVA: 0x000040FA File Offset: 0x000022FA
		public ScriptComponentDescriptorWrapper(ScriptComponentDescriptor descriptor)
		{
			this._descriptor = descriptor;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00004109 File Offset: 0x00002309
		public string ClientID
		{
			get
			{
				return this._descriptor.ClientID;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00004116 File Offset: 0x00002316
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00004123 File Offset: 0x00002323
		public string ID
		{
			get
			{
				return this._descriptor.ID;
			}
			set
			{
				this._descriptor.ID = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00004131 File Offset: 0x00002331
		// (set) Token: 0x060000EF RID: 239 RVA: 0x0000413E File Offset: 0x0000233E
		public string Type
		{
			get
			{
				return this._descriptor.Type;
			}
			set
			{
				this._descriptor.Type = value;
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000414C File Offset: 0x0000234C
		public void AddComponentProperty(string name, string componentID)
		{
			this._descriptor.AddComponentProperty(name, componentID);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000415B File Offset: 0x0000235B
		public void AddElementProperty(string name, string elementID)
		{
			this._descriptor.AddElementProperty(name, elementID);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000416A File Offset: 0x0000236A
		public void AddEvent(string name, string handler)
		{
			this._descriptor.AddEvent(name, handler);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004179 File Offset: 0x00002379
		public void AddProperty(string name, object value)
		{
			this._descriptor.AddProperty(name, value);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004188 File Offset: 0x00002388
		public void AddScriptProperty(string name, string script)
		{
			this._descriptor.AddScriptProperty(name, script);
		}

		// Token: 0x0400003A RID: 58
		private ScriptComponentDescriptor _descriptor;
	}
}
