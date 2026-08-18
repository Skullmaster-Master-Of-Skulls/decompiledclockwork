using System;
using System.Runtime.InteropServices;

namespace System.Web.Configuration
{
	// Token: 0x0200070C RID: 1804
	internal class IISVersionHelper : IDisposable
	{
		// Token: 0x06005704 RID: 22276 RVA: 0x001302B4 File Offset: 0x0012E4B4
		internal IISVersionHelper(string version)
		{
			if (version == null)
			{
				return;
			}
			try
			{
				this._versionManager = IISVersionHelper.CreateVersionManager();
				this._version = this._versionManager.GetVersionObject(version, 2);
				this._version.ApplyManifestContext();
			}
			catch
			{
				this.Release();
				throw;
			}
		}

		// Token: 0x06005705 RID: 22277 RVA: 0x00130310 File Offset: 0x0012E510
		private static IISVersionHelper.IIISVersionManager CreateVersionManager()
		{
			Type typeFromProgID = Type.GetTypeFromProgID("Microsoft.IIS.VersionManager", true);
			return (IISVersionHelper.IIISVersionManager)Activator.CreateInstance(typeFromProgID);
		}

		// Token: 0x06005706 RID: 22278 RVA: 0x00130334 File Offset: 0x0012E534
		public void Dispose()
		{
			if (this._version != null)
			{
				this._version.ClearManifestContext();
				this.Release();
			}
		}

		// Token: 0x06005707 RID: 22279 RVA: 0x0013034F File Offset: 0x0012E54F
		private void Release()
		{
			if (this._version != null)
			{
				Marshal.ReleaseComObject(this._version);
				this._version = null;
			}
			if (this._versionManager != null)
			{
				Marshal.ReleaseComObject(this._versionManager);
				this._versionManager = null;
			}
		}

		// Token: 0x04002E35 RID: 11829
		private const int IIS_PRODUCT_EXPRESS = 2;

		// Token: 0x04002E36 RID: 11830
		private IISVersionHelper.IIISVersionManager _versionManager;

		// Token: 0x04002E37 RID: 11831
		private IISVersionHelper.IIISVersion _version;

		// Token: 0x02000A45 RID: 2629
		[Guid("1B036F99-B240-4116-A6A0-B54EC5B2438E")]
		[InterfaceType(1)]
		[ComImport]
		private interface IIISVersion
		{
			// Token: 0x06006E9A RID: 28314
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetPropertyValue([MarshalAs(UnmanagedType.BStr)] [In] string bstrName);

			// Token: 0x06006E9B RID: 28315
			[return: MarshalAs(UnmanagedType.Struct)]
			object CreateObjectFromProgId([MarshalAs(UnmanagedType.BStr)] [In] string bstrObjectName);

			// Token: 0x06006E9C RID: 28316
			[return: MarshalAs(UnmanagedType.Struct)]
			object CreateObjectFromClsId([In] Guid clsidObject);

			// Token: 0x06006E9D RID: 28317
			void ApplyIISEnvironmentVariables();

			// Token: 0x06006E9E RID: 28318
			void ClearIISEnvironmentVariables();

			// Token: 0x06006E9F RID: 28319
			void ApplyManifestContext();

			// Token: 0x06006EA0 RID: 28320
			void ClearManifestContext();
		}

		// Token: 0x02000A46 RID: 2630
		[InterfaceType(1)]
		[Guid("9CDA0717-2EB5-42b3-B5B0-16F4941B2029")]
		[ComImport]
		private interface IIISVersionManager
		{
			// Token: 0x06006EA1 RID: 28321
			[return: MarshalAs(UnmanagedType.Interface)]
			IISVersionHelper.IIISVersion GetVersionObject([MarshalAs(UnmanagedType.BStr)] [In] string bstrVersion, [MarshalAs(UnmanagedType.I4)] [In] int productType);

			// Token: 0x06006EA2 RID: 28322
			[return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT)]
			IISVersionHelper.IIISVersion[] GetAllVersionObjects();
		}
	}
}
