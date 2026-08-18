using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000FB RID: 251
	internal class StaticFileHandler : IHttpHandler
	{
		// Token: 0x06000F21 RID: 3873 RVA: 0x000030B5 File Offset: 0x000012B5
		internal StaticFileHandler()
		{
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x0002AC28 File Offset: 0x00028E28
		private static bool IsOutDated(string ifRangeHeader, DateTime lastModified)
		{
			bool result;
			try
			{
				DateTime t = lastModified.ToUniversalTime();
				DateTime t2 = HttpDate.UtcParse(ifRangeHeader);
				result = (t2 < t);
			}
			catch
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x0002AC64 File Offset: 0x00028E64
		private static string GenerateETag(HttpContext context, DateTime lastModified, DateTime now)
		{
			long num = lastModified.ToFileTime();
			long num2 = now.ToFileTime();
			string str = num.ToString("X8", CultureInfo.InvariantCulture);
			if (num2 - num <= 30000000L)
			{
				return "W/\"" + str + "\"";
			}
			return "\"" + str + "\"";
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0002ACC0 File Offset: 0x00028EC0
		private static FileInfo GetFileInfo(string virtualPathWithPathInfo, string physicalPath, HttpResponse response)
		{
			if (!FileUtil.FileExists(physicalPath))
			{
				throw new HttpException(404, SR.GetString("File_does_not_exist"));
			}
			if (physicalPath[physicalPath.Length - 1] == '.')
			{
				throw new HttpException(404, SR.GetString("File_does_not_exist"));
			}
			FileInfo fileInfo;
			try
			{
				fileInfo = new FileInfo(physicalPath);
			}
			catch (IOException innerException)
			{
				if (!HttpRuntime.HasFilePermission(physicalPath))
				{
					throw new HttpException(404, SR.GetString("Error_trying_to_enumerate_files"));
				}
				throw new HttpException(404, SR.GetString("Error_trying_to_enumerate_files"), innerException);
			}
			catch (SecurityException innerException2)
			{
				if (!HttpRuntime.HasFilePermission(physicalPath))
				{
					throw new HttpException(401, SR.GetString("File_enumerator_access_denied"));
				}
				throw new HttpException(401, SR.GetString("File_enumerator_access_denied"), innerException2);
			}
			if ((fileInfo.Attributes & FileAttributes.Hidden) != (FileAttributes)0)
			{
				throw new HttpException(404, SR.GetString("File_is_hidden"));
			}
			if ((fileInfo.Attributes & FileAttributes.Directory) != (FileAttributes)0)
			{
				if (StringUtil.StringEndsWith(virtualPathWithPathInfo, '/'))
				{
					throw new HttpException(403, SR.GetString("Missing_star_mapping"));
				}
				response.Redirect(virtualPathWithPathInfo + "/");
			}
			return fileInfo;
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0002ADFC File Offset: 0x00028FFC
		private static bool GetLongFromSubstring(string s, ref int startIndex, out long result)
		{
			result = 0L;
			StaticFileHandler.MovePastSpaceCharacters(s, ref startIndex);
			int num = startIndex;
			StaticFileHandler.MovePastDigits(s, ref startIndex);
			int num2 = startIndex - 1;
			if (num2 < num)
			{
				return false;
			}
			long num3 = 1L;
			for (int i = num2; i >= num; i--)
			{
				int num4 = (int)(s[i] - '0');
				result += (long)num4 * num3;
				num3 *= 10L;
				if (result < 0L)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0002AE60 File Offset: 0x00029060
		private static bool GetNextRange(string rangeHeader, ref int startIndex, long fileLength, out long offset, out long length, out bool isSatisfiable)
		{
			offset = 0L;
			length = 0L;
			isSatisfiable = false;
			if (fileLength <= 0L)
			{
				startIndex = rangeHeader.Length;
				return true;
			}
			StaticFileHandler.MovePastSpaceCharacters(rangeHeader, ref startIndex);
			if (startIndex < rangeHeader.Length && rangeHeader[startIndex] == '-')
			{
				startIndex++;
				if (!StaticFileHandler.GetLongFromSubstring(rangeHeader, ref startIndex, out length))
				{
					return false;
				}
				if (length > fileLength)
				{
					offset = 0L;
					length = fileLength;
				}
				else
				{
					offset = fileLength - length;
				}
				isSatisfiable = StaticFileHandler.IsRangeSatisfiable(offset, length, fileLength);
				return StaticFileHandler.IncrementToNextRange(rangeHeader, ref startIndex);
			}
			else
			{
				if (!StaticFileHandler.GetLongFromSubstring(rangeHeader, ref startIndex, out offset))
				{
					return false;
				}
				if (startIndex < rangeHeader.Length && rangeHeader[startIndex] == '-')
				{
					startIndex++;
					long num;
					if (!StaticFileHandler.GetLongFromSubstring(rangeHeader, ref startIndex, out num))
					{
						length = fileLength - offset;
					}
					else
					{
						if (num > fileLength - 1L)
						{
							num = fileLength - 1L;
						}
						length = num - offset + 1L;
						if (length < 1L)
						{
							return false;
						}
					}
					isSatisfiable = StaticFileHandler.IsRangeSatisfiable(offset, length, fileLength);
					return StaticFileHandler.IncrementToNextRange(rangeHeader, ref startIndex);
				}
				return false;
			}
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0002AF5A File Offset: 0x0002915A
		private static bool IncrementToNextRange(string s, ref int startIndex)
		{
			StaticFileHandler.MovePastSpaceCharacters(s, ref startIndex);
			if (startIndex < s.Length)
			{
				if (s[startIndex] != ',')
				{
					return false;
				}
				startIndex++;
			}
			return true;
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x0002AF82 File Offset: 0x00029182
		private static bool IsRangeSatisfiable(long offset, long length, long fileLength)
		{
			return offset < fileLength && length > 0L;
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06000F29 RID: 3881 RVA: 0x000097B7 File Offset: 0x000079B7
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0002AF8F File Offset: 0x0002918F
		private static bool IsSecurityError(int ErrorCode)
		{
			return ErrorCode == 5;
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0002AF95 File Offset: 0x00029195
		private static void MovePastSpaceCharacters(string s, ref int startIndex)
		{
			while (startIndex < s.Length && s[startIndex] == ' ')
			{
				startIndex++;
			}
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x0002AFB5 File Offset: 0x000291B5
		private static void MovePastDigits(string s, ref int startIndex)
		{
			while (startIndex < s.Length && s[startIndex] <= '9' && s[startIndex] >= '0')
			{
				startIndex++;
			}
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0002AFE1 File Offset: 0x000291E1
		public void ProcessRequest(HttpContext context)
		{
			StaticFileHandler.ProcessRequestInternal(context, null);
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x0002AFEC File Offset: 0x000291EC
		private static bool ProcessRequestForNonMapPathBasedVirtualFile(HttpRequest request, HttpResponse response, string overrideVirtualPath)
		{
			bool result = false;
			if (HostingEnvironment.UsingMapPathBasedVirtualPathProvider)
			{
				return result;
			}
			VirtualFile virtualFile = null;
			string text = (overrideVirtualPath == null) ? request.FilePath : overrideVirtualPath;
			if (HostingEnvironment.VirtualPathProvider.FileExists(text))
			{
				virtualFile = HostingEnvironment.VirtualPathProvider.GetFile(text);
			}
			if (virtualFile == null)
			{
				throw new HttpException(404, SR.GetString("File_does_not_exist"));
			}
			if (virtualFile is MapPathBasedVirtualFile)
			{
				return result;
			}
			response.WriteVirtualFile(virtualFile);
			response.ContentType = MimeMapping.GetMimeMapping(text);
			return true;
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x0002B064 File Offset: 0x00029264
		internal unsafe static bool ProcessRangeRequest(HttpContext context, string physicalPath, long fileLength, string rangeHeader, string etag, DateTime lastModified)
		{
			HttpRequest request = context.Request;
			HttpResponse response = context.Response;
			bool result = false;
			if (fileLength <= 0L)
			{
				StaticFileHandler.SendRangeNotSatisfiable(response, fileLength);
				return true;
			}
			string text = request.Headers["If-Range"];
			if (text != null && text.Length > 1)
			{
				if (text[0] == '"')
				{
					if (text != etag)
					{
						return result;
					}
				}
				else
				{
					if (text[0] == 'W' && text[1] == '/')
					{
						return result;
					}
					if (StaticFileHandler.IsOutDated(text, lastModified))
					{
						return result;
					}
				}
			}
			int num = rangeHeader.IndexOf('=');
			if (num == -1 || num == rangeHeader.Length - 1)
			{
				return result;
			}
			int num2 = num + 1;
			bool flag = true;
			bool flag2 = false;
			ByteRange[] array = null;
			int num3 = 0;
			long num4 = 0L;
			while (num2 < rangeHeader.Length && flag)
			{
				long offset;
				long length;
				bool flag3;
				flag = StaticFileHandler.GetNextRange(rangeHeader, ref num2, fileLength, out offset, out length, out flag3);
				if (!flag)
				{
					break;
				}
				if (flag3)
				{
					if (array == null)
					{
						array = new ByteRange[16];
					}
					if (num3 >= array.Length)
					{
						ByteRange[] array2 = new ByteRange[array.Length * 2];
						int len = array.Length * Marshal.SizeOf(array[0]);
						ByteRange[] array3;
						ByteRange* src;
						if ((array3 = array) == null || array3.Length == 0)
						{
							src = null;
						}
						else
						{
							src = &array3[0];
						}
						ByteRange[] array4;
						ByteRange* dest;
						if ((array4 = array2) == null || array4.Length == 0)
						{
							dest = null;
						}
						else
						{
							dest = &array4[0];
						}
						StringUtil.memcpyimpl((byte*)src, (byte*)dest, len);
						array3 = null;
						array4 = null;
						array = array2;
					}
					array[num3].Offset = offset;
					array[num3].Length = length;
					num3++;
					num4 += length;
					if (num4 > fileLength * 5L)
					{
						flag2 = true;
						break;
					}
				}
			}
			if (!flag)
			{
				return result;
			}
			if (flag2)
			{
				StaticFileHandler.SendBadRequest(response);
				return true;
			}
			if (num3 == 0)
			{
				StaticFileHandler.SendRangeNotSatisfiable(response, fileLength);
				return true;
			}
			string mimeMapping = MimeMapping.GetMimeMapping(physicalPath);
			if (num3 == 1)
			{
				long offset = array[0].Offset;
				long length = array[0].Length;
				response.ContentType = mimeMapping;
				string value = string.Format(CultureInfo.InvariantCulture, "bytes {0}-{1}/{2}", new object[]
				{
					offset,
					offset + length - 1L,
					fileLength
				});
				response.AppendHeader("Content-Range", value);
				StaticFileHandler.SendFile(physicalPath, offset, length, fileLength, context);
			}
			else
			{
				response.ContentType = "multipart/byteranges; boundary=<q1w2e3r4t5y6u7i8o9p0zaxscdvfbgnhmjklkl>";
				string s = "Content-Type: " + mimeMapping + "\r\n";
				for (int i = 0; i < num3; i++)
				{
					long offset = array[i].Offset;
					long length = array[i].Length;
					response.Write("--<q1w2e3r4t5y6u7i8o9p0zaxscdvfbgnhmjklkl>\r\n");
					response.Write(s);
					response.Write("Content-Range: ");
					string s2 = string.Format(CultureInfo.InvariantCulture, "bytes {0}-{1}/{2}", new object[]
					{
						offset,
						offset + length - 1L,
						fileLength
					});
					response.Write(s2);
					response.Write("\r\n\r\n");
					StaticFileHandler.SendFile(physicalPath, offset, length, fileLength, context);
					response.Write("\r\n");
				}
				response.Write("--<q1w2e3r4t5y6u7i8o9p0zaxscdvfbgnhmjklkl>--\r\n\r\n");
			}
			response.StatusCode = 206;
			response.AppendHeader("Last-Modified", HttpUtility.FormatHttpDateTime(lastModified));
			response.AppendHeader("Accept-Ranges", "bytes");
			response.AppendHeader("ETag", etag);
			response.AppendHeader("Cache-Control", "public");
			return true;
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x0002B3F8 File Offset: 0x000295F8
		internal static void ProcessRequestInternal(HttpContext context, string overrideVirtualPath)
		{
			HttpRequest request = context.Request;
			HttpResponse response = context.Response;
			if (StaticFileHandler.ProcessRequestForNonMapPathBasedVirtualFile(request, response, overrideVirtualPath))
			{
				return;
			}
			string virtualPathWithPathInfo;
			string text;
			if (overrideVirtualPath == null)
			{
				virtualPathWithPathInfo = request.Path;
				text = request.PhysicalPath;
			}
			else
			{
				virtualPathWithPathInfo = overrideVirtualPath;
				text = request.MapPath(overrideVirtualPath);
			}
			FileInfo fileInfo = StaticFileHandler.GetFileInfo(virtualPathWithPathInfo, text, response);
			DateTime dateTime = new DateTime(fileInfo.LastWriteTimeUtc.Year, fileInfo.LastWriteTimeUtc.Month, fileInfo.LastWriteTimeUtc.Day, fileInfo.LastWriteTimeUtc.Hour, fileInfo.LastWriteTimeUtc.Minute, fileInfo.LastWriteTimeUtc.Second, 0, DateTimeKind.Utc);
			DateTime utcNow = DateTime.UtcNow;
			if (dateTime > utcNow)
			{
				dateTime = new DateTime(utcNow.Ticks - utcNow.Ticks % 10000000L, DateTimeKind.Utc);
			}
			string etag = StaticFileHandler.GenerateETag(context, dateTime, utcNow);
			long length = fileInfo.Length;
			string text2 = request.Headers["Range"];
			if (StringUtil.StringStartsWithIgnoreCase(text2, "bytes") && StaticFileHandler.ProcessRangeRequest(context, text, length, text2, etag, dateTime))
			{
				return;
			}
			StaticFileHandler.SendFile(text, 0L, length, length, context);
			response.ContentType = MimeMapping.GetMimeMapping(text);
			response.AppendHeader("Accept-Ranges", "bytes");
			response.AddFileDependency(text);
			response.Cache.SetIgnoreRangeRequests();
			response.Cache.SetExpires(utcNow.AddDays(1.0));
			response.Cache.SetLastModified(dateTime);
			response.Cache.SetETag(etag);
			response.Cache.SetCacheability(HttpCacheability.Public);
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x0002B5A1 File Offset: 0x000297A1
		private static void SendBadRequest(HttpResponse response)
		{
			response.StatusCode = 400;
			response.Write("<html><body>Bad Request</body></html>");
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x0002B5B9 File Offset: 0x000297B9
		private static void SendRangeNotSatisfiable(HttpResponse response, long fileLength)
		{
			response.StatusCode = 416;
			response.ContentType = null;
			response.AppendHeader("Content-Range", "bytes */" + fileLength.ToString(NumberFormatInfo.InvariantInfo));
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x0002B5F0 File Offset: 0x000297F0
		private static void SendFile(string physicalPath, long offset, long length, long fileLength, HttpContext context)
		{
			try
			{
				HttpRuntime.CheckFilePermission(physicalPath);
				context.Response.TransmitFile(physicalPath, offset, length);
			}
			catch (ExternalException ex)
			{
				if (StaticFileHandler.IsSecurityError(ex.ErrorCode))
				{
					throw new HttpException(401, SR.GetString("Resource_access_forbidden"));
				}
				throw;
			}
		}

		// Token: 0x040005CB RID: 1483
		private const string RANGE_BOUNDARY = "<q1w2e3r4t5y6u7i8o9p0zaxscdvfbgnhmjklkl>";

		// Token: 0x040005CC RID: 1484
		private const string MULTIPART_CONTENT_TYPE = "multipart/byteranges; boundary=<q1w2e3r4t5y6u7i8o9p0zaxscdvfbgnhmjklkl>";

		// Token: 0x040005CD RID: 1485
		private const string MULTIPART_RANGE_DELIMITER = "--<q1w2e3r4t5y6u7i8o9p0zaxscdvfbgnhmjklkl>\r\n";

		// Token: 0x040005CE RID: 1486
		private const string MULTIPART_RANGE_END = "--<q1w2e3r4t5y6u7i8o9p0zaxscdvfbgnhmjklkl>--\r\n\r\n";

		// Token: 0x040005CF RID: 1487
		private const string CONTENT_RANGE_FORMAT = "bytes {0}-{1}/{2}";

		// Token: 0x040005D0 RID: 1488
		private const int MAX_RANGE_ALLOWED = 5;

		// Token: 0x040005D1 RID: 1489
		private const int ERROR_ACCESS_DENIED = 5;
	}
}
