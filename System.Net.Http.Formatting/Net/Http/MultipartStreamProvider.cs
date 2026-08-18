using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	// Token: 0x02000013 RID: 19
	public abstract class MultipartStreamProvider
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000041D1 File Offset: 0x000023D1
		public Collection<HttpContent> Contents
		{
			get
			{
				return this._contents;
			}
		}

		// Token: 0x06000097 RID: 151
		public abstract Stream GetStream(HttpContent parent, HttpContentHeaders headers);

		// Token: 0x06000098 RID: 152 RVA: 0x000041D9 File Offset: 0x000023D9
		public virtual Task ExecutePostProcessingAsync()
		{
			return TaskHelpers.Completed();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000041E0 File Offset: 0x000023E0
		public virtual Task ExecutePostProcessingAsync(CancellationToken cancellationToken)
		{
			return this.ExecutePostProcessingAsync();
		}

		// Token: 0x0400002D RID: 45
		private Collection<HttpContent> _contents = new Collection<HttpContent>();
	}
}
