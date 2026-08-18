using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using TechnoPro.Common.UI.Web.ClockWork.Controls;

namespace ClockWorkWebAPIWeb
{
	// Token: 0x02000015 RID: 21
	public class FileWeb
	{
		// Token: 0x06000129 RID: 297 RVA: 0x0000F508 File Offset: 0x0000D708
		public static byte[] PackageUpFile(FileUpload fu, string encryptionMethodBlankForNoEncryption, string compressionMethodBlankForNoCompression)
		{
			int contentLength = fu.PostedFile.ContentLength;
			byte[] array = new byte[contentLength];
			fu.PostedFile.InputStream.Read(array, 0, contentLength);
			return FileWeb.PackageUpFile(array, fu.FileName, fu.PostedFile.ContentType, "", "");
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000F564 File Offset: 0x0000D764
		public static byte[] PackageUpFile(CtrlSingleFileUpload ctrlSingleFileUpload, string encryptionMethodBlankForNoEncryption, string compressionMethodBlankForNoCompression)
		{
			int contentLength = ctrlSingleFileUpload.ContentLength;
			byte[] array = new byte[contentLength];
			ctrlSingleFileUpload.InputStream.Read(array, 0, contentLength);
			return FileWeb.PackageUpFile(array, ctrlSingleFileUpload.FileName, ctrlSingleFileUpload.ContentType, "", "");
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000F5B0 File Offset: 0x0000D7B0
		public static byte[] PackageUpFile(byte[] FileBytes, string filename, string contentType, string encryptionMethodBlankForNoEncryption, string compressionMethodBlankForNoCompression)
		{
			byte[] array = FileBytes;
			bool flag = compressionMethodBlankForNoCompression.Length > 0;
			if (flag)
			{
				if (compressionMethodBlankForNoCompression == "gzip")
				{
					array = Compression.Compress(FileBytes);
				}
			}
			int num = array.Length;
			bool flag2 = encryptionMethodBlankForNoEncryption.Length > 0;
			if (flag2)
			{
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("filename=");
			stringBuilder.Append(Path.GetFileName(filename));
			stringBuilder.Append(";");
			stringBuilder.Append("filetype=");
			stringBuilder.Append(contentType);
			stringBuilder.Append(";");
			stringBuilder.Append("filesize=");
			stringBuilder.Append(num.ToString());
			stringBuilder.Append(";");
			stringBuilder.Append("emethod=");
			stringBuilder.Append(encryptionMethodBlankForNoEncryption);
			stringBuilder.Append(";");
			stringBuilder.Append("cmethod=");
			stringBuilder.Append(compressionMethodBlankForNoCompression);
			stringBuilder.Append(";");
			string txt = stringBuilder.ToString();
			byte[] array2 = Core.StringToBytes(txt, false, null);
			string text = array2.Length.ToString();
			int num2 = 6 - text.Length;
			bool flag3 = num2 > 0 && num2 < 7;
			if (flag3)
			{
				text = new string('0', num2) + text;
			}
			byte[] array3 = Core.StringToBytes(text, false, null);
			int num3 = array2.Length + array3.Length + array.Length;
			byte[] array4 = new byte[num3];
			array3.CopyTo(array4, 0);
			array2.CopyTo(array4, array3.Length);
			array.CopyTo(array4, array3.Length + array2.Length);
			return array4;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000F760 File Offset: 0x0000D960
		public static byte[] UnpackageFile(byte[] allBytes, out StringDictionary args, out string filename)
		{
			byte[] array = new byte[6];
			for (int i = 0; i < 6; i++)
			{
				array[i] = allBytes[i];
			}
			string s = Core.BytesToString(array, false, null);
			int num = int.Parse(s);
			byte[] array2 = new byte[num];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = allBytes[j + 6];
			}
			string text = Core.BytesToString(array2, false, null);
			byte[] array3 = new byte[allBytes.Length - 6 - num];
			Array.ConstrainedCopy(allBytes, 6 + num, array3, 0, array3.Length);
			string[] array4 = text.Split(new char[]
			{
				';'
			});
			args = new StringDictionary();
			foreach (string text2 in array4)
			{
				int num2 = text2.IndexOf('=');
				bool flag = num2 > 0;
				if (flag)
				{
					args.Add(text2.Substring(0, num2), text2.Substring(num2 + 1));
				}
			}
			string text3 = args["cmethod"];
			string text4 = args["emethod"];
			filename = args["filename"];
			bool flag2 = text4.Length > 0;
			if (flag2)
			{
			}
			bool flag3 = text3.Length > 0;
			if (flag3)
			{
				string text5 = text3;
				string a = text5;
				if (a == "gzip")
				{
					return Compression.Decompress(array3);
				}
			}
			return array3;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000F8E4 File Offset: 0x0000DAE4
		public static void DownloadFile(Page page, HttpResponse Response, string filename, byte[] bytes, bool forceDownload)
		{
			Response.Buffer = false;
			Response.Clear();
			Response.ClearContent();
			Response.ClearHeaders();
			Response.AddHeader("Content-Type", "binary/octet-stream");
			Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + "; size=" + bytes.Length.ToString());
			Response.OutputStream.Write(bytes, 0, bytes.Length);
			Response.Flush();
			Response.Close();
			try
			{
				Response.End();
			}
			catch
			{
			}
		}
	}
}
