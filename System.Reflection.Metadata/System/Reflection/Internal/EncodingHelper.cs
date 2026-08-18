using System;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection.Internal
{
	// Token: 0x0200015E RID: 350
	internal static class EncodingHelper
	{
		// Token: 0x06000ADD RID: 2781 RVA: 0x0001EEE6 File Offset: 0x0001D0E6
		public unsafe static string DecodeUtf8(byte* bytes, int byteCount, byte[] prefix, MetadataStringDecoder utf8Decoder)
		{
			if (prefix != null)
			{
				return EncodingHelper.DecodeUtf8Prefixed(bytes, byteCount, prefix, utf8Decoder);
			}
			if (byteCount == 0)
			{
				return string.Empty;
			}
			return utf8Decoder.GetString(bytes, byteCount);
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0001EF08 File Offset: 0x0001D108
		private unsafe static string DecodeUtf8Prefixed(byte* bytes, int byteCount, byte[] prefix, MetadataStringDecoder utf8Decoder)
		{
			int num = byteCount + prefix.Length;
			if (num == 0)
			{
				return string.Empty;
			}
			byte[] array = EncodingHelper.AcquireBuffer(num);
			prefix.CopyTo(array, 0);
			Marshal.Copy((IntPtr)((void*)bytes), array, prefix.Length, byteCount);
			string @string;
			fixed (byte* ptr = array)
			{
				@string = utf8Decoder.GetString(ptr, num);
			}
			EncodingHelper.ReleaseBuffer(array);
			return @string;
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0001EF6C File Offset: 0x0001D16C
		private static byte[] AcquireBuffer(int byteCount)
		{
			if (byteCount > 200)
			{
				return new byte[byteCount];
			}
			return EncodingHelper.s_pool.Allocate();
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0001EF87 File Offset: 0x0001D187
		private static void ReleaseBuffer(byte[] buffer)
		{
			if (buffer.Length == 200)
			{
				EncodingHelper.s_pool.Free(buffer);
			}
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0001EF9E File Offset: 0x0001D19E
		public unsafe static string GetString(this Encoding encoding, byte* bytes, int byteCount)
		{
			if (EncodingHelper.s_getStringPlatform == null)
			{
				return EncodingHelper.GetStringPortable(encoding, bytes, byteCount);
			}
			return EncodingHelper.s_getStringPlatform(encoding, bytes, byteCount);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0001EFC0 File Offset: 0x0001D1C0
		private unsafe static string GetStringPortable(Encoding encoding, byte* bytes, int byteCount)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount");
			}
			byte[] array = EncodingHelper.AcquireBuffer(byteCount);
			Marshal.Copy((IntPtr)((void*)bytes), array, 0, byteCount);
			string @string = encoding.GetString(array, 0, byteCount);
			EncodingHelper.ReleaseBuffer(array);
			return @string;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0001F010 File Offset: 0x0001D210
		private unsafe static EncodingHelper.Encoding_GetString LoadGetStringPlatform()
		{
			MethodInfo method = LightUpHelper.GetMethod(typeof(Encoding), "GetString", new Type[]
			{
				typeof(byte*),
				typeof(int)
			});
			if (method != null && method.ReturnType == typeof(string))
			{
				try
				{
					return (EncodingHelper.Encoding_GetString)method.CreateDelegate(typeof(EncodingHelper.Encoding_GetString), null);
				}
				catch (MemberAccessException)
				{
				}
				catch (InvalidOperationException)
				{
				}
			}
			foreach (MethodInfo methodInfo in typeof(string).GetTypeInfo().GetDeclaredMethods("CreateStringFromEncoding"))
			{
				ParameterInfo[] parameters = methodInfo.GetParameters();
				if (parameters.Length == 3 && parameters[0].ParameterType == typeof(byte*) && parameters[1].ParameterType == typeof(int) && parameters[2].ParameterType == typeof(Encoding) && methodInfo.ReturnType == typeof(string))
				{
					try
					{
						EncodingHelper.String_CreateStringFromEncoding createStringFromEncoding = (EncodingHelper.String_CreateStringFromEncoding)methodInfo.CreateDelegate(typeof(EncodingHelper.String_CreateStringFromEncoding), null);
						return (Encoding encoding, byte* bytes, int byteCount) => EncodingHelper.GetStringUsingCreateStringFromEncoding(createStringFromEncoding, bytes, byteCount, encoding);
					}
					catch (MemberAccessException)
					{
					}
					catch (InvalidOperationException)
					{
					}
				}
			}
			return null;
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0001F1A8 File Offset: 0x0001D3A8
		private unsafe static string GetStringUsingCreateStringFromEncoding(EncodingHelper.String_CreateStringFromEncoding createStringFromEncoding, byte* bytes, int byteCount, Encoding encoding)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount");
			}
			return createStringFromEncoding(bytes, byteCount, encoding);
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x0001F1D2 File Offset: 0x0001D3D2
		// (set) Token: 0x06000AE6 RID: 2790 RVA: 0x0001F1DC File Offset: 0x0001D3DC
		internal static bool TestOnly_LightUpEnabled
		{
			get
			{
				return EncodingHelper.s_getStringPlatform != null;
			}
			set
			{
				EncodingHelper.s_getStringPlatform = (value ? EncodingHelper.LoadGetStringPlatform() : null);
			}
		}

		// Token: 0x0400090A RID: 2314
		public const int PooledBufferSize = 200;

		// Token: 0x0400090B RID: 2315
		private static readonly ObjectPool<byte[]> s_pool = new ObjectPool<byte[]>(() => new byte[200]);

		// Token: 0x0400090C RID: 2316
		private static EncodingHelper.Encoding_GetString s_getStringPlatform = EncodingHelper.LoadGetStringPlatform();

		// Token: 0x020001DA RID: 474
		// (Invoke) Token: 0x06000C5D RID: 3165
		internal unsafe delegate string Encoding_GetString(Encoding encoding, byte* bytes, int byteCount);

		// Token: 0x020001DB RID: 475
		// (Invoke) Token: 0x06000C61 RID: 3169
		private unsafe delegate string String_CreateStringFromEncoding(byte* bytes, int byteCount, Encoding encoding);
	}
}
