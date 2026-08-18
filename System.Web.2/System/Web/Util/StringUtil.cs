using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.Hosting;

namespace System.Web.Util
{
	// Token: 0x02000222 RID: 546
	internal static class StringUtil
	{
		// Token: 0x06001A29 RID: 6697 RVA: 0x00051DEF File Offset: 0x0004FFEF
		internal static string CheckAndTrimString(string paramValue, string paramName)
		{
			return StringUtil.CheckAndTrimString(paramValue, paramName, true);
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x00051DF9 File Offset: 0x0004FFF9
		internal static string CheckAndTrimString(string paramValue, string paramName, bool throwIfNull)
		{
			return StringUtil.CheckAndTrimString(paramValue, paramName, throwIfNull, -1);
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x00051E04 File Offset: 0x00050004
		internal static string CheckAndTrimString(string paramValue, string paramName, bool throwIfNull, int lengthToCheck)
		{
			if (paramValue == null)
			{
				if (throwIfNull)
				{
					throw new ArgumentNullException(paramName);
				}
				return null;
			}
			else
			{
				string text = paramValue.Trim();
				if (text.Length == 0)
				{
					throw new ArgumentException(SR.GetString("PersonalizationProviderHelper_TrimmedEmptyString", new object[]
					{
						paramName
					}));
				}
				if (lengthToCheck > -1 && text.Length > lengthToCheck)
				{
					throw new ArgumentException(SR.GetString("StringUtil_Trimmed_String_Exceed_Maximum_Length", new object[]
					{
						paramValue,
						paramName,
						lengthToCheck.ToString(CultureInfo.InvariantCulture)
					}));
				}
				return text;
			}
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x00051E84 File Offset: 0x00050084
		internal static bool Equals(string s1, string s2)
		{
			return s1 == s2 || (string.IsNullOrEmpty(s1) && string.IsNullOrEmpty(s2));
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x00051EA4 File Offset: 0x000500A4
		internal unsafe static bool Equals(string s1, int offset1, string s2, int offset2, int length)
		{
			if (offset1 < 0)
			{
				throw new ArgumentOutOfRangeException("offset1");
			}
			if (offset2 < 0)
			{
				throw new ArgumentOutOfRangeException("offset2");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if (((s1 == null) ? 0 : s1.Length) - offset1 < length)
			{
				throw new ArgumentOutOfRangeException(SR.GetString("InvalidOffsetOrCount", new object[]
				{
					"offset1",
					"length"
				}));
			}
			if (((s2 == null) ? 0 : s2.Length) - offset2 < length)
			{
				throw new ArgumentOutOfRangeException(SR.GetString("InvalidOffsetOrCount", new object[]
				{
					"offset2",
					"length"
				}));
			}
			if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
			{
				return true;
			}
			fixed (string text = s1)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (string text2 = s2)
				{
					char* ptr2 = text2;
					if (ptr2 != null)
					{
						ptr2 += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr3 = ptr + offset1;
					char* ptr4 = ptr2 + offset2;
					int num = length;
					while (num-- > 0)
					{
						if (*(ptr3++) != *(ptr4++))
						{
							return false;
						}
					}
					text = null;
				}
				return true;
			}
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x00051FBC File Offset: 0x000501BC
		internal static bool EqualsIgnoreCase(string s1, string s2)
		{
			return (string.IsNullOrEmpty(s1) && string.IsNullOrEmpty(s2)) || (!string.IsNullOrEmpty(s1) && !string.IsNullOrEmpty(s2) && s2.Length == s1.Length && string.Compare(s1, 0, s2, 0, s2.Length, StringComparison.OrdinalIgnoreCase) == 0);
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x00052010 File Offset: 0x00050210
		internal static bool EqualsIgnoreCase(string s1, int index1, string s2, int index2, int length)
		{
			return string.Compare(s1, index1, s2, index2, length, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x00052021 File Offset: 0x00050221
		internal unsafe static string StringFromWCharPtr(IntPtr ip, int length)
		{
			return new string((char*)((void*)ip), 0, length);
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x00052030 File Offset: 0x00050230
		internal static string StringFromCharPtr(IntPtr ip, int length)
		{
			return Marshal.PtrToStringAnsi(ip, length);
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x0005203C File Offset: 0x0005023C
		internal static bool StringEndsWith(string s, char c)
		{
			int length = s.Length;
			return length != 0 && s[length - 1] == c;
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x00052064 File Offset: 0x00050264
		internal unsafe static bool StringEndsWith(string s1, string s2)
		{
			int num = s1.Length - s2.Length;
			if (num < 0)
			{
				return false;
			}
			fixed (string text = s1)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (string text2 = s2)
				{
					char* ptr2 = text2;
					if (ptr2 != null)
					{
						ptr2 += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr3 = ptr + num;
					char* ptr4 = ptr2;
					int length = s2.Length;
					while (length-- > 0)
					{
						if (*(ptr3++) != *(ptr4++))
						{
							return false;
						}
					}
					text = null;
				}
				return true;
			}
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x000520E0 File Offset: 0x000502E0
		internal static bool StringEndsWithIgnoreCase(string s1, string s2)
		{
			int num = s1.Length - s2.Length;
			return num >= 0 && string.Compare(s1, num, s2, 0, s2.Length, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x00052114 File Offset: 0x00050314
		internal static bool StringStartsWith(string s, char c)
		{
			return s.Length != 0 && s[0] == c;
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x0005212C File Offset: 0x0005032C
		internal unsafe static bool StringStartsWith(string s1, string s2)
		{
			if (s2.Length > s1.Length)
			{
				return false;
			}
			fixed (string text = s1)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (string text2 = s2)
				{
					char* ptr2 = text2;
					if (ptr2 != null)
					{
						ptr2 += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr3 = ptr;
					char* ptr4 = ptr2;
					int length = s2.Length;
					while (length-- > 0)
					{
						if (*(ptr3++) != *(ptr4++))
						{
							return false;
						}
					}
					text = null;
				}
				return true;
			}
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x0005219C File Offset: 0x0005039C
		internal static bool StringStartsWithIgnoreCase(string s1, string s2)
		{
			return !string.IsNullOrEmpty(s1) && !string.IsNullOrEmpty(s2) && s2.Length <= s1.Length && string.Compare(s1, 0, s2, 0, s2.Length, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x000521D4 File Offset: 0x000503D4
		internal unsafe static void UnsafeStringCopy(string src, int srcIndex, char[] dest, int destIndex, int len)
		{
			int len2 = len * 2;
			fixed (string text = src)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (char[] array = dest)
				{
					char* ptr2;
					if (dest == null || array.Length == 0)
					{
						ptr2 = null;
					}
					else
					{
						ptr2 = &array[0];
					}
					byte* src2 = (byte*)(ptr + srcIndex);
					byte* dest2 = (byte*)(ptr2 + destIndex);
					StringUtil.memcpyimpl(src2, dest2, len2);
					text = null;
				}
			}
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x00052230 File Offset: 0x00050430
		internal static bool StringArrayEquals(string[] a, string[] b)
		{
			if (a == null != (b == null))
			{
				return false;
			}
			if (a == null)
			{
				return true;
			}
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				if (a[i] != b[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x00052278 File Offset: 0x00050478
		internal unsafe static int GetStringHashCode(string s)
		{
			char* ptr = s;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			int num = 352654597;
			int num2 = num;
			int* ptr2 = (int*)ptr;
			for (int i = s.Length; i > 0; i -= 4)
			{
				num = ((num << 5) + num + (num >> 27) ^ *ptr2);
				if (i <= 2)
				{
					break;
				}
				num2 = ((num2 << 5) + num2 + (num2 >> 27) ^ ptr2[1]);
				ptr2 += 2;
			}
			return num + num2 * 1566083941;
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x000522EC File Offset: 0x000504EC
		internal static int GetNonRandomizedHashCode(string s, bool ignoreCase = false)
		{
			if (AppSettings.UseRandomizedStringHashAlgorithm)
			{
				if (ignoreCase)
				{
					s = s.ToLower(CultureInfo.InvariantCulture);
				}
				return StringUtil.GetStringHashCode(s);
			}
			if (!ignoreCase)
			{
				return s.GetHashCode();
			}
			return StringComparer.InvariantCultureIgnoreCase.GetHashCode(s);
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x00052324 File Offset: 0x00050524
		internal static int GetNonRandomizedStringComparerHashCode(string s)
		{
			if (!AppSettings.UseRandomizedStringHashAlgorithm)
			{
				return StringComparer.InvariantCultureIgnoreCase.GetHashCode(s);
			}
			ApplicationManager appManager = HostingEnvironment.GetApplicationManager();
			if (appManager != null)
			{
				int hashCode = 0;
				ExecutionContextUtil.RunInNullExecutionContext(delegate
				{
					hashCode = appManager.GetNonRandomizedStringComparerHashCode(s, true);
				});
				return hashCode;
			}
			return StringUtil.GetStringHashCode(s.ToLower(CultureInfo.InvariantCulture));
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x0005239D File Offset: 0x0005059D
		internal static int GetNullTerminatedByteArray(Encoding enc, string s, out byte[] bytes)
		{
			bytes = null;
			if (s == null)
			{
				return 0;
			}
			bytes = new byte[enc.GetMaxByteCount(s.Length) + 1];
			return enc.GetBytes(s, 0, s.Length, bytes, 0);
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x000523D0 File Offset: 0x000505D0
		internal unsafe static void memcpyimpl(byte* src, byte* dest, int len)
		{
			if (len >= 16)
			{
				do
				{
					*(long*)dest = *(long*)src;
					*(long*)(dest + 8) = *(long*)(src + 8);
					dest += 16;
					src += 16;
				}
				while ((len -= 16) >= 16);
			}
			if (len > 0)
			{
				if ((len & 8) != 0)
				{
					*(long*)dest = *(long*)src;
					dest += 8;
					src += 8;
				}
				if ((len & 4) != 0)
				{
					*(int*)dest = *(int*)src;
					dest += 4;
					src += 4;
				}
				if ((len & 2) != 0)
				{
					*(short*)dest = *(short*)src;
					dest += 2;
					src += 2;
				}
				if ((len & 1) != 0)
				{
					*(dest++) = *(src++);
				}
			}
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x00052458 File Offset: 0x00050658
		internal static string[] ObjectArrayToStringArray(object[] objectArray)
		{
			string[] array = new string[objectArray.Length];
			objectArray.CopyTo(array, 0);
			return array;
		}
	}
}
