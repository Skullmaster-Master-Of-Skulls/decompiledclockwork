using System;
using System.IO;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x0200029A RID: 666
	internal class a0
	{
		// Token: 0x06001778 RID: 6008 RVA: 0x0006B07C File Offset: 0x0006A07C
		public static PropertySet a(Stream A_0)
		{
			PropertySet propertySet = new PropertySet(A_0);
			PropertySet result;
			try
			{
				if (propertySet.IsSummaryInformation)
				{
					result = new SummaryInformation(propertySet);
				}
				else if (propertySet.IsDocumentSummaryInformation)
				{
					result = new DocumentSummaryInformation(propertySet);
				}
				else
				{
					result = propertySet;
				}
			}
			catch (UnexpectedPropertySetTypeException)
			{
				throw;
			}
			return result;
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x0006B0CC File Offset: 0x0006A0CC
		public static SummaryInformation b()
		{
			MutablePropertySet mutablePropertySet = new MutablePropertySet();
			((d)mutablePropertySet.FirstSection).a(@as.a);
			SummaryInformation result;
			try
			{
				result = new SummaryInformation(mutablePropertySet);
			}
			catch (UnexpectedPropertySetTypeException a_)
			{
				throw new HPSFRuntimeException(a_);
			}
			return result;
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x0006B114 File Offset: 0x0006A114
		public static DocumentSummaryInformation a()
		{
			MutablePropertySet mutablePropertySet = new MutablePropertySet();
			((d)mutablePropertySet.FirstSection).a(@as.b);
			DocumentSummaryInformation result;
			try
			{
				result = new DocumentSummaryInformation(mutablePropertySet);
			}
			catch (UnexpectedPropertySetTypeException a_)
			{
				throw new HPSFRuntimeException(a_);
			}
			return result;
		}
	}
}
