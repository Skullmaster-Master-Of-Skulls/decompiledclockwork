using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x02000297 RID: 663
	[Serializable]
	public sealed class ImageListStreamer : ISerializable, IDisposable
	{
		// Token: 0x060029FA RID: 10746 RVA: 0x000BF139 File Offset: 0x000BD339
		internal ImageListStreamer(ImageList il)
		{
			this.imageList = il;
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x000BF148 File Offset: 0x000BD348
		private ImageListStreamer(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			if (enumerator == null)
			{
				return;
			}
			while (enumerator.MoveNext())
			{
				if (string.Equals(enumerator.Name, "Data", StringComparison.OrdinalIgnoreCase))
				{
					byte[] array = (byte[])enumerator.Value;
					if (array != null)
					{
						IntPtr userCookie = UnsafeNativeMethods.ThemingScope.Activate();
						try
						{
							MemoryStream dataStream = new MemoryStream(this.Decompress(array));
							object obj = ImageListStreamer.internalSyncObject;
							lock (obj)
							{
								SafeNativeMethods.InitCommonControls();
								this.nativeImageList = new ImageList.NativeImageList(SafeNativeMethods.ImageList_Read(new UnsafeNativeMethods.ComStreamFromDataStream(dataStream)));
							}
						}
						finally
						{
							UnsafeNativeMethods.ThemingScope.Deactivate(userCookie);
						}
						if (this.nativeImageList.Handle == IntPtr.Zero)
						{
							throw new InvalidOperationException(SR.GetString("ImageListStreamerLoadFailed"));
						}
					}
				}
			}
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x000BF234 File Offset: 0x000BD434
		private byte[] Compress(byte[] input)
		{
			int num = 0;
			int i = 0;
			int num2 = 0;
			while (i < input.Length)
			{
				byte b = input[i++];
				byte b2 = 1;
				while (i < input.Length && input[i] == b && b2 < 255)
				{
					b2 += 1;
					i++;
				}
				num += 2;
			}
			byte[] array = new byte[num + ImageListStreamer.HEADER_MAGIC.Length];
			Buffer.BlockCopy(ImageListStreamer.HEADER_MAGIC, 0, array, 0, ImageListStreamer.HEADER_MAGIC.Length);
			int num3 = ImageListStreamer.HEADER_MAGIC.Length;
			i = 0;
			while (i < input.Length)
			{
				byte b3 = input[i++];
				byte b4 = 1;
				while (i < input.Length && input[i] == b3 && b4 < 255)
				{
					b4 += 1;
					i++;
				}
				array[num3 + num2++] = b4;
				array[num3 + num2++] = b3;
			}
			return array;
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x000BF300 File Offset: 0x000BD500
		private byte[] Decompress(byte[] input)
		{
			int num = 0;
			int num2 = 0;
			if (input.Length < ImageListStreamer.HEADER_MAGIC.Length)
			{
				return input;
			}
			int i;
			for (i = 0; i < ImageListStreamer.HEADER_MAGIC.Length; i++)
			{
				if (input[i] != ImageListStreamer.HEADER_MAGIC[i])
				{
					return input;
				}
			}
			for (i = ImageListStreamer.HEADER_MAGIC.Length; i < input.Length; i += 2)
			{
				num += (int)input[i];
			}
			byte[] array = new byte[num];
			i = ImageListStreamer.HEADER_MAGIC.Length;
			while (i < input.Length)
			{
				byte b = input[i++];
				byte b2 = input[i++];
				int j = num2;
				int num3 = num2 + (int)b;
				while (j < num3)
				{
					array[j++] = b2;
				}
				num2 += (int)b;
			}
			return array;
		}

		// Token: 0x060029FE RID: 10750 RVA: 0x000BF3A8 File Offset: 0x000BD5A8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public void GetObjectData(SerializationInfo si, StreamingContext context)
		{
			MemoryStream memoryStream = new MemoryStream();
			IntPtr intPtr = IntPtr.Zero;
			if (this.imageList != null)
			{
				intPtr = this.imageList.Handle;
			}
			else if (this.nativeImageList != null)
			{
				intPtr = this.nativeImageList.Handle;
			}
			if (intPtr == IntPtr.Zero || !this.WriteImageList(intPtr, memoryStream))
			{
				throw new InvalidOperationException(SR.GetString("ImageListStreamerSaveFailed"));
			}
			si.AddValue("Data", this.Compress(memoryStream.ToArray()));
		}

		// Token: 0x060029FF RID: 10751 RVA: 0x000BF429 File Offset: 0x000BD629
		internal ImageList.NativeImageList GetNativeImageList()
		{
			return this.nativeImageList;
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x000BF434 File Offset: 0x000BD634
		private bool WriteImageList(IntPtr imagelistHandle, Stream stream)
		{
			try
			{
				int num = SafeNativeMethods.ImageList_WriteEx(new HandleRef(this, imagelistHandle), 1, new UnsafeNativeMethods.ComStreamFromDataStream(stream));
				return num == 0;
			}
			catch (EntryPointNotFoundException)
			{
			}
			return SafeNativeMethods.ImageList_Write(new HandleRef(this, imagelistHandle), new UnsafeNativeMethods.ComStreamFromDataStream(stream));
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x000BF484 File Offset: 0x000BD684
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x000BF493 File Offset: 0x000BD693
		private void Dispose(bool disposing)
		{
			if (disposing && this.nativeImageList != null)
			{
				this.nativeImageList.Dispose();
				this.nativeImageList = null;
			}
		}

		// Token: 0x0400110A RID: 4362
		private static readonly byte[] HEADER_MAGIC = new byte[]
		{
			77,
			83,
			70,
			116
		};

		// Token: 0x0400110B RID: 4363
		private static object internalSyncObject = new object();

		// Token: 0x0400110C RID: 4364
		private ImageList imageList;

		// Token: 0x0400110D RID: 4365
		private ImageList.NativeImageList nativeImageList;
	}
}
