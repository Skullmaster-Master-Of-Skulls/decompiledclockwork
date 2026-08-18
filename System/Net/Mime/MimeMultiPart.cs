using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;

namespace System.Net.Mime
{
	// Token: 0x020006AB RID: 1707
	internal class MimeMultiPart : MimeBasePart
	{
		// Token: 0x060034BB RID: 13499 RVA: 0x000DFD95 File Offset: 0x000DED95
		internal MimeMultiPart(MimeMultiPartType type)
		{
			this.MimeMultiPartType = type;
		}

		// Token: 0x17000C58 RID: 3160
		// (set) Token: 0x060034BC RID: 13500 RVA: 0x000DFDA4 File Offset: 0x000DEDA4
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

		// Token: 0x060034BD RID: 13501 RVA: 0x000DFDC6 File Offset: 0x000DEDC6
		private void SetType(MimeMultiPartType type)
		{
			base.ContentType.MediaType = "multipart/" + type.ToString().ToLower(CultureInfo.InvariantCulture);
			base.ContentType.Boundary = this.GetNextBoundary();
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x060034BE RID: 13502 RVA: 0x000DFE03 File Offset: 0x000DEE03
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

		// Token: 0x060034BF RID: 13503 RVA: 0x000DFE20 File Offset: 0x000DEE20
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
			catch
			{
				if (e == null)
				{
					e = new Exception(SR.GetString("net_nonClsCompliantException"));
				}
			}
			mimePartContext.completed = true;
			mimePartContext.result.InvokeCallback(e);
		}

		// Token: 0x060034C0 RID: 13504 RVA: 0x000DFEA0 File Offset: 0x000DEEA0
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
			catch
			{
				this.Complete(result, new Exception(SR.GetString("net_nonClsCompliantException")));
			}
		}

		// Token: 0x060034C1 RID: 13505 RVA: 0x000DFF10 File Offset: 0x000DEF10
		private void MimeWriterCloseCallbackHandler(IAsyncResult result)
		{
			MimeMultiPart.MimePartContext mimePartContext = (MimeMultiPart.MimePartContext)result.AsyncState;
			((MimeWriter)mimePartContext.writer).EndClose(result);
			this.Complete(result, null);
		}

		// Token: 0x060034C2 RID: 13506 RVA: 0x000DFF44 File Offset: 0x000DEF44
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
			catch
			{
				this.Complete(result, new Exception(SR.GetString("net_nonClsCompliantException")));
			}
		}

		// Token: 0x060034C3 RID: 13507 RVA: 0x000DFFB4 File Offset: 0x000DEFB4
		private void MimePartSentCallbackHandler(IAsyncResult result)
		{
			MimeMultiPart.MimePartContext mimePartContext = (MimeMultiPart.MimePartContext)result.AsyncState;
			MimeBasePart mimeBasePart = mimePartContext.partsEnumerator.Current;
			mimeBasePart.EndSend(result);
			if (mimePartContext.partsEnumerator.MoveNext())
			{
				mimeBasePart = mimePartContext.partsEnumerator.Current;
				IAsyncResult asyncResult = mimeBasePart.BeginSend(mimePartContext.writer, this.mimePartSentCallback, mimePartContext);
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

		// Token: 0x060034C4 RID: 13508 RVA: 0x000E004C File Offset: 0x000DF04C
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
			catch
			{
				this.Complete(result, new Exception(SR.GetString("net_nonClsCompliantException")));
			}
		}

		// Token: 0x060034C5 RID: 13509 RVA: 0x000E00BC File Offset: 0x000DF0BC
		private void ContentStreamCallbackHandler(IAsyncResult result)
		{
			MimeMultiPart.MimePartContext mimePartContext = (MimeMultiPart.MimePartContext)result.AsyncState;
			mimePartContext.outputStream = mimePartContext.writer.EndGetContentStream(result);
			mimePartContext.writer = new MimeWriter(mimePartContext.outputStream, base.ContentType.Boundary);
			if (mimePartContext.partsEnumerator.MoveNext())
			{
				MimeBasePart mimeBasePart = mimePartContext.partsEnumerator.Current;
				this.mimePartSentCallback = new AsyncCallback(this.MimePartSentCallback);
				IAsyncResult asyncResult = mimeBasePart.BeginSend(mimePartContext.writer, this.mimePartSentCallback, mimePartContext);
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

		// Token: 0x060034C6 RID: 13510 RVA: 0x000E0180 File Offset: 0x000DF180
		internal override IAsyncResult BeginSend(BaseWriter writer, AsyncCallback callback, object state)
		{
			writer.WriteHeaders(base.Headers);
			MimeBasePart.MimePartAsyncResult result = new MimeBasePart.MimePartAsyncResult(this, state, callback);
			MimeMultiPart.MimePartContext state2 = new MimeMultiPart.MimePartContext(writer, result, this.Parts.GetEnumerator());
			IAsyncResult asyncResult = writer.BeginGetContentStream(new AsyncCallback(this.ContentStreamCallback), state2);
			if (asyncResult.CompletedSynchronously)
			{
				this.ContentStreamCallbackHandler(asyncResult);
			}
			return result;
		}

		// Token: 0x060034C7 RID: 13511 RVA: 0x000E01DC File Offset: 0x000DF1DC
		internal override void Send(BaseWriter writer)
		{
			writer.WriteHeaders(base.Headers);
			Stream contentStream = writer.GetContentStream();
			MimeWriter mimeWriter = new MimeWriter(contentStream, base.ContentType.Boundary);
			foreach (MimeBasePart mimeBasePart in this.Parts)
			{
				mimeBasePart.Send(mimeWriter);
			}
			mimeWriter.Close();
			contentStream.Close();
		}

		// Token: 0x060034C8 RID: 13512 RVA: 0x000E025C File Offset: 0x000DF25C
		internal string GetNextBoundary()
		{
			string result = "--boundary_" + MimeMultiPart.boundary.ToString(CultureInfo.InvariantCulture) + "_" + Guid.NewGuid().ToString(null, CultureInfo.InvariantCulture);
			MimeMultiPart.boundary++;
			return result;
		}

		// Token: 0x04003077 RID: 12407
		private Collection<MimeBasePart> parts;

		// Token: 0x04003078 RID: 12408
		private static int boundary;

		// Token: 0x04003079 RID: 12409
		private AsyncCallback mimePartSentCallback;

		// Token: 0x020006AC RID: 1708
		internal class MimePartContext
		{
			// Token: 0x060034C9 RID: 13513 RVA: 0x000E02A8 File Offset: 0x000DF2A8
			internal MimePartContext(BaseWriter writer, LazyAsyncResult result, IEnumerator<MimeBasePart> partsEnumerator)
			{
				this.writer = writer;
				this.result = result;
				this.partsEnumerator = partsEnumerator;
			}

			// Token: 0x0400307A RID: 12410
			internal IEnumerator<MimeBasePart> partsEnumerator;

			// Token: 0x0400307B RID: 12411
			internal Stream outputStream;

			// Token: 0x0400307C RID: 12412
			internal LazyAsyncResult result;

			// Token: 0x0400307D RID: 12413
			internal BaseWriter writer;

			// Token: 0x0400307E RID: 12414
			internal bool completed;

			// Token: 0x0400307F RID: 12415
			internal bool completedSynchronously = true;
		}
	}
}
