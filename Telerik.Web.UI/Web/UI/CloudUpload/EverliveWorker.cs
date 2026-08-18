using System;
using System.Collections.Specialized;
using System.Web;

namespace Telerik.Web.UI.CloudUpload
{
	// Token: 0x020001BC RID: 444
	internal class EverliveWorker : BaseWorker
	{
		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x0003C025 File Offset: 0x0003A225
		internal override string FileIdentifier
		{
			get
			{
				return this.Provider.FileID;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x0003C032 File Offset: 0x0003A232
		// (set) Token: 0x06001067 RID: 4199 RVA: 0x0003C03A File Offset: 0x0003A23A
		protected EverliveProvider Provider { get; set; }

		// Token: 0x06001068 RID: 4200 RVA: 0x0003C044 File Offset: 0x0003A244
		public EverliveWorker(HttpContext context, ICloudUploadConfiguration configuration, string name, Type type) : base(context, configuration)
		{
			base.GenericProvider = (this.Provider = (EverliveProvider)CloudProviderFactory.GetProvider(name, type));
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0003C075 File Offset: 0x0003A275
		public override void PerformChunkUpload()
		{
			throw new NotImplementedException("Everlive does not support chunk upload");
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0003C081 File Offset: 0x0003A281
		protected override string GetKeyName(string subFolderStructure)
		{
			return base.GetFileName();
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0003C08C File Offset: 0x0003A28C
		protected override NameValueCollection GetCustomMetaData()
		{
			return new NameValueCollection
			{
				{
					"contentType",
					base.GetContentType()
				}
			};
		}
	}
}
