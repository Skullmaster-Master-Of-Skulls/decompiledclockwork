using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.Security.Cryptography;
using System.Web.UI;

namespace System.Web.Util
{
	// Token: 0x02000203 RID: 515
	internal class HashCodeCombiner
	{
		// Token: 0x0600193E RID: 6462 RVA: 0x0004E53E File Offset: 0x0004C73E
		internal HashCodeCombiner()
		{
			this._combinedHash = 5381L;
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x0004E552 File Offset: 0x0004C752
		internal HashCodeCombiner(long initialCombinedHash)
		{
			this._combinedHash = initialCombinedHash;
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x0004E561 File Offset: 0x0004C761
		internal static int CombineHashCodes(int h1, int h2)
		{
			return (h1 << 5) + h1 ^ h2;
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x0004E56A File Offset: 0x0004C76A
		internal static int CombineHashCodes(int h1, int h2, int h3)
		{
			return HashCodeCombiner.CombineHashCodes(HashCodeCombiner.CombineHashCodes(h1, h2), h3);
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x0004E579 File Offset: 0x0004C779
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4)
		{
			return HashCodeCombiner.CombineHashCodes(HashCodeCombiner.CombineHashCodes(h1, h2), HashCodeCombiner.CombineHashCodes(h3, h4));
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x0004E58E File Offset: 0x0004C78E
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5)
		{
			return HashCodeCombiner.CombineHashCodes(HashCodeCombiner.CombineHashCodes(h1, h2, h3, h4), h5);
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x0004E5A0 File Offset: 0x0004C7A0
		internal static string GetDirectoryHash(VirtualPath virtualDir)
		{
			HashCodeCombiner hashCodeCombiner = new HashCodeCombiner();
			hashCodeCombiner.AddDirectory(virtualDir.MapPathInternal());
			return hashCodeCombiner.CombinedHashString;
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x0004E5C8 File Offset: 0x0004C7C8
		internal void AddArray(string[] a)
		{
			if (a != null)
			{
				int num = a.Length;
				for (int i = 0; i < num; i++)
				{
					this.AddObject(a[i]);
				}
			}
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x0004E5F1 File Offset: 0x0004C7F1
		internal void AddInt(int n)
		{
			this._combinedHash = ((this._combinedHash << 5) + this._combinedHash ^ (long)n);
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x0004E60B File Offset: 0x0004C80B
		internal void AddObject(int n)
		{
			this.AddInt(n);
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x0004E614 File Offset: 0x0004C814
		internal void AddObject(byte b)
		{
			this.AddInt(b.GetHashCode());
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x0004E623 File Offset: 0x0004C823
		internal void AddObject(long l)
		{
			this.AddInt(l.GetHashCode());
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x0004E632 File Offset: 0x0004C832
		internal void AddObject(bool b)
		{
			this.AddInt(b.GetHashCode());
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x0004E641 File Offset: 0x0004C841
		internal void AddObject(string s)
		{
			if (s != null)
			{
				this.AddInt(StringUtil.GetStringHashCode(s));
			}
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x0004E652 File Offset: 0x0004C852
		internal void AddObject(Type t)
		{
			if (t != null)
			{
				this.AddObject(Util.GetAssemblyQualifiedTypeName(t));
			}
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x0004E669 File Offset: 0x0004C869
		internal void AddObject(object o)
		{
			if (o != null)
			{
				this.AddInt(o.GetHashCode());
			}
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x0004E67A File Offset: 0x0004C87A
		internal void AddCaseInsensitiveString(string s)
		{
			if (s != null)
			{
				this.AddInt(StringUtil.GetNonRandomizedHashCode(s, true));
			}
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x0004E68C File Offset: 0x0004C88C
		internal void AddDateTime(DateTime dt)
		{
			this.AddInt(dt.GetHashCode());
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x0004E623 File Offset: 0x0004C823
		private void AddFileSize(long fileSize)
		{
			this.AddInt(fileSize.GetHashCode());
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x0004E69C File Offset: 0x0004C89C
		private void AddFileVersionInfo(FileVersionInfo fileVersionInfo)
		{
			this.AddInt(fileVersionInfo.FileMajorPart.GetHashCode());
			this.AddInt(fileVersionInfo.FileMinorPart.GetHashCode());
			this.AddInt(fileVersionInfo.FileBuildPart.GetHashCode());
			this.AddInt(fileVersionInfo.FilePrivatePart.GetHashCode());
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x0004E6F9 File Offset: 0x0004C8F9
		private void AddFileContentHashKey(string fileContentHashKey)
		{
			this.AddInt(StringUtil.GetNonRandomizedHashCode(fileContentHashKey, false));
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x0004E708 File Offset: 0x0004C908
		internal void AddFileContentHash(string fileName)
		{
			byte[] input = File.ReadAllBytes(fileName);
			byte[] array = CryptoUtil.ComputeSHA256Hash(input);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("X2", CultureInfo.InvariantCulture));
			}
			this.AddFileContentHashKey(stringBuilder.ToString());
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x0004E760 File Offset: 0x0004C960
		internal void AddFile(string fileName)
		{
			if (FileUtil.FileExists(fileName))
			{
				this.AddExistingFile(fileName);
				return;
			}
			if (FileUtil.DirectoryExists(fileName))
			{
				this.AddDirectory(fileName);
				return;
			}
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x0004E784 File Offset: 0x0004C984
		private void AddExistingFile(string fileName)
		{
			this.AddInt(StringUtil.GetStringHashCode(fileName));
			FileInfo fileInfo = new FileInfo(fileName);
			if (!AppSettings.PortableCompilationOutput)
			{
				this.AddDateTime(fileInfo.CreationTimeUtc);
			}
			this.AddDateTime(fileInfo.LastWriteTimeUtc);
			this.AddFileSize(fileInfo.Length);
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x0004E7D0 File Offset: 0x0004C9D0
		internal void AddExistingFileVersion(string fileName)
		{
			this.AddInt(StringUtil.GetStringHashCode(fileName));
			FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(fileName);
			this.AddFileVersionInfo(versionInfo);
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x0004E7F8 File Offset: 0x0004C9F8
		internal void AddDirectory(string directoryName)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(directoryName);
			if (!directoryInfo.Exists)
			{
				return;
			}
			this.AddObject(directoryName);
			foreach (object obj in ((IEnumerable)FileEnumerator.Create(directoryName)))
			{
				FileData fileData = (FileData)obj;
				if (fileData.IsDirectory)
				{
					this.AddDirectory(fileData.FullName);
				}
				else
				{
					this.AddExistingFile(fileData.FullName);
				}
			}
			if (!AppSettings.PortableCompilationOutput)
			{
				this.AddDateTime(directoryInfo.CreationTimeUtc);
				this.AddDateTime(directoryInfo.LastWriteTimeUtc);
			}
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x0004E8A4 File Offset: 0x0004CAA4
		internal void AddResourcesDirectory(string directoryName)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(directoryName);
			if (!directoryInfo.Exists)
			{
				return;
			}
			this.AddObject(directoryName);
			foreach (object obj in ((IEnumerable)FileEnumerator.Create(directoryName)))
			{
				FileData fileData = (FileData)obj;
				if (fileData.IsDirectory)
				{
					this.AddResourcesDirectory(fileData.FullName);
				}
				else
				{
					string fullName = fileData.FullName;
					if (Util.GetCultureName(fullName) == null)
					{
						this.AddExistingFile(fullName);
					}
				}
			}
			if (!AppSettings.PortableCompilationOutput)
			{
				this.AddDateTime(directoryInfo.CreationTimeUtc);
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001959 RID: 6489 RVA: 0x0004E950 File Offset: 0x0004CB50
		internal long CombinedHash
		{
			get
			{
				return this._combinedHash;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x0600195A RID: 6490 RVA: 0x0004E958 File Offset: 0x0004CB58
		internal int CombinedHash32
		{
			get
			{
				return this._combinedHash.GetHashCode();
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x0600195B RID: 6491 RVA: 0x0004E965 File Offset: 0x0004CB65
		internal string CombinedHashString
		{
			get
			{
				return this._combinedHash.ToString("x", CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x040017BB RID: 6075
		private long _combinedHash;
	}
}
