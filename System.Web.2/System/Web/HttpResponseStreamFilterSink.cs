using System;

namespace System.Web
{
	// Token: 0x020000C9 RID: 201
	internal sealed class HttpResponseStreamFilterSink : HttpResponseStream
	{
		// Token: 0x06000D9D RID: 3485 RVA: 0x0002607B File Offset: 0x0002427B
		internal HttpResponseStreamFilterSink(HttpWriter writer) : base(writer)
		{
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00026084 File Offset: 0x00024284
		private void VerifyState()
		{
			if (!this._filtering)
			{
				throw new HttpException(SR.GetString("Invalid_use_of_response_filter"));
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x0002609E File Offset: 0x0002429E
		// (set) Token: 0x06000DA0 RID: 3488 RVA: 0x000260A6 File Offset: 0x000242A6
		internal bool Filtering
		{
			get
			{
				return this._filtering;
			}
			set
			{
				this._filtering = value;
			}
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x000260AF File Offset: 0x000242AF
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00006164 File Offset: 0x00004364
		public override void Flush()
		{
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x000260B8 File Offset: 0x000242B8
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.VerifyState();
			base.Write(buffer, offset, count);
		}

		// Token: 0x04000509 RID: 1289
		private bool _filtering;
	}
}
