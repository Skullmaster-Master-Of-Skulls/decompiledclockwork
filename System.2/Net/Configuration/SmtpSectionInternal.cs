using System;
using System.Configuration;
using System.Net.Mail;
using System.Threading;

namespace System.Net.Configuration
{
	// Token: 0x02000343 RID: 835
	internal sealed class SmtpSectionInternal
	{
		// Token: 0x06001E02 RID: 7682 RVA: 0x0008D3D0 File Offset: 0x0008B5D0
		internal SmtpSectionInternal(SmtpSection section)
		{
			this.deliveryMethod = section.DeliveryMethod;
			this.deliveryFormat = section.DeliveryFormat;
			this.from = section.From;
			this.network = new SmtpNetworkElementInternal(section.Network);
			this.specifiedPickupDirectory = new SmtpSpecifiedPickupDirectoryElementInternal(section.SpecifiedPickupDirectory);
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06001E03 RID: 7683 RVA: 0x0008D429 File Offset: 0x0008B629
		internal SmtpDeliveryMethod DeliveryMethod
		{
			get
			{
				return this.deliveryMethod;
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06001E04 RID: 7684 RVA: 0x0008D431 File Offset: 0x0008B631
		internal SmtpDeliveryFormat DeliveryFormat
		{
			get
			{
				return this.deliveryFormat;
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06001E05 RID: 7685 RVA: 0x0008D439 File Offset: 0x0008B639
		internal SmtpNetworkElementInternal Network
		{
			get
			{
				return this.network;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06001E06 RID: 7686 RVA: 0x0008D441 File Offset: 0x0008B641
		internal string From
		{
			get
			{
				return this.from;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06001E07 RID: 7687 RVA: 0x0008D449 File Offset: 0x0008B649
		internal SmtpSpecifiedPickupDirectoryElementInternal SpecifiedPickupDirectory
		{
			get
			{
				return this.specifiedPickupDirectory;
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06001E08 RID: 7688 RVA: 0x0008D451 File Offset: 0x0008B651
		internal static object ClassSyncObject
		{
			get
			{
				if (SmtpSectionInternal.classSyncObject == null)
				{
					Interlocked.CompareExchange(ref SmtpSectionInternal.classSyncObject, new object(), null);
				}
				return SmtpSectionInternal.classSyncObject;
			}
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x0008D470 File Offset: 0x0008B670
		internal static SmtpSectionInternal GetSection()
		{
			object obj = SmtpSectionInternal.ClassSyncObject;
			SmtpSectionInternal result;
			lock (obj)
			{
				SmtpSection smtpSection = PrivilegedConfigurationManager.GetSection(ConfigurationStrings.SmtpSectionPath) as SmtpSection;
				if (smtpSection == null)
				{
					result = null;
				}
				else
				{
					result = new SmtpSectionInternal(smtpSection);
				}
			}
			return result;
		}

		// Token: 0x04001C9E RID: 7326
		private SmtpDeliveryMethod deliveryMethod;

		// Token: 0x04001C9F RID: 7327
		private SmtpDeliveryFormat deliveryFormat;

		// Token: 0x04001CA0 RID: 7328
		private string from;

		// Token: 0x04001CA1 RID: 7329
		private SmtpNetworkElementInternal network;

		// Token: 0x04001CA2 RID: 7330
		private SmtpSpecifiedPickupDirectoryElementInternal specifiedPickupDirectory;

		// Token: 0x04001CA3 RID: 7331
		private static object classSyncObject;
	}
}
