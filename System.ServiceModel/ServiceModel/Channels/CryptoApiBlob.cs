using System;
using System.Runtime.InteropServices;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A4A RID: 2634
	internal sealed class CryptoApiBlob : IDisposable
	{
		// Token: 0x06006823 RID: 26659 RVA: 0x001848A2 File Offset: 0x00182AA2
		public CryptoApiBlob()
		{
		}

		// Token: 0x06006824 RID: 26660 RVA: 0x001848AA File Offset: 0x00182AAA
		public CryptoApiBlob(byte[] bytes)
		{
			this.AllocateBlob(bytes.Length);
			Marshal.Copy(bytes, 0, this.data, bytes.Length);
			this.cbData = bytes.Length;
		}

		// Token: 0x170018EC RID: 6380
		// (get) Token: 0x06006825 RID: 26661 RVA: 0x001848D9 File Offset: 0x00182AD9
		public int DataSize
		{
			get
			{
				return this.cbData;
			}
		}

		// Token: 0x06006826 RID: 26662 RVA: 0x001848E1 File Offset: 0x00182AE1
		public void AllocateBlob(int size)
		{
			this.data = CriticalAllocHandle.FromSize(size);
			this.cbData = size;
		}

		// Token: 0x06006827 RID: 26663 RVA: 0x001848F6 File Offset: 0x00182AF6
		public CryptoApiBlob.InteropHelper GetMemoryForPinning()
		{
			return new CryptoApiBlob.InteropHelper(this.cbData, this.data);
		}

		// Token: 0x06006828 RID: 26664 RVA: 0x00184910 File Offset: 0x00182B10
		public byte[] GetBytes()
		{
			if (this.cbData == 0)
			{
				return null;
			}
			byte[] array = DiagnosticUtility.Utility.AllocateByteArray(this.cbData);
			Marshal.Copy(this.data, array, 0, this.cbData);
			return array;
		}

		// Token: 0x06006829 RID: 26665 RVA: 0x00184951 File Offset: 0x00182B51
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x0600682A RID: 26666 RVA: 0x0018495C File Offset: 0x00182B5C
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x04003BB7 RID: 15287
		private int cbData;

		// Token: 0x04003BB8 RID: 15288
		private CriticalAllocHandle data;

		// Token: 0x02000E7B RID: 3707
		[StructLayout(LayoutKind.Sequential)]
		public class InteropHelper
		{
			// Token: 0x060083F9 RID: 33785 RVA: 0x001E80CE File Offset: 0x001E62CE
			public InteropHelper(int size, IntPtr data)
			{
				this.size = size;
				this.data = data;
			}

			// Token: 0x04004B25 RID: 19237
			public int size;

			// Token: 0x04004B26 RID: 19238
			public IntPtr data;
		}
	}
}
