using System;
using System.Configuration;
using System.Net.Mail;
using System.Threading;

namespace System.Net.Configuration
{
	// Token: 0x02000663 RID: 1635
	internal sealed class SmtpSectionInternal
	{
		// Token: 0x0600329D RID: 12957 RVA: 0x000D6F18 File Offset: 0x000D5F18
		internal SmtpSectionInternal(SmtpSection section)
		{
			this.deliveryMethod = section.DeliveryMethod;
			this.from = section.From;
			this.network = new SmtpNetworkElementInternal(section.Network);
			this.specifiedPickupDirectory = new SmtpSpecifiedPickupDirectoryElementInternal(section.SpecifiedPickupDirectory);
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x0600329E RID: 12958 RVA: 0x000D6F65 File Offset: 0x000D5F65
		internal SmtpDeliveryMethod DeliveryMethod
		{
			get
			{
				return this.deliveryMethod;
			}
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x0600329F RID: 12959 RVA: 0x000D6F6D File Offset: 0x000D5F6D
		internal SmtpNetworkElementInternal Network
		{
			get
			{
				return this.network;
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x060032A0 RID: 12960 RVA: 0x000D6F75 File Offset: 0x000D5F75
		internal string From
		{
			get
			{
				return this.from;
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x060032A1 RID: 12961 RVA: 0x000D6F7D File Offset: 0x000D5F7D
		internal SmtpSpecifiedPickupDirectoryElementInternal SpecifiedPickupDirectory
		{
			get
			{
				return this.specifiedPickupDirectory;
			}
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x060032A2 RID: 12962 RVA: 0x000D6F85 File Offset: 0x000D5F85
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

		// Token: 0x060032A3 RID: 12963 RVA: 0x000D6FA4 File Offset: 0x000D5FA4
		internal static SmtpSectionInternal GetSection()
		{
			SmtpSectionInternal result;
			lock (SmtpSectionInternal.ClassSyncObject)
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

		// Token: 0x04002F5C RID: 12124
		private SmtpDeliveryMethod deliveryMethod;

		// Token: 0x04002F5D RID: 12125
		private string from;

		// Token: 0x04002F5E RID: 12126
		private SmtpNetworkElementInternal network;

		// Token: 0x04002F5F RID: 12127
		private SmtpSpecifiedPickupDirectoryElementInternal specifiedPickupDirectory;

		// Token: 0x04002F60 RID: 12128
		private static object classSyncObject;
	}
}
