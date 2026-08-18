using System;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using Telerik.Web.UI.Upload;

namespace Telerik.Web.UI
{
	// Token: 0x02001348 RID: 4936
	public sealed class RadProgressContext : ProgressData
	{
		// Token: 0x17004229 RID: 16937
		// (get) Token: 0x0600CDD1 RID: 52689 RVA: 0x002DCD8C File Offset: 0x002DAF8C
		public static RadProgressContext Current
		{
			get
			{
				RadProgressContext radProgressContext = RadProgressContext.GetProgressContext(HttpContext.Current);
				if (radProgressContext == null)
				{
					radProgressContext = RadProgressContext.SetProgressContext(HttpContext.Current);
				}
				return radProgressContext;
			}
		}

		// Token: 0x0600CDD2 RID: 52690 RVA: 0x002DCDB3 File Offset: 0x002DAFB3
		public static void RemoveProgressContext(HttpContext context)
		{
			context.Application.Remove("RadProgressContext" + RadUploadContext.GetUploadUniqueIdentifier(context));
		}

		// Token: 0x0600CDD3 RID: 52691 RVA: 0x002DCDD0 File Offset: 0x002DAFD0
		private RadProgressContext()
		{
		}

		// Token: 0x0600CDD4 RID: 52692 RVA: 0x002DCDD8 File Offset: 0x002DAFD8
		private static RadProgressContext GetProgressContext(HttpContext context)
		{
			return context.Application["RadProgressContext" + RadUploadContext.GetUploadUniqueIdentifier(context)] as RadProgressContext;
		}

		// Token: 0x0600CDD5 RID: 52693 RVA: 0x002DCDFC File Offset: 0x002DAFFC
		private static RadProgressContext SetProgressContext(HttpContext context)
		{
			RadProgressContext radProgressContext = new RadProgressContext();
			context.Application["RadProgressContext" + RadUploadContext.GetUploadUniqueIdentifier(context)] = radProgressContext;
			return radProgressContext;
		}

		// Token: 0x0600CDD6 RID: 52694 RVA: 0x002DCE2C File Offset: 0x002DB02C
		public override void Serialize(TextWriter writer)
		{
			ProgressData progressData = this.GetProgressData();
			if (progressData == null)
			{
				base.Serialize(writer);
				return;
			}
			progressData.Serialize(writer);
		}

		// Token: 0x0600CDD7 RID: 52695 RVA: 0x002DCE54 File Offset: 0x002DB054
		private ProgressData GetProgressData()
		{
			RadUploadContext current = RadUploadContext.GetCurrent(HttpContext.Current);
			if (current != null && !current.UploadComplete)
			{
				return current.GetProgressData();
			}
			return null;
		}

		// Token: 0x0600CDD8 RID: 52696 RVA: 0x002DCE80 File Offset: 0x002DB080
		public void Serialize(TextWriter writer, bool isJSON)
		{
			if (!isJSON)
			{
				this.Serialize(writer);
				return;
			}
			ProgressData progressData = this.GetProgressData();
			if (progressData == null)
			{
				base.Serialize(writer);
				return;
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			writer.Write(javaScriptSerializer.Serialize(progressData));
		}
	}
}
