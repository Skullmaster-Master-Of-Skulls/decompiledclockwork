using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using System.Web.Security;

namespace System.Web.ClientServices
{
	// Token: 0x0200010D RID: 269
	public class ClientFormsIdentity : IIdentity, IDisposable
	{
		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x00031178 File Offset: 0x0002F378
		public string Name
		{
			get
			{
				return this._Name;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x00031180 File Offset: 0x0002F380
		public bool IsAuthenticated
		{
			get
			{
				return this._IsAuthenticated;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06000DFB RID: 3579 RVA: 0x00031188 File Offset: 0x0002F388
		public string AuthenticationType
		{
			get
			{
				return this._AuthenticationType;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x00031190 File Offset: 0x0002F390
		public CookieContainer AuthenticationCookies
		{
			get
			{
				return this._AuthenticationCookies;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x00031198 File Offset: 0x0002F398
		public MembershipProvider Provider
		{
			get
			{
				return this._Provider;
			}
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x000311A0 File Offset: 0x0002F3A0
		public ClientFormsIdentity(string name, string password, MembershipProvider provider, string authenticationType, bool isAuthenticated, CookieContainer authenticationCookies)
		{
			this._Name = name;
			this._AuthenticationType = authenticationType;
			this._IsAuthenticated = isAuthenticated;
			this._AuthenticationCookies = authenticationCookies;
			this._Password = ClientFormsIdentity.GetSecureStringFromString(password);
			this._Provider = provider;
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x000311DA File Offset: 0x0002F3DA
		public void RevalidateUser()
		{
			if (this._Disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			this._Provider.ValidateUser(this._Name, ClientFormsIdentity.GetStringFromSecureString(this._Password));
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x00031212 File Offset: 0x0002F412
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x00031221 File Offset: 0x0002F421
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this._Password != null)
			{
				this._Password.Dispose();
			}
			this._Disposed = true;
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x00031240 File Offset: 0x0002F440
		private static SecureString GetSecureStringFromString(string password)
		{
			char[] array = password.ToCharArray();
			SecureString secureString = new SecureString();
			for (int i = 0; i < array.Length; i++)
			{
				secureString.AppendChar(array[i]);
			}
			secureString.MakeReadOnly();
			return secureString;
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00031278 File Offset: 0x0002F478
		[SecuritySafeCritical]
		private static string GetStringFromSecureString(SecureString securePass)
		{
			IntPtr intPtr = IntPtr.Zero;
			string result;
			try
			{
				intPtr = Marshal.SecureStringToBSTR(securePass);
				result = Marshal.PtrToStringBSTR(intPtr);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeBSTR(intPtr);
				}
			}
			return result;
		}

		// Token: 0x040003EF RID: 1007
		private string _Name;

		// Token: 0x040003F0 RID: 1008
		private bool _IsAuthenticated;

		// Token: 0x040003F1 RID: 1009
		private string _AuthenticationType;

		// Token: 0x040003F2 RID: 1010
		private CookieContainer _AuthenticationCookies;

		// Token: 0x040003F3 RID: 1011
		private SecureString _Password;

		// Token: 0x040003F4 RID: 1012
		private MembershipProvider _Provider;

		// Token: 0x040003F5 RID: 1013
		private bool _Disposed;
	}
}
