using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Telerik.Web.UI
{
	// Token: 0x0200182A RID: 6186
	[Serializable]
	public class CompressedPageState
	{
		// Token: 0x0600F084 RID: 61572 RVA: 0x0036AC31 File Offset: 0x00368E31
		private CompressedPageState(byte[] compressedData)
		{
			this._compressedData = compressedData;
		}

		// Token: 0x170048B7 RID: 18615
		// (get) Token: 0x0600F085 RID: 61573 RVA: 0x0036AC40 File Offset: 0x00368E40
		// (set) Token: 0x0600F086 RID: 61574 RVA: 0x0036AC48 File Offset: 0x00368E48
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		public byte[] CompressedData
		{
			get
			{
				return this._compressedData;
			}
			set
			{
				this._compressedData = value;
			}
		}

		// Token: 0x0600F087 RID: 61575 RVA: 0x0036AC54 File Offset: 0x00368E54
		public static CompressedPageState Compress(string state)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(state);
			CompressedPageState result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress))
				{
					deflateStream.Write(bytes, 0, bytes.Length);
				}
				result = new CompressedPageState(memoryStream.ToArray());
			}
			return result;
		}

		// Token: 0x0600F088 RID: 61576 RVA: 0x0036ACC8 File Offset: 0x00368EC8
		public string Decompress()
		{
			byte[] array = new byte[8192];
			string @string;
			using (MemoryStream memoryStream = new MemoryStream(this.CompressedData))
			{
				using (MemoryStream memoryStream2 = new MemoryStream())
				{
					using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Decompress))
					{
						while (deflateStream.CanRead)
						{
							int num = deflateStream.Read(array, 0, array.Length);
							if (num == 0)
							{
								break;
							}
							memoryStream2.Write(array, 0, num);
						}
					}
					@string = Encoding.UTF8.GetString(memoryStream2.ToArray());
				}
			}
			return @string;
		}

		// Token: 0x0400454B RID: 17739
		private byte[] _compressedData;
	}
}
