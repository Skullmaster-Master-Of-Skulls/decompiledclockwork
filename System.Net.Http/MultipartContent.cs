using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.Http
{
	// Token: 0x02000007 RID: 7
	[__DynamicallyInvokable]
	public class MultipartContent : HttpContent, IEnumerable<HttpContent>, IEnumerable
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00002621 File Offset: 0x00000821
		[__DynamicallyInvokable]
		public MultipartContent() : this("mixed", MultipartContent.GetDefaultBoundary())
		{
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002633 File Offset: 0x00000833
		[__DynamicallyInvokable]
		public MultipartContent(string subtype) : this(subtype, MultipartContent.GetDefaultBoundary())
		{
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002644 File Offset: 0x00000844
		[__DynamicallyInvokable]
		public MultipartContent(string subtype, string boundary)
		{
			if (string.IsNullOrWhiteSpace(subtype))
			{
				throw new ArgumentException(SR.net_http_argument_empty_string, "subtype");
			}
			MultipartContent.ValidateBoundary(boundary);
			this.boundary = boundary;
			string text = boundary;
			if (!text.StartsWith("\"", StringComparison.Ordinal))
			{
				text = "\"" + text + "\"";
			}
			MediaTypeHeaderValue mediaTypeHeaderValue = new MediaTypeHeaderValue("multipart/" + subtype);
			mediaTypeHeaderValue.Parameters.Add(new NameValueHeaderValue("boundary", text));
			base.Headers.ContentType = mediaTypeHeaderValue;
			this.nestedContent = new List<HttpContent>();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000026DC File Offset: 0x000008DC
		private static void ValidateBoundary(string boundary)
		{
			if (string.IsNullOrWhiteSpace(boundary))
			{
				throw new ArgumentException(SR.net_http_argument_empty_string, "boundary");
			}
			if (boundary.Length > 70)
			{
				throw new ArgumentOutOfRangeException("boundary", boundary, string.Format(CultureInfo.InvariantCulture, SR.net_http_content_field_too_long, new object[]
				{
					70
				}));
			}
			if (boundary.EndsWith(" ", StringComparison.Ordinal))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, SR.net_http_headers_invalid_value, new object[]
				{
					boundary
				}), "boundary");
			}
			string text = "'()+_,-./:=? ";
			foreach (char c in boundary)
			{
				if (('0' > c || c > '9') && ('a' > c || c > 'z') && ('A' > c || c > 'Z') && text.IndexOf(c) < 0)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, SR.net_http_headers_invalid_value, new object[]
					{
						boundary
					}), "boundary");
				}
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000027D8 File Offset: 0x000009D8
		private static string GetDefaultBoundary()
		{
			return Guid.NewGuid().ToString();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000027F8 File Offset: 0x000009F8
		[__DynamicallyInvokable]
		public virtual void Add(HttpContent content)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			this.nestedContent.Add(content);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002814 File Offset: 0x00000A14
		[__DynamicallyInvokable]
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				foreach (HttpContent httpContent in this.nestedContent)
				{
					httpContent.Dispose();
				}
				this.nestedContent.Clear();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000287C File Offset: 0x00000A7C
		[__DynamicallyInvokable]
		public IEnumerator<HttpContent> GetEnumerator()
		{
			return this.nestedContent.GetEnumerator();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000288E File Offset: 0x00000A8E
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.nestedContent.GetEnumerator();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000028A0 File Offset: 0x00000AA0
		[__DynamicallyInvokable]
		protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>();
			this.tcs = taskCompletionSource;
			this.outputStream = stream;
			this.nextContentIndex = 0;
			MultipartContent.EncodeStringToStreamAsync(this.outputStream, "--" + this.boundary + "\r\n").ContinueWithStandard(new Action<Task>(this.WriteNextContentHeadersAsync));
			return taskCompletionSource.Task;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002900 File Offset: 0x00000B00
		private void WriteNextContentHeadersAsync(Task task)
		{
			if (task.IsFaulted)
			{
				this.HandleAsyncException("WriteNextContentHeadersAsync", task.Exception.GetBaseException());
				return;
			}
			try
			{
				if (this.nextContentIndex >= this.nestedContent.Count)
				{
					this.WriteTerminatingBoundaryAsync();
				}
				else
				{
					string value = "\r\n--" + this.boundary + "\r\n";
					StringBuilder stringBuilder = new StringBuilder();
					if (this.nextContentIndex != 0)
					{
						stringBuilder.Append(value);
					}
					HttpContent httpContent = this.nestedContent[this.nextContentIndex];
					foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in httpContent.Headers)
					{
						stringBuilder.Append(keyValuePair.Key + ": " + string.Join(", ", keyValuePair.Value) + "\r\n");
					}
					stringBuilder.Append("\r\n");
					MultipartContent.EncodeStringToStreamAsync(this.outputStream, stringBuilder.ToString()).ContinueWithStandard(new Action<Task>(this.WriteNextContentAsync));
				}
			}
			catch (Exception ex)
			{
				this.HandleAsyncException("WriteNextContentHeadersAsync", ex);
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002A40 File Offset: 0x00000C40
		private void WriteNextContentAsync(Task task)
		{
			if (task.IsFaulted)
			{
				this.HandleAsyncException("WriteNextContentAsync", task.Exception.GetBaseException());
				return;
			}
			try
			{
				HttpContent httpContent = this.nestedContent[this.nextContentIndex];
				this.nextContentIndex++;
				httpContent.CopyToAsync(this.outputStream).ContinueWithStandard(new Action<Task>(this.WriteNextContentHeadersAsync));
			}
			catch (Exception ex)
			{
				this.HandleAsyncException("WriteNextContentAsync", ex);
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002ACC File Offset: 0x00000CCC
		private void WriteTerminatingBoundaryAsync()
		{
			try
			{
				MultipartContent.EncodeStringToStreamAsync(this.outputStream, "\r\n--" + this.boundary + "--\r\n").ContinueWithStandard(delegate(Task task)
				{
					if (task.IsFaulted)
					{
						this.HandleAsyncException("WriteTerminatingBoundaryAsync", task.Exception.GetBaseException());
						return;
					}
					TaskCompletionSource<object> taskCompletionSource = this.CleanupAsync();
					taskCompletionSource.TrySetResult(null);
				});
			}
			catch (Exception ex)
			{
				this.HandleAsyncException("WriteTerminatingBoundaryAsync", ex);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002B2C File Offset: 0x00000D2C
		private static Task EncodeStringToStreamAsync(Stream stream, string input)
		{
			byte[] bytes = HttpRuleParser.DefaultHttpEncoding.GetBytes(input);
			return Task.Factory.FromAsync<byte[], int, int>(new Func<byte[], int, int, AsyncCallback, object, IAsyncResult>(stream.BeginWrite), new Action<IAsyncResult>(stream.EndWrite), bytes, 0, bytes.Length, null);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002B70 File Offset: 0x00000D70
		private TaskCompletionSource<object> CleanupAsync()
		{
			TaskCompletionSource<object> result = this.tcs;
			this.outputStream = null;
			this.nextContentIndex = 0;
			this.tcs = null;
			return result;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002B9C File Offset: 0x00000D9C
		private void HandleAsyncException(string method, Exception ex)
		{
			if (Logging.On)
			{
				Logging.Exception(Logging.Http, this, method, ex);
			}
			TaskCompletionSource<object> taskCompletionSource = this.CleanupAsync();
			taskCompletionSource.TrySetException(ex);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002BCC File Offset: 0x00000DCC
		[__DynamicallyInvokable]
		protected internal override bool TryComputeLength(out long length)
		{
			long num = 0L;
			long num2 = (long)MultipartContent.GetEncodedLength("\r\n--" + this.boundary + "\r\n");
			num += (long)MultipartContent.GetEncodedLength("--" + this.boundary + "\r\n");
			bool flag = true;
			foreach (HttpContent httpContent in this.nestedContent)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					num += num2;
				}
				foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in httpContent.Headers)
				{
					num += (long)MultipartContent.GetEncodedLength(keyValuePair.Key + ": " + string.Join(", ", keyValuePair.Value) + "\r\n");
				}
				num += (long)"\r\n".Length;
				long num3 = 0L;
				if (!httpContent.TryComputeLength(out num3))
				{
					length = 0L;
					return false;
				}
				num += num3;
			}
			num += (long)MultipartContent.GetEncodedLength("\r\n--" + this.boundary + "--\r\n");
			length = num;
			return true;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002D28 File Offset: 0x00000F28
		private static int GetEncodedLength(string input)
		{
			return HttpRuleParser.DefaultHttpEncoding.GetByteCount(input);
		}

		// Token: 0x04000052 RID: 82
		private const string crlf = "\r\n";

		// Token: 0x04000053 RID: 83
		private List<HttpContent> nestedContent;

		// Token: 0x04000054 RID: 84
		private string boundary;

		// Token: 0x04000055 RID: 85
		private int nextContentIndex;

		// Token: 0x04000056 RID: 86
		private Stream outputStream;

		// Token: 0x04000057 RID: 87
		private TaskCompletionSource<object> tcs;
	}
}
