using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200025F RID: 607
	internal sealed class ServiceMonikerInternal : ContextBoundObject, IMoniker, IParseDisplayName, IDisposable
	{
		// Token: 0x06001181 RID: 4481 RVA: 0x0004027A File Offset: 0x0003E47A
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0004027C File Offset: 0x0003E47C
		public ServiceMonikerInternal()
		{
			this.PropertyTable = new Dictionary<MonikerHelper.MonikerAttribute, string>();
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x0004028F File Offset: 0x0003E48F
		void IMoniker.GetClassID(out Guid clsid)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x000402A0 File Offset: 0x0003E4A0
		int IMoniker.IsDirty()
		{
			return HR.S_FALSE;
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x000402A7 File Offset: 0x0003E4A7
		void IMoniker.Load(IStream stream)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x000402B8 File Offset: 0x0003E4B8
		void IMoniker.Save(IStream stream, bool isDirty)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x000402C9 File Offset: 0x0003E4C9
		void IMoniker.GetSizeMax(out long size)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x000402DA File Offset: 0x0003E4DA
		void IMoniker.BindToStorage(IBindCtx pbc, IMoniker pmkToLeft, ref Guid riid, out object ppvObj)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x000402EB File Offset: 0x0003E4EB
		void IMoniker.BindToObject(IBindCtx pbc, IMoniker pmkToLeft, ref Guid riidResult, IntPtr ppvResult)
		{
			ProxyBuilder.Build(this.PropertyTable, ref riidResult, ppvResult);
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x000402FB File Offset: 0x0003E4FB
		void IMoniker.Hash(IntPtr pdwHash)
		{
			if (IntPtr.Zero == pdwHash)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("pdwHash");
			}
			Marshal.WriteInt32(pdwHash, 0);
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x00040321 File Offset: 0x0003E521
		void IMoniker.CommonPrefixWith(IMoniker pmkOther, out IMoniker ppmkPrefix)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x00040332 File Offset: 0x0003E532
		void IMoniker.ComposeWith(IMoniker pmkRight, bool fOnlyIfNotGeneric, out IMoniker ppmkComposite)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00040343 File Offset: 0x0003E543
		void IMoniker.Enum(bool fForward, out IEnumMoniker ppenumMoniker)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00040354 File Offset: 0x0003E554
		void IMoniker.GetDisplayName(IBindCtx pbc, IMoniker pmkToLeft, out string ppszDisplayName)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x00040365 File Offset: 0x0003E565
		void IMoniker.GetTimeOfLastChange(IBindCtx pbc, IMoniker pmkToLeft, out System.Runtime.InteropServices.ComTypes.FILETIME pFileTime)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x00040376 File Offset: 0x0003E576
		void IMoniker.Inverse(out IMoniker ppmk)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00040387 File Offset: 0x0003E587
		int IMoniker.IsEqual(IMoniker pmkOtherMoniker)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00040398 File Offset: 0x0003E598
		int IMoniker.IsRunning(IBindCtx pbc, IMoniker pmkToLeft, IMoniker pmkNewlyRunning)
		{
			return HR.S_FALSE;
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x0004039F File Offset: 0x0003E59F
		int IMoniker.IsSystemMoniker(IntPtr pdwMksys)
		{
			if (IntPtr.Zero == pdwMksys)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("pdwMksys");
			}
			Marshal.WriteInt32(pdwMksys, 0);
			return HR.S_FALSE;
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x000403CA File Offset: 0x0003E5CA
		void IMoniker.ParseDisplayName(IBindCtx pbc, IMoniker pmkToLeft, string pszDisplayName, out int pchEaten, out IMoniker ppmkOut)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x000403DB File Offset: 0x0003E5DB
		void IMoniker.Reduce(IBindCtx pbc, int dwReduceHowFar, ref IMoniker ppmkToLeft, out IMoniker ppmkReduced)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x000403EC File Offset: 0x0003E5EC
		void IMoniker.RelativePathTo(IMoniker pmkOther, out IMoniker ppmkRelPath)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x00040400 File Offset: 0x0003E600
		void IParseDisplayName.ParseDisplayName(IBindCtx pbc, string pszDisplayName, IntPtr pchEaten, IntPtr ppmkOut)
		{
			if (IntPtr.Zero == ppmkOut)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("ppmkOut");
			}
			Marshal.WriteIntPtr(ppmkOut, IntPtr.Zero);
			if (IntPtr.Zero == pchEaten)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("pchEaten");
			}
			if (string.IsNullOrEmpty(pszDisplayName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("pszDisplayName");
			}
			MonikerUtility.Parse(pszDisplayName, ref this.PropertyTable);
			ComPlusServiceMonikerTrace.Trace(TraceEventType.Verbose, 327708, "TraceCodeComIntegrationServiceMonikerParsed", this.PropertyTable);
			Marshal.WriteInt32(pchEaten, pszDisplayName.Length);
			IntPtr interfacePtrForObject = InterfaceHelper.GetInterfacePtrForObject(typeof(IMoniker).GUID, this);
			Marshal.WriteIntPtr(ppmkOut, interfacePtrForObject);
		}

		// Token: 0x0400198C RID: 6540
		private Dictionary<MonikerHelper.MonikerAttribute, string> PropertyTable;
	}
}
