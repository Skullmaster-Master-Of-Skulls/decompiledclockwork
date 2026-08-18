using System;
using System.Runtime.InteropServices;
using NLog.Internal;

namespace NLog.Targets
{
	// Token: 0x02000147 RID: 327
	[Target("AspResponse")]
	public sealed class AspResponseTarget : TargetWithLayout
	{
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x0001AC34 File Offset: 0x00018E34
		// (set) Token: 0x06000B93 RID: 2963 RVA: 0x0001AC3C File Offset: 0x00018E3C
		public bool AddComments { get; set; }

		// Token: 0x06000B94 RID: 2964 RVA: 0x0001AC45 File Offset: 0x00018E45
		public AspResponseTarget()
		{
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0001AC4D File Offset: 0x00018E4D
		public AspResponseTarget(string name) : this()
		{
			base.Name = name;
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0001AC5C File Offset: 0x00018E5C
		protected override void Write(LogEventInfo logEvent)
		{
			AspHelper.IResponse responseObject = AspHelper.GetResponseObject();
			if (responseObject != null)
			{
				if (this.AddComments)
				{
					responseObject.Write("<!-- " + this.Layout.Render(logEvent) + "-->");
				}
				else
				{
					responseObject.Write(this.Layout.Render(logEvent));
				}
				Marshal.ReleaseComObject(responseObject);
			}
		}
	}
}
