using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace System.Runtime.Remoting.Channels.Http
{
	// Token: 0x0200002F RID: 47
	internal abstract class HttpSocketHandler : SocketHandler
	{
		// Token: 0x0600018C RID: 396 RVA: 0x00008308 File Offset: 0x00007308
		public HttpSocketHandler(Socket socket, RequestQueue requestQueue, Stream stream) : base(socket, requestQueue, stream)
		{
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00008314 File Offset: 0x00007314
		protected void ReadToEndOfHeaders(BaseTransportHeaders headers, out bool bChunked, out int contentLength, ref bool bKeepAlive, ref bool bSendContinue)
		{
			bChunked = false;
			contentLength = 0;
			for (;;)
			{
				string text = base.ReadToEndOfLine();
				if (text.Length == 0)
				{
					break;
				}
				int num = text.IndexOf(":");
				string text2 = text.Substring(0, num);
				string text3 = text.Substring(num + 1 + 1);
				if (string.Compare(text2, "Transfer-Encoding", StringComparison.OrdinalIgnoreCase) == 0)
				{
					if (string.Compare(text3, "chunked", StringComparison.OrdinalIgnoreCase) == 0)
					{
						bChunked = true;
					}
				}
				else if (string.Compare(text2, "Connection", StringComparison.OrdinalIgnoreCase) == 0)
				{
					if (string.Compare(text3, "Keep-Alive", StringComparison.OrdinalIgnoreCase) == 0)
					{
						bKeepAlive = true;
					}
					else if (string.Compare(text3, "Close", StringComparison.OrdinalIgnoreCase) == 0)
					{
						bKeepAlive = false;
					}
				}
				else if (string.Compare(text2, "Expect", StringComparison.OrdinalIgnoreCase) == 0)
				{
					if (string.Compare(text3, "100-continue", StringComparison.OrdinalIgnoreCase) == 0)
					{
						bSendContinue = true;
					}
				}
				else if (string.Compare(text2, "Content-Length", StringComparison.OrdinalIgnoreCase) == 0)
				{
					contentLength = int.Parse(text3, CultureInfo.InvariantCulture);
				}
				else
				{
					headers[text2] = text3;
				}
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000840C File Offset: 0x0000740C
		protected void WriteHeaders(ITransportHeaders headers, Stream outputStream)
		{
			if (headers == null)
			{
				return;
			}
			foreach (object obj in headers)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = (string)dictionaryEntry.Key;
				if (!text.StartsWith("__", StringComparison.Ordinal))
				{
					this.WriteHeader(text, (string)dictionaryEntry.Value, outputStream);
				}
			}
			outputStream.Write(HttpSocketHandler.s_endOfLine, 0, HttpSocketHandler.s_endOfLine.Length);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000084A0 File Offset: 0x000074A0
		private void WriteHeader(string name, string value, Stream outputStream)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(name);
			byte[] bytes2 = Encoding.ASCII.GetBytes(value);
			outputStream.Write(bytes, 0, bytes.Length);
			outputStream.Write(HttpSocketHandler.s_headerSeparator, 0, HttpSocketHandler.s_headerSeparator.Length);
			outputStream.Write(bytes2, 0, bytes2.Length);
			outputStream.Write(HttpSocketHandler.s_endOfLine, 0, HttpSocketHandler.s_endOfLine.Length);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00008504 File Offset: 0x00007504
		protected void WriteResponseFirstLine(string statusCode, string reasonPhrase, Stream outputStream)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(statusCode);
			byte[] bytes2 = Encoding.ASCII.GetBytes(reasonPhrase);
			outputStream.Write(HttpSocketHandler.s_httpVersionAndSpace, 0, HttpSocketHandler.s_httpVersionAndSpace.Length);
			outputStream.Write(bytes, 0, bytes.Length);
			outputStream.WriteByte(32);
			outputStream.Write(bytes2, 0, bytes2.Length);
			outputStream.Write(HttpSocketHandler.s_endOfLine, 0, HttpSocketHandler.s_endOfLine.Length);
		}

		// Token: 0x04000134 RID: 308
		private static byte[] s_httpVersion = Encoding.ASCII.GetBytes("HTTP/1.1");

		// Token: 0x04000135 RID: 309
		private static byte[] s_httpVersionAndSpace = Encoding.ASCII.GetBytes("HTTP/1.1 ");

		// Token: 0x04000136 RID: 310
		private static byte[] s_headerSeparator = new byte[]
		{
			58,
			32
		};

		// Token: 0x04000137 RID: 311
		private static byte[] s_endOfLine = new byte[]
		{
			13,
			10
		};
	}
}
