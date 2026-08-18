using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using a.j;

namespace MailBee.Security
{
	// Token: 0x02000101 RID: 257
	public class CryptoServiceProvider : IDisposable
	{
		// Token: 0x060008A4 RID: 2212 RVA: 0x00028464 File Offset: 0x00027464
		[SecuritySafeCritical]
		public CryptoServiceProvider(string name)
		{
			if (name == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			IntPtr intPtr = Marshal.StringToHGlobalUni(name);
			IntPtr[] array = new IntPtr[]
			{
				this.b
			};
			GCHandle gchandle = default(GCHandle);
			try
			{
				gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
				IntPtr a_ = gchandle.AddrOfPinnedObject();
				if (ab.b.CryptAcquireContext(a_, IntPtr.Zero, intPtr, this.a(name), 0U) == 0)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error == -2146893802 || lastWin32Error == 2)
					{
						int num;
						if (lastWin32Error == -2146893802)
						{
							num = ab.b.CryptAcquireContext(a_, IntPtr.Zero, intPtr, this.a(name), 8U);
						}
						else
						{
							num = ab.b.CryptAcquireContext(a_, IntPtr.Zero, intPtr, this.a(name), 32U);
							if (num == 0)
							{
								lastWin32Error = Marshal.GetLastWin32Error();
								if (lastWin32Error == -2146893802)
								{
									num = ab.b.CryptAcquireContext(a_, IntPtr.Zero, intPtr, this.a(name), 40U);
								}
							}
						}
						if (num == 0)
						{
							int lastWin32Error2 = Marshal.GetLastWin32Error();
							this.d = 1100;
							if (this.c)
							{
								throw new MailBeeCryptoProviderWin32Exception(lastWin32Error2);
							}
						}
					}
					else
					{
						int lastWin32Error3 = Marshal.GetLastWin32Error();
						this.d = 1100;
						if (this.c)
						{
							throw new MailBeeCryptoProviderWin32Exception(lastWin32Error3);
						}
					}
				}
				this.b = array[0];
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
				gchandle.Free();
			}
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x000285CC File Offset: 0x000275CC
		public CryptoServiceProvider() : this(CryptoServiceProvider.a())
		{
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x000285D9 File Offset: 0x000275D9
		internal IntPtr Handle
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x000285E1 File Offset: 0x000275E1
		// (set) Token: 0x060008A8 RID: 2216 RVA: 0x000285E9 File Offset: 0x000275E9
		public bool ThrowExceptions
		{
			get
			{
				return this.c;
			}
			set
			{
				this.c = value;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x000285F2 File Offset: 0x000275F2
		public int LastResult
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x000285FA File Offset: 0x000275FA
		public string GetProviderName()
		{
			return this.a(4U);
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00028604 File Offset: 0x00027604
		public string GetProviderVersion()
		{
			byte[] array = this.b(5U);
			if (array == null)
			{
				return null;
			}
			if (array.Length >= 2)
			{
				return string.Format("{0}.{1}", array[1], array[0]);
			}
			if (array.Length == 1)
			{
				return string.Format("{0}", array[0]);
			}
			return "0";
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0002865D File Offset: 0x0002765D
		public string GetKeyContainer()
		{
			return this.a(6U);
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00028668 File Offset: 0x00027668
		[SecuritySafeCritical]
		public Algorithm[] GetSupportedAlgorithms()
		{
			this.d = 0;
			ArrayList arrayList = new ArrayList();
			uint cb = 0U;
			IntPtr intPtr = IntPtr.Zero;
			if (ab.b.CryptGetProvParam(this.b, 1U, IntPtr.Zero, ref cb, 1U) != 0)
			{
				intPtr = Marshal.AllocHGlobal((int)cb);
				try
				{
					bool flag = true;
					uint a_ = 1U;
					while (flag)
					{
						if (ab.b.CryptGetProvParam(this.b, 1U, intPtr, ref cb, a_) == 1)
						{
							a_ = 0U;
							global::a.j.a a = default(global::a.j.a);
							a = (global::a.j.a)Marshal.PtrToStructure(intPtr, typeof(global::a.j.a));
							byte[] array = new byte[20];
							Array.Copy(BitConverter.GetBytes(a.d), 0, array, 0, 4);
							Array.Copy(BitConverter.GetBytes(a.e), 0, array, 4, 4);
							Array.Copy(BitConverter.GetBytes(a.f), 0, array, 8, 4);
							Array.Copy(BitConverter.GetBytes(a.g), 0, array, 12, 4);
							Array.Copy(BitConverter.GetBytes(a.h), 0, array, 16, 4);
							string @string = Encoding.ASCII.GetString(array, 0, (int)(a.c - 1U));
							Algorithm value = new Algorithm(a.a, (int)a.b, Algorithm.c(a.a), @string, Algorithm.b(a.a));
							arrayList.Add(value);
						}
						else
						{
							flag = false;
						}
					}
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
				Algorithm[] array2 = new Algorithm[arrayList.Count];
				for (int i = 0; i < arrayList.Count; i++)
				{
					array2[i] = (Algorithm)arrayList[i];
				}
				return array2;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			this.d = 1100;
			if (this.c)
			{
				throw new MailBeeCryptoProviderWin32Exception(lastWin32Error);
			}
			return null;
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0002883C File Offset: 0x0002783C
		[SecuritySafeCritical]
		public static string[] GetSystemProviders()
		{
			StringCollection stringCollection = new StringCollection();
			uint cb = 0U;
			uint a_ = 0U;
			uint num;
			while (ab.b.CryptEnumProviders(a_, IntPtr.Zero, 0U, out num, IntPtr.Zero, ref cb) == 1)
			{
				IntPtr intPtr = Marshal.AllocHGlobal((int)cb);
				if (ab.b.CryptEnumProviders(a_++, IntPtr.Zero, 0U, out num, intPtr, ref cb) == 1)
				{
					string value = Marshal.PtrToStringAuto(intPtr);
					stringCollection.Add(value);
				}
				Marshal.FreeHGlobal(intPtr);
			}
			string[] array = new string[stringCollection.Count];
			stringCollection.CopyTo(array, 0);
			return array;
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x000288C3 File Offset: 0x000278C3
		public void Dispose()
		{
			this.a(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x000288D4 File Offset: 0x000278D4
		[SecuritySafeCritical]
		private void a(bool A_0)
		{
			this.d = 0;
			if (!this.a && this.b != IntPtr.Zero && ab.b.CryptReleaseContext(this.b, 0U) == 0)
			{
				this.d = Marshal.GetLastWin32Error();
				if (this.c)
				{
					throw new MailBeeCryptoProviderWin32Exception(this.d);
				}
			}
			this.a = true;
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00028938 File Offset: 0x00027938
		[SecuritySafeCritical]
		private uint a(string A_0)
		{
			uint cb = 0U;
			string strA = null;
			uint a_ = 0U;
			uint result;
			while (ab.b.CryptEnumProviders(a_, IntPtr.Zero, 0U, out result, IntPtr.Zero, ref cb) == 1)
			{
				IntPtr intPtr = Marshal.AllocHGlobal((int)cb);
				if (ab.b.CryptEnumProviders(a_++, IntPtr.Zero, 0U, out result, intPtr, ref cb) == 1)
				{
					strA = Marshal.PtrToStringAuto(intPtr);
				}
				Marshal.FreeHGlobal(intPtr);
				if (string.Compare(strA, A_0, true) == 0)
				{
					return result;
				}
			}
			return 1U;
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x000289A4 File Offset: 0x000279A4
		[SecuritySafeCritical]
		private byte[] b(uint A_0)
		{
			this.d = 0;
			uint num = 0U;
			IntPtr intPtr = IntPtr.Zero;
			byte[] array = null;
			if (ab.b.CryptGetProvParam(this.b, A_0, intPtr, ref num, 0U) != 0)
			{
				intPtr = Marshal.AllocHGlobal((int)num);
				try
				{
					if (ab.b.CryptGetProvParam(this.b, A_0, intPtr, ref num, 0U) == 0)
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						this.d = 1100;
						if (this.c)
						{
							throw new MailBeeCryptoProviderWin32Exception(lastWin32Error);
						}
						return null;
					}
					else
					{
						array = new byte[num];
						Marshal.Copy(intPtr, array, 0, array.Length);
					}
				}
				catch (ArgumentOutOfRangeException a_)
				{
					this.d = 6;
					if (this.c)
					{
						throw new MailBeeInternalException(this.d, a_);
					}
					return null;
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
				return array;
			}
			int lastWin32Error2 = Marshal.GetLastWin32Error();
			this.d = 1100;
			if (this.c)
			{
				throw new MailBeeCryptoProviderWin32Exception(lastWin32Error2);
			}
			return null;
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00028A98 File Offset: 0x00027A98
		private string a(uint A_0)
		{
			byte[] array = this.b(A_0);
			if (array != null)
			{
				return Global.DefaultEncoding.GetString(array, 0, array.Length);
			}
			return null;
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00028AC4 File Offset: 0x00027AC4
		[SecuritySafeCritical]
		private static string a()
		{
			string result = "Microsoft Enhanced Cryptographic Provider v1.0";
			uint cb = 0U;
			IntPtr intPtr = IntPtr.Zero;
			if (ab.b.CryptGetDefaultProvider(1U, IntPtr.Zero, 2U, intPtr, ref cb) == 0)
			{
				return result;
			}
			intPtr = Marshal.AllocHGlobal((int)cb);
			if (ab.b.CryptGetDefaultProvider(1U, IntPtr.Zero, 2U, intPtr, ref cb) == 0)
			{
				Marshal.FreeHGlobal(intPtr);
				return result;
			}
			result = Marshal.PtrToStringUni(intPtr);
			Marshal.FreeHGlobal(intPtr);
			return result;
		}

		// Token: 0x040006D7 RID: 1751
		private bool a;

		// Token: 0x040006D8 RID: 1752
		private IntPtr b = IntPtr.Zero;

		// Token: 0x040006D9 RID: 1753
		private bool c = true;

		// Token: 0x040006DA RID: 1754
		private int d;

		// Token: 0x040006DB RID: 1755
		public const string Base = "Microsoft Base Cryptographic Provider v1.0";

		// Token: 0x040006DC RID: 1756
		public const string Enhanced = "Microsoft Enhanced Cryptographic Provider v1.0";

		// Token: 0x040006DD RID: 1757
		public const string Strong = "Microsoft Strong Cryptographic Provider";
	}
}
