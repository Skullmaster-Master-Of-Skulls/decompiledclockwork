using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Threading;

namespace System.Net.Mime
{
	// Token: 0x0200024A RID: 586
	internal class MimeMultiPart : MimeBasePart
	{
		// Token: 0x0600163F RID: 5695 RVA: 0x000734AC File Offset: 0x000716AC
		internal MimeMultiPart(MimeMultiPartType type)
		{
			this.MimeMultiPartType = type;
		}

		// Token: 0x170004AE RID: 1198
		// (set) Token: 0x06001640 RID: 5696 RVA: 0x000734BB File Offset: 0x000716BB
		internal MimeMultiPartType MimeMultiPartType
		{
			set
			{
				if (value > MimeMultiPartType.Related || value < MimeMultiPartType.Mixed)
				{
					throw new NotSupportedException(value.ToString());
				}
				this.SetType(value);
			}
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x000734DF File Offset: 0x000716DF
		private void SetType(MimeMultiPartType type)
		{
			base.ContentType.MediaType = "multipart/" + type.ToString().ToLower(CultureInfo.InvariantCulture);
			base.ContentType.Boundary = this.GetNextBoundary();
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x0007351E File Offset: 0x0007171E
		internal Collection<MimeBasePart> Parts
		{
			get
			{
				if (this.parts == null)
				{
					this.parts = new Collection<MimeBasePart>();
				}
				return this.parts;
			}
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x0007353C File Offset: 0x0007173C
		internal void Complete(IAsyncResult result, Exception e)
		{
			MimeMultiPart.MimePartContext mimePartContext = (MimeMultiPart.MimePartContext)result.AsyncState;
			if (mimePartContext.completed)
			{
				throw e;
			}
			try
			{
				mimePartContext.outputStream.Close();
			}
			catch (Exception ex)
			{
				if (e == null)
				{
					e = ex;
				}
			}
			mimePartContext.completed = true;
			mimePartContext.result.InvokeCallback(e);
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x0007359C File Offset: 0x0007179C
		internal void MimeWriterCloseCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimeMultiPart.MimePartContext)result.AsyncState).completedSynchronously = false;
			try
			{
				this.MimeWriterCloseCallbackHandler(result);
			}
			catch (Exception e)
			{
				this.Complete(result, e);
			}
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x000735E8 File Offset: 0x000717E8
		private void MimeWriterCloseCallbackHandler(IAsyncResult result)
		{
			MimeMultiPart.MimePartContext mimePartContext = (MimeMultiPart.MimePartContext)result.AsyncState;
			((MimeWriter)mimePartContext.writer).EndClose(result);
			this.Complete(result, null);
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x0007361C File Offset: 0x0007181C
		internal void MimePartSentCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimeMultiPart.MimePartContext)result.AsyncState).completedSynchronously = false;
			try
			{
				this.MimePartSentCallbackHandler(result);
			}
			catch (Exception e)
			{
				this.Complete(result, e);
			}
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x00073668 File Offset: 0x00071868
		private void MimePartSentCallbackHandler(IAsyncResult result)
		{
			MimeMultiPart.MimePartContext mimePartContext = (MimeMultiPart.MimePartContext)result.AsyncState;
			MimeBasePart mimeBasePart = mimePartContext.partsEnumerator.Current;
			mimeBasePart.EndSend(result);
			if (mimePartContext.partsEnumerator.MoveNext())
			{
				mimeBasePart = mimePartContext.partsEnumerator.Current;
				IAsyncResult asyncResult = mimeBasePart.BeginSend(mimePartContext.writer, this.mimePartSentCallback, this.allowUnicode, mimePartContext);
				if (asyncResult.CompletedSynchronously)
				{
					this.MimePartSentCallbackHandler(asyncResult);
				}
				return;
			}
			IAsyncResult asyncResult2 = ((MimeWriter)mimePartContext.writer).BeginClose(new AsyncCallback(this.MimeWriterCloseCallback), mimePartContext);
			if (asyncResult2.CompletedSynchronously)
			{
				this.MimeWriterCloseCallbackHandler(asyncResult2);
			}
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x00073704 File Offset: 0x00071904
		internal void ContentStreamCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimeMultiPart.MimePartContext)result.AsyncState).completedSynchronously = false;
			try
			{
				this.ContentStreamCallbackHandler(result);
			}
			catch (Exception e)
			{
				this.Complete(result, e);
			}
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x00073750 File Offset: 0x00071950
		private void ContentStreamCallbackHandler(IAsyncResult result)
		{
			MimeMultiPart.MimePartContext mimePartContext = (MimeMultiPart.MimePartContext)result.AsyncState;
			mimePartContext.outputStream = mimePartContext.writer.EndGetContentStream(result);
			mimePartContext.writer = new MimeWriter(mimePartContext.outputStream, base.ContentType.Boundary);
			if (mimePartContext.partsEnumerator.MoveNext())
			{
				MimeBasePart mimeBasePart = mimePartContext.partsEnumerator.Current;
				this.mimePartSentCallback = new AsyncCallback(this.MimePartSentCallback);
				IAsyncResult asyncResult = mimeBasePart.BeginSend(mimePartContext.writer, this.mimePartSentCallback, this.allowUnicode, mimePartContext);
				if (asyncResult.CompletedSynchronously)
				{
					this.MimePartSentCallbackHandler(asyncResult);
				}
				return;
			}
			IAsyncResult asyncResult2 = ((MimeWriter)mimePartContext.writer).BeginClose(new AsyncCallback(this.MimeWriterCloseCallback), mimePartContext);
			if (asyncResult2.CompletedSynchronously)
			{
				this.MimeWriterCloseCallbackHandler(asyncResult2);
			}
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x0007381C File Offset: 0x00071A1C
		internal override IAsyncResult BeginSend(BaseWriter writer, AsyncCallback callback, bool allowUnicode, object state)
		{
			this.allowUnicode = allowUnicode;
			base.PrepareHeaders(allowUnicode);
			writer.WriteHeaders(base.Headers, allowUnicode);
			MimeBasePart.MimePartAsyncResult result = new MimeBasePart.MimePartAsyncResult(this, state, callback);
			MimeMultiPart.MimePartContext state2 = new MimeMultiPart.MimePartContext(writer, result, this.Parts.GetEnumerator());
			IAsyncResult asyncResult = writer.BeginGetContentStream(new AsyncCallback(this.ContentStreamCallback), state2);
			if (asyncResult.CompletedSynchronously)
			{
				this.ContentStreamCallbackHandler(asyncResult);
			}
			return result;
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x00073888 File Offset: 0x00071A88
		internal override void Send(BaseWriter writer, bool allowUnicode)
		{
			base.PrepareHeaders(allowUnicode);
			writer.WriteHeaders(base.Headers, allowUnicode);
			Stream contentStream = writer.GetContentStream();
			MimeWriter mimeWriter = new MimeWriter(contentStream, base.ContentType.Boundary);
			foreach (MimeBasePart mimeBasePart in this.Parts)
			{
				mimeBasePart.Send(mimeWriter, allowUnicode);
			}
			mimeWriter.Close();
			contentStream.Close();
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x00073910 File Offset: 0x00071B10
		internal string GetNextBoundary()
		{
			return "--boundary_" + (Interlocked.Increment(ref MimeMultiPart.boundary) - 1).ToString(CultureInfo.InvariantCulture) + "_" + Guid.NewGuid().ToString(null, CultureInfo.InvariantCulture);
		}

		// Token: 0x0400172F RID: 5935
		private Collection<MimeBasePart> parts;

		// Token: 0x04001730 RID: 5936
		private static int boundary;

		// Token: 0x04001731 RID: 5937
		private AsyncCallback mimePartSentCallback;

		// Token: 0x04001732 RID: 5938
		private bool allowUnicode;

		// Token: 0x02000797 RID: 1943
		internal class MimePartContext
		{
			// Token: 0x060042E0 RID: 17120 RVA: 0x00118207 File Offset: 0x00116407
			internal MimePartContext(BaseWriter writer, LazyAsyncResult result, IEnumerator<MimeBasePart> partsEnumerator)
			{
				this.writer = writer;
				this.result = result;
				this.partsEnumerator = partsEnumerator;
			}

			// Token: 0x04003390 RID: 13200
			internal IEnumerator<MimeBasePart> partsEnumerator;

			// Token: 0x04003391 RID: 13201
			internal Stream outputStream;

			// Token: 0x04003392 RID: 13202
			internal LazyAsyncResult result;

			// Token: 0x04003393 RID: 13203
			internal BaseWriter writer;

			// Token: 0x04003394 RID: 13204
			internal bool completed;

			// Token: 0x04003395 RID: 13205
			internal bool completedSynchronously = true;
		}
	}
}
