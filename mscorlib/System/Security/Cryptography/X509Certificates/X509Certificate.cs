using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Util;
using System.Text;
using Microsoft.Win32;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008C2 RID: 2242
	[ComVisible(true)]
	[Serializable]
	public class X509Certificate : IDeserializationCallback, ISerializable
	{
		// Token: 0x0600517F RID: 20863 RVA: 0x00124017 File Offset: 0x00123017
		public X509Certificate()
		{
		}

		// Token: 0x06005180 RID: 20864 RVA: 0x0012402A File Offset: 0x0012302A
		public X509Certificate(byte[] data)
		{
			if (data != null && data.Length != 0)
			{
				this.LoadCertificateFromBlob(data, null, X509KeyStorageFlags.DefaultKeySet, false);
			}
		}

		// Token: 0x06005181 RID: 20865 RVA: 0x0012404F File Offset: 0x0012304F
		public X509Certificate(byte[] rawData, string password)
		{
			this.LoadCertificateFromBlob(rawData, password, X509KeyStorageFlags.DefaultKeySet, true);
		}

		// Token: 0x06005182 RID: 20866 RVA: 0x0012406C File Offset: 0x0012306C
		public X509Certificate(byte[] rawData, SecureString password)
		{
			this.LoadCertificateFromBlob(rawData, password, X509KeyStorageFlags.DefaultKeySet, true);
		}

		// Token: 0x06005183 RID: 20867 RVA: 0x00124089 File Offset: 0x00123089
		public X509Certificate(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
		{
			this.LoadCertificateFromBlob(rawData, password, keyStorageFlags, true);
		}

		// Token: 0x06005184 RID: 20868 RVA: 0x001240A6 File Offset: 0x001230A6
		public X509Certificate(byte[] rawData, SecureString password, X509KeyStorageFlags keyStorageFlags)
		{
			this.LoadCertificateFromBlob(rawData, password, keyStorageFlags, true);
		}

		// Token: 0x06005185 RID: 20869 RVA: 0x001240C3 File Offset: 0x001230C3
		public X509Certificate(string fileName)
		{
			this.LoadCertificateFromFile(fileName, null, X509KeyStorageFlags.DefaultKeySet);
		}

		// Token: 0x06005186 RID: 20870 RVA: 0x001240DF File Offset: 0x001230DF
		public X509Certificate(string fileName, string password)
		{
			this.LoadCertificateFromFile(fileName, password, X509KeyStorageFlags.DefaultKeySet);
		}

		// Token: 0x06005187 RID: 20871 RVA: 0x001240FB File Offset: 0x001230FB
		public X509Certificate(string fileName, SecureString password)
		{
			this.LoadCertificateFromFile(fileName, password, X509KeyStorageFlags.DefaultKeySet);
		}

		// Token: 0x06005188 RID: 20872 RVA: 0x00124117 File Offset: 0x00123117
		public X509Certificate(string fileName, string password, X509KeyStorageFlags keyStorageFlags)
		{
			this.LoadCertificateFromFile(fileName, password, keyStorageFlags);
		}

		// Token: 0x06005189 RID: 20873 RVA: 0x00124133 File Offset: 0x00123133
		public X509Certificate(string fileName, SecureString password, X509KeyStorageFlags keyStorageFlags)
		{
			this.LoadCertificateFromFile(fileName, password, keyStorageFlags);
		}

		// Token: 0x0600518A RID: 20874 RVA: 0x00124150 File Offset: 0x00123150
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public X509Certificate(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidHandle"), "handle");
			}
			X509Utils._DuplicateCertContext(handle, ref this.m_safeCertContext);
		}

		// Token: 0x0600518B RID: 20875 RVA: 0x0012419C File Offset: 0x0012319C
		public X509Certificate(X509Certificate cert)
		{
			if (cert == null)
			{
				throw new ArgumentNullException("cert");
			}
			if (cert.m_safeCertContext.pCertContext != IntPtr.Zero)
			{
				X509Utils._DuplicateCertContext(cert.m_safeCertContext.pCertContext, ref this.m_safeCertContext);
			}
			GC.KeepAlive(cert.m_safeCertContext);
		}

		// Token: 0x0600518C RID: 20876 RVA: 0x00124200 File Offset: 0x00123200
		public X509Certificate(SerializationInfo info, StreamingContext context)
		{
			byte[] array = (byte[])info.GetValue("RawData", typeof(byte[]));
			if (array != null)
			{
				this.LoadCertificateFromBlob(array, null, X509KeyStorageFlags.DefaultKeySet, false);
			}
		}

		// Token: 0x0600518D RID: 20877 RVA: 0x00124246 File Offset: 0x00123246
		public static X509Certificate CreateFromCertFile(string filename)
		{
			return new X509Certificate(filename);
		}

		// Token: 0x0600518E RID: 20878 RVA: 0x0012424E File Offset: 0x0012324E
		public static X509Certificate CreateFromSignedFile(string filename)
		{
			return new X509Certificate(filename);
		}

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x0600518F RID: 20879 RVA: 0x00124256 File Offset: 0x00123256
		[ComVisible(false)]
		public IntPtr Handle
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return this.m_safeCertContext.pCertContext;
			}
		}

		// Token: 0x06005190 RID: 20880 RVA: 0x00124263 File Offset: 0x00123263
		[Obsolete("This method has been deprecated.  Please use the Subject property instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public virtual string GetName()
		{
			if (this.m_safeCertContext.IsInvalid)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidHandle"), "m_safeCertContext");
			}
			return X509Utils._GetSubjectInfo(this.m_safeCertContext, 2U, true);
		}

		// Token: 0x06005191 RID: 20881 RVA: 0x00124294 File Offset: 0x00123294
		[Obsolete("This method has been deprecated.  Please use the Issuer property instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public virtual string GetIssuerName()
		{
			if (this.m_safeCertContext.IsInvalid)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidHandle"), "m_safeCertContext");
			}
			return X509Utils._GetIssuerName(this.m_safeCertContext, true);
		}

		// Token: 0x06005192 RID: 20882 RVA: 0x001242C4 File Offset: 0x001232C4
		public virtual byte[] GetSerialNumber()
		{
			if (this.m_safeCertContext.IsInvalid)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidHandle"), "m_safeCertContext");
			}
			if (this.m_serialNumber == null)
			{
				this.m_serialNumber = X509Utils._GetSerialNumber(this.m_safeCertContext);
			}
			return (byte[])this.m_serialNumber.Clone();
		}

		// Token: 0x06005193 RID: 20883 RVA: 0x0012431C File Offset: 0x0012331C
		public virtual string GetSerialNumberString()
		{
			return this.SerialNumber;
		}

		// Token: 0x06005194 RID: 20884 RVA: 0x00124324 File Offset: 0x00123324
		public virtual byte[] GetKeyAlgorithmParameters()
		{
			if (this.m_safeCertContext.IsInvalid)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidHandle"), "m_safeCertContext");
			}
			if (this.m_publicKeyParameters == null)
			{
				this.m_publicKeyParameters = X509Utils._GetPublicKeyParameters(this.m_safeCertContext);
			}
			return (byte[])this.m_publicKeyParameters.Clone();
		}

		// Token: 0x06005195 RID: 20885 RVA: 0x0012437C File Offset: 0x0012337C
		public virtual string GetKeyAlgorithmParametersString()
		{
			if (this.m_safeCertContext.IsInvalid)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidHandle"), "m_safeCertContext");
			}
			return Hex.EncodeHexString(this.GetKeyAlgorithmParameters());
		}

		// Token: 0x06005196 RID: 20886 RVA: 0x001243AC File Offset: 0x001233AC
		public virtual string GetKeyAlgorithm()
		{
			if (this.m_safeCertContext.IsInvalid)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidHandle"), "m_safeCertContext");
			}
			if (this.m_publicKeyOid == null)
			{
				this.m_publicKeyOid = X509Utils._GetPublicKeyOid(this.m_safeCertContext);
			}
			return this.m_publicKeyOid;
		}

		// Token: 0x06005197 RID: 20887 RVA: 0x001243FC File Offset: 0x001233FC
		public virtual byte[] GetPublicKey()
		{
			if (this.m_safeCertContext.IsInvalid)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidHandle"), "m_safeCertContext");
			}
			if (this.m_publicKeyValue == null)
			{
				this.m_publicKeyValue = X509Utils._GetPublicKeyValue(this.m_safeCertContext);
			}
			return (byte[])this.m_publicKeyValue.Clone();
		}

		// Token: 0x06005198 RID: 20888 RVA: 0x00124454 File Offset: 0x00123454
		public virtual string GetPublicKeyString()
		{
			return Hex.EncodeHexString(this.GetPublicKey());
		}

		// Token: 0x06005199 RID: 20889 RVA: 0x00124461 File Offset: 0x00123461
		public virtual byte[] GetRawCertData()
		{
			return this.RawData;
		}

		// Token: 0x0600519A RID: 20890 RVA: 0x00124469 File Offset: 0x00123469
		public virtual string GetRawCertDataString()
		{
			return Hex.EncodeHexString(this.GetRawCertData());
		}

		// Token: 0x0600519B RID: 20891 RVA: 0x00124476 File Offset: 0x00123476
		public virtual byte[] GetCertHash()
		{
			this.SetThumbprint();
			return (byte[])this.m_thumbprint.Clone();
		}

		// Token: 0x0600519C RID: 20892 RVA: 0x0012448E File Offset: 0x0012348E
		public virtual string GetCertHashString()
		{
			this.SetThumbprint();
			return Hex.EncodeHexString(this.m_thumbprint);
		}

		// Token: 0x0600519D RID: 20893 RVA: 0x001244A4 File Offset: 0x001234A4
		public virtual string GetEffectiveDateString()
		{
			return this.NotBefore.ToString();
		}

		// Token: 0x0600519E RID: 20894 RVA: 0x001244C8 File Offset: 0x001234C8
		public virtual string GetExpirationDateString()
		{
			return this.NotAfter.ToString();
		}

		// Token: 0x0600519F RID: 20895 RVA: 0x001244EC File Offset: 0x001234EC
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			if (!(obj is X509Certificate))
			{
				return false;
			}
			X509Certificate other = (X509Certificate)obj;
			return this.Equals(other);
		}

		// Token: 0x060051A0 RID: 20896 RVA: 0x00124514 File Offset: 0x00123514
		public virtual bool Equals(X509Certificate other)
		{
			if (other == null)
			{
				return false;
			}
			if (this.m_safeCertContext.IsInvalid)
			{
				return other.m_safeCertContext.IsInvalid;
			}
			return this.Issuer.Equals(other.Issuer) && this.SerialNumber.Equals(other.SerialNumber);
		}

		// Token: 0x060051A1 RID: 20897 RVA: 0x0012456C File Offset: 0x0012356C
		public override int GetHashCode()
		{
			if (this.m_safeCertContext.IsInvalid)
			{
				return 0;
			}
			this.SetThumbprint();
			int num = 0;
			int num2 = 0;
			while (num2 < this.m_thumbprint.Length && num2 < 4)
			{
				num = (num << 8 | (int)this.m_thumbprint[num2]);
				num2++;
			}
			return num;
		}

		// Token: 0x060051A2 RID: 20898 RVA: 0x001245B5 File Offset: 0x001235B5
		public override string ToString()
		{
			return this.ToString(false);
		}

		// Token: 0x060051A3 RID: 20899 RVA: 0x001245C0 File Offset: 0x001235C0
		public virtual string ToString(bool fVerbose)
		{
			if (!fVerbose || this.m_safeCertContext.IsInvalid)
			{
				return base.GetType().FullName;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[Subject]" + Environment.NewLine + "  ");
			stringBuilder.Append(this.Subject);
			stringBuilder.Append(string.Concat(new string[]
			{
				Environment.NewLine,
				Environment.NewLine,
				"[Issuer]",
				Environment.NewLine,
				"  "
			}));
			stringBuilder.Append(this.Issuer);
			stringBuilder.Append(string.Concat(new string[]
			{
				Environment.NewLine,
				Environment.NewLine,
				"[Serial Number]",
				Environment.NewLine,
				"  "
			}));
			stringBuilder.Append(this.SerialNumber);
			stringBuilder.Append(string.Concat(new string[]
			{
				Environment.NewLine,
				Environment.NewLine,
				"[Not Before]",
				Environment.NewLine,
				"  "
			}));
			stringBuilder.Append(this.NotBefore);
			stringBuilder.Append(string.Concat(new string[]
			{
				Environment.NewLine,
				Environment.NewLine,
				"[Not After]",
				Environment.NewLine,
				"  "
			}));
			stringBuilder.Append(this.NotAfter);
			stringBuilder.Append(string.Concat(new string[]
			{
				Environment.NewLine,
				Environment.NewLine,
				"[Thumbprint]",
				Environment.NewLine,
				"  "
			}));
			stringBuilder.Append(this.GetCertHashString());
			stringBuilder.Append(Environment.NewLine);
			return stringBuilder.ToString();
		}

		// Token: 0x060051A4 RID: 20900 RVA: 0x001247AE File Offset: 0x001237AE
		public virtual string GetFormat()
		{
			return "X509";
		}

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x060051A5 RID: 20901 RVA: 0x001247B8 File Offset: 0x001237B8
		public string Issuer
		{
			get
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				if (this.m_issuerName == null)
				{
					this.m_issuerName = X509Utils._GetIssuerName(this.m_safeCertContext, false);
				}
				return this.m_issuerName;
			}
		}

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x060051A6 RID: 20902 RVA: 0x00124808 File Offset: 0x00123808
		public string Subject
		{
			get
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				if (this.m_subjectName == null)
				{
					this.m_subjectName = X509Utils._GetSubjectInfo(this.m_safeCertContext, 2U, false);
				}
				return this.m_subjectName;
			}
		}

		// Token: 0x060051A7 RID: 20903 RVA: 0x00124858 File Offset: 0x00123858
		[ComVisible(false)]
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public virtual void Import(byte[] rawData)
		{
			this.Reset();
			this.LoadCertificateFromBlob(rawData, null, X509KeyStorageFlags.DefaultKeySet, false);
		}

		// Token: 0x060051A8 RID: 20904 RVA: 0x0012486A File Offset: 0x0012386A
		[ComVisible(false)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		public virtual void Import(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Reset();
			this.LoadCertificateFromBlob(rawData, password, keyStorageFlags, true);
		}

		// Token: 0x060051A9 RID: 20905 RVA: 0x0012487C File Offset: 0x0012387C
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		public virtual void Import(byte[] rawData, SecureString password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Reset();
			this.LoadCertificateFromBlob(rawData, password, keyStorageFlags, true);
		}

		// Token: 0x060051AA RID: 20906 RVA: 0x0012488E File Offset: 0x0012388E
		[ComVisible(false)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		public virtual void Import(string fileName)
		{
			this.Reset();
			this.LoadCertificateFromFile(fileName, null, X509KeyStorageFlags.DefaultKeySet);
		}

		// Token: 0x060051AB RID: 20907 RVA: 0x0012489F File Offset: 0x0012389F
		[ComVisible(false)]
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public virtual void Import(string fileName, string password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Reset();
			this.LoadCertificateFromFile(fileName, password, keyStorageFlags);
		}

		// Token: 0x060051AC RID: 20908 RVA: 0x001248B0 File Offset: 0x001238B0
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public virtual void Import(string fileName, SecureString password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Reset();
			this.LoadCertificateFromFile(fileName, password, keyStorageFlags);
		}

		// Token: 0x060051AD RID: 20909 RVA: 0x001248C1 File Offset: 0x001238C1
		[ComVisible(false)]
		public virtual byte[] Export(X509ContentType contentType)
		{
			return this.ExportHelper(contentType, null);
		}

		// Token: 0x060051AE RID: 20910 RVA: 0x001248CB File Offset: 0x001238CB
		[ComVisible(false)]
		public virtual byte[] Export(X509ContentType contentType, string password)
		{
			return this.ExportHelper(contentType, password);
		}

		// Token: 0x060051AF RID: 20911 RVA: 0x001248D5 File Offset: 0x001238D5
		public virtual byte[] Export(X509ContentType contentType, SecureString password)
		{
			return this.ExportHelper(contentType, password);
		}

		// Token: 0x060051B0 RID: 20912 RVA: 0x001248E0 File Offset: 0x001238E0
		[ComVisible(false)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		public virtual void Reset()
		{
			this.m_subjectName = null;
			this.m_issuerName = null;
			this.m_serialNumber = null;
			this.m_publicKeyParameters = null;
			this.m_publicKeyValue = null;
			this.m_publicKeyOid = null;
			this.m_rawData = null;
			this.m_thumbprint = null;
			this.m_notBefore = DateTime.MinValue;
			this.m_notAfter = DateTime.MinValue;
			if (!this.m_safeCertContext.IsInvalid)
			{
				this.m_safeCertContext.Dispose();
				this.m_safeCertContext = SafeCertContextHandle.InvalidHandle;
			}
		}

		// Token: 0x060051B1 RID: 20913 RVA: 0x0012495E File Offset: 0x0012395E
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (this.m_safeCertContext.IsInvalid)
			{
				info.AddValue("RawData", null);
				return;
			}
			info.AddValue("RawData", this.RawData);
		}

		// Token: 0x060051B2 RID: 20914 RVA: 0x0012498B File Offset: 0x0012398B
		void IDeserializationCallback.OnDeserialization(object sender)
		{
		}

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x060051B3 RID: 20915 RVA: 0x0012498D File Offset: 0x0012398D
		internal SafeCertContextHandle CertContext
		{
			get
			{
				return this.m_safeCertContext;
			}
		}

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x060051B4 RID: 20916 RVA: 0x00124998 File Offset: 0x00123998
		private DateTime NotAfter
		{
			get
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				if (this.m_notAfter == DateTime.MinValue)
				{
					Win32Native.FILE_TIME file_TIME = default(Win32Native.FILE_TIME);
					X509Utils._GetDateNotAfter(this.m_safeCertContext, ref file_TIME);
					this.m_notAfter = DateTime.FromFileTime(file_TIME.ToTicks());
				}
				return this.m_notAfter;
			}
		}

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x060051B5 RID: 20917 RVA: 0x00124A08 File Offset: 0x00123A08
		private DateTime NotBefore
		{
			get
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				if (this.m_notBefore == DateTime.MinValue)
				{
					Win32Native.FILE_TIME file_TIME = default(Win32Native.FILE_TIME);
					X509Utils._GetDateNotBefore(this.m_safeCertContext, ref file_TIME);
					this.m_notBefore = DateTime.FromFileTime(file_TIME.ToTicks());
				}
				return this.m_notBefore;
			}
		}

		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x060051B6 RID: 20918 RVA: 0x00124A78 File Offset: 0x00123A78
		private byte[] RawData
		{
			get
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				if (this.m_rawData == null)
				{
					this.m_rawData = X509Utils._GetCertRawData(this.m_safeCertContext);
				}
				return (byte[])this.m_rawData.Clone();
			}
		}

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x060051B7 RID: 20919 RVA: 0x00124AD0 File Offset: 0x00123AD0
		private string SerialNumber
		{
			get
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				if (this.m_serialNumber == null)
				{
					this.m_serialNumber = X509Utils._GetSerialNumber(this.m_safeCertContext);
				}
				return Hex.EncodeHexStringFromInt(this.m_serialNumber);
			}
		}

		// Token: 0x060051B8 RID: 20920 RVA: 0x00124B23 File Offset: 0x00123B23
		private void SetThumbprint()
		{
			if (this.m_safeCertContext.IsInvalid)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidHandle"), "m_safeCertContext");
			}
			if (this.m_thumbprint == null)
			{
				this.m_thumbprint = X509Utils._GetThumbprint(this.m_safeCertContext);
			}
		}

		// Token: 0x060051B9 RID: 20921 RVA: 0x00124B60 File Offset: 0x00123B60
		private byte[] ExportHelper(X509ContentType contentType, object password)
		{
			switch (contentType)
			{
			case X509ContentType.Cert:
			case X509ContentType.SerializedCert:
				break;
			case X509ContentType.Pfx:
			{
				KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.Open | KeyContainerPermissionFlags.Export);
				keyContainerPermission.Demand();
				break;
			}
			default:
				throw new CryptographicException(Environment.GetResourceString("Cryptography_X509_InvalidContentType"));
			}
			IntPtr intPtr = IntPtr.Zero;
			byte[] array = null;
			SafeCertStoreHandle safeCertStoreHandle = X509Utils.ExportCertToMemoryStore(this);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				intPtr = X509Utils.PasswordToCoTaskMemUni(password);
				array = X509Utils._ExportCertificatesToBlob(safeCertStoreHandle, contentType, intPtr);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.ZeroFreeCoTaskMemUnicode(intPtr);
				}
				safeCertStoreHandle.Dispose();
			}
			if (array == null)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_X509_ExportFailed"));
			}
			return array;
		}

		// Token: 0x060051BA RID: 20922 RVA: 0x00124C0C File Offset: 0x00123C0C
		private void LoadCertificateFromBlob(byte[] rawData, object password, X509KeyStorageFlags keyStorageFlags, bool passwordProvided)
		{
			if (rawData == null || rawData.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EmptyOrNullArray"), "rawData");
			}
			X509ContentType x509ContentType = X509Utils.MapContentType(X509Utils._QueryCertBlobType(rawData));
			if (x509ContentType == X509ContentType.Pfx && (keyStorageFlags & X509KeyStorageFlags.PersistKeySet) == X509KeyStorageFlags.PersistKeySet)
			{
				KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.Create);
				keyContainerPermission.Demand();
			}
			if (x509ContentType == X509ContentType.Pfx)
			{
				X509Certificate.EnforceIterationCountLimit(rawData, false, passwordProvided);
			}
			uint dwFlags = X509Utils.MapKeyStorageFlags(keyStorageFlags);
			IntPtr intPtr = IntPtr.Zero;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				intPtr = X509Utils.PasswordToCoTaskMemUni(password);
				X509Utils._LoadCertFromBlob(rawData, intPtr, dwFlags, (keyStorageFlags & X509KeyStorageFlags.PersistKeySet) != X509KeyStorageFlags.DefaultKeySet, ref this.m_safeCertContext);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.ZeroFreeCoTaskMemUnicode(intPtr);
				}
			}
		}

		// Token: 0x060051BB RID: 20923 RVA: 0x00124CC0 File Offset: 0x00123CC0
		private void LoadCertificateFromFile(string fileName, object password, X509KeyStorageFlags keyStorageFlags)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			string fullPathInternal = Path.GetFullPathInternal(fileName);
			new FileIOPermission(FileIOPermissionAccess.Read, fullPathInternal).Demand();
			X509ContentType x509ContentType = X509Utils.MapContentType(X509Utils._QueryCertFileType(fileName));
			if (x509ContentType == X509ContentType.Pfx && (keyStorageFlags & X509KeyStorageFlags.PersistKeySet) == X509KeyStorageFlags.PersistKeySet)
			{
				KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.Create);
				keyContainerPermission.Demand();
			}
			uint dwFlags = X509Utils.MapKeyStorageFlags(keyStorageFlags);
			IntPtr intPtr = IntPtr.Zero;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				intPtr = X509Utils.PasswordToCoTaskMemUni(password);
				X509Utils._LoadCertFromFile(fileName, intPtr, dwFlags, (keyStorageFlags & X509KeyStorageFlags.PersistKeySet) != X509KeyStorageFlags.DefaultKeySet, ref this.m_safeCertContext);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.ZeroFreeCoTaskMemUnicode(intPtr);
				}
			}
		}

		// Token: 0x060051BC RID: 20924 RVA: 0x00124D70 File Offset: 0x00123D70
		[SecurityCritical]
		[SecurityTreatAsSafe]
		[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
		private static long ReadSecuritySwitch()
		{
			long result = 0L;
			string environmentVariable = Environment.GetEnvironmentVariable("COMPlus_Pkcs12UnspecifiedPasswordIterationLimit");
			if (environmentVariable != null && long.TryParse(environmentVariable, out result))
			{
				return result;
			}
			if (X509Certificate.ReadSettingsFromRegistry(Registry.CurrentUser, ref result))
			{
				return result;
			}
			if (X509Certificate.ReadSettingsFromRegistry(Registry.LocalMachine, ref result))
			{
				return result;
			}
			return 600000L;
		}

		// Token: 0x060051BD RID: 20925 RVA: 0x00124DC0 File Offset: 0x00123DC0
		[SecurityCritical]
		[SecurityTreatAsSafe]
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		private static bool ReadSettingsFromRegistry(RegistryKey regKey, ref long value)
		{
			try
			{
				using (RegistryKey registryKey = regKey.OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework", false))
				{
					if (registryKey != null)
					{
						object value2 = registryKey.GetValue("Pkcs12UnspecifiedPasswordIterationLimit");
						if (value2 != null)
						{
							value = Convert.ToInt64(value2, CultureInfo.InvariantCulture);
							return true;
						}
					}
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x060051BE RID: 20926 RVA: 0x00124E30 File Offset: 0x00123E30
		internal static void EnforceIterationCountLimit(byte[] pkcs12, bool readingFromFile, bool passwordProvided)
		{
			if (readingFromFile || passwordProvided)
			{
				return;
			}
			long num = X509Certificate.s_pkcs12UnspecifiedPasswordIterationLimit;
			if (num == -1L)
			{
				return;
			}
			if (num < 0L)
			{
				num = 600000L;
			}
			checked
			{
				try
				{
					try
					{
						KdfWorkLimiter.SetIterationLimit((ulong)num);
						ulong iterationCount = X509Certificate.GetIterationCount(pkcs12);
						if (iterationCount > (ulong)num || KdfWorkLimiter.WasWorkLimitExceeded())
						{
							throw new CryptographicException();
						}
					}
					finally
					{
						KdfWorkLimiter.ResetIterationLimit();
					}
				}
				catch (Exception inner)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_X509_PfxWithoutPassword"), inner);
				}
			}
		}

		// Token: 0x060051BF RID: 20927 RVA: 0x00124EB4 File Offset: 0x00123EB4
		private static ulong GetIterationCount(byte[] pkcs12)
		{
			ReadOnlyMemory<byte> rebind = new ReadOnlyMemory<byte>(pkcs12);
			AsnValueReader asnValueReader = new AsnValueReader(pkcs12, AsnEncodingRules.BER);
			PfxAsn pfxAsn;
			PfxAsn.Decode(ref asnValueReader, rebind, out pfxAsn);
			return pfxAsn.CountTotalIterations();
		}

		// Token: 0x04002A1A RID: 10778
		private const long DefaultPkcs12UnspecifiedPasswordIterationLimit = 600000L;

		// Token: 0x04002A1B RID: 10779
		private const string m_format = "X509";

		// Token: 0x04002A1C RID: 10780
		private string m_subjectName;

		// Token: 0x04002A1D RID: 10781
		private string m_issuerName;

		// Token: 0x04002A1E RID: 10782
		private byte[] m_serialNumber;

		// Token: 0x04002A1F RID: 10783
		private byte[] m_publicKeyParameters;

		// Token: 0x04002A20 RID: 10784
		private byte[] m_publicKeyValue;

		// Token: 0x04002A21 RID: 10785
		private string m_publicKeyOid;

		// Token: 0x04002A22 RID: 10786
		private byte[] m_rawData;

		// Token: 0x04002A23 RID: 10787
		private byte[] m_thumbprint;

		// Token: 0x04002A24 RID: 10788
		private DateTime m_notBefore;

		// Token: 0x04002A25 RID: 10789
		private DateTime m_notAfter;

		// Token: 0x04002A26 RID: 10790
		private SafeCertContextHandle m_safeCertContext = SafeCertContextHandle.InvalidHandle;

		// Token: 0x04002A27 RID: 10791
		private static long s_pkcs12UnspecifiedPasswordIterationLimit = X509Certificate.ReadSecuritySwitch();
	}
}
