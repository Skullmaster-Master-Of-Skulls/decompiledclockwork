using System;
using System.IO;
using System.Text;
using System.Web;

namespace AjaxControlToolkit
{
	// Token: 0x02000023 RID: 35
	public static class AjaxFileUploadHelper
	{
		// Token: 0x0600017A RID: 378 RVA: 0x00005B67 File Offset: 0x00003D67
		public static void Abort(HttpContext context, string fileId)
		{
			new AjaxFileUploadStates(context, fileId).Abort = true;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00005B78 File Offset: 0x00003D78
		public static bool Process(HttpContext context)
		{
			HttpRequest request = context.Request;
			string fileId = request.QueryString["fileId"];
			string fileName = request.QueryString["fileName"];
			bool chunked = bool.Parse(request.QueryString["chunked"] ?? "false");
			bool isFirstChunk = bool.Parse(request.QueryString["firstChunk"] ?? "false");
			bool usePoll = bool.Parse(request.QueryString["usePoll"] ?? "false");
			bool result;
			using (Stream bufferlessInputStream = request.GetBufferlessInputStream())
			{
				bool flag = AjaxFileUploadHelper.ProcessStream(context, bufferlessInputStream, fileId, fileName, chunked, isFirstChunk, usePoll);
				if (!flag)
				{
					request.Form.Clear();
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00005C5C File Offset: 0x00003E5C
		public static bool ProcessStream(HttpContext context, Stream source, string fileId, string fileName, bool chunked, bool isFirstChunk, bool usePoll)
		{
			FileHeaderInfo fileHeaderInfo = null;
			Stream stream = null;
			AjaxFileUploadStates ajaxFileUploadStates = new AjaxFileUploadStates(context, fileId);
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int num = 0;
				bool flag = false;
				int num2 = 0;
				while (!ajaxFileUploadStates.Abort)
				{
					int num3 = usePoll ? 65536 : 4194304;
					if ((long)num3 > source.Length)
					{
						num3 = (int)source.Length;
						if (usePoll)
						{
							ajaxFileUploadStates.FileLength = num3;
						}
					}
					byte[] array = new byte[num3];
					int i;
					int num4;
					for (i = 0; i < array.Length; i += num4)
					{
						num4 = source.Read(array, i, array.Length - i);
						if (num4 == 0)
						{
							break;
						}
						if (usePoll)
						{
							ajaxFileUploadStates.Uploaded += num4;
						}
					}
					num += i;
					if (i != 0)
					{
						if (fileHeaderInfo == null)
						{
							memoryStream.Write(array, 0, i);
							byte[] array2 = memoryStream.ToArray();
							fileHeaderInfo = MultipartFormDataParser.ParseHeaderInfo(array2, Encoding.UTF8);
							if (fileHeaderInfo != null)
							{
								num2 = (int)(source.Length - (long)fileHeaderInfo.BoundaryDelimiterLength) - fileHeaderInfo.StartIndex;
								if (usePoll)
								{
									ajaxFileUploadStates.FileLength = num2;
								}
								int num5 = num - fileHeaderInfo.StartIndex;
								if (num5 > num2)
								{
									num5 = num2;
									flag = true;
								}
								byte[] array3 = new byte[num5];
								Buffer.BlockCopy(array2, fileHeaderInfo.StartIndex, array3, 0, num5);
								string text = AjaxFileUpload.BuildTempFolder(fileId);
								if (!Directory.Exists(text))
								{
									Directory.CreateDirectory(text);
								}
								string path = Path.Combine(text, fileName);
								if (!AjaxFileUploadHelper.IsSubDirectory(AjaxFileUpload.BuildRootTempFolder(), Path.GetDirectoryName(path)))
								{
									throw new Exception("Insecure operation prevented");
								}
								if (!chunked || isFirstChunk)
								{
									stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
								}
								else
								{
									stream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
								}
								stream.Write(array3, 0, num5);
							}
						}
						else
						{
							int num6 = i;
							if (stream.Length + (long)i > (long)num2)
							{
								num6 -= fileHeaderInfo.BoundaryDelimiterLength;
								flag = true;
							}
							stream.Write(array, 0, num6);
						}
					}
					if (flag || i != array.Length)
					{
						if (stream != null)
						{
							stream.Close();
							stream.Dispose();
						}
						return true;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005E98 File Offset: 0x00004098
		private static bool IsSubDirectory(string parentDirectory, string childDirectory)
		{
			for (DirectoryInfo parent = new DirectoryInfo(childDirectory).Parent; parent != null; parent = parent.Parent)
			{
				if (parent.FullName == parentDirectory)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000064 RID: 100
		private const int ChunkSize = 4194304;

		// Token: 0x04000065 RID: 101
		private const int ChunkSizeForPolling = 65536;
	}
}
