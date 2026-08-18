using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Services;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200023B RID: 571
	[DataContract]
	public class PersistStreamTypeWrapper : IExtensibleDataObject
	{
		// Token: 0x170003DC RID: 988
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x0003E370 File Offset: 0x0003C570
		// (set) Token: 0x060010FF RID: 4351 RVA: 0x0003E378 File Offset: 0x0003C578
		public ExtensionDataObject ExtensionData { get; set; }

		// Token: 0x06001100 RID: 4352 RVA: 0x0003E384 File Offset: 0x0003C584
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public void SetObject<T>(T obj)
		{
			if (Marshal.IsComObject(obj))
			{
				IntPtr iunknownForObject = Marshal.GetIUnknownForObject(obj);
				if (IntPtr.Zero == iunknownForObject)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("UnableToRetrievepUnk")));
				}
				try
				{
					IntPtr zero = IntPtr.Zero;
					Guid guid = typeof(IPersistStream).GUID;
					int num = Marshal.QueryInterface(iunknownForObject, ref guid, out zero);
					if (HR.S_OK == num)
					{
						try
						{
							if (IntPtr.Zero == zero)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("PersistWrapperIsNull")));
							}
							IPersistStream persistStream = (IPersistStream)EnterpriseServicesHelper.WrapIUnknownWithComObject(zero);
							try
							{
								this.dataStream = PersistHelper.PersistIPersistStreamToByteArray(persistStream);
								this.clsid = typeof(T).GUID;
								return;
							}
							finally
							{
								Marshal.ReleaseComObject(persistStream);
							}
						}
						finally
						{
							Marshal.Release(zero);
						}
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CLSIDDoesNotSupportIPersistStream", new object[]
					{
						typeof(T).GUID.ToString("B")
					})));
				}
				finally
				{
					Marshal.Release(iunknownForObject);
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("NotAComObject")));
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x0003E4F4 File Offset: 0x0003C6F4
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public void GetObject<T>(ref T obj)
		{
			if (this.clsid == typeof(T).GUID)
			{
				IntPtr iunknownForObject = Marshal.GetIUnknownForObject(obj);
				if (IntPtr.Zero == iunknownForObject)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("UnableToRetrievepUnk")));
				}
				try
				{
					IntPtr zero = IntPtr.Zero;
					Guid guid = typeof(IPersistStream).GUID;
					int num = Marshal.QueryInterface(iunknownForObject, ref guid, out zero);
					if (HR.S_OK == num)
					{
						try
						{
							if (IntPtr.Zero == zero)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("PersistWrapperIsNull")));
							}
							IPersistStream persistStream = (IPersistStream)EnterpriseServicesHelper.WrapIUnknownWithComObject(zero);
							try
							{
								PersistHelper.LoadIntoObjectFromByteArray(persistStream, this.dataStream);
								return;
							}
							finally
							{
								Marshal.ReleaseComObject(persistStream);
							}
						}
						finally
						{
							Marshal.Release(zero);
						}
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CLSIDDoesNotSupportIPersistStream", new object[]
					{
						typeof(T).GUID.ToString("B")
					})));
				}
				finally
				{
					Marshal.Release(iunknownForObject);
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CLSIDOfTypeDoesNotMatch", new object[]
			{
				typeof(T).GUID.ToString(),
				this.clsid.ToString("B")
			})));
		}

		// Token: 0x04001893 RID: 6291
		[DataMember]
		internal Guid clsid;

		// Token: 0x04001894 RID: 6292
		[DataMember]
		internal byte[] dataStream;
	}
}
