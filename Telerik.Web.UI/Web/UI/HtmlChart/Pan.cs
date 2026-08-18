using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.HtmlChart
{
	// Token: 0x020003C3 RID: 963
	public class Pan : SerializableChartElement
	{
		// Token: 0x06002348 RID: 9032 RVA: 0x00076160 File Offset: 0x00074360
		public Pan()
		{
			base.RegisterConverters(new List<JavaScriptConverter>
			{
				new PanConverter()
			});
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06002349 RID: 9033 RVA: 0x0007618B File Offset: 0x0007438B
		// (set) Token: 0x0600234A RID: 9034 RVA: 0x000761AC File Offset: 0x000743AC
		[DefaultValue(false)]
		public bool Enabled
		{
			get
			{
				return (bool)(base.ViewState["Enabled"] ?? false);
			}
			set
			{
				base.ViewState["Enabled"] = value;
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x0600234B RID: 9035 RVA: 0x000761C4 File Offset: 0x000743C4
		// (set) Token: 0x0600234C RID: 9036 RVA: 0x000761E5 File Offset: 0x000743E5
		[DefaultValue(AxisLock.None)]
		public AxisLock Lock
		{
			get
			{
				return (AxisLock)(base.ViewState["Lock"] ?? AxisLock.None);
			}
			set
			{
				base.ViewState["Lock"] = value;
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x0600234D RID: 9037 RVA: 0x000761FD File Offset: 0x000743FD
		// (set) Token: 0x0600234E RID: 9038 RVA: 0x0007621E File Offset: 0x0007441E
		[DefaultValue(ModifierKey.None)]
		public ModifierKey ModifierKey
		{
			get
			{
				return (ModifierKey)(base.ViewState["ModifierKey"] ?? ModifierKey.None);
			}
			set
			{
				base.ViewState["ModifierKey"] = value;
			}
		}
	}
}
