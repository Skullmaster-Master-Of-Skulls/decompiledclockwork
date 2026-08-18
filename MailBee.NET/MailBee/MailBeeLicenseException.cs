using System;
using System.Runtime.Serialization;
using a;

namespace MailBee
{
	// Token: 0x02000023 RID: 35
	[Serializable]
	public class MailBeeLicenseException : MailBeeLocalException, IMailBeeFatalException
	{
		// Token: 0x06000110 RID: 272 RVA: 0x00007800 File Offset: 0x00006800
		internal MailBeeLicenseException(bm A_0, Type A_1) : base(MailBeeLicenseException.a(A_0, A_1), 1)
		{
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00007810 File Offset: 0x00006810
		protected MailBeeLicenseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000781C File Offset: 0x0000681C
		private static string a(bm A_0, Type A_1)
		{
			if (A_1 == typeof(Global))
			{
				switch (A_0.g())
				{
				case f.d:
					return Resources.Instance.LicenseKeyTrialExpired;
				case f.e:
					return Resources.Instance.LicenseKeyOlderVersion;
				case f.f:
					return Resources.Instance.LicenseKeyComVersion;
				case f.g:
					return Resources.Instance.LicenseKeyInvalid;
				default:
					return Resources.Instance.ErrorDesc_Unknown;
				}
			}
			else
			{
				string str = string.Format(Resources.Instance.ErrorDesc_0ComponentNotLicensed, A_1.Name);
				switch (A_0.g())
				{
				case f.d:
					return str + " " + Resources.Instance.LicenseKeyTrialExpired;
				case f.e:
					return str + " " + Resources.Instance.LicenseKeyOlderVersion;
				case f.f:
					return str + " " + Resources.Instance.LicenseKeyComVersion;
				case f.g:
					return str + " " + Resources.Instance.LicenseKeyInvalid;
				default:
					return Resources.Instance.ErrorDesc_Unknown;
				}
			}
		}
	}
}
