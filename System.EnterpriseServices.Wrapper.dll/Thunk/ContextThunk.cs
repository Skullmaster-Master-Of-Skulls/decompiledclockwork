using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.Thunk
{
	// Token: 0x02000036 RID: 54
	internal class ContextThunk
	{
		// Token: 0x06000088 RID: 136 RVA: 0x00001F14 File Offset: 0x00001314
		private ContextThunk()
		{
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00001F28 File Offset: 0x00001328
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe static bool IsInTransaction()
		{
			IObjectContext* ptr = null;
			if (<Module>.GetContext(ref <Module>.IID_IObjectContext, (void**)(&ptr)) >= 0 && null != ptr)
			{
				IObjectContext* ptr2 = ptr;
				bool result = calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 64L)) != 0;
				IObjectContext* ptr3 = ptr;
				object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr3, *(*(long*)ptr3 + 16L));
				return result;
			}
			return false;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00001F74 File Offset: 0x00001374
		[return: MarshalAs(UnmanagedType.U1)]
		public static bool IsDefaultContext()
		{
			return <Module>.IsDefaultContext() != 0;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00001F90 File Offset: 0x00001390
		public unsafe static void SetAbort()
		{
			IObjectContext* ptr = null;
			int num = <Module>.GetContext(ref <Module>.IID_IObjectContext, (void**)(&ptr));
			if (num >= 0 && null != ptr)
			{
				IObjectContext* ptr2 = ptr;
				num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 40L));
				IObjectContext* ptr3 = ptr;
				object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr3, *(*(long*)ptr3 + 16L));
				if (num == 0)
				{
					return;
				}
			}
			num = ((num == -2147467262) ? -2147164156 : num);
			Marshal.ThrowExceptionForHR(num);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00001FEC File Offset: 0x000013EC
		public unsafe static void SetComplete()
		{
			IObjectContext* ptr = null;
			int num = <Module>.GetContext(ref <Module>.IID_IObjectContext, (void**)(&ptr));
			if (num >= 0 && null != ptr)
			{
				IObjectContext* ptr2 = ptr;
				num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 32L));
				IObjectContext* ptr3 = ptr;
				object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr3, *(*(long*)ptr3 + 16L));
				if (num == 0)
				{
					return;
				}
			}
			num = ((num == -2147467262) ? -2147164156 : num);
			Marshal.ThrowExceptionForHR(num);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00002048 File Offset: 0x00001448
		public unsafe static void DisableCommit()
		{
			IObjectContext* ptr = null;
			int num = <Module>.GetContext(ref <Module>.IID_IObjectContext, (void**)(&ptr));
			if (num >= 0 && null != ptr)
			{
				IObjectContext* ptr2 = ptr;
				num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 56L));
				IObjectContext* ptr3 = ptr;
				object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr3, *(*(long*)ptr3 + 16L));
				if (num == 0)
				{
					return;
				}
			}
			num = ((num == -2147467262) ? -2147164156 : num);
			Marshal.ThrowExceptionForHR(num);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000020A4 File Offset: 0x000014A4
		public unsafe static void EnableCommit()
		{
			IObjectContext* ptr = null;
			int num = <Module>.GetContext(ref <Module>.IID_IObjectContext, (void**)(&ptr));
			if (num >= 0 && null != ptr)
			{
				IObjectContext* ptr2 = ptr;
				num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 48L));
				IObjectContext* ptr3 = ptr;
				object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr3, *(*(long*)ptr3 + 16L));
				if (num == 0)
				{
					return;
				}
			}
			num = ((num == -2147467262) ? -2147164156 : num);
			Marshal.ThrowExceptionForHR(num);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002100 File Offset: 0x00001500
		public unsafe static Guid GetTransactionId()
		{
			Guid result = default(Guid);
			IObjectContextInfo* ptr = null;
			int num = <Module>.GetContext(ref <Module>.IID_IObjectContextInfo, (void**)(&ptr));
			if (num >= 0 && null != ptr)
			{
				_GUID a;
				num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID*), ptr, ref a, *(*(long*)ptr + 40L));
				IObjectContextInfo* ptr2 = ptr;
				object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
				if (num == 0)
				{
					Guid result2 = new Guid(a, *(ref a + 4), *(ref a + 6), *(ref a + 8), *(ref a + 9), *(ref a + 10), *(ref a + 11), *(ref a + 12), *(ref a + 13), *(ref a + 14), *(ref a + 15));
					return result2;
				}
			}
			num = ((num == -2147467262) ? -2147164156 : num);
			Marshal.ThrowExceptionForHR(num);
			return result;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000021AC File Offset: 0x000015AC
		public unsafe static int GetMyTransactionVote()
		{
			IContextState* ptr = null;
			int num = <Module>.GetContext(ref <Module>.IID_IContextState, (void**)(&ptr));
			int result;
			if (num >= 0 && null != ptr)
			{
				num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32*), ptr, ref result, *(*(long*)ptr + 48L));
				IContextState* ptr2 = ptr;
				object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
				if (num >= 0)
				{
					return result;
				}
			}
			num = ((num == -2147467262) ? -2147164156 : num);
			Marshal.ThrowExceptionForHR(num);
			return result;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002210 File Offset: 0x00001610
		public unsafe static void SetMyTransactionVote(int vote)
		{
			IContextState* ptr = null;
			int num = <Module>.GetContext(ref <Module>.IID_IContextState, (void**)(&ptr));
			if (num >= 0 && null != ptr)
			{
				num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32), ptr, vote, *(*(long*)ptr + 40L));
				IContextState* ptr2 = ptr;
				object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
			}
			if (num == -2147467262)
			{
				num = -2147164156;
			}
			else if (num >= 0)
			{
				return;
			}
			Marshal.ThrowExceptionForHR(num);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002270 File Offset: 0x00001670
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe static bool GetDeactivateOnReturn()
		{
			IContextState* ptr = null;
			int num = <Module>.GetContext(ref <Module>.IID_IContextState, (void**)(&ptr));
			if (num >= 0 && null != ptr)
			{
				short num2;
				num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int16*), ptr, ref num2, *(*(long*)ptr + 32L));
				IContextState* ptr2 = ptr;
				object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
				if (num >= 0)
				{
					return -1 == num2;
				}
			}
			num = ((num == -2147467262) ? -2147164156 : num);
			Marshal.ThrowExceptionForHR(num);
			return false;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000022D8 File Offset: 0x000016D8
		public unsafe static void SetDeactivateOnReturn([MarshalAs(UnmanagedType.U1)] bool deactivateOnReturn)
		{
			IContextState* ptr = null;
			int num = <Module>.GetContext(ref <Module>.IID_IContextState, (void**)(&ptr));
			if (num >= 0 && null != ptr)
			{
				int num3;
				int num2 = num3 = -1;
				if (!deactivateOnReturn)
				{
					num3 = ~num2;
				}
				short num4 = (short)num3;
				num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int16), ptr, num4, *(*(long*)ptr + 24L));
				IContextState* ptr2 = ptr;
				object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
			}
			if (num == -2147467262)
			{
				num = -2147164156;
			}
			else if (num >= 0)
			{
				return;
			}
			Marshal.ThrowExceptionForHR(num);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000233C File Offset: 0x0000173C
		public unsafe static object GetTransaction()
		{
			IUnknown* ptr = null;
			IObjectContextInfo* ptr2 = null;
			int num = <Module>.GetContext(ref <Module>.IID_IObjectContextInfo, (void**)(&ptr2));
			if (num >= 0 && null != ptr2)
			{
				num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,IUnknown**), ptr2, ref ptr, *(*(long*)ptr2 + 32L));
				IObjectContextInfo* ptr3 = ptr2;
				object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr3, *(*(long*)ptr3 + 16L));
			}
			if (num == -2147467262)
			{
				num = -2147164156;
			}
			else if (num >= 0)
			{
				goto IL_54;
			}
			Marshal.ThrowExceptionForHR(num);
			IL_54:
			object result = null;
			if (ptr != null)
			{
				try
				{
					IntPtr pUnk = new IntPtr((void*)ptr);
					result = Marshal.GetObjectForIUnknown(pUnk);
				}
				finally
				{
					IUnknown* ptr4 = ptr;
					object obj2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr4, *(*(long*)ptr4 + 16L));
				}
			}
			return result;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000023E0 File Offset: 0x000017E0
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe static bool GetTransactionProxyOrTransaction(ref object ppTx, TxInfo pTxInfo)
		{
			IObjectContext* ptr = null;
			pTxInfo.isDtcTransaction = false;
			bool result = false;
			ppTx = null;
			int num = <Module>.GetContext(ref <Module>.IID_IObjectContext, (void**)(&ptr));
			if (num >= 0 && null != ptr)
			{
				IObjectContext* ptr2 = ptr;
				if (calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 64L)))
				{
					result = true;
					IContextTransactionInfoPrivate* ptr3 = null;
					num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), ptr, ref <Module>._GUID_7d40fcc8_f81e_462e_bba1_8a99ebdc826c, ref ptr3, *(*(long*)ptr));
					if (num == 0)
					{
						try
						{
							IUnknown* ptr4 = null;
							num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,IUnknown**), ptr3, ref ptr4, *(*(long*)ptr3 + 24L));
							if (num >= 0)
							{
								if (ptr4 == null)
								{
									pTxInfo.isDtcTransaction = false;
									int isolationLevel;
									uint timeout;
									num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32 modopt(System.Runtime.CompilerServices.IsLong)*,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)*), ptr3, ref isolationLevel, ref timeout, *(*(long*)ptr3 + 40L));
									if (num >= 0)
									{
										pTxInfo.IsolationLevel = isolationLevel;
										pTxInfo.timeout = (int)timeout;
									}
								}
								else
								{
									IUnknown* ptr5 = null;
									num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), ptr4, ref <Module>._GUID_0fb15084_af41_11ce_bd2b_204c4f4f5020, ref ptr5, *(*(long*)ptr4));
									if (num == 0)
									{
										pTxInfo.isDtcTransaction = true;
										IUnknown* ptr6 = ptr5;
										object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr6, *(*(long*)ptr6 + 16L));
									}
									else
									{
										num = ((num == -2147467262) ? 0 : num);
									}
									try
									{
										if (num >= 0)
										{
											IntPtr pUnk = new IntPtr((void*)ptr4);
											ppTx = Marshal.GetObjectForIUnknown(pUnk);
										}
									}
									finally
									{
										IUnknown* ptr7 = ptr4;
										object obj2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr7, *(*(long*)ptr7 + 16L));
									}
								}
							}
							goto IL_1A1;
						}
						finally
						{
							IContextTransactionInfoPrivate* ptr8 = ptr3;
							object obj3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr8, *(*(long*)ptr8 + 16L));
						}
					}
					if (num == -2147467262)
					{
						IObjectContextInfo* ptr9 = null;
						IUnknown* ptr10 = null;
						num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), ptr, ref <Module>.IID_IObjectContextInfo, ref ptr9, *(*(long*)ptr));
						if (num >= 0)
						{
							try
							{
								num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,IUnknown**), ptr9, ref ptr10, *(*(long*)ptr9 + 32L));
								if (num >= 0)
								{
									pTxInfo.isDtcTransaction = true;
									try
									{
										IntPtr pUnk2 = new IntPtr((void*)ptr10);
										ppTx = Marshal.GetObjectForIUnknown(pUnk2);
									}
									finally
									{
										IUnknown* ptr11 = ptr10;
										object obj4 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr11, *(*(long*)ptr11 + 16L));
									}
								}
							}
							finally
							{
								IObjectContextInfo* ptr12 = ptr9;
								object obj5 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr12, *(*(long*)ptr12 + 16L));
							}
						}
					}
				}
				else
				{
					result = false;
				}
				IL_1A1:
				IObjectContext* ptr13 = ptr;
				object obj6 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr13, *(*(long*)ptr13 + 16L));
			}
			else if (num == -2147467262)
			{
				return result;
			}
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
			return result;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002618 File Offset: 0x00001A18
		public unsafe static Guid RegisterTransactionProxy(object pTransactionProxy)
		{
			IContextTransactionInfoPrivate* ptr = null;
			int num = <Module>.GetContext(ref <Module>._GUID_7d40fcc8_f81e_462e_bba1_8a99ebdc826c, (void**)(&ptr));
			if (num >= 0)
			{
				try
				{
					IUnknown* ptr2 = (IUnknown*)Marshal.GetIUnknownForObject(pTransactionProxy).ToPointer();
					ITransactionProxyPrivate* ptr3 = null;
					num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), ptr2, ref <Module>._GUID_02558374_df2e_4dae_bd6b_1d5c994f9bdc, ref ptr3, *(*(long*)ptr2));
					IUnknown* ptr4 = ptr2;
					object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr4, *(*(long*)ptr4 + 16L));
					if (num >= 0)
					{
						_GUID a;
						num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.EnterpriseServices.Thunk.ITransactionProxyPrivate*,_GUID*), ptr, ptr3, ref a, *(*(long*)ptr + 32L));
						ITransactionProxyPrivate* ptr5 = ptr3;
						object obj2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr5, *(*(long*)ptr5 + 16L));
						if (num >= 0)
						{
							Guid result = new Guid(a, *(ref a + 4), *(ref a + 6), *(ref a + 8), *(ref a + 9), *(ref a + 10), *(ref a + 11), *(ref a + 12), *(ref a + 13), *(ref a + 14), *(ref a + 15));
							return result;
						}
					}
				}
				finally
				{
					IContextTransactionInfoPrivate* ptr6 = ptr;
					object obj3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr6, *(*(long*)ptr6 + 16L));
				}
				if (num < 0)
				{
					goto IL_CE;
				}
				goto IL_D4;
			}
			IL_CE:
			Marshal.ThrowExceptionForHR(num);
			IL_D4:
			return Guid.Empty;
		}
	}
}
