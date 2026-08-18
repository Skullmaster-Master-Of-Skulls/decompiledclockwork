using System;
using System.IO;

namespace Telerik.Web.UI.Captcha
{
	// Token: 0x020000F8 RID: 248
	public class NoiseSynthesizer : IDisposable
	{
		// Token: 0x06000A7F RID: 2687 RVA: 0x00025687 File Offset: 0x00023887
		public NoiseSynthesizer()
		{
			this.InitializeMemebers();
			this.DataLength = 0;
			this.Channels = 2;
			this.SampleRate = 44100;
			this.BitsPerSample = 16;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x000256B6 File Offset: 0x000238B6
		public NoiseSynthesizer(Stream inputStream) : this()
		{
			this.SetFormatData(inputStream);
			this.CopyStream(inputStream);
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x000256CC File Offset: 0x000238CC
		public NoiseSynthesizer(string path) : this()
		{
			this.SetFormatData(path);
			using (Stream stream = File.Open(path, FileMode.Open))
			{
				this.CopyStream(stream);
			}
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00025714 File Offset: 0x00023914
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x00025723 File Offset: 0x00023923
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.OutputStream.Close();
				this.reader.Close();
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x0002573E File Offset: 0x0002393E
		// (set) Token: 0x06000A85 RID: 2693 RVA: 0x00025746 File Offset: 0x00023946
		public int DataLength { get; private set; }

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x0002574F File Offset: 0x0002394F
		// (set) Token: 0x06000A87 RID: 2695 RVA: 0x00025757 File Offset: 0x00023957
		public int Channels { get; private set; }

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x00025760 File Offset: 0x00023960
		// (set) Token: 0x06000A89 RID: 2697 RVA: 0x00025768 File Offset: 0x00023968
		public int SampleRate { get; private set; }

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x00025771 File Offset: 0x00023971
		// (set) Token: 0x06000A8B RID: 2699 RVA: 0x00025779 File Offset: 0x00023979
		public int BitsPerSample { get; private set; }

		// Token: 0x06000A8C RID: 2700 RVA: 0x00025784 File Offset: 0x00023984
		public MemoryStream GetMixedOutput()
		{
			byte[] noiseData = this.NoiseGenerator.GetNoiseData(this.DataLength * 8 / this.BitsPerSample, this.BitsPerSample, 3);
			this.OutputStream.Seek(44L, SeekOrigin.Begin);
			byte[] array = new byte[this.OutputStream.Length - 44L];
			this.OutputStream.Read(array, 0, array.Length);
			byte[] array2 = new byte[noiseData.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				int num = (int)(noiseData[i] + array[i]);
				array2[i] = ((num <= 255) ? (noiseData[i] + array[i]) : byte.MaxValue);
			}
			this.OutputStream.Seek(44L, SeekOrigin.Begin);
			this.OutputStream.Write(array2, 0, array2.Length);
			return this.OutputStream;
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0002584C File Offset: 0x00023A4C
		public void SetFormatData(Stream inputStream)
		{
			this.reader = new BinaryReader(inputStream);
			this.DataLength = (int)inputStream.Length - 44;
			inputStream.Seek(22L, SeekOrigin.Begin);
			this.Channels = (int)this.reader.ReadInt16();
			inputStream.Seek(24L, SeekOrigin.Begin);
			this.SampleRate = this.reader.ReadInt32();
			inputStream.Seek(34L, SeekOrigin.Begin);
			this.BitsPerSample = (int)this.reader.ReadInt16();
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x000258CC File Offset: 0x00023ACC
		public void SetFormatData(string path)
		{
			using (Stream stream = File.Open(path, FileMode.Open))
			{
				this.SetFormatData(stream);
			}
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00025904 File Offset: 0x00023B04
		private void CopyStream(Stream inputStream)
		{
			byte[] array = new byte[inputStream.Length];
			inputStream.Seek(0L, SeekOrigin.Begin);
			int count;
			while ((count = inputStream.Read(array, 0, array.Length)) > 0)
			{
				this.OutputStream.Write(array, 0, count);
			}
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0002594A File Offset: 0x00023B4A
		private void InitializeMemebers()
		{
			this.NoiseGenerator = new WhiteNoiseGenerator();
			this.OutputStream = new MemoryStream();
		}

		// Token: 0x04000287 RID: 647
		private const int HEADER_SIZE = 44;

		// Token: 0x04000288 RID: 648
		private const int CHANNELS_POSITION = 22;

		// Token: 0x04000289 RID: 649
		private const int SAMPLERATE_POSITION = 24;

		// Token: 0x0400028A RID: 650
		private const int BITSPERSAMPLE_POSITION = 34;

		// Token: 0x0400028B RID: 651
		private INoiseGenerator NoiseGenerator;

		// Token: 0x0400028C RID: 652
		private MemoryStream OutputStream;

		// Token: 0x0400028D RID: 653
		private BinaryReader reader;
	}
}
