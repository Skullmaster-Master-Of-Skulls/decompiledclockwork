using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;

namespace NLog.Internal
{
	// Token: 0x0200006D RID: 109
	internal class AspHelper
	{
		// Token: 0x060003CC RID: 972 RVA: 0x00009034 File Offset: 0x00007234
		private AspHelper()
		{
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000903C File Offset: 0x0000723C
		public static AspHelper.ISessionObject GetSessionObject()
		{
			AspHelper.ISessionObject result = null;
			AspHelper.IObjectContext objectContext;
			if (NativeMethods.CoGetObjectContext(ref AspHelper.IID_IObjectContext, out objectContext) == 0)
			{
				AspHelper.IGetContextProperties getContextProperties = (AspHelper.IGetContextProperties)objectContext;
				if (getContextProperties != null)
				{
					result = (AspHelper.ISessionObject)getContextProperties.GetProperty("Session");
					Marshal.ReleaseComObject(getContextProperties);
				}
				Marshal.ReleaseComObject(objectContext);
			}
			return result;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00009084 File Offset: 0x00007284
		public static AspHelper.IApplicationObject GetApplicationObject()
		{
			AspHelper.IApplicationObject result = null;
			AspHelper.IObjectContext objectContext;
			if (NativeMethods.CoGetObjectContext(ref AspHelper.IID_IObjectContext, out objectContext) == 0)
			{
				AspHelper.IGetContextProperties getContextProperties = (AspHelper.IGetContextProperties)objectContext;
				if (getContextProperties != null)
				{
					result = (AspHelper.IApplicationObject)getContextProperties.GetProperty("Application");
					Marshal.ReleaseComObject(getContextProperties);
				}
				Marshal.ReleaseComObject(objectContext);
			}
			return result;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x000090CC File Offset: 0x000072CC
		public static AspHelper.IRequest GetRequestObject()
		{
			AspHelper.IRequest result = null;
			AspHelper.IObjectContext objectContext;
			if (NativeMethods.CoGetObjectContext(ref AspHelper.IID_IObjectContext, out objectContext) == 0)
			{
				AspHelper.IGetContextProperties getContextProperties = (AspHelper.IGetContextProperties)objectContext;
				if (getContextProperties != null)
				{
					result = (AspHelper.IRequest)getContextProperties.GetProperty("Request");
					Marshal.ReleaseComObject(getContextProperties);
				}
				Marshal.ReleaseComObject(objectContext);
			}
			return result;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00009114 File Offset: 0x00007314
		public static AspHelper.IResponse GetResponseObject()
		{
			AspHelper.IResponse result = null;
			AspHelper.IObjectContext objectContext;
			if (NativeMethods.CoGetObjectContext(ref AspHelper.IID_IObjectContext, out objectContext) == 0)
			{
				AspHelper.IGetContextProperties getContextProperties = (AspHelper.IGetContextProperties)objectContext;
				if (getContextProperties != null)
				{
					result = (AspHelper.IResponse)getContextProperties.GetProperty("Response");
					Marshal.ReleaseComObject(getContextProperties);
				}
				Marshal.ReleaseComObject(objectContext);
			}
			return result;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000915B File Offset: 0x0000735B
		public static object GetComDefaultProperty(object o)
		{
			if (o == null)
			{
				return null;
			}
			return o.GetType().InvokeMember(string.Empty, BindingFlags.GetProperty, null, o, new object[0], CultureInfo.InvariantCulture);
		}

		// Token: 0x040000D1 RID: 209
		private static Guid IID_IObjectContext = new Guid("51372ae0-cae7-11cf-be81-00aa00a2fa25");

		// Token: 0x0200006E RID: 110
		[Guid("51372ae0-cae7-11cf-be81-00aa00a2fa25")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IObjectContext
		{
		}

		// Token: 0x0200006F RID: 111
		[Guid("51372af4-cae7-11cf-be81-00aa00a2fa25")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IGetContextProperties
		{
			// Token: 0x060003D3 RID: 979
			int Count();

			// Token: 0x060003D4 RID: 980
			object GetProperty(string name);
		}

		// Token: 0x02000070 RID: 112
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[Guid("D97A6DA0-A865-11cf-83AF-00A0C90C2BD8")]
		[ComImport]
		public interface ISessionObject
		{
			// Token: 0x060003D5 RID: 981
			string GetSessionID();

			// Token: 0x060003D6 RID: 982
			object GetValue(string name);

			// Token: 0x060003D7 RID: 983
			void PutValue(string name, object val);

			// Token: 0x060003D8 RID: 984
			int GetTimeout();

			// Token: 0x060003D9 RID: 985
			void PutTimeout(int t);

			// Token: 0x060003DA RID: 986
			void Abandon();

			// Token: 0x060003DB RID: 987
			int GetCodePage();

			// Token: 0x060003DC RID: 988
			void PutCodePage(int cp);

			// Token: 0x060003DD RID: 989
			int GetLCID();

			// Token: 0x060003DE RID: 990
			void PutLCID();
		}

		// Token: 0x02000071 RID: 113
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[Guid("D97A6DA0-A866-11cf-83AE-10A0C90C2BD8")]
		[ComImport]
		public interface IApplicationObject
		{
			// Token: 0x060003DF RID: 991
			object GetValue(string name);

			// Token: 0x060003E0 RID: 992
			void PutValue(string name, object val);
		}

		// Token: 0x02000072 RID: 114
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[Guid("D97A6DA0-A85D-11cf-83AE-00A0C90C2BD8")]
		[ComImport]
		public interface IStringList
		{
			// Token: 0x060003E1 RID: 993
			object GetItem(object key);

			// Token: 0x060003E2 RID: 994
			int GetCount();

			// Token: 0x060003E3 RID: 995
			object NewEnum();
		}

		// Token: 0x02000073 RID: 115
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[Guid("D97A6DA0-A85F-11df-83AE-00A0C90C2BD8")]
		[ComImport]
		public interface IRequestDictionary
		{
			// Token: 0x060003E4 RID: 996
			object GetItem(object var);

			// Token: 0x060003E5 RID: 997
			object NewEnum();

			// Token: 0x060003E6 RID: 998
			int GetCount();

			// Token: 0x060003E7 RID: 999
			object Key(object varKey);
		}

		// Token: 0x02000074 RID: 116
		[Guid("00020400-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IDispatch
		{
		}

		// Token: 0x02000075 RID: 117
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[Guid("D97A6DA0-A861-11cf-93AE-00A0C90C2BD8")]
		[ComImport]
		public interface IRequest
		{
			// Token: 0x060003E8 RID: 1000
			AspHelper.IDispatch GetItem(string name);

			// Token: 0x060003E9 RID: 1001
			AspHelper.IRequestDictionary GetQueryString();

			// Token: 0x060003EA RID: 1002
			AspHelper.IRequestDictionary GetForm();

			// Token: 0x060003EB RID: 1003
			AspHelper.IRequestDictionary GetBody();

			// Token: 0x060003EC RID: 1004
			AspHelper.IRequestDictionary GetServerVariables();

			// Token: 0x060003ED RID: 1005
			AspHelper.IRequestDictionary GetClientCertificates();

			// Token: 0x060003EE RID: 1006
			AspHelper.IRequestDictionary GetCookies();

			// Token: 0x060003EF RID: 1007
			int GetTotalBytes();

			// Token: 0x060003F0 RID: 1008
			void BinaryRead();
		}

		// Token: 0x02000076 RID: 118
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[Guid("D97A6DA0-A864-11cf-83BE-00A0C90C2BD8")]
		[ComImport]
		public interface IResponse
		{
			// Token: 0x060003F1 RID: 1009
			void GetBuffer();

			// Token: 0x060003F2 RID: 1010
			void PutBuffer();

			// Token: 0x060003F3 RID: 1011
			void GetContentType();

			// Token: 0x060003F4 RID: 1012
			void PutContentType();

			// Token: 0x060003F5 RID: 1013
			void GetExpires();

			// Token: 0x060003F6 RID: 1014
			void PutExpires();

			// Token: 0x060003F7 RID: 1015
			void GetExpiresAbsolute();

			// Token: 0x060003F8 RID: 1016
			void PutExpiresAbsolute();

			// Token: 0x060003F9 RID: 1017
			void GetCookies();

			// Token: 0x060003FA RID: 1018
			void GetStatus();

			// Token: 0x060003FB RID: 1019
			void PutStatus();

			// Token: 0x060003FC RID: 1020
			void Add();

			// Token: 0x060003FD RID: 1021
			void AddHeader();

			// Token: 0x060003FE RID: 1022
			void AppendToLog();

			// Token: 0x060003FF RID: 1023
			void BinaryWrite();

			// Token: 0x06000400 RID: 1024
			void Clear();

			// Token: 0x06000401 RID: 1025
			void End();

			// Token: 0x06000402 RID: 1026
			void Flush();

			// Token: 0x06000403 RID: 1027
			void Redirect();

			// Token: 0x06000404 RID: 1028
			void Write(object text);
		}

		// Token: 0x02000077 RID: 119
		[Guid("71EAF260-0CE0-11D0-A53E-00A0C90C2091")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[ComImport]
		public interface IReadCookie
		{
			// Token: 0x06000405 RID: 1029
			void GetItem(object key, out object val);

			// Token: 0x06000406 RID: 1030
			object HasKeys();

			// Token: 0x06000407 RID: 1031
			void GetNewEnum();

			// Token: 0x06000408 RID: 1032
			void GetCount(out int count);

			// Token: 0x06000409 RID: 1033
			object GetKey(object key);
		}
	}
}
