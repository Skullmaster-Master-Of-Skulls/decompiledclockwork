using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004FC RID: 1276
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	public sealed class DllImportAttribute : Attribute
	{
		// Token: 0x06003184 RID: 12676 RVA: 0x000A9534 File Offset: 0x000A8534
		internal static Attribute GetCustomAttribute(RuntimeMethodInfo method)
		{
			if ((method.Attributes & MethodAttributes.PinvokeImpl) == MethodAttributes.PrivateScope)
			{
				return null;
			}
			MetadataImport metadataImport = method.Module.ModuleHandle.GetMetadataImport();
			string dllName = null;
			int metadataToken = method.MetadataToken;
			PInvokeAttributes pinvokeAttributes = PInvokeAttributes.CharSetNotSpec;
			string entryPoint;
			metadataImport.GetPInvokeMap(metadataToken, out pinvokeAttributes, out entryPoint, out dllName);
			CharSet charSet = CharSet.None;
			switch (pinvokeAttributes & PInvokeAttributes.CharSetMask)
			{
			case PInvokeAttributes.CharSetNotSpec:
				charSet = CharSet.None;
				break;
			case PInvokeAttributes.CharSetAnsi:
				charSet = CharSet.Ansi;
				break;
			case PInvokeAttributes.CharSetUnicode:
				charSet = CharSet.Unicode;
				break;
			case PInvokeAttributes.CharSetMask:
				charSet = CharSet.Auto;
				break;
			}
			CallingConvention callingConvention = CallingConvention.Cdecl;
			PInvokeAttributes pinvokeAttributes2 = pinvokeAttributes & PInvokeAttributes.CallConvMask;
			if (pinvokeAttributes2 <= PInvokeAttributes.CallConvCdecl)
			{
				if (pinvokeAttributes2 != PInvokeAttributes.CallConvWinapi)
				{
					if (pinvokeAttributes2 == PInvokeAttributes.CallConvCdecl)
					{
						callingConvention = CallingConvention.Cdecl;
					}
				}
				else
				{
					callingConvention = CallingConvention.Winapi;
				}
			}
			else if (pinvokeAttributes2 != PInvokeAttributes.CallConvStdcall)
			{
				if (pinvokeAttributes2 != PInvokeAttributes.CallConvThiscall)
				{
					if (pinvokeAttributes2 == PInvokeAttributes.CallConvFastcall)
					{
						callingConvention = CallingConvention.FastCall;
					}
				}
				else
				{
					callingConvention = CallingConvention.ThisCall;
				}
			}
			else
			{
				callingConvention = CallingConvention.StdCall;
			}
			bool exactSpelling = (pinvokeAttributes & PInvokeAttributes.NoMangle) != PInvokeAttributes.CharSetNotSpec;
			bool setLastError = (pinvokeAttributes & PInvokeAttributes.SupportsLastError) != PInvokeAttributes.CharSetNotSpec;
			bool bestFitMapping = (pinvokeAttributes & PInvokeAttributes.BestFitMask) == PInvokeAttributes.BestFitEnabled;
			bool throwOnUnmappableChar = (pinvokeAttributes & PInvokeAttributes.ThrowOnUnmappableCharMask) == PInvokeAttributes.ThrowOnUnmappableCharEnabled;
			bool preserveSig = (method.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) != MethodImplAttributes.IL;
			return new DllImportAttribute(dllName, entryPoint, charSet, exactSpelling, setLastError, preserveSig, callingConvention, bestFitMapping, throwOnUnmappableChar);
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x000A967C File Offset: 0x000A867C
		internal static bool IsDefined(RuntimeMethodInfo method)
		{
			return (method.Attributes & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope;
		}

		// Token: 0x06003186 RID: 12678 RVA: 0x000A9690 File Offset: 0x000A8690
		internal DllImportAttribute(string dllName, string entryPoint, CharSet charSet, bool exactSpelling, bool setLastError, bool preserveSig, CallingConvention callingConvention, bool bestFitMapping, bool throwOnUnmappableChar)
		{
			this._val = dllName;
			this.EntryPoint = entryPoint;
			this.CharSet = charSet;
			this.ExactSpelling = exactSpelling;
			this.SetLastError = setLastError;
			this.PreserveSig = preserveSig;
			this.CallingConvention = callingConvention;
			this.BestFitMapping = bestFitMapping;
			this.ThrowOnUnmappableChar = throwOnUnmappableChar;
		}

		// Token: 0x06003187 RID: 12679 RVA: 0x000A96E8 File Offset: 0x000A86E8
		public DllImportAttribute(string dllName)
		{
			this._val = dllName;
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06003188 RID: 12680 RVA: 0x000A96F7 File Offset: 0x000A86F7
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04001992 RID: 6546
		internal string _val;

		// Token: 0x04001993 RID: 6547
		public string EntryPoint;

		// Token: 0x04001994 RID: 6548
		public CharSet CharSet;

		// Token: 0x04001995 RID: 6549
		public bool SetLastError;

		// Token: 0x04001996 RID: 6550
		public bool ExactSpelling;

		// Token: 0x04001997 RID: 6551
		public bool PreserveSig;

		// Token: 0x04001998 RID: 6552
		public CallingConvention CallingConvention;

		// Token: 0x04001999 RID: 6553
		public bool BestFitMapping;

		// Token: 0x0400199A RID: 6554
		public bool ThrowOnUnmappableChar;
	}
}
