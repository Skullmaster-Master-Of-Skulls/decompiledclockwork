using System;
using System.IO;
using System.Windows.Forms;

namespace ImportExportClassLibrary
{
	// Token: 0x02000028 RID: 40
	public class BinaryFile
	{
		// Token: 0x06000134 RID: 308 RVA: 0x00009461 File Offset: 0x00008461
		public static string CreateTemporaryFile(string fileName, string base64Text)
		{
			return BinaryFile.CreateTemporaryFile(fileName, base64Text, ".doc", false);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00009470 File Offset: 0x00008470
		public static string CreateTemporaryFile(string fileName, string base64Text, bool dontAskUserForReplaceFileIfMissing)
		{
			return BinaryFile.CreateTemporaryFile(fileName, base64Text, ".doc", dontAskUserForReplaceFileIfMissing);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000947F File Offset: 0x0000847F
		public static string CreateTemporaryFile(string fileName, string base64Text, string extension)
		{
			return BinaryFile.CreateTemporaryFile(fileName, base64Text, extension, false);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000948C File Offset: 0x0000848C
		public static string CreateTemporaryFile(string fileName, string base64Text, string extension, bool dontAskUserForReplaceFileIfMissing)
		{
			if (fileName.Length > 0)
			{
				extension = Path.GetExtension(fileName);
			}
			string tempFilename;
			if (base64Text.Length > 0)
			{
				tempFilename = TemplatesClass.GetTempFilename((fileName != null && fileName.Length > 0) ? Path.GetExtension(fileName) : extension);
				using (FileStream fileStream = File.Create(tempFilename))
				{
					byte[] array = Convert.FromBase64String(base64Text);
					fileStream.Write(array, 0, array.Length);
					return tempFilename;
				}
			}
			if (dontAskUserForReplaceFileIfMissing)
			{
				return null;
			}
			OpenFileDialog openFileDialog = new OpenFileDialog();
			DialogResult dialogResult = openFileDialog.ShowDialog();
			if (dialogResult != DialogResult.OK)
			{
				return null;
			}
			tempFilename = TemplatesClass.GetTempFilename((fileName != null && fileName.Length > 0) ? Path.GetExtension(fileName) : Path.GetExtension(openFileDialog.FileName));
			File.Copy(openFileDialog.FileName, tempFilename, true);
			return tempFilename;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00009554 File Offset: 0x00008554
		public static string ConvertFileToBase64Text(string fileName)
		{
			string result;
			using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
			{
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, (int)fileStream.Length);
				result = Convert.ToBase64String(array);
			}
			return result;
		}
	}
}
