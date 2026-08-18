using System;
using System.IO;

namespace Telerik.Web.UI
{
	// Token: 0x020016D2 RID: 5842
	public class CaptchaCombineAudio
	{
		// Token: 0x0600E1AC RID: 57772 RVA: 0x00322D7C File Offset: 0x00320F7C
		private void ReadWaveHeader(string filePath)
		{
			FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
			BinaryReader binaryReader = new BinaryReader(fileStream);
			this._length = (int)fileStream.Length - 8;
			fileStream.Position = 22L;
			this._channels = binaryReader.ReadInt16();
			fileStream.Position = 24L;
			this._samplerate = binaryReader.ReadInt32();
			fileStream.Position = 34L;
			this._bitsPerSample = binaryReader.ReadInt16();
			this._dataLength = (int)fileStream.Length - 44;
			binaryReader.Close();
			fileStream.Close();
		}

		// Token: 0x0600E1AD RID: 57773 RVA: 0x00322E28 File Offset: 0x00321028
		private MemoryStream WriteHeaderToOutputStream()
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			memoryStream.Position = 0L;
			binaryWriter.Write(new char[]
			{
				'R',
				'I',
				'F',
				'F'
			});
			binaryWriter.Write(this._length);
			binaryWriter.Write(new char[]
			{
				'W',
				'A',
				'V',
				'E',
				'f',
				'm',
				't',
				' '
			});
			binaryWriter.Write(16);
			binaryWriter.Write(1);
			binaryWriter.Write(this._channels);
			binaryWriter.Write(this._samplerate);
			binaryWriter.Write(this._samplerate * (int)(this._bitsPerSample * this._channels / 8));
			binaryWriter.Write(this._bitsPerSample * this._channels / 8);
			binaryWriter.Write(this._bitsPerSample);
			binaryWriter.Write(new char[]
			{
				'd',
				'a',
				't',
				'a'
			});
			binaryWriter.Write(this._dataLength);
			return memoryStream;
		}

		// Token: 0x0600E1AE RID: 57774 RVA: 0x00322F10 File Offset: 0x00321110
		public static MemoryStream Concatenate(string[] filePaths)
		{
			CaptchaCombineAudio captchaCombineAudio = new CaptchaCombineAudio();
			CaptchaCombineAudio captchaCombineAudio2 = new CaptchaCombineAudio();
			captchaCombineAudio2._dataLength = 0;
			captchaCombineAudio2._length = 0;
			foreach (string filePath in filePaths)
			{
				captchaCombineAudio.ReadWaveHeader(filePath);
				captchaCombineAudio2._dataLength += captchaCombineAudio._dataLength;
				captchaCombineAudio2._length += captchaCombineAudio._length;
			}
			captchaCombineAudio2._bitsPerSample = captchaCombineAudio._bitsPerSample;
			captchaCombineAudio2._channels = captchaCombineAudio._channels;
			captchaCombineAudio2._samplerate = captchaCombineAudio._samplerate;
			MemoryStream memoryStream = new MemoryStream();
			memoryStream = captchaCombineAudio2.WriteHeaderToOutputStream();
			foreach (string path in filePaths)
			{
				FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
				byte[] array = new byte[fileStream.Length - 44L];
				fileStream.Position = 44L;
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(array);
			}
			return memoryStream;
		}

		// Token: 0x04004168 RID: 16744
		private int _length;

		// Token: 0x04004169 RID: 16745
		private short _channels;

		// Token: 0x0400416A RID: 16746
		private int _samplerate;

		// Token: 0x0400416B RID: 16747
		private int _dataLength;

		// Token: 0x0400416C RID: 16748
		private short _bitsPerSample;
	}
}
